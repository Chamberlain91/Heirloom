# Heirloom.Collections.Spatial

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Collections.Spatial](../Heirloom.Collections.Spatial/Heirloom.Collections.Spatial.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md)</small>  

## IReadOnlySparseGrid\<T> (Interface)
<small>**Namespace**: Heirloom.Collections.Spatial</sub></small>  
<small>**Interfaces**: [IReadOnlyGrid\<T>](Heirloom.Collections.Spatial.IReadOnlyGrid[T].md)</small>  

A sparse 2D grid of values.

| Properties | Summary |
|------------|---------|
| [Keys](#KEY3D37EC76) |  |

| Methods | Summary |
|---------|---------|
| [HasValue](#HAS1E7B500D) | Determines if a value has been set on this cell of the sparse grid. |
| [HasValue](#HAS93C63DAD) | Determines if a value has been set on this cell of the sparse grid. |

### Properties

#### <a name="KEY3D37EC76"></a>Keys : IEnumerable\<IntVector>

<small>`Read Only`</small>

### Methods

#### <a name="HAS1E7B500D"></a>HasValue(in int x, in int y) : bool

<small>`Abstract`</small>

Determines if a value has been set on this cell of the sparse grid.


#### <a name="HAS93C63DAD"></a>HasValue(in [IntVector](../Heirloom.Math/Heirloom.Math.IntVector.md) co) : bool

<small>`Abstract`</small>

Determines if a value has been set on this cell of the sparse grid.


