# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  

## Heirloom

### Class

| Name                                    | Summary                                                                                                                                                                                                                                   |
|-----------------------------------------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| [AudioClip][1]                          | An object to contain (and decode) audio data into raw samples.                                                                                                                                                                            |
| [AudioEffect][2]                        | An audio effect. Implementations of this class mutate the audio for various effects.                                                                                                                                                      |
| [BandPassFilter][3]                     | An audio effect that implements a band pass filter.                                                                                                                                                                                       |
| [HighPassFilter][4]                     | An audio effect that implements a high pass filter.                                                                                                                                                                                       |
| [LowPassFilter][5]                      | An audio effect that implements a low pass filter.                                                                                                                                                                                        |
| [ReverbEffect][6]                       | An audio effect that implements a Schroeder reverb.                                                                                                                                                                                       |
| [AudioNode][7]                          | Represents a node in the audio mixing tree.                                                                                                                                                                                               |
| [AudioGroup][8]                         | An [AudioNode][7] to mix and apply effects to a group of other nodes.                                                                                                                                                                     |
| [AudioSource][9]                        | An instance of playable audio.                                                                                                                                                                                                            |
| [BoundingTreeSpatialCollection\<T>][10] | A spatial collection to store and query elements in 2D space, implemented as a BVH style tree and has infinite bounds.                                                                                                                    |
| [Calc][11]                              | Math operations for `float` and a other data types including `int` . Includes a few genric utility functions such as [Calc.Swap\<T>][12]                                                                                                  |
| [Curves][13]                            | Provides utilities for working with Quadratic and Cubic curves.                                                                                                                                                                           |
| [Delaunay][14]                          | An implementation of delaunay triangulation.                                                                                                                                                                                              |
| [DrawingPerformance][15]                | Contains information pertaining to draw performance.                                                                                                                                                                                      |
| [EmbeddedFile][16]                      | Represents an embedded file.                                                                                                                                                                                                              |
| [Extensions][17]                        | Provides extension methods for `Random` and other related random operations.                                                                                                                                                              |
| [Files][18]                             | A utility to unify access of embedded files and files on disk.                                                                                                                                                                            |
| [Font][19]                              |                                                                                                                                                                                                                                           |
| [FreeList\<T>][20]                      | A free list an allocation-centric data structure that allows insertion and removal of elements in O(1) time, but does not behave like a typical "list" data type.                                                                         |
| [GameLoop][21]                          | Provides a thread to manage invoking a render/update function continuously.                                                                                                                                                               |
| [Glyph][22]                             | A glyph represents the metrics and rendering of a character from the associated `!:Drawing.Font` .                                                                                                                                        |
| [Graph\<TVertexKey, TVertexValue>][23]  | A configurable adjacency list based graph.                                                                                                                                                                                                |
| [Graphics][24]                          |                                                                                                                                                                                                                                           |
| [Grid\<T>][25]                          | A finite grid (bounded by size) of values.                                                                                                                                                                                                |
| [GridUtilities][26]                     | Provides extra utilities for interacting with a grid.                                                                                                                                                                                     |
| [Heap\<T>][27]                          | Represents a heap data structure. Allows the insertion and removal of items by priority.                                                                                                                                                  |
| [ImageSource][28]                       |                                                                                                                                                                                                                                           |
| [Image][29]                             |                                                                                                                                                                                                                                           |
| [Surface][30]                           | Represents a surface a [Graphics][24] object can draw on.                                                                                                                                                                                 |
| [LinearSpatialCollection\<T>][31]       | DO NOT USE! This is incredibly slow, but useful for behaviour testing against more complex implementions of [ISpatialCollection\<T>][32] . It is effectively implemented as list of shapes and does not operate on any spatial structure. |
| [Log][33]                               | Provides a simple mechanism to log debug and info messages.                                                                                                                                                                               |
| [MergeSort][34]                         |                                                                                                                                                                                                                                           |
| [Mesh][35]                              |                                                                                                                                                                                                                                           |
| [NineSlice][36]                         | A special stretchable rectangle of an image.                                                                                                                                                                                              |
| [PerlinNoise][37]                       | Implements methods for sampling 1D, 2D and 3D perlin noise.                                                                                                                                                                               |
| [Polygon][38]                           | Represents a simple polygon.                                                                                                                                                                                                              |
| [PolygonTools][39]                      | Provides several operations for polygons represented as a read-only list of vectors.                                                                                                                                                      |
| [Rasterizer][40]                        | Contains rasterization methods for iterating over pixel positions.                                                                                                                                                                        |
| [RectanglePacker\<T>][41]               |                                                                                                                                                                                                                                           |
| [Search][42]                            |                                                                                                                                                                                                                                           |
| [Shader][43]                            | Provides GLSL shader support for custom image effects and other visual processing.                                                                                                                                                        |
| [DistortionShader][44]                  | Distortion shader.                                                                                                                                                                                                                        |
| [GrayscaleShader][45]                   | Grayscale shader.                                                                                                                                                                                                                         |
| [InvertShader][46]                      | Invert shader.                                                                                                                                                                                                                            |
| [VectorBlurShader][47]                  | Vector blur shader.                                                                                                                                                                                                                       |
| [SimplexNoise][48]                      | Implements methods for sampling 2D and 3D simplex noise.                                                                                                                                                                                  |
| [SparseGrid\<T>][49]                    | An infinite, sparse grid of values.                                                                                                                                                                                                       |
| [Sprite][50]                            | A representation of single animated sprite. May also contains per-frame and animation sequence information for animating the sprite.                                                                                                      |
| [SpriteBuilder][51]                     | Utility object for manually constructing a sprite and its animations from images.                                                                                                                                                         |
| [StyledText][52]                        | Styled text compiled by a [StyledTextParser][53] .                                                                                                                                                                                        |
| [StyledTextParser][53]                  | Provides an ability to parse text with some sort of markup into [StyledText][52] .                                                                                                                                                        |
| [StandardStyledTextParser][54]          | Provides implementation of a BBCode-esque text markup parser.                                                                                                                                                                             |
| [SurfaceEffect][55]                     |                                                                                                                                                                                                                                           |
| [SurfacePool][56]                       | Provides a mechanism for requesting temporary surfaces and recycling them for reuse later.                                                                                                                                                |
| [TextLayout][57]                        | Utility to measure text and manually invoke the text layout function. Internally used by [Graphics.DrawText][58] and its variants.                                                                                                        |
| [Time][59]                              |                                                                                                                                                                                                                                           |
| [TypeDictionary\<T>][60]                | Manages objects by their type hierarchy up to the base type, allowing access and enumeration of objects by type.                                                                                                                          |
| [UniformInfo][61]                       | Contains information of a uniform from a [Shader][43] .                                                                                                                                                                                   |

