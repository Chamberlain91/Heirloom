using System;

namespace Heirloom.Drawing.Backends.OpenGL
{
    [AttributeUsage(AttributeTargets.Field)]
    internal class VertexAttributeAttribute : Attribute
    {
        public VertexAttributeName Attribute;

        public bool Normalize;

        public VertexAttributeAttribute(VertexAttributeName attribute)
        {
            Attribute = attribute;
        }
    }
}
