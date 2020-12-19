using System;

using Meadows.Mathematics;

namespace Meadows.Drawing
{
    /// <summary>
    /// Represents a separate view drawn to the screen.
    /// Useful for implementing a game camera, handling spit screens views, minimaps, etc.
    /// </summary>
    public sealed class View
    {
        private readonly Version _version = new();

        private Rectangle _viewport = (0F, 0F, 1F, 1F);
        private IntSize _size;

        private Vector _position;
        private float _angle;

        public View(int width, int height)
            : this(new IntSize(width, height))
        { }

        public View(IntSize size)
        {
            Size = size;
        }

        /// <summary>
        /// Gets the view version number.
        /// </summary>
        public uint Version => _version;

        /// <summary>
        /// Gets or sets the viewport in normalized coordinates.
        /// </summary>
        public Rectangle Viewport
        {
            get => _viewport;

            set
            {
                // Warn the user the viewport is off the screen
                if (value.Top < 0F || value.Left < 0F || value.Bottom > 1F || value.Right > 1F)
                {
                    Log.Warning("The viewport is out of bounds");
                }

                _version.Increment();
                _viewport = value;
            }
        }

        /// <summary>
        /// Gets or sets the position of the view in pixels.
        /// </summary>
        public Vector Position
        {
            get => _position;

            set
            {
                _version.Increment();
                _position = value;
            }
        }

        /// <summary>
        /// Gets or sets the rotation of the view in radians.
        /// </summary>
        public float Rotation
        {
            get => _angle;

            set
            {
                _version.Increment();
                _angle = value;
            }
        }

        /// <summary>
        /// Gets or sets the size of view in pixels.
        /// </summary>
        public IntSize Size
        {
            get => _size;

            set
            {
                if (value.Width <= 0 || value.Height <= 0)
                {
                    throw new ArgumentException("Size must be greater than zero in both dimensions.");
                }

                _version.Increment();
                _size = value;
            }
        }
    }
}
