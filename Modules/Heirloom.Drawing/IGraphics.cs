using System;
using System.Collections.Generic;

using Heirloom.Math;

namespace Heirloom.Drawing
{
    public interface IGraphics : IDisposable
    {
        bool EnableFPSOverlay { get; set; }
        float CurrentFPS { get; }

        float ApproximatePixelScale { get; }

        Surface DefaultSurface { get; }
        Surface Surface { get; set; }

        Rectangle Viewport { get; set; }

        Matrix Transform { get; set; }
        Matrix InverseTransform { get; }

        Color Color { get; set; }
        Blending Blending { get; set; }

        void ResetState();
        void PushState();
        void PopState();

        bool IsDisposed { get; }

        void Clear(in Color color);

        void DrawMesh(ImageSource image, Mesh mesh, in Matrix transform);

        void DrawImage(ImageSource image, in Rectangle rectangle);
        void DrawImage(ImageSource image, in Matrix transform);
        void DrawImage(ImageSource image, in Vector position);
        void DrawImage(ImageSource image, in Vector position, float rotation);
        void DrawImage(ImageSource image, in Vector position, float rotation, in Vector scale);

        void DrawSprite(Sprite sprite, int frame, in Matrix transform);

        void DrawCircle(in Circle circle);
        void DrawCircle(in Vector position, float radius);
        void DrawCircleOutline(in Circle circle, float width = 1);
        void DrawCircleOutline(in Vector position, float radius, float width = 1);

        void DrawLine(in Vector p0, in Vector p1, float width = 1);

        void DrawCurve(in Vector p0, in Vector p1, in Vector p2, float width = 1);
        void DrawCurve(in Vector p0, in Vector p1, in Vector p2, in Vector p3, float width = 1);

        void DrawText(string text, in Rectangle bounds, Font font, int size, TextAlign align = TextAlign.Left, DrawTextCallback callback = null);
        void DrawText(string text, in Rectangle bounds, Font font, int size, DrawTextCallback callback);
        void DrawText(string text, in Vector position, Font font, int size, TextAlign align = TextAlign.Left, DrawTextCallback callback = null);
        void DrawText(string text, in Vector position, Font font, int size, DrawTextCallback callback);

        void DrawText(StyledText styledText, in Rectangle bounds, Font font, int size, TextAlign align = TextAlign.Left);
        void DrawText(StyledText styledText, in Vector position, Font font, int size, TextAlign align = TextAlign.Left);

        void DrawRect(in Rectangle rectangle);
        void DrawRectOutline(in Rectangle rectangle, float width = 1);

        void DrawTriangle(in Triangle triangle);
        void DrawTriangle(in Vector a, in Vector b, in Vector c);
        void DrawTriangleOutline(in Triangle triangle, float width = 1);
        void DrawTriangleOutline(in Vector a, in Vector b, in Vector c, float width = 1);

        void DrawPolygon(IEnumerable<Vector> polygon);
        void DrawPolygon(IEnumerable<Vector> polygon, in Matrix transform);
        void DrawPolygon(Polygon polygon);
        void DrawPolygon(Polygon polygon, in Matrix transform);
        void DrawPolygonOutline(IEnumerable<Vector> polygon, float width = 1);
        void DrawPolygonOutline(IEnumerable<Vector> polygon, in Matrix transform, float width = 1);
        void DrawPolygonOutline(Polygon polygon, float width = 1);
        void DrawPolygonOutline(Polygon polygon, in Matrix transform, float width = 1);

        void DrawPolygon(in Vector position, int sides, float radius);
        void DrawPolygonOutline(in Vector position, int sides, float radius, float width = 1);

        void DrawCross(in Vector center, float size = 2, float width = 1);
        void DrawNineSlice(NineSlice nine, in Rectangle rectangle);

        Image GrabPixels();
        Image GrabPixels(in IntRectangle region);

        void RefreshScreen();
        void Flush();
    }
}
