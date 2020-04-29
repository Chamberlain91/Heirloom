# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Mesh.CreateFromConvexPolygon (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Mesh][1]

### CreateFromConvexPolygon(IEnumerable<Vector>)

Constructs a mesh from the given convex polygon.   
 UV coordinates are the normalized polygon within its own bounds.

```cs
public static Mesh CreateFromConvexPolygon(IEnumerable<Vector> polygon)
```

| Name    | Type                   | Summary              |
|---------|------------------------|----------------------|
| polygon | `IEnumerable\<Vector>` | Some convex polygon. |

> **Returns** - [Mesh][1] - A new mesh representign the 'filled' space of the polygon.

[0]: ../../../Heirloom.Core.md
[1]: ../Mesh.md
