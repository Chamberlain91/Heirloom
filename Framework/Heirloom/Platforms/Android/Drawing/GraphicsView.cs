using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;

using Android.App;
using Android.Graphics;
using Android.Views;

using Heirloom.Android.EGL;
using Heirloom.Drawing;
using Heirloom.Drawing.OpenGLES;
using Heirloom.Mathematics;

namespace Heirloom.Android
{
    internal sealed class GraphicsView : SurfaceView, IScreen, ISurfaceHolderCallback, IInputSource
    {
        private readonly ESGraphicsContext _esGraphics;
        private readonly int _pixelDensity;
        private IntSize? _fixedSize;

        private bool _isInputEnabled;

        internal EglSurface EglSurface;

        internal GraphicsView(Activity activity, int pixelDensity, MultisampleQuality multisample = MultisampleQuality.None, bool vsync = true)
           : base(activity)
        {
            // Configure holder resolution
            Holder.SetFormat(Format.Rgb888);
            Holder.AddCallback(this);

            // ...
            _pixelDensity = pixelDensity;

            // Construct the default surface and context
            if (GraphicsBackend.Current is ESAndroidGraphicsBackend esBackend)
            {
                // Create default surface
                Surface = new Drawing.Surface(multisample, SurfaceFormat.UnsignedByte, this);

                // Create the graphics context
                _esGraphics = esBackend.CreateGraphics(this, vsync);
            }
            else
            {
                throw new InvalidOperationException("Unable to create graphics context, unknown backend.");
            }

            // Since android can only have the one window/view, set this as the input source.
            Input.SetInputSource(this);
        }

        public GraphicsContext Graphics => _esGraphics;

        public event Action GraphicsEnabled;

        public event Action GraphicsDisable;

        public event Action<IScreen, IntSize> Resized;

        public event Action<IScreen, IntSize> SurfaceResized;

        public IntSize Size
        {
            get => new IntSize(Width, Height);
            set => throw new NotImplementedException("Unable to change size of android view.");
        }

        public Drawing.Surface Surface { get; private set; }

        #region Surface Holder

        void ISurfaceHolderCallback.SurfaceCreated(ISurfaceHolder holder)
        {
            Log.Debug($"[Holder] Surface Created");

            if (GraphicsBackend.Current is ESAndroidGraphicsBackend esBackend)
            {
                Log.Debug($"[EGL] Creating Surface");

                // Create window surface via handle, this informs android to increment the window reference counter.
                EglSurface = Egl.CreateWindowSurface(esBackend.EglContext, AndroidWindow.GetWindowHandle(holder));

                // Launch GL thread, EGL surface has been created.
                if (_esGraphics.IsThreadRunning == false)
                {
                    _esGraphics.StartThread();

                    var w = EglSurface.Width;
                    var h = EglSurface.Height;

                    if (_pixelDensity > 0)
                    {
                        var d = (int) Resources.DisplayMetrics.DensityDpi;

                        //
                        var resolution = AndroidHelper.ComputeAutomaticResolution(w, h, d, _pixelDensity);
                        Log.Debug($"[Holder] Assign Fixed Size ({resolution})");
                        Holder.SetFixedSize(resolution.Width, resolution.Height);

                        // Update backbuffer (fixed resolution)
                        ResizeBackbufferSurface(resolution.Width, resolution.Height);

                        // Assign fixed size
                        _fixedSize = resolution;
                    }
                    else
                    {
                        // Update backbuffer (natural resolution)
                        ResizeBackbufferSurface(w, h);
                    }
                }

                // Notify that we need to rebind the context to the thread. The surface has changed.
                _esGraphics.NotifyMakeCurrent();

                // Notify graphics are available
                GraphicsEnabled?.Invoke();
            }
            else
            {
                throw new InvalidOperationException("Unable to create EGL surface, unknown backend.");
            }
        }

        void ISurfaceHolderCallback.SurfaceChanged(ISurfaceHolder holder, Format format, int width, int height)
        {
            if (_fixedSize.HasValue == false)
            {
                Log.Debug($"[Holder] Surface Changed ({width} by {height})");

                // If the surface size has actually changed...
                if (Surface.Width != width || Surface.Height != height)
                {
                    ResizeBackbufferSurface(width, height);
                    Log.Debug($"[Graphics] Surface Resized to {width} by {height}.");
                }
            }
        }

        private void ResizeBackbufferSurface(int width, int height)
        {
            Surface.SetSize(new IntSize(width, height));
            SurfaceResized?.Invoke(this, Surface.Size);
        }

        void ISurfaceHolderCallback.SurfaceDestroyed(ISurfaceHolder holder)
        {
            Log.Debug($"[Holder] Surface Destroyed");

            if (EglSurface != null)
            {
                Log.Debug($"[EGL] Release Surface");

                // Release window handle, this informs android to decrement the window reference counter.
                AndroidWindow.ReleaseWindowHandle(EglSurface.Handle);

                // Dispose surface
                EglSurface.Dispose();
                EglSurface = null;
            }

            // Notify graphics are not available.
            GraphicsDisable?.Invoke();
        }

        #endregion

