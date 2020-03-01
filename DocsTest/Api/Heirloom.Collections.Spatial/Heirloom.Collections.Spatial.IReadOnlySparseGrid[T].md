# Heirloom.Collections.Spatial

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Collections.Spatial](../Heirloom.Collections.Spatial/Heirloom.Collections.Spatial.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md)</small>  

## IReadOnlySparseGrid\<T> (Interface)
<small>**Namespace**: Heirloom.Collections.Spatial</sub></small>  
<small>**Interfaces**: [IReadOnlyGrid\<T>](Heirloom.Collections.Spatial.IReadOnlyGrid[T].md)</small>  

A sparse 2D grid of values.

| Properties        | Summary |
|-------------------|---------|
| [Keys](#KEYS3D37) |         |

| Methods               | Summary                                                             |
|-----------------------|---------------------------------------------------------------------|
| [HasValue](#HASVB314) | Determines if a value has been set on this cell of the sparse grid. |
| [HasValue](#HASVB314) | Determines if a value has been set on this cell of the sparse grid. |

### Properties

#### <a name="KEYS3D37"></a> Keys : IEnumerable\<IntVector>

<small>`Read Only`</small>

### Methods

#### <a name="HASV1E7B"></a> HasValue(in int x, in int y) : bool
<small>`Abstract`</small>

Determines if a value has been set on this cell of the sparse grid.


#### <a name="HASV93C6"></a> HasValue(in [IntVector](../Heirloom.Math/Heirloom.Math.IntVector.md) co) : bool
<small>`Abstract`</small>

Determines if a value has been set on this cell of the sparse grid.


