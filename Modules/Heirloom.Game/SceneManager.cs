using System.Collections.Generic;

using Heirloom.Drawing;

namespace Heirloom.Game
{
    public sealed class SceneManager
    {
        private readonly List<Scene> _activeScenes;

        internal SceneManager()
        {
            _activeScenes = new List<Scene>();
        }

        public IReadOnlyList<Scene> ActiveScenes => _activeScenes;

        public void Add(Scene scene)
        {
            _activeScenes.Add(scene);
        }

        public void Remove(Scene scene)
        {
            _activeScenes.Remove(scene);
        }

        internal void Update(RenderContext ctx, float dt)
        {
            // Process active scenes
            foreach (var scene in _activeScenes)
            {
                scene.Update(ctx, dt);
            }
        }
    }
}
