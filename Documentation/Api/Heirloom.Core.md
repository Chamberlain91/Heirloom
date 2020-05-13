# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Heirloom

### Class

| Name                           | Summary                                                                |
|--------------------------------|------------------------------------------------------------------------|
| [Calc][1]                      | Provides various math operations for float and int types including ... |
| [Extensions][2]                | Provides extension methods various types within Heirloom.              |
| [Font][3]                      | An object to represent a truetype font. Provides functionality to q... |
| [GameLoop][4]                  | Provides a thread to manage invoking a render/update function conti... |
| [Glyph][5]                     | A glyph represents the metrics and rendering of a character from th... |
| [GraphicsContext][6]           |                                                                        |
| [ImageSource][7]               |                                                                        |
| [Image][8]                     |                                                                        |
| [Surface][9]                   | Represents a surface a GraphicsContext object can draw on.             |
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
| [Sprite][27]                   | A representation of an animated sprite. May also contains per-frame... |
| [SpriteAnimation][28]          | Represents an image based per frame animation.                         |
| [SpriteFrame][29]              | Represents a single frame of a SpriteAnimation .                       |
| [SpritePlayer][30]             | A utility class to help drive sprite based animation.                  |
| [StyledText][31]               | Styled text compiled by a StyledTextParser .                           |
| [StyledTextParser][32]         | Provides an ability to parse text with some sort of markup into Sty... |
| [StandardStyledTextParser][33] | Provides implementation of a BBCode-esque text markup parser.          |
| [SurfaceEffect][34]            |                                                                        |
| [SurfacePool][35]              | Provides a mechanism for requesting temporary surfaces and recyclin... |
| [TextLayout][36]               | Utility to measure text and manually invoke the text layout functio... |
| [Time][37]                     |                                                                        |
| [UniformInfo][38]              | Contains information of a uniform from a Shader .                      |

### Struct

| Name                             | Summary                                                                |
|----------------------------------|------------------------------------------------------------------------|
| [CharacterEvent][39]             |                                                                        |
| [Color][40]                      | Color encoded as 4 component floats.                                   |
| [ColorBytes][41]                 | Color encoded as 4 component bytes.                                    |
| [FontMetrics][42]                | Contains information about a font (ie, the vertical metrics).          |
| [GlyphMetrics][43]               | Contains information about a glyph (ie, the horizontal metrics).       |
| [GraphicsContext.DrawCounts][44] |                                                                        |
| [IntRange][45]                   | Represents a range of integers from IntRange.Min to IntRange.Max .     |
| [IntRectangle][46]               | Represents a rectangle defined with integer coordinates.               |
| [IntSize][47]                    | Represents a size or dimensions defined with integer fields.           |
| [IntVector][48]                  | Represents a vector with two integer values.                           |
| [KeyEvent][49]                   |                                                                        |
| [Matrix][50]                     | A 2x3 transformation matrix.                                           |
| [MouseButtonEvent][51]           |                                                                        |
| [MouseMoveEvent][52]             |                                                                        |
| [MouseScrollEvent][53]           |                                                                        |
| [Range][54]                      | Represents a range of single-precision floating point numbers from ... |
| [Rectangle][55]                  |                                                                        |
| [Size][56]                       |                                                                        |
| [Statistics][57]                 | Represents statistics of some data.                                    |
| [TextDrawState][58]              | Represents information of any particular glyph when drawing text.      |
| [TextLayoutState][59]            | Represents information of any particular glyph during text layout.     |
| [Touch][60]                      |                                                                        |
| [TouchEvent][61]                 |                                                                        |
| [UnicodeCharacter][62]           | Represents a single 32 bit Unicode character.                          |
| [UnicodeRange][63]               | Represents a range of unicode 32 bit code points.                      |
| [Vector][64]                     | Represents a vector with two single-precision floating-point values.   |
| [Vertex][65]                     | Represents a vertex of Mesh .                                          |

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
| [AnimationDirection][73]     | Represents animation direction options.                                |
| [Axis][74]                   | Represents an axis of the 2D plane.                                    |
| [Blending][75]               | Controls how drawing operations are blended into existing pixels.      |
| [ButtonState][76]            | Represents the state of a button.                                      |
| [GamepadAxis][77]            |                                                                        |
| [GamepadButton][78]          |                                                                        |
| [InterpolationMode][79]      | Represents the behaviour when sampling an image on a non-integer co... |
| [Key][80]                    | Standard virtual key mapping (standard US keyboard layout) from GLFW.  |
| [KeyModifiers][81]           |                                                                        |
| [LogVerbosity][82]           | Controls the verbosity of Log .                                        |
| [MouseButton][83]            |                                                                        |
| [MultisampleQuality][84]     | Multisampling levels                                                   |
| [PackingAlgorithm][85]       | An enumeration of rectangle packing algorithms.                        |
| [PerformanceOverlayMode][86] | Controls showing the performance overlay on a GraphicsContext object.  |
| [RepeatMode][87]             | Represents the behaviour when sampling an image outside its natural... |
| [SurfaceType][88]            | Represents the surface type.                                           |
| [TextAlign][89]              | Controls how text is aligned to the layout rectangle.                  |
| [TimeUnit][90]               | Represents units of time, such as a millisecond.                       |
| [UniformType][91]            | Represents the type of a uniform in a Shader .                         |

