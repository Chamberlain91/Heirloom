# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Heirloom

### Class

| Name                           | Summary                                                                |
|--------------------------------|------------------------------------------------------------------------|
| [Calc][1]                      | Math operations for float and a other data types including int . In... |
| [DrawingPerformance][2]        | Contains information pertaining to draw performance.                   |
| [Extensions][3]                | Provides extension methods for Random and other related random oper... |
| [Font][4]                      |                                                                        |
| [GameLoop][5]                  | Provides a thread to manage invoking a render/update function conti... |
| [Glyph][6]                     | A glyph represents the metrics and rendering of a character from th... |
| [Graphics][7]                  |                                                                        |
| [ImageSource][8]               |                                                                        |
| [Image][9]                     |                                                                        |
| [Surface][10]                  | Represents a surface a Graphics object can draw on.                    |
| [Log][11]                      | Provides a simple mechanism to log debug and info messages.            |
| [MergeSort][12]                |                                                                        |
| [Mesh][13]                     |                                                                        |
| [NineSlice][14]                | A special stretchable rectangle of an image.                           |
| [PerlinNoise][15]              | Implements methods for sampling 1D, 2D and 3D perlin noise.            |
| [Rasterizer][16]               | Contains rasterization methods for iterating over pixel positions.     |
| [RectanglePacker\<T>][17]      |                                                                        |
| [Search][18]                   |                                                                        |
| [Shader][19]                   | Provides GLSL shader support for custom image effects and other vis... |
| [DistortionShader][20]         | Distortion shader.                                                     |
| [GrayscaleShader][21]          | Grayscale shader.                                                      |
| [InvertShader][22]             | Invert shader.                                                         |
| [VectorBlurShader][23]         | Vector blur shader.                                                    |
| [SimplexNoise][24]             | Implements methods for sampling 2D and 3D simplex noise.               |
| [Sprite][25]                   | A representation of single animated sprite. May also contains per-f... |
| [SpriteBuilder][26]            | Utility object for manually constructing a sprite and its animation... |
| [StyledText][27]               | Styled text compiled by a StyledTextParser .                           |
| [StyledTextParser][28]         | Provides an ability to parse text with some sort of markup into Sty... |
| [StandardStyledTextParser][29] | Provides implementation of a BBCode-esque text markup parser.          |
| [SurfaceEffect][30]            |                                                                        |
| [SurfacePool][31]              | Provides a mechanism for requesting temporary surfaces and recyclin... |
| [TextLayout][32]               | Utility to measure text and manually invoke the text layout functio... |
| [Time][33]                     |                                                                        |
| [UniformInfo][34]              | Contains information of a uniform from a Shader .                      |

### Struct

| Name                      | Summary                                                                |
|---------------------------|------------------------------------------------------------------------|
| [Color][35]               | Color encoded as 4 component floats.                                   |
| [ColorBytes][36]          | Color encoded as 4 component bytes.                                    |
| [FontMetrics][37]         | Contains information about a font (ie, the vertical metrics).          |
| [GlyphMetrics][38]        | Contains information about a glyph (ie, the horizontal metrics).       |
| [Graphics.DrawCounts][39] |                                                                        |
| [GraphicsAdapterInfo][40] |                                                                        |
| [IntRange][41]            | Represents a range of integers from IntRange.Min to IntRange.Max .     |
| [IntRectangle][42]        | Represents a rectangle defined with integer coordinates.               |
| [IntSize][43]             | Represents a size or dimensions defined with integer fields.           |
| [IntVector][44]           | Represents a vector with two integer values.                           |
| [Matrix][45]              | A 2x3 transformation matrix.                                           |
| [Range][46]               | Represents a range of single-precision floating point numbers from ... |
| [Rectangle][47]           |                                                                        |
| [Size][48]                |                                                                        |
| [Statistics][49]          | Represents statistics of some data.                                    |
| [TextDrawState][50]       | Represents information of any particular glyph when drawing text.      |
| [TextLayoutState][51]     | Represents information of any particular glyph during text layout.     |
| [UnicodeCharacter][52]    | Represents a single 32 bit Unicode character.                          |
| [UnicodeRange][53]        | Represents a range of unicode 32 bit code points.                      |
| [Vector][54]              | Represents a vector with two single-precision floating-point values.   |
| [Vertex][55]              | Represents a vertex of Mesh .                                          |

