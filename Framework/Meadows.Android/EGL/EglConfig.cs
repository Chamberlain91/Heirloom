using System;

namespace Meadows.Android.EGL
{
    public class EglConfig
    {
        internal IntPtr Address { get; set; }

        public EglDisplay Display { get; private set; }

        public EglConfigAttributes Attributes { get; private set; }

        internal EglConfig(EglDisplay display, IntPtr address)
        {
            Address = address;
            Display = display;

            // 
            Attributes = new EglConfigAttributes
            {
                //
                RedBits = GetAttribute(EglConfigAttribute.RedSize),
                BlueBits = GetAttribute(EglConfigAttribute.BlueSize),
                GreenBits = GetAttribute(EglConfigAttribute.GreenSize),
                AlphaBits = GetAttribute(EglConfigAttribute.AlphaSize),
                //
                DepthBits = GetAttribute(EglConfigAttribute.DepthSize),
                StencilBits = GetAttribute(EglConfigAttribute.StencilSize),
                // 
                Samples = GetAttribute(EglConfigAttribute.Samples),
                //
                RenderableType = (EglRenderableType) GetAttribute(EglConfigAttribute.RenderableType),
            };
        }

        // 
        internal int GetAttribute(EglConfigAttribute attribute)
        {
            unsafe
            {
                Egl.GetConfigAttrib(Display, this, attribute, out int attribValue);
                return attribValue;
            }
        }
    }
}
