using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Heirloom.GLFW
{
    public static unsafe partial class Glfw
    {
        private const string Library = "glfw3";

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwInit")]
        [return: MarshalAs(UnmanagedType.Bool)]
        // int glfwInit(void);
        public static extern bool Init();

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwTerminate")]
        // void glfwTerminate(void);
        public static extern void Terminate();

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwInitHint")]
        // void glfwInitHint(int hint, int value);
        public static extern void InitHint(int hint, int value);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwGetVersion")]
        // void glfwGetVersion(int* major, int* minor, int* rev);
        public static extern void GetVersion(out int major, out int minor, out int rev);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwGetVersionString")]
        // char* glfwGetVersionString(void);
        public static extern char* GetVersionString();

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwGetError")]
        // int glfwGetError( char** description);
        public static extern int GetError(char** description);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwSetErrorCallback")]
        // GLFWerrorfun glfwSetErrorCallback(GLFWerrorfun cbfun);
        public static extern ErrorCallback SetErrorCallback(ErrorCallback cbfun);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwGetMonitors")]
        // GLFWmonitor** glfwGetMonitors(int* count);
        public static extern Monitor** GetMonitors(out int count);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwGetPrimaryMonitor")]
        // GLFWmonitor* glfwGetPrimaryMonitor(void);
        // [return: MarshalAs(UnmanagedType.Struct)]
        public static extern Monitor* GetPrimaryMonitor();

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwGetMonitorPos")]
        // void glfwGetMonitorPos(GLFWmonitor* monitor, int* xpos, int* ypos);
        public static extern void GetMonitorPosition(Monitor* monitor, out int xpos, out int ypos);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwGetMonitorWorkarea")]
        // void glfwGetMonitorWorkarea(GLFWmonitor* monitor, int* xpos, int* ypos, int* width, int* height);
        public static extern void GetMonitorWorkarea(Monitor* monitor, out int xpos, out int ypos, out int width, out int height);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwGetMonitorPhysicalSize")]
        // void glfwGetMonitorPhysicalSize(GLFWmonitor* monitor, int* widthMM, int* heightMM);
        public static extern void GetMonitorPhysicalSize(Monitor* monitor, out int widthMM, out int heightMM);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwGetMonitorContentScale")]
        // void glfwGetMonitorContentScale(GLFWmonitor* monitor, float* xscale, float* yscale);
        public static extern void GetMonitorContentScale(Monitor* monitor, out float xscale, out float yscale);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwGetMonitorName")]
        // char* glfwGetMonitorName(GLFWmonitor* monitor);
        public static extern char* GetMonitorName(Monitor* monitor);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwSetMonitorUserPointer")]
        // void glfwSetMonitorUserPointer(GLFWmonitor* monitor, void* pointer);
        public static extern void SetMonitorUserPointer(Monitor* monitor, void* pointer);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwGetMonitorUserPointer")]
        // void* glfwGetMonitorUserPointer(GLFWmonitor* monitor);
        public static extern void* GetMonitorUserPointer(Monitor* monitor);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwSetMonitorCallback")]
        // GLFWmonitorfun glfwSetMonitorCallback(GLFWmonitorfun cbfun);
        public static extern MonitorCallback SetMonitorCallback(MonitorCallback cbfun);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwGetVideoModes")]
        // GLFWvidmode* glfwGetVideoModes(GLFWmonitor* monitor, int* count);
        public static extern VideoMode* GetVideoModes(Monitor* monitor, out int count);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwGetVideoMode")]
        // GLFWvidmode* glfwGetVideoMode(GLFWmonitor* monitor);
        public static extern VideoMode* GetVideoMode(Monitor* monitor);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwSetGamma")]
        // void glfwSetGamma(GLFWmonitor* monitor, float gamma);
        public static extern void SetGamma(Monitor* monitor, float gamma);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwGetGammaRamp")]
        // GLFWgammaramp* glfwGetGammaRamp(GLFWmonitor* monitor);
        public static extern GammaRamp* GetGammaRamp(Monitor* monitor);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwSetGammaRamp")]
        // void glfwSetGammaRamp(GLFWmonitor* monitor,  GLFWgammaramp* ramp);
        public static extern void SetGammaRamp(Monitor* monitor, GammaRamp* ramp);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwDefaultWindowHints")]
        // void glfwDefaultWindowHints(void);
        public static extern void DefaultWindowHints();

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwWindowHint")]
        // void glfwWindowHint(int hint, int value);
        public static extern void WindowHint(int hint, int value);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwWindowHintString")]
        // void glfwWindowHintString(int hint,  char* value);
        public static extern void WindowHintString(int hint, string value);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwCreateWindow")]
        [return: MarshalAs(UnmanagedType.Struct)]
        // GLFWwindow* glfwCreateWindow(int width, int height,  char* title, GLFWmonitor* monitor, GLFWwindow* share);
        public static extern Window CreateWindow(int width, int height, string title, Monitor* monitor, [MarshalAs(UnmanagedType.Struct)] Window share);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwDestroyWindow")]
        // void glfwDestroyWindow(GLFWwindow* window);
        public static extern void DestroyWindow([MarshalAs(UnmanagedType.Struct)] Window window);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwWindowShouldClose")]
        [return: MarshalAs(UnmanagedType.Bool)]
        // int glfwWindowShouldClose(GLFWwindow* window);
        public static extern bool WindowShouldClose([MarshalAs(UnmanagedType.Struct)] Window window);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwSetWindowShouldClose")]
        // void glfwSetWindowShouldClose(GLFWwindow* window, int value);
        public static extern void SetWindowShouldClose([MarshalAs(UnmanagedType.Struct)] Window window, int value);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwSetWindowTitle")]
        // void glfwSetWindowTitle(GLFWwindow* window,  char* title);
        public static extern void SetWindowTitle([MarshalAs(UnmanagedType.Struct)] Window window, string title);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwSetWindowIcon")]
        // void glfwSetWindowIcon(GLFWwindow* window, int count,  GLFWimage* images);
        public static extern void SetWindowIcon([MarshalAs(UnmanagedType.Struct)] Window window, int count, Image* images);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwGetWindowPos")]
        // void glfwGetWindowPos(GLFWwindow* window, int* xpos, int* ypos);
        public static extern void GetWindowPos([MarshalAs(UnmanagedType.Struct)] Window window, out int xpos, out int ypos);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwSetWindowPos")]
        // void glfwSetWindowPos(GLFWwindow* window, int xpos, int ypos);
        public static extern void SetWindowPos([MarshalAs(UnmanagedType.Struct)] Window window, int xpos, int ypos);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwGetWindowSize")]
        // void glfwGetWindowSize(GLFWwindow* window, int* width, int* height);
        public static extern void GetWindowSize([MarshalAs(UnmanagedType.Struct)] Window window, out int width, out int height);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwSetWindowSizeLimits")]
        // void glfwSetWindowSizeLimits(GLFWwindow* window, int minwidth, int minheight, int maxwidth, int maxheight);
        public static extern void SetWindowSizeLimits([MarshalAs(UnmanagedType.Struct)] Window window, int minwidth, int minheight, int maxwidth, int maxheight);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwSetWindowAspectRatio")]
        // void glfwSetWindowAspectRatio(GLFWwindow* window, int numer, int denom);
        public static extern void SetWindowAspectRatio([MarshalAs(UnmanagedType.Struct)] Window window, int numer, int denom);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwSetWindowSize")]
        // void glfwSetWindowSize(GLFWwindow* window, int width, int height);
        public static extern void SetWindowSize([MarshalAs(UnmanagedType.Struct)] Window window, int width, int height);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwGetFramebufferSize")]
        // void glfwGetFramebufferSize(GLFWwindow* window, int* width, int* height);
        public static extern void GetFramebufferSize([MarshalAs(UnmanagedType.Struct)] Window window, out int width, out int height);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwGetWindowFrameSize")]
        // void glfwGetWindowFrameSize(GLFWwindow* window, int* left, int* top, int* right, int* bottom);
        public static extern void GetWindowFrameSize([MarshalAs(UnmanagedType.Struct)] Window window, out int left, out int top, out int right, out int bottom);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwGetWindowContentScale")]
        // void glfwGetWindowContentScale(GLFWwindow* window, float* xscale, float* yscale);
        public static extern void GetWindowContentScale([MarshalAs(UnmanagedType.Struct)] Window window, out float xscale, out float yscale);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwGetWindowOpacity")]
        // float glfwGetWindowOpacity(GLFWwindow* window);
        public static extern float GetWindowOpacity([MarshalAs(UnmanagedType.Struct)] Window window);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwSetWindowOpacity")]
        // void glfwSetWindowOpacity(GLFWwindow* window, float opacity);
        public static extern void SetWindowOpacity([MarshalAs(UnmanagedType.Struct)] Window window, float opacity);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwIconifyWindow")]
        // void glfwIconifyWindow(GLFWwindow* window);
        public static extern void IconifyWindow([MarshalAs(UnmanagedType.Struct)] Window window);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwRestoreWindow")]
        // void glfwRestoreWindow(GLFWwindow* window);
        public static extern void RestoreWindow([MarshalAs(UnmanagedType.Struct)] Window window);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwMaximizeWindow")]
        // void glfwMaximizeWindow(GLFWwindow* window);
        public static extern void MaximizeWindow([MarshalAs(UnmanagedType.Struct)] Window window);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwShowWindow")]
        // void glfwShowWindow(GLFWwindow* window);
        public static extern void ShowWindow([MarshalAs(UnmanagedType.Struct)] Window window);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwHideWindow")]
        // void glfwHideWindow(GLFWwindow* window);
        public static extern void HideWindow([MarshalAs(UnmanagedType.Struct)] Window window);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwFocusWindow")]
        // void glfwFocusWindow(GLFWwindow* window);
        public static extern void FocusWindow([MarshalAs(UnmanagedType.Struct)] Window window);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwRequestWindowAttention")]
        // void glfwRequestWindowAttention(GLFWwindow* window);
        public static extern void RequestWindowAttention([MarshalAs(UnmanagedType.Struct)] Window window);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwGetWindowMonitor")]
        // GLFWmonitor* glfwGetWindowMonitor(GLFWwindow* window);
        public static extern Monitor* GetWindowMonitor([MarshalAs(UnmanagedType.Struct)] Window window);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwSetWindowMonitor")]
        // void glfwSetWindowMonitor(GLFWwindow* window, GLFWmonitor* monitor, int xpos, int ypos, int width, int height, int refreshRate);
        public static extern void SetWindowMonitor([MarshalAs(UnmanagedType.Struct)] Window window, Monitor* monitor, int xpos, int ypos, int width, int height, int refreshRate);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwGetWindowAttrib")]
        // int glfwGetWindowAttrib(GLFWwindow* window, int attrib);
        public static extern int GetWindowAttrib([MarshalAs(UnmanagedType.Struct)] Window window, int attrib);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwSetWindowAttrib")]
        // void glfwSetWindowAttrib(GLFWwindow* window, int attrib, int value);
        public static extern void SetWindowAttrib([MarshalAs(UnmanagedType.Struct)] Window window, int attrib, int value);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwSetWindowUserPointer")]
        // void glfwSetWindowUserPointer(GLFWwindow* window, void* pointer);
        public static extern void SetWindowUserPointer([MarshalAs(UnmanagedType.Struct)] Window window, void* pointer);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwGetWindowUserPointer")]
        // void* glfwGetWindowUserPointer(GLFWwindow* window);
        public static extern void* GetWindowUserPointer([MarshalAs(UnmanagedType.Struct)] Window window);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwSetWindowPosCallback")]
        // GLFWwindowposfun glfwSetWindowPosCallback(GLFWwindow* window, GLFWwindowposfun cbfun);
        public static extern WindowPositionCallback SetWindowPositionCallback([MarshalAs(UnmanagedType.Struct)] Window window, WindowPositionCallback cbfun);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwSetWindowSizeCallback", CallingConvention = CallingConvention.Cdecl)]
        // GLFWwindowsizefun glfwSetWindowSizeCallback(GLFWwindow* window, GLFWwindowsizefun cbfun);
        public static extern WindowSizeCallback SetWindowSizeCallback([MarshalAs(UnmanagedType.Struct)] Window window, WindowSizeCallback cbfun);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwSetWindowCloseCallback")]
        // GLFWwindowclosefun glfwSetWindowCloseCallback(GLFWwindow* window, GLFWwindowclosefun cbfun);
        public static extern WindowCloseCallback SetWindowCloseCallback([MarshalAs(UnmanagedType.Struct)] Window window, WindowCloseCallback cbfun);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwSetWindowRefreshCallback")]
        // GLFWwindowrefreshfun glfwSetWindowRefreshCallback(GLFWwindow* window, GLFWwindowrefreshfun cbfun);
        public static extern WindowRefreshCallback SetWindowRefreshCallback([MarshalAs(UnmanagedType.Struct)] Window window, WindowRefreshCallback cbfun);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwSetWindowFocusCallback")]
        // GLFWwindowfocusfun glfwSetWindowFocusCallback(GLFWwindow* window, GLFWwindowfocusfun cbfun);
        public static extern WindowFocusCallback SetWindowFocusCallback([MarshalAs(UnmanagedType.Struct)] Window window, WindowFocusCallback cbfun);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwSetWindowIconifyCallback")]
        // GLFWwindowiconifyfun glfwSetWindowIconifyCallback(GLFWwindow* window, GLFWwindowiconifyfun cbfun);
        public static extern WindowIconifyCallback SetWindowIconifyCallback([MarshalAs(UnmanagedType.Struct)] Window window, WindowIconifyCallback cbfun);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwSetWindowMaximizeCallback")]
        // GLFWwindowmaximizefun glfwSetWindowMaximizeCallback(GLFWwindow* window, GLFWwindowmaximizefun cbfun);
        public static extern WindowMaximizeCallback SetWindowMaximizeCallback([MarshalAs(UnmanagedType.Struct)] Window window, WindowMaximizeCallback cbfun);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwSetFramebufferSizeCallback")]
        // GLFWframebuffersizefun glfwSetFramebufferSizeCallback(GLFWwindow* window, GLFWframebuffersizefun cbfun);
        public static extern FramebufferSizeCallback SetFramebufferSizeCallback([MarshalAs(UnmanagedType.Struct)] Window window, FramebufferSizeCallback cbfun);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwSetWindowContentScaleCallback")]
        // GLFWwindowcontentscalefun glfwSetWindowContentScaleCallback(GLFWwindow* window, GLFWwindowcontentscalefun cbfun);
        public static extern WindowContentScaleCallback SetWindowContentScaleCallback([MarshalAs(UnmanagedType.Struct)] Window window, WindowContentScaleCallback cbfun);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwPollEvents")]
        // void glfwPollEvents(void);
        public static extern void PollEvents();

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwWaitEvents")]
        // void glfwWaitEvents(void);
        public static extern void WaitEvents();

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwWaitEventsTimeout")]
        // void glfwWaitEventsTimeout(double timeout);
        public static extern void WaitEventsTimeout(double timeout);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwPostEmptyEvent")]
        // void glfwPostEmptyEvent(void);
        public static extern void PostEmptyEvent();

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwGetInputMode")]
        // int glfwGetInputMode(GLFWwindow* window, int mode);
        public static extern int GetInputMode([MarshalAs(UnmanagedType.Struct)] Window window, int mode);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwSetInputMode")]
        // void glfwSetInputMode(GLFWwindow* window, int mode, int value);
        public static extern void SetInputMode([MarshalAs(UnmanagedType.Struct)] Window window, int mode, int value);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwRawMouseMotionSupported")]
        [return: MarshalAs(UnmanagedType.Bool)]
        // int glfwRawMouseMotionSupported(void);
        public static extern bool RawMouseMotionSupported();

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwGetKeyName")]
        // char* glfwGetKeyName(int key, int scancode);
        public static extern char* GetKeyName(int key, int scancode);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwGetKeyScancode")]
        // int glfwGetKeyScancode(int key);
        public static extern int GetKeyScancode(int key);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwGetKey")]
        // int glfwGetKey(GLFWwindow* window, int key);
        public static extern int GetKey([MarshalAs(UnmanagedType.Struct)] Window window, int key);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwGetMouseButton")]
        // int glfwGetMouseButton(GLFWwindow* window, int button);
        public static extern int GetMouseButton([MarshalAs(UnmanagedType.Struct)] Window window, int button);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwGetCursorPos")]
        // void glfwGetCursorPos(GLFWwindow* window, double* xpos, double* ypos);
        public static extern void GetCursorPos([MarshalAs(UnmanagedType.Struct)] Window window, out double xpos, out double ypos);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwSetCursorPos")]
        // void glfwSetCursorPos(GLFWwindow* window, double xpos, double ypos);
        public static extern void SetCursorPos([MarshalAs(UnmanagedType.Struct)] Window window, double xpos, double ypos);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwCreateCursor")]
        // GLFWcursor* glfwCreateCursor( GLFWimage* image, int xhot, int yhot);
        public static extern Cursor* CreateCursor(Image* image, int xhot, int yhot);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwCreateStandardCursor")]
        // GLFWcursor* glfwCreateStandardCursor(int shape);
        public static extern Cursor* CreateStandardCursor(int shape);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwDestroyCursor")]
        // void glfwDestroyCursor(GLFWcursor* cursor);
        public static extern void DestroyCursor(Cursor* cursor);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwSetCursor")]
        // void glfwSetCursor(GLFWwindow* window, GLFWcursor* cursor);
        public static extern void SetCursor([MarshalAs(UnmanagedType.Struct)] Window window, Cursor* cursor);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwSetKeyCallback")]
        // GLFWkeyfun glfwSetKeyCallback(GLFWwindow* window, GLFWkeyfun cbfun);
        public static extern KeyCallback SetKeyCallback([MarshalAs(UnmanagedType.Struct)] Window window, KeyCallback cbfun);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwSetCharCallback")]
        // GLFWcharfun glfwSetCharCallback(GLFWwindow* window, GLFWcharfun cbfun);
        public static extern CharCallback SetCharCallback([MarshalAs(UnmanagedType.Struct)] Window window, CharCallback cbfun);

        [SuppressUnmanagedCodeSecurity]
        [Obsolete, DllImport(Library, EntryPoint = "glfwSetCharModsCallback")]
        // GLFWcharmodsfun glfwSetCharModsCallback(GLFWwindow* window, GLFWcharmodsfun cbfun);
        public static extern CharModifierCallback SetCharModsCallback([MarshalAs(UnmanagedType.Struct)] Window window, CharModifierCallback cbfun);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwSetMouseButtonCallback")]
        // GLFWmousebuttonfun glfwSetMouseButtonCallback(GLFWwindow* window, GLFWmousebuttonfun cbfun);
        public static extern MouseButtonCallback SetMouseButtonCallback([MarshalAs(UnmanagedType.Struct)] Window window, MouseButtonCallback cbfun);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwSetCursorPosCallback")]
        // GLFWcursorposfun glfwSetCursorPosCallback(GLFWwindow* window, GLFWcursorposfun cbfun);
        public static extern CursorPositionCallback SetCursorPosCallback([MarshalAs(UnmanagedType.Struct)] Window window, CursorPositionCallback cbfun);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwSetCursorEnterCallback")]
        // GLFWcursorenterfun glfwSetCursorEnterCallback(GLFWwindow* window, GLFWcursorenterfun cbfun);
        public static extern CursorEnterCallback SetCursorEnterCallback([MarshalAs(UnmanagedType.Struct)] Window window, CursorEnterCallback cbfun);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwSetScrollCallback")]
        // GLFWscrollfun glfwSetScrollCallback(GLFWwindow* window, GLFWscrollfun cbfun);
        public static extern ScrollCallback SetScrollCallback([MarshalAs(UnmanagedType.Struct)] Window window, ScrollCallback cbfun);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwSetDropCallback")]
        // GLFWdropfun glfwSetDropCallback(GLFWwindow* window, GLFWdropfun cbfun);
        public static extern DropCallback SetDropCallback([MarshalAs(UnmanagedType.Struct)] Window window, DropCallback cbfun);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwJoystickPresent")]
        [return: MarshalAs(UnmanagedType.Bool)]
        // int glfwJoystickPresent(int jid);
        public static extern bool JoystickPresent(int jid);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwGetJoystickAxes")]
        // float* glfwGetJoystickAxes(int jid, int* count);
        public static extern float* GetJoystickAxes(int jid, out int count);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwGetJoystickButtons")]
        // unsigned char* glfwGetJoystickButtons(int jid, int* count);
        public static extern byte* GetJoystickButtons(int jid, out int count);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwGetJoystickHats")]
        // unsigned char* glfwGetJoystickHats(int jid, int* count);
        public static extern byte* GetJoystickHats(int jid, out int count);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwGetJoystickName")]
        // char* glfwGetJoystickName(int jid);
        public static extern char* GetJoystickName(int jid);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwGetJoystickGUID")]
        // char* glfwGetJoystickGUID(int jid);
        public static extern char* GetJoystickGUID(int jid);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwSetJoystickUserPointer")]
        // void glfwSetJoystickUserPointer(int jid, void* pointer);
        public static extern void SetJoystickUserPointer(int jid, void* pointer);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwGetJoystickUserPointer")]
        // void* glfwGetJoystickUserPointer(int jid);
        public static extern void* GetJoystickUserPointer(int jid);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwJoystickIsGamepad")]
        // int glfwJoystickIsGamepad(int jid);
        public static extern int JoystickIsGamepad(int jid);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwSetJoystickCallback")]
        // GLFWjoystickfun glfwSetJoystickCallback(GLFWjoystickfun cbfun);
        public static extern JoystickCallback SetJoystickCallback(JoystickCallback cbfun);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwUpdateGamepadMappings")]
        // int glfwUpdateGamepadMappings( char* string);
        public static extern int UpdateGamepadMappings(char* str);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwGetGamepadName")]
        // char* glfwGetGamepadName(int jid);
        public static extern char* GetGamepadName(int jid);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwGetGamepadState")]
        // int glfwGetGamepadState(int jid, GLFWgamepadstate* state);
        public static extern int GetGamepadState(int jid, out GamepadState state);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwSetClipboardString")]
        // void glfwSetClipboardString(GLFWwindow* window,  char* string);
        public static extern void SetClipboardString([MarshalAs(UnmanagedType.Struct)] Window window, string str);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwGetClipboardString")]
        // char* glfwGetClipboardString(GLFWwindow* window);
        public static extern char* GetClipboardString([MarshalAs(UnmanagedType.Struct)] Window window);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwGetTime")]
        // double glfwGetTime(void);
        public static extern double GetTime();

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwSetTime")]
        // void glfwSetTime(double time);
        public static extern void SetTime(double time);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwGetTimerValue")]
        // uint64_t glfwGetTimerValue(void);
        public static extern ulong GetTimerValue();

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwGetTimerFrequency")]
        // uint64_t glfwGetTimerFrequency(void);
        public static extern ulong GetTimerFrequency();

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwMakeContextCurrent")]
        // void glfwMakeContextCurrent(GLFWwindow* window);
        public static extern void MakeContextCurrent([MarshalAs(UnmanagedType.Struct)]Window window);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwGetCurrentContext")]
        [return: MarshalAs(UnmanagedType.Struct)]
        // GLFWwindow* glfwGetCurrentContext(void);
        public static extern Window GetCurrentContext();

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwSwapBuffers")]
        // void glfwSwapBuffers(GLFWwindow* window);
        public static extern void SwapBuffers([MarshalAs(UnmanagedType.Struct)] Window window);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwSwapInterval")]
        // void glfwSwapInterval(int interval);
        public static extern void SwapInterval(int interval);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwExtensionSupported")]
        [return: MarshalAs(UnmanagedType.Bool)]
        // int glfwExtensionSupported( char* extension);
        public static extern bool ExtensionSupported(string extension);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwGetProcAddress")]
        // GLFWglproc glfwGetProcAddress( char* procname);
        public static extern IntPtr GetProcAddress(string procname);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwVulkanSupported")]
        [return: MarshalAs(UnmanagedType.Bool)]
        // int glfwVulkanSupported(void);
        public static extern bool VulkanSupported();

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwGetRequiredInstanceExtensions")]
        // char** glfwGetRequiredInstanceExtensions(uint32_t* count);
        public static extern char** GetRequiredInstanceExtensions(out uint count);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwGetInstanceProcAddress")]
        // GLFWvkproc glfwGetInstanceProcAddress(VkInstance instance,  char* procname);
        public static extern IntPtr GetInstanceProcAddress(VkInstance instance, string procname);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwGetPhysicalDevicePresentationSupport")]
        [return: MarshalAs(UnmanagedType.Bool)]
        // int glfwGetPhysicalDevicePresentationSupport(VkInstance instance, VkPhysicalDevice device, uint32_t queuefamily);
        public static extern bool GetPhysicalDevicePresentationSupport(VkInstance instance, VkPhysicalDevice device, uint queuefamily);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(Library, EntryPoint = "glfwCreateWindowSurface")]
        // VkResult glfwCreateWindowSurface(VkInstance instance, GLFWwindow* window,  VkAllocationCallbacks* allocator, VkSurfaceKHR* surface);
        public static extern VkResult CreateWindowSurface(VkInstance instance, Window* window, VkAllocationCallbacks* allocator, out VkSurfaceKHR surface);
    }
}