### Interface

| Name                                  | Summary                                                                                                         |
|---------------------------------------|-----------------------------------------------------------------------------------------------------------------|
| [GraphicsAdapter.IShaderFactory][62]  |                                                                                                                 |
| [GraphicsAdapter.ISurfaceFactory][63] |                                                                                                                 |
| [IFiniteGrid\<T>][64]                 | A finite grid (bounded by [IFiniteGrid\<T>.Width][65] and [IFiniteGrid\<T>.Height][66] ).                       |
| [IGraph\<TKey, TValue, TGraph>][67]   |                                                                                                                 |
| [IGraphEdge\<TKey>][68]               | An edge between two vertices.                                                                                   |
| [IGraphVertex\<TKey, TValue>][69]     | A vertex representing a node on a graph.                                                                        |
| [IGrid\<T>][70]                       | A 2D grid of values.                                                                                            |
| [IHeap\<T>][71]                       | Represents a heap data structure. Allowing the access and removal of items by a priority ordering.              |
| [ILogHandler][72]                     |                                                                                                                 |
| [INoise1D][73]                        | Provides an interface for sampling one-dimensional noise.                                                       |
| [INoise2D][74]                        | Provides an interface for sampling two-dimensional noise.                                                       |
| [INoise3D][75]                        | Provides an interface for sampling three-dimensional noise.                                                     |
| [IReadOnlyGrid\<T>][76]               | A read-only view of a 2D grid of values.                                                                        |
| [IReadOnlyHeap\<T>][77]               | Represents a read-only view of a [Heap\<T>][27] .                                                               |
| [IReadOnlySparseGrid\<T>][78]         | A sparse 2D grid of values.                                                                                     |
| [IReadOnlySpatialCollection\<T>][79]  | A read-only view of a spatial collection to query elements in 2D space.                                         |
| [IReadOnlyTypeDictionary\<T>][80]     | A read-only view of [ITypeDictionary\<T>][81] .                                                                 |
| [IShape][82]                          | Represents the general interface of a shape and common operators each shape should support.                     |
| [ISparseGrid\<T>][83]                 | A sparse 2D grid of values.                                                                                     |
| [ISpatialCollection\<T>][32]          | A spatial collection to store and query elements in 2D space.                                                   |
| [ISpatialQuery\<T>][84]               | Provides methods for querying elements in 2D space.                                                             |
| [ITypeDictionary\<T>][81]             | Manages objects by their type hierarchy up to the base type, allowing access by enumeration of objects by type. |

