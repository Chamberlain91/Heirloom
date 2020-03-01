# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../Heirloom.Drawing/Heirloom.Drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## Graphics (Abstract Class)
<small>**Namespace**: Heirloom.Drawing</sub></small>  

| Properties                          | Summary                                                                                 |
|-------------------------------------|-----------------------------------------------------------------------------------------|
| [Capabilities](#CAPAE766)           | Gets the queried capabilities (ie, limits) for the current device.                      |
| [CurrentFPS](#CURR70FD)             | Gets how often the default surface is presented to the screen per second.               |
| [Performance](#PERFD985)            | Gets drawing performance information.                                                   |
| [IsDisposed](#ISDI6187)             | Gets a value determining if this [Graphics](Heirloom.Drawing.Graphics.md) was disposed. |
| [DefaultSurface](#DEFA465E)         | Gets the default surface (ie, window) of this render context.                           |
| [Surface](#SURF4078)                | Gets or sets the current surface.                                                       |
| [Shader](#SHAD5D12)                 | Gets or sets the active shader.                                                         |
| [Viewport](#VIEW365B)               | Gets or sets the viewport in normalized coordinates.                                    |
| [ViewportScreen](#VIEW9EEF)         | Gets the size of viewport in pixel coordinates.                                         |
| [GlobalTransform](#GLOB9D3F)        | Get or sets the global transform.                                                       |
| [InverseGlobalTransform](#INVE9F06) | Gets the inverse of the current global transform.                                       |
| [Blending](#BLENF02A)               | Gets or sets the current blending mode.                                                 |
| [Color](#COLOD122)                  | Gets or sets the current blending color.                                                |

| Methods                          | Summary                                                                                                        |
|----------------------------------|----------------------------------------------------------------------------------------------------------------|
| [ResetState](#RESEE0A0)          | Reset current context state to defaults (default surface, full viewport, no transform, alpha and white).       |
| [PushState](#PUSH86E2)           | Save the context state (push it on the state stack).                                                           |
| [PopState](#POPSC33B)            | Restore the context state (pop from the state stack).                                                          |
| [Clear](#CLEA3BB2)               | Clears the current surface with the specified color.                                                           |
| [DrawMesh](#DRAWDBE2)            | Draws a mesh with the given image to the current surface.                                                      |
| [GrabPixels](#GRAB1D23)          | Grab the pixels from a subregion of the current surface and return that image. (ie, a screenshot)              |
| [GrabPixels](#GRAB1D23)          | Grab the pixels from the current surface and return that image. (ie, a screenshot)                             |
| [GetDrawCounts](#GETDC2CC)       |                                                                                                                |
| [RefreshScreen](#REFRBE57)       | Present the drawing operations to the screen.                                                                  |
| [SwapBuffers](#SWAPEFBA)         |                                                                                                                |
| [EndFrame](#ENDFD6AD)            |                                                                                                                |
| [Flush](#FLUSCBEB)               | Force any pending drawing operations to complete.                                                              |
| [Dispose](#DISP8A0D)             |                                                                                                                |
| [Dispose](#DISP8A0D)             | Dispose this graphics context, freeing any resources occupied by it.                                           |
| [SetCameraTransform](#SETCB80D)  | Sets `Heirloom.Drawing.Graphics.GlobalTransform` to mimic a 2D camera.                                         |
| [DrawImage](#DRAW9CF5)           | Draws an image to the current surface.                                                                         |
| [DrawImage](#DRAW9CF5)           | Draws an image to the current surface.                                                                         |
| [DrawImage](#DRAW9CF5)           | Draws an image to the current surface.                                                                         |
| [DrawImage](#DRAW9CF5)           | Draws an image stretched to fill a rectangular region to the current surface ignoring the image origin offset. |
| [DrawImage](#DRAW9CF5)           | Draws an image to the current surface.                                                                         |
| [DrawSprite](#DRAW8041)          | Draw a sprite to the current surface.                                                                          |
| [DrawLine](#DRAW2AD0)            | Draws a line segment between two points to the current surface.                                                |
| [DrawCurve](#DRAW6814)           | Draws a quadratic curve using three control points to the current surface.                                     |
| [DrawCurve](#DRAW6814)           | Draws a cubic curve using four control points to the current surface.                                          |
| [DrawRect](#DRAW5145)            | Draws a rectangle to the current surface.                                                                      |
| [DrawRectOutline](#DRAW5314)     | Draws the outline of a rectangel to the current surface.                                                       |
| [DrawCross](#DRAW560C)           | Draws a simple axis aligned 'cross' or 'plus' shape, useful for debugging positions.                           |
| [DrawCircle](#DRAW4586)          | Draws a circle to the current surface.                                                                         |
| [DrawCircle](#DRAW4586)          | Draws a circle to the current surface.                                                                         |
| [DrawCircleOutline](#DRAWFE41)   | Draws the outline of a circle to the current surface.                                                          |
| [DrawCircleOutline](#DRAWFE41)   | Draws the outline of a circle to the current surface.                                                          |
| [DrawTriangle](#DRAW749E)        | Draw a triangle to the current surface.                                                                        |
| [DrawTriangle](#DRAW749E)        | Draw a triangle outline to the current surface.                                                                |
| [DrawTriangleOutline](#DRAW7869) | Draw a triangle outline to the current surface.                                                                |
| [DrawTriangleOutline](#DRAW7869) | Draw a triangle outline to the current surface.                                                                |
| [DrawPolygon](#DRAWB32A)         | Draws a regular polygon to the current surface.                                                                |
| [DrawPolygonOutline](#DRAW4EDC)  | Draws the outline of a regular polygon to the current surface.                                                 |
| [DrawPolygon](#DRAWB32A)         | Draws a simple polygon to the current surface.                                                                 |
| [DrawPolygon](#DRAWB32A)         | Draws a simple polygon to the current surface.                                                                 |
| [DrawPolygon](#DRAWB32A)         | Draws a simple polygon to the current surface.                                                                 |
| [DrawPolygon](#DRAWB32A)         | Draws a simple polygon to the current surface.                                                                 |
| [DrawPolygonOutline](#DRAW4EDC)  | Draws the outline of a simple polygon to the current surface.                                                  |
| [DrawPolygonOutline](#DRAW4EDC)  | Draws the outline of a simple polygon to the current surface.                                                  |
| [DrawPolygonOutline](#DRAW4EDC)  | Draws the outline of a simple polygon to the current surface.                                                  |
| [DrawPolygonOutline](#DRAW4EDC)  | Draws the outline of a simple polygon to the current surface.                                                  |
| [DrawText](#DRAW5145)            | Draws rich text to the current surface.                                                                        |
| [DrawText](#DRAW5145)            | Draws rich text to the current surface.                                                                        |
| [DrawText](#DRAW5145)            | Draws text to the current surface.                                                                             |
| [DrawText](#DRAW5145)            | Draws text to the current surface.                                                                             |
| [DrawText](#DRAW5145)            | Draws text to the current surface.                                                                             |
| [DrawText](#DRAW5145)            | Draws text to the current surface.                                                                             |

### Constructors

#### Graphics([MultisampleQuality](Heirloom.Drawing.MultisampleQuality.md) multisample)

### Properties

#### <a name="CAPAE766"></a> Capabilities : [GraphicsCapabilities](Heirloom.Drawing.GraphicsCapabilities.md)

<small>`Read Only`</small>

Gets the queried capabilities (ie, limits) for the current device.

#### <a name="CURR70FD"></a> CurrentFPS : float

<small>`Read Only`</small>

Gets how often the default surface is presented to the screen per second.

#### <a name="PERFD985"></a> Performance : [DrawingPerformance](Heirloom.Drawing.DrawingPerformance.md)

<small>`Read Only`</small>

Gets drawing performance information.

#### <a name="ISDI6187"></a> IsDisposed : bool

<small>`Read Only`</small>

Gets a value determining if this [Graphics](Heirloom.Drawing.Graphics.md) was disposed.

#### <a name="DEFA465E"></a> DefaultSurface : [Surface](Heirloom.Drawing.Surface.md)

<small>`Read Only`</small>

Gets the default surface (ie, window) of this render context.

#### <a name="SURF4078"></a> Surface : [Surface](Heirloom.Drawing.Surface.md)


Gets or sets the current surface.

#### <a name="SHAD5D12"></a> Shader : [Shader](Heirloom.Drawing.Shader.md)


Gets or sets the active shader.

#### <a name="VIEW365B"></a> Viewport : [Rectangle](../Heirloom.Math/Heirloom.Math.Rectangle.md)


Gets or sets the viewport in normalized coordinates.

#### <a name="VIEW9EEF"></a> ViewportScreen : [IntRectangle](../Heirloom.Math/Heirloom.Math.IntRectangle.md)


Gets the size of viewport in pixel coordinates.

#### <a name="GLOB9D3F"></a> GlobalTransform : [Matrix](../Heirloom.Math/Heirloom.Math.Matrix.md)


Get or sets the global transform.

#### <a name="INVE9F06"></a> InverseGlobalTransform : [Matrix](../Heirloom.Math/Heirloom.Math.Matrix.md)

<small>`Read Only`</small>

Gets the inverse of the current global transform.

#### <a name="BLENF02A"></a> Blending : [Blending](Heirloom.Drawing.Blending.md)


Gets or sets the current blending mode.

#### <a name="COLOD122"></a> Color : [Color](Heirloom.Drawing.Color.md)


Gets or sets the current blending color.

### Methods

#### <a name="RESEA704"></a> ResetState() : void

Reset current context state to defaults (default surface, full viewport, no transform, alpha and white).

#### <a name="PUSHDD33"></a> PushState() : void

Save the context state (push it on the state stack).

#### <a name="POPS630D"></a> PopState() : void

Restore the context state (pop from the state stack).

#### <a name="CLEA5D92"></a> Clear([Color](Heirloom.Drawing.Color.md) color) : void
<small>`Abstract`</small>

Clears the current surface with the specified color.


#### <a name="DRAW95B1"></a> DrawMesh([ImageSource](Heirloom.Drawing.ImageSource.md) image, [Mesh](Heirloom.Drawing.Mesh.md) mesh, in [Matrix](../Heirloom.Math/Heirloom.Math.Matrix.md) transform) : void
<small>`Abstract`</small>

Draws a mesh with the given image to the current surface.

<small>**image**: <param name="image">Some image.</param></small>  
<small>**mesh**: <param name="mesh">Some mesh.</param></small>  
<small>**transform**: <param name="transform">Some transform.</param></small>  

#### <a name="GRABEDDD"></a> GrabPixels([IntRectangle](../Heirloom.Math/Heirloom.Math.IntRectangle.md) region) : [Image](Heirloom.Drawing.Image.md)
<small>`Abstract`</small>

Grab the pixels from a subregion of the current surface and return that image. (ie, a screenshot)

<small>**region**: <param name="region">A region within the currently set surface.</param></small>  

#### <a name="GRABF748"></a> GrabPixels() : [Image](Heirloom.Drawing.Image.md)

Grab the pixels from the current surface and return that image. (ie, a screenshot)

#### <a name="GETD41AD"></a> GetDrawCounts() : [Graphics.DrawCounts](Heirloom.Drawing.Graphics.DrawCounts.md)
<small>`Abstract`, `Protected`</small>

#### <a name="REFRB28D"></a> RefreshScreen() : void

Present the drawing operations to the screen.

#### <a name="SWAPBF25"></a> SwapBuffers() : void
<small>`Abstract`, `Protected`</small>

#### <a name="ENDFE202"></a> EndFrame() : void
<small>`Abstract`, `Protected`</small>

#### <a name="FLUS2F0E"></a> Flush() : void
<small>`Abstract`</small>

Force any pending drawing operations to complete.

#### <a name="DISPD833"></a> Dispose(bool disposing) : void
<small>`Virtual`, `Protected`</small>


#### <a name="DISP4E62"></a> Dispose() : void

Dispose this graphics context, freeing any resources occupied by it.

#### <a name="SETC55A7"></a> SetCameraTransform([Vector](../Heirloom.Math/Heirloom.Math.Vector.md) center, float scale = 1) : void

Sets `Heirloom.Drawing.Graphics.GlobalTransform` to mimic a 2D camera.


#### <a name="DRAW5AEC"></a> DrawImage([ImageSource](Heirloom.Drawing.ImageSource.md) image, in [Vector](../Heirloom.Math/Heirloom.Math.Vector.md) position) : void

Draws an image to the current surface.

<small>**image**: <param name="image">Some image.</param></small>  
<small>**position**: <param name="position">The position of the image.</param></small>  

#### <a name="DRAWCC3C"></a> DrawImage([ImageSource](Heirloom.Drawing.ImageSource.md) image, in [Vector](../Heirloom.Math/Heirloom.Math.Vector.md) position, float rotation) : void

Draws an image to the current surface.

<small>**image**: <param name="image">Some image.</param></small>  
<small>**position**: <param name="position">The position of the image.</param></small>  
<small>**rotation**: <param name="rotation">The rotation applied to the image.</param></small>  

#### <a name="DRAWAA1F"></a> DrawImage([ImageSource](Heirloom.Drawing.ImageSource.md) image, in [Vector](../Heirloom.Math/Heirloom.Math.Vector.md) position, float rotation, in [Vector](../Heirloom.Math/Heirloom.Math.Vector.md) scale) : void

Draws an image to the current surface.

<small>**image**: <param name="image">Some image.</param></small>  
<small>**position**: <param name="position">The position of the image.</param></small>  
<small>**rotation**: <param name="rotation">The rotation applied to the image.</param></small>  
<small>**scale**: <param name="scale">The scale applied to the image.</param></small>  

#### <a name="DRAW2104"></a> DrawImage([ImageSource](Heirloom.Drawing.ImageSource.md) image, in [Rectangle](../Heirloom.Math/Heirloom.Math.Rectangle.md) rectangle) : void

Draws an image stretched to fill a rectangular region to the current surface ignoring the image origin offset.

<small>**image**: <param name="image">Some image.</param></small>  
<small>**rectangle**: <param name="rectangle">The bounds of the drawn image.</param></small>  

#### <a name="DRAWCC91"></a> DrawImage([ImageSource](Heirloom.Drawing.ImageSource.md) image, in [Matrix](../Heirloom.Math/Heirloom.Math.Matrix.md) transform) : void

Draws an image to the current surface.

<small>**image**: <param name="image">Some image.</param></small>  
<small>**transform**: <param name="transform">Some transform.</param></small>  

#### <a name="DRAW3D4F"></a> DrawSprite([Sprite](Heirloom.Drawing.Sprite.md) sprite, int index, in [Matrix](../Heirloom.Math/Heirloom.Math.Matrix.md) transform) : void

Draw a sprite to the current surface.

<small>**sprite**: <param name="sprite">Some sprite.</param></small>  
<small>**index**: <param name="index">Some valid frame number within the sprite.</param></small>  
<small>**transform**: <param name="transform">Some transform to draw the sprite.</param></small>  

#### <a name="DRAW5FD3"></a> DrawLine(in [Vector](../Heirloom.Math/Heirloom.Math.Vector.md) p0, in [Vector](../Heirloom.Math/Heirloom.Math.Vector.md) p1, float width = 1) : void

Draws a line segment between two points to the current surface.

<small>**p0**: <param name="p0">The start point.</param></small>  
<small>**p1**: <param name="p1">The end point.</param></small>  
<small>**width**: <param name="width">The thickness of the line in pixels.</param></small>  

#### <a name="DRAW1479"></a> DrawCurve(in [Vector](../Heirloom.Math/Heirloom.Math.Vector.md) p0, in [Vector](../Heirloom.Math/Heirloom.Math.Vector.md) p1, in [Vector](../Heirloom.Math/Heirloom.Math.Vector.md) p2, float width = 1) : void

Draws a quadratic curve using three control points to the current surface.

<small>**p0**: <param name="p0">The first control point.</param></small>  
<small>**p1**: <param name="p1">The second control point.</param></small>  
<small>**p2**: <param name="p2">The third control point.</param></small>  
<small>**width**: <param name="width">The thickness of the line in pixels.</param></small>  

#### <a name="DRAW6002"></a> DrawCurve(in [Vector](../Heirloom.Math/Heirloom.Math.Vector.md) p0, in [Vector](../Heirloom.Math/Heirloom.Math.Vector.md) p1, in [Vector](../Heirloom.Math/Heirloom.Math.Vector.md) p2, in [Vector](../Heirloom.Math/Heirloom.Math.Vector.md) p3, float width = 1) : void

Draws a cubic curve using four control points to the current surface.

<small>**p0**: <param name="p0">The first control point.</param></small>  
<small>**p1**: <param name="p1">The second control point.</param></small>  
<small>**p2**: <param name="p2">The third control point.</param></small>  
<small>**p3**: <param name="p3">The fourth control point.</param></small>  
<small>**width**: <param name="width">The thickness of the line in pixels.</param></small>  

#### <a name="DRAWA953"></a> DrawRect(in [Rectangle](../Heirloom.Math/Heirloom.Math.Rectangle.md) rectangle) : void

Draws a rectangle to the current surface.

<small>**rectangle**: <param name="rectangle">The rectangular region of the rectangle.</param></small>  

#### <a name="DRAWFE42"></a> DrawRectOutline(in [Rectangle](../Heirloom.Math/Heirloom.Math.Rectangle.md) rectangle, float width = 1) : void

Draws the outline of a rectangel to the current surface.

<small>**rectangle**: <param name="rectangle">The rectangular region of the rectangle.</param></small>  
<small>**width**: <param name="width">Width of the outline in pixels.</param></small>  

#### <a name="DRAWF5B1"></a> DrawCross(in [Vector](../Heirloom.Math/Heirloom.Math.Vector.md) center, float size = 2, float width = 1) : void

Draws a simple axis aligned 'cross' or 'plus' shape, useful for debugging positions.

<small>**center**: <param name="center">The position of the cross.</param></small>  
<small>**size**: <param name="size">Size in screen pixels (not world space).</param></small>  
<small>**width**: <param name="width">Width of the lines screen pixels (not world space).</param></small>  

#### <a name="DRAW5A37"></a> DrawCircle(in [Circle](../Heirloom.Math/Heirloom.Math.Circle.md) circle) : void

Draws a circle to the current surface.

<small>**circle**: <param name="circle">The circle to draw.</param></small>  

#### <a name="DRAW1B07"></a> DrawCircle(in [Vector](../Heirloom.Math/Heirloom.Math.Vector.md) position, float radius) : void

Draws a circle to the current surface.

<small>**position**: <param name="position">The center of the circle.</param></small>  
<small>**radius**: <param name="radius">The radius of the circle.</param></small>  

#### <a name="DRAWBDDF"></a> DrawCircleOutline(in [Circle](../Heirloom.Math/Heirloom.Math.Circle.md) circle, float width = 1) : void

Draws the outline of a circle to the current surface.

<small>**circle**: <param name="circle">The circle to draw.</param></small>  
<small>**width**: <param name="width">Width of the outline in pixels.</param></small>  

#### <a name="DRAWF367"></a> DrawCircleOutline(in [Vector](../Heirloom.Math/Heirloom.Math.Vector.md) position, float radius, float width = 1) : void

Draws the outline of a circle to the current surface.

<small>**position**: <param name="position">The centr of the circle.</param></small>  
<small>**radius**: <param name="radius">The radius of the circle.</param></small>  
<small>**width**: <param name="width">Width of the outline in pixels.</param></small>  

#### <a name="DRAW5123"></a> DrawTriangle(in [Triangle](../Heirloom.Math/Heirloom.Math.Triangle.md) triangle) : void

Draw a triangle to the current surface.

<small>**triangle**: <param name="triangle">The triangle to draw.</param></small>  

#### <a name="DRAWF3C0"></a> DrawTriangle(in [Vector](../Heirloom.Math/Heirloom.Math.Vector.md) a, in [Vector](../Heirloom.Math/Heirloom.Math.Vector.md) b, in [Vector](../Heirloom.Math/Heirloom.Math.Vector.md) c) : void

Draw a triangle outline to the current surface.

<small>**a**: <param name="a">The first point.</param></small>  
<small>**b**: <param name="b">The second point.</param></small>  
<small>**c**: <param name="c">The third point.</param></small>  

#### <a name="DRAW2E39"></a> DrawTriangleOutline(in [Triangle](../Heirloom.Math/Heirloom.Math.Triangle.md) triangle, float width = 1) : void

Draw a triangle outline to the current surface.

<small>**triangle**: <param name="triangle">The triangle to draw.</param></small>  
<small>**width**: <param name="width">The thickness of the line in pixels.</param></small>  

#### <a name="DRAW6968"></a> DrawTriangleOutline(in [Vector](../Heirloom.Math/Heirloom.Math.Vector.md) a, in [Vector](../Heirloom.Math/Heirloom.Math.Vector.md) b, in [Vector](../Heirloom.Math/Heirloom.Math.Vector.md) c, float width = 1) : void

Draw a triangle outline to the current surface.

<small>**a**: <param name="a">The first point.</param></small>  
<small>**b**: <param name="b">The second point.</param></small>  
<small>**c**: <param name="c">The third point.</param></small>  
<small>**width**: <param name="width">The thickness of the line in pixels.</param></small>  

#### <a name="DRAWFC9E"></a> DrawPolygon(in [Vector](../Heirloom.Math/Heirloom.Math.Vector.md) position, int sides, float radius) : void

Draws a regular polygon to the current surface.

<small>**sides**: <param name="sides">The number of sides in the regular polygon.</param></small>  
<small>**radius**: <param name="radius">The radius of the regular polygon.</param></small>  

#### <a name="DRAW1105"></a> DrawPolygonOutline(in [Vector](../Heirloom.Math/Heirloom.Math.Vector.md) position, int sides, float radius, float width = 1) : void

Draws the outline of a regular polygon to the current surface.

<small>**sides**: <param name="sides">The number of sides in the regular polygon.</param></small>  
<small>**radius**: <param name="radius">The radius of the regular polygon.</param></small>  
<small>**width**: <param name="width">Width of the outline in pixels.</param></small>  

#### <a name="DRAWF6BA"></a> DrawPolygon([Polygon](../Heirloom.Math/Heirloom.Math.Polygon.md) polygon) : void

Draws a simple polygon to the current surface.

<small>**polygon**: <param name="polygon">Some polygon.</param></small>  

#### <a name="DRAWFD16"></a> DrawPolygon([Polygon](../Heirloom.Math/Heirloom.Math.Polygon.md) polygon, in [Matrix](../Heirloom.Math/Heirloom.Math.Matrix.md) transform) : void

Draws a simple polygon to the current surface.

<small>**polygon**: <param name="polygon">Some polygon.</param></small>  
<small>**transform**: <param name="transform">Some transform.</param></small>  

#### <a name="DRAW2980"></a> DrawPolygon(IEnumerable\<Vector> polygon) : void

Draws a simple polygon to the current surface.

<small>**polygon**: <param name="polygon">Some polygon.</param></small>  

#### <a name="DRAW192E"></a> DrawPolygon(IEnumerable\<Vector> polygon, in [Matrix](../Heirloom.Math/Heirloom.Math.Matrix.md) transform) : void

Draws a simple polygon to the current surface.

<small>**polygon**: <param name="polygon">Some polygon.</param></small>  
<small>**transform**: <param name="transform">Some transform.</param></small>  

#### <a name="DRAWEEB1"></a> DrawPolygonOutline([Polygon](../Heirloom.Math/Heirloom.Math.Polygon.md) polygon, float width = 1) : void

Draws the outline of a simple polygon to the current surface.

<small>**polygon**: <param name="polygon">Some polygon.</param></small>  
<small>**width**: <param name="width">Width of the outline in pixels.</param></small>  

#### <a name="DRAW3814"></a> DrawPolygonOutline([Polygon](../Heirloom.Math/Heirloom.Math.Polygon.md) polygon, in [Matrix](../Heirloom.Math/Heirloom.Math.Matrix.md) transform, float width = 1) : void

Draws the outline of a simple polygon to the current surface.

<small>**polygon**: <param name="polygon">Some polygon.</param></small>  
<small>**transform**: <param name="transform">Some transform.</param></small>  
<small>**width**: <param name="width">Width of the outline in pixels.</param></small>  

#### <a name="DRAW1EDF"></a> DrawPolygonOutline(IEnumerable\<Vector> polygon, float width = 1) : void

Draws the outline of a simple polygon to the current surface.

<small>**polygon**: <param name="polygon">Some polygon.</param></small>  
<small>**width**: <param name="width">Width of the outline in pixels.</param></small>  

#### <a name="DRAWDC31"></a> DrawPolygonOutline(IEnumerable\<Vector> polygon, in [Matrix](../Heirloom.Math/Heirloom.Math.Matrix.md) transform, float width = 1) : void

Draws the outline of a simple polygon to the current surface.

<small>**polygon**: <param name="polygon">Some polygon.</param></small>  
<small>**transform**: <param name="transform">Some transform.</param></small>  
<small>**width**: <param name="width">Width of the outline in pixels.</param></small>  

#### <a name="DRAW87C3"></a> DrawText([StyledText](Heirloom.Drawing.StyledText.md) styledText, in [Vector](../Heirloom.Math/Heirloom.Math.Vector.md) position, [Font](Heirloom.Drawing.Font.md) font, int size, [TextAlign](Heirloom.Drawing.TextAlign.md) align = Left) : void

Draws rich text to the current surface.

<small>**styledText**: <param name="styledText">The rich text to draw.</param></small>  
<small>**position**: <param name="position">The anchor position to layout text around.</param></small>  
<small>**font**: <param name="font">The font to render with.</param></small>  
<small>**size**: <param name="size">The font size to render with.</param></small>  
<small>**align**: <param name="align">The text alignment.</param></small>  

#### <a name="DRAW2587"></a> DrawText([StyledText](Heirloom.Drawing.StyledText.md) styledText, in [Rectangle](../Heirloom.Math/Heirloom.Math.Rectangle.md) bounds, [Font](Heirloom.Drawing.Font.md) font, int size, [TextAlign](Heirloom.Drawing.TextAlign.md) align = Left) : void

Draws rich text to the current surface.

<small>**styledText**: <param name="styledText">The rich text to draw.</param></small>  
<small>**bounds**: <param name="bounds">The boundng region to layout text.</param></small>  
<small>**font**: <param name="font">The font to render with.</param></small>  
<small>**size**: <param name="size">The font size to render with.</param></small>  
<small>**align**: <param name="align">The text alignment.</param></small>  

#### <a name="DRAWB686"></a> DrawText(string text, in [Vector](../Heirloom.Math/Heirloom.Math.Vector.md) position, [Font](Heirloom.Drawing.Font.md) font, int size, [DrawTextCallback](Heirloom.Drawing.DrawTextCallback.md) callback) : void

Draws text to the current surface.

<small>**text**: <param name="text">The text to draw.</param></small>  
<small>**position**: <param name="position">The anchor position to layout text around.</param></small>  
<small>**font**: <param name="font">The font to render with.</param></small>  
<small>**size**: <param name="size">The font size to render with.</param></small>  
<small>**callback**: <param name="callback">A callback for manipulating the style of the rendered text.</param></small>  

#### <a name="DRAW8F4C"></a> DrawText(string text, in [Vector](../Heirloom.Math/Heirloom.Math.Vector.md) position, [Font](Heirloom.Drawing.Font.md) font, int size, [TextAlign](Heirloom.Drawing.TextAlign.md) align = Left, [DrawTextCallback](Heirloom.Drawing.DrawTextCallback.md) callback = "null") : void

Draws text to the current surface.

<small>**text**: <param name="text">The text to draw.</param></small>  
<small>**position**: <param name="position">The anchor position to layout text around.</param></small>  
<small>**font**: <param name="font">The font to render with.</param></small>  
<small>**size**: <param name="size">The font size to render with.</param></small>  
<small>**align**: <param name="align">The text alignment.</param></small>  
<small>**callback**: <param name="callback">A callback for manipulating the style of the rendered text.</param></small>  

#### <a name="DRAW6A2D"></a> DrawText(string text, in [Rectangle](../Heirloom.Math/Heirloom.Math.Rectangle.md) bounds, [Font](Heirloom.Drawing.Font.md) font, int size, [DrawTextCallback](Heirloom.Drawing.DrawTextCallback.md) callback) : void

Draws text to the current surface.

<small>**text**: <param name="text">The text to draw.</param></small>  
<small>**bounds**: <param name="bounds">The boundng region to layout text.</param></small>  
<small>**font**: <param name="font">The font to render with.</param></small>  
<small>**size**: <param name="size">The font size to render with.</param></small>  
<small>**callback**: <param name="callback">A callback for manipulating the style of the rendered text.</param></small>  

#### <a name="DRAW7B95"></a> DrawText(string text, in [Rectangle](../Heirloom.Math/Heirloom.Math.Rectangle.md) bounds, [Font](Heirloom.Drawing.Font.md) font, int size, [TextAlign](Heirloom.Drawing.TextAlign.md) align = Left, [DrawTextCallback](Heirloom.Drawing.DrawTextCallback.md) callback = "null") : void

Draws text to the current surface.

<small>**text**: <param name="text">The text to draw.</param></small>  
<small>**bounds**: <param name="bounds">The boundng region to layout text.</param></small>  
<small>**font**: <param name="font">The font to render with.</param></small>  
<small>**size**: <param name="size">The font size to render with.</param></small>  
<small>**align**: <param name="align">The text alignment.</param></small>  
<small>**callback**: <param name="callback">A callback for manipulating the style of the rendered text.</param></small>  

