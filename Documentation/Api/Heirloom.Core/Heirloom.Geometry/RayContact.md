# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## RayContact (Struct)

> **Namespace**: [Heirloom.Geometry][0]

Represents the result of a ray to shape intersection.

```cs
public struct RayContact : IEquatable<RayContact>
```

`IsReadOnlyAttribute`

### Inherits

IEquatable\<RayContact>

### Fields

[Distance][1], [Normal][2], [Position][3]

### Methods

[Equals][4], [GetHashCode][5]

## Fields

#### Instance

| Name          | Type        | Summary                                            |
|---------------|-------------|----------------------------------------------------|
| [Distance][1] | `float`     | The separating distance from the point of contact. |
| [Normal][2]   | [Vector][6] | The normal direction of the contacted surface.     |
| [Position][3] | [Vector][6] | The point of contact from a collision or ray.      |

## Methods

#### Instance

| Name                    | Return Type | Summary                                                         |
|-------------------------|-------------|-----------------------------------------------------------------|
| [Equals(object)][4]     | `bool`      | Compares this RayContact for equality with another object.      |
| [Equals(RayContact)][4] | `bool`      | Compares this RayContact for equality with another RayContact . |
| [GetHashCode()][5]      | `int`       | Returns the hash code for this instance.                        |

## Operators

| Name                           | Return Type | Summary                                              |
|--------------------------------|-------------|------------------------------------------------------|
| [Equality(RayContact, R...][7] | `bool`      | Compares two instances of RayContact for equality.   |
| [Inequality(RayContact,...][8] | `bool`      | Compares two instances of RayContact for inequality. |

[0]: ../../Heirloom.Core.md
[1]: RayContact/Distance.md
[2]: RayContact/Normal.md
[3]: RayContact/Position.md
[4]: RayContact/Equals.md
[5]: RayContact/GetHashCode.md
[6]: ../Heirloom/Vector.md
[7]: RayContact/op_Equality.md
[8]: RayContact/op_Inequality.md
