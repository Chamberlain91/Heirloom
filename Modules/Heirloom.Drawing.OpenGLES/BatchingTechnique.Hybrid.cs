using System.Collections.Generic;

using Heirloom.Math;

namespace Heirloom.Drawing.OpenGLES
{
    internal sealed class HybridBatchingTechnique : BatchingTechnique
    {
        private readonly InstancingBatchingTechnique _instancingTechnique;
        private readonly StreamingBatchingTechnique _streamingTechnique;

        private readonly List<Command> _commands = new List<Command>();
        private readonly HashSet<Mesh> _recycleSet = new HashSet<Mesh>();
        private readonly Queue<Mesh> _meshPool = new Queue<Mesh>();

        private Mesh _baseMesh, _copyMesh;
        private uint _baseMeshVersion;

        public HybridBatchingTechnique()
        {
            _instancingTechnique = new InstancingBatchingTechnique();
            _streamingTechnique = new StreamingBatchingTechnique();
        }

        public override bool IsDirty => _commands.Count > 0;

        protected internal override bool Submit(Mesh mesh, in Rectangle uvRect, in Matrix transform, in Color color)
        {
            // If the mesh isn't consistent with last submission defensively copy the mesh
            if (_baseMesh != mesh || _baseMeshVersion != mesh.Version)
            {
                _copyMesh = RequestMesh();
                _copyMesh.AddVertices(mesh.Vertices);
                _copyMesh.AddIndices(mesh.Indices);

                // 
                _baseMeshVersion = mesh.Version;
                _baseMesh = mesh;
            }

            // Add submission to buffer
            _commands.Add(new Command(_copyMesh, in uvRect, in transform, in color));
            return true;
        }

        protected internal override void DrawBatch()
        {
            for (var i = 0; i < _commands.Count; i++)
            {
                var submission = _commands[i];

                // Look into the future and determine how many instances exist
                var instances = CountInstances(i, submission.Mesh);

                // Are there enough instances detected to batch draw via instancing?
                if (instances > 50)
                {
                    // If streaming had anything batched, draw that now to keep order
                    if (_streamingTechnique.IsDirty) { _streamingTechnique.DrawBatch(); }

                    // Submit instances
                    for (var j = i; j < i + instances; j++)
                    {
                        var instance = _commands[j];

                        // Submit draw command to instancing technique
                        while (!_instancingTechnique.Submit(instance.Mesh, in instance.UVRect, in instance.Transform, in instance.Color))
                        {
                            _instancingTechnique.DrawBatch();
                        }

                        // Schedule to recycle mesh
                        _recycleSet.Add(instance.Mesh);
                    }

                    if (_instancingTechnique.IsDirty) { _instancingTechnique.DrawBatch(); }

                    // Advance main loop by instances
                    i += instances;
                }
                else
                {
                    // Submit draw command to streaming technique
                    while (!_streamingTechnique.Submit(submission.Mesh, in submission.UVRect, in submission.Transform, in submission.Color))
                    {
                        _streamingTechnique.DrawBatch();
                    }

                    // Schedule to recycle mesh
                    _recycleSet.Add(submission.Mesh);
                }
            }

            // 
            if (_streamingTechnique.IsDirty) { _streamingTechnique.DrawBatch(); }

            // Recycle meshes for future use
            foreach (var mesh in _recycleSet) { RecycleMesh(mesh); }
            _recycleSet.Clear();

            // Clear commands
            _commands.Clear();

            // Clear known mesh
            _baseMeshVersion = 0;
            _baseMesh = null;
        }

        private int CountInstances(int i, Mesh mesh)
        {
            var instances = 1;
            for (var j = i + 1; j < _commands.Count; j++)
            {
                if (_commands[j].Mesh != mesh) { break; }
                instances++;
                // todo: can probably optimize by breaking loop at decision boundary (ie. 10 instances)
            }

            return instances;
        }

        private Mesh RequestMesh()
        {
            if (_meshPool.Count > 0)
            {
                var mesh = _meshPool.Dequeue();
                mesh.Clear();

                return mesh;
            }
            else
            {
                return new Mesh();
            }
        }

        private void RecycleMesh(Mesh mesh)
        {
            _meshPool.Enqueue(mesh);
        }

        private class Command
        {
            public Mesh Mesh;
            public Rectangle UVRect;
            public Matrix Transform;
            public Color Color;

            public Command(Mesh mesh, in Rectangle uvRect, in Matrix transform, in Color color)
            {
                Mesh = mesh;
                UVRect = uvRect;
                Transform = transform;
                Color = color;
            }
        }
    }
}
