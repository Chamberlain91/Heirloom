# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Heirloom

### Class

| Name                           | Summary                                                                |
|--------------------------------|------------------------------------------------------------------------|
| [Calc][1]                      | Provides various math operations for float and int types including ... |
| [DrawingPerformance][2]        | Contains information pertaining to draw performance.                   |
| [Extensions][3]                | Provides extension methods for Random and other related random oper... |
| [Font][4]                      | An object to represent a truetype font. Provides functionality to q... |
| [GameLoop][5]                  | Provides a thread to manage invoking a render/update function conti... |
| [Glyph][6]                     | A glyph represents the metrics and rendering of a character from th... |
| [Graphics][7]                  |                                                                        |
| [ImageSource][8]               |                                                                        |
| [Image][9]                     |                                                                        |
| [Surface][10]                  | Represents a surface a Graphics object can draw on.                    |
| [Input][11]                    | Provides a centralized query style input layer. This is useful for ... |
| [Interval][12]                 | A utility object to check if an interval of time has occured.          |
| [Log][13]                      | Provides a simple mechanism to log debug and info messages.            |
| [MergeSort][14]                |                                                                        |
| [Mesh][15]                     |                                                                        |
| [NineSlice][16]                | A special stretchable rectangle of an image.                           |
| [PerlinNoise][17]              | Implements methods for sampling 1D, 2D and 3D perlin noise.            |
| [Rasterizer][18]               | Contains rasterization methods for iterating over pixel positions.     |
| [RectanglePacker\<T>][19]      |                                                                        |
| [Screen][20]                   | An abstract representation of the screen (ie, window, view, etc).      |
| [Search][21]                   |                                                                        |
| [Shader][22]                   | Provides GLSL shader support for custom image effects and other vis... |
| [DistortionShader][23]         | Distortion shader.                                                     |
| [GrayscaleShader][24]          | Grayscale shader.                                                      |
| [InvertShader][25]             | Invert shader.                                                         |
| [VectorBlurShader][26]         | Vector blur shader.                                                    |
| [SimplexNoise][27]             | Implements methods for sampling 2D and 3D simplex noise.               |
| [Sprite][28]                   | A representation of single animated sprite. May also contains per-f... |
| [SpriteBuilder][29]            | Utility object for manually constructing a sprite and its animation... |
| [StyledText][30]               | Styled text compiled by a StyledTextParser .                           |
| [StyledTextParser][31]         | Provides an ability to parse text with some sort of markup into Sty... |
| [StandardStyledTextParser][32] | Provides implementation of a BBCode-esque text markup parser.          |
| [SurfaceEffect][33]            |                                                                        |
| [SurfacePool][34]              | Provides a mechanism for requesting temporary surfaces and recyclin... |
| [TextLayout][35]               | Utility to measure text and manually invoke the text layout functio... |
| [Time][36]                     |                                                                        |
| [UniformInfo][37]              | Contains information of a uniform from a Shader .                      |

### Struct

