using System;
using System.Collections.Generic;
using System.Linq;

using Heirloom;
using Heirloom.Core.Drawing.Images;
using Heirloom.IO;

namespace Examples.GameInput
{
    internal sealed class Player
    {
        public readonly SpritePlayer SpritePlayer;

        public Vector Position;
        public Vector Velocity;

        private readonly Queue<Emote> _emoteQueue;
        private float _emoteTimer;
        private Image _emoteImage;

        private bool _onGround = false;
        private bool _flip = false;

        public Player()
        {
            SpritePlayer = new SpritePlayer(LoadSprite(), "idle");
            _emoteQueue = new Queue<Emote>();
        }

        public void Update(float dt, bool allowInput)
        {
            // Update physics
            Position += Velocity * dt;
            Velocity += (0, 10);

            // Update sprite player
            SpritePlayer.Update(dt);

            // Hit "floor"
            if (Position.Y > 0)
            {
                _onGround = true;

                if (Velocity.X == 0F && SpritePlayer.Animation.Name != "idle")
                {
                    SpritePlayer.Play("idle");
                }

                Position.Y = 0;
                Velocity.Y = 0;
            }

            // 
            if (Position.X < -400) { Position.X = -400; }
            if (Position.X > +400) { Position.X = +400; }

            if (allowInput)
            {
                if (_onGround && Input.CheckKey(Key.Space, ButtonState.Down))
                {
                    Velocity -= (0, 500);
                    SpritePlayer.Play("jump");
                    _onGround = false;
                }

                var pressLeft = Input.CheckKey(Key.A, ButtonState.Down);
                var pressRight = Input.CheckKey(Key.D, ButtonState.Down);
                if (pressLeft || pressRight)
                {
                    // If on the ground, begin walking
                    if (_onGround && SpritePlayer.Animation.Name != "walk")
                    {
                        SpritePlayer.Play("walk");
                    }

                    if (pressLeft)
                    {
                        Velocity.X -= 10;
                        _flip = true;
                    }
                    else
                    {
                        Velocity.X += 10;
                        _flip = false;
                    }
                }
                else if (_onGround)
                {
                    // Stop
                    SpritePlayer.Play("idle");
                    Velocity.X = 0F;
                }
            }

            if (Calc.Abs(Velocity.X) > 200)
            {
                Velocity.X = 200 * Calc.Sign(Velocity.X);
            }

            // Emote logic 
            ProcessEmotes(dt);
        }

        private void ProcessEmotes(float dt)
        {
            // Compute the duration of emotes. The more emotes in the queue
            // the shorter the duration, with a min of half second.
            var emoteDuration = 1F + 3F / (_emoteQueue.Count + 1);

            // Accumulate time
            _emoteTimer += dt;

            // When enough time has passed
            if (_emoteTimer > emoteDuration)
            {
                _emoteTimer -= emoteDuration;

                // If we have more emotes scheduled...
                if (_emoteQueue.Count > 0)
                {
                    // Set next emote 
                    var emote = _emoteQueue.Dequeue();
                    _emoteImage = emote.Image;
                }
                // No more emotes...
                else
                {
                    // No emote to show
                    _emoteImage = null;
                }
            }
        }

        public void SubmitEmote(Emote emote)
        {
            if (_emoteImage == null)
            {
                // Show image
                _emoteImage = emote.Image;
                _emoteTimer = 0;
            }
            else
            {
                // Schedule for next appearance
                _emoteQueue.Enqueue(emote);
            }
        }

        public void Draw(GraphicsContext gfx, float dt)
        {
            // Draw current frame
            gfx.Color = Color.White;
            var matrix = Matrix.CreateTransform(Position, 0, (_flip ? -1 : 1, 1));
            gfx.DrawImage(SpritePlayer.Image, matrix);

            // Draw emote
            if (_emoteImage != null)
            {
                gfx.DrawImage(_emoteImage, Position - (0, SpritePlayer.Image.Height + 2));
            }
        }

        private Sprite LoadSprite()
        {
            // 
            var spritesheet = new Image("assets/p1_spritesheet.png");

            // Extract sprites from sheet
            var submap = new Dictionary<string, Image>();
            var data = Files.ReadText("assets/p1_spritesheet.txt");
            foreach (var line in data.Split('\n'))
            {
                var idx = line.IndexOf('=');
                if (idx < 0) { throw new InvalidOperationException("spritesheet metadata invalid."); }

                // Parse frame data
                var name = line.Substring(0, idx).Trim();
                var values = line.Substring(idx + 1).Trim().Split(' ').Select(s => int.Parse(s)).ToArray();
                var rect = new IntRectangle(values[0], values[1], values[2], values[3]);

                // Extract image from spritesheet
                var image = Extract(spritesheet, rect);
                submap[name] = image;

                // Set image origin to bottom center
                image.Origin = (image.Width / 2, image.Height);
            }

            const float FrameDelay = 1 / 10F;

            var sprite = new Sprite();

            sprite.AddAnimation(new SpriteAnimation("idle") {
                { submap["p1_front"], FrameDelay }
            });

            sprite.AddAnimation(new SpriteAnimation("jump") {
                { submap["p1_jump"], FrameDelay }
            });

            sprite.AddAnimation(new SpriteAnimation("walk") {
                { submap["p1_walk01"], FrameDelay },
                { submap["p1_walk02"], FrameDelay },
                { submap["p1_walk03"], FrameDelay },
                { submap["p1_walk04"], FrameDelay },
                { submap["p1_walk05"], FrameDelay },
                { submap["p1_walk06"], FrameDelay },
                { submap["p1_walk07"], FrameDelay }
            });

            return sprite;

            static Image Extract(Image image, IntRectangle region)
            {
                var copy = new Image(region.Size);
                image.CopyTo(region, copy, IntVector.Zero);
                return copy;
            }
        }
    }
}
