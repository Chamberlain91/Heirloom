# Mesh

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  
> **Namespace**: [Heirloom][0]  

```cs
public sealed class Mesh
```

--------------------------------------------------------------------------------

**Properties**: [Vertices][1], [Indices][2], [IsIndexed][3], [Version][4]

**Methods**: [Clear][5], [AddVertex][6], [AddVertices][7], [AddIndex][8], [AddIndices][9]

**Static Methods**: [CreateFromPolygon][10], [CreateFromConvexPolygon][11], [CreateQuad][12]

--------------------------------------------------------------------------------

## Constructors

### Mesh()

```cs
public Mesh()
```

## Properties

| Name           | Summary                                                                               |
|----------------|---------------------------------------------------------------------------------------|
| [Vertices][1]  | Gets the vertices contained by this mesh.                                             |
| [Indices][2]   | Gets the (optional) indices defining triangles by index of the vertex list.           |
| [IsIndexed][3] | Is this mesh constructed triangles specified by indexed?                              |
| [Version][4]   | The version number of the mesh. Modifications to the mesh data increment this number. |

## Methods

| Name                          | Summary                                                                                                                                   |
|-------------------------------|-------------------------------------------------------------------------------------------------------------------------------------------|
| [Clear][5]                    | Clears the mesh data.                                                                                                                     |
| [AddVertex][6]                | Appends a vertex to this mesh.                                                                                                            |
| [AddVertices][7]              | Appends multiple vertices to this mesh.                                                                                                   |
| [AddVertices][7]              |                                                                                                                                           |
| [AddIndex][8]                 | Appends a triangle index to this mesh. Until [Clear][5] is called, this mesh becomes indexed.                                             |
| [AddIndices][9]               |                                                                                                                                           |
| [AddIndices][9]               | Appends a triangle index to this mesh. Until [Clear][5] is called, this mesh becomes indexed.                                             |
| [CreateFromPolygon][10]       | Constructs a mesh from the given polygon via [Polygon.Triangulate][13] . UV coordinates are the normalized polygon within its own bounds. |
| [CreateFromPolygon][10]       | Constructs a mesh from the given polygon via [Polygon.Triangulate][13] . UV coordinates are the normalized polygon within its own bounds. |
| [CreateFromConvexPolygon][11] | Constructs a mesh from the given convex polygon. UV coordinates are the normalized polygon within its own bounds.                         |
| [CreateQuad][12]              | Creates a simple quad mesh.                                                                                                               |

[0]: ..\Heirloom.Core.md
[1]: Heirloom.Mesh.Vertices.md
[2]: Heirloom.Mesh.Indices.md
[3]: Heirloom.Mesh.IsIndexed.md
[4]: Heirloom.Mesh.Version.md
[5]: Heirloom.Mesh.Clear.md
[6]: Heirloom.Mesh.AddVertex.md
[7]: Heirloom.Mesh.AddVertices.md
[8]: Heirloom.Mesh.AddIndex.md
[9]: Heirloom.Mesh.AddIndices.md
[10]: Heirloom.Mesh.CreateFromPolygon.md
[11]: Heirloom.Mesh.CreateFromConvexPolygon.md
[12]: Heirloom.Mesh.CreateQuad.md
[13]: Heirloom.Polygon.Triangulate.md
