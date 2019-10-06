using Heirloom.Drawing;
using Heirloom.Math;

namespace Heirloom.Game
{
    public sealed class Camera : Entity
    // todo: Compute camera bounds
    {
        private float _zoom = 1F;
        private Matrix _cameraMatrix;
        private bool _changed;

        /// <summary>
        /// Construct a new camera entity.
        /// </summary>
        public Camera()
        {
            Transform.Changed += OnTransformUpdated;
        }

        /// <summary>
        /// Camera magnification.
        /// </summary>
        public float Zoom
        {
            get => _zoom;
            set
            {
                _changed = true;
                _zoom = value;
            }
        }

        public Color BackgroundColor { get; set; } = Colors.FlatUI.WetAshphalt;

        public Rectangle Viewport { get; set; } = Rectangle.One;

        public Surface Surface { get; set; }

        public Matrix CameraMatrix
        {
            get
            {
                if (_changed)
                {
                    _cameraMatrix = Matrix.CreateTransform(-Transform.Position, 0, Zoom);
                    _changed = false;
                }

                return _cameraMatrix;
            }
        }

        private void OnTransformUpdated()
        {
            _changed = true;
        }

        protected internal override void Update(float dt)
        {
            // Does nothing?
            // Possibly have events for the camera bounds changing or something like that
        }
    }
}
