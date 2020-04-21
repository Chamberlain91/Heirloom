using System;
using System.Collections;
using System.Collections.Generic;

using Heirloom;

namespace Heirloom
{
    /// <summary>
    /// A spatial collection to store and query elements in 2D space, implemented as a BVH style tree and has infinite bounds.
    /// </summary>
    public sealed class BoundingTreeSpatialCollection<T> : ISpatialCollection<T>
    {
        private readonly Dictionary<T, Node> _nodes;
        private readonly float _margin;
        private Node _root;

        [ThreadStatic] private static readonly Queue<Node> _queryQueue = new Queue<Node>();

        #region Constructors

        public BoundingTreeSpatialCollection(float margin = 0.1F)
        {
            _nodes = new Dictionary<T, Node>();
            _margin = margin;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the number of elements stored in this collection.
        /// </summary>
        public int Count => _nodes.Count;

        #endregion

        #region Collection Operations

        /// <summary>
        /// Clears all elements from this spatial collection.
        /// </summary>
        public void Clear()
        {
            // todo: recycle nodes in node pool?
            _nodes.Clear();
            _root = null;
        }

        /// <summary>
        /// Adds an element with rectangle bounds into this spatial collection.
        /// </summary>
        public void Add(in T item, in IShape boundingShape)
        {
            if (_nodes.ContainsKey(item)) { throw new ArgumentException($"Spatial item already exists in collection, unable to add."); }
            else
            {
                var bounds = boundingShape.Bounds;

                // Create node
                var node = Node.Create(item, boundingShape, _margin);

                // Store node by item
                _nodes.Add(item, node);

                // Insert into root (descend tree)
                InsertNode(ref _root, node);
            }
        }

        /// <summary>
        /// Updates an exising element with new bounds in the collection.
        /// </summary>
        public void Update(in T item, in IShape boundingShape)
        {
            if (_nodes.TryGetValue(item, out var node))
            {
                var shapeBounds = boundingShape.Bounds;

                // If the existing node bounds do not still contain the bounds, process update
                if (!node.Bounds.Contains(shapeBounds))
                {
                    // Remove node from structure
                    RemoveNode(node);

                    // 
                    node.Bounds = Rectangle.Inflate(shapeBounds, _margin);

                    // Reinsert item (possibly with new bounds)
                    InsertNode(ref _root, node);
                }
            }
            else
            {
                throw new ArgumentException($"Spatial element does not exist, unable to update.");
            }
        }

        /// <summary>
        /// Removes an element from this spatial collection.
        /// </summary>
        public bool Remove(in T item)
        {
            // 
            if (_nodes.TryGetValue(item, out var node))
            {
                // Remove item from node lookup
                _nodes.Remove(item);

                RemoveNode(node);

                // Recycle (clear and put back into pool)
                Node.Recycle(node);

                // Was contained
                return true;
            }
            else
            {
                // Was not contained
                return false;
            }
        }

        /// <summary>
        /// Determines if the specified element exists in this collection.
        /// </summary>
        public bool Contains(in T item)
        {
            return _nodes.ContainsKey(item);
        }

        #endregion

        #region Query Operations

        /// <summary>
        /// Queries the spatial collection and returns the elements with bounds that overlap the specified point.
        /// </summary>
        public IEnumerable<T> Query(Vector point)
        {
            if (_root == null) { yield break; }
            else
            {
                _queryQueue.Clear();
                _queryQueue.Enqueue(_root);

                // 
                while (_queryQueue.Count > 0)
                {
                    var node = _queryQueue.Dequeue();

                    if (node.Bounds.Contains(point))
                    {
                        // 
                        if (node.IsLeaf) { yield return node.Item; }
                        else
                        {
                            // 
                            _queryQueue.Enqueue(node.Children[0]);
                            _queryQueue.Enqueue(node.Children[1]);
                        }
                    }
                }

                // Clear queue to prevent holding references
                _queryQueue.Clear();
            }
        }

        /// <summary>
        /// Queries the spatial collection and returns the elements with bounds that overlap the specified rectangle.
        /// </summary>
        public IEnumerable<T> Query(IShape queryShape)
        {
            if (_root == null) { yield break; }
            else
            {
                _queryQueue.Clear();
                _queryQueue.Enqueue(_root);

                // 
                while (_queryQueue.Count > 0)
                {
                    var node = _queryQueue.Dequeue();

                    if (node.Bounds.Overlaps(queryShape))
                    {
                        // 
                        if (node.IsLeaf) { yield return node.Item; }
                        else
                        {
                            // 
                            _queryQueue.Enqueue(node.Children[0]);
                            _queryQueue.Enqueue(node.Children[1]);
                        }
                    }
                }

                // Clear queue to prevent holding references
                _queryQueue.Clear();
            }
        }

        /// <summary>
        /// Queries the spatial collection and returns the elements with bounds that intersect the specified ray.
        /// </summary>
        public IEnumerable<T> Query(Ray ray, float maxDistance = float.PositiveInfinity)
        {
            if (_root == null) { yield break; }
            else
            {
                _queryQueue.Clear();
                _queryQueue.Enqueue(_root);

                // 
                while (_queryQueue.Count > 0)
                {
                    var node = _queryQueue.Dequeue();

                    // 
                    if (node.Bounds.Raycast(ray))
                    {
                        // 
                        if (node.IsLeaf) { yield return node.Item; }
                        else
                        {
                            // 
                            _queryQueue.Enqueue(node.Children[0]);
                            _queryQueue.Enqueue(node.Children[1]);
                        }
                    }
                }

                // Clear queue to prevent holding references
                _queryQueue.Clear();
            }
        }

        #endregion

        #region Tree Operations

        private void InsertNode(ref Node parent, Node node)
        {
            // Parent was null (root node)
            if (parent == null)
            {
                // Assign as root node (and done, no more is needed)
                parent = node;
            }
            else
            // Parent is a leaf, must split into two
            if (parent.IsLeaf)
            {
                // Construct new parent node with the old parent and new item as children
                var merged = Rectangle.Merge(parent.Bounds, node.Bounds);

                // Get copy of reference to old parent
                var oldParent = parent;

                // Replace parent node
                parent = Node.Create(merged, oldParent, node);
                parent.Parent = oldParent.Parent;

                // Assign new leaf nodes their new parent
                oldParent.Parent = parent;
                node.Parent = parent;
            }
            // Parent was not a leaf, must determine which child to descend into
            else
            {
                // Compute the increase in area if we were to insert under that child
                var c1 = parent.Children[0].Bounds;
                var c2 = parent.Children[1].Bounds;
                var v1 = Rectangle.Merge(c1, node.Bounds).Area - c1.Area;
                var v2 = Rectangle.Merge(c2, node.Bounds).Area - c2.Area;

                // Insertion under first child produces least increase in area
                if (v1 < v2)
                {
                    // Insert under first child
                    InsertNode(ref parent.Children[0], node);
                }
                // Insertion under second child produces least increase in area
                else
                {
                    // Insert under second child
                    InsertNode(ref parent.Children[1], node);
                }

                // Expand parents bounds by the inserted item
                parent.RecomputeBounds();
            }
        }

        private void RemoveNode(Node node)
        {
            if (node.IsRoot)
            {
                Node.Recycle(node);
                _root = null;
            }
            else
            {
                // The other node on the branch
                ReplaceNode(node.Parent, node.GetSibling());
            }

            void ReplaceNode(Node target, Node replace)
            {
                // The replace node's parent will become the same as the target node's parent
                replace.Parent = target.Parent;

                // Is the target the root node?
                if (target.IsRoot)
                {
                    // Simply replace the root
                    _root = replace;
                }
                else
                {
                    // Reassign target index with node in parent
                    target.Parent.Children[target.Index] = replace;
                    target.Parent.RecomputeBounds();

                    // Recycle (clear and put back into pool)
                    Node.Recycle(target);
                }
            }
        }

        #endregion

        #region Enumerator

        public IEnumerator<T> GetEnumerator()
        {
            // ??
            return _nodes.Keys.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        private class Node
        {
            // 
            private static readonly Queue<Node> _pool = new Queue<Node>();

            public Rectangle Bounds;

            public IShape Shape;

            public Node[] Children;

            public Node Parent;

            public T Item;

            private Node() { }

            public static Node Create(T item, IShape shape, float margin)
            {
                var bounds = Rectangle.Inflate(shape.Bounds, margin);

                var node = Request();
                node.Bounds = bounds;
                node.Shape = shape;
                node.Item = item;

                return node;
            }

            public static Node Create(Rectangle bounds, Node c0, Node c1)
            {
                var node = Request();
                node.Children = new Node[2] { c0, c1 };
                node.Bounds = bounds;
                node.Item = default;

                return node;
            }

            private static Node Request()
            {
                if (_pool.Count > 0)
                {
                    lock (_pool)
                    {
                        // Request recycled node
                        return _pool.Dequeue();
                    }
                }
                else
                {
                    // Allocate new node
                    return new Node();
                }
            }

            public static void Recycle(Node node)
            {
                lock (_pool)
                {
                    _pool.Enqueue(node);

                    // Reset node
                    node.Children = default;
                    node.Bounds = default;
                    node.Parent = default;
                    node.Item = default;
                }
            }

            public bool IsLeaf => Children == null;

            public bool IsRoot => Parent == null;

            public int Index => !IsRoot ? Equals(Parent.Children[0], this) ? 0 : 1 : -1;

            /// <summary>
            /// Get this nodes sibling, returning null of no sibling exists (ie, root node).
            /// </summary>
            public Node GetSibling()
            {
                if (Parent == null)
                {
                    return null;
                }
                else
                {
                    return Equals(Parent.Children[0], this)
                         ? Parent.Children[1]
                         : Parent.Children[0];
                }
            }

            internal void RecomputeBounds()
            {
                if (IsLeaf)
                {
                    throw new InvalidOperationException("Node was a leaf, unable to recompute bounds!");
                }
                else
                {
                    Bounds = Rectangle.Merge(Children[0].Bounds, Children[1].Bounds);
                }
            }
        }
    }
}
