# RayContact

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  
> **Namespace**: [Heirloom][0]  

Represents the result of a ray-shape intersection.

```cs
public struct RayContact : IEquatable<RayContact>
```

--------------------------------------------------------------------------------

**Inherits**: IEquatable\<RayContact>

**Fields**: [Position][1], [Normal][2], [Distance][3]

--------------------------------------------------------------------------------

## Fields

| Name          | Summary                                            |
|---------------|----------------------------------------------------|
| [Position][1] | The point of contact from a collision or ray.      |
| [Normal][2]   | The normal direction of the contacted surface.     |
| [Distance][3] | The separating distance from the point of contact. |

[0]: ..\Heirloom.Core.md
[1]: Heirloom.RayContact.Position.md
[2]: Heirloom.RayContact.Normal.md
[3]: Heirloom.RayContact.Distance.md
