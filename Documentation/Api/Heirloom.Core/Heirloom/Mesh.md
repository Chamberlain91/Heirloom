# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Mesh (Class)

> **Namespace**: [Heirloom][0]

Represents a triangle based mesh.

```cs
public sealed class Mesh
```

### Properties

[Indices][1], [Version][2], [Vertices][3]

### Methods

[AddTriangle][4], [AddTriangles][5], [AddVertex][6], [AddVertices][7], [Clear][8]

### Static Methods

[CreateFromConvexPolygon][9], [CreateFromPolygon][10], [CreateQuad][11]

## Properties

#### Instance

| Name          | Type                    | Summary                                                                |
|---------------|-------------------------|------------------------------------------------------------------------|
| [Indices][1]  | `IReadOnlyList<int>`    | Gets the indices defining triangles by index of the vertex list.       |
| [Version][2]  | `uint`                  | The version number of the mesh. Modifications to the mesh data incr... |
| [Vertices][3] | `IReadOnlyList<Vertex>` | Gets the vertices contained by this mesh.                              |

## Methods

#### Instance

| Name                           | Return Type | Summary                                                          |
|--------------------------------|-------------|------------------------------------------------------------------|
| [AddTriangle(int, int, ...][4] | `void`      | Appends and defines a triangle face to add to this mesh.         |
| [AddTriangles(params in...][5] | `void`      |                                                                  |
| [AddTriangles(IReadOnly...][5] | `void`      | Appends and defines multiple triangle faces to add to this mesh. |
| [AddVertex(Vertex)][6]         | `void`      | Appends a vertex to this mesh.                                   |
| [AddVertices(IEnumerabl...][7] | `void`      | Appends multiple vertices to this mesh.                          |
| [AddVertices(params Ver...][7] | `void`      |                                                                  |
| [Clear()][8]                   | `void`      | Clears the mesh data.                                            |

#### Static

| Name                            | Return Type | Summary                                                                |
|---------------------------------|-------------|------------------------------------------------------------------------|
| [CreateFromConvexPolygo...][9]  | [Mesh][12]  | Constructs a mesh from the given convex polygon. UV coordinates are... |
| [CreateFromPolygon(Poly...][10] | [Mesh][12]  | Constructs a mesh from the given polygon via Polygon.Triangulate . ... |
| [CreateFromPolygon(IRea...][10] | [Mesh][12]  | Constructs a mesh from the given polygon via Polygon.Triangulate . ... |
| [CreateQuad(float, float)][11]  | [Mesh][12]  | Creates a simple quad mesh.                                            |

[0]: ../../Heirloom.Core.md
[1]: Mesh/Indices.md
[2]: Mesh/Version.md
[3]: Mesh/Vertices.md
[4]: Mesh/AddTriangle.md
[5]: Mesh/AddTriangles.md
[6]: Mesh/AddVertex.md
[7]: Mesh/AddVertices.md
[8]: Mesh/Clear.md
[9]: Mesh/CreateFromConvexPolygon.md
[10]: Mesh/CreateFromPolygon.md
[11]: Mesh/CreateQuad.md
[12]: Mesh.md
