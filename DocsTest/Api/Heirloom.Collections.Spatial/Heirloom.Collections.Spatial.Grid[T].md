# Heirloom.Collections.Spatial

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Collections.Spatial](../Heirloom.Collections.Spatial/Heirloom.Collections.Spatial.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md)</small>  

## Grid\<T> (Sealed Class)
<small>**Namespace**: Heirloom.Collections.Spatial</sub></small>  
<small>**Interfaces**: [IFiniteGrid\<T>](Heirloom.Collections.Spatial.IFiniteGrid[T].md), [IGrid\<T>](Heirloom.Collections.Spatial.IGrid[T].md), [IReadOnlyGrid\<T>](Heirloom.Collections.Spatial.IReadOnlyGrid[T].md)</small>  

A finite grid (bounded by size) of values.

| Properties          | Summary                  |
|---------------------|--------------------------|
| [Width](#WIDT6892)  | The width of this grid.  |
| [Height](#HEIGE098) | The height of this grid. |
| [Item](#ITEM8B5A)   |                          |
| [Item](#ITEM8B5A)   |                          |

| Methods                        | Summary                                                                       |
|--------------------------------|-------------------------------------------------------------------------------|
| [Clear](#CLEA3BB2)             | Sets all values in the grid to default for type \<typeparamref name="T" />.   |
| [IsValidCoordinate](#ISVA6716) | Determines if the specified coordinate is a valid coordinate within the grid. |
| [IsValidCoordinate](#ISVA6716) | Determines if the specified coordinate is a valid coordinate within the grid. |

### Constructors

#### Grid(int width, int height)

### Properties

#### <a name="WIDT6892"></a> Width : int

<small>`Read Only`</small>

The width of this grid.

#### <a name="HEIGE098"></a> Height : int

<small>`Read Only`</small>

The height of this grid.

#### <a name="ITEM8B5A"></a> Item : T


#### <a name="ITEM8B5A"></a> Item : T


### Methods

#### <a name="CLEA4538"></a> Clear() : void
<small>`Virtual`</small>

Sets all values in the grid to default for type \<typeparamref name="T" />.

#### <a name="ISVAB586"></a> IsValidCoordinate(in int x, in int y) : bool

Determines if the specified coordinate is a valid coordinate within the grid.


#### <a name="ISVACA35"></a> IsValidCoordinate(in [IntVector](../Heirloom.Math/Heirloom.Math.IntVector.md) co) : bool

Determines if the specified coordinate is a valid coordinate within the grid.


