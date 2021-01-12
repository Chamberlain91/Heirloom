using Heirloom.Drawing;
using Heirloom.Sound;
using Heirloom.Utilities;

namespace Heirloom.Examples.FingerPaint
{
    public sealed class FingerPaintApp : GameWrapper
    {
        public FingerPaintApp(GraphicsContext graphics, int frameRate = -1)
            : base(graphics, frameRate)
        {
        }

        protected override void Update(float dt)
        {
            // todo: bug, should not be needed to set default surface without changing it
            Graphics.SetRenderTarget(Graphics.Surface);
            // Graphics.ResetState(); 

            Graphics.Clear(Color.Cyan);
            // todo: bug, doesn't clear if nothing is drawn after it
            Graphics.DrawLine((10, 10), (50, 50));
            Graphics.Screen.Refresh();
        }
    }
}
