using System;

using Meadows.Mathematics;

namespace Meadows.Drawing
{
    public interface IDrawContext
    {
        // sampler mode
        InterpolationMode Interpolation { get; set; }

        // blending state
        BlendingMode Blending { get; set; }

        // shader program 
        Shader Shader { get; set; }

        #region Stencil

        // clear stencil buffer
        void ClearMask();

        // colmask=0, stencil replace ref+1
        void BeginMask();

        // colmask=all, stencil equal ref 
        void EndMask();

        #endregion

        #region Draw

        // instanced mesh
        void Draw(Texture texture, in ReadOnlySpan<Vertex> vertices, in ReadOnlySpan<Matrix> matrices);

        public unsafe void Draw(Texture texture, in ReadOnlySpan<Vertex> vertices, in Matrix matrix)
        {
            fixed (Matrix* matrix_ptr = &matrix)
            {
                var matrices = new ReadOnlySpan<Matrix>(matrix_ptr, 1);
                Draw(texture, vertices, in matrices);
            }
        }

        public unsafe void Draw(DrawBuffer buffer)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public abstract class RenderContext
    {
        // sampler mode
        public abstract InterpolationMode Interpolation { get; set; }

        // blending state
        public abstract BlendingMode Blending { get; set; }

        // shader program 
        public abstract Shader Shader { get; set; }

        #region Stencil

        // clear stencil buffer
        public abstract void ClearMask();

        // colmask=0, stencil replace ref+1
        public abstract void BeginMask();

        // colmask=all, stencil equal ref 
        public abstract void EndMask();

        #endregion

        #region Draw

        // instanced mesh
        public abstract void Draw(Texture texture, in ReadOnlySpan<Vertex> vertices, in ReadOnlySpan<Matrix> matrices);

        public unsafe void Draw(Texture texture, in ReadOnlySpan<Vertex> vertices, in Matrix matrix)
        {
            fixed (Matrix* matrix_ptr = &matrix)
            {
                var matrices = new ReadOnlySpan<Matrix>(matrix_ptr, 1);
                Draw(texture, vertices, in matrices);
            }
        }

        #endregion
    }
     
    public abstract class SurfaceContext : RenderContext
    {
        public MultisampleQuality Multisample { get; init; }

        public abstract void SetViewport(Rectangle rectangle);

        public abstract void SetCamera(Vector center, float scale = 1F, float rotation = 0F);

        public abstract void Clear(Color color);
    }

    public sealed class RenderQueue : RenderContext
    {
        public override InterpolationMode Interpolation { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override BlendingMode Blending { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override Shader Shader { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void ClearMask()
        {
            throw new NotImplementedException();
        }

        public override void BeginMask()
        {
            throw new NotImplementedException();
        }

        public override void EndMask()
        {
            throw new NotImplementedException();
        }

        public override void Draw(Texture texture, in ReadOnlySpan<Vertex> vertices, in ReadOnlySpan<Matrix> matrices)
        {
            throw new NotImplementedException();
        }
    }
}
