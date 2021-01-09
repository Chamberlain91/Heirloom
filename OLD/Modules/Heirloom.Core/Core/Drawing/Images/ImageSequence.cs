using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Heirloom
{
    /// <summary>
    /// Represents an image based animation.
    /// </summary>
    /// <seealso cref="Animator"/>
    /// <category>Drawing</category>
    public sealed class ImageSequence : IEnumerable<Image>
    {
        private const int InvalidDuration = -1;

        private readonly List<Frame> _frames;
        private float _duration = 0;

        #region Constructors

        /// <summary>
        /// Constructs a new image sequence.
        /// </summary>
        /// <param name="direction">The direction behaviour of the animation.</param>
        public ImageSequence(AnimationDirection direction = AnimationDirection.Forward)
        {
            Direction = direction;

            _frames = new List<Frame>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the number of frames in the sequence.
        /// </summary>
        public int Length => _frames.Count;

        /// <summary>
        /// Gets the duration of the animation in seconds.
        /// </summary>
        public float Duration
        {
            get
            {
                // If duration is negative, we need to recompute the duration
                if ((int) _duration == InvalidDuration) { _duration = _frames.Sum(f => f.Delay); }
                return _duration;
            }
        }

        /// <summary>
        /// Gets or sets the intended animation direction.
        /// </summary>
        public AnimationDirection Direction { get; set; }

        #endregion

        #region Add & Remove Frames

        /// <summary>
        /// Removes all frames from this sequence.
        /// </summary>
        public void Clear()
        {
            _duration = InvalidDuration;
            _frames.Clear();
        }

        /// <summary>
        /// Adds a new frame to the end of the animation.
        /// </summary>
        /// <param name="image">Some image.</param>
        /// <param name="delay">The frame delay in seconds.</param> 
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="image"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="delay"/> is negative.</exception>
        public void Add(Image image, float delay)
        {
            // Insert at the end
            Insert(Length, image, delay);
        }

        /// <summary>
        /// Inserts a new frame into the sequence before the specified frame number.
        /// </summary>
        /// <param name="index">The frame number to insert before.</param>
        /// <param name="image">Some image.</param>
        /// <param name="delay">The frame delay in seconds.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="index"/> is out of frame range.</exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="image"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="delay"/> is negative.</exception>
        public void Insert(int index, Image image, float delay)
        {
            if (index < 0 || index > Length) { throw new ArgumentOutOfRangeException("Index must be between 0 and animation length."); }
            if (image is null) { throw new ArgumentNullException(nameof(image)); }
            if (delay < 0) { throw new ArgumentException("Delay must a non-negative number in seconds."); }

            var frame = new Frame(image, delay);
            _frames.Insert(index, frame);
            _duration = InvalidDuration;
        }

        /// <summary>
        /// Removes a frame at the specified frame number.
        /// </summary>
        /// <param name="index">The frame number.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="index"/> is out of frame range.</exception>
        public void Remove(int index)
        {
            if (index < 0 || index >= Length)
            {
                throw new ArgumentOutOfRangeException("Index must be between 0 and animation length.");
            }

            _frames.RemoveAt(index);
        }

        #endregion

        #region Get or Set Frame Info

        /// <summary>
        /// Gets the delay for a frame of this sequence.
        /// </summary>
        /// <param name="index">The frame number.</param>
        /// <returns>The frame delay, in seconds.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="index"/> is out of frame range.</exception>
        public float GetDelay(int index)
        {
            if (index < 0 || index >= Length) { throw new ArgumentOutOfRangeException("Index must be between 0 and animation length."); }
            return _frames[index].Delay;
        }

        /// <summary>
        /// Gets the image for a frame of this sequence.
        /// </summary>
        /// <param name="index">The frame number.</param>
        /// <returns>The frame image.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="index"/> is out of frame range.</exception>
        public Image GetImage(int index)
        {
            if (index < 0 || index >= Length) { throw new ArgumentOutOfRangeException("Index must be between 0 and animation length."); }
            return _frames[index].Image;
        }

        /// <summary>
        /// Sets the delay for a frame of this sequence.
        /// </summary>
        /// <param name="index">The frame number.</param>
        /// <param name="delay">The frame delay, in seconds.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="index"/> is out of frame range.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="delay"/> is negative.</exception>
        public void SetDelay(int index, float delay)
        {
            if (index < 0 || index >= Length) { throw new ArgumentOutOfRangeException("Index must be between 0 and animation length."); }
            if (delay < 0) { throw new ArgumentException("Delay must a non-negative number in seconds."); }

            _frames[index].Delay = delay;
        }

        /// <summary>
        /// Sets the image for a frame of this sequence.
        /// </summary>
        /// <param name="index">The frame number.</param>
        /// <param name="image">The frame image.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="index"/> is out of frame range.</exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="image"/> is null.</exception>
        public void SetImage(int index, Image image)
        {
            if (index < 0 || index >= Length) { throw new ArgumentOutOfRangeException("Index must be between 0 and animation length."); }
            if (image is null) { throw new ArgumentNullException(nameof(image)); }

            _frames[index].Image = image;
        }

        #endregion

        private sealed class Frame
        {
            public Frame(Image image, float delay)
            {
                Image = image ?? throw new ArgumentNullException(nameof(image));
                Delay = delay;
            }

            public Image Image;

            public float Delay;
        }

        /// <inheritdoc/>
        public IEnumerator<Image> GetEnumerator()
        {
            foreach (var frame in _frames)
            {
                yield return frame.Image;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
