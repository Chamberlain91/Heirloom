using System;

using Meadows.Drawing;
using Meadows.Mathematics;

namespace Meadows.UI
{
    public sealed class GuiStyle
    {
        public int BasicElementHeight { get; }

        public int BasicElementSpace { get; }

        public int BasicGroupSpace { get; init; }

        public SliderStyle Slider { get; init; }

        public BasicStyle Button { get; init; }

        public BasicStyle Panel { get; init; }

        public BasicStyle Text { get; init; }

        public readonly Font Font;

        public readonly Font FontBold;

        public readonly int FontSize;

        public GuiStyle(Font font, Font fontBold, int fontSize, int elementSpace = 4)
        {
            Font = font;
            FontBold = fontBold;
            FontSize = fontSize;

            // 
            BasicElementSpace = elementSpace;
            BasicElementHeight = (BasicElementSpace * 2) + (int) Font.GetMetrics(fontSize).Height;

            // Default element styles
            Slider = new SliderStyle { Padding = (0, 4), HandleSize = 3, HandlePadding = 1 };
            Button = new BasicStyle { Padding = (8, 8) };
            Panel = new BasicStyle { Padding = (16, 16) };
            Text = new BasicStyle { Padding = (8, 4) };
        }
    }

    public readonly struct BasicStyle
    {
        public IntVector Padding { get; init; }
    }

    public readonly struct SliderStyle
    {
        public IntVector Padding { get; init; }

        public int HandlePadding { get; init; }

        public int HandleSize { get; init; }
    }

    public static class Gui
    {
        private static IntRectangle _layoutBox;
        private static int _y;

        public static GraphicsContext Graphics { get; private set; }

        public static GuiTheme Theme { get; set; } = GuiTheme.Default;

        public static GuiStyle Style { get; set; } = new GuiStyle(Font.SansSerif, Font.SansSerifBold, 12);

        public static void BeginFrame(GraphicsContext graphics, float dt)
        {
            Graphics = graphics;

            // Update mouse state
            Mouse.Position = Input.MousePosition;
            Mouse.Button = Input.GetButtonState(MouseButton.Left);

            // Assign new active element 
            Element.ActiveElement = Element.NextActiveElement;

            // If we click, the active element will now change. This might change
            // into the same element but thats still effectively like changing elements.
            if (Mouse.IsPressed) { Element.NextActiveElement = 0; }

            // 
            TextEditor.Update(dt);
        }

        public static void SetLayoutBox(IntRectangle layoutBox)
        {
            // Render panel
            PrependPanel(ref layoutBox);

            // 
            _layoutBox = layoutBox;
            _y = layoutBox.Top;
        }

        private static IntRectangle GetNextLayoutBox(int height = -1, int space = -1)
        {
            if (height < 0) { height = Style.BasicElementHeight; }
            if (space < 0) { space = Style.BasicElementSpace; }

            var box = new IntRectangle(_layoutBox.X, _y, _layoutBox.Width, height);
            _y += height + space;

            return box;
        }

        private static void BeginElement(string title)
        {
            Element.CurrentElement = HashCode.Combine(title);
        }

        #region Theme Helpers

        public static Color GetInteractionColor(bool hover, bool click)
        {
            if (hover && click) { return Theme.ActiveColor; }
            else if (hover) { return Theme.HoverColor; }
            else { return Theme.BaseColor; }
        }

        public static Color GetBorderColor()
        {
            if (Element.IsActiveElement) { return Theme.FocusColor; }
            else { return Theme.BorderColor; }
        }

        #endregion

        #region Element Construction

        public static void PrependText(string text, ref IntRectangle bounds, bool centerAlign = false)
        {
            var textAlign = TextAlign.Left;
            if (centerAlign) { textAlign = TextAlign.Center; }

            // Draw label
            // todo: Clip text / shorten it if it would invalidate bounds?
            Graphics.Color = Theme.TextColor;
            var measure = Graphics.DrawText(text, bounds, Style.Font, Style.FontSize, textAlign | TextAlign.Middle);
            ShrinkLeft(ref bounds, Style.Text.Padding.X + (int) measure.Width);
        }

        public static void PrependIcon(Texture texture, ref IntRectangle bounds)
        {
            var aspect = texture.Size.Aspect;

            // Fit image into bounds
            var box = bounds;
            box.Width = (int) (bounds.Height * aspect);
            ShrinkLeft(ref bounds, Style.Text.Padding.X + box.Width);

            // Draw label
            Graphics.Color = Color.White;
            Graphics.DrawImage(texture, box);
        }

        public static void PrependRectangle(Color baseColor, ref IntRectangle bounds)
        {
            var oldColor = Graphics.Color;
            {
                // Draw rectangle w/ base color
                Graphics.Color = baseColor;
                Graphics.DrawRect(bounds);

                // Draw border
                Graphics.Color = GetBorderColor();
                Graphics.DrawRectOutline(bounds);
            }
            Graphics.Color = oldColor;
        }