### Struct

| Name                      | Summary                                                                                                   |
|---------------------------|-----------------------------------------------------------------------------------------------------------|
| [BitField][85]            | A structured byte to configure the 8 individual bits as a method of storing 'compressed' boolean values.  |
| [Circle][86]              | Represents a circle via center position and radius.                                                       |
| [Color][87]               | Color encoded as 4 component floats.                                                                      |
| [ColorBytes][88]          | Color encoded as 4 component bytes.                                                                       |
| [FontMetrics][89]         | Contains information about a font (ie, the vertical metrics).                                             |
| [GlyphMetrics][90]        | Contains information about a glyph (ie, the horizontal metrics).                                          |
| [Graphics.DrawCounts][91] |                                                                                                           |
| [GraphicsAdapterInfo][92] |                                                                                                           |
| [IntRange][93]            | Represents a range of integers from [IntRange.Min][94] to [IntRange.Max][95] .                            |
| [IntRectangle][96]        | Represents a rectangle defined with integer coordinates.                                                  |
| [IntSize][97]             | Represents a size or dimensions defined with integer fields.                                              |
| [IntVector][98]           | Represents a vector with two integer values.                                                              |
| [LineSegment][99]         | Represents a line segment defined by two [Vector][100] .                                                  |
| [Matrix][101]             | A 2x3 transformation matrix.                                                                              |
| [Range][102]              | Represents a range of single-precision floating point numbers from [Range.Min][103] to [Range.Max][104] . |
| [Ray][105]                | Represents a ray by orgin and direction vectors.                                                          |
| [RayContact][106]         | Represents the result of a ray-shape intersection.                                                        |
| [Rectangle][107]          |                                                                                                           |
| [Size][108]               |                                                                                                           |
| [Statistics][109]         | Represents statistics of some data.                                                                       |
| [TextDrawState][110]      | Represents information of any particular glyph when drawing text.                                         |
| [TextLayoutState][111]    | Represents information of any particular glyph during text layout.                                        |
| [Triangle][112]           |                                                                                                           |
| [UnicodeCharacter][113]   | Represents a single 32 bit Unicode character.                                                             |
| [UnicodeRange][114]       | Represents a range of unicode 32 bit code points.                                                         |
| [Vector][100]             | Represents a vector with two single-precision floating-point values.                                      |
| [Vertex][115]             | Represents a vertex of [Mesh][35] .                                                                       |

### Delegate

| Name                        | Summary                                                                    |
|-----------------------------|----------------------------------------------------------------------------|
| [AudioCaptureCallback][116] | Delegate describing the callback when audio is captured from a microphone. |
| [DrawTextCallback][117]     |                                                                            |
| [TextLayoutCallback][118]   |                                                                            |

### Enum

| Name                          | Summary                                                                       |
|-------------------------------|-------------------------------------------------------------------------------|
| [Axis][119]                   | Represents an axis of the 2D plane.                                           |
| [Blending][120]               | Controls how drawing operations are blended into existing pixels.             |
| [GridNeighborType][121]       | Describes the choice of neighbors in a grid.                                  |
| [HeapType][122]               | Describes the behaviour of a [Heap\<T>][27] .                                 |
| [InterpolationMode][123]      | Represents the behaviour when sampling an image on a non-integer coordinates. |
| [LogVerbosity][124]           | Controls the verbosity of [Log][33] .                                         |
| [MultisampleQuality][125]     | Multisampling levels                                                          |
| [PackingAlgorithm][126]       | An enumeration of rectangle packing algorithms.                               |
| [PerformanceOverlayMode][127] | Controls showing the performance overlay on a [Graphics][24] object.          |
| [RepeatMode][128]             | Represents the behaviour when sampling an image outside its natural bounds.   |
| [TextAlign][129]              | Controls how text is aligned to the layout rectangle.                         |
| [TimeUnit][130]               | Represents units of time, such as a millisecond.                              |
| [UniformType][131]            | Represents the type of a uniform in a [Shader][43] .                          |

