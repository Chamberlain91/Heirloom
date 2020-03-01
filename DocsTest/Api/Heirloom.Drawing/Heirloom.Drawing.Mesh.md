# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../Heirloom.Drawing/Heirloom.Drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## Mesh (Sealed Class)
<small>**Namespace**: Heirloom.Drawing</sub></small>  

| Properties             | Summary                                                                               |
|------------------------|---------------------------------------------------------------------------------------|
| [Vertices](#VERT648B)  | Gets the vertices contained by this mesh.                                             |
| [Indices](#INDIA2E8)   | Gets the (optional) indices defining triangles by index of the vertex list.           |
| [IsIndexed](#ISIN97FB) | Is this mesh constructed triangles specified by indexed?                              |
| [Version](#VERSFB25)   | The version number of the mesh. Modifications to the mesh data increment this number. |

| Methods                              | Summary                                                                                                                                            |
|--------------------------------------|----------------------------------------------------------------------------------------------------------------------------------------------------|
| [Clear](#CLEA3BB2)                   | Clears the mesh data.                                                                                                                              |
| [AddVertex](#ADDVA40B)               | Appends a vertex to this mesh.                                                                                                                     |
| [AddVertices](#ADDV60DF)             | Appends multiple vertices to this mesh.                                                                                                            |
| [AddVertices](#ADDV60DF)             | Appends multiple vertices to this mesh.                                                                                                            |
| [AddIndex](#ADDIEB34)                | Appends a triangle index to this mesh. Until `Heirloom.Drawing.Mesh.Clear` is called, this mesh becomes indexed.                                   |
| [AddIndices](#ADDIE65D)              | Appends a triangle index to this mesh. Until `Heirloom.Drawing.Mesh.Clear` is called, this mesh becomes indexed.                                   |
| [AddIndices](#ADDIE65D)              | Appends a triangle index to this mesh. Until `Heirloom.Drawing.Mesh.Clear` is called, this mesh becomes indexed.                                   |
| [CreateFromPolygon](#CREAA112)       | Constructs a mesh from the given polygon via `Heirloom.Math.Polygon.Triangulate`. UV coordinates are the normalized polygon within its own bounds. |
| [CreateFromPolygon](#CREAA112)       | Constructs a mesh from the given polygon via `Heirloom.Math.Polygon.Triangulate`. UV coordinates are the normalized polygon within its own bounds. |
| [CreateFromConvexPolygon](#CREA9A01) | Constructs a mesh from the given convex polygon. UV coordinates are the normalized polygon within its own bounds.                                  |
| [CreateQuad](#CREAC597)              | Creates a simple quad mesh.                                                                                                                        |

### Constructors

#### Mesh()

### Properties

#### <a name="VERT648B"></a> Vertices : IReadOnlyList\<Vertex>

<small>`Read Only`</small>

Gets the vertices contained by this mesh.

#### <a name="INDIA2E8"></a> Indices : IReadOnlyList\<int>

<small>`Read Only`</small>

Gets the (optional) indices defining triangles by index of the vertex list.

#### <a name="ISIN97FB"></a> IsIndexed : bool

<small>`Read Only`</small>

Is this mesh constructed triangles specified by indexed?

#### <a name="VERSFB25"></a> Version : uint

<small>`Read Only`</small>

The version number of the mesh. Modifications to the mesh data increment this number.

### Methods

#### <a name="CLEA4538"></a> Clear() : void

Clears the mesh data.

#### <a name="ADDV16B6"></a> AddVertex([Vertex](Heirloom.Drawing.Vertex.md) vertex) : void

Appends a vertex to this mesh.


#### <a name="ADDV4361"></a> AddVertices(IEnumerable\<Vertex> vertices) : void

Appends multiple vertices to this mesh.


#### <a name="ADDVD003"></a> AddVertices(params [Vertex[]](Heirloom.Drawing.Vertex.md) vertices) : void

Appends multiple vertices to this mesh.


#### <a name="ADDI8228"></a> AddIndex(int index) : void

Appends a triangle index to this mesh. Until `Heirloom.Drawing.Mesh.Clear` is called, this mesh becomes indexed.


#### <a name="ADDIA703"></a> AddIndices(params int indices) : void

Appends a triangle index to this mesh. Until `Heirloom.Drawing.Mesh.Clear` is called, this mesh becomes indexed.


#### <a name="ADDI9A18"></a> AddIndices(IEnumerable\<int> indices) : void

Appends a triangle index to this mesh. Until `Heirloom.Drawing.Mesh.Clear` is called, this mesh becomes indexed.


#### <a name="CREA5F92"></a> CreateFromPolygon([Polygon](../Heirloom.Math/Heirloom.Math.Polygon.md) polygon) : [Mesh](Heirloom.Drawing.Mesh.md)
<small>`Static`</small>

Constructs a mesh from the given polygon via `Heirloom.Math.Polygon.Triangulate`.   
 UV coordinates are the normalized polygon within its own bounds.

<small>**polygon**: <param name="polygon">Some polygon.</param></small>  

#### <a name="CREAAC2E"></a> CreateFromPolygon(IReadOnlyList\<Vector> polygon) : [Mesh](Heirloom.Drawing.Mesh.md)
<small>`Static`</small>

Constructs a mesh from the given polygon via `Heirloom.Math.Polygon.Triangulate`.   
 UV coordinates are the normalized polygon within its own bounds.

<small>**polygon**: <param name="polygon">Some polygon.</param></small>  

#### <a name="CREAE4C0"></a> CreateFromConvexPolygon(IEnumerable\<Vector> polygon) : [Mesh](Heirloom.Drawing.Mesh.md)
<small>`Static`</small>

Constructs a mesh from the given convex polygon.   
 UV coordinates are the normalized polygon within its own bounds.

<small>**polygon**: <param name="polygon">Some convex polygon.</param></small>  

#### <a name="CREA3F18"></a> CreateQuad(float w, float h) : [Mesh](Heirloom.Drawing.Mesh.md)
<small>`Static`</small>

Creates a simple quad mesh.

<small>**w**: <param name="w">Width of the quad mesh.</param></small>  
<small>**h**: <param name="h">Height of the quad mesh.</param></small>  

