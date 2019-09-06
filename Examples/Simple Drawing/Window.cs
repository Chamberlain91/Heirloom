using Heirloom.GLFW3;

namespace Heirloom.Desktop
{
    public class Window
    {
        public bool IsClosed { get; }

        public bool IsVisible { get; }

        public int Width => _width;

        public int Height => _height;

        public string Title
        {
            get => _title;

            set
            {
                _title = value;

                // 
                if (Handle != WindowHandle.None)
                {
                    Application.Queue.Invoke(() =>
                    {
                        // Glfw.SetWindowTitle(Handle, _title);
                    });
                }
            }
        }

        internal WindowHandle Handle;

        private int _width, _height;
        private string _title;

        public Window(int width, int height, string title)
        {
            _width = width;
            _height = height;
            _title = title;

            // Track window
            Application.AddWindow(this);
        }

        public void Show()
        {
            Application.Queue.Invoke(() =>
            {
                // No known window, create one
                if (Handle == WindowHandle.None)
                {
                    Handle = Glfw.CreateWindow(Width, Height, Title, MonitorHandle.None, Application.DummyWindow);
                }

                // 
                Glfw.ShowWindow(Handle);
            });
        }

        //public void Resize(int width, int height)
        //{
        //    _width = width;
        //    _height = height;

        //    // 
        //    if (Handle != Glfw.WindowHandle.None)
        //    {
        //        Application.Queue.Invoke(() =>
        //        {
        //            Glfw.SetWindowSize(Handle, width, height);
        //        });
        //    }
        //}
    }
}
