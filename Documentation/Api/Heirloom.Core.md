# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Heirloom

### Class

| Name                           | Summary                                                                |
|--------------------------------|------------------------------------------------------------------------|
| [Calc][1]                      | Provides various math operations for float and int types including ... |
| [Extensions][2]                | Provides extension methods for Random and other related random oper... |
| [Font][3]                      | An object to represent a truetype font. Provides functionality to q... |
| [GameLoop][4]                  | Provides a thread to manage invoking a render/update function conti... |
| [Glyph][5]                     | A glyph represents the metrics and rendering of a character from th... |
| [Graphics][6]                  |                                                                        |
| [ImageSource][7]               |                                                                        |
| [Image][8]                     |                                                                        |
| [Surface][9]                   | Represents a surface a Graphics object can draw on.                    |
| [Input][10]                    | Provides a centralized query style input layer. This is useful for ... |
| [Interval][11]                 | A utility object to check if an interval of time has occured.          |
| [Log][12]                      | Provides a simple mechanism to log debug and info messages.            |
| [MergeSort][13]                |                                                                        |
| [Mesh][14]                     |                                                                        |
| [NineSlice][15]                | A special stretchable rectangle of an image.                           |
| [PerlinNoise][16]              | Implements methods for sampling 1D, 2D and 3D perlin noise.            |
| [Rasterizer][17]               | Contains rasterization methods for iterating over pixel positions.     |
| [RectanglePacker\<T>][18]      |                                                                        |
| [Screen][19]                   | An abstract representation of the screen (ie, window, view, etc).      |
| [Search][20]                   | Contains search related features.                                      |
| [Shader][21]                   | Provides GLSL shader support for custom image effects and other vis... |
| [DistortionShader][22]         | Distortion shader.                                                     |
| [GrayscaleShader][23]          | Grayscale shader.                                                      |
| [InvertShader][24]             | Invert shader.                                                         |
| [VectorBlurShader][25]         | Vector blur shader.                                                    |
| [SimplexNoise][26]             | Implements methods for sampling 2D and 3D simplex noise.               |
| [Sprite][27]                   | A representation of single animated sprite. May also contains per-f... |
| [SpriteBuilder][28]            | Utility object for manually constructing a sprite and its animation... |
| [StyledText][29]               | Styled text compiled by a StyledTextParser .                           |
| [StyledTextParser][30]         | Provides an ability to parse text with some sort of markup into Sty... |
| [StandardStyledTextParser][31] | Provides implementation of a BBCode-esque text markup parser.          |
| [SurfaceEffect][32]            |                                                                        |
| [SurfacePool][33]              | Provides a mechanism for requesting temporary surfaces and recyclin... |
| [TextLayout][34]               | Utility to measure text and manually invoke the text layout functio... |
| [Time][35]                     |                                                                        |
| [UniformInfo][36]              | Contains information of a uniform from a Shader .                      |

### Struct

| Name                      | Summary                                                                |
|---------------------------|------------------------------------------------------------------------|
| [CharacterEvent][37]      |                                                                        |
| [Color][38]               | Color encoded as 4 component floats.                                   |
| [ColorBytes][39]          | Color encoded as 4 component bytes.                                    |
| [FontMetrics][40]         | Contains information about a font (ie, the vertical metrics).          |
| [GlyphMetrics][41]        | Contains information about a glyph (ie, the horizontal metrics).       |
| [Graphics.DrawCounts][42] |                                                                        |
| [IntRange][43]            | Represents a range of integers from IntRange.Min to IntRange.Max .     |
| [IntRectangle][44]        | Represents a rectangle defined with integer coordinates.               |
| [IntSize][45]             | Represents a size or dimensions defined with integer fields.           |
| [IntVector][46]           | Represents a vector with two integer values.                           |
| [KeyEvent][47]            |                                                                        |
| [Matrix][48]              | A 2x3 transformation matrix.                                           |
| [MouseButtonEvent][49]    |                                                                        |
| [MouseMoveEvent][50]      |                                                                        |
| [MouseScrollEvent][51]    |                                                                        |
| [Range][52]               | Represents a range of single-precision floating point numbers from ... |
| [Rectangle][53]           |                                                                        |
| [Size][54]                |                                                                        |
| [Statistics][55]          | Represents statistics of some data.                                    |
| [TextDrawState][56]       | Represents information of any particular glyph when drawing text.      |
| [TextLayoutState][57]     | Represents information of any particular glyph during text layout.     |
| [Touch][58]               |                                                                        |
| [TouchEvent][59]          |                                                                        |
| [UnicodeCharacter][60]    | Represents a single 32 bit Unicode character.                          |
| [UnicodeRange][61]        | Represents a range of unicode 32 bit code points.                      |
| [Vector][62]              | Represents a vector with two single-precision floating-point values.   |
| [Vertex][63]              | Represents a vertex of Mesh .                                          |

