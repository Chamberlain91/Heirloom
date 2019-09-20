using System;
using System.Runtime.CompilerServices;

using Heirloom.Math;

namespace Heirloom.Drawing.OpenGLES
{
    internal static class ResourceManager
    {
        private static readonly ConditionalWeakTable<Texture, Framebuffer> _framebuffers;
        private static readonly ConditionalWeakTable<Surface, Texture> _surfaces;
        private static readonly ConditionalWeakTable<Image, Texture> _textures;

        static ResourceManager()
        {
            _framebuffers = new ConditionalWeakTable<Texture, Framebuffer>();
            _surfaces = new ConditionalWeakTable<Surface, Texture>();
            _textures = new ConditionalWeakTable<Image, Texture>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static (Texture, Rectangle) GetTextureInfo(OpenGLRenderContext context, ImageSource input)
        {
            // If an image
            if (input is Image image)
            {
                // Get root image
                var root = image.Root;

                // If the associated texture does not exist,
                if (!_textures.TryGetValue(root, out var texture))
                {
                    // We need to create the texture
                    texture = context.Invoke(() => new Texture(context, root.Size));

                    // Store the texture by root image for next time
                    _textures.Add(root, texture);
                }

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
            else if (input is Surface surface)
            {
                var texture = GetSurfaceTexture(context, surface);

                // Is the root image out of date?
                if (surface.Version != texture.Version)
                {
                    // Surface was newer than texture knew about, update mip maps
                    // todo: configurable/avoid when not really needed?
                    // todo: maybe texture stores ImageSource so it can update version numbers internally
                    context.Invoke(() => texture.GenerateMips(surface.Version));
                }

                // Framebuffer, texture already exists
                return (texture, (0, 0, 1, -1));
            }
            // 
            else
            {
                throw new ArgumentException($"Image source wasn't valid to acquire a texture.", nameof(input));
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
        internal static Texture GetSurfaceTexture(OpenGLRenderContext context, Surface surface)
        {
            // This framebuffer is not configured, need to initialize
            if (_surfaces.TryGetValue(surface, out var texture) == false)
            {
                // Generate and bind framebuffer
                texture = new Texture(context, surface.Size);

                // Store newly created framebuffer
                _surfaces.Add(surface, texture);
            }

            return texture;
        }
    }
}
