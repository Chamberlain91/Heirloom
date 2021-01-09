using System;
using System.Collections.Generic;

using Heirloom.Drawing;
using Heirloom.Mathematics;

using static Heirloom.UI.GuiHelper;

namespace Heirloom.UI
{
    public class GuiContainer
    {
        public IntRectangle Bounds;

        public IntSize ContentSize;

        public bool RequiresHorizontalScrollbar => ContentSize.Width > Bounds.Width;

        public bool RequiresVerticalScrollbar => ContentSize.Height > Bounds.Height;

        public bool RequiresScrollbar => RequiresHorizontalScrollbar || RequiresVerticalScrollbar;

        public IntVector Scroll;
    }

    public struct GuiLayout
    {
        public IntRectangle Body;

        public IntRectangle Next;
    }

    public static class Gui
    {
        private static IntRectangle _layoutContainer;
        private static int _layoutCursor;

        public static GraphicsContext Graphics { get; private set; }

        public static GuiStyle Style { get; set; } = GuiStyle.Light;

        public static readonly ClipStack ClipStack = new();

        private static readonly GuiCache _cache = new();

        #region Clip Stack

        private static bool HasClippingShape => ClipStack.Current != null;

        public static void PushClip(IShape shape)
        {
            ClipStack.Push(shape);
            Graphics.ClipShape = ClipStack.Current;
        }

        public static void PopClip()
        {
            ClipStack.Pop();
            Graphics.ClipShape = ClipStack.Current;
        }

        #endregion

        public static T GetState<T>(GuiIdentifier identifier) where T : class, new()
        {
            return _cache.GetState<T>(identifier);
        }

        private static void SetLayoutContainer(IntRectangle container)
        {
            _layoutContainer = container;
            _layoutCursor = container.Y;
        }

        private static IntRectangle GetNextLayoutBox()
        {
            var rect = new IntRectangle
            {
                X = _layoutContainer.X,
                Y = _layoutCursor,
                Width = _layoutContainer.Width,
                Height = Style.BasicElementHeight
            };

            // Move cursor down
            _layoutCursor += rect.Height + Style.BasicElementSpace;

            return rect;
        }