[0]: Heirloom.Core.md
[1]: Heirloom.Core/Heirloom.AudioClip.md
[2]: Heirloom.Core/Heirloom.AudioEffect.md
[3]: Heirloom.Core/Heirloom.BandPassFilter.md
[4]: Heirloom.Core/Heirloom.HighPassFilter.md
[5]: Heirloom.Core/Heirloom.LowPassFilter.md
[6]: Heirloom.Core/Heirloom.ReverbEffect.md
[7]: Heirloom.Core/Heirloom.AudioNode.md
[8]: Heirloom.Core/Heirloom.AudioGroup.md
[9]: Heirloom.Core/Heirloom.AudioSource.md
[10]: Heirloom.Core/Heirloom.BoundingTreeSpatialCollection[T].md
[11]: Heirloom.Core/Heirloom.Calc.md
[12]: Heirloom.Core/Heirloom.Calc.Swap[T].md
[13]: Heirloom.Core/Heirloom.Curves.md
[14]: Heirloom.Core/Heirloom.Delaunay.md
[15]: Heirloom.Core/Heirloom.DrawingPerformance.md
[16]: Heirloom.Core/Heirloom.EmbeddedFile.md
[17]: Heirloom.Core/Heirloom.Extensions.md
[18]: Heirloom.Core/Heirloom.Files.md
[19]: Heirloom.Core/Heirloom.Font.md
[20]: Heirloom.Core/Heirloom.FreeList[T].md
[21]: Heirloom.Core/Heirloom.GameLoop.md
[22]: Heirloom.Core/Heirloom.Glyph.md
[23]: Heirloom.Core/Heirloom.Graph[TVertexKey,TVertexValue].md
[24]: Heirloom.Core/Heirloom.Graphics.md
[25]: Heirloom.Core/Heirloom.Grid[T].md
[26]: Heirloom.Core/Heirloom.GridUtilities.md
[27]: Heirloom.Core/Heirloom.Heap[T].md
[28]: Heirloom.Core/Heirloom.ImageSource.md
[29]: Heirloom.Core/Heirloom.Image.md
[30]: Heirloom.Core/Heirloom.Surface.md
[31]: Heirloom.Core/Heirloom.LinearSpatialCollection[T].md
[32]: Heirloom.Core/Heirloom.ISpatialCollection[T].md
[33]: Heirloom.Core/Heirloom.Log.md
[34]: Heirloom.Core/Heirloom.MergeSort.md
[35]: Heirloom.Core/Heirloom.Mesh.md
[36]: Heirloom.Core/Heirloom.NineSlice.md
[37]: Heirloom.Core/Heirloom.PerlinNoise.md
[38]: Heirloom.Core/Heirloom.Polygon.md
[39]: Heirloom.Core/Heirloom.PolygonTools.md
[40]: Heirloom.Core/Heirloom.Rasterizer.md
[41]: Heirloom.Core/Heirloom.RectanglePacker[T].md
[42]: Heirloom.Core/Heirloom.Search.md
[43]: Heirloom.Core/Heirloom.Shader.md
[44]: Heirloom.Core/Heirloom.DistortionShader.md
[45]: Heirloom.Core/Heirloom.GrayscaleShader.md
[46]: Heirloom.Core/Heirloom.InvertShader.md
[47]: Heirloom.Core/Heirloom.VectorBlurShader.md
[48]: Heirloom.Core/Heirloom.SimplexNoise.md
[49]: Heirloom.Core/Heirloom.SparseGrid[T].md
[50]: Heirloom.Core/Heirloom.Sprite.md
[51]: Heirloom.Core/Heirloom.SpriteBuilder.md
[52]: Heirloom.Core/Heirloom.StyledText.md
[53]: Heirloom.Core/Heirloom.StyledTextParser.md
[54]: Heirloom.Core/Heirloom.StandardStyledTextParser.md
[55]: Heirloom.Core/Heirloom.SurfaceEffect.md
[56]: Heirloom.Core/Heirloom.SurfacePool.md
[57]: Heirloom.Core/Heirloom.TextLayout.md
[58]: Heirloom.Core/Heirloom.Graphics.DrawText.md
[59]: Heirloom.Core/Heirloom.Time.md
[60]: Heirloom.Core/Heirloom.TypeDictionary[T].md
[61]: Heirloom.Core/Heirloom.UniformInfo.md
[62]: Heirloom.Core/Heirloom.GraphicsAdapter.IShaderFactory.md
[63]: Heirloom.Core/Heirloom.GraphicsAdapter.ISurfaceFactory.md
[64]: Heirloom.Core/Heirloom.IFiniteGrid[T].md
[65]: Heirloom.Core/Heirloom.IFiniteGrid[T].Width.md
[66]: Heirloom.Core/Heirloom.IFiniteGrid[T].Height.md
[67]: Heirloom.Core/Heirloom.IGraph[TKey,TValue,TGraph].md
[68]: Heirloom.Core/Heirloom.IGraphEdge[TKey].md
[69]: Heirloom.Core/Heirloom.IGraphVertex[TKey,TValue].md
[70]: Heirloom.Core/Heirloom.IGrid[T].md
[71]: Heirloom.Core/Heirloom.IHeap[T].md
[72]: Heirloom.Core/Heirloom.ILogHandler.md
[73]: Heirloom.Core/Heirloom.INoise1D.md
[74]: Heirloom.Core/Heirloom.INoise2D.md
[75]: Heirloom.Core/Heirloom.INoise3D.md
[76]: Heirloom.Core/Heirloom.IReadOnlyGrid[T].md
[77]: Heirloom.Core/Heirloom.IReadOnlyHeap[T].md
[78]: Heirloom.Core/Heirloom.IReadOnlySparseGrid[T].md
[79]: Heirloom.Core/Heirloom.IReadOnlySpatialCollection[T].md
[80]: Heirloom.Core/Heirloom.IReadOnlyTypeDictionary[T].md
[81]: Heirloom.Core/Heirloom.ITypeDictionary[T].md
[82]: Heirloom.Core/Heirloom.IShape.md
[83]: Heirloom.Core/Heirloom.ISparseGrid[T].md
[84]: Heirloom.Core/Heirloom.ISpatialQuery[T].md
[85]: Heirloom.Core/Heirloom.BitField.md
[86]: Heirloom.Core/Heirloom.Circle.md
[87]: Heirloom.Core/Heirloom.Color.md
[88]: Heirloom.Core/Heirloom.ColorBytes.md
[89]: Heirloom.Core/Heirloom.FontMetrics.md
[90]: Heirloom.Core/Heirloom.GlyphMetrics.md
[91]: Heirloom.Core/Heirloom.Graphics.DrawCounts.md
[92]: Heirloom.Core/Heirloom.GraphicsAdapterInfo.md
[93]: Heirloom.Core/Heirloom.IntRange.md
[94]: Heirloom.Core/Heirloom.IntRange.Min.md
[95]: Heirloom.Core/Heirloom.IntRange.Max.md
[96]: Heirloom.Core/Heirloom.IntRectangle.md
[97]: Heirloom.Core/Heirloom.IntSize.md
[98]: Heirloom.Core/Heirloom.IntVector.md
[99]: Heirloom.Core/Heirloom.LineSegment.md
[100]: Heirloom.Core/Heirloom.Vector.md
[101]: Heirloom.Core/Heirloom.Matrix.md
[102]: Heirloom.Core/Heirloom.Range.md
[103]: Heirloom.Core/Heirloom.Range.Min.md
[104]: Heirloom.Core/Heirloom.Range.Max.md
[105]: Heirloom.Core/Heirloom.Ray.md
[106]: Heirloom.Core/Heirloom.RayContact.md
[107]: Heirloom.Core/Heirloom.Rectangle.md
[108]: Heirloom.Core/Heirloom.Size.md
[109]: Heirloom.Core/Heirloom.Statistics.md
[110]: Heirloom.Core/Heirloom.TextDrawState.md
[111]: Heirloom.Core/Heirloom.TextLayoutState.md
[112]: Heirloom.Core/Heirloom.Triangle.md
[113]: Heirloom.Core/Heirloom.UnicodeCharacter.md
[114]: Heirloom.Core/Heirloom.UnicodeRange.md
[115]: Heirloom.Core/Heirloom.Vertex.md
[116]: Heirloom.Core/Heirloom.AudioCaptureCallback.md
[117]: Heirloom.Core/Heirloom.DrawTextCallback.md
[118]: Heirloom.Core/Heirloom.TextLayoutCallback.md
[119]: Heirloom.Core/Heirloom.Axis.md
[120]: Heirloom.Core/Heirloom.Blending.md
[121]: Heirloom.Core/Heirloom.GridNeighborType.md
[122]: Heirloom.Core/Heirloom.HeapType.md
[123]: Heirloom.Core/Heirloom.InterpolationMode.md
[124]: Heirloom.Core/Heirloom.LogVerbosity.md
[125]: Heirloom.Core/Heirloom.MultisampleQuality.md
[126]: Heirloom.Core/Heirloom.PackingAlgorithm.md
[127]: Heirloom.Core/Heirloom.PerformanceOverlayMode.md
[128]: Heirloom.Core/Heirloom.RepeatMode.md
[129]: Heirloom.Core/Heirloom.TextAlign.md
[130]: Heirloom.Core/Heirloom.TimeUnit.md
[131]: Heirloom.Core/Heirloom.UniformType.md
