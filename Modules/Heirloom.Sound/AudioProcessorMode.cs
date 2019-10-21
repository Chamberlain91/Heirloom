using System;

namespace Heirloom.Sound
{
    [Flags]
    public enum AudioProcessorMode
    {
        /// <summary>
        /// Configures the audio processor for audio playback.
        /// </summary>
        Playback,

        /// <summary>
        /// Configures the audio processor for audio capture.
        /// </summary>
        Capture,

        /// <summary>
        /// Configures the audio processor for audio capture and playback.
        /// </summary>
        Duplex = Playback | Capture
    };
}
