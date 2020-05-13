using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;

using Heirloom.Desktop.Hardware;

namespace Heirloom.Benchmark
{
    public class BenchmarkResults : IEquatable<BenchmarkResults>
    {
        public GpuInfo GpuInfo { get; }

        public CpuInfo CpuInfo { get; }

        public Dictionary<string, float> Scores { get; }

        public BenchmarkResults(GpuInfo gpuInfo, CpuInfo cpuInfo)
        {
            GpuInfo = gpuInfo;
            CpuInfo = cpuInfo;
            Scores = new Dictionary<string, float>();
        }

        public string GenerateFilename()
        {
            var name = $"{GpuInfo.Name}_with_{CpuInfo.Name}.json";
            return $"{(uint) GetHashCode() % 10000:0000}_{name.ToIdentifier()}";
        }

        public static string ToJson(BenchmarkResults results)
        {
            var options = CreateSerializerOptions();
            return JsonSerializer.Serialize(results, options);
        }

        public static BenchmarkResults FromJson(string json)
        {
            var options = CreateSerializerOptions();
            return JsonSerializer.Deserialize<BenchmarkResults>(json, options);
        }

        private static JsonSerializerOptions CreateSerializerOptions()
        {
            // Write pretty and use enum names
            var options = new JsonSerializerOptions { WriteIndented = true };
            options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
            return options;
        }

        #region Equality

        public override bool Equals(object obj)
        {
            return Equals(obj as BenchmarkResults);
        }

        public bool Equals([AllowNull] BenchmarkResults other)
        {
            return other != null &&
                   GpuInfo.Equals(other.GpuInfo) &&
                   CpuInfo.Equals(other.CpuInfo) &&
                   EqualityComparer<Dictionary<string, float>>.Default.Equals(Scores, other.Scores);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(GpuInfo, CpuInfo, Scores);
        }

        public static bool operator ==(BenchmarkResults left, BenchmarkResults right)
        {
            return EqualityComparer<BenchmarkResults>.Default.Equals(left, right);
        }

        public static bool operator !=(BenchmarkResults left, BenchmarkResults right)
        {
            return !(left == right);
        }

        #endregion
    }
}
