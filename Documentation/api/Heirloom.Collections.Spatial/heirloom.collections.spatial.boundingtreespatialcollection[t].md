# Heirloom.Collections.Spatial

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Collections.Spatial](../heirloom.collections.spatial/heirloom.collections.spatial.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md)</small>  

## BoundingTreeSpatialCollection\<T> (Sealed Class)
<small>**Namespace**: Heirloom.Collections.Spatial</sub></small>  
<small>**Interfaces**: [ISpatialCollection\<T>](heirloom.collections.spatial.ispatialcollection[t].md), [IReadOnlySpatialCollection\<T>](heirloom.collections.spatial.ireadonlyspatialcollection[t].md), [ISpatialQuery\<T>](heirloom.collections.spatial.ispatialquery[t].md), IReadOnlyCollection\<T>, IEnumerable\<T>, IEnumerable</small>  

A spatial collection to store and query elements in 2D space, implemented as a BVH style tree and has infinite bounds.

| Properties | Summary |
|------------|---------|
| [Count](#COU73CA0BBB) | Gets the number of elements stored in this collection. |

| Methods | Summary |
|---------|---------|
| [Clear](#CLE4538C554) | Clears all elements from this spatial collection. |
| [Add](#ADD25BF1CA8) | Adds an element with rectangle bounds into this spatial collection. |
| [Update](#UPD7EDDE81E) | Updates an exising element with new bounds in the collection. |
| [Remove](#REM1E1AE509) | Removes an element from this spatial collection. |
| [Contains](#CONC6E9849A) | Determines if the specified element exists in this collection. |
| [Query](#QUEFD039B7C) | Queries the spatial collection and returns the elements with bounds that overlap the specified point. |
| [Query](#QUEA2EC9ABF) | Queries the spatial collection and returns the elements with bounds that overlap the specified rectangle. |
| [Query](#QUE2DAE33C8) | Queries the spatial collection and returns the elements with bounds that intersect the specified ray. |
| [GetEnumerator](#GETDDD17E2E) |  |

### Constructors

#### BoundingTreeSpatialCollection(float margin = 0.1)

### Properties

#### <a name="COU73CA0BBB"></a>Count : int

<small>`Read Only`</small>

Gets the number of elements stored in this collection.

### Methods

#### <a name="CLE4538C554"></a>Clear() : void

<small>`Virtual`</small>

Clears all elements from this spatial collection.

#### <a name="ADD25BF1CA8"></a>Add(in T item, in [IShape](../heirloom.math/heirloom.math.ishape.md) boundingShape) : void


Adds an element with rectangle bounds into this spatial collection.


#### <a name="UPD7EDDE81E"></a>Update(in T item, in [IShape](../heirloom.math/heirloom.math.ishape.md) boundingShape) : void


Updates an exising element with new bounds in the collection.


#### <a name="REM1E1AE509"></a>Remove(in T item) : bool


Removes an element from this spatial collection.


#### <a name="CONC6E9849A"></a>Contains(in T item) : bool


Determines if the specified element exists in this collection.


#### <a name="QUEFD039B7C"></a>Query([Vector](../heirloom.math/heirloom.math.vector.md) point) : IEnumerable\<T>

<small>`Virtual`, `IteratorStateMachineAttribute`</small>

Queries the spatial collection and returns the elements with bounds that overlap the specified point.


#### <a name="QUEA2EC9ABF"></a>Query([IShape](../heirloom.math/heirloom.math.ishape.md) queryShape) : IEnumerable\<T>

<small>`Virtual`, `IteratorStateMachineAttribute`</small>

Queries the spatial collection and returns the elements with bounds that overlap the specified rectangle.


#### <a name="QUE2DAE33C8"></a>Query([Ray](../heirloom.math/heirloom.math.ray.md) ray, float maxDistance = ∞) : IEnumerable\<T>

<small>`Virtual`, `IteratorStateMachineAttribute`</small>

Queries the spatial collection and returns the elements with bounds that intersect the specified ray.


#### <a name="GETDDD17E2E"></a>GetEnumerator() : IEnumerator\<T>

<small>`Virtual`</small>

