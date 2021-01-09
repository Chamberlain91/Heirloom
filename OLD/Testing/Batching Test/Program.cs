using Heirloom;
using Heirloom.Desktop;

namespace Batching_Test
{
    internal class Program : GameLoop
    {
        public Program()
            : base(new Window("Batching Test") { IsResizable = false })
        {
            Graphics.Performance.OverlayMode = PerformanceOverlayMode.Full;
        }

        protected override void Update(float dt)
        {
            Graphics.Clear(Color.Red);

            const int CellSize = 4;
            var count = Screen.Width / CellSize * (Screen.Height / CellSize);

            var groupSize = Calc.Random.Next(10, 500);
            for (var i = 0; i < count; i++)
            {
                var x = i % (Screen.Width / CellSize) * CellSize;
                var y = i / (Screen.Width / CellSize) * CellSize;

                var kind = i / groupSize % 2;

                if (kind == 0)
                {
                    Graphics.Color = Color.DarkGray;
                    Graphics.DrawRect(new Rectangle(x, y, CellSize, CellSize));
                }
                else
                {
                    Graphics.Color = Color.LightGray;
                    Graphics.DrawCircle(new Circle(x + 2, y + 2, 3));
                }
            }
        }

        private static void Main(string[] args) { Application.Run<Program>(); }
    }
}
