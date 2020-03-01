# Heirloom.Collections.Spatial

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Collections.Spatial](../Heirloom.Collections.Spatial/Heirloom.Collections.Spatial.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md)</small>  

## Grid\<T> (Sealed Class)
<small>**Namespace**: Heirloom.Collections.Spatial</sub></small>  
<small>**Interfaces**: [IFiniteGrid\<T>](Heirloom.Collections.Spatial.IFiniteGrid[T].md), [IGrid\<T>](Heirloom.Collections.Spatial.IGrid[T].md), [IReadOnlyGrid\<T>](Heirloom.Collections.Spatial.IReadOnlyGrid[T].md)</small>  

A finite grid (bounded by size) of values.

| Properties             | Summary                  |
|------------------------|--------------------------|
| [Width](#WID68924896)  | The width of this grid.  |
| [Height](#HEIE098AAEB) | The height of this grid. |
| [Item](#ITE8B5A2F95)   |                          |
| [Item](#ITE8B5A2F95)   |                          |

| Methods                           | Summary                                                                       |
|-----------------------------------|-------------------------------------------------------------------------------|
| [Clear](#CLE4538C554)             | Sets all values in the grid to default for type \<typeparamref name="T" />.   |
| [IsValidCoordinate](#ISVB586DBEE) | Determines if the specified coordinate is a valid coordinate within the grid. |
| [IsValidCoordinate](#ISVCA356546) | Determines if the specified coordinate is a valid coordinate within the grid. |

### Constructors

#### Grid(int width, int height)

### Properties

#### <a name="WID68924896"></a>Width : int

<small>`Read Only`</small>

The width of this grid.

#### <a name="HEIE098AAEB"></a>Height : int

<small>`Read Only`</small>

The height of this grid.

#### <a name="ITE8B5A2F95"></a>Item : T


#### <a name="ITE8B5A2F95"></a>Item : T


### Methods

#### <a name="CLE4538C554"></a>Clear() : void
<small>`Virtual`</small>

Sets all values in the grid to default for type \<typeparamref name="T" />.

#### <a name="ISVB586DBEE"></a>IsValidCoordinate(in int x, in int y) : bool

Determines if the specified coordinate is a valid coordinate within the grid.


#### <a name="ISVCA356546"></a>IsValidCoordinate(in [IntVector](../Heirloom.Math/Heirloom.Math.IntVector.md) co) : bool

Determines if the specified coordinate is a valid coordinate within the grid.


