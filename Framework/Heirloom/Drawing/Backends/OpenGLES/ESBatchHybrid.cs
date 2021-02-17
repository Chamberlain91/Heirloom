using System;
using System.Collections.Generic;

using Heirloom.Collections;
using Heirloom.Mathematics;

namespace Heirloom.Drawing.OpenGLES
{
    internal sealed class ESBatchHybrid : ESBatch
    {
        private const int BatchDecisionThreshold = 100;

        private readonly ESBatchInstance _instancingTechnique;
        private readonly ESBatchStreaming _streamingTechnique;

        private readonly ObjectPool<Command> _commandPool = new ObjectPool<Command>();
        private readonly ObjectPool<Mesh> _meshPool = new ObjectPool<Mesh>(m => m.Clear());

        private readonly HashSet<Mesh> _meshRecycleSet = new HashSet<Mesh>();
        private readonly List<Command> _commandBuffer = new List<Command>();

        private Mesh _baseMesh, _copyMesh;
        private uint _baseMeshVersion;

        private Color? _clearColor;

        public ESBatchHybrid(ESGraphicsContext context)
            : base(context)
        {
            _instancingTechnique = new ESBatchInstance(context);
            _streamingTechnique = new ESBatchStreaming(context);
        }

        public override bool IsDirty => _clearColor.HasValue || _commandBuffer.Count > 0;

        public override void Clear(Color color)
        {
            _clearColor = color;
        }

        public override bool Submit(Mesh mesh, Rectangle uvRect, Matrix transform, Color color)
        {
            // If the mesh isn't consistent with last submission defensively copy the mesh
            if (_baseMesh != mesh || _baseMeshVersion != mesh.Version)
            {
                // Request a pooled mesh to defensively copy into
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

        public override void Commit()
        {
            if (_clearColor.HasValue)
            {
                // Extract color from nullable
                var color = _clearColor.Value;
                _clearColor = null;

                // Clear
                GLES.SetClearColor(color.R, color.G, color.B, color.A);
                GLES.Clear(ClearMask.Color);
            }

            if (IsDirty)
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
                    if (instances > BatchDecisionThreshold) // an arbitrary number I picked
                    {
                        // If streaming had anything batched, draw that now to keep order of operations
                        _streamingTechnique.Commit();

                        // Submit instances
                        for (var j = i; j < i + instances; j++)
                        {
                            var instance = _commandBuffer[j];

                            // Submit draw command to instancing technique
                            while (!_instancingTechnique.Submit(instance.Mesh, instance.UVRect, instance.Transform, instance.Color))
                            {
                                // Draw previously batched instances and try again
                                _instancingTechnique.Commit();
                            }

                            // We no longer need this command
                            _commandPool.Recycle(instance);
                        }

                        // Schedule to recycle mesh
                        _meshRecycleSet.Add(command.Mesh);

                        // Draw batched instances
                        _instancingTechnique.Commit();

                        // Advance main loop by instances
                        i += instances - 1;
                    }
                    else
                    {
                        // Submit draw command to streaming technique
                        while (!_streamingTechnique.Submit(command.Mesh, command.UVRect, command.Transform, command.Color))
                        {
                            // Draw previously batched triangles and try again
                            _streamingTechnique.Commit();
                        }

                        // Schedule to recycle mesh
                        _meshRecycleSet.Add(command.Mesh);

                        // We no longer need this command
                        _commandPool.Recycle(command);
                    }
                }

                // Draw batched triangles
                _streamingTechnique.Commit();

                // Recycle meshes for future use
                foreach (var mesh in _meshRecycleSet)
                {
                    _meshPool.Recycle(mesh);
                }

                // Clear commands and recycle set
                _meshRecycleSet.Clear();
                _commandBuffer.Clear();
            }

            // Clear known mesh
            _baseMeshVersion = 0;
            _baseMesh = null;
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
            private readonly Bag<T> _bucket = new Bag<T>();
            private readonly Action<T> _reset;

            public ObjectPool(Action<T> reset = null)
            {
                _reset = reset;
            }

            public T Request()
            {
                if (_bucket.Count > 0)
                {
                    return _bucket.Remove();
                }
                else
                {
                    return new T();
                }
            }

            public void Recycle(T item)
            {
                _reset?.Invoke(item);
                _bucket.Add(item);
            }
        }
    }
}
