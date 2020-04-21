using Heirloom;
using Heirloom.Desktop;

namespace Examples.FakeWindow
{
    internal static class Program
    {
        private static Window _window;
        private static NineSlice _frame;

        private static Image _cross;
        private static Image _crossHover;

        private static Image _card;
        private static bool _isMoveCard;
        private static Vector _cardPos;

        private static bool _isMoveWindow;
        private static IntVector _windowStart;
        private static Vector _dragStart;

        private static bool _isHoverClose;

        private static void Main(string[] args)
        {
            _frame = new NineSlice(new Image("files/frame.png"), new IntRectangle(30, 30, 10, 30));
            _card = new Image("files/cardHeartsQ.png");
            _card.Origin = (IntVector) _card.Size / 2;

            _cross = new Image("files/grey_crossGrey.png");
            _crossHover = new Image("files/red_cross.png");

            Application.Run(() =>
            {
                _window = new Window("Fake Window", transparent: true);
                _window.MoveToCenter();
                _window.IsDecorated = false;

                // 
                _window.MousePress += OnMouseClick;
                _window.MouseRelease += OnMouseClick;
                _window.MouseMove += OnMouseMove;

                // Initial card position
                _cardPos = (Vector) _window.Size / 2F;

                // Begin render loop
                var loop = GameLoop.Create(_window.Graphics, OnDraw);
                loop.Start();
            });
        }

        private static void OnDraw(Graphics gfx, float dt)
        {
            gfx.Clear(Color.Transparent);
            gfx.DrawNineSlice(_frame, new Rectangle(Vector.Zero, _window.Size));

            // Draw titlebar
            gfx.Color = Color.DarkGray;
            gfx.DrawText(_window.Title, (8, 0, _window.Size.Width - 60, 32), Font.Default, 32);

            gfx.Color = Color.White;
            gfx.DrawImage(_isHoverClose ? _crossHover : _cross, (_window.Size.Width - 32, 7));

            // Draw window content
            gfx.Viewport = new IntRectangle(8, 34, _window.Size.Width - 16, _window.Size.Height - 42);

            gfx.Color = Color.White;
            gfx.DrawImage(_card, _cardPos);

            gfx.Color = Color.DarkGray;
            gfx.DrawText("Try dragging the window and card", (2, 0), Font.Default, 16);
        }

        private static void OnMouseClick(Window w, MouseButtonEvent e)
        {
            if (e.Action == ButtonAction.Press)
            {
                if (e.Position.Y < 30 && e.Position.X < _window.Size.Width - 40)
                {
                    _windowStart = _window.Position;
                    _dragStart = _windowStart + e.Position;
                    _isMoveWindow = true;
                }

                // 
                var cardBounds = new Rectangle(_cardPos - (Vector) _card.Size / 2F, _card.Size);
                if (cardBounds.Contains(e.Position - (8, 30))) // minus viewport
                {
                    _isMoveCard = true;
                    _dragStart = e.Position;
                }
            }
            else
            if (e.Action == ButtonAction.Release)
            {
                if (_isHoverClose)
                {
                    // Bye!
                    _window.Close();
                }

                _isMoveWindow = false;
                _isMoveCard = false;
            }
        }

        private static void OnMouseMove(Window w, MouseMoveEvent e)
        {
            if (_isMoveWindow)
            {
                var delta = w.Position + e.Position - _dragStart;
                w.Position = (IntVector) (_windowStart + delta);
            }
            else
            if (_isMoveCard)
            {
                _cardPos += e.Position - _dragStart;
                _dragStart = e.Position;
            }
            else
            {
                _isHoverClose = e.Position.X > _window.Size.Width - 40 && e.Position.Y < 30;
            }
        }
    }
}
