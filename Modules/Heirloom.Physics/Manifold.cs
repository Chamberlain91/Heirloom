using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using Heirloom.Math;

namespace Heirloom.Physics
{
    [StructLayout(LayoutKind.Explicit, Pack = 4)]
    // todo: rename? ContactManifold? CollisionInfo? ContactGroup?
    // or, remove entirely if returning Contact[] doesn't affect performance, but I imagine it will.
    public unsafe struct Manifold : IEnumerable<Contact>
    {
        [FieldOffset(0 * 4)]
        private fixed float _contactsX[2];

        [FieldOffset(2 * 4)]
        private fixed float _contactsY[2];

        [FieldOffset(4 * 4)]
        private fixed float _depths[2];

        [FieldOffset(6 * 4)]
        private Vector _normal;

        [FieldOffset(8 * 4)]
        private Vector _tangent;

        [FieldOffset(10 * 4)]
        private int _count;

        public Vector Normal
        {
            get => _normal;

            internal set
            {
                _normal = value;
                _tangent = _normal.Perpendicular;
            }
        }

        public Vector Tangent => _tangent;

        public int Count
        {
            get => _count;
            internal set => _count = value;
        }

        public Contact this[int i] => GetContact(i);

        public Contact GetContact(int i)
        {
            var p = GetPosition(i);
            var d = GetSeparation(i);

            return new Contact(p, Normal, d);
        }

        public Vector GetPosition(int i)
        {
            if (i < 0) { throw new ArgumentOutOfRangeException(nameof(i)); }
            if (i >= Count) { throw new ArgumentOutOfRangeException(nameof(i)); }

            var x = _contactsX[i];
            var y = _contactsY[i];

            return new Vector(x, y);
        }

        public float GetSeparation(int i)
        {
            if (i < 0) { throw new ArgumentOutOfRangeException(nameof(i)); }
            if (i >= Count) { throw new ArgumentOutOfRangeException(nameof(i)); }

            return _depths[i];
        }

        internal void SetContact(int i, Vector position, float depth)
        {
            _contactsX[i] = position.X;
            _contactsY[i] = position.Y;
            _depths[i] = depth;
        }

        public IEnumerator<Contact> GetEnumerator()
        {
            for (var i = 0; i < Count; i++)
            {
                yield return GetContact(i);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