| Name                      | Summary                                                                |
|---------------------------|------------------------------------------------------------------------|
| [CharacterEvent][38]      |                                                                        |
| [Color][39]               | Color encoded as 4 component floats.                                   |
| [ColorBytes][40]          | Color encoded as 4 component bytes.                                    |
| [FontMetrics][41]         | Contains information about a font (ie, the vertical metrics).          |
| [GlyphMetrics][42]        | Contains information about a glyph (ie, the horizontal metrics).       |
| [Graphics.DrawCounts][43] |                                                                        |
| [GraphicsAdapterInfo][44] |                                                                        |
| [IntRange][45]            | Represents a range of integers from IntRange.Min to IntRange.Max .     |
| [IntRectangle][46]        | Represents a rectangle defined with integer coordinates.               |
| [IntSize][47]             | Represents a size or dimensions defined with integer fields.           |
| [IntVector][48]           | Represents a vector with two integer values.                           |
| [KeyEvent][49]            |                                                                        |
| [Matrix][50]              | A 2x3 transformation matrix.                                           |
| [MouseButtonEvent][51]    |                                                                        |
| [MouseMoveEvent][52]      |                                                                        |
| [MouseScrollEvent][53]    |                                                                        |
| [Range][54]               | Represents a range of single-precision floating point numbers from ... |
| [Rectangle][55]           |                                                                        |
| [Size][56]                |                                                                        |
| [Statistics][57]          | Represents statistics of some data.                                    |
| [TextDrawState][58]       | Represents information of any particular glyph when drawing text.      |
| [TextLayoutState][59]     | Represents information of any particular glyph during text layout.     |
| [Touch][60]               |                                                                        |
| [TouchEvent][61]          |                                                                        |
| [UnicodeCharacter][62]    | Represents a single 32 bit Unicode character.                          |
| [UnicodeRange][63]        | Represents a range of unicode 32 bit code points.                      |
| [Vector][64]              | Represents a vector with two single-precision floating-point values.   |
| [Vertex][65]              | Represents a vertex of Mesh .                                          |

### Interface

| Name                                  | Summary                                                     |
|---------------------------------------|-------------------------------------------------------------|
| [GraphicsAdapter.IShaderFactory][66]  |                                                             |
| [GraphicsAdapter.ISurfaceFactory][67] |                                                             |
| [IInputSource][68]                    | Represents the functionality of an input source.            |
| [ILogHandler][69]                     |                                                             |
| [INoise1D][70]                        | Provides an interface for sampling one-dimensional noise.   |
| [INoise2D][71]                        | Provides an interface for sampling two-dimensional noise.   |
| [INoise3D][72]                        | Provides an interface for sampling three-dimensional noise. |

### Enum

| Name                         | Summary                                                                |
|------------------------------|------------------------------------------------------------------------|
| [Axis][73]                   | Represents an axis of the 2D plane.                                    |
| [Blending][74]               | Controls how drawing operations are blended into existing pixels.      |
| [ButtonState][75]            | Represents the state of a button.                                      |
| [GamepadAxis][76]            |                                                                        |
| [GamepadButton][77]          |                                                                        |
| [InterpolationMode][78]      | Represents the behaviour when sampling an image on a non-integer co... |
| [Key][79]                    | Standard virtual key mapping (standard US keyboard layout) from GLFW.  |
| [KeyModifiers][80]           |                                                                        |
| [LogVerbosity][81]           | Controls the verbosity of Log .                                        |
| [MouseButton][82]            |                                                                        |
| [MultisampleQuality][83]     | Multisampling levels                                                   |
| [PackingAlgorithm][84]       | An enumeration of rectangle packing algorithms.                        |
| [PerformanceOverlayMode][85] | Controls showing the performance overlay on a Graphics object.         |
| [RepeatMode][86]             | Represents the behaviour when sampling an image outside its natural... |
| [SurfaceType][87]            | Represents the surface type.                                           |
| [TextAlign][88]              | Controls how text is aligned to the layout rectangle.                  |
| [TimeUnit][89]               | Represents units of time, such as a millisecond.                       |
| [UniformType][90]            | Represents the type of a uniform in a Shader .                         |

### Delegate

| Name                     | Summary                                                     |
|--------------------------|-------------------------------------------------------------|
| [ActualCost\<T>][91]     | Gets the known cost between two values.                     |
| [DrawTextCallback][92]   | Delegate type for the callback when drawing text.           |
| [HeuristicCost\<T>][93]  | Gets the estimated cost of the some value.                  |
| [TextLayoutCallback][94] | Delegate type for the callback when performing text layout. |

## Heirloom.Collections

### Class

