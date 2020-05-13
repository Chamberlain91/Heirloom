namespace Heirloom
{
    /// <summary>
    /// Invert shader.
    /// </summary>
    /// <category>Shaders and Effects</category>
    public sealed class InvertShader : Shader
    {
        private float _blend;

        /// <summary>
        /// Constructs a new color inversion shader.
        /// </summary>
        public InvertShader()
            : base("embedded/shaders/invert.frag")
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
