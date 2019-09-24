using System;
using System.Runtime.CompilerServices;

using Heirloom.Math;

namespace Heirloom.Drawing.OpenGLES
{
    internal static class ResourceManager
    {
        private static readonly Rectangle _surfaceUVRect = new Rectangle(0, 0, 1, -1);

        private static readonly ConditionalWeakTable<Surface, Framebuffer> _framebuffers;

        static ResourceManager()
        {
            _framebuffers = new ConditionalWeakTable<Surface, Framebuffer>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static (Texture, Rectangle) GetTextureInfo(OpenGLRenderContext ctx, ImageSource source)
        {
            // If an image
            if (source is Image image)
            {
                // Get root image
                var rootImage = image.Root;
                var texture = GetTexture(ctx, rootImage);

                // Is the root image out of date?
                if (rootImage.Version != texture.Version)
                {
                    // Update texture (image data and mips)
                    texture.Update(ctx, rootImage);
                }

                // Return texture information
                return (texture, image.UVRect);
            }
            // If a surface
            else if (source is Surface surface)
            {
                // Get the associated framebuffer
                var framebuffer = GetFramebuffer(ctx, surface);

                // Is the framebuffer out of date?
                if (framebuffer.Version != surface.Version)
                {
                    // Update texture (msaa blit and mips)
                    framebuffer.Update(ctx);
                }

                // Return texture information
                return (framebuffer.TextureBuffer.Texture, _surfaceUVRect);
            }
            // 
            else
            {
                throw new ArgumentException($"Image source wasn't valid to acquire a texture.", nameof(source));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Framebuffer GetFramebuffer(OpenGLRenderContext ctx, Surface surface)
        {
            var resource = surface as IDrawingResource;

            // Try to get framebuffer
            if (!(resource.NativeObject is Framebuffer framebuffer))
            {
                framebuffer = new Framebuffer(ctx, surface);
                resource.NativeObject = framebuffer;
            }

            return framebuffer;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Texture GetTexture(OpenGLRenderContext context, Image image)
        {
            var resource = image as IDrawingResource;

            // Try to get the native texture
            if (!(resource.NativeObject is Texture texture))
            {
                texture = new Texture(context, image.Size);
                resource.NativeObject = texture;
            }

            return texture;
        }
    }
}
