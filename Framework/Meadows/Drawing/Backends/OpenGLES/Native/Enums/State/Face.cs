namespace Meadows.Drawing.OpenGLES
{
    internal enum Face
    {
        /// <summary>
        /// Ignores processing fragments "front-facing"
        /// </summary>
        Front = 0x0404,

        /// <summary>
        /// Ignores processing fragments "back-facing"
        /// </summary>
        Back = 0x0405,

        /// <summary>
        /// Ignores all fragments ( but still renders primitives via non-face draw modes, such as Lines or Points )
        /// </summary>
        FrontAndBack = 0x0408
    }
}
