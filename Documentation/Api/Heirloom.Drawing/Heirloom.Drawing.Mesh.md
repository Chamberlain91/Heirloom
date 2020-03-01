# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../Heirloom.Drawing/Heirloom.Drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## Mesh (Sealed Class)
<small>**Namespace**: Heirloom.Drawing</sub></small>  

| Properties                | Summary                                                                               |
|---------------------------|---------------------------------------------------------------------------------------|
| [Vertices](#VER648B0F21)  | Gets the vertices contained by this mesh.                                             |
| [Indices](#INDA2E87CDB)   | Gets the (optional) indices defining triangles by index of the vertex list.           |
| [IsIndexed](#ISI97FBDC75) | Is this mesh constructed triangles specified by indexed?                              |
| [Version](#VERFB25B632)   | The version number of the mesh. Modifications to the mesh data increment this number. |

| Methods                                 | Summary                                                                                                                                            |
|-----------------------------------------|----------------------------------------------------------------------------------------------------------------------------------------------------|
| [Clear](#CLE4538C554)                   | Clears the mesh data.                                                                                                                              |
| [AddVertex](#ADD16B64495)               | Appends a vertex to this mesh.                                                                                                                     |
| [AddVertices](#ADD43612F17)             | Appends multiple vertices to this mesh.                                                                                                            |
| [AddVertices](#ADDD003481B)             | Appends multiple vertices to this mesh.                                                                                                            |
| [AddIndex](#ADD822885DD)                | Appends a triangle index to this mesh. Until `Heirloom.Drawing.Mesh.Clear` is called, this mesh becomes indexed.                                   |
| [AddIndices](#ADDA703B231)              | Appends a triangle index to this mesh. Until `Heirloom.Drawing.Mesh.Clear` is called, this mesh becomes indexed.                                   |
| [AddIndices](#ADD9A18089A)              | Appends a triangle index to this mesh. Until `Heirloom.Drawing.Mesh.Clear` is called, this mesh becomes indexed.                                   |
| [CreateFromPolygon](#CRE5F92A686)       | Constructs a mesh from the given polygon via `Heirloom.Math.Polygon.Triangulate`. UV coordinates are the normalized polygon within its own bounds. |
| [CreateFromPolygon](#CREAC2ED974)       | Constructs a mesh from the given polygon via `Heirloom.Math.Polygon.Triangulate`. UV coordinates are the normalized polygon within its own bounds. |
| [CreateFromConvexPolygon](#CREE4C0310F) | Constructs a mesh from the given convex polygon. UV coordinates are the normalized polygon within its own bounds.                                  |
| [CreateQuad](#CRE3F182D02)              | Creates a simple quad mesh.                                                                                                                        |

### Constructors

#### Mesh()

### Properties

#### <a name="VER648B0F21"></a>Vertices : IReadOnlyList\<Vertex>

<small>`Read Only`</small>

Gets the vertices contained by this mesh.

#### <a name="INDA2E87CDB"></a>Indices : IReadOnlyList\<int>

<small>`Read Only`</small>

Gets the (optional) indices defining triangles by index of the vertex list.

#### <a name="ISI97FBDC75"></a>IsIndexed : bool

<small>`Read Only`</small>

Is this mesh constructed triangles specified by indexed?

#### <a name="VERFB25B632"></a>Version : uint

<small>`Read Only`</small>

The version number of the mesh. Modifications to the mesh data increment this number.

### Methods

#### <a name="CLE4538C554"></a>Clear() : void

Clears the mesh data.

#### <a name="ADD16B64495"></a>AddVertex([Vertex](Heirloom.Drawing.Vertex.md) vertex) : void

Appends a vertex to this mesh.


#### <a name="ADD43612F17"></a>AddVertices(IEnumerable\<Vertex> vertices) : void

Appends multiple vertices to this mesh.


#### <a name="ADDD003481B"></a>AddVertices(params [Vertex[]](Heirloom.Drawing.Vertex.md) vertices) : void

Appends multiple vertices to this mesh.


#### <a name="ADD822885DD"></a>AddIndex(int index) : void

Appends a triangle index to this mesh. Until `Heirloom.Drawing.Mesh.Clear` is called, this mesh becomes indexed.


#### <a name="ADDA703B231"></a>AddIndices(params int indices) : void

Appends a triangle index to this mesh. Until `Heirloom.Drawing.Mesh.Clear` is called, this mesh becomes indexed.


#### <a name="ADD9A18089A"></a>AddIndices(IEnumerable\<int> indices) : void

Appends a triangle index to this mesh. Until `Heirloom.Drawing.Mesh.Clear` is called, this mesh becomes indexed.


#### <a name="CRE5F92A686"></a>CreateFromPolygon([Polygon](../Heirloom.Math/Heirloom.Math.Polygon.md) polygon) : [Mesh](Heirloom.Drawing.Mesh.md)
<small>`Static`</small>

Constructs a mesh from the given polygon via `Heirloom.Math.Polygon.Triangulate`.   
 UV coordinates are the normalized polygon within its own bounds.

<small>**polygon**: <param name="polygon">Some polygon.</param></small>  

#### <a name="CREAC2ED974"></a>CreateFromPolygon(IReadOnlyList\<Vector> polygon) : [Mesh](Heirloom.Drawing.Mesh.md)
<small>`Static`</small>

Constructs a mesh from the given polygon via `Heirloom.Math.Polygon.Triangulate`.   
 UV coordinates are the normalized polygon within its own bounds.

<small>**polygon**: <param name="polygon">Some polygon.</param></small>  

#### <a name="CREE4C0310F"></a>CreateFromConvexPolygon(IEnumerable\<Vector> polygon) : [Mesh](Heirloom.Drawing.Mesh.md)
<small>`Static`</small>

Constructs a mesh from the given convex polygon.   
 UV coordinates are the normalized polygon within its own bounds.

<small>**polygon**: <param name="polygon">Some convex polygon.</param></small>  

#### <a name="CRE3F182D02"></a>CreateQuad(float w, float h) : [Mesh](Heirloom.Drawing.Mesh.md)
<small>`Static`</small>

Creates a simple quad mesh.

<small>**w**: <param name="w">Width of the quad mesh.</param></small>  
<small>**h**: <param name="h">Height of the quad mesh.</param></small>  

