# Heirloom.Math

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Math](../heirloom.math/heirloom.math.md)</small>  

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
| [Compute](#COMEE063A63) |  |
| [Compute](#COMCAF69844) |  |
| [Compute](#COM22A70C2) |  |

### Fields

#### <a name="AVE2099683"></a>Average : float
<small>`Read Only`</small>

The average value. Also known as the mean or expected value.

#### <a name="VAR7547CE81"></a>Variance : float
<small>`Read Only`</small>

The variance value.

#### <a name="DEVB36CB"></a>Deviation : float
<small>`Read Only`</small>

The standard deviation.

#### <a name="RAN67C3808B"></a>Range : [Range](heirloom.math.range.md)
<small>`Read Only`</small>

The range of values.

### Constructors

#### Statistics(float average, float variance, [Range](heirloom.math.range.md) range)

### Methods

#### <a name="COMEE063A63"></a>Compute(IEnumerable\<int> values) : [Statistics](heirloom.math.statistics.md)

<small>`Static`</small>


#### <a name="COMCAF69844"></a>Compute(IEnumerable\<float> values) : [Statistics](heirloom.math.statistics.md)

<small>`Static`</small>


#### <a name="COM22A70C2"></a>Compute(float sum, float squareSum, [Range](heirloom.math.range.md) range, int count) : [Statistics](heirloom.math.statistics.md)

<small>`Static`</small>


