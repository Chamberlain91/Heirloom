# Heirloom.Math

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Math](../heirloom.math/heirloom.math.md)</small>  

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
| [GetShortTimeName](#GET6326A075) | Gets a short name of the time unit (or its plural). |
| [GetTimeName](#GET62AD944B) | Gets the name of the time unit (or its plural). |
| [GetTimeString](#GET62F2C903) | Gets a human readable format for the given time and unit. |
| [GetDuration](#GET24CDCFC2) | Gets the duration of a time unit in seconds. |
| [Convert](#CON6A9C862F) | Converts time from unit to another. |

### Fields

#### <a name="NANAB7C80EF"></a>NanosecondAsSeconds : float

#### <a name="MICF645D1B3"></a>MicrosecondAsSeconds : float

#### <a name="MIL5A8D1158"></a>MillisecondAsSeconds : float

#### <a name="SEC7D61D6C5"></a>SecondAsSeconds : float

#### <a name="MINA84AD1C3"></a>MinuteAsSeconds : float

#### <a name="HOUA85F810D"></a>HourAsSeconds : float

#### <a name="DAYF9EF09CD"></a>DayAsSeconds : float

#### <a name="WEECE3141C9"></a>WeekAsSeconds : float

#### <a name="NANAB7C80EF"></a>NanosecondAsSeconds : float
<small>`Static`</small>

#### <a name="MICF645D1B3"></a>MicrosecondAsSeconds : float
<small>`Static`</small>

#### <a name="MIL5A8D1158"></a>MillisecondAsSeconds : float
<small>`Static`</small>

#### <a name="SEC7D61D6C5"></a>SecondAsSeconds : float
<small>`Static`</small>

#### <a name="MINA84AD1C3"></a>MinuteAsSeconds : float
<small>`Static`</small>

#### <a name="HOUA85F810D"></a>HourAsSeconds : float
<small>`Static`</small>

#### <a name="DAYF9EF09CD"></a>DayAsSeconds : float
<small>`Static`</small>

#### <a name="WEECE3141C9"></a>WeekAsSeconds : float
<small>`Static`</small>

### Methods

#### <a name="GETAA5C8DBB"></a>GetEnglishTime(float duration, string numberFormat = 0.0) : string

<small>`Static`</small>

Gets a human readable format for the time specified in seconds.


#### <a name="GET6326A075"></a>GetShortTimeName([TimeUnit](heirloom.math.timeunit.md) unit) : string

<small>`Static`</small>

Gets a short name of the time unit (or its plural).

<small>**unit**: <param name="unit">Some time unit.</param>  
</small>

#### <a name="GET62AD944B"></a>GetTimeName([TimeUnit](heirloom.math.timeunit.md) unit, bool plural = False) : string

<small>`Static`</small>

Gets the name of the time unit (or its plural).

<small>**unit**: <param name="unit">Some time unit.</param>  
</small>
<small>**plural**: <param name="plural">Should it be plural?</param>  
</small>

#### <a name="GET62F2C903"></a>GetTimeString(float duration, [TimeUnit](heirloom.math.timeunit.md) unit) : string

<small>`Static`</small>

Gets a human readable format for the given time and unit.


#### <a name="GET24CDCFC2"></a>GetDuration([TimeUnit](heirloom.math.timeunit.md) unit) : float

<small>`Static`</small>

Gets the duration of a time unit in seconds.

<small>**unit**: <param name="unit">Some time unit.</param>  
</small>

#### <a name="CON6A9C862F"></a>Convert(float value, [TimeUnit](heirloom.math.timeunit.md) source, [TimeUnit](heirloom.math.timeunit.md) target) : float

<small>`Static`</small>

Converts time from unit to another.

<small>**value**: <param name="value"> Some value of time in <paramref name="source" /> units. </param>  
</small>
<small>**source**: <param name="source"> Some representation of units of time ( input ). </param>  
</small>
<small>**target**: <param name="target"> Some representation of units of time ( return ). </param>  
</small>