        public static void BeginFrame(GraphicsContext graphics, float dt)
        {
            Graphics = graphics;
            Graphics.ResetState();

            if (HasClippingShape)
            {
                // Forcefully trash prior clipping state
                ClipStack.Clear();
                Graphics.ClipShape = null;
            }

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

        private static void BeginElement(ref string title, string elementType)
        {
            Element.CurrentElement = HashCode.Combine(title, elementType);
            // todo: strip special hash syntax from title (if present)
        }

        #region Element Construction

        public static void PrependText(string text, ref IntRectangle bounds, bool centerAlign = false)
        {
            var textAlign = TextAlign.Left;
            if (centerAlign) { textAlign = TextAlign.Center; }

            // Draw label
            // todo: Clip text / shorten it if it would invalidate bounds?
            Graphics.Color = Style.TextColor;
            var measure = Graphics.DrawText(text, bounds, Style.Font, Style.FontSize, textAlign | TextAlign.Middle);
            ShrinkLeft(ref bounds, Style.TextPadding.X + Calc.Max((int) measure.Width, 100));
        }

        public static void PrependIcon(Texture texture, ref IntRectangle bounds)
        {
            var aspect = texture.Size.Aspect;

            // Fit image into bounds
            var box = bounds;
            box.Width = (int) (bounds.Height * aspect);
            ShrinkLeft(ref bounds, Style.TextPadding.X + box.Width);

            // Draw label
            Graphics.Color = Color.White;
            Graphics.DrawImage(texture, box);
        }


        #endregion

        #region Non-Interactive Elements

        public static void Panel(string title, IntRectangle bounds, Action action)
        {
            BeginElement(ref title, "panel");

            // Draw whole panel
            // PrependRectangle(Style.Background, bounds);
            DrawRect(bounds, Style.Background);
            SetLayoutContainer(bounds);

            // Draw title bar
            var titleBox = GetNextLayoutBox();
            Graphics.Color = Style.TextColor;
            Graphics.DrawRect(titleBox);
            Graphics.Color = Style.Background;
            Graphics.DrawText(title, titleBox, Style.FontBold, Style.FontSize, TextAlign.Center | TextAlign.Middle);

            // Adjust container box to compensate for visuals
            ShrinkTop(ref _layoutContainer, titleBox.Height);
            ShrinkHorizontal(ref _layoutContainer, Style.ContainerPadding.X);
            ShrinkVertical(ref _layoutContainer, Style.ContainerPadding.Y);
            _layoutCursor = _layoutContainer.Y;

            // 
            // PushClip((Rectangle) _layoutContainer);
            action();
            // PopClip();
        }

        public static void Divider()
        {
            var layout = GetNextLayoutBox();

            // Compute divider rect
            var divider = layout;
            divider.Y = layout.Y + (layout.Height - 2) / 2;
            divider.Height = 2;

            // Draw divider rect
            DrawRect(divider, Style.BorderColor);
        }

        public static void Label(string text)
        {
            var bounds = GetNextLayoutBox();

            // Draw label
            // todo: Clip text / shorten it if it would invalidate bounds.
            Graphics.Color = Style.TextColor;
            Graphics.DrawText(text, bounds, Style.FontBold, Style.FontSize, TextAlign.Left | TextAlign.Middle);
        }

        public static void Tooltip(string text, IntVector position)
        {
            var bounds = (IntRectangle) TextLayout.Measure(text, Style.Font, Style.FontSize);
            bounds.Position = position - (IntVector) bounds.Size;

            // Draw rectangle w/ base color
            var backgroundBounds = IntRectangle.Inflate(bounds, Style.TextPadding);
            DrawRect(backgroundBounds, Style.BaseColor, Style.BorderColor);

            PrependText(text, ref bounds, true);
        }

        #endregion

        #region Interactive Elements

        public static bool Button(string title, Texture icon = null)
        {
            // Registers element, generates ID for element based on title string
            BeginElement(ref title, "button");

            // Requests next layout box.
            var bounds = GetNextLayoutBox();

            // Mark element as active element if clicked
            var isMouseOver = bounds.Contains(Mouse.Position);
            if (isMouseOver && Mouse.IsPressed)
            {
                Element.SetElementActive();
            }

            // Draw button
            DrawRect(bounds, GetInteractionColor(isMouseOver, Mouse.IsDown), GetFocusColor());
            DrawRect((bounds.X + 1, bounds.Bottom - 2 - 1, bounds.Width - 2, 2), Style.BorderColor);

            // Compensate bounds for button border
            ShrinkBottom(ref bounds, 2);

            // Shrink to draw text
            Shrink(ref bounds, Style.Padding.X, Style.Padding.Y);

            // Prepend icon (if specified)
            if (icon != null) { PrependIcon(icon, ref bounds); }

            // Draw button text
            PrependText(title, ref bounds);

            return isMouseOver && Element.IsActiveElement && Mouse.IsReleased;
        }

        public static bool Slider(string title, ref float value, float min = 0F, float max = 100F, float step = 1F)
        {
            BeginElement(ref title, "slider");

            var bounds = GetNextLayoutBox();

            // Draws text label
            PrependText(title, ref bounds);

            // Determine if the mouse is hovering this element.
            // If clicked on, store id and determine if selected.
            var isHoverSliderFrame = bounds.Contains(Mouse.Position);
            if (isHoverSliderFrame && Mouse.IsPressed) { Element.SetElementActive(); }

            // Shrink
            ShrinkVertical(ref bounds, Style.Padding.Y);

            // Copy the bounds for drawing the active slider value
            var sliderRect = bounds;

            // Shrink the bounds to prevent the handle from escaping the slider frame
            ShrinkHorizontal(ref bounds, Style.HandleSize);

            // Compute where in the slider the current value is
            var valueBetween = Calc.Clamp(Calc.Between(value, min, max), 0F, 1F);
            var sliderPosition = (int) Calc.Lerp(bounds.Left, bounds.Right, valueBetween);

            // Draw the active value representation
            Graphics.Color = Style.ActiveColor;
            Graphics.DrawRect(sliderRect);

            // Compute the handle box
            var handleRect = bounds;
            handleRect.X = sliderPosition - Style.HandleSize;
            handleRect.Width = (Style.HandleSize * 2) + 1;
            //handleRect.Height += Style.HandlePadding * 2;
            //handleRect.Y -= Style.HandlePadding;

            // Draw the slider handle
            Graphics.Color = Style.TextColor;
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
            BeginElement(ref title, "textbox");

            var bounds = LayoutTitleAndBox(title, text, multiline);

            // If mouse has clicked on this box
            var isHovering = bounds.Contains(Mouse.Position);
            if (isHovering && Mouse.IsPressed)
            {
                Element.SetElementActive();
            }

            // Draw text box frame
            DrawRect(bounds, GetInteractionColor(isHovering, false));
            Shrink(ref bounds, Style.TextPadding);

            PushClip((Rectangle) bounds);

            if (Element.IsActiveElement)
            // todo: Element.IsElementFresh - frame to initialize element
            // todo: Element.IsElementBlur - frame to deinitialize element
            {
                // Initialize the text editor
                TextEditor.Initialize(text, bounds);

                // Draw selections
                Graphics.Color = Style.ActiveColor;
                foreach (var selectionRect in TextEditor.SelectionRectangles)
                {
                    Graphics.DrawRect(selectionRect);
                }

                // todo: scroll text into view?
                Graphics.Color = Style.TextColor;
                Graphics.DrawText(text, bounds, Style.Font, Style.FontSize, TextEditor.CharacterCallback);

                // Render cursor
                if (TextEditor.ShowCursor) // causes the cursor to blink
                {
                    var lineHeight = Style.Font.GetMetrics(Style.FontSize).Height;

                    // Draw the cursor
                    Graphics.Color = Style.TextColor;
                    Graphics.DrawLine(TextEditor.CursorPosition, TextEditor.CursorPosition + (Vector.Down * lineHeight), 2);
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
                Graphics.Color = Style.TextColor;
                Graphics.DrawText(text, bounds, Style.Font, Style.FontSize);
            }

            PopClip();

            // Text is not modified.
            return false;

            static IntRectangle LayoutTitleAndBox(string title, string text, bool multiline)
            {
                //if (multiline)
                //{
                //    // Draw title
                //    var titleHeight = (int) TextLayout.Measure(title, Style.Font, Style.FontSize).Height;
                //    var bounds = GetNextLayoutBox(titleHeight, space: Style.TextPadding.Y);
                //    var textboxWidth = bounds.Width - (Style.TextPadding.X * 2);
                //    PrependText(title, ref bounds, false);

                //    // Compute text area (multi-line)
                //    var textMeasure = TextLayout.Measure(text, (textboxWidth, int.MaxValue), Style.Font, Style.FontSize);
                //    var textHeight = (int) Calc.Max(textMeasure.Height + Style.Padding.Y, Style.BasicElementHeight);
                //    return GetNextLayoutBox(textHeight);
                //}
                //else
                //{
                //    // Tile and text area (single-line)
                //    var bounds = GetNextLayoutBox();
                //    PrependText(title, ref bounds, false);
                //    return bounds;
                //}
                return GetNextLayoutBox();
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

        public static void Shrink(ref IntRectangle bounds, IntVector size)
        {
            ShrinkHorizontal(ref bounds, size.X);
            ShrinkVertical(ref bounds, size.Y);
        }

        public static void Shrink(ref IntRectangle bounds, IntSize size)
        {
            ShrinkHorizontal(ref bounds, size.Width);
            ShrinkVertical(ref bounds, size.Height);
        }

        public static void Shrink(ref IntRectangle bounds, int size)
        {
            Shrink(ref bounds, size, size);
        }

        #endregion

        private class ScrollState
        {
            public IntVector Offset;
        }

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

            // an element is active if is current this frame and will be next frame
            internal static bool IsActiveElement => ActiveElement == CurrentElement && NextActiveElement == CurrentElement;

            internal static void SetElementActive()
            {
                NextActiveElement = CurrentElement;
            }
        }
    }
}
