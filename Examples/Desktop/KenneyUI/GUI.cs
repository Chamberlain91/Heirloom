using System;
using Heirloom.Drawing;
using Heirloom.Input;
using Heirloom.Math;

namespace KenneyUI
{
    public static class GUI
    {
        private static RenderContext _ctx;

        // Layout state
        public static GUIStyle Style;

        // Input State
        public static Vector MousePosition;
        public static bool IsMousePressed;
        public static bool IsMouseDown;

        private static uint _idCounter = 1;
        private static uint _idCounterEnd = 1;

        private static uint _currentId = 0;

        public enum LayoutDirection
        {
            Horizontal,
            Vertical
        }

        public struct TextStyle
        {
            public Font Font;
            public Color Color;
            public int Size;

            public Size MeasureText(string text)
            {
                return Font.MeasureText(text, Size);
            }

            public Size MeasureText(string text, Size layoutBox)
            {
                return Font.MeasureText(text, layoutBox, Size);
            }
        }

        public struct ButtonStyle
        {
            public TextStyle Text;

            public NineSlice UpFrame;
            public NineSlice DownFrame;

            public float PressedOffset;
        }

        public struct WindowStyle
        {
            public NineSlice WindowFrame;
            public NineSlice ContentFrame;

            public TextAlign TextAlign;
            public TextStyle Text;
        }

        public struct SliderStyle
        {
            public TextStyle Text;

            public Image HorizontalBar;
            public Image HorizontalHandle;
            public Image VerticalBar;
            public Image VerticalHandle;
            public Image EndCap;
        }

        public struct GUIStyle
        {
            public WindowStyle Window;
            public ButtonStyle Button;
            public SliderStyle Slider;
            public TextStyle Text;

            public float Padding;
            public float ElementHeight;
        }

        public static void SetRenderingContext(RenderContext ctx)
        {
            _ctx = ctx;
        }

        public static void BindEvents(Keyboard keyboard, Mouse mouse)
        {
            // Bind Keyboard
            keyboard.CharacterTyped += Keyboard_CharacterTyped;
            keyboard.KeyDown += Keyboard_KeyDown;
            keyboard.KeyUp += Keyboard_KeyUp;

            // Bind Mouse
            mouse.ButtonDown += Mouse_ButtonDown;
            mouse.ButtonUp += Mouse_ButtonUp;
            mouse.Scroll += Mouse_Scroll;
            mouse.Moved += Mouse_Moved;
        }

        private static void Mouse_Scroll(object sender, MouseScrollEventArgs e)
        {
            // 
        }

        private static void Mouse_Moved(object sender, MouseMoveEventArgs e)
        {
            MousePosition = e.Position;
        }

        private static void Mouse_ButtonUp(object sender, MouseButtonEventArgs e)
        {
            IsMousePressed = false;
            IsMouseDown = false;

            // todo: only do this if the last input source mouse, 
            // as mouse is the only way to interact with the GUI atm, this is ok
            _currentId = 0;
        }

        private static void Mouse_ButtonDown(object sender, MouseButtonEventArgs e)
        {
            IsMousePressed = true;
            IsMouseDown = true;
        }

        private static void Keyboard_KeyUp(object sender, KeyEventArgs e)
        {
            // 
        }

        private static void Keyboard_KeyDown(object sender, KeyEventArgs e)
        {
            // 
        }

        private static void Keyboard_CharacterTyped(object sender, CharacterTypedEventArgs e)
        {
            // 
        }

        public static void BeginFrame()
        {
            _idCounter = 1;
        }

        public static void EndFrame()
        {
            if (_idCounterEnd != _idCounter)
            {
                // Layout changed?
            }

            // 
            _idCounterEnd = _idCounter;
            IsMousePressed = false;
        }

        public static Rectangle Window(string text, ref Rectangle bounds)
        {
            var panelRect = bounds;
            var innerRect = panelRect.Inflate(-Style.Padding);

            var titleHeight = Style.Window.Text.MeasureText("TITLE").Height + Style.Padding;
            var titleRect = new Rectangle(innerRect.X, innerRect.Top, innerRect.Width, titleHeight);
            innerRect.Height -= titleHeight;
            innerRect.Y += titleHeight;

            _ctx.Draw(Style.Window.WindowFrame, panelRect, Color.White);
            _ctx.Draw(Style.Window.ContentFrame, innerRect, Color.White);
            DrawText(text, titleRect, Style.Window.Text, Style.Window.TextAlign);

            return innerRect.Inflate(-Style.Padding);
        }

        public static void Text(string text, in Rectangle bounds, TextAlign align = TextAlign.Left)
        {
            DrawText(text, bounds, Style.Text, align);
        }

        public static bool Button(string text, in Rectangle bounds)
        {
            var id = _idCounter++;

            var isWithin = bounds.Contains(MousePosition);
            var isHold = IsMouseDown && isWithin && _currentId == id;

            DrawButton(text, isHold, bounds);

            // 
            if (isWithin && IsMousePressed)
            {
                _currentId = id;

                // Consumes mouse pressed state
                IsMousePressed = false;
                return true;
            }
            else
            {
                // Was not pressed
                return false;
            }
        }

