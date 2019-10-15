using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

using Heirloom.Math;
using Heirloom.OpenGLES;

namespace Heirloom.Drawing.OpenGLES
{
    internal class VertexAttribute
    {
        #region Constructors

        public VertexAttribute(VertexAttributeName name, int size, VertexAttributeType type, bool normalized, uint offset = 0)
        {
            Name = name;
            Size = size;
            Type = type;

            Normalized = normalized;

            Index = GetAttributeIndex(name) + offset;
        }

        #endregion

        #region Properties

        public VertexAttributeName Name { get; }

        public VertexAttributeType Type { get; }

        public int Size { get; }

        public bool Normalized { get; }

        public uint Index { get; }

        #endregion

        internal void SetAttributePointer(int offset, int stride, int divisor)
        {
            GL.EnableVertexAttribArray(Index);
            GL.SetVertexAttribPointer(Index, Size, Type, Normalized, stride, (uint) offset);
            GL.SetVertexAttribDivisor(Index, divisor);
        }

        #region Static Attribute Helpers

        private static readonly IReadOnlyDictionary<VertexAttributeName, uint> _attributeIndices;

        static VertexAttribute()
        {
            // Construct attribute index map
            // TODO: Automate better with enum attributes or something, so it isn't hard coded in here?
            var offset = 0u;
            var attributeIndices = new Dictionary<VertexAttributeName, uint>();
            foreach (VertexAttributeName name in Enum.GetValues(typeof(VertexAttributeName)))
            {
                attributeIndices[name] = offset;

                // Move 
                if (name == VertexAttributeName.Transform) { offset += 2; }
                else { offset += 1; }
            }

            // 
            _attributeIndices = attributeIndices;
        }

        public static VertexAttributeName[] GetAttributes()
        {
            return (VertexAttributeName[]) Enum.GetValues(typeof(VertexAttributeName));
        }

        public static uint GetAttributeIndex(VertexAttributeName name)
        {
            return _attributeIndices[name];
        }

        internal static int GetSizeInBytes(VertexAttributeType attrType, int count)
        {
            switch (attrType)
            {
                case VertexAttributeType.Byte:
                case VertexAttributeType.UnsignedByte:
                    return 1 * count;

                case VertexAttributeType.Short:
                case VertexAttributeType.UnsignedShort:
                case VertexAttributeType.HalfFloat:
                    return 2 * count;

                case VertexAttributeType.Int:
                case VertexAttributeType.UnsignedInt:
                case VertexAttributeType.Float:
                case VertexAttributeType.Fixed:
                    return 4 * count;
            }

            throw new InvalidOperationException($"Unknown vertex attribute type: {attrType}");
        }

        internal static VertexAttribute[] GenerateAttributes(Type type)
        {
            // Get all fields w/ attribute defs
            var fields = type.GetFields()
                .Where(x => x.GetCustomAttribute<VertexAttributeAttribute>() != null)
                .ToArray();

            // Must have at least one attribute defined
            if (fields.Length == 0)
            {
                throw new InvalidOperationException("Must have at least one field with a vertex attribute defined.");
            }

            // Console.WriteLine($"Attribute Layout: {type}");

            // Create attribute list (one attributed per field)
            var attributes = new List<VertexAttribute>();

            // For each field
            for (var f = 0; f < fields.Length; f++)
            {
                var attribute = fields[f].GetCustomAttribute<VertexAttributeAttribute>();
                var field = fields[f];

                // Number of attributes needed to describe this data, most attributes will be one.
                var attributeCount = GetAttributeCount(field.FieldType);

                // Get offset and usable size
                var nextOffset = f != (fields.Length - 1) ? OffsetOf(fields[f + 1]) : Marshal.SizeOf(type);
                var currOffset = OffsetOf(field);
                var size = nextOffset - currOffset;

                // Gather respective field type
                var attr = GetAttributeType(field.FieldType);
                var step = GetSizeInBytes(attr, 1) * attributeCount;
                var count = size / step;
                var waste = size % step;
                if (waste != 0) { throw new Exception($"Vertex attribute defined in struct has unattributed space after '{attr}'."); }

                // Store extracted attribute data
                for (var o = 0u; o < attributeCount; o++)
                {
                    var _attribute = new VertexAttribute(attribute.Attribute, count, attr, attribute.Normalize, o);
                    // Console.WriteLine($"  {attribute.Attribute} ({_attribute.Index}): {count} x {attr}");
                    attributes.Add(_attribute);
                }
            }

            return attributes.ToArray();

            int OffsetOf(FieldInfo field)
            {
                return (int) Marshal.OffsetOf(field.DeclaringType, field.Name);
            }
        }

        private static VertexAttributeType GetAttributeType(Type type)
        {
            if (type == typeof(float) ||
                type == typeof(Color) ||
                type == typeof(Vector) ||
                type == typeof(Matrix) ||
                type == typeof(Rectangle) ||
                type == typeof(Size))
            {
                return VertexAttributeType.Float;
            }
            else
            if (type == typeof(int) ||
                type == typeof(IntVector) ||
                type == typeof(IntRectangle) ||
                type == typeof(IntSize))
            {
                return VertexAttributeType.Int;
            }
            //else
            //if (type == typeof(Half))
            //{
            //    return VertexAttributeType.HalfFloat;
            //}
            else
            if (type == typeof(uint))
            {
                return VertexAttributeType.UnsignedInt;
            }
            else
            if (type == typeof(short))
            {
                return VertexAttributeType.Short;
            }
            else
            if (type == typeof(ushort))
            {
                return VertexAttributeType.UnsignedShort;
            }
            else
            if (type == typeof(sbyte))
            {
                return VertexAttributeType.Byte;
            }
            else
            if (type == typeof(byte) || type == typeof(ColorBytes))
            {
                return VertexAttributeType.UnsignedByte;
            }

            throw new InvalidOperationException($"Unknown GLSL vertex attribute type associated with '{type}'");
        }

        private static int GetAttributeCount(Type type)
        {
            if (type == typeof(Matrix)) { return 2; }
            return 1;
        }

        #endregion
    }
}
