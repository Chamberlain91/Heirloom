using System;

namespace Heirloom
{
    /// <summary>
    /// Provides utility functions for converting time between units of measure and string representation.
    /// </summary>
    /// <category>Utility</category>
    public static class Time
    {
        /// <summary>
        /// A value of one nanosecond measured in seconds.
        /// </summary>
        public const float NanosecondAsSeconds = 1e-9F;

        /// <summary>
        /// A value of one microsecond measured in seconds.
        /// </summary>
        public const float MicrosecondAsSeconds = 1e-6F;

        /// <summary>
        /// A value of one millisecond measured in seconds.
        /// </summary>
        public const float MillisecondAsSeconds = 1e-3F;

        /// <summary>
        /// A value of one second measured in seconds.
        /// </summary>
        public const float SecondAsSeconds = 1F;

        /// <summary>
        /// A value of one minute measured in seconds.
        /// </summary>
        public const float MinuteAsSeconds = 60 * SecondAsSeconds;

        /// <summary>
        /// A value of one hour measured in seconds.
        /// </summary>
        public const float HourAsSeconds = 60 * MinuteAsSeconds;

        /// <summary>
        /// A value of one day measured in seconds.
        /// </summary>
        public const float DayAsSeconds = 24 * HourAsSeconds;

        /// <summary>
        /// A value of one week measured in seconds.
        /// </summary>
        public const float WeekAsSeconds = 7 * DayAsSeconds;

        /// <summary>
        /// Gets a human readable format for the time specified in seconds.
        /// </summary>
        public static string GetEnglishTime(float duration, string numberFormat = "N1")
        {
            if (duration >= WeekAsSeconds) { return str(TimeUnit.Week); }
            else if (duration >= DayAsSeconds) { return str(TimeUnit.Day); }
            else if (duration >= HourAsSeconds) { return str(TimeUnit.Hour); }
            else if (duration >= MinuteAsSeconds) { return str(TimeUnit.Minute); }
            else if (duration >= SecondAsSeconds) { return str(TimeUnit.Second); }
            else if (duration >= MillisecondAsSeconds) { return str(TimeUnit.Millisecond); }
            else if (duration >= MicrosecondAsSeconds) { return str(TimeUnit.Microsecond); }
            else if (duration >= NanosecondAsSeconds) { return str(TimeUnit.Nanosecond); }
            else
            {
                // unable to humanify the time
                return duration.ToString(numberFormat);
            }

            string str(TimeUnit unit)
            {
                // 
                var time = Convert(duration, TimeUnit.Second, unit);
                var timeName = GetTimeName(unit, time > 1F);

                return $"{time.ToString(numberFormat)} {timeName}";
            }
        }

        /// <summary>
        /// Gets a short name of the time unit.
        /// </summary>
        /// <param name="unit">Some time unit.</param>
        public static string GetShortTimeName(TimeUnit unit)
        {
            return unit switch
            {
                TimeUnit.Week => "w",
                TimeUnit.Day => "d",
                TimeUnit.Hour => "h",
                TimeUnit.Minute => "m",
                TimeUnit.Second => "s",
                TimeUnit.Millisecond => "ms",
                TimeUnit.Microsecond => "us",
                TimeUnit.Nanosecond => "ns",

                _ => throw new InvalidOperationException($"Unable to get short {nameof(TimeUnit)} name for {unit}."),
            };
        }

        /// <summary>
        /// Gets the name of the time unit (or its plural).
        /// </summary>
        /// <param name="unit">Some time unit.</param>
        /// <param name="plural">Should it be plural?</param> 
        public static string GetTimeName(TimeUnit unit, bool plural = false)
        {
            return unit switch
            {
                TimeUnit.Week => plural ? "weeks" : "week",
                TimeUnit.Day => plural ? "days" : "day",
                TimeUnit.Hour => plural ? "hours" : "hour",
                TimeUnit.Minute => plural ? "minutes" : "minute",
                TimeUnit.Second => plural ? "seconds" : "second",
                TimeUnit.Millisecond => plural ? "milliseconds" : "millisecond",
                TimeUnit.Microsecond => plural ? "microseconds" : "microsecond",
                TimeUnit.Nanosecond => plural ? "nanoseconds" : "nanosecond",

                _ => throw new InvalidOperationException($"Unable to get short {nameof(TimeUnit)} name for {unit}."),
            };
        }

        /// <summary>
        /// Gets a human readable format for the given time and unit.
        /// </summary>
        public static string GetTimeString(float duration, TimeUnit unit)
        {
            var seconds = Convert(duration, unit, TimeUnit.Second);
            return GetEnglishTime(seconds);
        }

