# Heirloom.Math

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md)</small>  

## RandomExtensions (Static Class)
<small>**Namespace**: Heirloom.Math</sub></small>  

Provides extension methods for `System.Random` and other related random operations.

| Methods                     | Summary                                                                                         |
|-----------------------------|-------------------------------------------------------------------------------------------------|
| [NextFloat](#NEXTE2BB)      | Returns a random floating-point number that is greater than or equal to 0.0, and less than 1.0. |
| [NextFloat](#NEXTE2BB)      | Returns a random floating-point number that is within the specified range.                      |
| [NextDouble](#NEXTF1EE)     | Returns a random floating-point number that is within the specified range.                      |
| [NextFloat](#NEXTE2BB)      | Returns a random floating-point number that is within the specified range.                      |
| [Next](#NEXT9A90)           | Returns a random integer number that is within the specified range.                             |
| [NextVectorDisk](#NEXT8BC3) | Returns a random point within a unit circle.                                                    |
| [NextVectorDisk](#NEXT8BC3) | Returns a random point within a circle.                                                         |
| [NextUnitVector](#NEXTECA9) | Returns a random unit vector (point on edge of unit circle).                                    |
| [NextVector](#NEXTB4AA)     | Returns a random point within the specified rectangular domain.                                 |
| [Chance](#CHAN6203)         | Randomly return true for occurrences with the specified probability.                            |
| [Choose<T>](#CHOO16E1)      | Randomly select one of the specified items.                                                     |
| [Choose<T>](#CHOO16E1)      | Randomly select one of the specified items.                                                     |
| [Shuffle<T>](#SHUFB92E)     | Shuffles all elements in the list randomly.                                                     |
| [Shuffle<T>](#SHUFB92E)     | Shuffles all elements in the list randomly.                                                     |

### Methods

#### <a name="NEXTDC2B"></a> NextFloat(Random this) : float
<small>`Static`, `ExtensionAttribute`</small>

Returns a random floating-point number that is greater than or equal to 0.0, and less than 1.0.


#### <a name="NEXTDA61"></a> NextFloat(Random this, float min, float max) : float
<small>`Static`, `ExtensionAttribute`</small>

Returns a random floating-point number that is within the specified range.


#### <a name="NEXTEFCA"></a> NextDouble(Random this, double min, double max) : double
<small>`Static`, `ExtensionAttribute`</small>

Returns a random floating-point number that is within the specified range.


#### <a name="NEXT3494"></a> NextFloat(Random this, [Range](Heirloom.Math.Range.md) range) : float
<small>`Static`, `ExtensionAttribute`</small>

Returns a random floating-point number that is within the specified range.


#### <a name="NEXTFDB7"></a> Next(Random this, [IntRange](Heirloom.Math.IntRange.md) range) : float
<small>`Static`, `ExtensionAttribute`</small>

Returns a random integer number that is within the specified range.


#### <a name="NEXT211E"></a> NextVectorDisk(Random this) : [Vector](Heirloom.Math.Vector.md)
<small>`Static`, `ExtensionAttribute`</small>

Returns a random point within a unit circle.


#### <a name="NEXT63EC"></a> NextVectorDisk(Random this, float r) : [Vector](Heirloom.Math.Vector.md)
<small>`Static`, `ExtensionAttribute`</small>

Returns a random point within a circle.


#### <a name="NEXTCF76"></a> NextUnitVector(Random this) : [Vector](Heirloom.Math.Vector.md)
<small>`Static`, `ExtensionAttribute`</small>

Returns a random unit vector (point on edge of unit circle).


#### <a name="NEXT9D34"></a> NextVector(Random this, in [Rectangle](Heirloom.Math.Rectangle.md) domain) : [Vector](Heirloom.Math.Vector.md)
<small>`Static`, `ExtensionAttribute`</small>

Returns a random point within the specified rectangular domain.


#### <a name="CHAN4F5B"></a> Chance(Random this, float probability) : bool
<small>`Static`, `ExtensionAttribute`</small>

Randomly return true for occurrences with the specified probability.


#### <a name="CHOO35D9"></a> Choose<T>(Random this, IReadOnlyList\<T> items) : T
<small>`Static`, `ExtensionAttribute`</small>

Randomly select one of the specified items.


#### <a name="CHOOAA17"></a> Choose<T>(Random this, params T items) : T
<small>`Static`, `ExtensionAttribute`</small>

Randomly select one of the specified items.


#### <a name="SHUF79DA"></a> Shuffle<T>(Random this, IList\<T> items) : void
<small>`Static`, `ExtensionAttribute`</small>

Shuffles all elements in the list randomly.


#### <a name="SHUF28E3"></a> Shuffle<T>(IList\<T> this, Random random) : void
<small>`Static`, `ExtensionAttribute`</small>

Shuffles all elements in the list randomly.