### Interface

| Name                                  | Summary                                                     |
|---------------------------------------|-------------------------------------------------------------|
| [GraphicsAdapter.IShaderFactory][56]  |                                                             |
| [GraphicsAdapter.ISurfaceFactory][57] |                                                             |
| [ILogHandler][58]                     |                                                             |
| [INoise1D][59]                        | Provides an interface for sampling one-dimensional noise.   |
| [INoise2D][60]                        | Provides an interface for sampling two-dimensional noise.   |
| [INoise3D][61]                        | Provides an interface for sampling three-dimensional noise. |

### Enum

| Name                         | Summary                                                                |
|------------------------------|------------------------------------------------------------------------|
| [Axis][62]                   | Represents an axis of the 2D plane.                                    |
| [Blending][63]               | Controls how drawing operations are blended into existing pixels.      |
| [InterpolationMode][64]      | Represents the behaviour when sampling an image on a non-integer co... |
| [LogVerbosity][65]           | Controls the verbosity of Log .                                        |
| [MultisampleQuality][66]     | Multisampling levels                                                   |
| [PackingAlgorithm][67]       | An enumeration of rectangle packing algorithms.                        |
| [PerformanceOverlayMode][68] | Controls showing the performance overlay on a Graphics object.         |
| [RepeatMode][69]             | Represents the behaviour when sampling an image outside its natural... |
| [TextAlign][70]              | Controls how text is aligned to the layout rectangle.                  |
| [TimeUnit][71]               | Represents units of time, such as a millisecond.                       |
| [UniformType][72]            | Represents the type of a uniform in a Shader .                         |

### Delegate

| Name                     | Summary |
|--------------------------|---------|
| [DrawTextCallback][73]   |         |
| [TextLayoutCallback][74] |         |

## Heirloom.Collections

### Class

| Name                                   | Summary                                                                |
|----------------------------------------|------------------------------------------------------------------------|
| [BvhSpatialCollection\<T>][75]         | A spatial collection to store and query elements in 2D space, imple... |
| [FreeList\<T>][76]                     | A free list an allocation-centric data structure that allows insert... |
| [Graph\<TVertexKey, TVertexValue>][77] | A configurable adjacency list based graph.                             |
| [Grid\<T>][78]                         | A finite grid (bounded by size) of values.                             |
| [GridUtilities][79]                    | Provides extra utilities for interacting with a grid.                  |
| [Heap\<T>][80]                         | Represents a heap data structure. Allows the insertion and removal ... |
| [LinearSpatialCollection\<T>][81]      | DO NOT USE! This is incredibly slow, but useful for behaviour testi... |
| [SparseGrid\<T>][82]                   | An infinite, sparse grid of values.                                    |
| [TypeDictionary\<T>][83]               | Manages objects by their type hierarchy up to the base type, allowi... |

### Interface

