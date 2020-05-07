using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using Heirloom;

namespace Examples.Input
{
    using Input = Heirloom.Input;

    /// <summary>
    /// This object demonstrates usage of text input using the <see cref="Screen.CharacterTyped"/> event.
    /// </summary>
    internal sealed class Chatbox
    {
        private readonly Queue<string> _messageQueue = new Queue<string>();
        private string _messageBuffer = string.Empty;

        private float _blinkInterval = 0;
        private bool _blinkCursor;

        /// <summary>
        /// Constructs a new <see cref="Chatbox"/> with the specified bounds.
        /// </summary>
        public Chatbox(IntRectangle bounds)
        {
            Bounds = bounds;

            // Hook up keyboard events
            Input.CharacterTyped += OnCharacterTyped;
            Input.KeyPressed += OnKeyPressed;
            Input.KeyRepeat += OnKeyPressed;

            // 
            SubmitMessage("System: Press <Enter> to type a chat message.");
            SubmitMessage("System: Click and drag to toss boxes.");
        }

        /// <summary>
        /// Gets a value that determines if the chatbox has focus.
        /// </summary>
        public bool HasFocus => Program.Focus == this;

        /// <summary>
        /// Gets or sets the bounds of the chatbox.
        /// </summary>
        public IntRectangle Bounds { get; set; }

        private void OnKeyPressed(Screen s, KeyEvent e)
        {
            if (HasFocus)
            {
                // Delete character (backspace)
                if (e.Key == Key.Backspace && _messageBuffer.Length > 0)
                {
                    // Substring except last character
                    _messageBuffer = _messageBuffer[0..^1];
                }

                // Submit message
                if (e.Key == Key.Enter && !string.IsNullOrWhiteSpace(_messageBuffer))
                {
                    SubmitMessage("User: " + _messageBuffer);

                    // Try to reply if the user types a keyword
                    var replyBuffer = Regex.Replace(_messageBuffer, @"[^\w\d\s]", " ").ToUpper();
                    foreach (var word in replyBuffer.Split(' ', System.StringSplitOptions.RemoveEmptyEntries))
                    {
                        if (word == "HI" || word == "HELLO" || word == "HEY" || word == "HIYA")
                        {
                            SubmitMessage("System: Hello, User.");
                            break;
                        }

                        if (word == "EXIT" || word == "STOP" || word == "QUIT")
                        {
                            SubmitMessage("System: Press the 'X' on the window.");
                            break;
                        }

                        if (word == "HELP")
                        {
                            SubmitMessage("System: I'm not a real AI.");
                            break;
                        }
                    }

                    // Clear what the user has typed
                    _messageBuffer = string.Empty;

                    // Disable focus
                    Program.Focus = null;
                }

                // Stop typing
                if (e.Key == Key.Escape)
                {
                    Program.Focus = null;
                }
            }
            else
            {
                // Begin typing
                if (e.Key == Key.Enter)
                {
                    Program.Focus = this;
                }
            }
        }

        private void SubmitMessage(string message)
        {
            _messageQueue.Enqueue(message);

            // Throw away oldest message
            if (_messageQueue.Count > 6) { _messageQueue.Dequeue(); }
        }

        private void OnCharacterTyped(Screen s, CharacterEvent e)
        {
            if (HasFocus)
            {
                _messageBuffer += e.Character;
            }
        }

        public void Update(GraphicsContext gfx, float dt)
        {
            // Animate the "text cursor"
            _blinkInterval += dt;
            if (_blinkInterval >= 0.5F)
            {
                _blinkCursor = !_blinkCursor;
                _blinkInterval -= 0.5F;
            }

            gfx.PushState(reset: true);
            {
                // Draw box background
                gfx.Color = Palette.ChatboxBackground;
                gfx.DrawRect(Bounds);

                // Draw box border
                gfx.Color = HasFocus ? Palette.ChatboxFocusBorder : Palette.ChatboxBorder;
                gfx.DrawRectOutline(Bounds, 2);

                // Draw messages
                gfx.Viewport = IntRectangle.Inflate(Bounds, -8);
                {
                    var offset = 0F;

                    // Draw the user text
                    if (HasFocus)
                    {
                        gfx.Color = Palette.ChatboxUserText;

                        // Draws the "text input" buffer
                        var layout = TextLayout.Measure(_messageBuffer + " ", gfx.Viewport.Size, Font.Default, 16);
                        var messageBounds = new Rectangle((0, gfx.Viewport.Height - layout.Height), gfx.Viewport.Size);
                        gfx.DrawText(_messageBuffer + (_blinkCursor && HasFocus ? "_" : ""), messageBounds, Font.Default, 16);
                        offset += layout.Height;
                    }

                    // Draw the submitted messages
                    {
                        gfx.Color = Palette.ChatboxText;

                        // Draws each message in the chatbox
                        foreach (var message in _messageQueue.Reverse())
                        {
                            // Measure the height of this text
                            var layout = TextLayout.Measure(message, gfx.Viewport.Size, Font.Default, 16);
                            var messageBounds = new Rectangle((0, gfx.Viewport.Height - offset - layout.Height), gfx.Viewport.Size);
                            gfx.DrawText(message, messageBounds, Font.Default, 16);
                            offset += layout.Height;
                        }
                    }
                }
            }
            gfx.PopState();
        }
    }
}
