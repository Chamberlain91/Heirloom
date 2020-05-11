using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using Heirloom;
using Heirloom.IO;

namespace Examples.GameInput
{
    /// <summary>
    /// This object demonstrates usage of text input using the <see cref="Screen.CharacterTyped"/> event.
    /// </summary>
    internal sealed class Chatbox
    {
        private readonly Queue<string> _messageQueue = new Queue<string>();
        private string _messageBuffer = string.Empty;

        private readonly Emote[] _emotes;

        private float _blinkInterval = 0;
        private bool _blinkCursor;

        /// <summary>
        /// Constructs a new <see cref="Chatbox"/> with the specified bounds.
        /// </summary>
        public Chatbox(IntRectangle bounds)
        {
            Bounds = bounds;

            _emotes = CreateEmotes();

            // Hook up keyboard events
            Input.CharacterTyped += OnCharacterTyped;
            Input.KeyPressed += OnKeyPressed;
            Input.KeyRepeat += OnKeyPressed;

            // 
            SubmitMessage("System", "Press <Enter> to type a chat message.");
            SubmitMessage("System", "WASD to move player.");
            SubmitMessage("System", "Type '!help' for more info.");
        }

        /// <summary>
        /// Gets a value that determines if the chatbox has focus.
        /// </summary>
        public bool HasFocus { get; set; }

        /// <summary>
        /// Gets or sets the bounds of the chatbox.
        /// </summary>
        public IntRectangle Bounds { get; set; }

        #region Emotes

        public event Action<Emote> EmoteDetected;

        private void OnEmoteDetected(Emote emote)
        {
            EmoteDetected?.Invoke(emote);
        }

        /// <summary>
        /// Constructs the known set of emotes.
        /// </summary>
        private Emote[] CreateEmotes()
        {
            var emoteRegex = new Regex(@"emote_([a-z]+)\.png", RegexOptions.Compiled);
            var emoteFiles = Files.GetEmbeddedFiles()
                                  .Select(e => e.Identifiers.First())
                                  .Where(path => emoteRegex.IsMatch(path));

            // 
            return LoadEmotes().ToArray();

            IEnumerable<Emote> LoadEmotes()
            {
                // 
                foreach (var emoteIdentifier in emoteFiles)
                {
                    // Match regex to extract command name
                    var match = emoteRegex.Match(emoteIdentifier);
                    var command = match.Groups[1].Value;

                    // 
                    yield return new Emote(command, GetKeywords(command).ToArray());
                }
            }

            static IEnumerable<Regex> GetKeywords(string command)
            {
                const RegexOptions IgnoreCase = RegexOptions.IgnoreCase | RegexOptions.Compiled;

                switch (command)
                {
                    case "angry":
                        yield return new Regex(@">:\($"); // >:(
                        break;

                    case "broken_heart":
                        yield return new Regex(@"</3$"); // </3
                        break;

                    case "cash":
                        yield return new Regex(@"^\$+$"); // $, $$$, etc
                        break;

                    case "sad":
                        yield return new Regex(@":\($"); // :(
                        yield return new Regex(@"=\($"); // =(
                        break;

                    case "exclamation":
                        yield return new Regex(@".*!+$"); // !
                        break;

                    case "happy":
                        yield return new Regex(@":\)$"); // :)
                        yield return new Regex(@":D$");  // :D
                        yield return new Regex(@"=D$");  // =)
                        break;

                    case "heart":
                        yield return new Regex(@"<3$"); // <3
                        break;

                    case "hearts":
                        yield return new Regex(@"<33$");  // <33
                        yield return new Regex(@"<3<3$"); // <3<3
                        break;

                    case "laugh":
                        yield return new Regex(@"^haha$", IgnoreCase); // haha
                        yield return new Regex(@"^lol$", IgnoreCase);  // lol
                        break;

                    case "question":
                        yield return new Regex(@".*\?+$");
                        break;

                    case "sleep":
                        yield return new Regex(@"^zzz$");
                        break;

                    case "star":
                        yield return new Regex(@"^\*$");
                        break;

                    case "stars":
                        yield return new Regex(@"^\*\*\*$");
                        break;

                    case "swirl":
                        yield return new Regex(@"^@$");
                        break;
                }
            }
        }

