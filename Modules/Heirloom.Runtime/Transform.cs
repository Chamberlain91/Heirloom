using Heirloom.Math;

namespace Heirloom.Runtime
{
    public sealed class Transform
    {
        private Vector _position;
        private Vector _scale;
        private float _rotation;

        private LazyValue<Matrix> _matrix;
        private LazyValue<Matrix> _invMatrix;
        private uint _version;

        internal Transform()
        {
            Position = (0, 0);
            Scale = (1, 1);
            Rotation = 0;
        }

        // == Components

        public Vector Position
        {
            get => _position;

            set
            {
                _position = value;
                _version++;
            }
        }

        public Vector Scale
        {
            get => _scale;

            set
            {
                _scale = value;
                _version++;
            }
        }

        public float Rotation
        {
            get => _rotation;

            set
            {
                _rotation = value;
                _version++;
            }
        }

        // == Matrix

        public Matrix Matrix
        {
            get
            {
                // Is matrix out of date?
                if (_matrix.Version != _version)
                {
                    // todo: parented entities/transforms
                    var matrix = Matrix.CreateTransform((Vector) Position, (float) Rotation, (Vector) Scale);

                    // Set the current value
                    _matrix.Set(matrix, _version);
                }

                return _matrix;
            }
        }

        public Matrix InverseMatrix
        {
            get
            {
                if (_invMatrix.Version != _version)
                {
                    // Set the current value
                    _invMatrix.Set(Matrix.Inverse(Matrix), _version);
                }

                return _invMatrix;
            }
        }
    }
}
