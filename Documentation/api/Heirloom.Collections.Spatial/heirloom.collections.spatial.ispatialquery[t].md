# Heirloom.Collections.Spatial

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Collections.Spatial](../heirloom.collections.spatial/heirloom.collections.spatial.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md)</small>  

## ISpatialQuery\<T> (Interface)
<small>**Namespace**: Heirloom.Collections.Spatial</sub></small>  

Provides methods for querying elements in 2D space.

| Methods | Summary |
|---------|---------|
| [Query](#QUEFD039B7C) | Finds spatial elements that overlap the specified point. |
| [Query](#QUEA2EC9ABF) | Finds spatial elements that overlap the specified rectangle. |
| [Query](#QUE2DAE33C8) | Finds spatial elements that intersect the specified ray. |

### Methods

#### <a name="QUEFD039B7C"></a>Query([Vector](../heirloom.math/heirloom.math.vector.md) point) : IEnumerable\<T>

<small>`Abstract`</small>

Finds spatial elements that overlap the specified point.


#### <a name="QUEA2EC9ABF"></a>Query([IShape](../heirloom.math/heirloom.math.ishape.md) queryShape) : IEnumerable\<T>

<small>`Abstract`</small>

Finds spatial elements that overlap the specified rectangle.


#### <a name="QUE2DAE33C8"></a>Query([Ray](../heirloom.math/heirloom.math.ray.md) ray, float maxDistance = ∞) : IEnumerable\<T>

<small>`Abstract`</small>

Finds spatial elements that intersect the specified ray.


