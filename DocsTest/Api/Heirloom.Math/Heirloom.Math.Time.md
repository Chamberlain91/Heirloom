# Heirloom.Math

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md)</small>  

## Time (Static Class)
<small>**Namespace**: Heirloom.Math</sub></small>  

| Fields                            | Summary |
|-----------------------------------|---------|
| [NanosecondAsSeconds](#NANOAB7C)  |         |
| [MicrosecondAsSeconds](#MICRF645) |         |
| [MillisecondAsSeconds](#MILL5A8D) |         |
| [SecondAsSeconds](#SECO7D61)      |         |
| [MinuteAsSeconds](#MINUA84A)      |         |
| [HourAsSeconds](#HOURA85F)        |         |
| [DayAsSeconds](#DAYAF9EF)         |         |
| [WeekAsSeconds](#WEEKCE31)        |         |

| Methods                       | Summary                                                         |
|-------------------------------|-----------------------------------------------------------------|
| [GetEnglishTime](#GETEB2CC)   | Gets a human readable format for the time specified in seconds. |
| [GetShortTimeName](#GETSE4D4) | Gets a short name of the time unit (or its plural).             |
| [GetTimeName](#GETT2327)      | Gets the name of the time unit (or its plural).                 |
| [GetTimeString](#GETT29B4)    | Gets a human readable format for the given time and unit.       |
| [GetDuration](#GETD2C61)      | Gets the duration of a time unit in seconds.                    |
| [Convert](#CONV9AEE)          | Converts time from unit to another.                             |

### Fields

#### <a name="NANOAB7C"></a> NanosecondAsSeconds : float

#### <a name="MICRF645"></a> MicrosecondAsSeconds : float

#### <a name="MILL5A8D"></a> MillisecondAsSeconds : float

#### <a name="SECO7D61"></a> SecondAsSeconds : float

#### <a name="MINUA84A"></a> MinuteAsSeconds : float

#### <a name="HOURA85F"></a> HourAsSeconds : float

#### <a name="DAYAF9EF"></a> DayAsSeconds : float

#### <a name="WEEKCE31"></a> WeekAsSeconds : float

#### <a name="NANOAB7C"></a> NanosecondAsSeconds : float
<small>`Static`</small>

#### <a name="MICRF645"></a> MicrosecondAsSeconds : float
<small>`Static`</small>

#### <a name="MILL5A8D"></a> MillisecondAsSeconds : float
<small>`Static`</small>

#### <a name="SECO7D61"></a> SecondAsSeconds : float
<small>`Static`</small>

#### <a name="MINUA84A"></a> MinuteAsSeconds : float
<small>`Static`</small>

#### <a name="HOURA85F"></a> HourAsSeconds : float
<small>`Static`</small>

#### <a name="DAYAF9EF"></a> DayAsSeconds : float
<small>`Static`</small>

#### <a name="WEEKCE31"></a> WeekAsSeconds : float
<small>`Static`</small>

### Methods

#### <a name="GETE6BFD"></a> GetEnglishTime(float duration, string numberFormat = "0.0") : string
<small>`Static`</small>

Gets a human readable format for the time specified in seconds.


#### <a name="GETS394C"></a> GetShortTimeName([TimeUnit](Heirloom.Math.TimeUnit.md) unit) : string
<small>`Static`</small>

Gets a short name of the time unit (or its plural).

<small>**unit**: <param name="unit">Some time unit.</param></small>  

#### <a name="GETT83E3"></a> GetTimeName([TimeUnit](Heirloom.Math.TimeUnit.md) unit, bool plural = False) : string
<small>`Static`</small>

Gets the name of the time unit (or its plural).

<small>**unit**: <param name="unit">Some time unit.</param></small>  
<small>**plural**: <param name="plural">Should it be plural?</param></small>  

#### <a name="GETT6185"></a> GetTimeString(float duration, [TimeUnit](Heirloom.Math.TimeUnit.md) unit) : string
<small>`Static`</small>

Gets a human readable format for the given time and unit.


#### <a name="GETD6B27"></a> GetDuration([TimeUnit](Heirloom.Math.TimeUnit.md) unit) : float
<small>`Static`</small>

Gets the duration of a time unit in seconds.

<small>**unit**: <param name="unit">Some time unit.</param></small>  

#### <a name="CONV2778"></a> Convert(float value, [TimeUnit](Heirloom.Math.TimeUnit.md) source, [TimeUnit](Heirloom.Math.TimeUnit.md) target) : float
<small>`Static`</small>

Converts time from unit to another.

<small>**value**: <param name="value"> Some value of time in <paramref name="source" /> units. </param></small>  
<small>**source**: <param name="source"> Some representation of units of time ( input ). </param></small>  
<small>**target**: <param name="target"> Some representation of units of time ( return ). </param></small>  

