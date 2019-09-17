using Heirloom.Desktop;

namespace Examples.Slideshow
{
    internal static class Program
    {
        private static void Main(string[] _)
        {
            // == This implicitly runs a GameWindow
            Application.Run(() => new SlideshowExample());

            // == The above code is equivalent to below
            //Application.Run(() =>
            //{
            //    var game = new SlideshowExample();
            //    game.Run();
            //});
        }
    }
}
