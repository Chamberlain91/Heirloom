using Meadows.Drawing;
using Meadows.Mathematics;

namespace Meadows.UI
{
    public static class ImGuiLayout
    {
        private const int BasicElementHeight = 23;
        private const int BasicSpace = 2;

        private static IntRectangle _layoutBox;
        private static int _y;

        public static void SetLayoutBox(IntRectangle layoutBox)
        {
            _layoutBox = layoutBox;
            _y = layoutBox.Top;
        }

        private static IntRectangle GetNextLayoutBox(int height)
        {
            var box = new IntRectangle(_layoutBox.X, _y, _layoutBox.Width, height);
            _y += height + BasicSpace;

            return box;
        }

        public static void Label(string text)
        {
            var bounds = GetNextLayoutBox(BasicElementHeight);
            ImGui.Label(bounds, text);
        }

        public static void Icon(Texture icon)
        {
            var bounds = GetNextLayoutBox(BasicElementHeight);
            ImGui.Icon(bounds, icon);
        }

        public static bool Button(string text, Texture icon = null)
        {
            var bounds = GetNextLayoutBox(BasicElementHeight);
            return ImGui.Button(bounds, text, icon);
        }
    }
}
