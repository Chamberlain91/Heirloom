# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## GeometryTools.Triangulate (Method)

> **Namespace**: [Heirloom.Geometry][0]  
> **Declaring Type**: [GeometryTools][1]

### Triangulate(IEnumerable<Vector>)

Constructs the Delaunay triangulation of a set of points.

```cs
public static List<Triangle> Triangulate(IEnumerable<Vector> points)
```

`ExtensionAttribute`

| Name   | Type                   | Summary |
|--------|------------------------|---------|
| points | `IEnumerable\<Vector>` |         |

> **Returns** - `List\<Triangle>`

### Triangulate(IEnumerable<Vector>, List<Triangle>)

Constructs the Delaunay triangulation of a set of points.

```cs
public static void Triangulate(IEnumerable<Vector> points, List<Triangle> triangles)
```

`ExtensionAttribute`

| Name      | Type                   | Summary |
|-----------|------------------------|---------|
| points    | `IEnumerable\<Vector>` |         |
| triangles | `List\<Triangle>`      |         |

> **Returns** - `void`

[0]: ../../../Heirloom.Core.md
[1]: ../GeometryTools.md
