using System;

namespace Heirloom
{
    /// <summary>
    /// A utility class to help drive sprite based animation.
    /// </summary>
    /// <seealso cref="Sprite"/>
    /// <seealso cref="SpriteAnimation"/>
    /// <category>Drawing</category>
    public sealed class SpritePlayer
    {
        private float _frameTime;
        private int _frameNumber;
        private AnimationDirection _direction;

        /// <summary>
        /// Construcs a new <see cref="SpritePlayer"/> with the specified <see cref="Heirloom.Sprite"/>.
        /// </summary>
        /// <param name="sprite">Some sprite.</param>
        /// <param name="initialAnimation">The starting / default animation.</param>
        public SpritePlayer(Sprite sprite, string initialAnimation)
        {
            Sprite = sprite ?? throw new ArgumentNullException(nameof(sprite));

            // Begin the default anim
            Play(initialAnimation);
        }

        /// <summary>
        /// Gets the <see cref="Heirloom.Sprite"/> the player is reponsible for.
        /// </summary>
        public Sprite Sprite { get; }

        /// <summary>
        /// Gets the active animation.
        /// </summary>
        public SpriteAnimation Animation { get; private set; }

        /// <summary>
        /// Gets a value that determines if the player is performing playback of <see cref="Animation"/>.
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
                if (value >= 0 && value < Animation.Length)
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
        public Image Image => Animation[FrameNumber].Image;

        /// <summary>
        /// Begins playback of the specified animation.
        /// </summary>
        /// <param name="animation">The name of some animation.</param>
        /// <exception cref="ArgumentException">An animation with the specified name does not exist.</exception>
        public void Play(string animation)
        {
            if (Sprite.ContainsAnimation(animation))
            {
                Animation = Sprite.GetAnimation(animation);

                // Reset animation progress
                _direction = Animation.Direction;
                if (_direction == AnimationDirection.PingPong) { _direction = AnimationDirection.Forward; }
                FrameNumber = 0;
                _frameTime = 0;

                Play();
            }
            else
            {
                throw new ArgumentException("The specified animation does not exist.");
            }
        }

        /// <summary>
        /// Resumes playback of the active animation.
        /// </summary>
        public void Play()
        {
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
        /// Updates the player, advancing animation by elapsed time.
        /// </summary>
        /// <param name="dt">The difference in time in seconds since last update.</param>
        public void Update(float dt)
        {
            if (IsPlaying)
            {
                _frameTime += dt;

                while (_frameTime > Animation[FrameNumber].Delay)
                {
                    // Remove frame length from elapsed time
                    _frameTime -= Animation[FrameNumber].Delay;

                    // If animating forward
                    if (_direction == AnimationDirection.Forward)
                    {
                        // Advance a frame
                        var nextFrame = FrameNumber + 1;
                        if (nextFrame >= Animation.Length)
                        {
                            // If a ping pong style animation, flip direction
                            if (Animation.Direction == AnimationDirection.PingPong)
                            {
                                _direction = AnimationDirection.Reverse;
                                nextFrame = Animation.Length - 1;
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
                            if (Animation.Direction == AnimationDirection.PingPong)
                            {
                                _direction = AnimationDirection.Forward;
                                nextFrame = 0;
                            }
                            else
                            {
                                nextFrame = Animation.Length - 1;
                            }
                        }

                        FrameNumber = nextFrame;
                    }
                }
            }
        }
    }
}
