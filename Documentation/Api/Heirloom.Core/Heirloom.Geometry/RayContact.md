# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## RayContact (Struct)

> **Namespace**: [Heirloom.Geometry][0]

Represents the result of a ray-shape intersection.

```cs
public struct RayContact : IEquatable<RayContact>
```

`IsReadOnlyAttribute`

### Inherits

IEquatable\<RayContact>

### Fields

[Distance][1], [Normal][2], [Position][3]

## Fields

#### Instance

| Name          | Type        | Summary                                            |
|---------------|-------------|----------------------------------------------------|
| [Distance][1] | `float`     | The separating distance from the point of contact. |
| [Normal][2]   | [Vector][4] | The normal direction of the contacted surface.     |
| [Position][3] | [Vector][4] | The point of contact from a collision or ray.      |

[0]: ../../Heirloom.Core.md
[1]: RayContact/Distance.md
[2]: RayContact/Normal.md
[3]: RayContact/Position.md
[4]: ../Heirloom/Vector.md