        public static void PrependPanel(ref IntRectangle bounds)
        {
            PrependRectangle(Theme.Background, ref bounds);
            Shrink(ref bounds, Style.Panel.Padding.X, Style.Panel.Padding.Y);
        }

        #endregion

        #region Non-Interactive Elements

        public static void Space()
        {
            _y += Style.BasicGroupSpace;
        }

        public static void Label(string text)
        {
            var bounds = GetNextLayoutBox();

            // Draw label
            // todo: Clip text / shorten it if it would invalidate bounds.
            Graphics.Color = Theme.TextColor;
            Graphics.DrawText(text, bounds, Style.FontBold, Style.FontSize, TextAlign.Left | TextAlign.Middle);
        }

        public static void Tooltip(string text, IntVector position)
        {
            var bounds = (IntRectangle) TextLayout.Measure(text, Style.Font, Style.FontSize);
            bounds.Position = position - (IntVector) bounds.Size;

            var backgroundBounds = IntRectangle.Inflate(bounds, Style.Text.Padding.X, Style.Text.Padding.Y);
            PrependRectangle(Theme.BaseColor, ref backgroundBounds);
            PrependText(text, ref bounds, true);
        }

        #endregion

        #region Interactive Elements

        public static bool Window(string title, ref IntRectangle bounds)
        {
            throw new NotImplementedException();
            BeginElement(title);

            // todo: titlebar, close?
            SetLayoutBox(bounds);

            return true; // did not close
        }

        public static int TabGroup(string[] names, int currentGroup, ref IntRectangle bounds)
        {
            throw new NotImplementedException();
            // modify bounds to fit container?
            // return current tab group
        }

        public static bool Button(string title, Texture icon = null)
        {
            BeginElement(title);

            var bounds = GetNextLayoutBox();

            var isMouseOver = bounds.Contains(Mouse.Position);
            if (isMouseOver && Mouse.IsPressed)
            {
                Element.SetElementActive();
            }

            var isActiveMouseOver = isMouseOver && Element.IsActiveElement;

            // Draw button body
            var baseColor = GetInteractionColor(isMouseOver, Mouse.IsDown);
            PrependRectangle(baseColor, ref bounds);

            // Collapse bounds to give padding to the button text
            Shrink(ref bounds, Style.Button.Padding.X, Style.Button.Padding.Y);

            if (icon != null)
            {
                // Prepend icon
                PrependIcon(icon, ref bounds);
            }

            // Draw button text
            PrependText(title, ref bounds);

            return isActiveMouseOver && Mouse.IsReleased;
        }

        public static bool Slider(string title, ref float value, float min = 0F, float max = 100F, float step = 1F)
        {
            BeginElement(title);

            var bounds = GetNextLayoutBox();

            // Draws text label
            PrependText(title, ref bounds);

            // Determine if the mouse is hovering this element.
            // If clicked on, store id and determine if selected.
            var isHoverSliderFrame = bounds.Contains(Mouse.Position);
            if (isHoverSliderFrame && Mouse.IsPressed) { Element.SetElementActive(); }

            // Shrink
            ShrinkVertical(ref bounds, Style.Button.Padding.Y);

            // Copy the bounds for drawing the active slider value
            var sliderRect = bounds;

            // Shrink the bounds to prevent the handle from escaping the slider frame
            ShrinkHorizontal(ref bounds, Style.Slider.HandleSize);

            // Compute where in the slider the current value is
            var valueBetween = Calc.Clamp(Calc.Between(value, min, max), 0F, 1F);
            var sliderPosition = (int) Calc.Lerp(bounds.Left, bounds.Right, valueBetween);

            // Draw the active value representation
            Graphics.Color = Theme.ActiveColor;
            Graphics.DrawRect(sliderRect);

            // Compute the handle box
            var handleRect = bounds;
            handleRect.X = sliderPosition - Style.Slider.HandleSize;
            handleRect.Width = (Style.Slider.HandleSize * 2) + 1;
            handleRect.Height += Style.Slider.HandlePadding * 2;
            handleRect.Y -= Style.Slider.HandlePadding;

            // Draw the slider handle
            Graphics.Color = Theme.TextColor;
            Graphics.DrawRect(handleRect);

            // 
            var isHoverHandle = handleRect.Contains(Mouse.Position);
            var isInteracting = Element.IsActiveElement && Mouse.IsDown && isHoverSliderFrame;

            var isModified = false;

            if (isInteracting || isHoverHandle)
            {
                if (isInteracting)
                {
                    // Slider value has changed
                    valueBetween = Calc.Clamp(Calc.Between(Mouse.Position.X, bounds.Left, bounds.Right), 0F, 1F);

                    var newValue = Calc.Lerp(min, max, valueBetween);
                    newValue = Calc.Round(newValue / step) * step;

                    isModified = !Calc.NearEquals(newValue, value);
                    value = newValue;
                }

                // Display tooltip
                Tooltip($"{value}", handleRect.TopLeft);
            }

            return isModified;
        }