| Name                               | Summary                                                                |
|------------------------------------|------------------------------------------------------------------------|
| [BvhSpatialCollection\<T>][95]     | A spatial collection to store and query elements in 2D space, imple... |
| [DirectedGraph\<T>][96]            |                                                                        |
| [FreeList\<T>][97]                 | A free list an allocation-centric data structure that allows insert... |
| [Grid\<T>][98]                     | A finite grid (bounded by size) of values.                             |
| [GridUtilities][99]                | Provides extra utilities for interacting with a grid.                  |
| [Heap\<T>][100]                    | Represents a heap data structure. Allows the insertion and removal ... |
| [LinearSpatialCollection\<T>][101] | DO NOT USE! This is incredibly slow, but useful for behaviour testi... |
| [SparseGrid\<T>][102]              | An infinite, sparse grid of values.                                    |
| [TypeDictionary\<T>][103]          | Manages objects by their type hierarchy up to the base type, allowi... |
| [UndirectedGraph\<T>][104]         |                                                                        |

### Interface

| Name                                  | Summary                                                                |
|---------------------------------------|------------------------------------------------------------------------|
| [IFiniteGrid\<T>][105]                | A finite grid (bounded by IFiniteGrid<T>.Width and IFiniteGrid<T>.H... |
| [IGraph\<T>][106]                     |                                                                        |
| [IGrid\<T>][107]                      | A 2D grid of values.                                                   |
| [IHeap\<T>][108]                      | Represents a heap data structure. Allowing the access and removal o... |
| [IReadOnlyGrid\<T>][109]              | A read-only view of a 2D grid of values.                               |
| [IReadOnlyHeap\<T>][110]              | Represents a read-only view of a Heap<T> .                             |
| [IReadOnlySparseGrid\<T>][111]        | A sparse 2D grid of values.                                            |
| [IReadOnlySpatialCollection\<T>][112] | A read-only view of a spatial collection to query elements in 2D sp... |
| [IReadOnlyTypeDictionary\<T>][113]    | A read-only view of ITypeDictionary<T> .                               |
| [ISparseGrid\<T>][114]                | A sparse 2D grid of values.                                            |
| [ISpatialCollection\<T>][115]         | A spatial collection to store and query elements in 2D space.          |
| [ISpatialQuery\<T>][116]              | Provides methods for querying elements in 2D space.                    |
| [ITypeDictionary\<T>][117]            | Manages objects by their type hierarchy up to the base type, allowi... |

### Enum

| Name                    | Summary                                      |
|-------------------------|----------------------------------------------|
| [GridNeighborType][118] | Describes the choice of neighbors in a grid. |
| [HeapType][119]         | Describes the behaviour of a Heap<T> .       |
| [TraversalMethod][120]  | Represents a choice of traversing a graph.   |

## Heirloom.Geometry

### Class

| Name                 | Summary                                                                |
|----------------------|------------------------------------------------------------------------|
| [Curve][121]         |                                                                        |
| [CurveTools][122]    | Utility function for computation with quadratic and cubic curves.      |
| [GeometryTools][123] | Provides utilities for generating and manipulating shapes.             |
| [Polygon][124]       | Represents a simple polygon.                                           |
| [PolygonTools][125]  | Provides several operations for polygons represented as a read-only... |

### Struct

| Name               | Summary                                                 |
|--------------------|---------------------------------------------------------|
| [Circle][126]      | Represents a circle via center position and radius.     |
| [LineSegment][127] | Represents a line segment defined by two end points.    |
| [Ray][128]         | Represents a ray by orgin point and directional vector. |
| [RayContact][129]  | Represents the result of a ray to shape intersection.   |
| [Triangle][130]    |                                                         |

### Interface

| Name          | Summary                                                                |
|---------------|------------------------------------------------------------------------|
| [IShape][131] | Represents the general interface of a shape and common operators ea... |

### Enum

| Name             | Summary |
|------------------|---------|
| [CurveType][132] |         |

## Heirloom.IO

### Class

| Name                | Summary                                                        |
|---------------------|----------------------------------------------------------------|
| [EmbeddedFile][133] | Represents an embedded file.                                   |
| [Files][134]        | A utility to unify access of embedded files and files on disk. |

### Struct

| Name            | Summary                                                                |
|-----------------|------------------------------------------------------------------------|
| [BitField][135] | A structured byte to configure the 8 individual bits as a method of... |

## Heirloom.Sound

### Class

| Name                  | Summary                                                                |
|-----------------------|------------------------------------------------------------------------|
| [AudioClip][136]      | An object to contain (and decode) audio data into raw samples.         |
| [AudioEffect][137]    | An audio effect. Implementations of this class mutate the audio for... |
| [BandPassFilter][138] | An audio effect that implements a band pass filter.                    |
| [HighPassFilter][139] | An audio effect that implements a high pass filter.                    |
| [LowPassFilter][140]  | An audio effect that implements a low pass filter.                     |
| [ReverbEffect][141]   | An audio effect that implements a Schroeder reverb.                    |
| [AudioNode][142]      | Represents a node in the audio mixing tree.                            |
| [AudioGroup][143]     | An AudioNode to mix and apply effects to a group of other nodes.       |
| [AudioSource][144]    | An instance of playable audio.                                         |

### Delegate

| Name                        | Summary |
|-----------------------------|---------|
| [AudioCaptureCallback][145] |         |

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
[11]: Heirloom.Core/Heirloom/Input.md
[12]: Heirloom.Core/Heirloom/Interval.md
[13]: Heirloom.Core/Heirloom/Log.md
[14]: Heirloom.Core/Heirloom/MergeSort.md
[15]: Heirloom.Core/Heirloom/Mesh.md
[16]: Heirloom.Core/Heirloom/NineSlice.md
[17]: Heirloom.Core/Heirloom/PerlinNoise.md
[18]: Heirloom.Core/Heirloom/Rasterizer.md
[19]: Heirloom.Core/Heirloom/RectanglePacker[T].md
[20]: Heirloom.Core/Heirloom/Screen.md
[21]: Heirloom.Core/Heirloom/Search.md
[22]: Heirloom.Core/Heirloom/Shader.md
[23]: Heirloom.Core/Heirloom/DistortionShader.md
[24]: Heirloom.Core/Heirloom/GrayscaleShader.md
[25]: Heirloom.Core/Heirloom/InvertShader.md
[26]: Heirloom.Core/Heirloom/VectorBlurShader.md
[27]: Heirloom.Core/Heirloom/SimplexNoise.md
[28]: Heirloom.Core/Heirloom/Sprite.md
[29]: Heirloom.Core/Heirloom/SpriteBuilder.md
[30]: Heirloom.Core/Heirloom/StyledText.md
[31]: Heirloom.Core/Heirloom/StyledTextParser.md
[32]: Heirloom.Core/Heirloom/StandardStyledTextParser.md
[33]: Heirloom.Core/Heirloom/SurfaceEffect.md
[34]: Heirloom.Core/Heirloom/SurfacePool.md
[35]: Heirloom.Core/Heirloom/TextLayout.md
[36]: Heirloom.Core/Heirloom/Time.md
[37]: Heirloom.Core/Heirloom/UniformInfo.md
[38]: Heirloom.Core/Heirloom/CharacterEvent.md
[39]: Heirloom.Core/Heirloom/Color.md
[40]: Heirloom.Core/Heirloom/ColorBytes.md
[41]: Heirloom.Core/Heirloom/FontMetrics.md
[42]: Heirloom.Core/Heirloom/GlyphMetrics.md
[43]: Heirloom.Core/Heirloom/Graphics.DrawCounts.md
[44]: Heirloom.Core/Heirloom/GraphicsAdapterInfo.md
[45]: Heirloom.Core/Heirloom/IntRange.md
[46]: Heirloom.Core/Heirloom/IntRectangle.md
[47]: Heirloom.Core/Heirloom/IntSize.md
[48]: Heirloom.Core/Heirloom/IntVector.md
[49]: Heirloom.Core/Heirloom/KeyEvent.md
[50]: Heirloom.Core/Heirloom/Matrix.md
[51]: Heirloom.Core/Heirloom/MouseButtonEvent.md
[52]: Heirloom.Core/Heirloom/MouseMoveEvent.md
[53]: Heirloom.Core/Heirloom/MouseScrollEvent.md
[54]: Heirloom.Core/Heirloom/Range.md
[55]: Heirloom.Core/Heirloom/Rectangle.md
[56]: Heirloom.Core/Heirloom/Size.md
[57]: Heirloom.Core/Heirloom/Statistics.md
[58]: Heirloom.Core/Heirloom/TextDrawState.md
[59]: Heirloom.Core/Heirloom/TextLayoutState.md
[60]: Heirloom.Core/Heirloom/Touch.md
[61]: Heirloom.Core/Heirloom/TouchEvent.md
[62]: Heirloom.Core/Heirloom/UnicodeCharacter.md
[63]: Heirloom.Core/Heirloom/UnicodeRange.md
[64]: Heirloom.Core/Heirloom/Vector.md
[65]: Heirloom.Core/Heirloom/Vertex.md
[66]: Heirloom.Core/Heirloom/GraphicsAdapter.IShaderFactory.md
[67]: Heirloom.Core/Heirloom/GraphicsAdapter.ISurfaceFactory.md
[68]: Heirloom.Core/Heirloom/IInputSource.md
[69]: Heirloom.Core/Heirloom/ILogHandler.md
[70]: Heirloom.Core/Heirloom/INoise1D.md
[71]: Heirloom.Core/Heirloom/INoise2D.md
[72]: Heirloom.Core/Heirloom/INoise3D.md
[73]: Heirloom.Core/Heirloom/Axis.md
[74]: Heirloom.Core/Heirloom/Blending.md
[75]: Heirloom.Core/Heirloom/ButtonState.md
[76]: Heirloom.Core/Heirloom/GamepadAxis.md
[77]: Heirloom.Core/Heirloom/GamepadButton.md
[78]: Heirloom.Core/Heirloom/InterpolationMode.md
[79]: Heirloom.Core/Heirloom/Key.md
[80]: Heirloom.Core/Heirloom/KeyModifiers.md
[81]: Heirloom.Core/Heirloom/LogVerbosity.md
[82]: Heirloom.Core/Heirloom/MouseButton.md
[83]: Heirloom.Core/Heirloom/MultisampleQuality.md
[84]: Heirloom.Core/Heirloom/PackingAlgorithm.md
[85]: Heirloom.Core/Heirloom/PerformanceOverlayMode.md
[86]: Heirloom.Core/Heirloom/RepeatMode.md
[87]: Heirloom.Core/Heirloom/SurfaceType.md
[88]: Heirloom.Core/Heirloom/TextAlign.md
[89]: Heirloom.Core/Heirloom/TimeUnit.md
[90]: Heirloom.Core/Heirloom/UniformType.md
[91]: Heirloom.Core/Heirloom/ActualCost[T].md
[92]: Heirloom.Core/Heirloom/DrawTextCallback.md
[93]: Heirloom.Core/Heirloom/HeuristicCost[T].md
[94]: Heirloom.Core/Heirloom/TextLayoutCallback.md
[95]: Heirloom.Core/Heirloom.Collections/BvhSpatialCollection[T].md
[96]: Heirloom.Core/Heirloom.Collections/DirectedGraph[T].md
[97]: Heirloom.Core/Heirloom.Collections/FreeList[T].md
[98]: Heirloom.Core/Heirloom.Collections/Grid[T].md
[99]: Heirloom.Core/Heirloom.Collections/GridUtilities.md
[100]: Heirloom.Core/Heirloom.Collections/Heap[T].md
[101]: Heirloom.Core/Heirloom.Collections/LinearSpatialCollection[T].md
[102]: Heirloom.Core/Heirloom.Collections/SparseGrid[T].md
[103]: Heirloom.Core/Heirloom.Collections/TypeDictionary[T].md
[104]: Heirloom.Core/Heirloom.Collections/UndirectedGraph[T].md
[105]: Heirloom.Core/Heirloom.Collections/IFiniteGrid[T].md
[106]: Heirloom.Core/Heirloom.Collections/IGraph[T].md
[107]: Heirloom.Core/Heirloom.Collections/IGrid[T].md
[108]: Heirloom.Core/Heirloom.Collections/IHeap[T].md
[109]: Heirloom.Core/Heirloom.Collections/IReadOnlyGrid[T].md
[110]: Heirloom.Core/Heirloom.Collections/IReadOnlyHeap[T].md
[111]: Heirloom.Core/Heirloom.Collections/IReadOnlySparseGrid[T].md
[112]: Heirloom.Core/Heirloom.Collections/IReadOnlySpatialCollection[T].md
[113]: Heirloom.Core/Heirloom.Collections/IReadOnlyTypeDictionary[T].md
[114]: Heirloom.Core/Heirloom.Collections/ISparseGrid[T].md
[115]: Heirloom.Core/Heirloom.Collections/ISpatialCollection[T].md
[116]: Heirloom.Core/Heirloom.Collections/ISpatialQuery[T].md
[117]: Heirloom.Core/Heirloom.Collections/ITypeDictionary[T].md
[118]: Heirloom.Core/Heirloom.Collections/GridNeighborType.md
[119]: Heirloom.Core/Heirloom.Collections/HeapType.md
[120]: Heirloom.Core/Heirloom.Collections/TraversalMethod.md
[121]: Heirloom.Core/Heirloom.Geometry/Curve.md
[122]: Heirloom.Core/Heirloom.Geometry/CurveTools.md
[123]: Heirloom.Core/Heirloom.Geometry/GeometryTools.md
[124]: Heirloom.Core/Heirloom.Geometry/Polygon.md
[125]: Heirloom.Core/Heirloom.Geometry/PolygonTools.md
[126]: Heirloom.Core/Heirloom.Geometry/Circle.md
[127]: Heirloom.Core/Heirloom.Geometry/LineSegment.md
[128]: Heirloom.Core/Heirloom.Geometry/Ray.md
[129]: Heirloom.Core/Heirloom.Geometry/RayContact.md
[130]: Heirloom.Core/Heirloom.Geometry/Triangle.md
[131]: Heirloom.Core/Heirloom.Geometry/IShape.md
[132]: Heirloom.Core/Heirloom.Geometry/CurveType.md
[133]: Heirloom.Core/Heirloom.IO/EmbeddedFile.md
[134]: Heirloom.Core/Heirloom.IO/Files.md
[135]: Heirloom.Core/Heirloom.IO/BitField.md
[136]: Heirloom.Core/Heirloom.Sound/AudioClip.md
[137]: Heirloom.Core/Heirloom.Sound/AudioEffect.md
[138]: Heirloom.Core/Heirloom.Sound/BandPassFilter.md
[139]: Heirloom.Core/Heirloom.Sound/HighPassFilter.md
[140]: Heirloom.Core/Heirloom.Sound/LowPassFilter.md
[141]: Heirloom.Core/Heirloom.Sound/ReverbEffect.md
[142]: Heirloom.Core/Heirloom.Sound/AudioNode.md
[143]: Heirloom.Core/Heirloom.Sound/AudioGroup.md
[144]: Heirloom.Core/Heirloom.Sound/AudioSource.md
[145]: Heirloom.Core/Heirloom.Sound/AudioCaptureCallback.md
