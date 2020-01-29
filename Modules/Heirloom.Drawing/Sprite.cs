using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Heirloom.Drawing.Extras;
using Heirloom.IO;

namespace Heirloom.Drawing
{
    /// <summary>
    /// A representation of single animated sprite.
    /// May also contains per-frame and animation sequence information for animating the sprite.
    /// </summary>
    public sealed class Sprite
    {
        private readonly Dictionary<string, Animation> _animations;
        private readonly FrameInfo[] _frames;

        #region Constructors

        internal Sprite(SpriteBuilder builder)
        {
            // Create frame array (we clone to prevent unexpected modifications from the builder)
            _frames = builder.Frames.Select(f => new FrameInfo(f.Image, f.Delay)).ToArray();

            // Create animation map (we clone to prevent unexpected modifications from the builder)
            var animations = new Dictionary<string, Animation>();
            foreach (var anim in builder.Animations.Values)
            {
                animations[anim.Name] = new Animation(anim.Name, anim.From, anim.To, anim.Direction);
            }

            _animations = animations;

            // Select default animation
            DefaultAnimation = _animations.FirstOrDefault().Value;
        }

        /// <summary>
        /// Constructs a new sprite from the specified file path resolved by <see cref="Files.OpenStream(string)"/>.
        /// </summary>
        /// <param name="path"></param>
        public Sprite(string path)
            : this(Files.OpenStream(path))
        { }

        /// <summary>
        /// Constructs a new sprite from a stream (ie, an Aseprite file or other supported format).
        /// </summary>
        /// <param name="stream">Some stream to a known sprite data format.</param>
        public Sprite(Stream stream)
        {
            ConstructFromStream(stream, out _frames, out _animations);

            // Select default animation
            DefaultAnimation = _animations.FirstOrDefault().Value;
        }

        /// <summary>
        /// Constructs a new sprite from a single image.
        /// </summary>
        /// <param name="image">Some image.</param>
        public Sprite(Image image)
        {
            if (image is null) { throw new ArgumentNullException(nameof(image)); }

            // 
            _frames = new[] { new FrameInfo(image, 1F) };
            _animations = new Dictionary<string, Animation> {
                { "default", new Animation("default", 0, 1, Direction.Forward) }
            };

            // Select default animation
            DefaultAnimation = _animations.FirstOrDefault().Value;
        }

        #endregion

        #region Stream Constructor Helpers

        private void ConstructFromStream(Stream stream, out FrameInfo[] frames, out Dictionary<string, Animation> animations)
        {
            // todo: magically determine asset type
            ConstructFromAseprite(stream, out frames, out animations);
        }

        private void ConstructFromAseprite(Stream stream, out FrameInfo[] frames, out Dictionary<string, Animation> animations)
        {
            using var ase = new AsepriteFile(stream);

            var an = new Dictionary<string, Animation>();
            var fr = new List<FrameInfo>();

            // For each known frame
            for (var i = 0; i < ase.Frames.Length; i++)
            {
                var aseFrame = ase.Frames[i];
                fr.Add(new FrameInfo(aseFrame.Image, aseFrame.Duration));
            }

            // For each named animation
            foreach (var tag in ase.Tags)
            {
                an[tag.Name] = new Animation(tag.Name, tag.From, tag.To, tag.Direction);
            }

            // 
            animations = an;
            frames = fr.ToArray();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a read-only list of frames contained by this sprite.
        /// </summary>
        public IReadOnlyList<FrameInfo> Frames => _frames;

        /// <summary>
        /// Gets the name of each known animation sequence.
        /// </summary>
        public IReadOnlyCollection<string> Animations => _animations.Keys;

        /// <summary>
        /// Gets the default animation.
        /// </summary>
        public Animation DefaultAnimation { get; }

        #endregion

        /// <summary>
        /// Gets an animation sequence by name.
        /// </summary>
        public Animation GetAnimation(string name)
        {
            if (_animations.TryGetValue(name, out var animation))
            {
                return animation;
            }

            throw new KeyNotFoundException($"Unable to find animation named \"{name}\" in sprite.");
        }

        public class FrameInfo
        {
            internal FrameInfo(Image image, float delay)
            {
                Image = image ?? throw new ArgumentNullException(nameof(image));
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
            /// Gets this frame's frame number.
            /// </summary>
            public int Index { get; }
        }

        public class Animation
        {
            internal Animation(string name, int from, int to, Direction direction)
            {
                Name = name ?? throw new ArgumentNullException(nameof(name));
                From = from;
                To = to;
                Length = to - from;
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
            /// The length of the animation in frames.
            /// </summary>
            public int Length { get; }

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
}
