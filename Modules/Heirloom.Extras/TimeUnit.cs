namespace Heirloom.Extras
{
    /// <summary>
    /// Represents units of time, such as a millisecond.
    /// </summary>
    public enum TimeUnit
    {
        /// <summary>
        /// One week (604800 seconds)
        /// </summary>
        Week,

        /// <summary>
        /// One day (86400 seconds)
        /// </summary>
        Day,

        /// <summary>
        /// One hour (3600 seconds)
        /// </summary>
        Hour,

        /// <summary>
        /// One minute (60 seconds).
        /// </summary>
        Minute,

        /// <summary>
        /// One second (base unit).
        /// </summary>
        Second,

        /// <summary>
        /// One millisecond ( 1/1000 of a second )
        /// </summary>
        Millisecond,

        /// <summary>
        /// One microsecond ( 1/1000000 of a second )
        /// </summary>
        Microsecond,

        /// <summary>
        /// One nanosecond ( 1/100000000 of a second )
        /// </summary>
        Nanosecond
    }
}
