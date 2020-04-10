using System;

namespace Heirloom.Collections.Testing
{
    public sealed class SortableObject : IComparable<SortableObject>
    {
        public SortableObject(int number)
        {
            Number = number;
        }

        public int Number { get; set; }

        public int CompareTo(SortableObject other)
        {
            return Number.CompareTo(other.Number);
        }
    }
}