        public static bool ToggleButton(string text, bool state, Rectangle bounds)
        {
            var id = _idCounter++;

            var isWithin = bounds.Contains(MousePosition);

            DrawButton(text, state, bounds);

            // 
            if (isWithin && IsMousePressed)
            {
                _currentId = id;

                // Consumes mouse pressed state
                IsMousePressed = false;
                return !state;
            }
            else
            {
                // Was not changed
                return state;
            }
        }

        public static float SliderHorizontal(float value, float min, float max, Rectangle bounds)
        {
            var id = _idCounter++;

            // Compute slider graphic bounds
            var sliderRect = bounds;
            sliderRect.Height = Style.Slider.HorizontalBar.Height;
            sliderRect.Width -= Style.Padding * 4;
            sliderRect.Y += (bounds.Height - sliderRect.Height) / 2F;
            sliderRect.X += (bounds.Width - sliderRect.Width) / 2F;

            // Draw the rail/bar
            _ctx.Draw(Style.Slider.HorizontalBar, sliderRect, Color.White);

            // Get the Y coordinate of the center of that rail
            var y = sliderRect.Center.Y;

            // End cap is optional
            if (Style.Slider.EndCap != null)
            {
                // Draw end caps
                // note: ball style end caps, something more will be needed for horizontal or vertical aligned caps
                // for example, maybe ThreeSlice image types, or a more general Image type capable of defining slices/offset
                DrawCenteredImage(Style.Slider.EndCap, (sliderRect.Left, y), out _);
                DrawCenteredImage(Style.Slider.EndCap, (sliderRect.Right, y), out _);
            }

            // Draw min/max text values at end points
            var textPad = Style.Padding / 4F;
            var p0 = sliderRect.BottomLeft;
            p0.Y += textPad;

            var p1 = sliderRect.BottomRight;
            p1.Y += textPad;

            DrawText($"{min}", p0, Style.Slider.Text, TextAlign.Left);
            DrawText($"{max}", p1, Style.Slider.Text, TextAlign.Right);

            // Draw the slider handle
            var x = Calc.Rescale(value, min, max, sliderRect.Left, sliderRect.Right);
            DrawCenteredImage(Style.Slider.HorizontalHandle, (x, y), out var handleRect);

            // 
            if (IsMouseDown)
            {
                // Clicked within the handle or rail
                var isWithinHandle = handleRect.Contains(MousePosition);
                var isClickHandle = IsMousePressed && isWithinHandle;
                var isClickRail = IsMousePressed && sliderRect.Inflate(3).Contains(MousePosition);

                // Compute value
                var newValue = Calc.Rescale(MousePosition.X, sliderRect.Left, sliderRect.Right, min, max);
                newValue = Calc.Clamp(newValue, min, max);

                // Clicked something valid
                if (isClickHandle || isClickRail)
                {
                    IsMousePressed = false;
                    _currentId = id;
                }

                // Cross frame consistent click
                // note: Input consistency model may be flawed, have to evaluate.
                // possible flaw, GUI elements change, id's conflict and input is applied to incorrect element
                // the single pass nature of a imgui makes detecting this a frame late. This might be good enough though,
                // if the layout is detected to have changed, invalidate *all* input until input becomes neutral.
                if (_currentId == id)
                {
                    // Slider was changed
                    return newValue;
                }
            }

            // Nothing happened, return old value
            return value;
        }

        internal static void DrawText(string text, in Rectangle rectangle, TextStyle style, TextAlign align = TextAlign.Left)
        {
            _ctx.DrawText(text, rectangle, align, style.Font, style.Size, style.Color);
        }

        internal static void DrawText(string text, in Vector position, TextStyle style, TextAlign align = TextAlign.Left)
        {
            _ctx.DrawText(text, position, align, style.Font, style.Size, style.Color);
        }

        internal static void DrawCenteredImage(Image image, Vector position, out Rectangle bounds)
        {
            // Compute bounds
            bounds.X = position.X - image.Width / 2F;
            bounds.Y = position.Y - image.Height / 2F;
            bounds.Width = image.Width;
            bounds.Height = image.Height;

            // 
            _ctx.Draw(image, bounds, Color.White);
        }

        private static void DrawButton(string text, bool state, Rectangle bounds)
        {
            var textSize = Style.Button.Text.Font.MeasureText(text, Style.Button.Text.Size);
            var textBounds = bounds.Inflate(-Style.Padding);
            textBounds.Y = (state ? 0 : -2) + bounds.Y + ((bounds.Height - textSize.Height) / 2F);
            textBounds.Height = textSize.Height;

            // Draw button graphic
            // todo: color blending configurable?
            if (state) { _ctx.Draw(Style.Button.DownFrame, bounds, Color.LightGray); }
            else { _ctx.Draw(Style.Button.UpFrame, bounds, Color.White); }

            // Draw button text
            DrawText(text, textBounds, Style.Button.Text, TextAlign.Center);
        }
    }
}
