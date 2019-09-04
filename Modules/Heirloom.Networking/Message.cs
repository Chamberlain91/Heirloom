using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Heirloom.IO.Networking
{
    public sealed class Message
    {
        internal Message(int type, byte[] data)
        {
            Type = type;
            Data = data ?? throw new ArgumentNullException(nameof(data));
        }

        public int Type { get; }

        public byte[] Data { get; }

        public BinaryReader GetBinaryReader()
        {
            var ms = new MemoryStream(Data);
            return new BinaryReader(ms, Encoding.UTF8, false);
        }

        public T GetStruct<T>()
        {
            return GetStruct<T>(Data);
        }

        public static byte[] GetBytes(Action<BinaryWriter> writeAction)
        {
            using (var memStream = new MemoryStream())
            using (var memWriter = new BinaryWriter(memStream, Encoding.UTF8))
            {
                // Write message
                writeAction(memWriter);

                // Send message to all connections
                return memStream.ToArray();
            }
        }

        public static byte[] GetBytes<T>(T obj)
        {
            // Create byte array size of the object
            var size = Marshal.SizeOf(obj);
            var data = new byte[size];

            // TODO: Can be implemented with fixed(byte* ...) to skip the copy

            // Copy object to bytes
            var ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(obj, ptr, true);
            Marshal.Copy(ptr, data, 0, size);
            Marshal.FreeHGlobal(ptr);

            return data;
        }

        internal static T GetStruct<T>(byte[] data)
        {
            var size = Marshal.SizeOf<T>();

            // TODO: Can be implemented with fixed(byte* ...) to skip the copy

            // Copy bytes to unmanaged bytes
            var ptr = Marshal.AllocHGlobal(size);
            Marshal.Copy(data, 0, ptr, size);

            // Convert unmanaged byes to object
            var obj = Marshal.PtrToStructure<T>(ptr);

            // Free unmanaged bytes
            Marshal.FreeHGlobal(ptr);

            return obj;
        }
    }

}
