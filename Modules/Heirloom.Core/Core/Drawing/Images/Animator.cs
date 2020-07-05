using System;

namespace Heirloom
{
    /// <summary>
    /// A utility class to help drive image based animation.
    /// </summary>
    /// <seealso cref="Sprite"/>
    /// <seealso cref="ImageSequence"/>
    /// <category>Drawing</category>
    public sealed class Animator
    {
        private AnimationDirection _direction;
        private float _frameTime;
        private int _frameNumber;

        /// <summary>
        /// Gets the active animation.
        /// </summary>
        public ImageSequence Current { get; private set; }

        /// <summary>
        /// Gets a value that determines if the player is performing playback of <see cref="Current"/>.
        /// </summary>
        public bool IsPlaying { get; private set; }

        /// <summary>
        /// Gets which frame number the player is currently at.
        /// </summary>
        public int FrameNumber
        {
            get => _frameNumber;

            set
            {
                if (value >= 0 && value < Current.Length)
                {
                    _frameNumber = value;
                    _frameTime = 0;
                }
                else
                {
                    throw new IndexOutOfRangeException($"Unable to seek animation to desired frame, out of range.");
                }
            }
        }

        /// <summary>
        /// Gets the image for the current frame of the active animation.
        /// </summary>
        public Image Image => Current.GetImage(FrameNumber);

        /// <summary>
        /// Begins playback of the specified animation.
        /// </summary>
        /// <param name="animation">The name of some animation.</param>
        /// <exception cref="ArgumentException">An animation with the specified name does not exist.</exception>
        public void Play(ImageSequence animation)
        {
            Current = animation ?? throw new ArgumentNullException(nameof(animation));

            // Reset animation progress
            _direction = Current.Direction;
            if (_direction == AnimationDirection.PingPong) { _direction = AnimationDirection.Forward; }
            FrameNumber = 0;
            _frameTime = 0;

            Play();
        }

        /// <summary>
        /// Resumes playback of the active animation.
        /// </summary>
        public void Play()
        {
            if (Current == null)
            {
                throw new InvalidOperationException("Unable to result playback, no active animation.");
            }

            IsPlaying = true;
        }

        /// <summary>
        /// Stops playback of the active animation.
        /// </summary>
        public void Stop()
        {
            IsPlaying = false;
        }

        /// <summary>
        /// Updates the player, advancing the animation by elapsed time.
        /// </summary>
        /// <param name="dt">The difference in time in seconds since last update.</param>
        public void Update(float dt)
        {
            if (IsPlaying)
            {
                _frameTime += dt;

                while (_frameTime > Current.GetDelay(FrameNumber))
                {
                    // Remove frame length from elapsed time
                    _frameTime -= Current.GetDelay(FrameNumber);

                    // If animating forward
                    if (_direction == AnimationDirection.Forward)
                    {
                        // Advance a frame
                        var nextFrame = FrameNumber + 1;
                        if (nextFrame >= Current.Length)
                        {
                            // If a ping pong style animation, flip direction
                            if (Current.Direction == AnimationDirection.PingPong)
                            {
                                _direction = AnimationDirection.Reverse;
                                nextFrame = Current.Length - 1;
                            }
                            else
                            {
                                nextFrame = 0;
                            }
                        }

                        FrameNumber = nextFrame;
                    }
                    // Animating reverse
                    else
                    {
                        // Advance a frame
                        var nextFrame = FrameNumber - 1;
                        if (nextFrame < 0)
                        {
                            // If a ping pong style animation, flip direction
                            if (Current.Direction == AnimationDirection.PingPong)
                            {
                                _direction = AnimationDirection.Forward;
                                nextFrame = 0;
                            }
                            else
                            {
                                nextFrame = Current.Length - 1;
                            }
                        }

                        FrameNumber = nextFrame;
                    }
                }
            }
        }
    }
}
