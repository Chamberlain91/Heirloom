using Heirloom;
using Heirloom;

namespace Heirloom
{
    /// <summary>
    /// Distortion shader.
    /// </summary>
    public sealed class DistortionShader : Shader
    {
        private Image _distortionMap;
        private float _strength;
        private Vector _offset;

        /// <summary>
        /// Constructs a new distortion shader.
        /// </summary>
        public DistortionShader(Image distortionMap)
            : base("embedded/shaders/distort.frag")
        {
            DistortionMap = distortionMap;
            Strength = 0.05F; // 5% distortion
            Offset = Vector.Zero;
        }

        /// <summary>
        /// Gets or sets the distortion map. Only the RG channels are used and are remapped to -1 to +1.
        /// </summary>
        public Image DistortionMap
        {
            get => _distortionMap;

            set
            {
                _distortionMap = value ?? throw new System.ArgumentNullException(nameof(value));
                SetUniform("uNoiseImage", value);
            }
        }

        /// <summary>
        /// Gets or sets the offset applied to the distortion map (in uv coordinates).
        /// </summary>
        public Vector Offset
        {
            get => _offset;

            set
            {
                _offset = value;
                SetUniform("uOffset", value);
            }
        }

        /// <summary>
        /// Gets or sets the strength of the distortion (0.0 to 1.0, unclamped).
        /// </summary>
        public float Strength
        {
            get => _strength;

            set
            {
                _strength = value;
                SetUniform("uStrength", value);
            }
        }
    }
}
