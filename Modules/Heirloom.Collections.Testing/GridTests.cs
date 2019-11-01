using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Heirloom.IO;
using Heirloom.Math;
using NUnit.Framework;

namespace Heirloom.Collections.Testing
{
    [TestFixture]
    public class GridTests
    {
        // todo: use a multiple test case thing to construct IGrid to test both Grid and SparseGrid
        // todo: explicity test more sparse grid?

        public IReadOnlyList<string> Names { get; private set; }

        [OneTimeSetUp]
        public void Setup()
        {
            Names = LoadNamesFromFile();
        }

        [Test]
        public void SparseGridKeyTest()
        {
            GridIndexTest(new SparseGrid<Person>());
        }

        [Test]
        public void FiniteGridKeyTest()
        {
            GridIndexTest(new Grid<Person>(8, 8));
        }

        [Test]
        public void SparseGridKeyPerformanceTest()
        {
            const int IterationCount = 50;

            var avg = 0F;
            for (var i = 0; i < IterationCount; i++)
            {
                avg += GridPerformanceTest(new SparseGrid<string>(), 70, 70);
            }

            avg /= IterationCount;
            Console.WriteLine($"{Time.GetEnglishTime(avg)}");
        }

        [Test]
        public void FiniteGridKeyPerformanceTest()
        {
            const int IterationCount = 50;

            var avg = 0F;
            for (var i = 0; i < IterationCount; i++)
            {
                avg += GridPerformanceTest(new Grid<string>(70, 70), 70, 70);
            }

            avg /= IterationCount;
            Console.WriteLine($"{Time.GetEnglishTime(avg)}");
        }

        private static void GridIndexTest(IGrid<Person> grid)
        {
            var Joaquin = new Person("Joaquin");
            var Amanda = new Person("Amanda");

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

        private float GridPerformanceTest(IGrid<string> grid, int w, int h)
        {
            var sw = Stopwatch.StartNew();

            // Populate grid with names
            for (var y = 0; y < h; y++)
            {
                for (var x = 0; x < w; x++)
                {
                    var i = (y * w) + x;
                    grid[x, y] = Names[i];
                }
            }

            // Validate access
            for (var y = 0; y < h; y++)
            {
                for (var x = 0; x < w; x++)
                {
                    var i = (y * w) + x;
                    Assert.AreEqual(grid[x, y], Names[i]);
                }
            }

            return (float) sw.Elapsed.TotalSeconds;
        }

        private static IReadOnlyList<string> LoadNamesFromFile()
        {
            var names = new List<string>(); // 4945 names 
            var text = Files.ReadText("files.first-names.txt");
            foreach (var name in text.Split('\n'))
            {
                names.Add(name);
            }

            return names;
        }

        private readonly struct Person : IEquatable<Person>
        {
            public readonly string Name;

            public Person(string name)
            {
                Name = name;
            }

            public override bool Equals(object obj)
            {
                return obj is Person element && Equals(element);
            }

            public bool Equals([AllowNull] Person other)
            {
                return Name == other.Name;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(Name);
            }

            public static bool operator ==(Person left, Person right)
            {
                return left.Equals(right);
            }

            public static bool operator !=(Person left, Person right)
            {
                return !(left == right);
            }
        }
    }
}