        /// <summary>
        /// Gets the duration of a time unit in seconds.
        /// </summary>
        /// <param name="unit">Some time unit.</param>
        /// <returns>The respective time in seconds.</returns>
        public static float GetDuration(TimeUnit unit)
        {
            return Convert(1F, unit, TimeUnit.Second);
        }

        /// <summary>
        /// Converts time from unit to another.
        /// </summary>
        /// <param name="value"> Some value of time in <paramref name="source"/> units. </param>
        /// <param name="source"> Some representation of units of time ( input ). </param>
        /// <param name="target"> Some representation of units of time ( return ). </param>
        /// <returns> Some conversion from <paramref name="source"/> to <paramref name="target"/> units. </returns>
        public static float Convert(float value, TimeUnit source, TimeUnit target)
        {
            switch (source)
            {
                case TimeUnit.Week:
                    return Convert(value * WeekAsSeconds, TimeUnit.Second, target);

                case TimeUnit.Day:
                    return Convert(value * DayAsSeconds, TimeUnit.Second, target);

                case TimeUnit.Hour:
                    return Convert(value * HourAsSeconds, TimeUnit.Second, target);

                case TimeUnit.Minute:
                    return Convert(value * MinuteAsSeconds, TimeUnit.Second, target);

                case TimeUnit.Second:
                    switch (target)
                    {
                        case TimeUnit.Week: return value / WeekAsSeconds;
                        case TimeUnit.Day: return value / DayAsSeconds;
                        case TimeUnit.Hour: return value / HourAsSeconds;
                        case TimeUnit.Minute: return value / MinuteAsSeconds;
                        case TimeUnit.Second: return value;
                        case TimeUnit.Millisecond: return value * 1e+3F;
                        case TimeUnit.Microsecond: return value * 1e+6F;
                        case TimeUnit.Nanosecond: return value * 1e+9F;
                    }
                    break;

                case TimeUnit.Millisecond:
                    switch (target)
                    {
                        case TimeUnit.Week: return Convert(value / 1e+3F, TimeUnit.Second, TimeUnit.Week);
                        case TimeUnit.Day: return Convert(value / 1e+3F, TimeUnit.Second, TimeUnit.Day);
                        case TimeUnit.Hour: return Convert(value / 1e+3F, TimeUnit.Second, TimeUnit.Hour);
                        case TimeUnit.Minute: return Convert(value / 1e+3F, TimeUnit.Second, TimeUnit.Minute);

                        case TimeUnit.Second: return value / 1e+3F;
                        case TimeUnit.Millisecond: return value;
                        case TimeUnit.Microsecond: return value * 1e+3F;
                        case TimeUnit.Nanosecond: return value * 1e+6F;
                    }
                    break;

                case TimeUnit.Microsecond:
                    switch (target)
                    {
                        case TimeUnit.Week: return Convert(value / 1e+6F, TimeUnit.Second, TimeUnit.Week);
                        case TimeUnit.Day: return Convert(value / 1e+6F, TimeUnit.Second, TimeUnit.Day);
                        case TimeUnit.Hour: return Convert(value / 1e+6F, TimeUnit.Second, TimeUnit.Hour);
                        case TimeUnit.Minute: return Convert(value / 1e+6F, TimeUnit.Second, TimeUnit.Minute);

                        case TimeUnit.Second: return value / 1e+6F;
                        case TimeUnit.Millisecond: return value / 1e+3F;
                        case TimeUnit.Microsecond: return value;
                        case TimeUnit.Nanosecond: return value * 1e+3F;
                    }
                    break;

                case TimeUnit.Nanosecond:
                    switch (target)
                    {
                        case TimeUnit.Week: return Convert(value / 1e+9F, TimeUnit.Second, TimeUnit.Week);
                        case TimeUnit.Day: return Convert(value / 1e+9F, TimeUnit.Second, TimeUnit.Day);
                        case TimeUnit.Hour: return Convert(value / 1e+9F, TimeUnit.Second, TimeUnit.Hour);
                        case TimeUnit.Minute: return Convert(value / 1e+9F, TimeUnit.Second, TimeUnit.Minute);

                        case TimeUnit.Second: return value / 1e+9F;
                        case TimeUnit.Millisecond: return value / 1e+6F;
                        case TimeUnit.Microsecond: return value / 1e+3F;
                        case TimeUnit.Nanosecond: return value;
                    }
                    break;
            }

            throw new InvalidOperationException($"Invalid enum values must have been passed to {nameof(Convert)}.");
        }
    }
}
