using System;
using System.Runtime.CompilerServices;

using Heirloom.Math;

namespace Heirloom.Drawing.OpenGLES
{
    internal static class ResourceManager
    {
        private static readonly Rectangle _surfaceUVRect = new Rectangle(0, 0, 1, -1);

        private static readonly ConditionalWeakTable<Texture, Framebuffer> _framebuffers;

        static ResourceManager()
        {
            _framebuffers = new ConditionalWeakTable<Texture, Framebuffer>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static (Texture, Rectangle) GetTextureInfo(OpenGLRenderContext context, ImageSource source)
        {
            // If an image
            if (source is Image image)
            {
                // Get root image
                var root = image.Root;
                var texture = GetTexture(context, root);

                // Is the root image out of date?
                if (root.Version != texture.Version)
                {
                    // Update texture by root image
                    context.Invoke(() => texture.Update(root));
                }

                // 
                return (texture, image.UVRect);
            }
            // If a surface
            else if (source is Surface surface)
            {
                var texture = GetTexture(context, surface);

                // Is the root image out of date?
                if (surface.Version != texture.Version)
                {
                    // Surface was newer than texture knew about, update mip maps
                    // todo: configurable/avoid when not really needed?
                    // todo: maybe texture stores ImageSource so it can update version numbers internally
                    context.Invoke(() => texture.GenerateMips(surface.Version));
                }

                // Framebuffer, texture already exists
                return (texture, _surfaceUVRect);
            }
            // 
            else
            {
                throw new ArgumentException($"Image source wasn't valid to acquire a texture.", nameof(source));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Framebuffer GetFramebuffer(OpenGLRenderContext context, Texture texture)
        {
            // This framebuffer is not configured, need to initialize
            if (_framebuffers.TryGetValue(texture, out var framebuffer) == false)
            {
                // Generate and bind framebuffer
                framebuffer = new Framebuffer(context, texture);

                // Store newly created framebuffer
                _framebuffers.Add(texture, framebuffer);
            }

            return framebuffer;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Texture GetTexture(OpenGLRenderContext context, ImageSource source)
        {
            return GetTextureFromResource(context, source, source.Size);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Texture GetTextureFromResource(OpenGLRenderContext context, IDrawingResource resource, IntSize size)
        {
            if (!(resource.NativeObject is Texture texture))
            {
                // Generate and bind framebuffer
                texture = new Texture(context, size);

                // Store newly created framebuffer
                resource.NativeObject = texture;
            }

            return texture;
        }
    }
}
