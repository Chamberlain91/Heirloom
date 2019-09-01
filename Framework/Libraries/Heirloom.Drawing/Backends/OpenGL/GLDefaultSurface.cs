﻿using Heirloom.Math;
using Heirloom.OpenGLES;

namespace Heirloom.Drawing.Backends.OpenGL
{
    internal sealed class GLDefaultSurface : GLSurface
    {
        internal GLDefaultSurface()
            : base(new IntSize(1, 1))
        { }

        protected internal override void Prepare(OpenGLRenderContext context)
        {
            GL.BindFramebuffer(FramebufferTarget.DrawFramebuffer, 0);
        }

        protected override void Dispose(bool managed)
        {
            // Nothing to dispose here
        }

        internal void SetSize(IntSize size)
        {
            Size = size;
        }
    }
}
