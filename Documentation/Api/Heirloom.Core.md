# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Heirloom

| Name                                    | Summary                                                                |
|-----------------------------------------|------------------------------------------------------------------------|
| [Calc][1]                               | Provides various math operations for float and int types including ... |
| [Extensions][2]                         | Provides extension methods various types within Heirloom.              |
| [Font][3]                               | An object to represent a truetype font. Provides functionality to q... |
| [GameLoop][4]                           | Provides a thread to manage invoking a render/update function conti... |
| [Glyph][5]                              | A glyph represents the metrics and rendering of a character from th... |
| [GraphicsContext][6]                    | Represents the graphical context for performing drawing operations ... |
| [GraphicsContext.PerformanceMetrics][7] | Contains information pertaining to draw performance.                   |
| [ImageSource][8]                        | Represents the abstract representation of an image.                    |
| [Image][9]                              | Represents an image as a grid of ColorBytes .                          |
| [Surface][10]                           | Represents a surface a GraphicsContext object can draw on.             |
| [Input][11]                             | Provides a centralized query style input layer. This is useful for ... |
| [Interval][12]                          | A utility object to check if an interval of time has occured.          |
| [Log][13]                               | Provides a simple mechanism to log debug and info messages.            |
| [MergeSort][14]                         | Implements merge sort as extension methods to provide stable sorting.  |
| [Mesh][15]                              | Represents a triangle based mesh.                                      |
| [NineSlice][16]                         | A special stretchable rectangle of an image.                           |
| [PerlinNoise][17]                       | Implements methods for sampling 1D, 2D and 3D perlin noise.            |
| [Rasterizer][18]                        | Contains rasterization methods for iterating over pixel positions.     |
| [RectanglePacker\<T>][19]               | A utility object for packing rectangles into a larger container rec... |
| [Screen][20]                            | An abstract representation of the screen (ie, window, view, etc).      |
| [Search][21]                            | Contains search related features.                                      |
| [Shader][22]                            | Provides GLSL shader support for custom image effects and other vis... |
| [DistortionShader][23]                  | Distortion shader.                                                     |
| [GrayscaleShader][24]                   | Grayscale shader.                                                      |
| [InvertShader][25]                      | Invert shader.                                                         |
| [VectorBlurShader][26]                  | Vector blur shader.                                                    |
| [SimplexNoise][27]                      | Implements methods for sampling 2D and 3D simplex noise.               |
| [Sprite][28]                            | A representation of an animated sprite. May also contains per-frame... |
| [SpriteAnimation][29]                   | Represents an image based per frame animation.                         |
| [SpriteFrame][30]                       | Represents a single frame of a SpriteAnimation .                       |
| [SpritePlayer][31]                      | A utility class to help drive sprite based animation.                  |
| [StyledText][32]                        | Styled text compiled by a StyledTextParser .                           |
| [StyledTextParser][33]                  | Provides an ability to parse text with some sort of markup into Sty... |
| [StandardStyledTextParser][34]          | Provides implementation of a BBCode-esque text markup parser.          |
| [SurfaceEffect][35]                     | The abstract representation of a particular surface effect.            |
| [SurfacePool][36]                       | Provides a mechanism for requesting temporary surfaces and recyclin... |
| [TextLayout][37]                        | Utility to measure text and manually invoke the text layout functio... |
| [Time][38]                              | Provides utility functions for converting time between units of mea... |
| [UniformInfo][39]                       | Contains information of a uniform from a Shader .                      |
| [CharacterEvent][40]                    | Contains the data of an event when a character has been typed on so... |
| [Color][41]                             | Color encoded as 4 component floats.                                   |
| [ColorBytes][42]                        | Color encoded as 4 component bytes.                                    |
| [FontMetrics][43]                       | Contains information about a font (ie, the vertical metrics).          |
| [GlyphMetrics][44]                      | Contains information about a glyph (ie, the horizontal metrics).       |
| [IntRange][45]                          | Represents a range of integers from IntRange.Min to IntRange.Max .     |
| [IntRectangle][46]                      | Represents a rectangle defined with integer coordinates.               |
| [IntSize][47]                           | Represents two dimensional size by a measure in each axis.             |
| [IntVector][48]                         | Represents a vector with two integer values.                           |
| [KeyEvent][49]                          | Contains the data of an event when a key has been pressed or releas... |
| [Matrix][50]                            | A 2x3 transformation matrix.                                           |
| [MouseButtonEvent][51]                  | Contains the data of an event when a mouse button has been pressed ... |
| [MouseMoveEvent][52]                    | Contains the data of an event when the mouse has been moved on some... |
| [MouseScrollEvent][53]                  | Contains the data of an event when the mouse wheel has been scrolle... |
| [Range][54]                             | Represents a range of single-precision floating point numbers from ... |
| [Rectangle][55]                         | Represents a rectangle, defined by the top left corner position and... |
| [Size][56]                              | Represents two dimensional size by a measure in each axis.             |
| [Statistics][57]                        | Represents statistics of some data.                                    |
| [TextDrawState][58]                     | Represents information of any particular glyph when drawing text.      |
| [TextLayoutState][59]                   | Represents information of any particular glyph during text layout.     |
| [UnicodeCharacter][60]                  | Represents a single 32 bit Unicode character.                          |
| [Vector][61]                            | Represents a vector with two single-precision floating-point values.   |
| [Vertex][62]                            | Represents a vertex of Mesh .                                          |
| [IInputSource][63]                      | Represents the functionality of an input source.                       |
| [ILogHandler][64]                       | Represents the interface for handling log messages from Log .          |
| [INoise1D][65]                          | Provides an interface for sampling one-dimensional noise.              |
| [INoise2D][66]                          | Provides an interface for sampling two-dimensional noise.              |
| [INoise3D][67]                          | Provides an interface for sampling three-dimensional noise.            |
| [AnimationDirection][68]                | Represents animation direction options.                                |
| [Axis][69]                              | Represents an axis of the 2D plane.                                    |
| [Blending][70]                          | Controls how drawing operations are blended into existing pixels.      |
| [ButtonState][71]                       | Represents the state of a button.                                      |
| [GamepadAxis][72]                       | Represents the various axis on a standard game pad.                    |
| [GamepadButton][73]                     | Represents the buttons on a standard gamepad.                          |
| [InterpolationMode][74]                 | Represents the behaviour when sampling an image on a non-integer co... |
| [Key][75]                               | Standardized virtual key mapping from GLFW.                            |
| [KeyModifiers][76]                      | Flags that represent the modifier keys pressed or toggled when an a... |
| [LogVerbosity][77]                      | Controls the verbosity of Log .                                        |
| [MouseButton][78]                       | Represents the buttons on a mouse.                                     |
| [MultisampleQuality][79]                | Multisampling levels                                                   |
| [PackingAlgorithm][80]                  | An enumeration of rectangle packing algorithms.                        |
| [PerformanceOverlayMode][81]            | Controls showing the performance overlay on a GraphicsContext object.  |
| [RepeatMode][82]                        | Represents the behaviour when sampling an image outside its natural... |
| [SurfaceType][83]                       | Represents the surface type.                                           |
| [TextAlign][84]                         | Controls how text is aligned to the layout rectangle.                  |
| [TimeUnit][85]                          | Represents units of time, such as a millisecond.                       |
| [UniformType][86]                       | Represents the type of a uniform in a Shader .                         |
| [ActualCost\<T>][87]                    | Gets the known cost between two values.                                |
| [DrawTextCallback][88]                  | Delegate type for the callback when drawing text.                      |
| [GameLoop.UpdateFunction][89]           | Update function called every iteration of the game loop.               |
| [HeuristicCost\<T>][90]                 | Gets the estimated cost of the some value.                             |
| [TextLayoutCallback][91]                | Delegate type for the callback when performing text layout.            |