        protected override void OnSizeChanged(int w, int h, int oldw, int oldh)
        {
            base.OnSizeChanged(w, h, oldw, oldh);
            Log.Debug($"[Holder] Screen Resize ({w} by {h})");

            // The view has resized. This will likely be a different size than the surface.
            Resized?.Invoke(this, new IntSize(w, h));
        }

        public void Refresh()
        {
            // Process input events
            ProcessInputEvents();

            // Flush all pending graphics commands
            Graphics.CompleteFrame();
        }

        #region Input Source

        private const int MAX_TOUCH_POINTS = 10;

        private readonly Vector[] _touchesPrior = new Vector[MAX_TOUCH_POINTS];
        private readonly Touch[] _touches = new Touch[MAX_TOUCH_POINTS];

        private readonly List<int> _fingers = new List<int>();

        private readonly Queue<Touch> _events = new Queue<Touch>();

        public void Activate()
        {
            _isInputEnabled = true;
        }

        public void Deactivate()
        {
            _isInputEnabled = false;

            // Recycle pooled arrays and clear touch information
            for (var i = 0; i < _touches.Length; i++) { _touches[i] = default; }
            _fingers.Clear();
            _events.Clear();
        }

        internal void ProcessInputEvents()
        {
            // todo: process keyboard?
            ProcessTouchEvents();

            void ProcessTouchEvents()
            {
                // Prepare touch points for new frame
                for (var finger = 0; finger < MAX_TOUCH_POINTS; finger++)
                {
                    var touch = _touches[finger];

                    // Finger has stopped touching
                    if (touch.State == ButtonState.Released) { _fingers.Remove(touch.Finger); }

                    var state = touch.State;

                    // Demote recent states
                    // * Released -> Up
                    // * Pressed -> Down
                    if (touch.State.HasFlag(ButtonState.Recent))
                    {
                        state &= ~ButtonState.Recent;
                    }

                    // Assign touch, removing delta and recent (consumed last frame)
                    _touches[finger] = new Touch(touch.Position, Vector.Zero, touch.Finger, state);
                }

                // Process new events
                var delta = Vector.Zero;
                while (_events.Count > 0)
                {
                    var touch = _events.Dequeue();

                    // Finger has begun touching
                    if (touch.State == ButtonState.Pressed) { _fingers.Add(touch.Finger); }

                    // Compute delta position
                    if (touch.State != ButtonState.Pressed)
                    {
                        var prior = _touchesPrior[touch.Finger];
                        delta += touch.Position - prior;
                    }

                    // Store latest touch information
                    _touchesPrior[touch.Finger] = touch.Position;
                    _touches[touch.Finger] = new Touch(touch.Position, delta, touch.Finger, touch.State);
                }
            }
        }

        public override bool OnTouchEvent(MotionEvent ev)
        {
            if (_isInputEnabled)
            {
                if (ev.ActionMasked == MotionEventActions.Move)
                {
                    // Request a temporary array from the pool
                    for (var i = 0; i < ev.PointerCount; i++)
                    {
                        var position = GetPointerPosition(ev, i);
                        var finger = ev.GetPointerId(i);

                        // Enqueue touch event
                        _events.Enqueue(new Touch(position, Vector.Zero, finger, ButtonState.Down));
                    }
                }
                else
                {
                    var position = GetPointerPosition(ev, ev.ActionIndex);
                    var finger = ev.GetPointerId(ev.ActionIndex);

                    var state = ButtonState.Down;
                    switch (ev.ActionMasked)
                    {
                        case MotionEventActions.Down:
                        case MotionEventActions.PointerDown:
                            state = ButtonState.Pressed;
                            break;

                        case MotionEventActions.Up:
                        case MotionEventActions.PointerUp:
                        case MotionEventActions.Cancel:
                            state = ButtonState.Released;
                            break;
                    }

                    // Enqueue touch event
                    _events.Enqueue(new Touch(position, Vector.Zero, finger, state));
                }
            }

            return _isInputEnabled;

            Vector GetPointerPosition(MotionEvent ev, int i)
            {
                var x = ev.GetX(i);
                var y = ev.GetY(i);

                // Rescale from raw pixels to surface
                x = x / Width * Surface.Width;
                y = y / Height * Surface.Height;

                return new Vector(x, y);
            }
        }

        #region Keyboard

        // todo: implement software keyboard
        public bool SupportsSoftwareKeyboard => false;

        public string TextInput => string.Empty;

        public bool TryGetKey(Key key, out ButtonState state)
        {
            state = default;
            return false;
        }

        public void ShowSoftwareKeyboard()
        {
            throw new NotImplementedException();
        }

        public void HideSoftwareKeyboard()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Mouse

        public Vector MousePosition => Vector.Zero;

        public Vector MouseDelta => Vector.Zero;

        public Vector MouseScroll => Vector.Zero;

        public bool TryGetButton(MouseButton button, out ButtonState state)
        {
            state = default;
            return false;
        }

        #endregion

        #region Touch

        public bool HasTouchSupport => true;

        public int TouchCount => _fingers.Count;

        public Touch GetTouch(int index)
        {
            var finger = _fingers[index];
            return _touches[finger];
        }

        #endregion 

        #endregion
    }
}
