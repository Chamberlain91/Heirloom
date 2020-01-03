using System;
using System.Collections.Generic;

using Heirloom.Drawing;
using Heirloom.Math;

namespace Heirloom.Desktop
{
    public struct WindowCreationSettings : IEquatable<WindowCreationSettings>
    {
        /// <summary>
        /// Configures the windows initial size.
        /// </summary>
        public IntSize? Size;

        /// <summary>
        /// Enables the window graphics to block calls to <see cref="Graphics.RefreshScreen()"/> to synchronize with vertical blanks (default: true).
        /// </summary>
        public bool? VSync;

        /// <summary>
        /// Configures the desired multisampling of the window framebuffer (default: none).
        /// </summary>
        public MultisampleQuality? Multisample;

        /// <summary>
        /// Configures the window to use transparent framebuffers (default false, if supported).
        /// </summary>
        /// <see cref="Application.SupportsTransparentFramebuffer"/>
        public bool? UseTransparentFramebuffer;

        /// <summary>
        /// Gets the default window creation values.
        /// </summary>
        public static readonly WindowCreationSettings Default = new WindowCreationSettings
        {
            Size = new IntSize(1280, 720),
            Multisample = MultisampleQuality.None,
            UseTransparentFramebuffer = false,
            VSync = true,
        };

        internal static void FillDefaults(ref WindowCreationSettings settings)
        {
            settings.Size ??= Default.Size;
            settings.Multisample ??= Default.Multisample;
            settings.UseTransparentFramebuffer ??= Default.UseTransparentFramebuffer;
            settings.VSync ??= Default.VSync;
        }

        #region Equality

        public override bool Equals(object obj)
        {
            return obj is WindowCreationSettings settings && Equals(settings);
        }

        public bool Equals(WindowCreationSettings other)
        {
            return EqualityComparer<IntSize?>.Default.Equals(Size, other.Size) &&
                   VSync == other.VSync &&
                   Multisample == other.Multisample &&
                   UseTransparentFramebuffer == other.UseTransparentFramebuffer;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(VSync, Multisample, UseTransparentFramebuffer);
        }

        public static bool operator ==(WindowCreationSettings left, WindowCreationSettings right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(WindowCreationSettings left, WindowCreationSettings right)
        {
            return !(left == right);
        }

        #endregion
    }
}
