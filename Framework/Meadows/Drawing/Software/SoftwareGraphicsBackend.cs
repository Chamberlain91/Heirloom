using System;

using Meadows.Hardware;
using Meadows.Mathematics;

namespace Meadows.Drawing.Software
{
    public sealed class SoftwareGraphicsBackend : GraphicsBackend
    {
        public SoftwareGraphicsBackend()
        {
            InitializeBackend();
        }

        internal override bool SupportsCustomShaders => false;

        public GraphicsContext CreateContext(int width, int height)
        {
            return CreateContext(new IntSize(width, height));
        }

        public GraphicsContext CreateContext(IntSize size)
        {
            return new SoftwareGraphicsContext(this, size);
        }

        internal override Uniform[] CompileShader(Shader shader)
        {
            throw new NotImplementedException("Compiling shaders not supported on the software backend.");
        }

        protected internal override object GenerateNativeObject(GraphicsResource resource)
        {
            switch (resource)
            {
                case Surface surface:
                    return new SoftwareSurface(surface);

                case Image image:
                    return new SoftwareImage(image);

                default:
                    throw new InvalidOperationException($"Unable to generate native reresentation of {resource}");
            }
        }

        protected override GpuInfo GetGpuInfo()
        {
            return GpuInfo.Unknown;
        }

        protected override GraphicsCapabilities GetGraphicsCapabilities()
        {
            return new GraphicsCapabilities
            {
                MaxSupportedMultisample = 0
            };
        }

        public override void Dispose()
        {
            // Nothing to do, software backend is all managed code.
        }
    }
}
