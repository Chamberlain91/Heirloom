using System;

namespace Meadows.Drawing.OpenGLES
{
    [AttributeUsage(AttributeTargets.Field)]
    internal sealed class ESVertexAttributeAttribute : Attribute
    {
        public ESVertexAttributeName Attribute;

        public bool Normalize;

        public ESVertexAttributeAttribute(ESVertexAttributeName attribute, bool normalize = false)
        {
            Attribute = attribute;
            Normalize = normalize;
        }
    }
}
