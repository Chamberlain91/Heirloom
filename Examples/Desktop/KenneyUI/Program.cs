using Heirloom.Drawing;
using Heirloom.IO;
using Heirloom.Math;
using Heirloom.Platforms.Desktop;

namespace KenneyUI
{
    public class Program : GameWindow
    {
        public string Text = @"Alice was beginning to get very tired of sitting by her sister on the bank, and of having nothing to do: once or twice she had peeped into the book her sister was reading, but it had no pictures or conversations in it, `and what is the use of a book,' thought Alice `without pictures or conversation?'
So she was considering in her own mind (as well as she could, for the hot day made her feel very sleepy and stupid), whether the pleasure of making a daisy-chain would be worth the trouble of getting up and picking the daisies, when suddenly a White Rabbit with pink eyes ran close by her.
There was nothing so very remarkable in that; nor did Alice think it so very much out of the way to hear the Rabbit say to itself, `Oh dear! Oh dear! I shall be late!' (when she thought it over afterwards, it occurred to her that she ought to have wondered at this, but at the time it all seemed quite natural); but when the Rabbit actually took a watch out of its waistcoat-pocket, and looked at it, and then hurried on, Alice started to her feet, for it flashed across her mind that she had never before seen a rabbit with either a waistcoat-pocket, or a watch to take out of it, and burning with curiosity, she ran across the field after it, and fortunately was just in time to see it pop down a large rabbit-hole under the hedge.";

        private ExampleButton[] _buttons = new ExampleButton[3];
        private float _sliderPosition = 25;
        private bool _toggleButton = false;

        private NineSlice _dropdownFrame;
        private string[] _words = new[]
        {
            "Congratulations",
            "Rumpelstiltskin",
            "Characteristics",
            "Thermochemistry",
            "Miniaturization",
            "Discombobulated",
            "Parthenogenesis"
        };

        struct ExampleButton
        {
            public string Text;
            public int Count;
        }

        public Program()
            : base("Kenney UI Example", vsync: true)
        {
            // SetSwapInterval(0);
            Maximize();

            // Configure GUI on this window
            GUI.SetRenderingContext(RenderContext);
            GUI.BindEvents(Keyboard, Mouse);

            var fontBold = LoadFont("trueno_bold.otf");
            var font = LoadFont("trueno_regular.otf");

            _dropdownFrame = NineSlice.Create(LoadImage("grey_button05.png"), (16, 16, 160, 16));

            // TODO: Embed assets inside GUI libray
            GUI.Style = new GUI.GUIStyle
            {
                // 
                ElementHeight = 32,
                Padding = 8,

                // Set button style
                Button = new GUI.ButtonStyle
                {
                    // Graphics
                    UpFrame = NineSlice.Create(LoadImage("blue_button09.png"), (16, 16, 16, 16)),
                    DownFrame = NineSlice.Create(LoadImage("blue_button10.png"), (16, 16, 16, 16)),

                    // Text styling
                    PressedOffset = 2,
                    Text = new GUI.TextStyle
                    {
                        Color = Color.White,
                        Font = font,
                        Size = 13
                    }
                },

                // Set window style
                Window = new GUI.WindowStyle
                {
                    // Graphics
                    ContentFrame = NineSlice.Create(LoadImage("grey_panel.png"), (16, 16, 64, 64)),
                    WindowFrame = NineSlice.Create(LoadImage("blue_panel.png"), (16, 16, 64, 64)),

                    // Text styling
                    TextAlign = TextAlign.Center,
                    Text = new GUI.TextStyle
                    {
                        Color = Color.White,
                        Font = fontBold,
                        Size = 18
                    }
                },

                // Set slider style
                Slider = new GUI.SliderStyle
                {
                    // Graphics
                    HorizontalBar = LoadImage("grey_sliderHorizontal.png"),
                    HorizontalHandle = LoadImage("grey_sliderUp.png"),
                    VerticalBar = LoadImage("grey_sliderVertical.png"),
                    VerticalHandle = LoadImage("grey_sliderRight.png"),
                    EndCap = LoadImage("grey_sliderEnd.png"),

                    // Text styling
                    Text = new GUI.TextStyle
                    {
                        Color = Color.Gray,
                        Font = font,
                        Size = 10
                    }
                },

                // Set generic text style (text, labels, etc)
                Text = new GUI.TextStyle
                {
                    Color = Color.Gray,
                    Font = font,
                    Size = 13
                }
            };

            // 
            _buttons[0].Text = "Option A";
            _buttons[1].Text = "Option B";
            _buttons[2].Text = "Option C";
        }

        protected override void Update()
        {
            // 
        }

        protected override void Render(RenderContext ctx)
        {
            // 
            ctx.Clear(Colors.FlatUI.Sunflower);

            // 
            GUI.BeginFrame();

            // Compute window size
            var width = Calc.Min(800, Width);
            var height = Calc.Min(600, Height);
            var x = (Width - width) / 2F;
            var y = (Height - height) / 2F;

            // Draw window
            var windowRect = new Rectangle(8 + x, 8 + y, width - 16, height - 16);
            GUILayout.Container = GUI.Window("Tutorial 01 - Traffic Simulation", ref windowRect);

            // Draw text
            GUILayout.Text(Text);

            // Draw buttons
            for (var i = 0; i < 3; i++)
            {
                if (GUILayout.Button($"{_buttons[i].Text} ({_buttons[i].Count} clicks)"))
                {
                    _buttons[i].Count++;
                }
            }

            // 
            _sliderPosition = GUILayout.Slider(_sliderPosition, 0, 100);
            _toggleButton = GUILayout.ToggleButton($"Enable hyperdrive?", _toggleButton);

            // 
            GUI.EndFrame();

            // Something like a dropdown/list...?

            var itemHeight = 16F + GUI.Style.Padding; // 16, estimate line height...
            var totalHeight = (itemHeight * _words.Length) - GUI.Style.Padding;
            var totalWidth = 200;

            var dropRect = new Rectangle(0, 0, totalWidth + 16, totalHeight + 16);
            ctx.Draw(_dropdownFrame, dropRect, Color.White);

            for (var i = 0; i < _words.Length; i++)
            {
                var word = _words[i];

                var textRect = new Rectangle(GUI.Style.Padding, GUI.Style.Padding + (i * itemHeight), totalWidth, itemHeight);
                ctx.DrawText(word, textRect, TextAlign.Left, GUI.Style.Text.Font, GUI.Style.Text.Size, GUI.Style.Text.Color);

                if (i != 0)
                {
                    // Draw divider between this and previous element
                    var v = GUI.Style.Padding / 2F;

                    var p0 = new Vector(textRect.Left, textRect.Top - v);
                    var p1 = new Vector(textRect.Right, textRect.Top - v);
                    ctx.DrawLine(p0, p1, Color.LightGray, 1); // todo: style configurable? ListStyle?
                }
            }
        }

        static void Main(string[] _)
        {
            Run(new Program());
        }

        public static Font LoadFont(string path)
        {
            using (var stream = Files.OpenStream($"assets/fonts/{path}"))
            {
                return new Font(stream);
            }
        }

        public static Image LoadImage(string path)
        {
            using (var stream = Files.OpenStream($"assets/images/{path}"))
            {
                return new Image(stream);
            }
        }
    }
}