## Heirloom.Collections

#### Graph

| Name                     | Summary                                                |
|--------------------------|--------------------------------------------------------|
| [DirectedGraph\<T>][92]  | A directed graph implemented using adjacency lists.    |
| [Graph\<T>][93]          | An undirected graph implemented using adjacency lists. |
| [IDirectedGraph\<T>][94] | An interface that represents a graph.                  |
| [IGraph\<T>][95]         | An interface that represents a graph.                  |
| [TraversalMethod][96]    | Represents a choice of traversing a graph.             |

#### Grids

| Name                           | Summary                                                                |
|--------------------------------|------------------------------------------------------------------------|
| [Grid\<T>][97]                 | A finite grid (bounded by size) of values.                             |
| [GridUtilities][98]            | Provides extra utilities for interacting with a grid.                  |
| [SparseGrid\<T>][99]           | An infinite, sparse grid of values.                                    |
| [IFiniteGrid\<T>][100]         | A finite grid (bounded by IFiniteGrid<T>.Width and IFiniteGrid<T>.H... |
| [IGrid\<T>][101]               | A 2D grid of values.                                                   |
| [IReadOnlyGrid\<T>][102]       | A read-only view of a 2D grid of values.                               |
| [IReadOnlySparseGrid\<T>][103] | A sparse 2D grid of values.                                            |
| [ISparseGrid\<T>][104]         | A sparse 2D grid of values.                                            |
| [GridNeighborType][105]        | Describes the choice of neighbors in a grid.                           |

#### Heap

| Name                     | Summary                                                                |
|--------------------------|------------------------------------------------------------------------|
| [Heap\<T>][106]          | Represents a heap data structure. Allows the insertion and removal ... |
| [IHeap\<T>][107]         | Represents a heap data structure. Allowing the access and removal o... |
| [IReadOnlyHeap\<T>][108] | Represents a read-only view of a Heap<T> .                             |
| [HeapType][109]          | Describes the behaviour of a Heap<T> .                                 |

#### Spatial Collections

| Name                                  | Summary                                                                |
|---------------------------------------|------------------------------------------------------------------------|
| [BvhSpatialCollection\<T>][110]       | A spatial collection to store and query elements in 2D space, imple... |
| [IReadOnlySpatialCollection\<T>][111] | A read-only view of a spatial collection to query elements in 2D sp... |
| [ISpatialCollection\<T>][112]         | A spatial collection to store and query elements in 2D space.          |
| [ISpatialQuery\<T>][113]              | Provides methods for querying elements in 2D space.                    |

#### Uncategorized

| Name                                   | Summary                                                                |
|----------------------------------------|------------------------------------------------------------------------|
| [FreeList\<T>][114]                    | A free list an allocation-centric data structure that allows insert... |
| [ObjectPool\<T>][115]                  | Implements an object pool to recycle objects and reduce allocatio s... |
| [SparseGridSpatialCollection\<T>][116] | Implements ISpatialCollection<T> using a SparseGrid<T> .               |
| [TypeDictionary\<T>][117]              | Manages objects by their type hierarchy up to the base type, allowi... |
| [IReadOnlyTypeDictionary\<T>][118]     | A read-only view of ITypeDictionary<T> .                               |
| [ITypeDictionary\<T>][119]             | Manages objects by their type hierarchy up to the base type, allowi... |

## Heirloom.Geometry

| Name                  | Summary                                                                |
|-----------------------|------------------------------------------------------------------------|
| [Collision][120]      | Collision detection routines.                                          |
| [Curve][121]          | An implementation of a multi-point bezier curve using multiple 'seg... |
| [CurveTools][122]     | Utility function for computation with quadratic and cubic curves.      |
| [GeometryTools][123]  | Provides utilities for generating and manipulating shapes.             |
| [Polygon][124]        | Represents a simple polygon.                                           |
| [PolygonTools][125]   | Provides several operations for polygons represented as a read-only... |
| [SeparatingAxis][126] | Implementation of 2D collisions/overlap using separating axis theorem. |
| [Circle][127]         | Represents a circle via center position and radius.                    |
| [CollisionData][128]  | Contains the results of a collision function in Collision .            |
| [LineSegment][129]    | Represents a line segment defined by two end points.                   |
| [Ray][130]            | Represents a ray by orgin point and directional vector.                |
| [RayContact][131]     | Represents the result of a ray to shape intersection.                  |
| [Triangle][132]       | Represents a triangle shape defined by three points.                   |
| [IShape][133]         | Represents the general interface of a shape and common operators ea... |
| [CurveType][134]      | Represents the type of curve.                                          |

## Heirloom.IO

| Name                | Summary                                                        |
|---------------------|----------------------------------------------------------------|
| [EmbeddedFile][135] | Represents an embedded file.                                   |
| [Files][136]        | A utility to unify access of embedded files and files on disk. |

## Heirloom.Sound

| Name                        | Summary                                                                |
|-----------------------------|------------------------------------------------------------------------|
| [AudioAdapter][137]         | The abstraction of a low level audio system.                           |
| [AudioClip][138]            | An object to contain (and decode) audio data into raw samples.         |
| [AudioEffect][139]          | An abstarct representation of an audio effect. Implementations of t... |
| [BandPassFilter][140]       | An audio effect that implements a band pass filter.                    |
| [HighPassFilter][141]       | An audio effect that implements a high pass filter.                    |
| [LowPassFilter][142]        | An audio effect that implements a low pass filter.                     |
| [ReverbEffect][143]         | An audio effect that implements a Schroeder reverb.                    |
| [AudioNode][144]            | Represents a node in the audio mixing tree.                            |
| [AudioGroup][145]           | An AudioNode to mix and apply effects to a group of other nodes.       |
| [AudioSource][146]          | An instance of playable audio.                                         |
| [IAudioDecoder][147]        | An interface representing an audio decoder.                            |
| [AudioCaptureCallback][148] | A delegate for a callback when audio samples are captured by a inpu... |

[0]: Heirloom.Core.md
[1]: Heirloom.Core/Heirloom/Calc.md
[2]: Heirloom.Core/Heirloom/Extensions.md
[3]: Heirloom.Core/Heirloom/Font.md
[4]: Heirloom.Core/Heirloom/GameLoop.md
[5]: Heirloom.Core/Heirloom/Glyph.md
[6]: Heirloom.Core/Heirloom/GraphicsContext.md
[7]: Heirloom.Core/Heirloom/GraphicsContext.PerformanceMetrics.md
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
[29]: Heirloom.Core/Heirloom/SpriteAnimation.md
[30]: Heirloom.Core/Heirloom/SpriteFrame.md
[31]: Heirloom.Core/Heirloom/SpritePlayer.md
[32]: Heirloom.Core/Heirloom/StyledText.md
[33]: Heirloom.Core/Heirloom/StyledTextParser.md
[34]: Heirloom.Core/Heirloom/StandardStyledTextParser.md
[35]: Heirloom.Core/Heirloom/SurfaceEffect.md
[36]: Heirloom.Core/Heirloom/SurfacePool.md
[37]: Heirloom.Core/Heirloom/TextLayout.md
[38]: Heirloom.Core/Heirloom/Time.md
[39]: Heirloom.Core/Heirloom/UniformInfo.md
[40]: Heirloom.Core/Heirloom/CharacterEvent.md
[41]: Heirloom.Core/Heirloom/Color.md
[42]: Heirloom.Core/Heirloom/ColorBytes.md
[43]: Heirloom.Core/Heirloom/FontMetrics.md
[44]: Heirloom.Core/Heirloom/GlyphMetrics.md
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
[60]: Heirloom.Core/Heirloom/UnicodeCharacter.md
[61]: Heirloom.Core/Heirloom/Vector.md
[62]: Heirloom.Core/Heirloom/Vertex.md
[63]: Heirloom.Core/Heirloom/IInputSource.md
[64]: Heirloom.Core/Heirloom/ILogHandler.md
[65]: Heirloom.Core/Heirloom/INoise1D.md
[66]: Heirloom.Core/Heirloom/INoise2D.md
[67]: Heirloom.Core/Heirloom/INoise3D.md
[68]: Heirloom.Core/Heirloom/AnimationDirection.md
[69]: Heirloom.Core/Heirloom/Axis.md
[70]: Heirloom.Core/Heirloom/Blending.md
[71]: Heirloom.Core/Heirloom/ButtonState.md
[72]: Heirloom.Core/Heirloom/GamepadAxis.md
[73]: Heirloom.Core/Heirloom/GamepadButton.md
[74]: Heirloom.Core/Heirloom/InterpolationMode.md
[75]: Heirloom.Core/Heirloom/Key.md
[76]: Heirloom.Core/Heirloom/KeyModifiers.md
[77]: Heirloom.Core/Heirloom/LogVerbosity.md
[78]: Heirloom.Core/Heirloom/MouseButton.md
[79]: Heirloom.Core/Heirloom/MultisampleQuality.md
[80]: Heirloom.Core/Heirloom/PackingAlgorithm.md
[81]: Heirloom.Core/Heirloom/PerformanceOverlayMode.md
[82]: Heirloom.Core/Heirloom/RepeatMode.md
[83]: Heirloom.Core/Heirloom/SurfaceType.md
[84]: Heirloom.Core/Heirloom/TextAlign.md
[85]: Heirloom.Core/Heirloom/TimeUnit.md
[86]: Heirloom.Core/Heirloom/UniformType.md
[87]: Heirloom.Core/Heirloom/ActualCost[T].md
[88]: Heirloom.Core/Heirloom/DrawTextCallback.md
[89]: Heirloom.Core/Heirloom/GameLoop.UpdateFunction.md
[90]: Heirloom.Core/Heirloom/HeuristicCost[T].md
[91]: Heirloom.Core/Heirloom/TextLayoutCallback.md
[92]: Heirloom.Core/Heirloom.Collections/DirectedGraph[T].md
[93]: Heirloom.Core/Heirloom.Collections/Graph[T].md
[94]: Heirloom.Core/Heirloom.Collections/IDirectedGraph[T].md
[95]: Heirloom.Core/Heirloom.Collections/IGraph[T].md
[96]: Heirloom.Core/Heirloom.Collections/TraversalMethod.md
[97]: Heirloom.Core/Heirloom.Collections/Grid[T].md
[98]: Heirloom.Core/Heirloom.Collections/GridUtilities.md
[99]: Heirloom.Core/Heirloom.Collections/SparseGrid[T].md
[100]: Heirloom.Core/Heirloom.Collections/IFiniteGrid[T].md
[101]: Heirloom.Core/Heirloom.Collections/IGrid[T].md
[102]: Heirloom.Core/Heirloom.Collections/IReadOnlyGrid[T].md
[103]: Heirloom.Core/Heirloom.Collections/IReadOnlySparseGrid[T].md
[104]: Heirloom.Core/Heirloom.Collections/ISparseGrid[T].md
[105]: Heirloom.Core/Heirloom.Collections/GridNeighborType.md
[106]: Heirloom.Core/Heirloom.Collections/Heap[T].md
[107]: Heirloom.Core/Heirloom.Collections/IHeap[T].md
[108]: Heirloom.Core/Heirloom.Collections/IReadOnlyHeap[T].md
[109]: Heirloom.Core/Heirloom.Collections/HeapType.md
[110]: Heirloom.Core/Heirloom.Collections/BvhSpatialCollection[T].md
[111]: Heirloom.Core/Heirloom.Collections/IReadOnlySpatialCollection[T].md
[112]: Heirloom.Core/Heirloom.Collections/ISpatialCollection[T].md
[113]: Heirloom.Core/Heirloom.Collections/ISpatialQuery[T].md
[114]: Heirloom.Core/Heirloom.Collections/FreeList[T].md
[115]: Heirloom.Core/Heirloom.Collections/ObjectPool[T].md
[116]: Heirloom.Core/Heirloom.Collections/SparseGridSpatialCollection[T].md
[117]: Heirloom.Core/Heirloom.Collections/TypeDictionary[T].md
[118]: Heirloom.Core/Heirloom.Collections/IReadOnlyTypeDictionary[T].md
[119]: Heirloom.Core/Heirloom.Collections/ITypeDictionary[T].md
[120]: Heirloom.Core/Heirloom.Geometry/Collision.md
[121]: Heirloom.Core/Heirloom.Geometry/Curve.md
[122]: Heirloom.Core/Heirloom.Geometry/CurveTools.md
[123]: Heirloom.Core/Heirloom.Geometry/GeometryTools.md
[124]: Heirloom.Core/Heirloom.Geometry/Polygon.md
[125]: Heirloom.Core/Heirloom.Geometry/PolygonTools.md
[126]: Heirloom.Core/Heirloom.Geometry/SeparatingAxis.md
[127]: Heirloom.Core/Heirloom.Geometry/Circle.md
[128]: Heirloom.Core/Heirloom.Geometry/CollisionData.md
[129]: Heirloom.Core/Heirloom.Geometry/LineSegment.md
[130]: Heirloom.Core/Heirloom.Geometry/Ray.md
[131]: Heirloom.Core/Heirloom.Geometry/RayContact.md
[132]: Heirloom.Core/Heirloom.Geometry/Triangle.md
[133]: Heirloom.Core/Heirloom.Geometry/IShape.md
[134]: Heirloom.Core/Heirloom.Geometry/CurveType.md
[135]: Heirloom.Core/Heirloom.IO/EmbeddedFile.md
[136]: Heirloom.Core/Heirloom.IO/Files.md
[137]: Heirloom.Core/Heirloom.Sound/AudioAdapter.md
[138]: Heirloom.Core/Heirloom.Sound/AudioClip.md
[139]: Heirloom.Core/Heirloom.Sound/AudioEffect.md
[140]: Heirloom.Core/Heirloom.Sound/BandPassFilter.md
[141]: Heirloom.Core/Heirloom.Sound/HighPassFilter.md
[142]: Heirloom.Core/Heirloom.Sound/LowPassFilter.md
[143]: Heirloom.Core/Heirloom.Sound/ReverbEffect.md
[144]: Heirloom.Core/Heirloom.Sound/AudioNode.md
[145]: Heirloom.Core/Heirloom.Sound/AudioGroup.md
[146]: Heirloom.Core/Heirloom.Sound/AudioSource.md
[147]: Heirloom.Core/Heirloom.Sound/IAudioDecoder.md
[148]: Heirloom.Core/Heirloom.Sound/AudioCaptureCallback.md
