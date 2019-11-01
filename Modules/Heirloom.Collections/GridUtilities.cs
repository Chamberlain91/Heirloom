using System.Collections.Generic;

namespace Heirloom.Collections
{
    public static class GridUtilities
    {
        public static IEnumerable<(int X, int Y)> GetNeighboringCoordinates((int X, int Y) co, GridNeighborType neighborType)
        {
            if (neighborType == GridNeighborType.Axis)
            {
                // Clockwise from three o'clock
                yield return (co.X + 1, co.Y + 0);
                yield return (co.X + 0, co.Y + 1);
                yield return (co.X - 1, co.Y + 0);
                yield return (co.X + 0, co.Y - 1);
            }
            else
            if (neighborType == GridNeighborType.Diagonal)
            {
                // Clockwise from half past one
                yield return (co.X + 1, co.Y - 1);
                yield return (co.X + 1, co.Y + 1);
                yield return (co.X - 1, co.Y + 1);
                yield return (co.X - 1, co.Y - 1);
            }
            else
            {
                // Clockwise from three o'clock
                yield return (co.X + 1, co.Y + 0);
                yield return (co.X + 1, co.Y + 1);
                yield return (co.X + 0, co.Y + 1);
                yield return (co.X - 1, co.Y + 1);
                yield return (co.X - 1, co.Y + 0);
                yield return (co.X - 1, co.Y - 1);
                yield return (co.X + 0, co.Y - 1);
                yield return (co.X + 1, co.Y - 1);
            }
        }
    }
}
