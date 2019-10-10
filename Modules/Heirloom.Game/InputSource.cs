using Heirloom.Math;

namespace Heirloom.Game
{
    public abstract class InputSource
    {
        protected internal abstract void Poll();

        protected internal abstract bool TryGetPointerPosition(out Vector state);
        protected internal abstract bool TryGetButton(string identifier, out ButtonState state);
        protected internal abstract bool TryGetAxis(string identifier, out float state);
    }
}