        #endregion

        #region Text Input / Messages

        private void SubmitMessage(string who, string message)
        {
            message = message.Trim();

            // Enqueue message to chatbox
            _messageQueue.Enqueue($"({who}) {message}");

            // Throw away oldest message
            if (_messageQueue.Count > 6) { _messageQueue.Dequeue(); }

            // Detect and react to emotes
            foreach (var emote in _emotes)
            {
                // Check if messsage is directly an emote command
                // ie, !happy
                if (message == $"!{emote.Command}")
                {
                    OnEmoteDetected(emote);
                    return;
                }

                // Test message for other patterns
                foreach (var pattern in emote.Patterns)
                {
                    if (pattern.IsMatch(message))
                    {
                        OnEmoteDetected(emote);
                        return;
                    }
                }
            }
        }

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
                if (e.Key == Key.Enter)
                {
                    // Trim text from leading and trailing whitespaces
                    _messageBuffer = _messageBuffer.Trim();

                    // Ensure the text has content
                    if (!string.IsNullOrWhiteSpace(_messageBuffer))
                    {
                        SubmitMessage("User", _messageBuffer);

                        // Help was requested
                        if (_messageBuffer == "!help")
                        {
                            SubmitMessage("System", $"Commands are the pattern of '!command'.");
                            SubmitMessage("System", $"Emote commands: {string.Join(", ", _emotes.Select(e => e.Command))}.");
                        }
                    }

                    // Clear what the user has typed
                    _messageBuffer = string.Empty;
                }

                // Stop typing
                if (e.Key == Key.Escape)
                {
                    HasFocus = false;
                }
            }
            else
            {
                // Begin typing
                if (e.Key == Key.Enter)
                {
                    HasFocus = true;
                }
            }
        }

        private void OnCharacterTyped(Screen s, CharacterEvent e)
        {
            if (HasFocus)
            {
                _messageBuffer += e.Character;
            }
        }

        #endregion

        public void Draw(GraphicsContext gfx, float dt)
        {
            // Animate the "text cursor"
            _blinkInterval += dt;
            if (_blinkInterval >= 0.5F)
            {
                _blinkCursor = !_blinkCursor;
                _blinkInterval -= 0.5F;
            }

            var fade = HasFocus ? Color.White : Palette.ChatboxFade;

            gfx.PushState(reset: true);
            {
                // Draw box background
                gfx.Color = Palette.ChatboxBackground * fade;
                gfx.DrawRect(Bounds);

                // Draw box border
                gfx.Color = Palette.ChatboxBorder * fade;
                gfx.DrawRectOutline(Bounds, 1);

                // Draw messages
                gfx.Viewport = IntRectangle.Inflate(Bounds, -8);
                {
                    var offset = 0F;

                    // Draw the user text
                    gfx.Color = Palette.ChatboxUserText * fade;
                    {
                        var layout = TextLayout.Measure(_messageBuffer + " ", gfx.Viewport.Size, Font.Default, 16);
                        var messageBounds = new Rectangle((0, gfx.Viewport.Height - layout.Height), gfx.Viewport.Size);
                        gfx.DrawText(_messageBuffer + (_blinkCursor && HasFocus ? "_" : ""), messageBounds, Font.Default, 16);
                        offset += layout.Height;
                    }

                    // 
                    offset += 2;
                    gfx.Color = Palette.ChatboxBorder * fade;
                    gfx.DrawLine((0, gfx.Viewport.Height - offset), (gfx.Viewport.Width, gfx.Viewport.Height - offset));
                    offset += 8;

                    // Draw the submitted messages
                    gfx.Color = Palette.ChatboxText * fade;
                    {
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
