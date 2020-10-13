using System;

using Meadows.Drawing;
using Meadows.Input;
using Meadows.Mathematics;

namespace Meadows
{
    public sealed class SoftwareGraphcsContext : GraphicsContext
    {
        public SoftwareGraphcsContext(int width, int height)
            : base(new SoftwareScreen((width, height)))
        {
            // Associate this context to the virtual screen 
            Screen.SetGraphicsContext(this);
        }

        private new SoftwareScreen Screen => base.Screen as SoftwareScreen;

        public override void SetCamera(Vector center, float scale = 1, float rotation = 0)
        {
            throw new NotImplementedException();
        }

        public override void Clear(Color color)
        {
            var surface = GetNativeResource<SoftwareSurface>(Surface);
            foreach (var co in Rasterizer.Rectangle(Viewport))
            {
                surface.ColorBuffer.Set(co, color);
            }
        }

        public override void Draw(Texture texture, in Rectangle uvRegion, in Mesh mesh, in Matrix matrix)
        {
            // throw new NotImplementedException();
        }

        public override void ClearStencil()
        {
            throw new NotImplementedException();
        }

        public override void BeginStencil()
        {
            throw new NotImplementedException();
        }

        public override void EndStencil()
        {
            throw new NotImplementedException();
        }

        public override Image GrabPixels(IntRectangle region)
        {
            // todo: validate region will indeed fit in region
            var surface = GetNativeResource<SoftwareSurface>(Surface);
            return surface.ColorBuffer.GrabPixels(region);
        }

        protected override void SwapBuffers()
        {
            throw new NotImplementedException();
        }

        protected override void Flush(bool blockCompletion = false)
        {
            throw new NotImplementedException();
        }

        protected override object GenerateNativeResource(NativeResource resource)
        {
            switch (resource)
            {
                case Surface surface:
                    return new SoftwareSurface(surface);

                default:
                    throw new InvalidOperationException($"Unable to generate native reresentation of {resource}");
            }
        }

        #region Software Implementation Resources

        private sealed class SoftwareScreen : Screen
        {
            private SoftwareGraphcsContext _graphicsContext;

            public SoftwareScreen(IntSize size)
                : base(size, MultisampleQuality.None)
            {
                // todo: potentially dummy "null" input devices?
            }

            public void SetGraphicsContext(SoftwareGraphcsContext graphics)
            {
                _graphicsContext = graphics;
            }

            public override GraphicsContext Graphics => _graphicsContext;

            public override KeyboardDevice Keyboard { get; }

            public override MouseDevice Mouse { get; }

            public override GamepadDevice Gamepad { get; }

            public override TouchDevice Touch { get; }
        }

        private sealed class SoftwareSurface
        {
            public readonly Surface RenderTexture;

            public readonly IColorBuffer ColorBuffer;

            public readonly StencilBuffer StencilBuffer;

            public SoftwareSurface(Surface surface)
            {
                RenderTexture = surface;
                StencilBuffer = new StencilBuffer(surface.Size);
                ColorBuffer = CreateColorBuffer(surface);
            }

            private static IColorBuffer CreateColorBuffer(Surface texture)
            {
                IColorBuffer buffer;
                if (texture.Format == SurfaceFormat.UnsignedByte)
                {
                    buffer = new UByteColorBuffer(texture.Width, texture.Height);
                }
                else
                {
                    buffer = new FloatColorBuffer(texture.Width, texture.Height);
                }

                return buffer;
            }
        }

        #region Surface Buffers

        public interface IColorBuffer
        {
            Color Get(IntVector co);

            void Set(IntVector co, Color color);

            Image GrabPixels(IntRectangle region);
        }

        private sealed class UByteColorBuffer : IColorBuffer
        {
            public Image Image;

            public UByteColorBuffer(int width, int height)
            {
                Image = new Image(width, height);
            }

            public Color Get(IntVector co)
            {
                return Image.GetPixel(co);
            }

            public void Set(IntVector co, Color color)
            {
                Image.SetPixel(co, color);
            }

            public Image GrabPixels(IntRectangle region)
            {
                var output = new Image(region.Size);
                Image.CopyTo(region, output, IntVector.Zero);
                return output;
            }
        }

        private sealed class FloatColorBuffer : IColorBuffer
        {
            public Color[,] Colors;

            public FloatColorBuffer(int width, int height)
            {
                Colors = new Color[height, width];
            }

            public Color Get(IntVector co)
            {
                return Colors[co.Y, co.X];
            }

            public void Set(IntVector co, Color color)
            {
                Colors[co.Y, co.X] = color;
            }

            public Image GrabPixels(IntRectangle region)
            {
                var output = new Image(region.Size);
                foreach (var co in Rasterizer.Rectangle(region))
                {
                    output.SetPixel(co - region.Position, Colors[co.Y, co.X]);
                }
                return output;
            }
        }

        private sealed class StencilBuffer
        {
            private readonly byte[,] _byte;

            public StencilBuffer(IntSize size)
            {
                _byte = new byte[size.Height, size.Width];
            }

            public byte GetValue(IntVector co)
            {
                return _byte[co.Y, co.X];
            }

            public void SetValue(IntVector co, byte value)
            {
                _byte[co.Y, co.X] = value;
            }
        }

        #endregion

        #endregion
    }
}
