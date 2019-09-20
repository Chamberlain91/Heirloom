using System;
using System.Runtime.CompilerServices;

using Heirloom.Math;

namespace Heirloom.Drawing.OpenGLES
{
    internal static class ResourceManager
    {
        private static readonly ConditionalWeakTable<GLTexture, GLFramebuffer> _framebuffers;
        private static readonly ConditionalWeakTable<Image, GLTexture> _textures;

        static ResourceManager()
        {
            _framebuffers = new ConditionalWeakTable<GLTexture, GLFramebuffer>();
            _textures = new ConditionalWeakTable<Image, GLTexture>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static (GLTexture, Rectangle) GetTextureInfo(OpenGLRenderContext context, ImageSource input)
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
                    texture = context.Invoke(() => new GLTexture(root.Size));

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
            // If a framebuffer surface
            else if (input is GLFramebufferSurface surface)
            {
                // Is the root image out of date?
                if (surface.Version != surface.Texture.Version)
                {
                    // Surface was newer than texture knew about, update mip maps
                    // todo: configurable/avoid when not really needed?
                    context.Invoke(() => surface.Texture.GenerateMips(surface.Version));
                }

                // Framebuffer, texture already exists
                return (surface.Texture, (0, 0, 1, -1));
            }
            // 
            else
            {
                throw new ArgumentException($"Image source wasn't valid to acquire a texture.", nameof(input));
            }
        }

        internal static GLFramebuffer GetFramebuffer(OpenGLRenderContext context, GLTexture texture)
        {
            // This framebuffer is not configured, need to initialize
            if (_framebuffers.TryGetValue(texture, out var framebuffer) == false)
            {
                // Generate and bind framebuffer
                framebuffer = new GLFramebuffer(context, texture);

                // Store newly created framebuffer
                _framebuffers.Add(texture, framebuffer);
            }

            return framebuffer;
        }
    }
}
