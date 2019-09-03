using System;
using System.Collections.Generic;
using System.Text;

namespace Heirloom.Input
{
    public abstract class Touch
    {
        /// <summary>
        /// A hollow implementation that always reports zero, disconnected, etc.
        /// </summary>
        public static Touch Null { get; } = new DummyTouch();

        private sealed class DummyTouch : Touch { }
    }
}
