using System;
using System.Collections;
using System.Collections.Generic;

using Heirloom.Mathematics;

namespace Heirloom.Drawing
{
    /// <summary>
    /// Represents a multi-stop gradient.
    /// </summary>
    public sealed class Gradient : IEnumerable<GradientStop>
    {
        private readonly SortedList<float, Color> _stops;

        /// <summary>
        /// Constructs a new instance of <see cref="Gradient"/>.
        /// </summary>
        /// <param name="mode"></param>
        public Gradient(GradientMode mode = GradientMode.CIELab)
        {
            _stops = new SortedList<float, Color>();
            Mode = mode;
        }

        /// <summary>
        /// Gets or sets the interpolation mode.
        /// </summary>
        public GradientMode Mode { get; set; }

        /// <summary>
        /// Gets the number of color stops.
        /// </summary>
        public int Count => _stops.Count;

        /// <summary>
        /// Removes all color stops from this gradient.
        /// </summary>
        public void Clear()
        {
            _stops.Clear();
        }

        /// <summary>
        /// Adds a color stop.
        /// </summary>
        /// <param name="time">The normalized time along the gradient.</param>
        /// <param name="color">The color to add.</param>
        public void Add(float time, Color color)
        {
            _stops.Add(time, color);
        }

        /// <summary>
        /// Evaluates the color at the desired normalized time.
        /// </summary>
        /// <param name="time">The normalized time along the gradient.</param>
        /// <returns>The interpolated color.</returns>
        public Color Evaluate(float time)
        {
            if (_stops.Count == 0)
            {
                // No stops, return transparent (or exception?!)
                return Color.TransparentBlack;
            }
            else if (_stops.Count == 1)
            {
                // Only one color, constant color
                return _stops.Values[0];
            }
            else
            {
                // todo: binary search for hi/lo pair (if it would improve performance)

                var lo = _stops.Keys[0]; // left most
                var hi = _stops.Keys[_stops.Count - 1]; // right most

                // Beyond domain (constant color)
                if (time < lo) { return _stops[lo]; }
                else if (time > hi) { return _stops[hi]; }
                // Within domain
                else
                {
                    // Find upper and lower keys
                    for (var i = 1; i < _stops.Count; i++)
                    {
                        var key = _stops.Keys[i];

                        if (time < key)
                        {
                            // Key was above target time
                            hi = key;
                            break;
                        }

                        // Key was less than target time
                        lo = key;
                    }

                    // Compute blending value between keys
                    var t = Calc.Between(time, lo, hi);

                    var cLo = _stops[lo];
                    var cHi = _stops[hi];

                    // Interpolate colors
                    return Mode switch
                    {
                        GradientMode.RGB => Color.Lerp(cLo, cHi, t),
                        GradientMode.CIELab => (Color) ColorLab.Lerp((ColorLab) cLo, (ColorLab) cHi, t),
                        _ => throw new InvalidOperationException("Invalid gradient mode."),
                    };
                }
            }
        }

        /// <inheritdoc/>
        public IEnumerator<GradientStop> GetEnumerator()
        {
            foreach (var (time, color) in _stops)
            {
                yield return new GradientStop(time, color);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