### Delegate

| Name                     | Summary                                                     |
|--------------------------|-------------------------------------------------------------|
| [ActualCost\<T>][92]     | Gets the known cost between two values.                     |
| [DrawTextCallback][93]   | Delegate type for the callback when drawing text.           |
| [HeuristicCost\<T>][94]  | Gets the estimated cost of the some value.                  |
| [TextLayoutCallback][95] | Delegate type for the callback when performing text layout. |

## Heirloom.Collections

### Class

| Name                                   | Summary                                                                |
|----------------------------------------|------------------------------------------------------------------------|
| [BvhSpatialCollection\<T>][96]         | A spatial collection to store and query elements in 2D space, imple... |
| [DirectedGraph\<T>][97]                | A directed graph implemented using adjacency lists.                    |
| [FreeList\<T>][98]                     | A free list an allocation-centric data structure that allows insert... |
| [Graph\<T>][99]                        | An undirected graph implemented using adjacency lists.                 |
| [Grid\<T>][100]                        | A finite grid (bounded by size) of values.                             |
| [GridUtilities][101]                   | Provides extra utilities for interacting with a grid.                  |
| [Heap\<T>][102]                        | Represents a heap data structure. Allows the insertion and removal ... |
| [ObjectPool\<T>][103]                  | Implements an object pool to recycle objects and reduce allocatio s... |
| [SparseGrid\<T>][104]                  | An infinite, sparse grid of values.                                    |
| [SparseGridSpatialCollection\<T>][105] |                                                                        |
| [TypeDictionary\<T>][106]              | Manages objects by their type hierarchy up to the base type, allowi... |

### Interface

