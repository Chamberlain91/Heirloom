# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Heirloom

### Class

| Name                                    | Summary                                                                |
|-----------------------------------------|------------------------------------------------------------------------|
| [AudioClip][1]                          | An object to contain (and decode) audio data into raw samples.         |
| [AudioEffect][2]                        | An audio effect. Implementations of this class mutate the audio for... |
| [BandPassFilter][3]                     | An audio effect that implements a band pass filter.                    |
| [HighPassFilter][4]                     | An audio effect that implements a high pass filter.                    |
| [LowPassFilter][5]                      | An audio effect that implements a low pass filter.                     |
| [ReverbEffect][6]                       | An audio effect that implements a Schroeder reverb.                    |
| [AudioNode][7]                          | Represents a node in the audio mixing tree.                            |
| [AudioGroup][8]                         | An AudioNode to mix and apply effects to a group of other nodes.       |
| [AudioSource][9]                        | An instance of playable audio.                                         |
| [BoundingTreeSpatialCollection\<T>][10] | A spatial collection to store and query elements in 2D space, imple... |
| [Calc][11]                              | Math operations for float and a other data types including int . In... |
| [Curves][12]                            | Provides utilities for working with Quadratic and Cubic curves.        |
| [Delaunay][13]                          | An implementation of delaunay triangulation.                           |
| [DrawingPerformance][14]                | Contains information pertaining to draw performance.                   |
| [EmbeddedFile][15]                      | Represents an embedded file.                                           |
| [Extensions][16]                        | Provides extension methods for Random and other related random oper... |
| [Files][17]                             | A utility to unify access of embedded files and files on disk.         |
| [Font][18]                              |                                                                        |
| [FreeList\<T>][19]                      | A free list an allocation-centric data structure that allows insert... |
| [GameLoop][20]                          | Provides a thread to manage invoking a render/update function conti... |
| [Glyph][21]                             | A glyph represents the metrics and rendering of a character from th... |
| [Graph\<TVertexKey, TVertexValue>][22]  | A configurable adjacency list based graph.                             |
| [Graphics][23]                          |                                                                        |
| [Grid\<T>][24]                          | A finite grid (bounded by size) of values.                             |
| [GridUtilities][25]                     | Provides extra utilities for interacting with a grid.                  |
| [Heap\<T>][26]                          | Represents a heap data structure. Allows the insertion and removal ... |
| [ImageSource][27]                       |                                                                        |
| [Image][28]                             |                                                                        |
| [Surface][29]                           | Represents a surface a Graphics object can draw on.                    |
| [LinearSpatialCollection\<T>][30]       | DO NOT USE! This is incredibly slow, but useful for behaviour testi... |
| [Log][31]                               | Provides a simple mechanism to log debug and info messages.            |
| [MergeSort][32]                         |                                                                        |
| [Mesh][33]                              |                                                                        |
| [NineSlice][34]                         | A special stretchable rectangle of an image.                           |
| [PerlinNoise][35]                       | Implements methods for sampling 1D, 2D and 3D perlin noise.            |
| [Polygon][36]                           | Represents a simple polygon.                                           |
| [PolygonTools][37]                      | Provides several operations for polygons represented as a read-only... |
| [Rasterizer][38]                        | Contains rasterization methods for iterating over pixel positions.     |
| [RectanglePacker\<T>][39]               |                                                                        |
| [Search][40]                            |                                                                        |
| [Shader][41]                            | Provides GLSL shader support for custom image effects and other vis... |
| [DistortionShader][42]                  | Distortion shader.                                                     |
| [GrayscaleShader][43]                   | Grayscale shader.                                                      |
| [InvertShader][44]                      | Invert shader.                                                         |
| [VectorBlurShader][45]                  | Vector blur shader.                                                    |
| [SimplexNoise][46]                      | Implements methods for sampling 2D and 3D simplex noise.               |
| [SparseGrid\<T>][47]                    | An infinite, sparse grid of values.                                    |
| [Sprite][48]                            | A representation of single animated sprite. May also contains per-f... |
| [SpriteBuilder][49]                     | Utility object for manually constructing a sprite and its animation... |
| [StyledText][50]                        | Styled text compiled by a StyledTextParser .                           |
| [StyledTextParser][51]                  | Provides an ability to parse text with some sort of markup into Sty... |
| [StandardStyledTextParser][52]          | Provides implementation of a BBCode-esque text markup parser.          |
| [SurfaceEffect][53]                     |                                                                        |
| [SurfacePool][54]                       | Provides a mechanism for requesting temporary surfaces and recyclin... |
| [TextLayout][55]                        | Utility to measure text and manually invoke the text layout functio... |
| [Time][56]                              |                                                                        |
| [TypeDictionary\<T>][57]                | Manages objects by their type hierarchy up to the base type, allowi... |
| [UniformInfo][58]                       | Contains information of a uniform from a Shader .                      |

