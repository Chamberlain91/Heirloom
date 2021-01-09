using Heirloom.Drawing;
using Heirloom.Mathematics;

using static Heirloom.UI.Gui;

namespace Heirloom.UI
{
    public static class GuiHelper
    {
        private static GraphicsContext Graphics => Gui.Graphics;

        private static GuiStyle Style => Gui.Style;

        public static Color GetInteractionColor(bool hover, bool click)
        {
            if (hover && click) { return Style.ActiveColor; }
            else if (hover) { return Style.HoverColor; }
            else { return Style.BaseColor; }
        }

        public static Color? GetFocusColor()
        {
            return Element.IsActiveElement ? Style.FocusColor : null;
        }

        public static void DrawRect(IntRectangle bounds, Color color, Color? borderColor = null)
        {
            // Draw rectangle
            Graphics.Color = color;
            Graphics.DrawRect(bounds);

            if (borderColor.HasValue)
            {
                // todo: is this expected? left vs right edge of IntRect
                bounds.Width--;
                bounds.Height--;

                // Draw border
                Graphics.Color = borderColor.Value;
                Graphics.DrawRectOutline(bounds);
            }
        }
    }
}
