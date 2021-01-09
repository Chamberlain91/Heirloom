using System.Collections.Generic;

using Heirloom;

namespace Examples.Depth
{
    public class Renderer
    {
        public Renderer(MultisampleQuality multisample)
        {
            Multisample = multisample;

            EffectLayers = new List<EffectLayer>()
            {
                // Always have a "do nothing layer" to render anything at all
                new EffectLayer(int.MaxValue, SurfaceEffect.None)
            };
        }

        public MultisampleQuality Multisample { get; }

        public List<EffectLayer> EffectLayers { get; }

        /// <summary>
        /// The background color used when drawing.
        /// </summary>
        public Color BackgroundColor { get; set; } = Color.DarkGray;

        internal void Render(GraphicsContext gfx, float dt, IEnumerable<Entity> entities)
        {
            var screenSize = gfx.Screen.Surface.Size;

            // Sort effect layers 
            EffectLayers.StableSort();

            // Get first effect layer
            var layer = GetEffectLayer(0);
            var layerIndex = 0;

            // Get initial surface
            var surface = GetSurface(screenSize, layer);

            // Clear initial surface with background color 
            gfx.Surface = surface;
            gfx.Clear(BackgroundColor);
            gfx.GlobalTransform *= Matrix.CreateScale(1F / layer.Downscale);

            // Draw entities from back to front (negative to positive)
            foreach (var entity in entities)
            {
                // Update Entity
                entity.Update(dt);

                // Entity is above layer, advance to next layer.
                if (entity.Depth > layer.Depth)
                {
                    // Apply effect to surface
                    gfx.Apply(layer.Effect);

                    // Mark surface as old surface
                    var oldSurface = surface;

                    // Get next layer and surface
                    layer = GetEffectLayer(++layerIndex);
                    surface = GetSurface(screenSize, layer);

                    // Copy old surface to new surface (basis)
                    gfx.Blit(oldSurface, surface);

                    // Recycle old surface (no longer needed)
                    SurfacePool.Recycle(oldSurface);

                    // Begin drawing to new layer surface
                    gfx.ResetState();
                    gfx.Surface = surface;
                    gfx.GlobalTransform *= Matrix.CreateScale(1F / layer.Downscale);
                }

                // Draw Entity
                gfx.PushState();
                entity.Draw(gfx, dt);
                gfx.PopState();
            }

            // Copy surface to screen
            gfx.Blit(surface, gfx.Screen.Surface);

            // Recycle old surface (no longer needed)
            SurfacePool.Recycle(surface);
        }

        private Surface GetSurface(IntSize screenSize, EffectLayer layer)
        {
            var surface = SurfacePool.Request(screenSize / layer.Downscale, Multisample);
            surface.Interpolation = InterpolationMode.Linear;
            surface.Repeat = RepeatMode.Clamp;
            return surface;
        }

        private EffectLayer GetEffectLayer(int index)
        {
            if (index >= 0 && index < EffectLayers.Count)
            {
                return EffectLayers[index];
            }
            else
            {
                return null;
            }
        }
    }
}
