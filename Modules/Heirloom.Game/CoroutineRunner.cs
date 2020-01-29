using System.Collections;
using System.Collections.Generic;

namespace Heirloom.Game
{
    internal class CoroutineRunner
    {
        private readonly List<Coroutine> _coroutines;

        public CoroutineRunner()
        {
            _coroutines = new List<Coroutine>();
        }

        public Coroutine Run(float delay, IEnumerator enumerator)
        {
            var coroutine = new Coroutine(enumerator, delay);
            _coroutines.Add(coroutine);
            return coroutine;
        }

        public void Update(float dt)
        {
            // Iterate in reverse order so removals are safe
            for (var i = _coroutines.Count - 1; i >= 0; i--)
            {
                // Update coroutine
                if (!_coroutines[i].Update(dt))
                {
                    // If returning false, the coroutine is terminated
                    _coroutines.RemoveAt(i);
                }
            }
        }
    }
}
