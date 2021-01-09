namespace Heirloom.Drawing.OpenGLES
{
    internal enum FramebufferBlitMask : uint
    {
        Color = ClearMask.Color,
        Depth = ClearMask.Depth,
        Stencil = ClearMask.Stencil,
    }
}
