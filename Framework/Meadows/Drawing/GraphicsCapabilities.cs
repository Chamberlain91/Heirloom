namespace Meadows.Drawing
{
    public struct GraphicsCapabilities
    {
        public MultisampleQuality MaxSupportedMultisample;
        public int MaxTextureSize;

        public override string ToString()
        {
            return $"Max Supported Multisample: {MaxSupportedMultisample}\n"
                 + $"Max Texture Size: {MaxTextureSize}";
        }
    }
}
