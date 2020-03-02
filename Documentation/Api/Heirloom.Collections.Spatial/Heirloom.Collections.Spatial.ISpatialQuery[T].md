# Heirloom.Collections.Spatial

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Collections.Spatial](../Heirloom.Collections.Spatial/Heirloom.Collections.Spatial.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md)</small>  

## ISpatialQuery\<T> (Interface)
<small>**Namespace**: Heirloom.Collections.Spatial</small>  

Provides methods for querying elements in 2D space.

| Methods               | Summary                                                      |
|-----------------------|--------------------------------------------------------------|
| [Query](#QUEF49A009C) | Finds spatial elements that overlap the specified point.     |
| [Query](#QUEA1C7943F) | Finds spatial elements that overlap the specified rectangle. |
| [Query](#QUEC52C85A8) | Finds spatial elements that intersect the specified ray.     |

### Methods

#### <a name="QUEF49A009C"></a>Query([Vector](../Heirloom.Math/Heirloom.Math.Vector.md) point) : IEnumerable\<T>
<small>`Abstract`</small>

Finds spatial elements that overlap the specified point.


#### <a name="QUEA1C7943F"></a>Query([IShape](../Heirloom.Math/Heirloom.Math.IShape.md) queryShape) : IEnumerable\<T>
<small>`Abstract`</small>

Finds spatial elements that overlap the specified rectangle.


#### <a name="QUEC52C85A8"></a>Query([Ray](../Heirloom.Math/Heirloom.Math.Ray.md) ray, float maxDistance = ∞) : IEnumerable\<T>
<small>`Abstract`</small>

Finds spatial elements that intersect the specified ray.


