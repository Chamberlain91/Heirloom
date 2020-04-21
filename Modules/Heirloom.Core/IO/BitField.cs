using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Heirloom
{
    /// <summary>
    /// A structured byte to configure the 8 individual bits as a method of storing 'compressed' boolean values.
    /// </summary>
    [Serializable, StructLayout(LayoutKind.Sequential, Size = 1, Pack = 1)]
    public struct BitField : IEquatable<BitField>, IReadOnlyList<bool>
    {
        private const int BitCount = 8;

        private byte _value;

        #region Constructors

        public BitField(byte value = 0)
        {
            _value = value;
        }

        #endregion

        #region Indexer

        public bool this[int index]
        {
            get => GetBit(index);
            set => SetBit(index, value);
        }

        #endregion

        #region Properties

        public int Count => BitCount;

        #endregion

        #region Get/Set Bits

        /// <summary>
        /// Gets the bit value at <paramref name="index"/> offset.
        /// </summary>
        public bool GetBit(int index)
        {
            if ((index < 0) && (index >= BitCount)) { throw new ArgumentOutOfRangeException(nameof(index)); }

            // 
            return ((_value >> index) & 0x1) == 0x1;
        }

        /// <summary>
        /// Sets the bit value at <paramref name="index"/> offset.
        /// </summary>
        public void SetBit(int index, bool bit)
        {
            if ((index < 0) && (index >= BitCount)) { throw new ArgumentOutOfRangeException(nameof(index)); }

            // 
            var mask = (byte) (1 << index); // pow(2, nth)
            if (bit) { _value = (byte) (_value | mask); }
            else { _value = (byte) (_value & ~mask); }
        }

        #endregion

        #region IEnumerable<bool>

        public IEnumerator<bool> GetEnumerator()
        {
            return Enumerate(this).GetEnumerator();

            static IEnumerable<bool> Enumerate(BitField field)
            {
                for (var i = 0; i < BitCount; i++)
                {
                    yield return field.GetBit(i);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Conversion Operators

        public static implicit operator byte(BitField field)
        {
            return field._value;
        }

        public static implicit operator BitField(byte value)
        {
            return new BitField(value);
        }

        #endregion

        #region Equality

        public override bool Equals(object obj)
        {
            return obj is BitField field && Equals(field);
        }

        public bool Equals(BitField other)
        {
            return _value == other._value;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_value);
        }

        public static bool operator ==(BitField left, BitField right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(BitField left, BitField right)
        {
            return !(left == right);
        }

        #endregion
    }
}
