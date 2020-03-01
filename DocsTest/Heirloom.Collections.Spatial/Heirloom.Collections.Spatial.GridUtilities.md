# Heirloom.Collections.Spatial

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Collections.Spatial](../Heirloom.Collections.Spatial/Heirloom.Collections.Spatial.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md)</small>  

## GridUtilities (Static Class)
<small>**Namespace**: Heirloom.Collections.Spatial</sub></small>  

Provides extra utilities for interacting with a grid.

| Methods | Summary |
|---------|---------|
| [GetNeighbors<T>](#GETF9E41AA2) | Gets the specified cell's neighbors. |
| [GetNeighbors<T>](#GET44C2288D) | Gets the specified cell's neighbors. |
| [GetNeighborCoordinates<T>](#GETCCCEB44D) | Gets the specified cell's neighbor coordinates. |
| [GetNeighborCoordinates<T>](#GET7098F10E) | Gets the specified cell's neighbor coordinates. |
| [GetNeighborCoordinates](#GETB5329BCB) | Gets neighboring grid coordinates relative to the specified input coordinates. |
| [GetNeighborCoordinates](#GETFFBC511E) | Gets neighboring grid coordinates relative to the specified input coordinates. |

### Methods

#### <a name="GETF9E41AA2"></a>GetNeighbors<T>([IGrid\<T>](Heirloom.Collections.Spatial.IGrid[T].md) grid, [IntVector](../Heirloom.Math/Heirloom.Math.IntVector.md) co, [GridNeighborType](Heirloom.Collections.Spatial.GridNeighborType.md) neighborType = 0) : IEnumerable\<T>

<small>`Static`, `ExtensionAttribute`</small>

Gets the specified cell's neighbors.


#### <a name="GET44C2288D"></a>GetNeighbors<T>([IGrid\<T>](Heirloom.Collections.Spatial.IGrid[T].md) grid, int x, int y, [GridNeighborType](Heirloom.Collections.Spatial.GridNeighborType.md) neighborType = 0) : IEnumerable\<T>

<small>`Static`, `IteratorStateMachineAttribute`, `ExtensionAttribute`</small>

Gets the specified cell's neighbors.


#### <a name="GETCCCEB44D"></a>GetNeighborCoordinates<T>([IGrid\<T>](Heirloom.Collections.Spatial.IGrid[T].md) grid, int x, int y, [GridNeighborType](Heirloom.Collections.Spatial.GridNeighborType.md) neighborType = 0) : IEnumerable\<IntVector>

<small>`Static`, `IteratorStateMachineAttribute`, `ExtensionAttribute`</small>

Gets the specified cell's neighbor coordinates.


#### <a name="GET7098F10E"></a>GetNeighborCoordinates<T>([IGrid\<T>](Heirloom.Collections.Spatial.IGrid[T].md) grid, [IntVector](../Heirloom.Math/Heirloom.Math.IntVector.md) co, [GridNeighborType](Heirloom.Collections.Spatial.GridNeighborType.md) neighborType = 0) : IEnumerable\<IntVector>

<small>`Static`, `ExtensionAttribute`</small>

Gets the specified cell's neighbor coordinates.


#### <a name="GETB5329BCB"></a>GetNeighborCoordinates([IntVector](../Heirloom.Math/Heirloom.Math.IntVector.md) co, [GridNeighborType](Heirloom.Collections.Spatial.GridNeighborType.md) neighborType) : IEnumerable\<IntVector>

<small>`Static`</small>

Gets neighboring grid coordinates relative to the specified input coordinates.


#### <a name="GETFFBC511E"></a>GetNeighborCoordinates(int x, int y, [GridNeighborType](Heirloom.Collections.Spatial.GridNeighborType.md) neighborType) : IEnumerable\<IntVector>

<small>`Static`, `IteratorStateMachineAttribute`</small>

Gets neighboring grid coordinates relative to the specified input coordinates.


