# Heirloom.Math

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md)</small>  

## RandomExtensions (Static Class)
<small>**Namespace**: Heirloom.Math</sub></small>  

Provides extension methods for `System.Random` and other related random operations.

| Methods                        | Summary                                                                                         |
|--------------------------------|-------------------------------------------------------------------------------------------------|
| [NextFloat](#NEXE2BBABAB)      | Returns a random floating-point number that is greater than or equal to 0.0, and less than 1.0. |
| [NextFloat](#NEXE2BBABAB)      | Returns a random floating-point number that is within the specified range.                      |
| [NextDouble](#NEXF1EEB0FA)     | Returns a random floating-point number that is within the specified range.                      |
| [NextFloat](#NEXE2BBABAB)      | Returns a random floating-point number that is within the specified range.                      |
| [Next](#NEX9A907737)           | Returns a random integer number that is within the specified range.                             |
| [NextVectorDisk](#NEX8BC37A07) | Returns a random point within a unit circle.                                                    |
| [NextVectorDisk](#NEX8BC37A07) | Returns a random point within a circle.                                                         |
| [NextUnitVector](#NEXECA9ABD6) | Returns a random unit vector (point on edge of unit circle).                                    |
| [NextVector](#NEXB4AACA7C)     | Returns a random point within the specified rectangular domain.                                 |
| [Chance](#CHA6203B22)          | Randomly return true for occurrences with the specified probability.                            |
| [Choose<T>](#CHO16E1D157)      | Randomly select one of the specified items.                                                     |
| [Choose<T>](#CHO16E1D157)      | Randomly select one of the specified items.                                                     |
| [Shuffle<T>](#SHUB92EBACD)     | Shuffles all elements in the list randomly.                                                     |
| [Shuffle<T>](#SHUB92EBACD)     | Shuffles all elements in the list randomly.                                                     |

### Methods

#### <a name="NEXDC2B233D"></a>NextFloat(Random this) : float
<small>`Static`, `ExtensionAttribute`</small>

Returns a random floating-point number that is greater than or equal to 0.0, and less than 1.0.


#### <a name="NEXDA61B1EF"></a>NextFloat(Random this, float min, float max) : float
<small>`Static`, `ExtensionAttribute`</small>

Returns a random floating-point number that is within the specified range.


#### <a name="NEXEFCA8567"></a>NextDouble(Random this, double min, double max) : double
<small>`Static`, `ExtensionAttribute`</small>

Returns a random floating-point number that is within the specified range.


#### <a name="NEX34949485"></a>NextFloat(Random this, [Range](Heirloom.Math.Range.md) range) : float
<small>`Static`, `ExtensionAttribute`</small>

Returns a random floating-point number that is within the specified range.


#### <a name="NEXFDB7F181"></a>Next(Random this, [IntRange](Heirloom.Math.IntRange.md) range) : float
<small>`Static`, `ExtensionAttribute`</small>

Returns a random integer number that is within the specified range.


#### <a name="NEX211E62B0"></a>NextVectorDisk(Random this) : [Vector](Heirloom.Math.Vector.md)
<small>`Static`, `ExtensionAttribute`</small>

Returns a random point within a unit circle.


#### <a name="NEX63ECDA00"></a>NextVectorDisk(Random this, float r) : [Vector](Heirloom.Math.Vector.md)
<small>`Static`, `ExtensionAttribute`</small>

Returns a random point within a circle.


#### <a name="NEXCF7602C1"></a>NextUnitVector(Random this) : [Vector](Heirloom.Math.Vector.md)
<small>`Static`, `ExtensionAttribute`</small>

Returns a random unit vector (point on edge of unit circle).


#### <a name="NEX9D3451B7"></a>NextVector(Random this, in [Rectangle](Heirloom.Math.Rectangle.md) domain) : [Vector](Heirloom.Math.Vector.md)
<small>`Static`, `ExtensionAttribute`</small>

Returns a random point within the specified rectangular domain.


#### <a name="CHA4F5BB1DF"></a>Chance(Random this, float probability) : bool
<small>`Static`, `ExtensionAttribute`</small>

Randomly return true for occurrences with the specified probability.


#### <a name="CHO35D979BA"></a>Choose<T>(Random this, IReadOnlyList\<T> items) : T
<small>`Static`, `ExtensionAttribute`</small>

Randomly select one of the specified items.


#### <a name="CHOAA1704A7"></a>Choose<T>(Random this, params T items) : T
<small>`Static`, `ExtensionAttribute`</small>

Randomly select one of the specified items.


#### <a name="SHU79DAFD52"></a>Shuffle<T>(Random this, IList\<T> items) : void
<small>`Static`, `ExtensionAttribute`</small>

Shuffles all elements in the list randomly.


#### <a name="SHU28E3FA21"></a>Shuffle<T>(IList\<T> this, Random random) : void
<small>`Static`, `ExtensionAttribute`</small>

Shuffles all elements in the list randomly.


