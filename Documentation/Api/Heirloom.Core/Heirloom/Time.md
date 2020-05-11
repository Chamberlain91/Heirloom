# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Time (Class)

> **Namespace**: [Heirloom][0]

```cs
public static class Time
```

### Constants

[DayAsSeconds][1], [HourAsSeconds][2], [MicrosecondAsSeconds][3], [MillisecondAsSeconds][4], [MinuteAsSeconds][5], [NanosecondAsSeconds][6], [SecondAsSeconds][7], [WeekAsSeconds][8]

### Static Methods

[Convert][9], [GetDuration][10], [GetEnglishTime][11], [GetShortTimeName][12], [GetTimeName][13], [GetTimeString][14]

## Fields

| Name                      | Type    | Summary |
|---------------------------|---------|---------|
| [DayAsSeconds][1]         | `float` |         |
| [HourAsSeconds][2]        | `float` |         |
| [MicrosecondAsSeconds][3] | `float` |         |
| [MillisecondAsSeconds][4] | `float` |         |
| [MinuteAsSeconds][5]      | `float` |         |
| [NanosecondAsSeconds][6]  | `float` |         |
| [SecondAsSeconds][7]      | `float` |         |
| [WeekAsSeconds][8]        | `float` |         |

## Methods

| Name                            | Return Type | Summary                                                         |
|---------------------------------|-------------|-----------------------------------------------------------------|
| [Convert(float, TimeUni...][9]  | `float`     | Converts time from unit to another.                             |
| [GetDuration(TimeUnit)][10]     | `float`     | Gets the duration of a time unit in seconds.                    |
| [GetEnglishTime(float, ...][11] | `string`    | Gets a human readable format for the time specified in seconds. |
| [GetShortTimeName(TimeU...][12] | `string`    | Gets a short name of the time unit (or its plural).             |
| [GetTimeName(TimeUnit, ...][13] | `string`    | Gets the name of the time unit (or its plural).                 |
| [GetTimeString(float, T...][14] | `string`    | Gets a human readable format for the given time and unit.       |

[0]: ../../Heirloom.Core.md
[1]: Time/DayAsSeconds.md
[2]: Time/HourAsSeconds.md
[3]: Time/MicrosecondAsSeconds.md
[4]: Time/MillisecondAsSeconds.md
[5]: Time/MinuteAsSeconds.md
[6]: Time/NanosecondAsSeconds.md
[7]: Time/SecondAsSeconds.md
[8]: Time/WeekAsSeconds.md
[9]: Time/Convert.md
[10]: Time/GetDuration.md
[11]: Time/GetEnglishTime.md
[12]: Time/GetShortTimeName.md
[13]: Time/GetTimeName.md
[14]: Time/GetTimeString.md
