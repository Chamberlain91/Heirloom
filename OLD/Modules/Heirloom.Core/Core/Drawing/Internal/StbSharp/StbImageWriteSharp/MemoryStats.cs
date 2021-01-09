using System.Threading;

namespace StbImageWriteSharp
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0007:Use implicit type", Justification = "C to C# Ported Code")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "C to C# Ported Code")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0011:Add braces", Justification = "C to C# Ported Code")]
#if !STBSHARP_INTERNAL
	public
#else
    internal
#endif
	static class MemoryStats
	{
		private static int _allocations;

        public static int Allocations => _allocations;

        internal static void Allocated()
		{
			Interlocked.Increment(ref _allocations);
		}

		internal static void Freed()
		{
			Interlocked.Decrement(ref _allocations);
		}
	}
}
