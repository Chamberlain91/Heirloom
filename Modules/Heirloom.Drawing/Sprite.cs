using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Heirloom.Drawing.Extra;
using Heirloom.Math;

namespace Heirloom.Drawing
{
    /// <summary>
    /// A representation of single animated sprite.
    /// May also contains per-frame and sequence information for animating the sprite.
    /// </summary>
    public sealed class Sprite
    {
        private readonly List<Frame> _frames;
        private readonly Dictionary<string, Sequence> _sequences;

        #region Constructors

        /// <summary>
        /// Constructs a new empty sprite.
        /// </summary>
        public Sprite()
        {
            _sequences = new Dictionary<string, Sequence>();
            _frames = new List<Frame>();
        }

        /// <summary>
        /// Constructs a new sprite from a stream (ie, an Aseprite file or other supported format).
        /// </summary>
        /// <param name="stream">Some stream to a known sprite data format.</param>
        public Sprite(Stream stream) : this()
        {
            ConstructFromStream(stream);
        }

        #endregion

        #region Stream Constructor Helpers

        private void ConstructFromStream(Stream stream)
        {
            // todo: magically determine asset type
            ConstructFromAseprite(stream);
        }

        private void ConstructFromAseprite(Stream stream)
        {
            using (var ase = new AsepriteFile(stream))
            {
                // For each known frame
                for (var i = 0; i < ase.Frames.Length; i++)
                {
                    var aseFrame = ase.Frames[i];
                    var origin = Vector.Zero; // todo: from aseFrame

                    // 
                    AddFrame(aseFrame.Image, aseFrame.Duration, origin);
                }

                // For each named animation
                foreach (var tag in ase.Tags)
                {
                    var count = tag.To - tag.From;
                    AddSequence(tag.Name, tag.From, count, tag.Direction);
                }
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a read-only list of frames contained by this sprite.
        /// </summary>
        public IReadOnlyList<Frame> Frames => _frames;

        /// <summary>
        /// Gets the name of each known animation sequence.
        /// </summary>
        public IReadOnlyCollection<string> Sequences => _sequences.Keys;

        #endregion

        /// <summary>
        /// Removes all frames and sequences.
        /// </summary>
        public void Clear()
        {
            _sequences.Clear();
            _frames.Clear();
        }

        /// <summary>
        /// Appends a new frame to the end of the sprite animation.
        /// </summary>
        /// <param name="image">Some image.</param>
        /// <param name="delay">The delay in seconds before the next frame when animated.</param>
        /// <param name="origin">The origin of the sprite for this frame.</param>
        public void AddFrame(Image image, float delay, Vector origin = default)
        {
            if (image is null) { throw new ArgumentNullException(nameof(image)); }
            if (delay <= 0) { throw new ArgumentOutOfRangeException(nameof(delay), "Must be greater than zero"); }

            // Append frame
            _frames.Add(new Frame(image, delay, origin));
        }

        /// <summary>
        /// Defines a new animation sequence on this sprite.
        /// </summary>
        /// <param name="name">Some name.</param>
        /// <param name="start">The first frame of the animation.</param>
        /// <param name="count">The duration of the animation in frames.</param>
        /// <param name="direction">The intended playback direction of the animation.</param>
        public void AddSequence(string name, int start, int count, Direction direction = Direction.Forward)
        {
            if (string.IsNullOrWhiteSpace(name)) { throw new ArgumentException("message", nameof(name)); }
            if (start < 0) { throw new ArgumentOutOfRangeException(nameof(start), "Must be non-negative"); }
            if (count > 0) { throw new ArgumentOutOfRangeException(nameof(start), "Must be greater than zero."); }

            // Append sequence
            _sequences.Add(name, new Sequence(name, start, start + count, direction));
        }

        /// <summary>
        /// Gets an animation sequence by name.
        /// </summary>
        public Sequence GetSequence(string name)
        {
            if (_sequences.TryGetValue(name, out var sequence))
            {
                return sequence;
            }

            throw new KeyNotFoundException($"Unable to find sequence named \"{name}\" in sprite.");
        }

        public class Frame
        {
            internal Frame(Image image, float delay, Vector origin)
            {
                Image = image ?? throw new ArgumentNullException(nameof(image));
                Origin = origin;
                Delay = delay;
            }

            /// <summary>
            /// The image for this sprite frame.
            /// </summary>
            public Image Image { get; }

            /// <summary>
            /// The delay in seconds to be used when animating the sprite.
            /// </summary>
            public float Delay { get; }

            /// <summary>
            /// The offset used to 'center' the sprite around a non-zero origin.
            /// </summary>
            public Vector Origin { get; }
        }

        public class Sequence
        {
            internal Sequence(string name, int from, int to, Direction direction)
            {
                Name = name ?? throw new ArgumentNullException(nameof(name));
                From = from;
                To = to;
                Direction = direction;
            }

            /// <summary>
            /// The name of the animation sequence.
            /// </summary>
            public string Name { get; }

            /// <summary>
            /// The index of the first frame of the animation.
            /// </summary>
            public int From { get; }

            /// <summary>
            /// The index of the last frame of the animation.
            /// </summary>
            public int To { get; }

            /// <summary>
            /// The intended animation direction.
            /// </summary>
            public Direction Direction { get; }
        }

        /// <summary>
        /// Enumerates animation direction options.
        /// </summary>
        public enum Direction : byte
        {
            /// <summary>
            /// Animation plays from zero to greater.
            /// </summary>
            Forward,

            /// <summary>
            /// Animation plays from greater to zero.
            /// </summary>
            Reverse,

            /// <summary>
            /// Animation bounces between <see cref="Forward"/> and then <see cref="Reverse"/> changing at each end, starting with <see cref="Forward"/>.
            /// </summary>
            PingPong
        }
    }

    public static class SpriteRenderExtensions
    {
        /// <summary>
        /// Draw a sprite to the current surface.
        /// </summary>
        /// <param name="ctx">Some render context.</param>
        /// <param name="sprite">Some sprite.</param>
        /// <param name="index">Some valid frame number within the sprite.</param>
        /// <param name="transform">Some transform to draw the sprite.</param>
        public static void DrawSprite(this RenderContext ctx, Sprite sprite, int index, Matrix transform)
        {
            var frame = sprite.Frames[index];
            transform = transform * Matrix.CreateTranslation(-frame.Origin);
            ctx.DrawImage(frame.Image, transform);
        }
    }
}
