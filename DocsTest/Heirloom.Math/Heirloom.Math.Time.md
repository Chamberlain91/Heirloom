# Heirloom.Math

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md)</small>  

## Time (Static Class)
<small>**Namespace**: Heirloom.Math</sub></small>  

| Fields | Summary |
|-------|---------|
| [NanosecondAsSeconds](#NANAB7C80EF) |  |
| [MicrosecondAsSeconds](#MICF645D1B3) |  |
| [MillisecondAsSeconds](#MIL5A8D1158) |  |
| [SecondAsSeconds](#SEC7D61D6C5) |  |
| [MinuteAsSeconds](#MINA84AD1C3) |  |
| [HourAsSeconds](#HOUA85F810D) |  |
| [DayAsSeconds](#DAYF9EF09CD) |  |
| [WeekAsSeconds](#WEECE3141C9) |  |

| Methods | Summary |
|---------|---------|
| [GetEnglishTime](#GETAA5C8DBB) | Gets a human readable format for the time specified in seconds. |
| [GetShortTimeName](#GET394CEAF5) | Gets a short name of the time unit (or its plural). |
| [GetTimeName](#GET83E3260B) | Gets the name of the time unit (or its plural). |
| [GetTimeString](#GET61852783) | Gets a human readable format for the given time and unit. |
| [GetDuration](#GET6B270B02) | Gets the duration of a time unit in seconds. |
| [Convert](#CON27782F2F) | Converts time from unit to another. |

### Fields

#### NanosecondAsSeconds : float

#### MicrosecondAsSeconds : float

#### MillisecondAsSeconds : float

#### SecondAsSeconds : float

#### MinuteAsSeconds : float

#### HourAsSeconds : float

#### DayAsSeconds : float

#### WeekAsSeconds : float

#### NanosecondAsSeconds : float
<small>`Static`</small>

#### MicrosecondAsSeconds : float
<small>`Static`</small>

#### MillisecondAsSeconds : float
<small>`Static`</small>

#### SecondAsSeconds : float
<small>`Static`</small>

#### MinuteAsSeconds : float
<small>`Static`</small>

#### HourAsSeconds : float
<small>`Static`</small>

#### DayAsSeconds : float
<small>`Static`</small>

#### WeekAsSeconds : float
<small>`Static`</small>

### Methods

#### <a name="GETAA5C8DBB"></a>GetEnglishTime(float duration, string numberFormat = 0.0) : string

<small>`Static`</small>

Gets a human readable format for the time specified in seconds.


#### <a name="GET394CEAF5"></a>GetShortTimeName([TimeUnit](Heirloom.Math.TimeUnit.md) unit) : string

<small>`Static`</small>

Gets a short name of the time unit (or its plural).

<small>**unit**: <param name="unit">Some time unit.</param>  
</small>

#### <a name="GET83E3260B"></a>GetTimeName([TimeUnit](Heirloom.Math.TimeUnit.md) unit, bool plural = False) : string

<small>`Static`</small>

Gets the name of the time unit (or its plural).

<small>**unit**: <param name="unit">Some time unit.</param>  
</small>
<small>**plural**: <param name="plural">Should it be plural?</param>  
</small>

#### <a name="GET61852783"></a>GetTimeString(float duration, [TimeUnit](Heirloom.Math.TimeUnit.md) unit) : string

<small>`Static`</small>

Gets a human readable format for the given time and unit.


#### <a name="GET6B270B02"></a>GetDuration([TimeUnit](Heirloom.Math.TimeUnit.md) unit) : float

<small>`Static`</small>

Gets the duration of a time unit in seconds.

<small>**unit**: <param name="unit">Some time unit.</param>  
</small>

#### <a name="CON27782F2F"></a>Convert(float value, [TimeUnit](Heirloom.Math.TimeUnit.md) source, [TimeUnit](Heirloom.Math.TimeUnit.md) target) : float

<small>`Static`</small>

Converts time from unit to another.

<small>**value**: <param name="value"> Some value of time in <paramref name="source" /> units. </param>  
</small>
<small>**source**: <param name="source"> Some representation of units of time ( input ). </param>  
</small>
<small>**target**: <param name="target"> Some representation of units of time ( return ). </param>  
</small>

