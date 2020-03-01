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

| Properties            | Summary |
|-----------------------|---------|
| [Count](#COU73CA0BBB) |         |

| Methods                       | Summary |
|-------------------------------|---------|
| [Clear](#CLE3BB23EF9)         |         |
| [Add](#ADDBCD0F225)           |         |
| [Update](#UPDD1771A75)        |         |
| [Remove](#REMF10744DE)        |         |
| [Contains](#COND0AE797B)      |         |
| [Query](#QUEF3BF732)          |         |
| [Query](#QUEF3BF732)          |         |
| [Query](#QUEF3BF732)          |         |
| [GetEnumerator](#GETF1F90828) |         |

### Constructors

#### LinearSpatialCollection()

### Properties

#### <a name="COU73CA0BBB"></a>Count : int

<small>`Read Only`</small>

### Methods

#### <a name="CLE4538C554"></a>Clear() : void
<small>`Virtual`</small>

#### <a name="ADD873258A8"></a>Add(in T item, in [IShape](../Heirloom.Math/Heirloom.Math.IShape.md) boundingShape) : void


#### <a name="UPDC8E3D6DE"></a>Update(in T item, in [IShape](../Heirloom.Math/Heirloom.Math.IShape.md) boundingShape) : void


#### <a name="REM1E1AE509"></a>Remove(in T item) : bool


#### <a name="CONC6E9849A"></a>Contains(in T item) : bool


#### <a name="QUEA1C7943F"></a>Query([IShape](../Heirloom.Math/Heirloom.Math.IShape.md) queryShape) : IEnumerable\<T>
<small>`Virtual`</small>


#### <a name="QUEF49A009C"></a>Query([Vector](../Heirloom.Math/Heirloom.Math.Vector.md) point) : IEnumerable\<T>
<small>`Virtual`</small>


#### <a name="QUEC52C85A8"></a>Query([Ray](../Heirloom.Math/Heirloom.Math.Ray.md) ray, float maxDistance = ∞) : IEnumerable\<T>
<small>`Virtual`</small>


#### <a name="GETDDD17E2E"></a>GetEnumerator() : IEnumerator\<T>
<small>`Virtual`</small>

