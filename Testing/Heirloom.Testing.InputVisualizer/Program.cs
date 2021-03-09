using System;
using System.Collections.Generic;
using System.Linq;

using Heirloom.Desktop;
using Heirloom.Drawing;
using Heirloom.Mathematics;
using Heirloom.Utilities;

namespace Heirloom.Testing.InputVisualizer
{
    public sealed class Program : GameWrapper
    {
        private readonly RectanglePacker<Visual> _packer;

        public Program()
            : base(CreateWindowGraphics())
        {
            _packer = new RectanglePacker<Visual>(500, 500, PackingAlgorithm.Shelf);

            var visuals = new List<Visual>();
            visuals.AddRange(GetEnumValues<MouseButton>().Select(e => new MouseVisual(e)));
            visuals.AddRange(GetEnumValues<Key>().Select(e => new KeyVisual(e)));

            // Pack visuals in order
            foreach (var visual in visuals.OrderBy(v => v.Layout.Area))
            {
                // 
                if (_packer.TryAdd(visual, visual.Layout.Size))
                {
                    visual.Layout = _packer.GetRectangle(visual);
                }
                else
                {
                    Log.Warning($"Unable to insert '{visual.Name}'");
                }
            }
        }

        private static T[] GetEnumValues<T>() where T : Enum
        {
            return (T[]) Enum.GetValues(typeof(T));
        }

        protected override void Update(float dt)
        {
            Graphics.Clear(Color.DarkGray);
            Graphics.ResetState();

            Graphics.Transform = Matrix.CreateTranslation(6, 6);
            foreach (var visual in _packer.Elements)
            {
                // is recent (change this frame)
                if (visual.IsRecent())
                {
                    Graphics.Color = Color.LightGray;
                    Graphics.DrawRect(visual.Layout);
                }
                // is repeating (os retrigger)
                else if (visual.IsRepeat())
                {
                    Graphics.Color = Color.Orange;
                    Graphics.DrawRect(visual.Layout);
                }

                // Draw label
                Graphics.Color = visual.IsDown() ? Color.White : Color.Black;
                Graphics.DrawText(visual.Name, visual.Layout, Font.Default, 16, TextAlign.Center | TextAlign.Middle);

                // Draw border if held down
                if (visual.IsDown())
                {
                    Graphics.DrawRectOutline(visual.Layout);
                }
            }

            Graphics.Screen.Refresh();
        }

        static void Main(string[] args)
        {
            Application.Run<Program>();
        }

        private static GraphicsContext CreateWindowGraphics()
        {
            var window = new Window("Heirloom - Input Visualization", (512, 512));
            window.Position = (IntVector) (Display.Primary.Size - window.Size) / 2;

            return window.Graphics;
        }

        private abstract class Visual
        {
            public readonly string Name;

            public IntRectangle Layout;

            public Visual(string name)
            {
                Name = name;

                // Initial layout
                var layout = TextLayout.Measure(Name, Font.Default, 16);
                Layout = (IntRectangle) Rectangle.Inflate(layout, 4);
            }

            internal abstract bool IsRecent();

            internal abstract bool IsRepeat();

            internal abstract bool IsDown();
        }

        private sealed class KeyVisual : Visual
        {
            public readonly Key Key;

            public KeyVisual(Key key)
                : base($"{key}")
            {
                Key = key;
            }

            internal override bool IsDown()
            {
                return Input.IsKeyDown(Key);
            }

            internal override bool IsRecent()
            {
                return Input.IsKeyPressed(Key) || Input.IsKeyReleased(Key);
            }

            internal override bool IsRepeat()
            {
                return Input.IsKeyPressed(Key, true);
            }
        }

        private sealed class MouseVisual : Visual
        {
            public readonly MouseButton Button;

            public MouseVisual(MouseButton button)
                : base($"Mouse {button}")
            {
                Button = button;
            }

            internal override bool IsDown()
            {
                return Input.IsMouseDown(Button);
            }

            internal override bool IsRecent()
            {
                return Input.IsMousePressed(Button) || Input.IsMousePressed(Button);
            }

            internal override bool IsRepeat()
            {
                return false;
            }
        }
    }
}
