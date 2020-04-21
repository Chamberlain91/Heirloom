namespace Heirloom.OpenGLES
{
    public enum BufferUsage
    {
        /// <summary>
        /// A hint for the GPU that the data will update frequently and perform any optimization accordingly.
        /// </summary>
        Stream = 0x88E0,

        /// <summary>
        /// A hint for the GPU that the data won't likely update and perform any optimization accordingly.
        /// </summary>
        Static = 0x88E4,

        /// <summary>
        /// A hint for the GPU that the data will update perodically and perform any optimization accordingly.
        /// </summary>
        Dynamic = 0x88E8
    }
}
