using Heirloom.Math;

namespace Examples.Gridcannon.Engine
{
    public sealed class Transform : Component
    {
        private Vector _position = Vector.Zero;
        private float _rotation = 0;
        private Vector _scale = Vector.One;

        private Matrix _matrix = Matrix.Identity;
        private Matrix _inverseMatrix = Matrix.Identity;
        private bool _needComputeInverse = false;
        private bool _needCompute = false;

        /// <summary>
        /// Gets or sets the position of the object.
        /// </summary>
        public Vector Position
        {
            get => _position;

            set
            {
                _position = value;
                _needCompute = true;
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
                _needCompute = true;
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
                _needCompute = true;
            }
        }

        /// <summary>
        /// Gets the transformation matrix of the object.
        /// </summary>
        public Matrix Matrix
        {
            get
            {
                if (_needCompute)
                {
                    _matrix = Matrix.CreateTransform(_position, _rotation, _scale);
                    _needComputeInverse = true;
                    _needCompute = false;
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

        internal Transform(Entity entity)
            : base(entity)
        { }
    }
}
