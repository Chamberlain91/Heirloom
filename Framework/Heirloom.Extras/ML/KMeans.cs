using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Heirloom.Mathematics;

namespace Heirloom.Extras.ML
{
    /// <summary>
    /// An implementation of the k-Means algorithm.
    /// </summary>
    public static class KMeans
    {
        /// <summary>
        /// Computes the classification of values into clusters using K-Means.
        /// </summary>
        /// <param name="values">Some values.</param>
        /// <param name="numberOfClusters">The number of clusters to detect.</param>
        /// <param name="tolerance">The error/distance tolerance before termination.</param>
        /// <param name="computeMean">A function to compute the mean of each cluster of values.</param>
        /// <param name="computeDistance">A function to compute the error/distance of a pair of values.</param>
        /// <param name="maxIterations">The maximum number of iterations allowed before termination.</param>
        /// <returns>The K clusters found.</returns>
        public static Cluster<T>[] FindClusters<T>(IEnumerable<T> values, int numberOfClusters, float tolerance,
                                                   Func<IEnumerable<(T value, int cost)>, T> computeMean,
                                                   Func<T, T, float> computeDistance, int maxIterations = 200)
        {
            // Construct data points
            var points = values.GroupBy(v => v)
                               .Select(g => new DataPoint<T>(g.Key, g.Count()))
                               .ToArray();

            // Initialize the cluster centroids
            var means = InitializeMeans(numberOfClusters, computeDistance, points);

            // Initialize the clusters
            var clusters = new HashSet<DataPoint<T>>[numberOfClusters];
            for (var i = 0; i < numberOfClusters; i++) { clusters[i] = new HashSet<DataPoint<T>>(); }

            var iterations = 0;

            var error = 0F;

            do
            {
                // Update the cluster means
                UpdateMeans(out error);

                // Reclassify points into new clusters
                DetectClusters();

                // Terminates the loop if the loop has continued for too long.
                if (iterations++ > maxIterations) { break; }
            }
            while (error > tolerance);

            // Construct final value -> cluster mapping
            var _clusters = new Cluster<T>[numberOfClusters];
            for (var i = 0; i < numberOfClusters; i++)
            {
                var list = new List<T>();
                // todo: replace points list with dictionary?
                foreach (var data in points.Where(d => d.Cluster == i))
                {
                    list.AddRange(Enumerable.Repeat(data.Value, data.Count));
                }

                _clusters[i] = new Cluster<T>(list);
            }

            return _clusters;

            void UpdateMeans(out float error)
            {
                error = 0F;

                for (var i = 0; i < numberOfClusters; i++)
                {
                    ref var mean = ref means[i];

                    var prior = mean;
                    mean = computeMean(clusters[i].Select(d => (d.Value, d.Count)));

                    // Accumulate amount of change (error).
                    error += computeDistance(prior, mean);
                }
            }

            void DetectClusters()
            {
                foreach (var data in points)
                {
                    // Select the minimal element
                    var nearestDistance = float.PositiveInfinity;
                    var nearest = 0;

                    for (var cluster = 0; cluster < numberOfClusters; cluster++)
                    {
                        ref var mean = ref means[cluster];

                        // Select this mean if it is the nearest to the item.
                        var distance = computeDistance(mean, data.Value);
                        if (distance < nearestDistance)
                        {
                            nearestDistance = distance;
                            nearest = cluster;
                        }
                    }

                    // The data point has changed clusters
                    if (data.Cluster != nearest)
                    {
                        // If data point is already part of another cluster, remove it from there.
                        if (data.Cluster >= 0) { clusters[data.Cluster].Remove(data); }

                        // Reclassify the data point as part of the new cluster
                        clusters[nearest].Add(data);
                        data.Cluster = nearest;
                    }
                }
            }
        }

        private static T[] InitializeMeans<T>(int numberOfClusters, Func<T, T, float> computeDistance, DataPoint<T>[] points)
        {

            // Initialize the cluster centers (aka, the means)
            var means = new T[numberOfClusters];

            // We need to select the initial position of each mean
            for (var i = 0; i < numberOfClusters; i++)
            {
                ref var mean = ref means[i];

                if (i == 0)
                {
                    // First mean should just be randomly chosen
                    mean = points[Calc.Random.Next(0, points.Length)].Value;
                }
                else
                {
                    var furthestDistance = float.NegativeInfinity;
                    var furthest = default(T);

                    // Look at all previously assigned clusters
                    for (var j = 0; j < i; j++)
                    {
                        // Look at all points
                        for (var k = 0; k < points.Length; k++)
                        {
                            var data = points[k];

                            // Select this mean if it is the nearest to the item.
                            var distance = computeDistance(means[j], data.Value);
                            if (distance > furthestDistance)
                            {
                                furthestDistance = distance;
                                furthest = data.Value;
                            }
                        }
                    }

                    mean = furthest;
                }
            }

            return means;
        }

        private class DataPoint<T>
        {
            public readonly T Value;
            public readonly int Count;
            public int Cluster;

            public DataPoint(T value, int count)
            {
                Value = value;
                Count = count;
                Cluster = -1;
            }
        }
    }

    public class Cluster<T> : IReadOnlyList<T>
    {
        private readonly IReadOnlyList<T> _values;

        public Cluster(IReadOnlyList<T> values)
        {
            _values = values ?? throw new ArgumentNullException(nameof(values));
        }

        public T this[int index] => _values[index];

        public int Count => _values.Count;

        public IEnumerator<T> GetEnumerator()
        {
            return _values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) _values).GetEnumerator();
        }
    }
}
