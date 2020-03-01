# Heirloom.Math

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md)</small>  

## Statistics (Struct)
<small>**Namespace**: Heirloom.Math</sub></small>  
<small>**Interfaces**: IEquatable\<Statistics></small>  

Represents statistics of some data.

| Fields                 | Summary                                                      |
|------------------------|--------------------------------------------------------------|
| [Average](#AVER2099)   | The average value. Also known as the mean or expected value. |
| [Variance](#VARI7547)  | The variance value.                                          |
| [Deviation](#DEVIB36C) | The standard deviation.                                      |
| [Range](#RANG67C3)     | The range of values.                                         |

| Methods              | Summary |
|----------------------|---------|
| [Compute](#COMPF111) |         |
| [Compute](#COMPF111) |         |
| [Compute](#COMPF111) |         |

### Fields

#### <a name="AVER2099"></a> Average : float
<small>`Read Only`</small>

The average value. Also known as the mean or expected value.

#### <a name="VARI7547"></a> Variance : float
<small>`Read Only`</small>

The variance value.

#### <a name="DEVIB36C"></a> Deviation : float
<small>`Read Only`</small>

The standard deviation.

#### <a name="RANG67C3"></a> Range : [Range](Heirloom.Math.Range.md)
<small>`Read Only`</small>

The range of values.

### Constructors

#### Statistics(float average, float variance, [Range](Heirloom.Math.Range.md) range)

### Methods

#### <a name="COMP4784"></a> Compute(IEnumerable\<int> values) : [Statistics](Heirloom.Math.Statistics.md)
<small>`Static`</small>


#### <a name="COMP6B72"></a> Compute(IEnumerable\<float> values) : [Statistics](Heirloom.Math.Statistics.md)
<small>`Static`</small>


#### <a name="COMP5279"></a> Compute(float sum, float squareSum, [Range](Heirloom.Math.Range.md) range, int count) : [Statistics](Heirloom.Math.Statistics.md)
<small>`Static`</small>


