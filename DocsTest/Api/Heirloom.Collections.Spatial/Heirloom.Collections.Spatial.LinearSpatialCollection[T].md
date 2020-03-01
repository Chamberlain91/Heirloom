# Heirloom.Collections.Spatial

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Collections.Spatial](../Heirloom.Collections.Spatial/Heirloom.Collections.Spatial.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md)</small>  

## LinearSpatialCollection\<T> (Sealed Class)
<small>**Namespace**: Heirloom.Collections.Spatial</sub></small>  
<small>**Interfaces**: [ISpatialCollection\<T>](Heirloom.Collections.Spatial.ISpatialCollection[T].md), [IReadOnlySpatialCollection\<T>](Heirloom.Collections.Spatial.IReadOnlySpatialCollection[T].md), [ISpatialQuery\<T>](Heirloom.Collections.Spatial.ISpatialQuery[T].md), IReadOnlyCollection\<T>, IEnumerable\<T>, IEnumerable</small>  

DO NOT USE!   
 This is incredibly slow, but useful for behaviour testing against more complex implementions of [ISpatialCollection\<T>](Heirloom.Collections.Spatial.ISpatialCollection[T].md).   
 It is effectively implemented as list of shapes and does not operate on any spatial structure.

| Properties         | Summary |
|--------------------|---------|
| [Count](#COUN73CA) |         |

| Methods                    | Summary |
|----------------------------|---------|
| [Clear](#CLEA3BB2)         |         |
| [Add](#ADDBCD0)            |         |
| [Update](#UPDAD177)        |         |
| [Remove](#REMOF107)        |         |
| [Contains](#CONTD0AE)      |         |
| [Query](#QUERF3BF)         |         |
| [Query](#QUERF3BF)         |         |
| [Query](#QUERF3BF)         |         |
| [GetEnumerator](#GETEF1F9) |         |

### Constructors

#### LinearSpatialCollection()

### Properties

#### <a name="COUN73CA"></a> Count : int

<small>`Read Only`</small>

### Methods

#### <a name="CLEA4538"></a> Clear() : void
<small>`Virtual`</small>

#### <a name="ADD(8732"></a> Add(in T item, in [IShape](../Heirloom.Math/Heirloom.Math.IShape.md) boundingShape) : void


#### <a name="UPDAC8E3"></a> Update(in T item, in [IShape](../Heirloom.Math/Heirloom.Math.IShape.md) boundingShape) : void


#### <a name="REMO1E1A"></a> Remove(in T item) : bool


#### <a name="CONTC6E9"></a> Contains(in T item) : bool


#### <a name="QUERA1C7"></a> Query([IShape](../Heirloom.Math/Heirloom.Math.IShape.md) queryShape) : IEnumerable\<T>
<small>`Virtual`</small>


#### <a name="QUERF49A"></a> Query([Vector](../Heirloom.Math/Heirloom.Math.Vector.md) point) : IEnumerable\<T>
<small>`Virtual`</small>


#### <a name="QUERC52C"></a> Query([Ray](../Heirloom.Math/Heirloom.Math.Ray.md) ray, float maxDistance = ∞) : IEnumerable\<T>
<small>`Virtual`</small>


#### <a name="GETEDDD1"></a> GetEnumerator() : IEnumerator\<T>
<small>`Virtual`</small>

