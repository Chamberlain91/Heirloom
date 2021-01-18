using Heirloom.Mathematics;

namespace Heirloom.Testing.Performance
{
    internal sealed class MatrixPerformance : PerformanceTest
    {
        private readonly Matrix _a;
        private readonly Matrix _b;
        private Matrix _c;

        public MatrixPerformance()
        {
            _a = Matrix.CreateRotation(Calc.Pi / 3);
            _b = Matrix.CreateTranslation(12, 34);
        }

        [Test]
        public void MultiplyOperator()
        {
            _c = _a * _b;
        }

        [Test]
        public void MultiplyFunction()
        {
            _c = Matrix.Multiply(_a, _b);
        }

        [Test]
        public void MultiplyFunctionRef()
        {
            Matrix.Multiply(_a, _b, ref _c);
        }

        [Test]
        public void Inverse()
        {
            _c = Matrix.Inverse(_a);
        }

        [Test]
        public void InverseRef()
        {
            Matrix.Inverse(_a, ref _c);
        }

        [Test]
        public void Inverted()
        {
            _c = _a.Inverted;
        }
    }
}
