# Heirloom.Math

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md)</small>  

## Statistics (Struct)
<small>**Namespace**: Heirloom.Math</small>  
<small>**Interfaces**: IEquatable\<Statistics></small>  
<small>`IsReadOnlyAttribute`</small>

Represents statistics of some data.

| Fields                   | Summary                                                      |
|--------------------------|--------------------------------------------------------------|
| [Average](#AVE2099683)   | The average value. Also known as the mean or expected value. |
| [Variance](#VAR7547CE81) | The variance value.                                          |
| [Deviation](#DEVB36CB)   | The standard deviation.                                      |
| [Range](#RAN67C3808B)    | The range of values.                                         |

| Methods                 | Summary                                                             |
|-------------------------|---------------------------------------------------------------------|
| [Compute](#COM4784E1C3) | Computes new statistics from a collection of integers.              |
| [Compute](#COM6B725764) | Computes new statistics from a collection of numbers.               |
| [Compute](#COM52793202) | Computes new statistics from a sum, squared sum, extents and count. |

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

#### <a name="RAN67C3808B"></a>Range : [Range](Heirloom.Math.Range.md)
<small>`Read Only`</small>

The range of values.

### Constructors

#### Statistics(float average, float variance, [Range](Heirloom.Math.Range.md) range)

Constructs a new statistics instance.

### Methods

#### <a name="COM4784E1C3"></a>Compute(IEnumerable\<int> values) : [Statistics](Heirloom.Math.Statistics.md)
<small>`Static`</small>

Computes new statistics from a collection of integers.


#### <a name="COM6B725764"></a>Compute(IEnumerable\<float> values) : [Statistics](Heirloom.Math.Statistics.md)
<small>`Static`</small>

Computes new statistics from a collection of numbers.


#### <a name="COM52793202"></a>Compute(float sum, float squareSum, [Range](Heirloom.Math.Range.md) range, int count) : [Statistics](Heirloom.Math.Statistics.md)
<small>`Static`</small>

Computes new statistics from a sum, squared sum, extents and count.


