namespace Heirloom
{
    /// <summary>
    /// Grayscale shader.
    /// </summary>
    /// <category>Shaders and Effects</category>
    public sealed class GrayscaleShader : Shader
    {
        private float _blend;

        /// <summary>
        /// Constructs a new grayscale shader.
        /// </summary>
        public GrayscaleShader()
            : base("embedded/shaders/grayscale.frag")
        {
            Blend = 1F;
        }

        /// <summary>
        /// Gets or sets the blending factor (0.0 to 1.0).
        /// </summary>
        public float Blend
        {
            get => _blend;

            set
            {
                _blend = value;
                SetUniform("uBlend", value);
            }
        }
    }
}
