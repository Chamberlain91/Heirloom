﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heirloom.EGL
{
    [Flags]
    internal enum EglRenderableType
    {
        EGL_OPENGL_ES_BIT = 0x0001,
        EGL_OPENVG_BIT = 0x0002,
        EGL_OPENGL_ES2_BIT = 0x0004,
        EGL_OPENGL_BIT = 0x0008,
    }
}
