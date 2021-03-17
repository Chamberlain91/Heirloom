using Heirloom.Desktop;
using Heirloom.Drawing;
using Heirloom.Mathematics;

namespace Heirloom.Testing.SoftwareResize
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Application.Run(() =>
            {
                var source = new Image("pexels-photo-247819.jpg");

                var height = 500;
                var width = (int) (height * source.Size.Aspect);

                // Fit window to image size
                var window = new Window("Heirloom - Software Image Resize", (width, height), vsync: false)
                {
                    IsResizable = false
                };

                // Generate resized image
                var target = Image.Resize(source, width, height);

                Log.Info($"Image is originally: {source.Size}");
                Log.Info($"Image is resized to: {target.Size}");

                // Draw images to window
                window.Graphics.ResetState();
                window.Graphics.Clear(Color.Pink);
                window.Graphics.DrawImage(target, Matrix.Identity);

                // Put images on screen
                window.Refresh();
            });
        }
    }
}
