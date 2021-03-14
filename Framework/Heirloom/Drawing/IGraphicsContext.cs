using System.Collections.Generic;

using Heirloom.Mathematics;

namespace Heirloom.Drawing
{
    internal interface IGraphicsContext
    {
        // Object State (Batch Safe)
        Matrix Transform { get; set; }
        Color Color { get; set; }

        // Object State (Batch Break)
        BlendingMode Blending { get; set; }
        Shader Shader { get; set; }

        // View State (Batch Break)
        Surface Surface { get; set; }
        View View { get; set; }

        // Performance metrics
        GraphicsPerformance Performance { get; }

        IScreen Screen { get; }

        // todo: consider if its now possible to neglect GraphicsContext and perform drawing commands directly on a surface?
        //       ie, surface.Clear()... surface.DrawLine()

        void ResetState();
        void PushState();
        void PopState();

        void BeginStencil();
        void EndStencil();
        void ClearStencil();

        void Clear(Color color);

        void Draw(Mesh mesh, Texture texture, Rectangle uvRegion, Matrix matrix); // fundamental draw method
        void Draw(Mesh mesh, Texture texture, Rectangle uvRegion); // todo: ???
        void Draw(Mesh mesh, Texture texture, Matrix matrix);
        void Draw(Mesh mesh, Texture texture); // todo: ???

        Image GrabPixels(IntRectangle region);
        Image GrabPixels();

        void Commit();

        void Dispose();

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

        #region Draw Polygon

        void DrawPolygon(Polygon polygon, Matrix matrix);
        void DrawPolygon(IEnumerable<Vector> polygon, Matrix matrix);

        void DrawPolygonOutline(Polygon polygon, Matrix matrix, float width = 1F);
        void DrawPolygonOutline(IEnumerable<Vector> polygon, Matrix matrix, float width = 1F);

        #endregion

        #region Draw Regular Polygon

        void DrawRegularPolygon(Vector center, int sides, Matrix matrix);
        void DrawRegularPolygonOutline(Vector center, int sides, Matrix matrix, float width = 1F);

        #endregion

        #region Draw Circle

        void DrawCircle(Vector center, Matrix matrix);
        void DrawCircleOutline(Vector center, int sides, Matrix matrix, float width = 1F);

        #endregion

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
    }

    //public readonly struct Transform
    //{
    //    public readonly Matrix Matrix;

    //    public Transform(Matrix matrix)
    //    {
    //        Matrix = matrix;
    //    }

    //    public static implicit operator Matrix(Transform transform)
    //    {
    //        return transform.Matrix;
    //    }

    //    public static implicit operator Transform(Matrix transform)
    //    {
    //        return new Transform(transform);
    //    }

    //    public static implicit operator Transform(Vector translation)
    //    {
    //        return Matrix.CreateTranslation(translation);
    //    }
    //}
}
