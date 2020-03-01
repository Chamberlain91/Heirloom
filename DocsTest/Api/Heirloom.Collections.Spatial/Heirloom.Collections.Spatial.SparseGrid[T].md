# Heirloom.Collections.Spatial

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Collections.Spatial](../Heirloom.Collections.Spatial/Heirloom.Collections.Spatial.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md)</small>  

## SparseGrid\<T> (Sealed Class)
<small>**Namespace**: Heirloom.Collections.Spatial</sub></small>  
<small>**Interfaces**: [ISparseGrid\<T>](Heirloom.Collections.Spatial.ISparseGrid[T].md), [IReadOnlySparseGrid\<T>](Heirloom.Collections.Spatial.IReadOnlySparseGrid[T].md), [IReadOnlyGrid\<T>](Heirloom.Collections.Spatial.IReadOnlyGrid[T].md), [IGrid\<T>](Heirloom.Collections.Spatial.IGrid[T].md)</small>  

An infinite, sparse grid of values.

| Properties        | Summary |
|-------------------|---------|
| [Item](#ITEM8B5A) |         |
| [Item](#ITEM8B5A) |         |
| [Keys](#KEYS3D37) |         |

| Methods                        | Summary                                                             |
|--------------------------------|---------------------------------------------------------------------|
| [Clear](#CLEA3BB2)             | Removes all values in the grid, marking everything as unoccupied.   |
| [ClearValue](#CLEA6B29)        | Clears the assigned valueon this cell of the sparse grid.           |
| [ClearValue](#CLEA6B29)        | Clears the assigned valueon this cell of the sparse grid.           |
| [HasValue](#HASVB314)          | Determines if a value has been set on this cell of the sparse grid. |
| [HasValue](#HASVB314)          | Determines if a value has been set on this cell of the sparse grid. |
| [IsValidCoordinate](#ISVA6716) | Is the specified coordinate valid on this grid?                     |
| [IsValidCoordinate](#ISVA6716) | Is the specified coordinate valid on this grid?                     |

### Constructors

#### SparseGrid()

### Properties

#### <a name="ITEM8B5A"></a> Item : T


#### <a name="ITEM8B5A"></a> Item : T


#### <a name="KEYS3D37"></a> Keys : IEnumerable\<IntVector>

<small>`Read Only`</small>

### Methods

#### <a name="CLEA4538"></a> Clear() : void
<small>`Virtual`</small>

Removes all values in the grid, marking everything as unoccupied.

#### <a name="CLEAFB28"></a> ClearValue(in int x, in int y) : void

Clears the assigned valueon this cell of the sparse grid.


#### <a name="CLEAA463"></a> ClearValue(in [IntVector](../Heirloom.Math/Heirloom.Math.IntVector.md) co) : void

Clears the assigned valueon this cell of the sparse grid.


#### <a name="HASV1E7B"></a> HasValue(in int x, in int y) : bool

Determines if a value has been set on this cell of the sparse grid.


#### <a name="HASV93C6"></a> HasValue(in [IntVector](../Heirloom.Math/Heirloom.Math.IntVector.md) co) : bool

Determines if a value has been set on this cell of the sparse grid.


#### <a name="ISVAB586"></a> IsValidCoordinate(in int x, in int y) : bool

Is the specified coordinate valid on this grid?


#### <a name="ISVACA35"></a> IsValidCoordinate(in [IntVector](../Heirloom.Math/Heirloom.Math.IntVector.md) co) : bool

Is the specified coordinate valid on this grid?


