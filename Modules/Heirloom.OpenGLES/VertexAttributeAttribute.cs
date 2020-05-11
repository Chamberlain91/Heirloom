using System;

namespace Heirloom.OpenGLES
{
    [AttributeUsage(AttributeTargets.Field)]
    internal sealed class VertexAttributeAttribute : Attribute
    {
        public VertexAttributeName Attribute;

        public bool Normalize;

        public VertexAttributeAttribute(VertexAttributeName attribute)
        {
            Attribute = attribute;
        }
    }
}
