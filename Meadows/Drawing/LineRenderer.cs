using System;

using Meadows.Mathematics;

namespace Meadows.Drawing
{
    public static class LineRenderer
    {
        public static void DrawLine(this GraphicsContext ctx, Vector start, Vector end, Color color, float width = 1F)
        {
            DrawLine(ctx, start, end, color, color, width);
        }

        public static void DrawLine(this GraphicsContext ctx, Vector start, Vector end, Color startColor, Color endColor, float width = 1F)
        {
            throw new NotImplementedException();
        }
    }
}