| Name                                  | Summary                                                                |
|---------------------------------------|------------------------------------------------------------------------|
| [IDirectedGraph\<T>][107]             | An interface that represents a graph.                                  |
| [IFiniteGrid\<T>][108]                | A finite grid (bounded by IFiniteGrid<T>.Width and IFiniteGrid<T>.H... |
| [IGraph\<T>][109]                     | An interface that represents a graph.                                  |
| [IGrid\<T>][110]                      | A 2D grid of values.                                                   |
| [IHeap\<T>][111]                      | Represents a heap data structure. Allowing the access and removal o... |
| [IReadOnlyGrid\<T>][112]              | A read-only view of a 2D grid of values.                               |
| [IReadOnlyHeap\<T>][113]              | Represents a read-only view of a Heap<T> .                             |
| [IReadOnlySparseGrid\<T>][114]        | A sparse 2D grid of values.                                            |
| [IReadOnlySpatialCollection\<T>][115] | A read-only view of a spatial collection to query elements in 2D sp... |
| [IReadOnlyTypeDictionary\<T>][116]    | A read-only view of ITypeDictionary<T> .                               |
| [ISparseGrid\<T>][117]                | A sparse 2D grid of values.                                            |
| [ISpatialCollection\<T>][118]         | A spatial collection to store and query elements in 2D space.          |
| [ISpatialQuery\<T>][119]              | Provides methods for querying elements in 2D space.                    |
| [ITypeDictionary\<T>][120]            | Manages objects by their type hierarchy up to the base type, allowi... |

### Enum

| Name                    | Summary                                      |
|-------------------------|----------------------------------------------|
| [GridNeighborType][121] | Describes the choice of neighbors in a grid. |
| [HeapType][122]         | Describes the behaviour of a Heap<T> .       |
| [TraversalMethod][123]  | Represents a choice of traversing a graph.   |

## Heirloom.Geometry

### Class

| Name                  | Summary                                                                |
|-----------------------|------------------------------------------------------------------------|
| [Collision][124]      | Collision detection routines.                                          |
| [Curve][125]          | An implementation of a multi-point bezier curve using multiple 'seg... |
| [CurveTools][126]     | Utility function for computation with quadratic and cubic curves.      |
| [GeometryTools][127]  | Provides utilities for generating and manipulating shapes.             |
| [Polygon][128]        | Represents a simple polygon.                                           |
| [PolygonTools][129]   | Provides several operations for polygons represented as a read-only... |
| [SeparatingAxis][130] | Implementation of 2D collisions/overlap using separating axis theorem. |

### Struct

| Name                 | Summary                                                     |
|----------------------|-------------------------------------------------------------|
| [Circle][131]        | Represents a circle via center position and radius.         |
| [CollisionData][132] | Contains the results of a collision function in Collision . |
| [LineSegment][133]   | Represents a line segment defined by two end points.        |
| [Ray][134]           | Represents a ray by orgin point and directional vector.     |
| [RayContact][135]    | Represents the result of a ray to shape intersection.       |
| [Triangle][136]      | Represents a triangle shape defined by three points.        |

### Interface

| Name          | Summary                                                                |
|---------------|------------------------------------------------------------------------|
| [IShape][137] | Represents the general interface of a shape and common operators ea... |

### Enum

| Name             | Summary                       |
|------------------|-------------------------------|
| [CurveType][138] | Represents the type of curve. |

## Heirloom.IO

### Class

| Name                | Summary                                                        |
|---------------------|----------------------------------------------------------------|
| [EmbeddedFile][139] | Represents an embedded file.                                   |
| [Files][140]        | A utility to unify access of embedded files and files on disk. |

## Heirloom.Sound

### Class

| Name                  | Summary                                                                |
|-----------------------|------------------------------------------------------------------------|
| [AudioAdapter][141]   | The abstraction of a low level audio system.                           |
| [AudioClip][142]      | An object to contain (and decode) audio data into raw samples.         |
| [AudioEffect][143]    | An abstarct representation of an audio effect. Implementations of t... |
| [BandPassFilter][144] | An audio effect that implements a band pass filter.                    |
| [HighPassFilter][145] | An audio effect that implements a high pass filter.                    |
| [LowPassFilter][146]  | An audio effect that implements a low pass filter.                     |
| [ReverbEffect][147]   | An audio effect that implements a Schroeder reverb.                    |
| [AudioNode][148]      | Represents a node in the audio mixing tree.                            |
| [AudioGroup][149]     | An AudioNode to mix and apply effects to a group of other nodes.       |
| [AudioSource][150]    | An instance of playable audio.                                         |

### Interface

| Name                 | Summary                                     |
|----------------------|---------------------------------------------|
| [IAudioDecoder][151] | An interface representing an audio decoder. |

### Delegate

| Name                        | Summary                                                                |
|-----------------------------|------------------------------------------------------------------------|
| [AudioCaptureCallback][152] | A delegate for a callback when audio samples are captured by a inpu... |

[0]: Heirloom.Core.md
[1]: Heirloom.Core/Heirloom/Calc.md
[2]: Heirloom.Core/Heirloom/Extensions.md
[3]: Heirloom.Core/Heirloom/Font.md
[4]: Heirloom.Core/Heirloom/GameLoop.md
[5]: Heirloom.Core/Heirloom/Glyph.md
[6]: Heirloom.Core/Heirloom/GraphicsContext.md
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
[28]: Heirloom.Core/Heirloom/SpriteAnimation.md
[29]: Heirloom.Core/Heirloom/SpriteFrame.md
[30]: Heirloom.Core/Heirloom/SpritePlayer.md
[31]: Heirloom.Core/Heirloom/StyledText.md
[32]: Heirloom.Core/Heirloom/StyledTextParser.md
[33]: Heirloom.Core/Heirloom/StandardStyledTextParser.md
[34]: Heirloom.Core/Heirloom/SurfaceEffect.md
[35]: Heirloom.Core/Heirloom/SurfacePool.md
[36]: Heirloom.Core/Heirloom/TextLayout.md
[37]: Heirloom.Core/Heirloom/Time.md
[38]: Heirloom.Core/Heirloom/UniformInfo.md
[39]: Heirloom.Core/Heirloom/CharacterEvent.md
[40]: Heirloom.Core/Heirloom/Color.md
[41]: Heirloom.Core/Heirloom/ColorBytes.md
[42]: Heirloom.Core/Heirloom/FontMetrics.md
[43]: Heirloom.Core/Heirloom/GlyphMetrics.md
[44]: Heirloom.Core/Heirloom/GraphicsContext.DrawCounts.md
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
[73]: Heirloom.Core/Heirloom/AnimationDirection.md
[74]: Heirloom.Core/Heirloom/Axis.md
[75]: Heirloom.Core/Heirloom/Blending.md
[76]: Heirloom.Core/Heirloom/ButtonState.md
[77]: Heirloom.Core/Heirloom/GamepadAxis.md
[78]: Heirloom.Core/Heirloom/GamepadButton.md
[79]: Heirloom.Core/Heirloom/InterpolationMode.md
[80]: Heirloom.Core/Heirloom/Key.md
[81]: Heirloom.Core/Heirloom/KeyModifiers.md
[82]: Heirloom.Core/Heirloom/LogVerbosity.md
[83]: Heirloom.Core/Heirloom/MouseButton.md
[84]: Heirloom.Core/Heirloom/MultisampleQuality.md
[85]: Heirloom.Core/Heirloom/PackingAlgorithm.md
[86]: Heirloom.Core/Heirloom/PerformanceOverlayMode.md
[87]: Heirloom.Core/Heirloom/RepeatMode.md
[88]: Heirloom.Core/Heirloom/SurfaceType.md
[89]: Heirloom.Core/Heirloom/TextAlign.md
[90]: Heirloom.Core/Heirloom/TimeUnit.md
[91]: Heirloom.Core/Heirloom/UniformType.md
[92]: Heirloom.Core/Heirloom/ActualCost[T].md
[93]: Heirloom.Core/Heirloom/DrawTextCallback.md
[94]: Heirloom.Core/Heirloom/HeuristicCost[T].md
[95]: Heirloom.Core/Heirloom/TextLayoutCallback.md
[96]: Heirloom.Core/Heirloom.Collections/BvhSpatialCollection[T].md
[97]: Heirloom.Core/Heirloom.Collections/DirectedGraph[T].md
[98]: Heirloom.Core/Heirloom.Collections/FreeList[T].md
[99]: Heirloom.Core/Heirloom.Collections/Graph[T].md
[100]: Heirloom.Core/Heirloom.Collections/Grid[T].md
[101]: Heirloom.Core/Heirloom.Collections/GridUtilities.md
[102]: Heirloom.Core/Heirloom.Collections/Heap[T].md
[103]: Heirloom.Core/Heirloom.Collections/ObjectPool[T].md
[104]: Heirloom.Core/Heirloom.Collections/SparseGrid[T].md
[105]: Heirloom.Core/Heirloom.Collections/SparseGridSpatialCollection[T].md
[106]: Heirloom.Core/Heirloom.Collections/TypeDictionary[T].md
[107]: Heirloom.Core/Heirloom.Collections/IDirectedGraph[T].md
[108]: Heirloom.Core/Heirloom.Collections/IFiniteGrid[T].md
[109]: Heirloom.Core/Heirloom.Collections/IGraph[T].md
[110]: Heirloom.Core/Heirloom.Collections/IGrid[T].md
[111]: Heirloom.Core/Heirloom.Collections/IHeap[T].md
[112]: Heirloom.Core/Heirloom.Collections/IReadOnlyGrid[T].md
[113]: Heirloom.Core/Heirloom.Collections/IReadOnlyHeap[T].md
[114]: Heirloom.Core/Heirloom.Collections/IReadOnlySparseGrid[T].md
[115]: Heirloom.Core/Heirloom.Collections/IReadOnlySpatialCollection[T].md
[116]: Heirloom.Core/Heirloom.Collections/IReadOnlyTypeDictionary[T].md
[117]: Heirloom.Core/Heirloom.Collections/ISparseGrid[T].md
[118]: Heirloom.Core/Heirloom.Collections/ISpatialCollection[T].md
[119]: Heirloom.Core/Heirloom.Collections/ISpatialQuery[T].md
[120]: Heirloom.Core/Heirloom.Collections/ITypeDictionary[T].md
[121]: Heirloom.Core/Heirloom.Collections/GridNeighborType.md
[122]: Heirloom.Core/Heirloom.Collections/HeapType.md
[123]: Heirloom.Core/Heirloom.Collections/TraversalMethod.md
[124]: Heirloom.Core/Heirloom.Geometry/Collision.md
[125]: Heirloom.Core/Heirloom.Geometry/Curve.md
[126]: Heirloom.Core/Heirloom.Geometry/CurveTools.md
[127]: Heirloom.Core/Heirloom.Geometry/GeometryTools.md
[128]: Heirloom.Core/Heirloom.Geometry/Polygon.md
[129]: Heirloom.Core/Heirloom.Geometry/PolygonTools.md
[130]: Heirloom.Core/Heirloom.Geometry/SeparatingAxis.md
[131]: Heirloom.Core/Heirloom.Geometry/Circle.md
[132]: Heirloom.Core/Heirloom.Geometry/CollisionData.md
[133]: Heirloom.Core/Heirloom.Geometry/LineSegment.md
[134]: Heirloom.Core/Heirloom.Geometry/Ray.md
[135]: Heirloom.Core/Heirloom.Geometry/RayContact.md
[136]: Heirloom.Core/Heirloom.Geometry/Triangle.md
[137]: Heirloom.Core/Heirloom.Geometry/IShape.md
[138]: Heirloom.Core/Heirloom.Geometry/CurveType.md
[139]: Heirloom.Core/Heirloom.IO/EmbeddedFile.md
[140]: Heirloom.Core/Heirloom.IO/Files.md
[141]: Heirloom.Core/Heirloom.Sound/AudioAdapter.md
[142]: Heirloom.Core/Heirloom.Sound/AudioClip.md
[143]: Heirloom.Core/Heirloom.Sound/AudioEffect.md
[144]: Heirloom.Core/Heirloom.Sound/BandPassFilter.md
[145]: Heirloom.Core/Heirloom.Sound/HighPassFilter.md
[146]: Heirloom.Core/Heirloom.Sound/LowPassFilter.md
[147]: Heirloom.Core/Heirloom.Sound/ReverbEffect.md
[148]: Heirloom.Core/Heirloom.Sound/AudioNode.md
[149]: Heirloom.Core/Heirloom.Sound/AudioGroup.md
[150]: Heirloom.Core/Heirloom.Sound/AudioSource.md
[151]: Heirloom.Core/Heirloom.Sound/IAudioDecoder.md
[152]: Heirloom.Core/Heirloom.Sound/AudioCaptureCallback.md
