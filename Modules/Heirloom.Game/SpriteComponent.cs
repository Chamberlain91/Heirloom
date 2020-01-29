using System;
using System.Runtime.CompilerServices;

using Heirloom.Drawing;

namespace Heirloom.Game
{
    /// <summary>
    /// Provides rendering and animating a sprite to the attached entity.
    /// </summary>
    public sealed class SpriteComponent : DrawableComponent
    {
        private bool _isAnimateForward = true;
        private bool _isPlaying = false;
        private float _time;

        private Sprite _sprite;

        #region Constructors

        public SpriteComponent(Sprite sprite)
        {
            Sprite = sprite ?? throw new ArgumentNullException(nameof(sprite));
            Animation = Sprite.DefaultAnimation;
        }

        #endregion

        #region Properties

        public Sprite Sprite
        {
            get => _sprite;

            set
            {
                _sprite = value ?? throw new ArgumentNullException(nameof(value));
                Animation = Sprite.DefaultAnimation;
            }
        }

        public Sprite.Animation Animation { get; private set; }

        internal Sprite.FrameInfo FrameInfo => Sprite.Frames[Frame];

        public int Frame { get; private set; }

        #endregion

        /// <summary>
        /// Jumps to a specified frame relative to the current anmation.
        /// </summary>
        public void GotoFrame(int frame)
        {
            if (frame < 0 || frame >= Animation.Length)
            {
                const string message = "Unable to goto desired frame, number must be greater or equal to zero and less than the current animation length.";
                throw new ArgumentOutOfRangeException(nameof(frame), message);
            }

            // 
            Frame = Animation.From + frame;
        }

        #region Animation Playback

        /// <summary>
        /// Begins playback of an animation by name.
        /// </summary>
        public void Play(string name)
        {
            if (name is null) { throw new ArgumentNullException(nameof(name)); }

            // Set the new animation
            Animation = Sprite.GetAnimation(name);

            // Reset and play
            Reset();
            Play();
        }

        /// <summary>
        /// Begins playback of an animation by name.
        /// </summary>
        public void SetAnimation(string name)
        {
            if (name is null) { throw new ArgumentNullException(nameof(name)); }

            // Set the new animation
            Animation = Sprite.GetAnimation(name);
            Reset();
        }

        /// <summary>
        /// Enables playback of the current animation.
        /// </summary>
        public void Play()
        {
            // Causes the animation to update
            _isPlaying = true;
        }

        /// <summary>
        /// Disables playback of the current animation, effectively pausing it.
        /// </summary>
        public void Stop()
        {
            // Causes the animation to not update
            _isPlaying = false;
        }

        /// <summary>
        /// Resets the current animation state to the beginning.
        /// </summary>
        public void Reset()
        {
            // Note: Both PingPong and Forward start in the forward state
            _isAnimateForward = Animation.Direction != Sprite.Direction.Reverse;
            Frame = Animation.From;
            _time = 0;
        }

        #endregion

        protected internal override void Update(float dt)
        {
            if (_isPlaying)
            {
                // Accumulate time
                _time += dt;

                // If the accumulated time exceeds the current frame delay
                while (_time >= FrameInfo.Delay)
                {
                    // Remove frame delay from accumulated time
                    _time -= FrameInfo.Delay;

                    // Advance frame number in the animation direction
                    AdvanceAnimation();
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void AdvanceAnimation()
        {
            if (_isAnimateForward)
            {
                // Advance frame to the 'forward edge'
                Frame++;

                // If beyond the 'forward edge' of the animation, wrap around
                if (Frame >= Animation.To)
                {
                    Frame = Animation.From;
                    CheckAndPingPongAnimation();
                }
            }
            else
            {
                // Advance frame to the 'reverse edge'
                Frame--;

                // If beyond the 'reverse edge' of the animation, wrap around
                if (Frame < Animation.From)
                {
                    Frame = Animation.To;
                    CheckAndPingPongAnimation();
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void CheckAndPingPongAnimation()
        {
            // If a ping pong animation, toggle forward boolean
            if (Animation.Direction == Sprite.Direction.PingPong)
            {
                _isAnimateForward = !_isAnimateForward;
            }
        }

        protected override void Draw(Graphics ctx)
        {
            ctx.DrawSprite(Sprite, Frame, Transform.Matrix);
        }
    }
}
