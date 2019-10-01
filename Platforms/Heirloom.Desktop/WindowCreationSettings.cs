using Heirloom.Drawing;

namespace Heirloom.Desktop
{
    public struct WindowCreationSettings
    {
        public int? Width;
        public int? Height;

        public MultisampleQuality? Multisample;
        public bool? UseTransparentFramebuffer;
        public bool? EnableVSync;

        public static readonly WindowCreationSettings Default = new WindowCreationSettings
        {
            Width = 1280,
            Height = 720,
            Multisample = MultisampleQuality.None,
            UseTransparentFramebuffer = false,
            EnableVSync = true,
        };

        internal static void FillDefaults(ref WindowCreationSettings settings)
        {
            settings.Width = settings.Width ?? Default.Width;
            settings.Height = settings.Height ?? Default.Height;
            settings.Multisample = settings.Multisample ?? Default.Multisample;
            settings.UseTransparentFramebuffer = settings.UseTransparentFramebuffer ?? Default.UseTransparentFramebuffer;
            settings.EnableVSync = settings.EnableVSync ?? Default.EnableVSync;
        }
    }
}
