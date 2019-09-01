using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Heirloom.OpenGLES.Platform
{
    public static unsafe partial class Egl
    {
        // TODO: [ThreadStatic]
        private static readonly Dictionary<Thread, EglContext> _threadContexts;
        private static readonly Dictionary<Thread, EglSurface> _threadDrawSurfaces;
        private static readonly Dictionary<Thread, EglSurface> _threadReadSurfaces;
        private static readonly Dictionary<Thread, EglDisplay> _threadDisplays;

        private static readonly Dictionary<IntPtr, EglDisplay> _displays;
        private static readonly Dictionary<IntPtr, EglConfig> _configs;
        private static readonly Dictionary<IntPtr, EglContext> _contexts;
        private static readonly Dictionary<IntPtr, EglSurface> _surfaces;

        static Egl()
        {
            _threadContexts = new Dictionary<Thread, EglContext>();
            _threadDrawSurfaces = new Dictionary<Thread, EglSurface>();
            _threadReadSurfaces = new Dictionary<Thread, EglSurface>();
            _threadDisplays = new Dictionary<Thread, EglDisplay>();

            _displays = new Dictionary<IntPtr, EglDisplay>();
            _configs = new Dictionary<IntPtr, EglConfig>();
            _contexts = new Dictionary<IntPtr, EglContext>();
            _surfaces = new Dictionary<IntPtr, EglSurface>();
        }

        #region Api

        internal static EglBindApi QueryAPI()
        {
            return eglQueryAPI();
        }

        internal static void BindAPI(EglBindApi api)
        {
            eglBindAPI(api);
        }

        #endregion

        #region Query

        internal static string QueryString(EglDisplay display, EglStringQuery name)
        {
            return new string(eglQueryString(display.Address, name));
        }

        internal static bool QuerySurface(EglDisplay display, EglSurface surface, EglSurfaceQuery name, out int result)
        {
            return eglQuerySurface(display.Address, surface.Address, name, out result);
        }

        #endregion

        #region Initialize / Terminate

        public static bool Initialize(EglDisplay display, out int major, out int minor)
        {
            int maj, min;
            var b = eglInitialize(display.Address, &maj, &min);
            if (!b)
            {
                throw new EglException("Unable to initialize display");
            }

            // 
            major = maj;
            minor = min;

            return b;
        }

        public static bool Initialize(EglDisplay display)
        {
            return eglInitialize(display.Address, (int*) 0, (int*) 0);
        }

        public static bool Terminate(EglDisplay display)
        {
            return eglTerminate(display.Address);
        }

        #endregion

        #region Context

        public static EglContext CreateContext(int contextVersion = 3, EglContext shareContext = null)
        {
            return CreateContext(GetDisplay(IntPtr.Zero), ChooseConfig(new EglConfigAttributes(8, 24, 8)), contextVersion, shareContext);
        }

        public static EglContext CreateContext(EglConfig config, int contextVersion = 3, EglContext shareContext = null)
        {
            return CreateContext(GetDisplay(IntPtr.Zero), config, contextVersion, shareContext);
        }

        public static EglContext CreateContext(EglConfigAttributes attr, int contextVersion = 3, EglContext shareContext = null)
        {
            return CreateContext(GetDisplay(IntPtr.Zero), ChooseConfig(attr), contextVersion, shareContext);
        }

        public static EglContext CreateContext(EglDisplay display, EglConfig config, int contextVersion = 3, EglContext shareContext = null)
        {
            var attribs = new[] { EGL_CONTEXT_CLIENT_VERSION, contextVersion, EGL_NONE };
            var contextAddress = eglCreateContext(display.Address, config.Address, shareContext?.Address ?? IntPtr.Zero, attribs);
            if (contextAddress == NO_CONTEXT)
            {
                throw new EglException("Unable to create EGL context");
            }

            var context = new EglContext(config, contextAddress);
            return _contexts[contextAddress] = context;
        }

        public static bool DestroyContext(EglContext context)
        {
            return DestroyContext(GetDisplay(IntPtr.Zero), context);
        }

        public static bool DestroyContext(EglDisplay display, EglContext context)
        {
            if (eglDestroyContext(display.Address, context.Address))
            {
                _contexts.Remove(context.Address);
                context.Address = IntPtr.Zero;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool ReleaseThread()
        {
            if (eglReleaseThread())
            {
                var thread = Thread.CurrentThread;

                // 
                _threadContexts.Remove(thread);
                _threadDrawSurfaces.Remove(thread);
                _threadReadSurfaces.Remove(thread);
                _threadDisplays.Remove(thread);

                return true;
            }
            else
            {
                throw new EglException("Unable to release egl-state from thread");
            }
        }

        public static void MakeCurrent(EglSurface surface, EglContext context)
        {
            MakeCurrent(GetDisplay(IntPtr.Zero), surface, surface, context);
        }

        public static void MakeCurrent(EglDisplay display, EglSurface surface, EglContext context)
        {
            MakeCurrent(display, surface, surface, context);
        }

        public static void MakeCurrent(EglSurface drawSurface, EglSurface readSurface, EglContext context)
        {
            MakeCurrent(GetDisplay(IntPtr.Zero), drawSurface, readSurface, context);
        }

        public static void MakeCurrent(EglDisplay display, EglSurface drawSurface, EglSurface readSurface, EglContext context)
        {
            if (eglMakeCurrent(display.Address, drawSurface?.Address ?? NO_SURFACE, readSurface?.Address ?? NO_SURFACE, context?.Address ?? NO_CONTEXT))
            {
                var thread = Thread.CurrentThread;

                // 
                _threadContexts[thread] = context;
                _threadDrawSurfaces[thread] = drawSurface;
                _threadReadSurfaces[thread] = readSurface;
                _threadDisplays[thread] = display;
            }
            else
            {
                // throw new EglException($"Unable make surface + context current");
            }
        }

        public static EglContext GetCurrentContext()
        {
            var thread = Thread.CurrentThread;
            if (_threadContexts.ContainsKey(thread))
            {
                return _threadContexts[thread];
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region Display

        public static EglDisplay GetDisplay()
        {
            return GetDisplay(IntPtr.Zero);
        }

        internal static bool SwapInterval(EglDisplay display, int interval)
        {
            return eglSwapInterval(display.Address, interval);
        }

        private enum PlatformType
        {
            /// <summary>
            /// EGL_PLATFORM_ANGLE_TYPE_OPENGL_ANGLE
            /// </summary>
            OpenGL = 0x320D,

            /// <summary>
            /// EGL_PLATFORM_ANGLE_TYPE_OPENGLES_ANGLE
            /// </summary>
            OpenGLES = 0x320E,

            /// <summary>
            /// EGL_PLATFORM_ANGLE_TYPE_D3D11_ANGLE
            /// </summary>
            D3D11 = 0x3208,

            /// <summary>
            /// EGL_PLATFORM_ANGLE_TYPE_D3D9_ANGLE
            /// </summary>
            D3D9 = 0x3207
        }

        private static IntPtr GetAnglePlatformDisplay(IntPtr handle, int maj, int min, PlatformType type)
        {
            try
            {
                const int EGL_PLATFORM_ANGLE_ANGLE = 0x3202;
                const int EGL_PLATFORM_ANGLE_MAX_VERSION_MAJOR_ANGLE = 0x3204;
                const int EGL_PLATFORM_ANGLE_MAX_VERSION_MINOR_ANGLE = 0x3205;
                const int EGL_PLATFORM_ANGLE_ENABLE_AUTOMATIC_TRIM_ANGLE = 0x320F;

                const int EGL_PLATFORM_ANGLE_DEVICE_TYPE_ANGLE = 0x3209;
                const int EGL_PLATFORM_ANGLE_DEVICE_TYPE_HARDWARE_ANGLE = 0x320A;
                const int EGL_PLATFORM_ANGLE_DEVICE_TYPE_NULL_ANGLE = 0x345E;

                const int EGL_PLATFORM_ANGLE_TYPE_ANGLE = 0x3203;
                const int EGL_PLATFORM_ANGLE_TYPE_DEFAULT_ANGLE = 0x3206;
                const int EGL_PLATFORM_ANGLE_TYPE_OPENGL_ANGLE = 0x320D;
                const int EGL_PLATFORM_ANGLE_TYPE_OPENGLES_ANGLE = 0x320E;

                // Try ANGLE OpenGL Platform
                return eglGetPlatformDisplayEXT(EGL_PLATFORM_ANGLE_ANGLE, handle,
                    new int[] {
                        EGL_PLATFORM_ANGLE_TYPE_ANGLE, (int) type,
                        EGL_NONE,
                    });
            }
            catch
            {
                // Something went wrong, so no display.
                return NO_DISPLAY;
            }
        }

        public static EglDisplay GetDisplay(IntPtr native_display)
        {
            if (_displays.ContainsKey(native_display))
            {
                return _displays[native_display];
            }
            else
            {
                var displayAddress = NO_DISPLAY;

                // Try to get Angle OpenGL Display
                displayAddress = GetAnglePlatformDisplay(native_display, 3, 3, PlatformType.OpenGL);
                if (displayAddress != NO_DISPLAY)
                {
                    Console.WriteLine($"Angle: Returning OpenGL Display - {displayAddress}.");
                }

                // Try to get Angle Direct3D 11 Display
                if (displayAddress == NO_DISPLAY)
                {
                    displayAddress = GetAnglePlatformDisplay(native_display, 11, 0, PlatformType.OpenGLES);
                    if (displayAddress != NO_DISPLAY)
                    {
                        Console.WriteLine($"Angle: Returning GLES Display - {displayAddress}.");
                    }
                }

                // Try to get Angle Direct3D 11 Display
                if (displayAddress == NO_DISPLAY)
                {
                    displayAddress = GetAnglePlatformDisplay(native_display, 11, 0, PlatformType.D3D11);
                    if (displayAddress != NO_DISPLAY)
                    {
                        Console.WriteLine($"Angle: Returning Direct3D 11 Display - {displayAddress}.");
                    }
                }

                // Try EGL Default Display
                if (displayAddress == NO_DISPLAY)
                {
                    displayAddress = eglGetDisplay(native_display);
                }

                // Throw exception if we haven't acquired a display.
                if (displayAddress == NO_DISPLAY)
                {
                    throw new EglException("Unable to get display");
                }

                // Create display
                var display = new EglDisplay(displayAddress);
                _displays[native_display] = display;
                return display;
            }
        }

        public static EglDisplay GetCurrentDisplay()
        {
            var thread = Thread.CurrentThread;
            if (_threadDisplays.ContainsKey(thread))
            {
                return _threadDisplays[thread];
            }
            else
            {
                return null;
            }
        }

        internal static bool SwapBuffers(EglDisplay display, EglSurface surface)
        {
            return eglSwapBuffers(display.Address, surface.Address);
        }

        #endregion

        #region Surface

        public static EglSurface CreateWindowSurface(EglContext context, IntPtr handle)
        {
            return CreateWindowSurface(GetDisplay(IntPtr.Zero), context, handle);
        }

        public static EglSurface CreateWindowSurface(EglDisplay display, EglContext context, IntPtr handle)
        {
            var surfaceAddress = eglCreateWindowSurface(display.Address, context.Config.Address, handle, null);
            if (surfaceAddress == NO_SURFACE)
            {
                throw new EglException("Unable to create EGL window surface");
            }

            var surface = new EglSurface(display, surfaceAddress, handle);
            return _surfaces[surfaceAddress] = surface;
        }

        public static EglSurface CreatePbufferSurface(EglContext context, int width, int height)
        {
            return CreatePbufferSurface(GetDisplay(IntPtr.Zero), context, width, height);
        }

        public static EglSurface CreatePbufferSurface(EglDisplay display, EglContext context, int width, int height)
        {
            fixed (int* attr_ptr = new int[] {
                EGL_WIDTH, width,
                EGL_HEIGHT, height,
                EGL_NONE
            })
            {
                var surfaceAddress = eglCreatePbufferSurface(display.Address, context.Config.Address, attr_ptr);
                if (surfaceAddress == NO_SURFACE)
                {
                    throw new EglException("Unable to create EGL pbuffer surface");
                }

                var surface = new EglSurface(display, surfaceAddress, IntPtr.Zero);
                return _surfaces[surfaceAddress] = surface;
            }
        }

        public static bool DestroySurface(EglDisplay display, EglSurface surface)
        {
            if (eglDestroySurface(display.Address, surface.Address))
            {
                _surfaces.Remove(surface.Address);
                surface.Address = IntPtr.Zero;
                return true;
            }
            else
            {
                return false;
            }
        }

        internal static bool SetSurfaceAttribute(EglDisplay display, EglSurface surface, int name, int value)
        {
            return eglSurfaceAttrib(display.Address, surface.Address, name, value);
        }

        public static EglSurface GetCurrentWriteSurface()
        {
            var thread = Thread.CurrentThread;
            if (_threadReadSurfaces.ContainsKey(thread))
            {
                return _threadReadSurfaces[thread];
            }
            else
            {
                return null;
            }
        }

        public static EglSurface GetCurrentReadSurface()
        {
            var thread = Thread.CurrentThread;
            if (_threadReadSurfaces.ContainsKey(thread))
            {
                return _threadReadSurfaces[thread];
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region Config

        public static EglConfig[] GetConfigs(EglDisplay display)
        {
            var configAddrs = new IntPtr[64];
            if (eglGetConfigs(display.Address, configAddrs, configAddrs.Length, out var numConfigs))
            {
                var configs = new EglConfig[numConfigs];
                for (var i = 0; i < configs.Length; i++)
                {
                    var address = configAddrs[i];
                    if (_configs.ContainsKey(address))
                    {
                        configs[i] = _configs[address];
                    }
                    else
                    {
                        configs[i] = new EglConfig(display, configAddrs[i]);
                        _configs[address] = configs[i];
                    }
                }

                return configs;
            }
            else
            {
                throw new EglException("Unable to get all EGL configurations");
            }
        }

        public static EglConfig ChooseConfig(EglConfigAttributes attr)
        {
            return ChooseConfig(GetDisplay(IntPtr.Zero), attr);
        }

        public static EglConfig ChooseConfig(EglDisplay display, EglConfigAttributes attr)
        {
            return ChooseConfigs(display, attr).FirstOrDefault();
        }

        public static EglConfig[] ChooseConfigs(EglConfigAttributes attr)
        {
            return ChooseConfigs(GetDisplay(IntPtr.Zero), attr);
        }

        public static EglConfig[] ChooseConfigs(EglDisplay display, EglConfigAttributes attr)
        {
            var attribs = new[]
            {
                (int) EglConfigAttribute.RedSize, attr.RedBits,
                (int) EglConfigAttribute.GreenSize, attr.GreenBits,
                (int) EglConfigAttribute.BlueSize, attr.BlueBits,
                (int) EglConfigAttribute.AlphaSize, attr.AlphaBits,
                //
                (int) EglConfigAttribute.DepthSize, attr.DepthBits,
                (int) EglConfigAttribute.StencilSize, attr.StencilBits,
                //
                (int) EglConfigAttribute.Samples, attr.Samples,
                //
                (int) EglConfigAttribute.RenderableType,(int) attr.RenderableType,
                //
                (int) EglConfigAttribute.SurfaceType,(int) attr.SurfaceType,
                EGL_NONE
            };

            var configAddrs = new IntPtr[64];
            if (eglChooseConfig(display.Address, attribs, configAddrs, configAddrs.Length, out var numConfigs))
            {
                var configs = new EglConfig[numConfigs];
                for (var i = 0; i < numConfigs; i++)
                {
                    var address = configAddrs[i];
                    if (_configs.ContainsKey(address))
                    {
                        configs[i] = _configs[address];
                    }
                    else
                    {
                        configs[i] = new EglConfig(display, address);
                        _configs[address] = configs[i];
                    }
                }

                return configs;
            }
            else
            {
                throw new EglException("Unable to choose EGL configurations");
            }
        }

        internal static bool GetConfigAttrib(EglDisplay display, EglConfig config, EglConfigAttribute attribute, out int value)
        {
            return eglGetConfigAttrib(display.Address, config.Address, attribute, out value);
        }

        #endregion

        #region Error

        public static EglErrorCode GetError()
        {
            return eglGetError();
        }

        public static string GetErrorMessage(EglErrorCode code)
        {
            switch (code)
            {
                case EglErrorCode.Success:
                    return "The last function succeeded without error.";

                case EglErrorCode.NotInitialized:
                    return "EGL is not initialized, or could not be initialized, for the specified EGL display connection.";

                case EglErrorCode.BadAccess:
                    return "EGL cannot access a requested resource (for example a context is bound in another thread).";

                case EglErrorCode.BadAllocation:
                    return "EGL failed to allocate resources for the requested operation.";

                case EglErrorCode.BadAttribute:
                    return "An unrecognized attribute or attribute value was passed in the attribute list.";

                case EglErrorCode.BadContext:
                    return "An EGLContext argument does not name a valid EGL rendering context.";

                case EglErrorCode.BadConfig:
                    return "An IntPtr argument does not name a valid EGL frame buffer configuration.";

                case EglErrorCode.BadCurrentSurface:
                    return "The current surface of the calling thread is a window, pixel buffer or pixmap that is no longer valid.";

                case EglErrorCode.BadDisplay:
                    return "An IntPtr argument does not name a valid EGL display connection.";

                case EglErrorCode.BadSurface:
                    return "An IntPtr argument does not name a valid surface (window, pixel buffer or pixmap) configured for GL rendering.";

                case EglErrorCode.BadMatch:
                    return "Arguments are inconsistent (for example, a valid context requires buffers not supplied by a valid surface).";

                case EglErrorCode.BadParameter:
                    return "One or more argument values are invalid.";

                case EglErrorCode.BadNativePixmap:
                    return "A NativePixmapType argument does not refer to a valid native pixmap.";

                case EglErrorCode.BadNativeWindow:
                    return "A NativeWindowType argument does not refer to a valid native window.";

                case EglErrorCode.ContextLost:
                    return "The EGL context was lost. The application must destroy all contexts and reinitialise OpenGL ES state and objects to continue rendering.";

                default:
                    return $"An unknown EGL error occured [{code}] (or there is no known message for this error code).";
            }
        }

        #endregion

        public static IntPtr GetProcAddress(string procname)
        {
            return (IntPtr) eglGetProcAddress(procname);
        }
    }
}