### Interface

| Name                                  | Summary                                                     |
|---------------------------------------|-------------------------------------------------------------|
| [GraphicsAdapter.IShaderFactory][64]  |                                                             |
| [GraphicsAdapter.ISurfaceFactory][65] |                                                             |
| [IInputSource][66]                    | Represents the functionality of an input source.            |
| [ILogHandler][67]                     |                                                             |
| [INoise1D][68]                        | Provides an interface for sampling one-dimensional noise.   |
| [INoise2D][69]                        | Provides an interface for sampling two-dimensional noise.   |
| [INoise3D][70]                        | Provides an interface for sampling three-dimensional noise. |

### Enum

| Name                         | Summary                                                                |
|------------------------------|------------------------------------------------------------------------|
| [Axis][71]                   | Represents an axis of the 2D plane.                                    |
| [Blending][72]               | Controls how drawing operations are blended into existing pixels.      |
| [ButtonState][73]            | Represents the state of a button.                                      |
| [GamepadAxis][74]            |                                                                        |
| [GamepadButton][75]          |                                                                        |
| [InterpolationMode][76]      | Represents the behaviour when sampling an image on a non-integer co... |
| [Key][77]                    | Standard virtual key mapping (standard US keyboard layout) from GLFW.  |
| [KeyModifiers][78]           |                                                                        |
| [LogVerbosity][79]           | Controls the verbosity of Log .                                        |
| [MouseButton][80]            |                                                                        |
| [MultisampleQuality][81]     | Multisampling levels                                                   |
| [PackingAlgorithm][82]       | An enumeration of rectangle packing algorithms.                        |
| [PerformanceOverlayMode][83] | Controls showing the performance overlay on a Graphics object.         |
| [RepeatMode][84]             | Represents the behaviour when sampling an image outside its natural... |
| [SurfaceType][85]            | Represents the surface type.                                           |
| [TextAlign][86]              | Controls how text is aligned to the layout rectangle.                  |
| [TimeUnit][87]               | Represents units of time, such as a millisecond.                       |
| [UniformType][88]            | Represents the type of a uniform in a Shader .                         |

### Delegate

| Name                     | Summary                                                     |
|--------------------------|-------------------------------------------------------------|
| [ActualCost\<T>][89]     | Gets the known cost between two values.                     |
| [DrawTextCallback][90]   | Delegate type for the callback when drawing text.           |
| [HeuristicCost\<T>][91]  | Gets the estimated cost of the some value.                  |
| [TextLayoutCallback][92] | Delegate type for the callback when performing text layout. |

## Heirloom.Collections

### Class

