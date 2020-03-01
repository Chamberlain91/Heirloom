# Heirloom.Collections.Spatial

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Collections.Spatial](../heirloom.collections.spatial/heirloom.collections.spatial.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md)</small>  

## LinearSpatialCollection\<T> (Sealed Class)
<small>**Namespace**: Heirloom.Collections.Spatial</sub></small>  
<small>**Interfaces**: [ISpatialCollection\<T>](heirloom.collections.spatial.ispatialcollection[t].md), [IReadOnlySpatialCollection\<T>](heirloom.collections.spatial.ireadonlyspatialcollection[t].md), [ISpatialQuery\<T>](heirloom.collections.spatial.ispatialquery[t].md), IReadOnlyCollection\<T>, IEnumerable\<T>, IEnumerable</small>  

DO NOT USE!   
 This is incredibly slow, but useful for behaviour testing against more complex implementions of [ISpatialCollection\<T>](heirloom.collections.spatial.ispatialcollection[t].md).   
 It is effectively implemented as list of shapes and does not operate on any spatial structure.

| Properties | Summary |
|------------|---------|
| [Count](#COU73CA0BBB) |  |

| Methods | Summary |
|---------|---------|
| [Clear](#CLE4538C554) |  |
| [Add](#ADD25BF1CA8) |  |
| [Update](#UPD7EDDE81E) |  |
| [Remove](#REM1E1AE509) |  |
| [Contains](#CONC6E9849A) |  |
| [Query](#QUEA2EC9ABF) |  |
| [Query](#QUEFD039B7C) |  |
| [Query](#QUE2DAE33C8) |  |
| [GetEnumerator](#GETDDD17E2E) |  |

### Constructors

#### LinearSpatialCollection()

### Properties

#### <a name="COU73CA0BBB"></a>Count : int

<small>`Read Only`</small>

### Methods

#### <a name="CLE4538C554"></a>Clear() : void

<small>`Virtual`</small>

#### <a name="ADD25BF1CA8"></a>Add(in T item, in [IShape](../heirloom.math/heirloom.math.ishape.md) boundingShape) : void



#### <a name="UPD7EDDE81E"></a>Update(in T item, in [IShape](../heirloom.math/heirloom.math.ishape.md) boundingShape) : void



#### <a name="REM1E1AE509"></a>Remove(in T item) : bool



#### <a name="CONC6E9849A"></a>Contains(in T item) : bool



#### <a name="QUEA2EC9ABF"></a>Query([IShape](../heirloom.math/heirloom.math.ishape.md) queryShape) : IEnumerable\<T>

<small>`Virtual`</small>


#### <a name="QUEFD039B7C"></a>Query([Vector](../heirloom.math/heirloom.math.vector.md) point) : IEnumerable\<T>

<small>`Virtual`</small>


#### <a name="QUE2DAE33C8"></a>Query([Ray](../heirloom.math/heirloom.math.ray.md) ray, float maxDistance = ∞) : IEnumerable\<T>

<small>`Virtual`</small>


#### <a name="GETDDD17E2E"></a>GetEnumerator() : IEnumerator\<T>

<small>`Virtual`</small>

