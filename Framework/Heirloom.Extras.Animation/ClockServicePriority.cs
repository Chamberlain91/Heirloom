namespace Heirloom.Extras.Animation
{
    /// <summary>
    /// Represents a standard set of priorities for a <see cref="ClockService"/>.
    /// </summary>
    public enum ClockServicePriority : int
    {
        Animation = 3000,
        Coroutines = 2000,
        Intervals = 1000,
        Default = 0
    }
}
