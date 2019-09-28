using Heirloom.Drawing;

namespace Examples.Gridcannon.Engine
{
    public abstract class Component
    {
        public Entity Entity { get; }

        public Transform Transform => Entity.Transform;

        protected Component(Entity entity)
        {
            Entity = entity;
            // todo: add to entity list
        }

        protected internal abstract void Update(float dt);
    }

    public abstract class DrawableComponent : Component
    {
        protected DrawableComponent(Entity entity)
            : base(entity)
        { }

        protected internal abstract void Draw(RenderContext ctx);
    }

    public sealed class SpriteComponent : DrawableComponent
    {
        private float _time;
        private int _currentFrame;
        private bool _forward = true;

        public SpriteComponent(Entity entity)
            : base(entity)
        { }

        public Sprite Sprite { get; set; }

        public Sprite.Animation Animation { get; private set; }

        public void SetAnimation(string name)
        {
            Animation = Sprite.GetAnimation(name);
            _currentFrame = Animation.From;
            _time = 0;
        }

        protected internal override void Update(float dt)
        {
            // Get the current frame
            var frame = Sprite.Frames[_currentFrame];

            // 
            _time += dt;

            // If exceeding the delay
            while (_time > frame.Delay)
            {
                // Remove frame delay from accumulated time
                _time -= frame.Delay;

                if (_forward) { _currentFrame++; }
                else { _currentFrame--; }

                // 
                if (_currentFrame > Animation.To)
                {
                    _currentFrame = 0;
                    if (Animation.Direction == Sprite.Direction.PingPong) { _forward = !_forward; }
                }

                if (_currentFrame < Animation.From)
                {
                    _currentFrame = Animation.To;
                    if (Animation.Direction == Sprite.Direction.PingPong) { _forward = !_forward; }
                }

                // Get the next frame
                frame = Sprite.Frames[_currentFrame];
            }
        }

        protected internal override void Draw(RenderContext ctx)
        {
            ctx.DrawSprite(Sprite, 0, Transform.Matrix);
        }
    }

    public sealed class ImageComponent : DrawableComponent
    {
        public ImageComponent(Entity entity)
            : base(entity)
        { }

        public Image Image { get; set; }

        protected internal override void Update(float dt)
        {
            // Nothing to do
        }

        protected internal override void Draw(RenderContext ctx)
        {
            ctx.DrawImage(Image, Transform.Matrix);
        }
    }
}
