# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## GeometryTools (Class)

> **Namespace**: [Heirloom.Geometry][0]

Provides utilities for generating and manipulating shapes.

```cs
public static class GeometryTools
```

`ExtensionAttribute`

### Static Methods

[GenerateRegularPolygon][1], [GenerateStar][2], [Triangulate][3]

## Methods

| Name                           | Return Type           | Summary                                                   |
|--------------------------------|-----------------------|-----------------------------------------------------------|
| [GenerateRegularPolygon...][1] | `IEnumerable<Vector>` | Generates the points of a regular polygon.                |
| [GenerateRegularPolygon...][1] | `IEnumerable<Vector>` | Generates the points of a regular polygon.                |
| [GenerateStar(float)][2]       | `IEnumerable<Vector>` | Generates the points of a common five-pointed star.       |
| [GenerateStar(Vector, f...][2] | `IEnumerable<Vector>` | Generates the points of a common five-pointed star.       |
| [GenerateStar(int, float)][2]  | `IEnumerable<Vector>` | Generates the points of a star.                           |
| [GenerateStar(Vector, i...][2] | `IEnumerable<Vector>` | Generates the points of a star.                           |
| [GenerateStar(int, floa...][2] | `IEnumerable<Vector>` | Generates the points of a star.                           |
| [GenerateStar(Vector, i...][2] | `IEnumerable<Vector>` | Generates the points of a star.                           |
| [Triangulate(IEnumerabl...][3] | `List<Triangle>`      | Constructs the Delaunay triangulation of a set of points. |
| [Triangulate(IEnumerabl...][3] | `void`                | Constructs the Delaunay triangulation of a set of points. |

[0]: ../../Heirloom.Core.md
[1]: GeometryTools/GenerateRegularPolygon.md
[2]: GeometryTools/GenerateStar.md
[3]: GeometryTools/Triangulate.md
