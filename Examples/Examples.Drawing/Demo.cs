using Heirloom;

namespace Examples.Drawing
{
    public abstract class Demo
    {
        public string Name { get; }

        public float Delta { get; private set; }

        public float Time { get; private set; }

        protected Demo(string name)
        {
            Name = name;
        }

        internal virtual void Update(float dt)
        {
            Delta = dt;
            Time += dt;
        }

        internal virtual void Draw(GraphicsContext ctx, Rectangle contentBounds) { }
    }
}