| Name                                 | Summary                                                                |
|--------------------------------------|------------------------------------------------------------------------|
| [IFiniteGrid\<T>][84]                | A finite grid (bounded by IFiniteGrid<T>.Width and IFiniteGrid<T>.H... |
| [IGraph\<TKey, TValue, TGraph>][85]  |                                                                        |
| [IGraphEdge\<TKey>][86]              | An edge between two vertices.                                          |
| [IGraphVertex\<TKey, TValue>][87]    | A vertex representing a node on a graph.                               |
| [IGrid\<T>][88]                      | A 2D grid of values.                                                   |
| [IHeap\<T>][89]                      | Represents a heap data structure. Allowing the access and removal o... |
| [IReadOnlyGrid\<T>][90]              | A read-only view of a 2D grid of values.                               |
| [IReadOnlyHeap\<T>][91]              | Represents a read-only view of a Heap<T> .                             |
| [IReadOnlySparseGrid\<T>][92]        | A sparse 2D grid of values.                                            |
| [IReadOnlySpatialCollection\<T>][93] | A read-only view of a spatial collection to query elements in 2D sp... |
| [IReadOnlyTypeDictionary\<T>][94]    | A read-only view of ITypeDictionary<T> .                               |
| [ISparseGrid\<T>][95]                | A sparse 2D grid of values.                                            |
| [ISpatialCollection\<T>][96]         | A spatial collection to store and query elements in 2D space.          |
| [ISpatialQuery\<T>][97]              | Provides methods for querying elements in 2D space.                    |
| [ITypeDictionary\<T>][98]            | Manages objects by their type hierarchy up to the base type, allowi... |

### Enum

| Name                   | Summary                                      |
|------------------------|----------------------------------------------|
| [GridNeighborType][99] | Describes the choice of neighbors in a grid. |
| [HeapType][100]        | Describes the behaviour of a Heap<T> .       |

## Heirloom.Geometry

### Class

| Name                | Summary                                                                |
|---------------------|------------------------------------------------------------------------|
| [Curves][101]       | Provides utilities for working with Quadratic and Cubic curves.        |
| [Delaunay][102]     | An implementation of delaunay triangulation.                           |
| [Polygon][103]      | Represents a simple polygon.                                           |
| [PolygonTools][104] | Provides several operations for polygons represented as a read-only... |

### Struct

| Name               | Summary                                             |
|--------------------|-----------------------------------------------------|
| [Circle][105]      | Represents a circle via center position and radius. |
| [LineSegment][106] | Represents a line segment defined by two Vector .   |
| [Ray][107]         | Represents a ray by orgin and direction vectors.    |
| [RayContact][108]  | Represents the result of a ray-shape intersection.  |
| [Triangle][109]    |                                                     |

### Interface

| Name          | Summary                                                                |
|---------------|------------------------------------------------------------------------|
| [IShape][110] | Represents the general interface of a shape and common operators ea... |

## Heirloom.IO

### Class

| Name                | Summary                                                        |
|---------------------|----------------------------------------------------------------|
| [EmbeddedFile][111] | Represents an embedded file.                                   |
| [Files][112]        | A utility to unify access of embedded files and files on disk. |

### Struct

| Name            | Summary                                                                |
|-----------------|------------------------------------------------------------------------|
| [BitField][113] | A structured byte to configure the 8 individual bits as a method of... |

## Heirloom.Sound

### Class

| Name                  | Summary                                                                |
|-----------------------|------------------------------------------------------------------------|
| [AudioClip][114]      | An object to contain (and decode) audio data into raw samples.         |
| [AudioEffect][115]    | An audio effect. Implementations of this class mutate the audio for... |
| [BandPassFilter][116] | An audio effect that implements a band pass filter.                    |
| [HighPassFilter][117] | An audio effect that implements a high pass filter.                    |
| [LowPassFilter][118]  | An audio effect that implements a low pass filter.                     |
| [ReverbEffect][119]   | An audio effect that implements a Schroeder reverb.                    |
| [AudioNode][120]      | Represents a node in the audio mixing tree.                            |
| [AudioGroup][121]     | An AudioNode to mix and apply effects to a group of other nodes.       |
| [AudioSource][122]    | An instance of playable audio.                                         |

### Delegate

| Name                        | Summary |
|-----------------------------|---------|
| [AudioCaptureCallback][123] |         |

[0]: Heirloom.Core.md
[1]: Heirloom.Core/Heirloom/Calc.md
[2]: Heirloom.Core/Heirloom/DrawingPerformance.md
[3]: Heirloom.Core/Heirloom/Extensions.md
[4]: Heirloom.Core/Heirloom/Font.md
[5]: Heirloom.Core/Heirloom/GameLoop.md
[6]: Heirloom.Core/Heirloom/Glyph.md
[7]: Heirloom.Core/Heirloom/Graphics.md
[8]: Heirloom.Core/Heirloom/ImageSource.md
[9]: Heirloom.Core/Heirloom/Image.md
[10]: Heirloom.Core/Heirloom/Surface.md
[11]: Heirloom.Core/Heirloom/Log.md
[12]: Heirloom.Core/Heirloom/MergeSort.md
[13]: Heirloom.Core/Heirloom/Mesh.md
[14]: Heirloom.Core/Heirloom/NineSlice.md
[15]: Heirloom.Core/Heirloom/PerlinNoise.md
[16]: Heirloom.Core/Heirloom/Rasterizer.md
[17]: Heirloom.Core/Heirloom/RectanglePacker[T].md
[18]: Heirloom.Core/Heirloom/Search.md
[19]: Heirloom.Core/Heirloom/Shader.md
[20]: Heirloom.Core/Heirloom/DistortionShader.md
[21]: Heirloom.Core/Heirloom/GrayscaleShader.md
[22]: Heirloom.Core/Heirloom/InvertShader.md
[23]: Heirloom.Core/Heirloom/VectorBlurShader.md
[24]: Heirloom.Core/Heirloom/SimplexNoise.md
[25]: Heirloom.Core/Heirloom/Sprite.md
[26]: Heirloom.Core/Heirloom/SpriteBuilder.md
[27]: Heirloom.Core/Heirloom/StyledText.md
[28]: Heirloom.Core/Heirloom/StyledTextParser.md
[29]: Heirloom.Core/Heirloom/StandardStyledTextParser.md
[30]: Heirloom.Core/Heirloom/SurfaceEffect.md
[31]: Heirloom.Core/Heirloom/SurfacePool.md
[32]: Heirloom.Core/Heirloom/TextLayout.md
[33]: Heirloom.Core/Heirloom/Time.md
[34]: Heirloom.Core/Heirloom/UniformInfo.md
[35]: Heirloom.Core/Heirloom/Color.md
[36]: Heirloom.Core/Heirloom/ColorBytes.md
[37]: Heirloom.Core/Heirloom/FontMetrics.md
[38]: Heirloom.Core/Heirloom/GlyphMetrics.md
[39]: Heirloom.Core/Heirloom/Graphics.DrawCounts.md
[40]: Heirloom.Core/Heirloom/GraphicsAdapterInfo.md
[41]: Heirloom.Core/Heirloom/IntRange.md
[42]: Heirloom.Core/Heirloom/IntRectangle.md
[43]: Heirloom.Core/Heirloom/IntSize.md
[44]: Heirloom.Core/Heirloom/IntVector.md
[45]: Heirloom.Core/Heirloom/Matrix.md
[46]: Heirloom.Core/Heirloom/Range.md
[47]: Heirloom.Core/Heirloom/Rectangle.md
[48]: Heirloom.Core/Heirloom/Size.md
[49]: Heirloom.Core/Heirloom/Statistics.md
[50]: Heirloom.Core/Heirloom/TextDrawState.md
[51]: Heirloom.Core/Heirloom/TextLayoutState.md
[52]: Heirloom.Core/Heirloom/UnicodeCharacter.md
[53]: Heirloom.Core/Heirloom/UnicodeRange.md
[54]: Heirloom.Core/Heirloom/Vector.md
[55]: Heirloom.Core/Heirloom/Vertex.md
[56]: Heirloom.Core/Heirloom/GraphicsAdapter.IShaderFactory.md
[57]: Heirloom.Core/Heirloom/GraphicsAdapter.ISurfaceFactory.md
[58]: Heirloom.Core/Heirloom/ILogHandler.md
[59]: Heirloom.Core/Heirloom/INoise1D.md
[60]: Heirloom.Core/Heirloom/INoise2D.md
[61]: Heirloom.Core/Heirloom/INoise3D.md
[62]: Heirloom.Core/Heirloom/Axis.md
[63]: Heirloom.Core/Heirloom/Blending.md
[64]: Heirloom.Core/Heirloom/InterpolationMode.md
[65]: Heirloom.Core/Heirloom/LogVerbosity.md
[66]: Heirloom.Core/Heirloom/MultisampleQuality.md
[67]: Heirloom.Core/Heirloom/PackingAlgorithm.md
[68]: Heirloom.Core/Heirloom/PerformanceOverlayMode.md
[69]: Heirloom.Core/Heirloom/RepeatMode.md
[70]: Heirloom.Core/Heirloom/TextAlign.md
[71]: Heirloom.Core/Heirloom/TimeUnit.md
[72]: Heirloom.Core/Heirloom/UniformType.md
[73]: Heirloom.Core/Heirloom/DrawTextCallback.md
[74]: Heirloom.Core/Heirloom/TextLayoutCallback.md
[75]: Heirloom.Core/Heirloom.Collections/BvhSpatialCollection[T].md
[76]: Heirloom.Core/Heirloom.Collections/FreeList[T].md
[77]: Heirloom.Core/Heirloom.Collections/Graph[TVertexKey,TVertexValue].md
[78]: Heirloom.Core/Heirloom.Collections/Grid[T].md
[79]: Heirloom.Core/Heirloom.Collections/GridUtilities.md
[80]: Heirloom.Core/Heirloom.Collections/Heap[T].md
[81]: Heirloom.Core/Heirloom.Collections/LinearSpatialCollection[T].md
[82]: Heirloom.Core/Heirloom.Collections/SparseGrid[T].md
[83]: Heirloom.Core/Heirloom.Collections/TypeDictionary[T].md
[84]: Heirloom.Core/Heirloom.Collections/IFiniteGrid[T].md
[85]: Heirloom.Core/Heirloom.Collections/IGraph[TKey,TValue,TGraph].md
[86]: Heirloom.Core/Heirloom.Collections/IGraphEdge[TKey].md
[87]: Heirloom.Core/Heirloom.Collections/IGraphVertex[TKey,TValue].md
[88]: Heirloom.Core/Heirloom.Collections/IGrid[T].md
[89]: Heirloom.Core/Heirloom.Collections/IHeap[T].md
[90]: Heirloom.Core/Heirloom.Collections/IReadOnlyGrid[T].md
[91]: Heirloom.Core/Heirloom.Collections/IReadOnlyHeap[T].md
[92]: Heirloom.Core/Heirloom.Collections/IReadOnlySparseGrid[T].md
[93]: Heirloom.Core/Heirloom.Collections/IReadOnlySpatialCollection[T].md
[94]: Heirloom.Core/Heirloom.Collections/IReadOnlyTypeDictionary[T].md
[95]: Heirloom.Core/Heirloom.Collections/ISparseGrid[T].md
[96]: Heirloom.Core/Heirloom.Collections/ISpatialCollection[T].md
[97]: Heirloom.Core/Heirloom.Collections/ISpatialQuery[T].md
[98]: Heirloom.Core/Heirloom.Collections/ITypeDictionary[T].md
[99]: Heirloom.Core/Heirloom.Collections/GridNeighborType.md
[100]: Heirloom.Core/Heirloom.Collections/HeapType.md
[101]: Heirloom.Core/Heirloom.Geometry/Curves.md
[102]: Heirloom.Core/Heirloom.Geometry/Delaunay.md
[103]: Heirloom.Core/Heirloom.Geometry/Polygon.md
[104]: Heirloom.Core/Heirloom.Geometry/PolygonTools.md
[105]: Heirloom.Core/Heirloom.Geometry/Circle.md
[106]: Heirloom.Core/Heirloom.Geometry/LineSegment.md
[107]: Heirloom.Core/Heirloom.Geometry/Ray.md
[108]: Heirloom.Core/Heirloom.Geometry/RayContact.md
[109]: Heirloom.Core/Heirloom.Geometry/Triangle.md
[110]: Heirloom.Core/Heirloom.Geometry/IShape.md
[111]: Heirloom.Core/Heirloom.IO/EmbeddedFile.md
[112]: Heirloom.Core/Heirloom.IO/Files.md
[113]: Heirloom.Core/Heirloom.IO/BitField.md
[114]: Heirloom.Core/Heirloom.Sound/AudioClip.md
[115]: Heirloom.Core/Heirloom.Sound/AudioEffect.md
[116]: Heirloom.Core/Heirloom.Sound/BandPassFilter.md
[117]: Heirloom.Core/Heirloom.Sound/HighPassFilter.md
[118]: Heirloom.Core/Heirloom.Sound/LowPassFilter.md
[119]: Heirloom.Core/Heirloom.Sound/ReverbEffect.md
[120]: Heirloom.Core/Heirloom.Sound/AudioNode.md
[121]: Heirloom.Core/Heirloom.Sound/AudioGroup.md
[122]: Heirloom.Core/Heirloom.Sound/AudioSource.md
[123]: Heirloom.Core/Heirloom.Sound/AudioCaptureCallback.md
