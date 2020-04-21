namespace Heirloom.OpenGLES
{
    /// <summary>
    /// Enum for <see cref="GL.GetString(StringParameter)"/>
    /// </summary>
    internal enum StringParameter
    {
        /// <summary>
        /// Vendor name
        /// </summary>
        Vendor = 0x1F00,

        /// <summary>
        /// Renderer name
        /// </summary>
        Renderer = 0x1F01,

        /// <summary>
        /// Version string
        /// </summary>
        Version = 0x1F02,

        /// <summary>
        /// GLSL Version string
        /// </summary>
        ShadingLanguageVersion = 0x8B8C,

        /// <summary>
        /// String containing all extensions.
        /// </summary>
        Extensions = 0x1F03
    }
}
