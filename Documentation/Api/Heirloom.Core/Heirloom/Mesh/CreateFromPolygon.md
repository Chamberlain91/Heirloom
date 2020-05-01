# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Mesh.CreateFromPolygon (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Mesh][1]

### CreateFromPolygon(Polygon)

Constructs a mesh from the given polygon via [Triangulate][2] .   
 UV coordinates are the normalized polygon within its own bounds.

```cs
public static Mesh CreateFromPolygon(Polygon polygon)
```

| Name    | Type         | Summary       |
|---------|--------------|---------------|
| polygon | [Polygon][3] | Some polygon. |

> **Returns** - [Mesh][1] - A new mesh representign the 'filled' space of the polygon.

### CreateFromPolygon(IReadOnlyList<Vector>)

Constructs a mesh from the given polygon via [Triangulate][2] .   
 UV coordinates are the normalized polygon within its own bounds.

```cs
public static Mesh CreateFromPolygon(IReadOnlyList<Vector> polygon)
```

| Name    | Type                     | Summary       |
|---------|--------------------------|---------------|
| polygon | `IReadOnlyList\<Vector>` | Some polygon. |

> **Returns** - [Mesh][1] - A new mesh representign the 'filled' space of the polygon.

[0]: ../../../Heirloom.Core.md
[1]: ../Mesh.md
[2]: ../../Heirloom.Geometry/Polygon/Triangulate.md
[3]: ../../Heirloom.Geometry/Polygon.md