| Name                               | Summary                                                                |
|------------------------------------|------------------------------------------------------------------------|
| [BvhSpatialCollection\<T>][93]     | A spatial collection to store and query elements in 2D space, imple... |
| [DirectedGraph\<T>][94]            |                                                                        |
| [FreeList\<T>][95]                 | A free list an allocation-centric data structure that allows insert... |
| [Graph\<T>][96]                    |                                                                        |
| [Grid\<T>][97]                     | A finite grid (bounded by size) of values.                             |
| [GridUtilities][98]                | Provides extra utilities for interacting with a grid.                  |
| [Heap\<T>][99]                     | Represents a heap data structure. Allows the insertion and removal ... |
| [LinearSpatialCollection\<T>][100] | DO NOT USE! This is incredibly slow, but useful for behaviour testi... |
| [SparseGrid\<T>][101]              | An infinite, sparse grid of values.                                    |
| [TypeDictionary\<T>][102]          | Manages objects by their type hierarchy up to the base type, allowi... |

### Interface

| Name                                  | Summary                                                                |
|---------------------------------------|------------------------------------------------------------------------|
| [IDirectedGraph\<T>][103]             |                                                                        |
| [IFiniteGrid\<T>][104]                | A finite grid (bounded by IFiniteGrid<T>.Width and IFiniteGrid<T>.H... |
| [IGraph\<T>][105]                     | An interface that represents a graph.                                  |
| [IGrid\<T>][106]                      | A 2D grid of values.                                                   |
| [IHeap\<T>][107]                      | Represents a heap data structure. Allowing the access and removal o... |
| [IReadOnlyGrid\<T>][108]              | A read-only view of a 2D grid of values.                               |
| [IReadOnlyHeap\<T>][109]              | Represents a read-only view of a Heap<T> .                             |
| [IReadOnlySparseGrid\<T>][110]        | A sparse 2D grid of values.                                            |
| [IReadOnlySpatialCollection\<T>][111] | A read-only view of a spatial collection to query elements in 2D sp... |
| [IReadOnlyTypeDictionary\<T>][112]    | A read-only view of ITypeDictionary<T> .                               |
| [ISparseGrid\<T>][113]                | A sparse 2D grid of values.                                            |
| [ISpatialCollection\<T>][114]         | A spatial collection to store and query elements in 2D space.          |
| [ISpatialQuery\<T>][115]              | Provides methods for querying elements in 2D space.                    |
| [ITypeDictionary\<T>][116]            | Manages objects by their type hierarchy up to the base type, allowi... |

### Enum

| Name                    | Summary                                      |
|-------------------------|----------------------------------------------|
| [GridNeighborType][117] | Describes the choice of neighbors in a grid. |
| [HeapType][118]         | Describes the behaviour of a Heap<T> .       |
| [TraversalMethod][119]  | Represents a choice of traversing a graph.   |

## Heirloom.Geometry

### Class

| Name                 | Summary                                                                |
|----------------------|------------------------------------------------------------------------|
| [Curve][120]         |                                                                        |
| [CurveTools][121]    | Utility function for computation with quadratic and cubic curves.      |
| [GeometryTools][122] | Provides utilities for generating and manipulating shapes.             |
| [Polygon][123]       | Represents a simple polygon.                                           |
| [PolygonTools][124]  | Provides several operations for polygons represented as a read-only... |

### Struct

| Name               | Summary                                                 |
|--------------------|---------------------------------------------------------|
| [Circle][125]      | Represents a circle via center position and radius.     |
| [LineSegment][126] | Represents a line segment defined by two end points.    |
| [Ray][127]         | Represents a ray by orgin point and directional vector. |
| [RayContact][128]  | Represents the result of a ray to shape intersection.   |
| [Triangle][129]    |                                                         |

### Interface

| Name          | Summary                                                                |
|---------------|------------------------------------------------------------------------|
| [IShape][130] | Represents the general interface of a shape and common operators ea... |

### Enum

| Name             | Summary |
|------------------|---------|
| [CurveType][131] |         |

## Heirloom.IO

### Class

| Name                | Summary                                                        |
|---------------------|----------------------------------------------------------------|
| [EmbeddedFile][132] | Represents an embedded file.                                   |
| [Files][133]        | A utility to unify access of embedded files and files on disk. |

### Struct

| Name            | Summary                                                                |
|-----------------|------------------------------------------------------------------------|
| [BitField][134] | A structured byte to configure the 8 individual bits as a method of... |

## Heirloom.Sound

### Class

| Name                  | Summary                                                                |
|-----------------------|------------------------------------------------------------------------|
| [AudioClip][135]      | An object to contain (and decode) audio data into raw samples.         |
| [AudioEffect][136]    | An audio effect. Implementations of this class mutate the audio for... |
| [BandPassFilter][137] | An audio effect that implements a band pass filter.                    |
| [HighPassFilter][138] | An audio effect that implements a high pass filter.                    |
| [LowPassFilter][139]  | An audio effect that implements a low pass filter.                     |
| [ReverbEffect][140]   | An audio effect that implements a Schroeder reverb.                    |
| [AudioNode][141]      | Represents a node in the audio mixing tree.                            |
| [AudioGroup][142]     | An AudioNode to mix and apply effects to a group of other nodes.       |
| [AudioSource][143]    | An instance of playable audio.                                         |

### Delegate

| Name                        | Summary |
|-----------------------------|---------|
| [AudioCaptureCallback][144] |         |

[0]: Heirloom.Core.md
[1]: Heirloom.Core/Heirloom/Calc.md
[2]: Heirloom.Core/Heirloom/Extensions.md
[3]: Heirloom.Core/Heirloom/Font.md
[4]: Heirloom.Core/Heirloom/GameLoop.md
[5]: Heirloom.Core/Heirloom/Glyph.md
[6]: Heirloom.Core/Heirloom/Graphics.md
[7]: Heirloom.Core/Heirloom/ImageSource.md
[8]: Heirloom.Core/Heirloom/Image.md
[9]: Heirloom.Core/Heirloom/Surface.md
[10]: Heirloom.Core/Heirloom/Input.md
[11]: Heirloom.Core/Heirloom/Interval.md
[12]: Heirloom.Core/Heirloom/Log.md
[13]: Heirloom.Core/Heirloom/MergeSort.md
[14]: Heirloom.Core/Heirloom/Mesh.md
[15]: Heirloom.Core/Heirloom/NineSlice.md
[16]: Heirloom.Core/Heirloom/PerlinNoise.md
[17]: Heirloom.Core/Heirloom/Rasterizer.md
[18]: Heirloom.Core/Heirloom/RectanglePacker[T].md
[19]: Heirloom.Core/Heirloom/Screen.md
[20]: Heirloom.Core/Heirloom/Search.md
[21]: Heirloom.Core/Heirloom/Shader.md
[22]: Heirloom.Core/Heirloom/DistortionShader.md
[23]: Heirloom.Core/Heirloom/GrayscaleShader.md
[24]: Heirloom.Core/Heirloom/InvertShader.md
[25]: Heirloom.Core/Heirloom/VectorBlurShader.md
[26]: Heirloom.Core/Heirloom/SimplexNoise.md
[27]: Heirloom.Core/Heirloom/Sprite.md
[28]: Heirloom.Core/Heirloom/SpriteBuilder.md
[29]: Heirloom.Core/Heirloom/StyledText.md
[30]: Heirloom.Core/Heirloom/StyledTextParser.md
[31]: Heirloom.Core/Heirloom/StandardStyledTextParser.md
[32]: Heirloom.Core/Heirloom/SurfaceEffect.md
[33]: Heirloom.Core/Heirloom/SurfacePool.md
[34]: Heirloom.Core/Heirloom/TextLayout.md
[35]: Heirloom.Core/Heirloom/Time.md
[36]: Heirloom.Core/Heirloom/UniformInfo.md
[37]: Heirloom.Core/Heirloom/CharacterEvent.md
[38]: Heirloom.Core/Heirloom/Color.md
[39]: Heirloom.Core/Heirloom/ColorBytes.md
[40]: Heirloom.Core/Heirloom/FontMetrics.md
[41]: Heirloom.Core/Heirloom/GlyphMetrics.md
[42]: Heirloom.Core/Heirloom/Graphics.DrawCounts.md
[43]: Heirloom.Core/Heirloom/IntRange.md
[44]: Heirloom.Core/Heirloom/IntRectangle.md
[45]: Heirloom.Core/Heirloom/IntSize.md
[46]: Heirloom.Core/Heirloom/IntVector.md
[47]: Heirloom.Core/Heirloom/KeyEvent.md
[48]: Heirloom.Core/Heirloom/Matrix.md
[49]: Heirloom.Core/Heirloom/MouseButtonEvent.md
[50]: Heirloom.Core/Heirloom/MouseMoveEvent.md
[51]: Heirloom.Core/Heirloom/MouseScrollEvent.md
[52]: Heirloom.Core/Heirloom/Range.md
[53]: Heirloom.Core/Heirloom/Rectangle.md
[54]: Heirloom.Core/Heirloom/Size.md
[55]: Heirloom.Core/Heirloom/Statistics.md
[56]: Heirloom.Core/Heirloom/TextDrawState.md
[57]: Heirloom.Core/Heirloom/TextLayoutState.md
[58]: Heirloom.Core/Heirloom/Touch.md
[59]: Heirloom.Core/Heirloom/TouchEvent.md
[60]: Heirloom.Core/Heirloom/UnicodeCharacter.md
[61]: Heirloom.Core/Heirloom/UnicodeRange.md
[62]: Heirloom.Core/Heirloom/Vector.md
[63]: Heirloom.Core/Heirloom/Vertex.md
[64]: Heirloom.Core/Heirloom/GraphicsAdapter.IShaderFactory.md
[65]: Heirloom.Core/Heirloom/GraphicsAdapter.ISurfaceFactory.md
[66]: Heirloom.Core/Heirloom/IInputSource.md
[67]: Heirloom.Core/Heirloom/ILogHandler.md
[68]: Heirloom.Core/Heirloom/INoise1D.md
[69]: Heirloom.Core/Heirloom/INoise2D.md
[70]: Heirloom.Core/Heirloom/INoise3D.md
[71]: Heirloom.Core/Heirloom/Axis.md
[72]: Heirloom.Core/Heirloom/Blending.md
[73]: Heirloom.Core/Heirloom/ButtonState.md
[74]: Heirloom.Core/Heirloom/GamepadAxis.md
[75]: Heirloom.Core/Heirloom/GamepadButton.md
[76]: Heirloom.Core/Heirloom/InterpolationMode.md
[77]: Heirloom.Core/Heirloom/Key.md
[78]: Heirloom.Core/Heirloom/KeyModifiers.md
[79]: Heirloom.Core/Heirloom/LogVerbosity.md
[80]: Heirloom.Core/Heirloom/MouseButton.md
[81]: Heirloom.Core/Heirloom/MultisampleQuality.md
[82]: Heirloom.Core/Heirloom/PackingAlgorithm.md
[83]: Heirloom.Core/Heirloom/PerformanceOverlayMode.md
[84]: Heirloom.Core/Heirloom/RepeatMode.md
[85]: Heirloom.Core/Heirloom/SurfaceType.md
[86]: Heirloom.Core/Heirloom/TextAlign.md
[87]: Heirloom.Core/Heirloom/TimeUnit.md
[88]: Heirloom.Core/Heirloom/UniformType.md
[89]: Heirloom.Core/Heirloom/ActualCost[T].md
[90]: Heirloom.Core/Heirloom/DrawTextCallback.md
[91]: Heirloom.Core/Heirloom/HeuristicCost[T].md
[92]: Heirloom.Core/Heirloom/TextLayoutCallback.md
[93]: Heirloom.Core/Heirloom.Collections/BvhSpatialCollection[T].md
[94]: Heirloom.Core/Heirloom.Collections/DirectedGraph[T].md
[95]: Heirloom.Core/Heirloom.Collections/FreeList[T].md
[96]: Heirloom.Core/Heirloom.Collections/Graph[T].md
[97]: Heirloom.Core/Heirloom.Collections/Grid[T].md
[98]: Heirloom.Core/Heirloom.Collections/GridUtilities.md
[99]: Heirloom.Core/Heirloom.Collections/Heap[T].md
[100]: Heirloom.Core/Heirloom.Collections/LinearSpatialCollection[T].md
[101]: Heirloom.Core/Heirloom.Collections/SparseGrid[T].md
[102]: Heirloom.Core/Heirloom.Collections/TypeDictionary[T].md
[103]: Heirloom.Core/Heirloom.Collections/IDirectedGraph[T].md
[104]: Heirloom.Core/Heirloom.Collections/IFiniteGrid[T].md
[105]: Heirloom.Core/Heirloom.Collections/IGraph[T].md
[106]: Heirloom.Core/Heirloom.Collections/IGrid[T].md
[107]: Heirloom.Core/Heirloom.Collections/IHeap[T].md
[108]: Heirloom.Core/Heirloom.Collections/IReadOnlyGrid[T].md
[109]: Heirloom.Core/Heirloom.Collections/IReadOnlyHeap[T].md
[110]: Heirloom.Core/Heirloom.Collections/IReadOnlySparseGrid[T].md
[111]: Heirloom.Core/Heirloom.Collections/IReadOnlySpatialCollection[T].md
[112]: Heirloom.Core/Heirloom.Collections/IReadOnlyTypeDictionary[T].md
[113]: Heirloom.Core/Heirloom.Collections/ISparseGrid[T].md
[114]: Heirloom.Core/Heirloom.Collections/ISpatialCollection[T].md
[115]: Heirloom.Core/Heirloom.Collections/ISpatialQuery[T].md
[116]: Heirloom.Core/Heirloom.Collections/ITypeDictionary[T].md
[117]: Heirloom.Core/Heirloom.Collections/GridNeighborType.md
[118]: Heirloom.Core/Heirloom.Collections/HeapType.md
[119]: Heirloom.Core/Heirloom.Collections/TraversalMethod.md
[120]: Heirloom.Core/Heirloom.Geometry/Curve.md
[121]: Heirloom.Core/Heirloom.Geometry/CurveTools.md
[122]: Heirloom.Core/Heirloom.Geometry/GeometryTools.md
[123]: Heirloom.Core/Heirloom.Geometry/Polygon.md
[124]: Heirloom.Core/Heirloom.Geometry/PolygonTools.md
[125]: Heirloom.Core/Heirloom.Geometry/Circle.md
[126]: Heirloom.Core/Heirloom.Geometry/LineSegment.md
[127]: Heirloom.Core/Heirloom.Geometry/Ray.md
[128]: Heirloom.Core/Heirloom.Geometry/RayContact.md
[129]: Heirloom.Core/Heirloom.Geometry/Triangle.md
[130]: Heirloom.Core/Heirloom.Geometry/IShape.md
[131]: Heirloom.Core/Heirloom.Geometry/CurveType.md
[132]: Heirloom.Core/Heirloom.IO/EmbeddedFile.md
[133]: Heirloom.Core/Heirloom.IO/Files.md
[134]: Heirloom.Core/Heirloom.IO/BitField.md
[135]: Heirloom.Core/Heirloom.Sound/AudioClip.md
[136]: Heirloom.Core/Heirloom.Sound/AudioEffect.md
[137]: Heirloom.Core/Heirloom.Sound/BandPassFilter.md
[138]: Heirloom.Core/Heirloom.Sound/HighPassFilter.md
[139]: Heirloom.Core/Heirloom.Sound/LowPassFilter.md
[140]: Heirloom.Core/Heirloom.Sound/ReverbEffect.md
[141]: Heirloom.Core/Heirloom.Sound/AudioNode.md
[142]: Heirloom.Core/Heirloom.Sound/AudioGroup.md
[143]: Heirloom.Core/Heirloom.Sound/AudioSource.md
[144]: Heirloom.Core/Heirloom.Sound/AudioCaptureCallback.md
