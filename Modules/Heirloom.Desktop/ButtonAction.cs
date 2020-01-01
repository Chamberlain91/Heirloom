namespace Heirloom.Desktop
{
    public enum ButtonAction
    {
        /// <summary>
        /// Button or Key was released.
        /// </summary>
        Release = 0,

        /// <summary>
        /// Button or Key was pressed.
        /// </summary>
        Press = 1,

        /// <summary>
        /// Key was repeated (ie, that behaviour when key is held down).
        /// </summary>
        Repeat = 2
    }
}
