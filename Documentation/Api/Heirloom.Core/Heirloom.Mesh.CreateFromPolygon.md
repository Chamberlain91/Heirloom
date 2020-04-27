# Mesh.CreateFromPolygon

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  
> **Namespace**: [Heirloom][0]  
> **Type**: [Mesh][1]

--------------------------------------------------------------------------------

### CreateFromPolygon(Polygon)

Constructs a mesh from the given polygon via [Polygon.Triangulate][2] .   
 UV coordinates are the normalized polygon within its own bounds.

```cs
public Mesh CreateFromPolygon(Polygon polygon)
```

### CreateFromPolygon(IReadOnlyList<Vector>)

Constructs a mesh from the given polygon via [Polygon.Triangulate][2] .   
 UV coordinates are the normalized polygon within its own bounds.

```cs
public Mesh CreateFromPolygon(IReadOnlyList<Vector> polygon)
```

[0]: ../Heirloom.Core.md
[1]: Heirloom.Mesh.md
[2]: Heirloom.Polygon.Triangulate.md
