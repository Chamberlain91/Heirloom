# Heirloom.Collections.Spatial

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Collections.Spatial](../heirloom.collections.spatial/heirloom.collections.spatial.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md)</small>  

## GridUtilities (Static Class)
<small>**Namespace**: Heirloom.Collections.Spatial</sub></small>  

Provides extra utilities for interacting with a grid.

| Methods | Summary |
|---------|---------|
| [GetNeighbors<T>](#GET353AB362) | Gets the specified cell's neighbors. |
| [GetNeighbors<T>](#GETCEAE118D) | Gets the specified cell's neighbors. |
| [GetNeighborCoordinates<T>](#GETE0BC9D4D) | Gets the specified cell's neighbor coordinates. |
| [GetNeighborCoordinates<T>](#GETC3FA544E) | Gets the specified cell's neighbor coordinates. |
| [GetNeighborCoordinates](#GET218584CB) | Gets neighboring grid coordinates relative to the specified input coordinates. |
| [GetNeighborCoordinates](#GETF8B9701E) | Gets neighboring grid coordinates relative to the specified input coordinates. |

### Methods

#### <a name="GET353AB362"></a>GetNeighbors<T>([IGrid\<T>](heirloom.collections.spatial.igrid[t].md) grid, [IntVector](../heirloom.math/heirloom.math.intvector.md) co, [GridNeighborType](heirloom.collections.spatial.gridneighbortype.md) neighborType = 0) : IEnumerable\<T>

<small>`Static`, `ExtensionAttribute`</small>

Gets the specified cell's neighbors.


#### <a name="GETCEAE118D"></a>GetNeighbors<T>([IGrid\<T>](heirloom.collections.spatial.igrid[t].md) grid, int x, int y, [GridNeighborType](heirloom.collections.spatial.gridneighbortype.md) neighborType = 0) : IEnumerable\<T>

<small>`Static`, `IteratorStateMachineAttribute`, `ExtensionAttribute`</small>

Gets the specified cell's neighbors.


#### <a name="GETE0BC9D4D"></a>GetNeighborCoordinates<T>([IGrid\<T>](heirloom.collections.spatial.igrid[t].md) grid, int x, int y, [GridNeighborType](heirloom.collections.spatial.gridneighbortype.md) neighborType = 0) : IEnumerable\<IntVector>

<small>`Static`, `IteratorStateMachineAttribute`, `ExtensionAttribute`</small>

Gets the specified cell's neighbor coordinates.


#### <a name="GETC3FA544E"></a>GetNeighborCoordinates<T>([IGrid\<T>](heirloom.collections.spatial.igrid[t].md) grid, [IntVector](../heirloom.math/heirloom.math.intvector.md) co, [GridNeighborType](heirloom.collections.spatial.gridneighbortype.md) neighborType = 0) : IEnumerable\<IntVector>

<small>`Static`, `ExtensionAttribute`</small>

Gets the specified cell's neighbor coordinates.


#### <a name="GET218584CB"></a>GetNeighborCoordinates([IntVector](../heirloom.math/heirloom.math.intvector.md) co, [GridNeighborType](heirloom.collections.spatial.gridneighbortype.md) neighborType) : IEnumerable\<IntVector>

<small>`Static`</small>

Gets neighboring grid coordinates relative to the specified input coordinates.


#### <a name="GETF8B9701E"></a>GetNeighborCoordinates(int x, int y, [GridNeighborType](heirloom.collections.spatial.gridneighbortype.md) neighborType) : IEnumerable\<IntVector>

<small>`Static`, `IteratorStateMachineAttribute`</small>

Gets neighboring grid coordinates relative to the specified input coordinates.


