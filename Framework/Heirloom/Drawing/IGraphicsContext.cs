using System.Collections.Generic;

using Heirloom.Mathematics;

namespace Heirloom.Drawing
{
    public interface IGraphicsContext
    {
        // Object State
        Matrix Transform { get; set; }
        BlendingMode BlendingMode { get; set; }
        Shader Shader { get; }
        Color Color { get; set; }

        // View State
        Surface Surface { get; set; }
        View View { get; set; }

        // Performance metrics
        GraphicsPerformance Performance { get; }

        IScreen Screen { get; } // todo: may remove

        // todo: consider if its now possible to neglect GraphicsContext and perform drawing commands directly on a surface?
        //       ie, surface.Clear()... surface.DrawLine()

        void ResetState();
        void PushState();
        void PopState();

        void SetShader(Shader shader);            // todo: or setter in property?
        void SetUniform<T>(string name, T value); // todo: perhaps a dict in the shader object?

        Image GrabPixels(IntRectangle region);
        Image GrabPixels();

        void Clear(Color color);

        void BeginStencil();
        void EndStencil();
        void ClearStencil();

        void Draw(Mesh mesh, Texture texture, Rectangle uvRegion, Matrix matrix); // fundamental draw method
        void Draw(Mesh mesh, Texture texture, Rectangle uvRegion); // todo: ???
        void Draw(Mesh mesh, Texture texture, Matrix matrix);
        void Draw(Mesh mesh, Texture texture); // todo: ???

        void Commit();

        void Dispose();

        #region Drawing Implementation

        #region Draw Line

        void DrawLine(Vector p0, Vector p1, Texture texture, float width = 1);
        void DrawLine(Vector p0, Vector p1, float width = 1);

        #endregion

        #region Draw PolyLine

        void DrawPolyLine(IEnumerable<Vector> points, Texture texture, float width, bool loop = false);
        void DrawPolyLine(IEnumerable<Vector> points, float width = 1F, bool loop = false);

        #endregion

        #region Draw Curve

        // Cubic
        void DrawCurve(Vector p0, Vector p1, Vector p2, Vector p3, Texture texture, float width = 1);
        void DrawCurve(Vector p0, Vector p1, Vector p2, Vector p3, float width = 1);

        // Quadradic
        void DrawCurve(Vector p0, Vector p1, Vector p2, Texture texture, float width = 1);
        void DrawCurve(Vector p0, Vector p1, Vector p2, float width = 1);

        // Multiple Segments
        void DrawCurve(Bezier curve, Texture texture, float width = 1);
        void DrawCurve(Bezier curve, float width = 1);

        #endregion

        #region Draw Image

        void DrawImage(Texture texture, IntRectangle subregion, Matrix matrix);
        void DrawImage(Texture texture, Matrix matrix);

        void DrawImage(Texture texture, IntRectangle subregion, Vector position);
        void DrawImage(Texture texture, Vector position);

        void DrawImage(Texture texture, IntRectangle subregion, Rectangle rectangle);
        void DrawImage(Texture texture, Rectangle rectangle);

        #endregion

        // todo: Draw Polygon
        // todo: Draw Regular Polygon
        // todo: Draw Circle

        #region Draw Rectangle

        void DrawRect(Rectangle rectangle);

        void DrawRectOutline(Rectangle rectangle, float width = 1);

        #endregion

        #region Draw Triangle

        // 
        void DrawTriangle(Vector a, Vector b, Vector c);
        void DrawTriangle(Triangle triangle);

        void DrawTriangleOutline(Vector a, Vector b, Vector c, float width = 1);
        void DrawTriangleOutline(Triangle triangle, float width = 1);

        #endregion

        #region Draw Utility Shapes

        void DrawCross(Vector center, float size = 3, float width = 1);

        #endregion

        #endregion
    }
}
