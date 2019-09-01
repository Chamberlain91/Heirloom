using System;
using Heirloom.Math;

using static KenneyUI.GUI;

namespace KenneyUI
{
    public static class GUILayout
    {
        private static Rectangle _container;
        private static float _offset = 0;

        public static LayoutDirection Direction = LayoutDirection.Vertical;


        public static Rectangle Container
        {
            get => _container;

            set
            {
                _container = value;
                _offset = 0;
            }
        }

        public static float Slider(float value, float min, float max)
        {
            var result = SliderHorizontal(value, min, max, (Container.X, Container.Y + _offset, Container.Width, Style.ElementHeight));
            _offset += Style.ElementHeight + Style.Padding;
            return result;
        }

        public static bool Button(string text)
        {
            var result = GUI.Button(text, (Container.X, Container.Y + _offset, Container.Width, Style.ElementHeight));
            _offset += Style.ElementHeight + Style.Padding;
            return result;
        }

        public static bool ToggleButton(string text, bool state)
        {
            var result = GUI.ToggleButton(text, state, (Container.X, Container.Y + _offset, Container.Width, Style.ElementHeight));
            _offset += Style.ElementHeight + Style.Padding;
            return result;
        }

        public static void Text(string text)
        {
            var textSize = Style.Text.Font.MeasureText(text, Container.Size, Style.Text.Size);
            var textRect = new Rectangle((Container.X, Container.Y + _offset), textSize);

            GUI.Text(text, textRect);

            _offset += textSize.Height + Style.Padding;
        }
    }
}
