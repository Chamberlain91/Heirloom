using System;
using System.Runtime.InteropServices;

namespace Heirloom.IO
{
    /// <summary>
    /// A structured byte to configure the 8 individual bits as a method of storing 'compressed' boolean values.
    /// </summary>
    [Serializable, StructLayout(LayoutKind.Sequential, Size = 1, Pack = 1)]
    public struct BitField
    {
        private const int BitCount = 8;

        private byte _value;

        public BitField(byte value = 0)
        {
            _value = value;
        }

        public bool GetBit(int index)
        {
            //
            if ((index < 0) && (index >= BitCount))
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            // 
            return ((_value >> index) & 0x1) == 0x1;
        }

        public void SetBit(int index, bool bit)
        {
            //
            if ((index < 0) && (index >= BitCount))
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            // 
            var mask = (byte) (1 << index); // pow(2, nth)
            if (bit) { _value = (byte) (_value | mask); }
            else { _value = (byte) (_value & ~mask); }
        }

        public static implicit operator byte(BitField field)
        {
            return field._value;
        }

        public static implicit operator BitField(byte value)
        {
            return new BitField(value);
        }
    }
}
