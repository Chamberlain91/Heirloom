using Heirloom;
using Heirloom.Desktop;
using Heirloom.Geometry;

namespace Batching_Test
{
    internal class Program : GameLoop
    {
        public Program()
            : base(new Window("Batching Test"))
        {
            Graphics.Performance.OverlayMode = PerformanceOverlayMode.Full;
        }

        protected override void Update(float dt)
        {
            Graphics.Clear(Color.Red);

            const int CellSize = 4;
            var count = (Screen.Width / CellSize) * (Screen.Height / CellSize);

            Graphics.Color = Color.DarkGray;
            for (var i = 0; i < count; i++)
            {
                var x = i % (Screen.Width / CellSize) * CellSize;
                var y = i / (Screen.Width / CellSize) * CellSize;

                var kind = (i / 200) % 2;

                if (kind == 0)
                {
                    Graphics.DrawRect(new Rectangle(x, y, CellSize, CellSize));
                }
                else
                {
                    Graphics.DrawCircle(new Circle(x + 2, y + 2, 3));
                }
            }
        }

        private static void Main(string[] args) { Application.Run<Program>(); }
    }
}
