# Heirloom.Collections.Spatial

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Collections.Spatial](../Heirloom.Collections.Spatial/Heirloom.Collections.Spatial.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md)</small>  

## ISpatialCollection\<T> (Interface)
<small>**Namespace**: Heirloom.Collections.Spatial</sub></small>  
<small>**Interfaces**: [IReadOnlySpatialCollection\<T>](Heirloom.Collections.Spatial.IReadOnlySpatialCollection[T].md), [ISpatialQuery\<T>](Heirloom.Collections.Spatial.ISpatialQuery[T].md), IReadOnlyCollection\<T>, IEnumerable\<T>, IEnumerable</small>  

A spatial collection to store and query elements in 2D space.

| Methods | Summary |
|---------|---------|
| [Clear](#CLE4538C554) | Clears all elements from this spatial collection. |
| [Add](#ADD873258A8) | Adds an element with rectangle bounds into this spatial collection. |
| [Update](#UPDC8E3D6DE) | Updates an exising element with new bounds in the collection. |
| [Remove](#REM1E1AE509) | Removes an element from this spatial collection. |

### Methods

#### <a name="CLE4538C554"></a>Clear() : void

<small>`Abstract`</small>

Clears all elements from this spatial collection.

#### <a name="ADD873258A8"></a>Add(in T item, in [IShape](../Heirloom.Math/Heirloom.Math.IShape.md) boundingShape) : void

<small>`Abstract`</small>

Adds an element with rectangle bounds into this spatial collection.


#### <a name="UPDC8E3D6DE"></a>Update(in T item, in [IShape](../Heirloom.Math/Heirloom.Math.IShape.md) boundingShape) : void

<small>`Abstract`</small>

Updates an exising element with new bounds in the collection.


#### <a name="REM1E1AE509"></a>Remove(in T item) : bool

<small>`Abstract`</small>

Removes an element from this spatial collection.


