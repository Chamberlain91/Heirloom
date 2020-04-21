namespace Heirloom.OpenGLES
{
    internal sealed class FramebufferStorage
    {
        public readonly Renderbuffer Renderbuffer;

        public readonly Texture Texture;

        public FramebufferStorage(IntSize size, int samples)
        {
            if (samples < 1) { throw new System.ArgumentException("Must be larger than zero.", nameof(samples)); }

            Texture = new Texture(size);

            if (samples > 1)
            {
                Renderbuffer = new Renderbuffer(size, samples);
            }
        }

        public bool HasRenderbuffer => Renderbuffer != null;
    }
}
