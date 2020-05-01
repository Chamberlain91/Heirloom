using System;

using Heirloom;

namespace Heirloom
{
    /// <summary>
    /// Vector blur shader.
    /// </summary>
    public sealed class VectorBlurShader : Shader
    {
        private Vector _vector;

        /// <summary>
        /// Constructs a new blur shader.
        /// </summary>
        public VectorBlurShader(int quality)
            : base("embedded/shaders/blur.frag", ("KERNEL_SIZE", 5 + (quality * 2)))
        {
            if (quality < 0 || quality > 5)
            {
                throw new InvalidOperationException("Blur shader quality ranges from 0 to 5 inclusive.");
            }
        }

        /// <summary>
        /// Gets or sets the blur vector. Strength of the blur is determined by the magnitude of the vector.
        /// </summary>
        public Vector Vector
        {
            get => _vector;

            set
            {
                _vector = value;
                SetUniform("uVector", value);
            }
        }
    }
}
