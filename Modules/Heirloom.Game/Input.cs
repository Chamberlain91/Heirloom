using System;
using System.Collections.Generic;

using Heirloom.Math;

namespace Heirloom.Game
{
    public static class Input
    {
        private static readonly List<InputSource> _sources;

        static Input()
        {
            _sources = new List<InputSource>();
        }

        internal static void Update()
        {
            // Poll each source
            foreach (var source in _sources)
            {
                source.Poll();
            }
        }

        #region Input Sources

        public static void AddInputSource(InputSource inputSource)
        {
            _sources.Add(inputSource);
        }

        public static bool ContainsInputSource(InputSource inputSource)
        {
            return _sources.Contains(inputSource);
        }

        public static void RemoveInputSource(InputSource inputSource)
        {
            _sources.Remove(inputSource);
        }

        #endregion

        /// <summary>
        /// Gets the position of a pointer (ie, mouse, etc).
        /// </summary>
        public static Vector GetPointer()
        {
            // Try each source for pointer input
            foreach (var source in _sources)
            {
                if (source.TryGetPointer(out var state))
                {
                    return state;
                }
            }

            return default;
        }

        /// <summary>
        /// Gets the state of a button (ie, keyboard, etc).
        /// </summary>
        public static ButtonState GetButton(string identifier)
        {
            if (string.IsNullOrWhiteSpace(identifier))
            { throw new ArgumentException("Button identifier must not be null, blank or only whitespace.", nameof(identifier)); }

            // Try each source for button input
            foreach (var source in _sources)
            {
                if (source.TryGetButton(identifier, out var state))
                {
                    return state;
                }
            }

            return default;
        }

        /// <summary>
        /// Gets the state of an axis (ie, horizontal thumbstick or trigger).
        /// </summary>
        public static float GetAxis(string identifier)
        {
            if (string.IsNullOrWhiteSpace(identifier))
            { throw new ArgumentException("Axis identifier must not be null, blank or only whitespace.", nameof(identifier)); }

            // Try each source for axis input
            foreach (var source in _sources)
            {
                if (source.TryGetAxis(identifier, out var state))
                {
                    return state;
                }
            }

            return default;
        }

        /// <summary>
        /// Gets the state of two axii (ie, a thumbstick).
        /// </summary>
        public static Vector GetVector(string xIdentifier, string yIdentifier)
        {
            var x = GetAxis(xIdentifier);
            var y = GetAxis(yIdentifier);
            return new Vector(x, y);
        }

        /// <summary>
        /// Gets the state of two axii (ie, a thumbstick).
        /// </summary>
        public static Vector GetVector(string partialIdentifier)
        {
            var x = GetAxis($"{partialIdentifier}_x");
            var y = GetAxis($"{partialIdentifier}_y");
            return new Vector(x, y);
        }
    }
}
