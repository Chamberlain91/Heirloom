using System;

using Heirloom.Drawing;
using Heirloom.Math;

namespace Heirloom.Game
{
    public sealed class Camera : Entity
    {
        private float _scale = 1F;
        private Rectangle _bounds;
        private Matrix _matrix, _inverse;

        private IntSize _surfaceSize;
        private Surface _surface;

        private bool _needComputeMatrix;
        private bool _needComputeInverse;
        private bool _needComputeBounds;

        /// <summary>
        /// Construct a new camera entity.
        /// </summary>
        public Camera()
        {
            Surface = GameContext.Instance.Graphics.DefaultSurface;
            Transform.Changed += MarkDirty;
        }

        /// <summary>
        /// Gets or sets the camera scaling factor (ie, zoom).
        /// </summary>
        public float Scale
        {
            get => _scale;

            set
            {
                _scale = value;
                MarkDirty();
            }
        }

        /// <summary>
        /// Gets or sets the background color used when rendering from this camera.
        /// </summary>
        public Color BackgroundColor { get; set; } = Color.DarkGray;

        /// <summary>
        /// Gets or sets the normalized viewport coordinates used when rendering from this camera.
        /// </summary>
        public Rectangle Viewport { get; set; } = Rectangle.One;

        /// <summary>
        /// Gets or sets the target surface used to draw on when rendering from this camera.
        /// </summary>
        public Surface Surface
        {
            get => _surface;

            set
            {
                _surface = value ?? throw new ArgumentNullException(nameof(value));
                _surfaceSize = _surface.Size;

                MarkDirty();
            }
        }

        /// <summary>
        /// Gets the matrix used to transform from world to surface.
        /// </summary>
        public Matrix Matrix
        {
            get
            {
                DetectSurfaceResize();

                if (_needComputeMatrix)
                {
                    var viewportScale = (Vector) Viewport.Size;

                    var offset = viewportScale * Surface.Bounds.Center;
                    _matrix = Matrix.CreateTransform(-Transform.Position + offset, -Transform.Rotation, viewportScale * Scale);
                    _needComputeMatrix = false;
                }

                return _matrix;
            }
        }

        /// <summary>
        /// Gets the matrix used to transform from surface to world.
        /// </summary>
        public Matrix InverseMatrix
        {
            get
            {
                DetectSurfaceResize();

                if (_needComputeInverse)
                {
                    _inverse = Matrix.Inverse(Matrix);
                    _needComputeInverse = false;
                }

                return _inverse;
            }
        }

        /// <summary>
        /// Gets the bounds of the camera in world space.
        /// </summary>
        public Rectangle Bounds
        {
            get
            {
                DetectSurfaceResize();

                if (_needComputeBounds)
                {
                    // Compute transformed points
                    var size = (Vector) Viewport.Size;
                    var p0 = InverseMatrix * (Surface.Bounds.TopLeft * size + Viewport.Position);
                    var p1 = InverseMatrix * (Surface.Bounds.TopRight * size + Viewport.Position);
                    var p2 = InverseMatrix * (Surface.Bounds.BottomLeft * size + Viewport.Position);
                    var p3 = InverseMatrix * (Surface.Bounds.BottomRight * size + Viewport.Position);

                    // Construct bounds
                    _bounds = Rectangle.FromPoints(p0, p1, p2, p3);

                    _needComputeBounds = false;
                }

                return _bounds;
            }
        }

        private void MarkDirty()
        {
            _needComputeMatrix = true;
            _needComputeInverse = true;
            _needComputeBounds = true;
        }

        private void DetectSurfaceResize()
        {
            if (_surfaceSize != _surface.Size)
            {
                _surfaceSize = _surface.Size;
                MarkDirty();
            }
        }
    }
}
