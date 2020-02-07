﻿using System;
using System.Runtime.CompilerServices;

using Heirloom.Math;

namespace Heirloom.Drawing.OpenGLES
{
    internal static class ResourceManager
    // todo: See about merging w/ OpenGLGraphicsAdapter
    {
        private static readonly Rectangle _surfaceUVRect = new Rectangle(0, 0, 1, -1);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static (Texture, Rectangle) GetTextureInfo(OpenGLGraphics gfx, ImageSource source)
        {
            // If an image
            if (source is Image image)
            {
                // Get texture for root image
                var texture = GetTexture(gfx, image.Root);

                // Return texture information
                return (texture, image.UVRect);
            }
            // If a surface
            else if (source is Surface surface)
            {
                // Get the associated framebuffer
                var framebuffer = GetFramebuffer(gfx, surface);

                // Return texture information
                return (framebuffer.Texture, _surfaceUVRect);
            }
            // 
            else
            {
                throw new ArgumentException($"Image source wasn't valid to acquire a texture.", nameof(source));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Framebuffer GetFramebuffer(OpenGLGraphics gfx, Surface surface)
        {
            var resource = surface as IDrawingResource;

            // Try to get framebuffer
            if (!(resource.NativeObject is Framebuffer framebuffer))
            {
                framebuffer = new Framebuffer(gfx, surface);
                resource.NativeObject = framebuffer;
            }

            // Is the framebuffer out of date?
            if (framebuffer.Version != surface.Version)
            {
                // Update texture (msaa blit and mips)
                framebuffer.Update(gfx);
            }

            return framebuffer;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Texture GetTexture(OpenGLGraphics gfx, Image image)
        {
            image = image.Root; // 

            var resource = image as IDrawingResource;

            // Try to get the native texture
            if (!(resource.NativeObject is Texture texture))
            {
                texture = new Texture(gfx, image.Size);
                resource.NativeObject = texture;
            }

            // Is the root image out of date?
            if (image.Version != texture.Version)
            {
                // Update texture (image data and mips)
                gfx.Invoke(() => texture.UpdateByImage(image));
            }

            return texture;
        }
    }
}
