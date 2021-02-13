using System.Text;
using System.IO;

namespace DragonBones
{
    internal class BinaryDataReader : BinaryReader
    {
        private int i;
        private int readLength;

        internal BinaryDataReader(Stream stream) : base(stream)
        {
        }

        internal BinaryDataReader(Stream stream, Encoding encoding) : base(stream, encoding)
        {
        }

        public virtual void Seek(int offset, SeekOrigin origin = SeekOrigin.Current)
        {
            if (offset == 0)
            {
                return;
            }

            BaseStream.Seek(offset, origin);
        }

        public virtual bool[] ReadBooleans(int offset, int readLength)
        {
            Seek(offset);

            this.readLength = readLength;
            bool[] flagArray = new bool[this.readLength];
            i = 0;
            while (i < this.readLength)
            {
                flagArray[i] = base.ReadBoolean();
                i++;
            }
            return flagArray;
        }

        public virtual byte[] ReadBytes(int offset, int readLength)
        {
            Seek(offset);

            this.readLength = readLength;
            byte[] buffer = new byte[this.readLength];
            i = 0;
            while (i < this.readLength)
            {
                buffer[i] = base.ReadByte();
                i++;
            }
            return buffer;
        }

        public virtual char[] ReadChars(int offset, int readLength)
        {
            Seek(offset);

            this.readLength = readLength;
            char[] chArray = new char[this.readLength];
            i = 0;
            while (i < this.readLength)
            {
                chArray[i] = base.ReadChar();
                i++;
            }
            return chArray;
        }

        public virtual decimal[] ReadDecimals(int offset, int readLength)
        {
            Seek(offset);

            this.readLength = readLength;
            decimal[] numArray = new decimal[this.readLength];
            i = 0;
            while (i < this.readLength)
            {
                numArray[i] = base.ReadDecimal();
                i++;
            }
            return numArray;
        }

        public virtual double[] ReadDoubles(int offset, int readLength)
        {
            Seek(offset);

            this.readLength = readLength;
            double[] numArray = new double[this.readLength];
            i = 0;
            while (i < this.readLength)
            {
                numArray[i] = base.ReadDouble();
                i++;
            }
            return numArray;
        }

        public virtual short[] ReadInt16s(int offset, int readLength)
        {
            Seek(offset);

            this.readLength = readLength;
            short[] numArray = new short[this.readLength];
            i = 0;
            while (i < this.readLength)
            {
                numArray[i] = base.ReadInt16();
                i++;
            }
            return numArray;
        }

        public virtual int[] ReadInt32s(int offset, int readLength)
        {
            Seek(offset);

            this.readLength = readLength;
            int[] numArray = new int[this.readLength];
            i = 0;
            while (i < this.readLength)
            {
                numArray[i] = base.ReadInt32();
                i++;
            }
            return numArray;
        }

        public virtual long[] ReadInt64s(int offset, int readLength)
        {
            Seek(offset);

            this.readLength = readLength;
            long[] numArray = new long[this.readLength];
            i = 0;
            while (i < this.readLength)
            {
                numArray[i] = base.ReadInt64();
                i++;
            }
            return numArray;
        }

        public virtual sbyte[] ReadSBytes(int offset, int readLength)
        {
            Seek(offset);

            this.readLength = readLength;
            sbyte[] numArray = new sbyte[this.readLength];
            i = 0;
            while (i < this.readLength)
            {
                numArray[i] = base.ReadSByte();
                i++;
            }
            return numArray;
        }

        public virtual float[] ReadSingles(int offset, int readLength)
        {
            Seek(offset);

            this.readLength = readLength;
            float[] numArray = new float[this.readLength];
            i = 0;
            while (i < this.readLength)
            {
                numArray[i] = base.ReadSingle();
                i++;
            }
            return numArray;
        }

        public virtual string[] ReadStrings(int offset, int readLength)
        {
            Seek(offset);

            this.readLength = readLength;
            string[] strArray = new string[this.readLength];
            i = 0;
            while (i < this.readLength)
            {
                strArray[i] = base.ReadString();
                i++;
            }
            return strArray;
        }

        public virtual ushort[] ReadUInt16s(int offset, int readLength)
        {
            Seek(offset);

            this.readLength = readLength;
            ushort[] numArray = new ushort[this.readLength];
            i = 0;
            while (i < this.readLength)
            {
                numArray[i] = base.ReadUInt16();
                i++;
            }
            return numArray;
        }

        public virtual uint[] ReadUInt32s(int offset, int readLength)
        {
            Seek(offset);

            this.readLength = readLength;
            uint[] numArray = new uint[this.readLength];
            i = 0;
            while (i < this.readLength)
            {
                numArray[i] = base.ReadUInt32();
                i++;
            }
            return numArray;
        }

        public virtual ulong[] ReadUInt64s(int offset, int readLength)
        {
            Seek(offset);

            this.readLength = readLength;
            ulong[] numArray = new ulong[this.readLength];
            i = 0;
            while (i < this.readLength)
            {
                numArray[i] = base.ReadUInt64();
                i++;
            }
            return numArray;
        }

        private long Length
        {
            get
            {
                return BaseStream.Length;
            }
        }
    }

}
