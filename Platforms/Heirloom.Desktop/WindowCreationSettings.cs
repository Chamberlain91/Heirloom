using Heirloom.Drawing;
using Heirloom.Math;

namespace Heirloom.Desktop
{
    public struct WindowCreationSettings
    {
        public IntSize? Size;
        public bool? IsResizable;

        public MultisampleQuality? Multisample;
        public bool? UseTransparentFramebuffer;
        public bool? VSync;

        public static readonly WindowCreationSettings Default = new WindowCreationSettings
        {
            Size = (1280, 720),
            IsResizable = true,

            Multisample = MultisampleQuality.None,
            UseTransparentFramebuffer = false,
            VSync = true,
        };

        internal static void FillDefaults(ref WindowCreationSettings settings)
        {
            settings.Size = settings.Size ?? Default.Size;
            settings.IsResizable = settings.IsResizable ?? Default.IsResizable;

            settings.Multisample = settings.Multisample ?? Default.Multisample;
            settings.UseTransparentFramebuffer = settings.UseTransparentFramebuffer ?? Default.UseTransparentFramebuffer;
            settings.VSync = settings.VSync ?? Default.VSync;
        }
    }
}