### Struct

| Name                      | Summary                                                                |
|---------------------------|------------------------------------------------------------------------|
| [BitField][59]            | A structured byte to configure the 8 individual bits as a method of... |
| [Circle][60]              | Represents a circle via center position and radius.                    |
| [Color][61]               | Color encoded as 4 component floats.                                   |
| [ColorBytes][62]          | Color encoded as 4 component bytes.                                    |
| [FontMetrics][63]         | Contains information about a font (ie, the vertical metrics).          |
| [GlyphMetrics][64]        | Contains information about a glyph (ie, the horizontal metrics).       |
| [Graphics.DrawCounts][65] |                                                                        |
| [GraphicsAdapterInfo][66] |                                                                        |
| [IntRange][67]            | Represents a range of integers from IntRange.Min to IntRange.Max .     |
| [IntRectangle][68]        | Represents a rectangle defined with integer coordinates.               |
| [IntSize][69]             | Represents a size or dimensions defined with integer fields.           |
| [IntVector][70]           | Represents a vector with two integer values.                           |
| [LineSegment][71]         | Represents a line segment defined by two Vector .                      |
| [Matrix][72]              | A 2x3 transformation matrix.                                           |
| [Range][73]               | Represents a range of single-precision floating point numbers from ... |
| [Ray][74]                 | Represents a ray by orgin and direction vectors.                       |
| [RayContact][75]          | Represents the result of a ray-shape intersection.                     |
| [Rectangle][76]           |                                                                        |
| [Size][77]                |                                                                        |
| [Statistics][78]          | Represents statistics of some data.                                    |
| [TextDrawState][79]       | Represents information of any particular glyph when drawing text.      |
| [TextLayoutState][80]     | Represents information of any particular glyph during text layout.     |
| [Triangle][81]            |                                                                        |
| [UnicodeCharacter][82]    | Represents a single 32 bit Unicode character.                          |
| [UnicodeRange][83]        | Represents a range of unicode 32 bit code points.                      |
| [Vector][84]              | Represents a vector with two single-precision floating-point values.   |
| [Vertex][85]              | Represents a vertex of Mesh .                                          |

### Interface

