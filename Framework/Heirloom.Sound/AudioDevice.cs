using System;

namespace Heirloom.Sound
{
    public abstract class AudioDevice
    {
        protected AudioDevice(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public string Name { get; }
    }
}
