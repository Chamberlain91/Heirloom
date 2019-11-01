using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Heirloom.Math;
using NUnit.Framework;

namespace Heirloom.Collections.Spatial.Testing
{
    [TestFixture]
    public class GridTest
    {
        // todo: use a multiple test case thing to construct IGrid to test both Grid and SparseGrid
        // todo: explicity test more sparse grid?

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SparseGridKeyTest()
        {
            GridIndexTest(new SparseGrid<ToyElement>());
        }

        [Test]
        public void FiniteGridKeyTest()
        {
            GridIndexTest(new Grid<ToyElement>(8, 8));
        }

        private static void GridIndexTest(IGrid<ToyElement> grid)
        {
            var Joaquin = new ToyElement("Joaquin");
            var Amanda = new ToyElement("Amanda");

            // Set grid elements
            grid[0, 0] = Joaquin;
            grid[0, 1] = Amanda;

            // Coordinate 1
            Assert.IsTrue(grid[0, 0] == Joaquin);                // int pair
            Assert.IsTrue(grid[(0, 0)] == Joaquin);              // int tuple
            Assert.IsTrue(grid[new IntVector(0, 0)] == Joaquin); // int vector to implicit int tuple

            // Coordinate 2
            Assert.IsTrue(grid[0, 1] == Amanda);                // int pair
            Assert.IsTrue(grid[(0, 1)] == Amanda);              // int tuple
            Assert.IsTrue(grid[new IntVector(0, 1)] == Amanda); // int vector to implicit int tuple
        }

        private readonly struct ToyElement : IEquatable<ToyElement>
        {
            public readonly string Name;

            public ToyElement(string name)
            {
                Name = name;
            }

            public override bool Equals(object obj)
            {
                return obj is ToyElement element && Equals(element);
            }

            public bool Equals([AllowNull] ToyElement other)
            {
                return Name == other.Name;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(Name);
            }

            public static bool operator ==(ToyElement left, ToyElement right)
            {
                return left.Equals(right);
            }

            public static bool operator !=(ToyElement left, ToyElement right)
            {
                return !(left == right);
            }
        }
    }
}