| Name                                  | Summary                                                                |
|---------------------------------------|------------------------------------------------------------------------|
| [GraphicsAdapter.IShaderFactory][86]  |                                                                        |
| [GraphicsAdapter.ISurfaceFactory][87] |                                                                        |
| [IFiniteGrid\<T>][88]                 | A finite grid (bounded by IFiniteGrid<T>.Width and IFiniteGrid<T>.H... |
| [IGraph\<TKey, TValue, TGraph>][89]   |                                                                        |
| [IGraphEdge\<TKey>][90]               | An edge between two vertices.                                          |
| [IGraphVertex\<TKey, TValue>][91]     | A vertex representing a node on a graph.                               |
| [IGrid\<T>][92]                       | A 2D grid of values.                                                   |
| [IHeap\<T>][93]                       | Represents a heap data structure. Allowing the access and removal o... |
| [ILogHandler][94]                     |                                                                        |
| [INoise1D][95]                        | Provides an interface for sampling one-dimensional noise.              |
| [INoise2D][96]                        | Provides an interface for sampling two-dimensional noise.              |
| [INoise3D][97]                        | Provides an interface for sampling three-dimensional noise.            |
| [IReadOnlyGrid\<T>][98]               | A read-only view of a 2D grid of values.                               |
| [IReadOnlyHeap\<T>][99]               | Represents a read-only view of a Heap<T> .                             |
| [IReadOnlySparseGrid\<T>][100]        | A sparse 2D grid of values.                                            |
| [IReadOnlySpatialCollection\<T>][101] | A read-only view of a spatial collection to query elements in 2D sp... |
| [IReadOnlyTypeDictionary\<T>][102]    | A read-only view of ITypeDictionary<T> .                               |
| [IShape][103]                         | Represents the general interface of a shape and common operators ea... |
| [ISparseGrid\<T>][104]                | A sparse 2D grid of values.                                            |
| [ISpatialCollection\<T>][105]         | A spatial collection to store and query elements in 2D space.          |
| [ISpatialQuery\<T>][106]              | Provides methods for querying elements in 2D space.                    |
| [ITypeDictionary\<T>][107]            | Manages objects by their type hierarchy up to the base type, allowi... |

### Enum

| Name                          | Summary                                                                |
|-------------------------------|------------------------------------------------------------------------|
| [Axis][108]                   | Represents an axis of the 2D plane.                                    |
| [Blending][109]               | Controls how drawing operations are blended into existing pixels.      |
| [GridNeighborType][110]       | Describes the choice of neighbors in a grid.                           |
| [HeapType][111]               | Describes the behaviour of a Heap<T> .                                 |
| [InterpolationMode][112]      | Represents the behaviour when sampling an image on a non-integer co... |
| [LogVerbosity][113]           | Controls the verbosity of Log .                                        |
| [MultisampleQuality][114]     | Multisampling levels                                                   |
| [PackingAlgorithm][115]       | An enumeration of rectangle packing algorithms.                        |
| [PerformanceOverlayMode][116] | Controls showing the performance overlay on a Graphics object.         |
| [RepeatMode][117]             | Represents the behaviour when sampling an image outside its natural... |
| [TextAlign][118]              | Controls how text is aligned to the layout rectangle.                  |
| [TimeUnit][119]               | Represents units of time, such as a millisecond.                       |
| [UniformType][120]            | Represents the type of a uniform in a Shader .                         |

### Delegate

| Name                        | Summary |
|-----------------------------|---------|
| [AudioCaptureCallback][121] |         |
| [DrawTextCallback][122]     |         |
| [TextLayoutCallback][123]   |         |

[0]: Heirloom.Core.md
[1]: Heirloom.Core/Heirloom/AudioClip.md
[2]: Heirloom.Core/Heirloom/AudioEffect.md
[3]: Heirloom.Core/Heirloom/BandPassFilter.md
[4]: Heirloom.Core/Heirloom/HighPassFilter.md
[5]: Heirloom.Core/Heirloom/LowPassFilter.md
[6]: Heirloom.Core/Heirloom/ReverbEffect.md
[7]: Heirloom.Core/Heirloom/AudioNode.md
[8]: Heirloom.Core/Heirloom/AudioGroup.md
[9]: Heirloom.Core/Heirloom/AudioSource.md
[10]: Heirloom.Core/Heirloom/BoundingTreeSpatialCollection[T].md
[11]: Heirloom.Core/Heirloom/Calc.md
[12]: Heirloom.Core/Heirloom/Curves.md
[13]: Heirloom.Core/Heirloom/Delaunay.md
[14]: Heirloom.Core/Heirloom/DrawingPerformance.md
[15]: Heirloom.Core/Heirloom/EmbeddedFile.md
[16]: Heirloom.Core/Heirloom/Extensions.md
[17]: Heirloom.Core/Heirloom/Files.md
[18]: Heirloom.Core/Heirloom/Font.md
[19]: Heirloom.Core/Heirloom/FreeList[T].md
[20]: Heirloom.Core/Heirloom/GameLoop.md
[21]: Heirloom.Core/Heirloom/Glyph.md
[22]: Heirloom.Core/Heirloom/Graph[TVertexKey,TVertexValue].md
[23]: Heirloom.Core/Heirloom/Graphics.md
[24]: Heirloom.Core/Heirloom/Grid[T].md
[25]: Heirloom.Core/Heirloom/GridUtilities.md
[26]: Heirloom.Core/Heirloom/Heap[T].md
[27]: Heirloom.Core/Heirloom/ImageSource.md
[28]: Heirloom.Core/Heirloom/Image.md
[29]: Heirloom.Core/Heirloom/Surface.md
[30]: Heirloom.Core/Heirloom/LinearSpatialCollection[T].md
[31]: Heirloom.Core/Heirloom/Log.md
[32]: Heirloom.Core/Heirloom/MergeSort.md
[33]: Heirloom.Core/Heirloom/Mesh.md
[34]: Heirloom.Core/Heirloom/NineSlice.md
[35]: Heirloom.Core/Heirloom/PerlinNoise.md
[36]: Heirloom.Core/Heirloom/Polygon.md
[37]: Heirloom.Core/Heirloom/PolygonTools.md
[38]: Heirloom.Core/Heirloom/Rasterizer.md
[39]: Heirloom.Core/Heirloom/RectanglePacker[T].md
[40]: Heirloom.Core/Heirloom/Search.md
[41]: Heirloom.Core/Heirloom/Shader.md
[42]: Heirloom.Core/Heirloom/DistortionShader.md
[43]: Heirloom.Core/Heirloom/GrayscaleShader.md
[44]: Heirloom.Core/Heirloom/InvertShader.md
[45]: Heirloom.Core/Heirloom/VectorBlurShader.md
[46]: Heirloom.Core/Heirloom/SimplexNoise.md
[47]: Heirloom.Core/Heirloom/SparseGrid[T].md
[48]: Heirloom.Core/Heirloom/Sprite.md
[49]: Heirloom.Core/Heirloom/SpriteBuilder.md
[50]: Heirloom.Core/Heirloom/StyledText.md
[51]: Heirloom.Core/Heirloom/StyledTextParser.md
[52]: Heirloom.Core/Heirloom/StandardStyledTextParser.md
[53]: Heirloom.Core/Heirloom/SurfaceEffect.md
[54]: Heirloom.Core/Heirloom/SurfacePool.md
[55]: Heirloom.Core/Heirloom/TextLayout.md
[56]: Heirloom.Core/Heirloom/Time.md
[57]: Heirloom.Core/Heirloom/TypeDictionary[T].md
[58]: Heirloom.Core/Heirloom/UniformInfo.md
[59]: Heirloom.Core/Heirloom/BitField.md
[60]: Heirloom.Core/Heirloom/Circle.md
[61]: Heirloom.Core/Heirloom/Color.md
[62]: Heirloom.Core/Heirloom/ColorBytes.md
[63]: Heirloom.Core/Heirloom/FontMetrics.md
[64]: Heirloom.Core/Heirloom/GlyphMetrics.md
[65]: Heirloom.Core/Heirloom/Graphics.DrawCounts.md
[66]: Heirloom.Core/Heirloom/GraphicsAdapterInfo.md
[67]: Heirloom.Core/Heirloom/IntRange.md
[68]: Heirloom.Core/Heirloom/IntRectangle.md
[69]: Heirloom.Core/Heirloom/IntSize.md
[70]: Heirloom.Core/Heirloom/IntVector.md
[71]: Heirloom.Core/Heirloom/LineSegment.md
[72]: Heirloom.Core/Heirloom/Matrix.md
[73]: Heirloom.Core/Heirloom/Range.md
[74]: Heirloom.Core/Heirloom/Ray.md
[75]: Heirloom.Core/Heirloom/RayContact.md
[76]: Heirloom.Core/Heirloom/Rectangle.md
[77]: Heirloom.Core/Heirloom/Size.md
[78]: Heirloom.Core/Heirloom/Statistics.md
[79]: Heirloom.Core/Heirloom/TextDrawState.md
[80]: Heirloom.Core/Heirloom/TextLayoutState.md
[81]: Heirloom.Core/Heirloom/Triangle.md
[82]: Heirloom.Core/Heirloom/UnicodeCharacter.md
[83]: Heirloom.Core/Heirloom/UnicodeRange.md
[84]: Heirloom.Core/Heirloom/Vector.md
[85]: Heirloom.Core/Heirloom/Vertex.md
[86]: Heirloom.Core/Heirloom/GraphicsAdapter.IShaderFactory.md
[87]: Heirloom.Core/Heirloom/GraphicsAdapter.ISurfaceFactory.md
[88]: Heirloom.Core/Heirloom/IFiniteGrid[T].md
[89]: Heirloom.Core/Heirloom/IGraph[TKey,TValue,TGraph].md
[90]: Heirloom.Core/Heirloom/IGraphEdge[TKey].md
[91]: Heirloom.Core/Heirloom/IGraphVertex[TKey,TValue].md
[92]: Heirloom.Core/Heirloom/IGrid[T].md
[93]: Heirloom.Core/Heirloom/IHeap[T].md
[94]: Heirloom.Core/Heirloom/ILogHandler.md
[95]: Heirloom.Core/Heirloom/INoise1D.md
[96]: Heirloom.Core/Heirloom/INoise2D.md
[97]: Heirloom.Core/Heirloom/INoise3D.md
[98]: Heirloom.Core/Heirloom/IReadOnlyGrid[T].md
[99]: Heirloom.Core/Heirloom/IReadOnlyHeap[T].md
[100]: Heirloom.Core/Heirloom/IReadOnlySparseGrid[T].md
[101]: Heirloom.Core/Heirloom/IReadOnlySpatialCollection[T].md
[102]: Heirloom.Core/Heirloom/IReadOnlyTypeDictionary[T].md
[103]: Heirloom.Core/Heirloom/IShape.md
[104]: Heirloom.Core/Heirloom/ISparseGrid[T].md
[105]: Heirloom.Core/Heirloom/ISpatialCollection[T].md
[106]: Heirloom.Core/Heirloom/ISpatialQuery[T].md
[107]: Heirloom.Core/Heirloom/ITypeDictionary[T].md
[108]: Heirloom.Core/Heirloom/Axis.md
[109]: Heirloom.Core/Heirloom/Blending.md
[110]: Heirloom.Core/Heirloom/GridNeighborType.md
[111]: Heirloom.Core/Heirloom/HeapType.md
[112]: Heirloom.Core/Heirloom/InterpolationMode.md
[113]: Heirloom.Core/Heirloom/LogVerbosity.md
[114]: Heirloom.Core/Heirloom/MultisampleQuality.md
[115]: Heirloom.Core/Heirloom/PackingAlgorithm.md
[116]: Heirloom.Core/Heirloom/PerformanceOverlayMode.md
[117]: Heirloom.Core/Heirloom/RepeatMode.md
[118]: Heirloom.Core/Heirloom/TextAlign.md
[119]: Heirloom.Core/Heirloom/TimeUnit.md
[120]: Heirloom.Core/Heirloom/UniformType.md
[121]: Heirloom.Core/Heirloom/AudioCaptureCallback.md
[122]: Heirloom.Core/Heirloom/DrawTextCallback.md
[123]: Heirloom.Core/Heirloom/TextLayoutCallback.md
