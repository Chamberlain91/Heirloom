using System.Collections.Generic;

using Heirloom.Drawing;
using Heirloom.Input;
using Heirloom.Math;

namespace KenneyUI
{
    public abstract class UIElement
    {
        public Rectangle Bounds;

        public List<UIElement> Children;

        internal void Render(RenderingContext ctx, UITheme theme)
        {
            // 
            Draw(ctx, theme);

            // 
            foreach (var children in Children)
            {
                children.Render(ctx, theme);
            }
        }

        protected abstract void Draw(RenderingContext ctx, UITheme theme);

        protected abstract void OnMouseMove(Vector position);

        protected abstract void OnMouseDown(MouseButton button);

        protected abstract void OnMouseUp(MouseButton button);
    }

    public class Button : UIElement
    {
        public string Text;

        private bool _isWithin, _isDown;

        protected override void Draw(RenderingContext ctx, UITheme theme)
        {
            var textSize = theme.Font.MeasureText(Text, theme.FontSize);
            var textBounds = Bounds.Inflate(-4);
            textBounds.Y = (_isDown ? 0 : -2) + Bounds.Y + ((Bounds.Height - textSize.Height) / 2F);
            textBounds.Height = textSize.Height;

            // 
            if (_isDown) { ctx.Draw(theme.ButtonDownFrame, Bounds, Color.White); }
            else { ctx.Draw(theme.ButtonFrame, Bounds, Color.White); }

            // Draw button label
            ctx.DrawText(Text, textBounds, TextAlign.Center, theme.Font, theme.FontSize, Color.White);
        }

        protected override void OnMouseUp(MouseButton button)
        {
            _isDown = false;
        }

        protected override void OnMouseDown(MouseButton button)
        {
            _isDown = _isWithin;
        }

        protected override void OnMouseMove(Vector position)
        {
            _isWithin = Bounds.Contains(position);
        }
    }

    public class TextArea : UIElement
    {
        public string Text;

        public TextAlign Align = TextAlign.Left;

        private bool _isWithin, _isDown;

        protected override void Draw(RenderingContext ctx, UITheme theme)
        {
            var textSize = theme.Font.MeasureText(Text, theme.FontSize);
            var textBounds = Bounds.Inflate(-4);
            textBounds.Y = (_isDown ? 0 : -2) + Bounds.Y + ((Bounds.Height - textSize.Height) / 2F);
            textBounds.Height = textSize.Height;

            ctx.DrawText(Text, Bounds, Align, theme.Font, theme.FontSize, Color.White);
        }

        protected override void OnMouseUp(MouseButton button)
        {
            _isDown = false;
        }

        protected override void OnMouseDown(MouseButton button)
        {
            _isDown = _isWithin;
        }

        protected override void OnMouseMove(Vector position)
        {
            _isWithin = Bounds.Contains(position);
        }
    }
}
