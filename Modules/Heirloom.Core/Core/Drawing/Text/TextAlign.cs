using System;

namespace Heirloom
{
    /// <summary>
    /// Controls how text is aligned to the layout rectangle.
    /// </summary>
    [Flags]
    public enum TextAlign
    {
        /// <summary>
        /// Text is horizontally aligned to the left edge of the layout box.
        /// </summary>
        Left = 0b00_00,

        /// <summary>
        /// Text is horizontally aligned to the center of the layout box.
        /// </summary>
        Center = 0b00_01,

        /// <summary>
        /// Text is horizontally aligned to the right edge of the layout box.
        /// </summary>
        Right = 0b00_10,

        /// <summary>
        /// Text is vertically aligned to the top edge of the layout box.
        /// </summary>
        Top = 0b00_00,

        /// <summary>
        /// Text is vertically aligned to the middle of the layout box.
        /// </summary>
        Middle = 0b01_00,

        /// <summary>
        /// Text is vertically aligned to the bottom edge of the layout box.
        /// </summary>
        Bottom = 0b10_00
    }
}
