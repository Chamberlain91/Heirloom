# Heirloom.Collections.Spatial

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Collections.Spatial](../Heirloom.Collections.Spatial/Heirloom.Collections.Spatial.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md)</small>  

## GridUtilities (Static Class)
<small>**Namespace**: Heirloom.Collections.Spatial</sub></small>  

Provides extra utilities for interacting with a grid.

| Methods                                | Summary                                                                        |
|----------------------------------------|--------------------------------------------------------------------------------|
| [GetNeighbors<T>](#GETN28C4)           | Gets the specified cell's neighbors.                                           |
| [GetNeighbors<T>](#GETN28C4)           | Gets the specified cell's neighbors.                                           |
| [GetNeighborCoordinates<T>](#GETN861B) | Gets the specified cell's neighbor coordinates.                                |
| [GetNeighborCoordinates<T>](#GETN861B) | Gets the specified cell's neighbor coordinates.                                |
| [GetNeighborCoordinates](#GETNC409)    | Gets neighboring grid coordinates relative to the specified input coordinates. |
| [GetNeighborCoordinates](#GETNC409)    | Gets neighboring grid coordinates relative to the specified input coordinates. |

### Methods

#### <a name="GETN4A71"></a> GetNeighbors<T>([IGrid\<T>](Heirloom.Collections.Spatial.IGrid[T].md) grid, [IntVector](../Heirloom.Math/Heirloom.Math.IntVector.md) co, [GridNeighborType](Heirloom.Collections.Spatial.GridNeighborType.md) neighborType = Axis) : IEnumerable\<T>
<small>`Static`, `ExtensionAttribute`</small>

Gets the specified cell's neighbors.


#### <a name="GETN6537"></a> GetNeighbors<T>([IGrid\<T>](Heirloom.Collections.Spatial.IGrid[T].md) grid, int x, int y, [GridNeighborType](Heirloom.Collections.Spatial.GridNeighborType.md) neighborType = Axis) : IEnumerable\<T>
<small>`Static`, `IteratorStateMachineAttribute`, `ExtensionAttribute`</small>

Gets the specified cell's neighbors.


#### <a name="GETNE578"></a> GetNeighborCoordinates<T>([IGrid\<T>](Heirloom.Collections.Spatial.IGrid[T].md) grid, int x, int y, [GridNeighborType](Heirloom.Collections.Spatial.GridNeighborType.md) neighborType = Axis) : IEnumerable\<IntVector>
<small>`Static`, `IteratorStateMachineAttribute`, `ExtensionAttribute`</small>

Gets the specified cell's neighbor coordinates.


#### <a name="GETN5E76"></a> GetNeighborCoordinates<T>([IGrid\<T>](Heirloom.Collections.Spatial.IGrid[T].md) grid, [IntVector](../Heirloom.Math/Heirloom.Math.IntVector.md) co, [GridNeighborType](Heirloom.Collections.Spatial.GridNeighborType.md) neighborType = Axis) : IEnumerable\<IntVector>
<small>`Static`, `ExtensionAttribute`</small>

Gets the specified cell's neighbor coordinates.


#### <a name="GETNB532"></a> GetNeighborCoordinates([IntVector](../Heirloom.Math/Heirloom.Math.IntVector.md) co, [GridNeighborType](Heirloom.Collections.Spatial.GridNeighborType.md) neighborType) : IEnumerable\<IntVector>
<small>`Static`</small>

Gets neighboring grid coordinates relative to the specified input coordinates.


#### <a name="GETNFFBC"></a> GetNeighborCoordinates(int x, int y, [GridNeighborType](Heirloom.Collections.Spatial.GridNeighborType.md) neighborType) : IEnumerable\<IntVector>
<small>`Static`, `IteratorStateMachineAttribute`</small>

Gets neighboring grid coordinates relative to the specified input coordinates.


