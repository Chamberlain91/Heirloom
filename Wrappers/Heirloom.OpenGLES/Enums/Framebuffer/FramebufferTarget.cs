using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heirloom.OpenGLES
{
    public enum FramebufferTarget : uint
    {
        Framebuffer = 0x8D40,
        DrawFramebuffer = 0x8CA9,
        ReadFramebuffer = 0x8CA8
    }
}
