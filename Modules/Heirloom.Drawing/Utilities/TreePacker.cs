using Heirloom.Math;

namespace Heirloom.Drawing.Utilities
{
    public sealed class TreePacker<TElement> : RectanglePacker<TElement>
    {
        private Node _root;

        #region Constructor

        public TreePacker(int width, int height)
            : this(new IntSize(width, height))
        { }

        public TreePacker(IntSize size)
            : base(size)
        {
            // Start clean
            Clear();
        }

        #endregion

        protected override bool Insert(IntSize size, out IntRectangle rect)
        {
            // Initial node
            if (_root == null)
            {
                _root = new Node(0, 0, size.Width, size.Height);
            }

            // Insert rect
            var node = FindNode(_root, size.Width, size.Height);
            if (node != null) { node = SplitNode(node, size.Width, size.Height); }
            else { node = GrowNode(size.Width, size.Height); }

            // Unable to find suitable node for insertion
            if (node == null)
            {
                rect = default;
                return false;
            }

            // Assign size to node
            node.Size = size;

            // Store rectangle
            rect = node.Bounds;
            return true;
        }

        #region Binary Tree / Node

        private Node FindNode(Node root, int w, int h)
        {
            if (root.Used)
            {
                var r = FindNode(root.Right, w, h);
                if (r == null) { return FindNode(root.Down, w, h); }
                else { return r; }
            }
            else if ((w <= root.W) && (h <= root.H))
            {
                return root;
            }
            else
            {
                return null;
            }
        }

        private Node SplitNode(Node node, int w, int h)
        {
            node.Used = true;
            node.Down = new Node(node.X, node.Y + h, node.W, node.H - h);
            node.Right = new Node(node.X + w, node.Y, node.W - w, h);
            return node;
        }

        private Node GrowNode(int w, int h)
        {
            var canGrowDown = w <= _root.W;
            var canGrowRight = h <= _root.H;

            var shouldGrowRight = canGrowRight && (_root.H >= (_root.W + w)); // attempt to keep square-ish by growing right when height is much greater than width
            var shouldGrowDown = canGrowDown && (_root.W >= (_root.H + h)); // attempt to keep square-ish by growing down  when width  is much greater than height

            if (shouldGrowRight)
            {
                return GrowRight(w, h);
            }
            else if (shouldGrowDown)
            {
                return GrowDown(w, h);
            }
            else if (canGrowRight)
            {
                return GrowRight(w, h);
            }
            else if (canGrowDown)
            {
                return GrowDown(w, h);
            }
            else
            {
                return null; // need to ensure sensible root starting size to avoid this happening
            }
        }

        private Node GrowRight(int w, int h)
        {
            _root = new Node(0, 0, _root.W + w, _root.H)
            {
                Used = true,
                Down = _root,
                Right = new Node(_root.W, 0, w, _root.H)
            };

            var node = FindNode(_root, w, h);
            if (node != null) { return SplitNode(node, w, h); }
            else { return null; }
        }

        private Node GrowDown(int w, int h)
        {
            _root = new Node(0, 0, _root.W, _root.H + h)
            {
                Used = true,
                Down = new Node(0, _root.H, _root.W, h),
                Right = _root
            };

            var node = FindNode(_root, w, h);
            if (node != null) { return SplitNode(node, w, h); }
            else { return null; }
        }

        private class Node
        {
            public int X;
            public int Y;
            public int W;
            public int H;

            public bool Used;

            public Node Down;
            public Node Right;

            public IntRectangle Bounds => new IntRectangle((X, Y), Size);

            public IntSize Size { get; set; }

            public Node(int x, int y, int w, int h)
            {
                X = x;
                Y = y;
                W = w;
                H = h;
            }
        }

        #endregion
    }
}