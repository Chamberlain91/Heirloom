using Heirloom.Drawing;
using Heirloom.Math;

namespace Heirloom.Runtime
{
    public class TileGridRenderer : Renderer
    {
        public TileGrid Grid { get; set; }

        protected internal override void Update()
        {
            if (Grid == null) { return; }
        }

        protected internal override void Render(RenderContext ctx)
        {
            if (Grid == null) { return; }
            else { ctx.Draw(Grid, Transform.Matrix, Color); }
        }
    }

    public static class TileGridDrawingExtensions
    {
        public static void Draw(this RenderContext ctx, TileGrid grid, Matrix matrix, Color color)
        {
            for (var y = 0; y < grid.Height; y++)
            {
                for (var x = 0; x < grid.Width; x++)
                {
                    var tile = grid[x, y];
                    if (tile != null)
                    {
                        var transform = matrix * Matrix.CreateTranslation(x * grid.TileWidth, y * grid.TileHeight);
                        ctx.Draw(tile.Sprite, 0, transform, color);
                    }
                }
            }
        }
    }
}