        public static bool TextInput(string title, ref string text, bool multiline = false)
        {
            BeginElement(title);

            var bounds = LayoutTitle(title, text, multiline);

            // If mouse has clicked on this box
            var isHovering = bounds.Contains(Mouse.Position);
            if (isHovering && Mouse.IsPressed)
            {
                Element.SetElementActive();
            }

            // Draw text box frame
            var baseColor = GetInteractionColor(isHovering, false);
            PrependRectangle(baseColor, ref bounds);

            Shrink(ref bounds, Style.Text.Padding.X, Style.Text.Padding.Y);

            if (Element.IsActiveElement)
            // todo: Element.IsElementFresh - frame to initialize element
            // todo: Element.IsElementBlur - frame to deinitialize element
            {
                // Initialize the text editor
                TextEditor.Initialize(text, bounds);

                // todo: scroll text into view?
                Graphics.Color = Theme.TextColor;
                Graphics.DrawText(text, bounds, Style.Font, Style.FontSize, TextEditor.CharacterCallback);

                // Render cursor
                if (TextEditor.ShowCursor) // causes the cursor to blink
                {
                    var lineHeight = Style.Font.GetMetrics(Style.FontSize).Height;

                    // Draw the cursor
                    Graphics.Color = Theme.TextColor;
                    Graphics.DrawLine(TextEditor.CursorPosition, TextEditor.CursorPosition + (Vector.Down * lineHeight));
                }

                // Process text editor input
                TextEditor.ProcessInput(allowNewline: multiline);

                // If the text isn't equivalent, it has been modified. 
                if (text != TextEditor.Text)
                {
                    text = TextEditor.Text;
                    return true;
                }
            }
            else
            {
                // Simply draw the text from the top
                Graphics.Color = Theme.TextColor;
                Graphics.DrawText(text, bounds, Style.Font, Style.FontSize);
            }

            // Text is not modified.
            return false;

            static IntRectangle LayoutTitle(string title, string text, bool multiline)
            {
                // 
                IntRectangle bounds;
                if (multiline)
                {
                    // Draw title
                    var titleHeight = (int) TextLayout.Measure(title, Style.Font, Style.FontSize).Height;
                    bounds = GetNextLayoutBox(titleHeight, space: Style.Text.Padding.Y);
                    var textboxWidth = bounds.Width - (Style.Text.Padding.X * 2);
                    PrependText(title, ref bounds, false);

                    // Draw text area (multi-line)
                    var textMeasure = TextLayout.Measure(text, (textboxWidth, int.MaxValue), Style.Font, Style.FontSize);
                    var textHeight = (int) Calc.Max(textMeasure.Height + Style.Button.Padding.Y * 2, Style.BasicElementHeight);
                    bounds = GetNextLayoutBox(textHeight);
                }
                else
                {
                    // Tile and text area (single-line)
                    bounds = GetNextLayoutBox();
                    PrependText(title, ref bounds, false);
                }

                return bounds;
            }
        }

        #endregion

        #region Shrink Box

        public static void ShrinkRight(ref IntRectangle bounds, int size)
        {
            bounds.Width -= size;
        }

        public static void ShrinkLeft(ref IntRectangle bounds, int size)
        {
            bounds.Width -= size;
            bounds.X += size;
        }

        public static void ShrinkHorizontal(ref IntRectangle bounds, int size)
        {
            bounds.Width -= size * 2;
            bounds.X += size;
        }

        public static void ShrinkTop(ref IntRectangle bounds, int size)
        {
            bounds.Height -= size;
            bounds.Y += size;
        }

        public static void ShrinkBottom(ref IntRectangle bounds, int size)
        {
            bounds.Height -= size;
        }

        public static void ShrinkVertical(ref IntRectangle bounds, int size)
        {
            bounds.Height -= size * 2;
            bounds.Y += size;
        }

        public static void Shrink(ref IntRectangle bounds, int sizeX, int sizeY)
        {
            ShrinkHorizontal(ref bounds, sizeX);
            ShrinkVertical(ref bounds, sizeY);
        }

        public static void Shrink(ref IntRectangle bounds, int size)
        {
            Shrink(ref bounds, size, size);
        }

        #endregion

        internal static class Mouse
        {
            internal static ButtonState Button;

            internal static Vector Position;

            internal static bool IsDown => Button.HasFlag(ButtonState.Down);

            internal static bool IsPressed => Button.HasFlag(ButtonState.Pressed);

            internal static bool IsReleased => Button.HasFlag(ButtonState.Released);

            internal static bool IsUp => Button.HasFlag(ButtonState.Up);

            internal static bool HasMouseInput => IsDown || IsPressed || IsReleased;
        }

        internal static class Element
        {
            internal static int ActiveElement;

            internal static int NextActiveElement;

            internal static int CurrentElement;

            internal static bool IsActiveElement => ActiveElement == CurrentElement;

            internal static void SetElementActive()
            {
                NextActiveElement = CurrentElement;
            }
        }
    }
}
