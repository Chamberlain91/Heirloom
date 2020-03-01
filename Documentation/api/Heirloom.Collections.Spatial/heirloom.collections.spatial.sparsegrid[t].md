# Heirloom.Collections.Spatial

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Collections.Spatial](../heirloom.collections.spatial/heirloom.collections.spatial.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md)</small>  

## SparseGrid\<T> (Sealed Class)
<small>**Namespace**: Heirloom.Collections.Spatial</sub></small>  
<small>**Interfaces**: [ISparseGrid\<T>](heirloom.collections.spatial.isparsegrid[t].md), [IReadOnlySparseGrid\<T>](heirloom.collections.spatial.ireadonlysparsegrid[t].md), [IReadOnlyGrid\<T>](heirloom.collections.spatial.ireadonlygrid[t].md), [IGrid\<T>](heirloom.collections.spatial.igrid[t].md)</small>  

An infinite, sparse grid of values.

| Properties | Summary |
|------------|---------|
| [Item](#ITE8B5A2F95) |  |
| [Item](#ITE8B5A2F95) |  |
| [Keys](#KEY3D37EC76) |  |

| Methods | Summary |
|---------|---------|
| [Clear](#CLE4538C554) | Removes all values in the grid, marking everything as unoccupied. |
| [ClearValue](#CLEFB28FAFA) | Clears the assigned valueon this cell of the sparse grid. |
| [ClearValue](#CLE20AFDEEA) | Clears the assigned valueon this cell of the sparse grid. |
| [HasValue](#HAS1E7B500D) | Determines if a value has been set on this cell of the sparse grid. |
| [HasValue](#HAS8EE55FAD) | Determines if a value has been set on this cell of the sparse grid. |
| [IsValidCoordinate](#ISVB586DBEE) | Is the specified coordinate valid on this grid? |
| [IsValidCoordinate](#ISV739E9C6) | Is the specified coordinate valid on this grid? |

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


#### <a name="CLE20AFDEEA"></a>ClearValue(in [IntVector](../heirloom.math/heirloom.math.intvector.md) co) : void


Clears the assigned valueon this cell of the sparse grid.


#### <a name="HAS1E7B500D"></a>HasValue(in int x, in int y) : bool


Determines if a value has been set on this cell of the sparse grid.


#### <a name="HAS8EE55FAD"></a>HasValue(in [IntVector](../heirloom.math/heirloom.math.intvector.md) co) : bool


Determines if a value has been set on this cell of the sparse grid.


#### <a name="ISVB586DBEE"></a>IsValidCoordinate(in int x, in int y) : bool


Is the specified coordinate valid on this grid?


#### <a name="ISV739E9C6"></a>IsValidCoordinate(in [IntVector](../heirloom.math/heirloom.math.intvector.md) co) : bool


Is the specified coordinate valid on this grid?


