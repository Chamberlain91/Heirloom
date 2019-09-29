using System;

using Heirloom.Math;

namespace Heirloom.Game
{
    public sealed class Transform : Component
    // todo: Transform Hierarchy via Parent and Children properties
    {
        private Vector _position = Vector.Zero;
        private float _rotation = 0;
        private Vector _scale = Vector.One;

        private Matrix _matrix = Matrix.Identity;
        private Matrix _inverseMatrix = Matrix.Identity;

        private bool _needComputeInverse;
        private bool _needComputeMatrix;
        private bool _wasChanged;

        public event Action Changed;

        internal Transform()
        {
            MarkMutation();
        }

        /// <summary>
        /// Gets or sets the position of the object.
        /// </summary>
        public Vector Position
        {
            get => _position;

            set
            {
                _position = value;
                MarkMutation();
            }
        }

        /// <summary>
        /// Gets or sets the rotation of the object (in radians).
        /// </summary>
        public float Rotation
        {
            get => _rotation;

            set
            {
                _rotation = value;
                MarkMutation();
            }
        }

        /// <summary>
        /// Gets or sets the scale of the object.
        /// </summary>
        public Vector Scale
        {
            get => _scale;

            set
            {
                _scale = value;
                MarkMutation();
            }
        }

        /// <summary>
        /// Gets the transformation matrix of the object.
        /// </summary>
        public Matrix Matrix
        {
            get
            {
                if (_needComputeMatrix)
                {
                    _matrix = Matrix.CreateTransform(_position, _rotation, _scale);
                    _needComputeMatrix = false;
                }

                return _matrix;
            }
        }

        /// <summary>
        /// Gets the inverse transformation matrix of the object.
        /// </summary>
        public Matrix InverseMatrix
        {
            get
            {
                if (_needComputeInverse)
                {
                    _inverseMatrix = Matrix.Inverse(Matrix);
                    _needComputeInverse = false;
                }

                return _inverseMatrix;
            }
        }

        protected internal override void Update(float dt)
        {
            if (_wasChanged)
            {
                _wasChanged = false;
                Changed?.Invoke();
            }
        }

        private void MarkMutation()
        {
            _needComputeMatrix = true;
            _needComputeInverse = true;
            _wasChanged = true;
        }
    }
}
