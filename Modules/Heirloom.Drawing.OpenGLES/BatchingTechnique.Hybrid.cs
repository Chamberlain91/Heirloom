using System.Collections.Generic;

using Heirloom.Math;

namespace Heirloom.Drawing.OpenGLES
{
    internal sealed class HybridBatchingTechnique : BatchingTechnique
    {
        private readonly InstancingBatchingTechnique _instancingTechnique;
        private readonly StreamingBatchingTechnique _streamingTechnique;

        private readonly ObjectPool<Command> _commandPool = new ObjectPool<Command>();
        private readonly ObjectPool<Mesh> _meshPool = new ObjectPool<Mesh>();

        private readonly HashSet<Mesh> _meshRecycleSet = new HashSet<Mesh>();
        private readonly List<Command> _commandBuffer = new List<Command>();

        private Mesh _baseMesh, _copyMesh;
        private uint _baseMeshVersion;

        public HybridBatchingTechnique()
        {
            _instancingTechnique = new InstancingBatchingTechnique();
            _streamingTechnique = new StreamingBatchingTechnique();
        }

        public override bool IsDirty => _commandBuffer.Count > 0;

        protected internal override bool Submit(Mesh mesh, in Rectangle uvRect, in Matrix transform, in Color color)
        {
            // If the mesh isn't consistent with last submission defensively copy the mesh
            if (_baseMesh != mesh || _baseMeshVersion != mesh.Version)
            {
                // Copy data into defensive copy mesh
                _copyMesh = _meshPool.Request();
                _copyMesh.AddVertices(mesh.Vertices);
                _copyMesh.AddIndices(mesh.Indices);

                // Track mesh information
                _baseMeshVersion = mesh.Version;
                _baseMesh = mesh;
            }

            // Construct command
            var command = _commandPool.Request();
            command.Mesh = _copyMesh;
            command.Transform = transform;
            command.UVRect = uvRect;
            command.Color = color;

            // Add submission to buffer
            _commandBuffer.Add(command);

            return true;
        }

        protected internal override void DrawBatch()
        {
            for (var i = 0; i < _commandBuffer.Count; i++)
            {
                var command = _commandBuffer[i];

                // Look into the future and determine how many instances exist
                // todo: Can probably optimize by breaking loop in CountInstances at
                // decision boundary (ie. 10 instances) and then when submitting scanning
                // until invalid command is visited. This might improve performance as
                // this look ahead will do less work. Perhaps tracking the instancing length
                // in Submit() and avoiding a look-ahead all together.
                var instances = CountInstances(i, command.Mesh);

                // Are there enough instances detected to batch draw via instancing?
                if (instances > 100) // an arbitrary number I picked
                {
                    // If streaming had anything batched, draw that now to keep order of operations
                    _streamingTechnique.DrawBatch();

                    // Submit instances
                    for (var j = i; j < i + instances; j++)
                    {
                        var instance = _commandBuffer[j];

                        // Submit draw command to instancing technique
                        while (!_instancingTechnique.Submit(instance.Mesh, in instance.UVRect, in instance.Transform, in instance.Color))
                        {
                            // Draw previously batched instances and try again
                            _instancingTechnique.DrawBatch();
                        }

                        // We no longer need this command
                        _commandPool.Recycle(instance);
                    }

                    // Schedule to recycle mesh
                    _meshRecycleSet.Add(command.Mesh);

                    // Draw batched instances
                    _instancingTechnique.DrawBatch();

                    // Advance main loop by instances
                    i += instances;
                }
                else
                {
                    // Submit draw command to streaming technique
                    while (!_streamingTechnique.Submit(command.Mesh, in command.UVRect, in command.Transform, in command.Color))
                    {
                        // Draw previously batched triangles and try again
                        _streamingTechnique.DrawBatch();
                    }

                    // Schedule to recycle mesh
                    _meshRecycleSet.Add(command.Mesh);

                    // We no longer need this command
                    _commandPool.Recycle(command);
                }
            }

            // Draw batched triangles
            _streamingTechnique.DrawBatch();

            // Recycle meshes for future use
            foreach (var mesh in _meshRecycleSet)
            {
                _meshPool.Recycle(mesh);
                mesh.Clear();
            }

            // Clear commands and recycle set
            _meshRecycleSet.Clear();
            _commandBuffer.Clear();

            // Clear known mesh
            _baseMeshVersion = 0;
            _baseMesh = null;

            // Accumulate scores of each renderer
            BatchCount = _instancingTechnique.BatchCount + _streamingTechnique.BatchCount;
            DrawCount = _instancingTechnique.DrawCount + _streamingTechnique.DrawCount;
            TriCount = _instancingTechnique.TriCount + _streamingTechnique.TriCount;
        }

        internal override void ResetCounts()
        {
            // Reset each sub technique
            _instancingTechnique.ResetCounts();
            _streamingTechnique.ResetCounts();
            base.ResetCounts();
        }

        private int CountInstances(int i, Mesh mesh)
        {
            var instances = 1;
            for (var j = i + 1; j < _commandBuffer.Count; j++)
            {
                if (_commandBuffer[j].Mesh != mesh) { break; }
                instances++;
            }

            return instances;
        }

        private sealed class Command
        {
            public Mesh Mesh;
            public Rectangle UVRect;
            public Matrix Transform;
            public Color Color;
        }

        private sealed class ObjectPool<T> where T : class, new()
        {
            private readonly Queue<T> _queue = new Queue<T>();

            public T Request()
            {
                if (_queue.Count > 0)
                {
                    return _queue.Dequeue();
                }
                else
                {
                    return new T();
                }
            }

            public void Recycle(T item)
            {
                _queue.Enqueue(item);
            }
        }
    }
}
