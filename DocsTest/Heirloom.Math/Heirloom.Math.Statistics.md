# Heirloom.Math

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md)</small>  

## Statistics (Struct)
<small>**Namespace**: Heirloom.Math</sub></small>  
<small>**Interfaces**: IEquatable\<Statistics></small>  

Represents statistics of some data.

| Fields | Summary |
|-------|---------|
| [Average](#AVE2099683) | The average value. Also known as the mean or expected value. |
| [Variance](#VAR7547CE81) | The variance value. |
| [Deviation](#DEVB36CB) | The standard deviation. |
| [Range](#RAN67C3808B) | The range of values. |

| Methods | Summary |
|---------|---------|
| [Compute](#COM4784E1C3) |  |
| [Compute](#COM6B725764) |  |
| [Compute](#COM52793202) |  |

### Fields

#### Average : float
<small>`Read Only`</small>

The average value. Also known as the mean or expected value.

#### Variance : float
<small>`Read Only`</small>

The variance value.

#### Deviation : float
<small>`Read Only`</small>

The standard deviation.

#### Range : [Range](Heirloom.Math.Range.md)
<small>`Read Only`</small>

The range of values.

### Constructors

#### Statistics(float average, float variance, [Range](Heirloom.Math.Range.md) range)

### Methods

#### <a name="COM4784E1C3"></a>Compute(IEnumerable\<int> values) : [Statistics](Heirloom.Math.Statistics.md)

<small>`Static`</small>


#### <a name="COM6B725764"></a>Compute(IEnumerable\<float> values) : [Statistics](Heirloom.Math.Statistics.md)

<small>`Static`</small>


#### <a name="COM52793202"></a>Compute(float sum, float squareSum, [Range](Heirloom.Math.Range.md) range, int count) : [Statistics](Heirloom.Math.Statistics.md)

<small>`Static`</small>


