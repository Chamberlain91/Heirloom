# Heirloom.Collections.Spatial

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Collections.Spatial](../Heirloom.Collections.Spatial/Heirloom.Collections.Spatial.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md)</small>  

## BoundingTreeSpatialCollection\<T> (Sealed Class)
<small>**Namespace**: Heirloom.Collections.Spatial</sub></small>  
<small>**Interfaces**: [ISpatialCollection\<T>](Heirloom.Collections.Spatial.ISpatialCollection[T].md), [IReadOnlySpatialCollection\<T>](Heirloom.Collections.Spatial.IReadOnlySpatialCollection[T].md), [ISpatialQuery\<T>](Heirloom.Collections.Spatial.ISpatialQuery[T].md), IReadOnlyCollection\<T>, IEnumerable\<T>, IEnumerable</small>  

A spatial collection to store and query elements in 2D space, implemented as a BVH style tree and has infinite bounds.

| Properties         | Summary                                                |
|--------------------|--------------------------------------------------------|
| [Count](#COUN73CA) | Gets the number of elements stored in this collection. |

| Methods                    | Summary                                                                                                   |
|----------------------------|-----------------------------------------------------------------------------------------------------------|
| [Clear](#CLEA3BB2)         | Clears all elements from this spatial collection.                                                         |
| [Add](#ADDBCD0)            | Adds an element with rectangle bounds into this spatial collection.                                       |
| [Update](#UPDAD177)        | Updates an exising element with new bounds in the collection.                                             |
| [Remove](#REMOF107)        | Removes an element from this spatial collection.                                                          |
| [Contains](#CONTD0AE)      | Determines if the specified element exists in this collection.                                            |
| [Query](#QUERF3BF)         | Queries the spatial collection and returns the elements with bounds that overlap the specified point.     |
| [Query](#QUERF3BF)         | Queries the spatial collection and returns the elements with bounds that overlap the specified rectangle. |
| [Query](#QUERF3BF)         | Queries the spatial collection and returns the elements with bounds that intersect the specified ray.     |
| [GetEnumerator](#GETEF1F9) |                                                                                                           |

### Constructors

#### BoundingTreeSpatialCollection(float margin = 0.1)

### Properties

#### <a name="COUN73CA"></a> Count : int

<small>`Read Only`</small>

Gets the number of elements stored in this collection.

### Methods

#### <a name="CLEA4538"></a> Clear() : void
<small>`Virtual`</small>

Clears all elements from this spatial collection.

#### <a name="ADD(8732"></a> Add(in T item, in [IShape](../Heirloom.Math/Heirloom.Math.IShape.md) boundingShape) : void

Adds an element with rectangle bounds into this spatial collection.


#### <a name="UPDAC8E3"></a> Update(in T item, in [IShape](../Heirloom.Math/Heirloom.Math.IShape.md) boundingShape) : void

Updates an exising element with new bounds in the collection.


#### <a name="REMO1E1A"></a> Remove(in T item) : bool

Removes an element from this spatial collection.


#### <a name="CONTC6E9"></a> Contains(in T item) : bool

Determines if the specified element exists in this collection.


#### <a name="QUERF49A"></a> Query([Vector](../Heirloom.Math/Heirloom.Math.Vector.md) point) : IEnumerable\<T>
<small>`Virtual`, `IteratorStateMachineAttribute`</small>

Queries the spatial collection and returns the elements with bounds that overlap the specified point.


#### <a name="QUERA1C7"></a> Query([IShape](../Heirloom.Math/Heirloom.Math.IShape.md) queryShape) : IEnumerable\<T>
<small>`Virtual`, `IteratorStateMachineAttribute`</small>

Queries the spatial collection and returns the elements with bounds that overlap the specified rectangle.


#### <a name="QUERC52C"></a> Query([Ray](../Heirloom.Math/Heirloom.Math.Ray.md) ray, float maxDistance = ∞) : IEnumerable\<T>
<small>`Virtual`, `IteratorStateMachineAttribute`</small>

Queries the spatial collection and returns the elements with bounds that intersect the specified ray.


#### <a name="GETEDDD1"></a> GetEnumerator() : IEnumerator\<T>
<small>`Virtual`</small>

