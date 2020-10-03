using System;

using Meadows.Mathematics;

namespace Meadows.Drawing
{
    public static class DrawExtensions
    {
        public static unsafe void Draw(this IDrawContext ctx, Texture texture, in ReadOnlySpan<Vertex> vertices, in Matrix matrix)
        {
            fixed (Matrix* matrix_ptr = &matrix)
            {
                var matrices = new ReadOnlySpan<Matrix>(matrix_ptr, 1);
                ctx.Draw(texture, vertices, in matrices);
            }
        }
    }

    public static class LineRenderer
    {
        public static void DrawLine(this IDrawContext ctx, Vector start, Vector end, Color color, float width = 1F)
        {
            DrawLine(ctx, start, end, color, color, width);
        }

        public static void DrawLine(this IDrawContext ctx, Vector start, Vector end, Color startColor, Color endColor, float width = 1F)
        {
            throw new NotImplementedException();
        }
    }

    public static class TextRenderer
    {
        public static void DrawText(this IDrawContext ctx, string text, Vector position, TextAlign align, Font font, int size)
        {
            throw new NotImplementedException();
        }
    }
}
