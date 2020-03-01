# Heirloom.Collections.Spatial

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Collections.Spatial](../Heirloom.Collections.Spatial/Heirloom.Collections.Spatial.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md)</small>  

## SparseGrid\<T> (Sealed Class)
<small>**Namespace**: Heirloom.Collections.Spatial</sub></small>  
<small>**Interfaces**: [ISparseGrid\<T>](Heirloom.Collections.Spatial.ISparseGrid[T].md), [IReadOnlySparseGrid\<T>](Heirloom.Collections.Spatial.IReadOnlySparseGrid[T].md), [IReadOnlyGrid\<T>](Heirloom.Collections.Spatial.IReadOnlyGrid[T].md), [IGrid\<T>](Heirloom.Collections.Spatial.IGrid[T].md)</small>  

An infinite, sparse grid of values.

| Properties           | Summary |
|----------------------|---------|
| [Item](#ITE8B5A2F95) |         |
| [Item](#ITE8B5A2F95) |         |
| [Keys](#KEY3D37EC76) |         |

| Methods                           | Summary                                                             |
|-----------------------------------|---------------------------------------------------------------------|
| [Clear](#CLE4538C554)             | Removes all values in the grid, marking everything as unoccupied.   |
| [ClearValue](#CLEFB28FAFA)        | Clears the assigned valueon this cell of the sparse grid.           |
| [ClearValue](#CLEA46312EA)        | Clears the assigned valueon this cell of the sparse grid.           |
| [HasValue](#HAS1E7B500D)          | Determines if a value has been set on this cell of the sparse grid. |
| [HasValue](#HAS93C63DAD)          | Determines if a value has been set on this cell of the sparse grid. |
| [IsValidCoordinate](#ISVB586DBEE) | Is the specified coordinate valid on this grid?                     |
| [IsValidCoordinate](#ISVCA356546) | Is the specified coordinate valid on this grid?                     |

### Constructors

#### SparseGrid()

### Properties

#### <a name="ITE8B5A2F95"></a>Item : T


#### <a name="ITE8B5A2F95"></a>Item : T


#### <a name="KEY3D37EC76"></a>Keys : IEnumerable\<IntVector>

<small>`Read Only`</small>

### Methods

#### <a name="CLE4538C554"></a>Clear() : void
<small>`Virtual`</small>

Removes all values in the grid, marking everything as unoccupied.

#### <a name="CLEFB28FAFA"></a>ClearValue(in int x, in int y) : void

Clears the assigned valueon this cell of the sparse grid.


#### <a name="CLEA46312EA"></a>ClearValue(in [IntVector](../Heirloom.Math/Heirloom.Math.IntVector.md) co) : void

Clears the assigned valueon this cell of the sparse grid.


#### <a name="HAS1E7B500D"></a>HasValue(in int x, in int y) : bool

Determines if a value has been set on this cell of the sparse grid.


#### <a name="HAS93C63DAD"></a>HasValue(in [IntVector](../Heirloom.Math/Heirloom.Math.IntVector.md) co) : bool

Determines if a value has been set on this cell of the sparse grid.


#### <a name="ISVB586DBEE"></a>IsValidCoordinate(in int x, in int y) : bool

Is the specified coordinate valid on this grid?


#### <a name="ISVCA356546"></a>IsValidCoordinate(in [IntVector](../Heirloom.Math/Heirloom.Math.IntVector.md) co) : bool

Is the specified coordinate valid on this grid?


