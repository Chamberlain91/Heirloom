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

[Indices][1], [IsIndexed][2], [Version][3], [Vertices][4]

### Methods

[AddIndex][5], [AddIndices][6], [AddVertex][7], [AddVertices][8], [Clear][9]

### Static Methods

[CreateFromConvexPolygon][10], [CreateFromPolygon][11], [CreateQuad][12]

## Properties

#### Instance

| Name           | Type                    | Summary                                                                |
|----------------|-------------------------|------------------------------------------------------------------------|
| [Indices][1]   | `IReadOnlyList<int>`    | Gets the (optional) indices defining triangles by index of the vert... |
| [IsIndexed][2] | `bool`                  | Is this mesh constructed triangles specified by indexed?               |
| [Version][3]   | `uint`                  | The version number of the mesh. Modifications to the mesh data incr... |
| [Vertices][4]  | `IReadOnlyList<Vertex>` | Gets the vertices contained by this mesh.                              |

## Methods

#### Instance

| Name                           | Return Type | Summary                                                                |
|--------------------------------|-------------|------------------------------------------------------------------------|
| [AddIndex(int)][5]             | `void`      | Appends a triangle index to this mesh. Until Clear is called, this ... |
| [AddIndices(params int[])][6]  | `void`      |                                                                        |
| [AddIndices(IEnumerable...][6] | `void`      | Appends a triangle index to this mesh. Until Clear is called, this ... |
| [AddVertex(Vertex)][7]         | `void`      | Appends a vertex to this mesh.                                         |
| [AddVertices(IEnumerabl...][8] | `void`      | Appends multiple vertices to this mesh.                                |
| [AddVertices(params Ver...][8] | `void`      |                                                                        |
| [Clear()][9]                   | `void`      | Clears the mesh data.                                                  |

#### Static

| Name                            | Return Type | Summary                                                                |
|---------------------------------|-------------|------------------------------------------------------------------------|
| [CreateFromConvexPolygo...][10] | [Mesh][13]  | Constructs a mesh from the given convex polygon. UV coordinates are... |
| [CreateFromPolygon(Poly...][11] | [Mesh][13]  | Constructs a mesh from the given polygon via Polygon.Triangulate . ... |
| [CreateFromPolygon(IRea...][11] | [Mesh][13]  | Constructs a mesh from the given polygon via Polygon.Triangulate . ... |
| [CreateQuad(float, float)][12]  | [Mesh][13]  | Creates a simple quad mesh.                                            |

[0]: ../../Heirloom.Core.md
[1]: Mesh/Indices.md
[2]: Mesh/IsIndexed.md
[3]: Mesh/Version.md
[4]: Mesh/Vertices.md
[5]: Mesh/AddIndex.md
[6]: Mesh/AddIndices.md
[7]: Mesh/AddVertex.md
[8]: Mesh/AddVertices.md
[9]: Mesh/Clear.md
[10]: Mesh/CreateFromConvexPolygon.md
[11]: Mesh/CreateFromPolygon.md
[12]: Mesh/CreateQuad.md
[13]: Mesh.md
