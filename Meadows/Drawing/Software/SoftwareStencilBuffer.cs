
using Meadows.Mathematics;

namespace Meadows.Drawing.Software
{
    internal sealed class SoftwareStencilBuffer
    {
        private readonly byte[,] _byte;

        public SoftwareStencilBuffer(IntSize size)
        {
            _byte = new byte[size.Height, size.Width];
        }

        public byte GetValue(IntVector co)
        {
            return _byte[co.Y, co.X];
        }

        public void SetValue(IntVector co, byte value)
        {
            _byte[co.Y, co.X] = value;
        }
    }
}
