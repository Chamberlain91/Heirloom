# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../Heirloom.Drawing/Heirloom.Drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## Graphics (Abstract Class)
<small>**Namespace**: Heirloom.Drawing</small>  

| Properties                             | Summary                                                                                         |
|----------------------------------------|-------------------------------------------------------------------------------------------------|
| [CurrentFPS](#CUR70FDB632)             | Gets how often the default surface is presented to the screen per second.                       |
| [Performance](#PERD985221C)            | Gets drawing performance information.                                                           |
| [IsDisposed](#ISD61874DA9)             | Gets a value determining if this [Graphics](Heirloom.Drawing.Graphics.md) was disposed.         |
| [IsInitialized](#ISIBEED663C)          | Gets a value determining if this [Graphics](Heirloom.Drawing.Graphics.md) has been initialized. |
| [DefaultSurface](#DEF465EF61E)         | Gets the default surface (ie, window) of this render context.                                   |
| [Surface](#SUR40785EE9)                | Gets or sets the current surface.                                                               |
| [Shader](#SHA5D122CB9)                 | Gets or sets the active shader.                                                                 |
| [Viewport](#VIE365B3434)               | Gets or sets the viewport in pixel coordinates.                                                 |
| [GlobalTransform](#GLO9D3F3F33)        | Get or sets the global transform.                                                               |
| [InverseGlobalTransform](#INV9F065FB7) | Gets the inverse of the current global transform.                                               |
| [Blending](#BLEF02A3CD5)               | Gets or sets the current blending mode.                                                         |
| [Color](#COLD1229651)                  | Gets or sets the current blending color.                                                        |

| Methods                             | Summary                                                                                                                        |
|-------------------------------------|--------------------------------------------------------------------------------------------------------------------------------|
| [ResetState](#RESA7048869)          | Reset current context state to defaults (default surface, full viewport, no transform, alpha and white).                       |
| [PushState](#PUS40C5CBDD)           | Save the context state (push it on the state stack).                                                                           |
| [PopState](#POP630D10FB)            | Restore the context state (pop from the state stack).                                                                          |
| [Clear](#CLE5D92D22C)               | Clears the current surface with the specified color.                                                                           |
| [DrawMesh](#DRA95B1E3F1)            | Draws a mesh with the given image to the current surface.                                                                      |
| [GrabPixels](#GRAEDDDAE23)          | Grab the pixels from a subregion of the current surface and return that image. (ie, a screenshot)                              |
| [GrabPixels](#GRAF748E6A9)          | Grab the pixels from the current surface and return that image. (ie, a screenshot)                                             |
| [GetDrawCounts](#GET41ADC268)       | Populates and returns drawing metrics.                                                                                         |
| [RefreshScreen](#REFB28DEE96)       | Present the drawing operations to the screen.                                                                                  |
| [SwapBuffers](#SWABF25FF09)         | Causes the back and front buffers to be swapped.                                                                               |
| [EndFrame](#ENDE20271D1)            | Called at the end of frame to do any last minute work (resetting metrics, buffers, etc).                                       |
| [Flush](#FLU7815CD9A)               | Submit all pending drawing operations, optionally blocking for completion.                                                     |
| [Commit](#COM5B23B1FC)              | Commits pending drawing operations, blocking until all operations complete.                                                    |
| [Dispose](#DISFDE72264)             | Dispose and cleanup resources.                                                                                                 |
| [Dispose](#DIS4E62D250)             | Dispose this graphics context, freeing any resources occupied by it.                                                           |
| [SetCameraTransform](#SET55A77415)  | Sets `Heirloom.Drawing.Graphics.GlobalTransform` to mimic a 2D camera. The center of the camera is set to `center`.            |
| [DrawImage](#DRA5AECA746)           | Draws an image to the current surface.                                                                                         |
| [DrawImage](#DRACC3CC47E)           | Draws an image to the current surface.                                                                                         |
| [DrawImage](#DRAAA1FB811)           | Draws an image to the current surface.                                                                                         |
| [DrawImage](#DRA2104C49C)           | Draws an image stretched to fill a rectangular region to the current surface ignoring the image origin offset.                 |
| [DrawImage](#DRACC91BF51)           | Draws an image to the current surface.                                                                                         |
| [DrawSubImage](#DRAB2D3B729)        | Draws a sub-region of an image stretched to fill a rectangular region to the current surface ignoring the image origin offset. |
| [DrawSubImage](#DRA91EED5E9)        | Draws a sub-region of an image to the current surface ignoring the image origin offset.                                        |
| [DrawSubImage](#DRAA1F15CBA)        | Draws a sub-region of an image to the current surface ignoring the image origin offset.                                        |
| [DrawSprite](#DRA3D4FFB62)          | Draw a sprite to the current surface.                                                                                          |
| [DrawNineSlice](#DRA3F04D01B)       |                                                                                                                                |
| [Apply](#APP7809999F)               | Applies the specified surface effect to the current surface.                                                                   |
| [Blit](#BLI2FABC2F2)                | Overwrites an image to target surface.                                                                                         |
| [DrawLine](#DRA5FD38818)            | Draws a line segment between two points to the current surface.                                                                |
| [DrawCurve](#DRA1479E674)           | Draws a quadratic curve using three control points to the current surface.                                                     |
| [DrawCurve](#DRA600226C8)           | Draws a cubic curve using four control points to the current surface.                                                          |
| [DrawRect](#DRAA9530853)            | Draws a rectangle to the current surface.                                                                                      |
| [DrawRectOutline](#DRAFE423E7D)     | Draws the outline of a rectangel to the current surface.                                                                       |
| [DrawCross](#DRAF5B1CCE3)           | Draws a simple axis aligned 'cross' or 'plus' shape, useful for debugging positions.                                           |
| [DrawCircle](#DRA5A371506)          | Draws a circle to the current surface.                                                                                         |
| [DrawCircle](#DRA1B07AE29)          | Draws a circle to the current surface.                                                                                         |
| [DrawCircleOutline](#DRABDDF3480)   | Draws the outline of a circle to the current surface.                                                                          |
| [DrawCircleOutline](#DRAF367D6B)    | Draws the outline of a circle to the current surface.                                                                          |
| [DrawTriangle](#DRA51237C46)        | Draw a triangle to the current surface.                                                                                        |
| [DrawTriangle](#DRAF3C0F560)        | Draw a triangle outline to the current surface.                                                                                |
| [DrawTriangleOutline](#DRA2E3976E8) | Draw a triangle outline to the current surface.                                                                                |
| [DrawTriangleOutline](#DRA69688A8E) | Draw a triangle outline to the current surface.                                                                                |
| [DrawPolygon](#DRAFC9E988)          | Draws a regular polygon to the current surface.                                                                                |
| [DrawPolygonOutline](#DRA11055056)  | Draws the outline of a regular polygon to the current surface.                                                                 |
| [DrawPolygon](#DRAF6BAE1ED)         | Draws a simple polygon to the current surface.                                                                                 |
| [DrawPolygon](#DRAFD160EA8)         | Draws a simple polygon to the current surface.                                                                                 |
| [DrawPolygon](#DRA2980A4EB)         | Draws a simple polygon to the current surface.                                                                                 |
| [DrawPolygon](#DRA192EC12E)         | Draws a simple polygon to the current surface.                                                                                 |
| [DrawPolygonOutline](#DRAEEB1914F)  | Draws the outline of a simple polygon to the current surface.                                                                  |
| [DrawPolygonOutline](#DRA3814BD0E)  | Draws the outline of a simple polygon to the current surface.                                                                  |
| [DrawPolygonOutline](#DRA1EDFEE5)   | Draws the outline of a simple polygon to the current surface.                                                                  |
| [DrawPolygonOutline](#DRADC317460)  | Draws the outline of a simple polygon to the current surface.                                                                  |
| [DrawText](#DRA87C34A5)             | Draws rich text to the current surface.                                                                                        |
| [DrawText](#DRA258730C9)            | Draws rich text to the current surface.                                                                                        |
| [DrawText](#DRAB686520E)            | Draws text to the current surface.                                                                                             |
| [DrawText](#DRA3336BBF4)            | Draws text to the current surface.                                                                                             |
| [DrawText](#DRA6A2DA0DC)            | Draws text to the current surface.                                                                                             |
| [DrawText](#DRA58EE00B2)            | Draws text to the current surface.                                                                                             |

### Constructors

#### Graphics([Surface](Heirloom.Drawing.Surface.md) surface)

Constructs a new graphics instance with the specified multisampling quality.

### Properties

#### <a name="CUR70FDB632"></a>CurrentFPS : float

<small>`Read Only`</small>

Gets how often the default surface is presented to the screen per second.

#### <a name="PERD985221C"></a>Performance : [DrawingPerformance](Heirloom.Drawing.DrawingPerformance.md)

<small>`Read Only`</small>

Gets drawing performance information.

#### <a name="ISD61874DA9"></a>IsDisposed : bool


Gets a value determining if this [Graphics](Heirloom.Drawing.Graphics.md) was disposed.

#### <a name="ISIBEED663C"></a>IsInitialized : bool

<small>`Read Only`</small>

Gets a value determining if this [Graphics](Heirloom.Drawing.Graphics.md) has been initialized.

#### <a name="DEF465EF61E"></a>DefaultSurface : [Surface](Heirloom.Drawing.Surface.md)

<small>`Read Only`</small>

Gets the default surface (ie, window) of this render context.

#### <a name="SUR40785EE9"></a>Surface : [Surface](Heirloom.Drawing.Surface.md)


Gets or sets the current surface.

When changed, the viewport is automatically reset to the full surface.

#### <a name="SHA5D122CB9"></a>Shader : [Shader](Heirloom.Drawing.Shader.md)


Gets or sets the active shader.

#### <a name="VIE365B3434"></a>Viewport : [IntRectangle](../Heirloom.Math/Heirloom.Math.IntRectangle.md)


Gets or sets the viewport in pixel coordinates.

When `Heirloom.Drawing.Graphics.Surface` is changed, the viewport is automatically reset to the full surface.

#### <a name="GLO9D3F3F33"></a>GlobalTransform : [Matrix](../Heirloom.Math/Heirloom.Math.Matrix.md)


Get or sets the global transform.

#### <a name="INV9F065FB7"></a>InverseGlobalTransform : [Matrix](../Heirloom.Math/Heirloom.Math.Matrix.md)

<small>`Read Only`</small>

Gets the inverse of the current global transform.

#### <a name="BLEF02A3CD5"></a>Blending : [Blending](Heirloom.Drawing.Blending.md)


Gets or sets the current blending mode.

#### <a name="COLD1229651"></a>Color : [Color](Heirloom.Drawing.Color.md)


Gets or sets the current blending color.

### Methods

#### <a name="RESA7048869"></a>ResetState() : void

Reset current context state to defaults (default surface, full viewport, no transform, alpha and white).

#### <a name="PUS40C5CBDD"></a>PushState(bool reset = False) : void

Save the context state (push it on the state stack).


#### <a name="POP630D10FB"></a>PopState() : void

Restore the context state (pop from the state stack).

#### <a name="CLE5D92D22C"></a>Clear([Color](Heirloom.Drawing.Color.md) color) : void
<small>`Abstract`</small>

Clears the current surface with the specified color.


#### <a name="DRA95B1E3F1"></a>DrawMesh([ImageSource](Heirloom.Drawing.ImageSource.md) image, [Mesh](Heirloom.Drawing.Mesh.md) mesh, in [Matrix](../Heirloom.Math/Heirloom.Math.Matrix.md) transform) : void
<small>`Abstract`</small>

Draws a mesh with the given image to the current surface.

<small>**image**: <param name="image">Some image.</param></small>  
<small>**mesh**: <param name="mesh">Some mesh.</param></small>  
<small>**transform**: <param name="transform">Some transform.</param></small>  

#### <a name="GRAEDDDAE23"></a>GrabPixels([IntRectangle](../Heirloom.Math/Heirloom.Math.IntRectangle.md) region) : [Image](Heirloom.Drawing.Image.md)
<small>`Abstract`</small>

Grab the pixels from a subregion of the current surface and return that image. (ie, a screenshot)

<small>**region**: <param name="region">A region within the currently set surface.</param></small>  

#### <a name="GRAF748E6A9"></a>GrabPixels() : [Image](Heirloom.Drawing.Image.md)

Grab the pixels from the current surface and return that image. (ie, a screenshot)

#### <a name="GET41ADC268"></a>GetDrawCounts() : [Graphics.DrawCounts](Heirloom.Drawing.Graphics.DrawCounts.md)
<small>`Abstract`, `Protected`</small>

Populates and returns drawing metrics.

Counts should be reset in the implementation of `Heirloom.Drawing.Graphics.EndFrame`.

#### <a name="REFB28DEE96"></a>RefreshScreen() : void

Present the drawing operations to the screen.

#### <a name="SWABF25FF09"></a>SwapBuffers() : void
<small>`Abstract`, `Protected`</small>

Causes the back and front buffers to be swapped.

#### <a name="ENDE20271D1"></a>EndFrame() : void
<small>`Abstract`, `Protected`</small>

Called at the end of frame to do any last minute work (resetting metrics, buffers, etc).

#### <a name="FLU7815CD9A"></a>Flush(bool blockCompletion = False) : void
<small>`Abstract`, `Protected`</small>

Submit all pending drawing operations, optionally blocking for completion.


#### <a name="COM5B23B1FC"></a>Commit() : void

Commits pending drawing operations, blocking until all operations complete.

#### <a name="DISFDE72264"></a>Dispose(bool disposeManaged) : void
<small>`Abstract`, `Protected`</small>

Dispose and cleanup resources.


#### <a name="DIS4E62D250"></a>Dispose() : void

Dispose this graphics context, freeing any resources occupied by it.

#### <a name="SET55A77415"></a>SetCameraTransform([Vector](../Heirloom.Math/Heirloom.Math.Vector.md) center, float scale = 1) : void

Sets `Heirloom.Drawing.Graphics.GlobalTransform` to mimic a 2D camera. The center of the camera is set to `center`.


#### <a name="DRA5AECA746"></a>DrawImage([ImageSource](Heirloom.Drawing.ImageSource.md) image, in [Vector](../Heirloom.Math/Heirloom.Math.Vector.md) position) : void

Draws an image to the current surface.

<small>**image**: <param name="image">Some image.</param></small>  
<small>**position**: <param name="position">The position of the image.</param></small>  

#### <a name="DRACC3CC47E"></a>DrawImage([ImageSource](Heirloom.Drawing.ImageSource.md) image, in [Vector](../Heirloom.Math/Heirloom.Math.Vector.md) position, float rotation) : void

Draws an image to the current surface.

<small>**image**: <param name="image">Some image.</param></small>  
<small>**position**: <param name="position">The position of the image.</param></small>  
<small>**rotation**: <param name="rotation">The rotation applied to the image.</param></small>  

#### <a name="DRAAA1FB811"></a>DrawImage([ImageSource](Heirloom.Drawing.ImageSource.md) image, in [Vector](../Heirloom.Math/Heirloom.Math.Vector.md) position, float rotation, in [Vector](../Heirloom.Math/Heirloom.Math.Vector.md) scale) : void

Draws an image to the current surface.

<small>**image**: <param name="image">Some image.</param></small>  
<small>**position**: <param name="position">The position of the image.</param></small>  
<small>**rotation**: <param name="rotation">The rotation applied to the image.</param></small>  
<small>**scale**: <param name="scale">The scale applied to the image.</param></small>  

#### <a name="DRA2104C49C"></a>DrawImage([ImageSource](Heirloom.Drawing.ImageSource.md) image, in [Rectangle](../Heirloom.Math/Heirloom.Math.Rectangle.md) rectangle) : void

Draws an image stretched to fill a rectangular region to the current surface ignoring the image origin offset.

<small>**image**: <param name="image">Some image.</param></small>  
<small>**rectangle**: <param name="rectangle">The bounds of the drawn image.</param></small>  

#### <a name="DRACC91BF51"></a>DrawImage([ImageSource](Heirloom.Drawing.ImageSource.md) image, in [Matrix](../Heirloom.Math/Heirloom.Math.Matrix.md) transform) : void

Draws an image to the current surface.

<small>**image**: <param name="image">Some image.</param></small>  
<small>**transform**: <param name="transform">Some transform.</param></small>  

#### <a name="DRAB2D3B729"></a>DrawSubImage([ImageSource](Heirloom.Drawing.ImageSource.md) image, in [IntRectangle](../Heirloom.Math/Heirloom.Math.IntRectangle.md) region, in [Rectangle](../Heirloom.Math/Heirloom.Math.Rectangle.md) rectangle) : void

Draws a sub-region of an image stretched to fill a rectangular region to the current surface ignoring the image origin offset.

<small>**image**: <param name="image">Some image.</param></small>  
<small>**region**: <param name="region">Some subregion of the image.</param></small>  
<small>**rectangle**: <param name="rectangle">The bounds of the drawn image.</param></small>  

#### <a name="DRA91EED5E9"></a>DrawSubImage([ImageSource](Heirloom.Drawing.ImageSource.md) image, in [IntRectangle](../Heirloom.Math/Heirloom.Math.IntRectangle.md) region, in [Vector](../Heirloom.Math/Heirloom.Math.Vector.md) position) : void

Draws a sub-region of an image to the current surface ignoring the image origin offset.

<small>**image**: <param name="image">Some image.</param></small>  
<small>**region**: <param name="region">Some subregion of the image.</param></small>  
<small>**position**: <param name="position">The position of the image.</param></small>  

#### <a name="DRAA1F15CBA"></a>DrawSubImage([ImageSource](Heirloom.Drawing.ImageSource.md) image, in [IntRectangle](../Heirloom.Math/Heirloom.Math.IntRectangle.md) region, in [Matrix](../Heirloom.Math/Heirloom.Math.Matrix.md) transform) : void

Draws a sub-region of an image to the current surface ignoring the image origin offset.

<small>**image**: <param name="image">Some image.</param></small>  
<small>**region**: <param name="region">Some subregion of the image.</param></small>  
<small>**transform**: <param name="transform">Some transform.</param></small>  

#### <a name="DRA3D4FFB62"></a>DrawSprite([Sprite](Heirloom.Drawing.Sprite.md) sprite, int index, in [Matrix](../Heirloom.Math/Heirloom.Math.Matrix.md) transform) : void

Draw a sprite to the current surface.

<small>**sprite**: <param name="sprite">Some sprite.</param></small>  
<small>**index**: <param name="index">Some valid frame number within the sprite.</param></small>  
<small>**transform**: <param name="transform">Some transform to draw the sprite.</param></small>  

#### <a name="DRA3F04D01B"></a>DrawNineSlice([NineSlice](Heirloom.Drawing.NineSlice.md) slice, [Rectangle](../Heirloom.Math/Heirloom.Math.Rectangle.md) rectangle) : void


#### <a name="APP7809999F"></a>Apply([SurfaceEffect](Heirloom.Drawing.SurfaceEffect.md) effect) : void

Applies the specified surface effect to the current surface.


#### <a name="BLI2FABC2F2"></a>Blit([ImageSource](Heirloom.Drawing.ImageSource.md) source, [Surface](Heirloom.Drawing.Surface.md) target) : void

Overwrites an image to target surface.


#### <a name="DRA5FD38818"></a>DrawLine(in [Vector](../Heirloom.Math/Heirloom.Math.Vector.md) p0, in [Vector](../Heirloom.Math/Heirloom.Math.Vector.md) p1, float width = 1) : void

Draws a line segment between two points to the current surface.

<small>**p0**: <param name="p0">The start point.</param></small>  
<small>**p1**: <param name="p1">The end point.</param></small>  
<small>**width**: <param name="width">The thickness of the line in pixels.</param></small>  

#### <a name="DRA1479E674"></a>DrawCurve(in [Vector](../Heirloom.Math/Heirloom.Math.Vector.md) p0, in [Vector](../Heirloom.Math/Heirloom.Math.Vector.md) p1, in [Vector](../Heirloom.Math/Heirloom.Math.Vector.md) p2, float width = 1) : void

Draws a quadratic curve using three control points to the current surface.

<small>**p0**: <param name="p0">The first control point.</param></small>  
<small>**p1**: <param name="p1">The second control point.</param></small>  
<small>**p2**: <param name="p2">The third control point.</param></small>  
<small>**width**: <param name="width">The thickness of the line in pixels.</param></small>  

#### <a name="DRA600226C8"></a>DrawCurve(in [Vector](../Heirloom.Math/Heirloom.Math.Vector.md) p0, in [Vector](../Heirloom.Math/Heirloom.Math.Vector.md) p1, in [Vector](../Heirloom.Math/Heirloom.Math.Vector.md) p2, in [Vector](../Heirloom.Math/Heirloom.Math.Vector.md) p3, float width = 1) : void

Draws a cubic curve using four control points to the current surface.

<small>**p0**: <param name="p0">The first control point.</param></small>  
<small>**p1**: <param name="p1">The second control point.</param></small>  
<small>**p2**: <param name="p2">The third control point.</param></small>  
<small>**p3**: <param name="p3">The fourth control point.</param></small>  
<small>**width**: <param name="width">The thickness of the line in pixels.</param></small>  

#### <a name="DRAA9530853"></a>DrawRect(in [Rectangle](../Heirloom.Math/Heirloom.Math.Rectangle.md) rectangle) : void

Draws a rectangle to the current surface.

<small>**rectangle**: <param name="rectangle">The rectangular region of the rectangle.</param></small>  

#### <a name="DRAFE423E7D"></a>DrawRectOutline(in [Rectangle](../Heirloom.Math/Heirloom.Math.Rectangle.md) rectangle, float width = 1) : void

Draws the outline of a rectangel to the current surface.

<small>**rectangle**: <param name="rectangle">The rectangular region of the rectangle.</param></small>  
<small>**width**: <param name="width">Width of the outline in pixels.</param></small>  

#### <a name="DRAF5B1CCE3"></a>DrawCross(in [Vector](../Heirloom.Math/Heirloom.Math.Vector.md) center, float size = 2, float width = 1) : void

Draws a simple axis aligned 'cross' or 'plus' shape, useful for debugging positions.

<small>**center**: <param name="center">The position of the cross.</param></small>  
<small>**size**: <param name="size">Size in screen pixels (not world space).</param></small>  
<small>**width**: <param name="width">Width of the lines screen pixels (not world space).</param></small>  

#### <a name="DRA5A371506"></a>DrawCircle(in [Circle](../Heirloom.Math/Heirloom.Math.Circle.md) circle) : void

Draws a circle to the current surface.

<small>**circle**: <param name="circle">The circle to draw.</param></small>  

#### <a name="DRA1B07AE29"></a>DrawCircle(in [Vector](../Heirloom.Math/Heirloom.Math.Vector.md) position, float radius) : void

Draws a circle to the current surface.

<small>**position**: <param name="position">The center of the circle.</param></small>  
<small>**radius**: <param name="radius">The radius of the circle.</param></small>  

#### <a name="DRABDDF3480"></a>DrawCircleOutline(in [Circle](../Heirloom.Math/Heirloom.Math.Circle.md) circle, float width = 1) : void

Draws the outline of a circle to the current surface.

<small>**circle**: <param name="circle">The circle to draw.</param></small>  
<small>**width**: <param name="width">Width of the outline in pixels.</param></small>  

#### <a name="DRAF367D6B"></a>DrawCircleOutline(in [Vector](../Heirloom.Math/Heirloom.Math.Vector.md) position, float radius, float width = 1) : void

Draws the outline of a circle to the current surface.

<small>**position**: <param name="position">The centr of the circle.</param></small>  
<small>**radius**: <param name="radius">The radius of the circle.</param></small>  
<small>**width**: <param name="width">Width of the outline in pixels.</param></small>  

#### <a name="DRA51237C46"></a>DrawTriangle(in [Triangle](../Heirloom.Math/Heirloom.Math.Triangle.md) triangle) : void

Draw a triangle to the current surface.

<small>**triangle**: <param name="triangle">The triangle to draw.</param></small>  

#### <a name="DRAF3C0F560"></a>DrawTriangle(in [Vector](../Heirloom.Math/Heirloom.Math.Vector.md) a, in [Vector](../Heirloom.Math/Heirloom.Math.Vector.md) b, in [Vector](../Heirloom.Math/Heirloom.Math.Vector.md) c) : void

Draw a triangle outline to the current surface.

<small>**a**: <param name="a">The first point.</param></small>  
<small>**b**: <param name="b">The second point.</param></small>  
<small>**c**: <param name="c">The third point.</param></small>  

#### <a name="DRA2E3976E8"></a>DrawTriangleOutline(in [Triangle](../Heirloom.Math/Heirloom.Math.Triangle.md) triangle, float width = 1) : void

Draw a triangle outline to the current surface.

<small>**triangle**: <param name="triangle">The triangle to draw.</param></small>  
<small>**width**: <param name="width">The thickness of the line in pixels.</param></small>  

#### <a name="DRA69688A8E"></a>DrawTriangleOutline(in [Vector](../Heirloom.Math/Heirloom.Math.Vector.md) a, in [Vector](../Heirloom.Math/Heirloom.Math.Vector.md) b, in [Vector](../Heirloom.Math/Heirloom.Math.Vector.md) c, float width = 1) : void

Draw a triangle outline to the current surface.

<small>**a**: <param name="a">The first point.</param></small>  
<small>**b**: <param name="b">The second point.</param></small>  
<small>**c**: <param name="c">The third point.</param></small>  
<small>**width**: <param name="width">The thickness of the line in pixels.</param></small>  

#### <a name="DRAFC9E988"></a>DrawPolygon(in [Vector](../Heirloom.Math/Heirloom.Math.Vector.md) position, int sides, float radius) : void

Draws a regular polygon to the current surface.

<small>**sides**: <param name="sides">The number of sides in the regular polygon.</param></small>  
<small>**radius**: <param name="radius">The radius of the regular polygon.</param></small>  

#### <a name="DRA11055056"></a>DrawPolygonOutline(in [Vector](../Heirloom.Math/Heirloom.Math.Vector.md) position, int sides, float radius, float width = 1) : void

Draws the outline of a regular polygon to the current surface.

<small>**sides**: <param name="sides">The number of sides in the regular polygon.</param></small>  
<small>**radius**: <param name="radius">The radius of the regular polygon.</param></small>  
<small>**width**: <param name="width">Width of the outline in pixels.</param></small>  

#### <a name="DRAF6BAE1ED"></a>DrawPolygon([Polygon](../Heirloom.Math/Heirloom.Math.Polygon.md) polygon) : void

Draws a simple polygon to the current surface.

<small>**polygon**: <param name="polygon">Some polygon.</param></small>  

#### <a name="DRAFD160EA8"></a>DrawPolygon([Polygon](../Heirloom.Math/Heirloom.Math.Polygon.md) polygon, in [Matrix](../Heirloom.Math/Heirloom.Math.Matrix.md) transform) : void

Draws a simple polygon to the current surface.

<small>**polygon**: <param name="polygon">Some polygon.</param></small>  
<small>**transform**: <param name="transform">Some transform.</param></small>  

#### <a name="DRA2980A4EB"></a>DrawPolygon(IEnumerable\<Vector> polygon) : void

Draws a simple polygon to the current surface.

<small>**polygon**: <param name="polygon">Some polygon.</param></small>  

#### <a name="DRA192EC12E"></a>DrawPolygon(IEnumerable\<Vector> polygon, in [Matrix](../Heirloom.Math/Heirloom.Math.Matrix.md) transform) : void

Draws a simple polygon to the current surface.

<small>**polygon**: <param name="polygon">Some polygon.</param></small>  
<small>**transform**: <param name="transform">Some transform.</param></small>  

#### <a name="DRAEEB1914F"></a>DrawPolygonOutline([Polygon](../Heirloom.Math/Heirloom.Math.Polygon.md) polygon, float width = 1) : void

Draws the outline of a simple polygon to the current surface.

<small>**polygon**: <param name="polygon">Some polygon.</param></small>  
<small>**width**: <param name="width">Width of the outline in pixels.</param></small>  

#### <a name="DRA3814BD0E"></a>DrawPolygonOutline([Polygon](../Heirloom.Math/Heirloom.Math.Polygon.md) polygon, in [Matrix](../Heirloom.Math/Heirloom.Math.Matrix.md) transform, float width = 1) : void

Draws the outline of a simple polygon to the current surface.

<small>**polygon**: <param name="polygon">Some polygon.</param></small>  
<small>**transform**: <param name="transform">Some transform.</param></small>  
<small>**width**: <param name="width">Width of the outline in pixels.</param></small>  

#### <a name="DRA1EDFEE5"></a>DrawPolygonOutline(IEnumerable\<Vector> polygon, float width = 1) : void

Draws the outline of a simple polygon to the current surface.

<small>**polygon**: <param name="polygon">Some polygon.</param></small>  
<small>**width**: <param name="width">Width of the outline in pixels.</param></small>  

#### <a name="DRADC317460"></a>DrawPolygonOutline(IEnumerable\<Vector> polygon, in [Matrix](../Heirloom.Math/Heirloom.Math.Matrix.md) transform, float width = 1) : void

Draws the outline of a simple polygon to the current surface.

<small>**polygon**: <param name="polygon">Some polygon.</param></small>  
<small>**transform**: <param name="transform">Some transform.</param></small>  
<small>**width**: <param name="width">Width of the outline in pixels.</param></small>  

#### <a name="DRA87C34A5"></a>DrawText([StyledText](Heirloom.Drawing.StyledText.md) styledText, in [Vector](../Heirloom.Math/Heirloom.Math.Vector.md) position, [Font](Heirloom.Drawing.Font.md) font, int size, [TextAlign](Heirloom.Drawing.TextAlign.md) align = Left) : void

Draws rich text to the current surface.

<small>**styledText**: <param name="styledText">The rich text to draw.</param></small>  
<small>**position**: <param name="position">The anchor position to layout text around.</param></small>  
<small>**font**: <param name="font">The font to render with.</param></small>  
<small>**size**: <param name="size">The font size to render with.</param></small>  
<small>**align**: <param name="align">The text alignment.</param></small>  

#### <a name="DRA258730C9"></a>DrawText([StyledText](Heirloom.Drawing.StyledText.md) styledText, in [Rectangle](../Heirloom.Math/Heirloom.Math.Rectangle.md) bounds, [Font](Heirloom.Drawing.Font.md) font, int size, [TextAlign](Heirloom.Drawing.TextAlign.md) align = Left) : void

Draws rich text to the current surface.

<small>**styledText**: <param name="styledText">The rich text to draw.</param></small>  
<small>**bounds**: <param name="bounds">The boundng region to layout text.</param></small>  
<small>**font**: <param name="font">The font to render with.</param></small>  
<small>**size**: <param name="size">The font size to render with.</param></small>  
<small>**align**: <param name="align">The text alignment.</param></small>  

#### <a name="DRAB686520E"></a>DrawText(string text, in [Vector](../Heirloom.Math/Heirloom.Math.Vector.md) position, [Font](Heirloom.Drawing.Font.md) font, int size, [DrawTextCallback](Heirloom.Drawing.DrawTextCallback.md) callback) : void

Draws text to the current surface.

<small>**text**: <param name="text">The text to draw.</param></small>  
<small>**position**: <param name="position">The anchor position to layout text around.</param></small>  
<small>**font**: <param name="font">The font to render with.</param></small>  
<small>**size**: <param name="size">The font size to render with.</param></small>  
<small>**callback**: <param name="callback">A callback for manipulating the style of the rendered text.</param></small>  

#### <a name="DRA3336BBF4"></a>DrawText(string text, in [Vector](../Heirloom.Math/Heirloom.Math.Vector.md) position, [Font](Heirloom.Drawing.Font.md) font, int size, [TextAlign](Heirloom.Drawing.TextAlign.md) align = Left, [DrawTextCallback](Heirloom.Drawing.DrawTextCallback.md) callback = null) : void

Draws text to the current surface.

<small>**text**: <param name="text">The text to draw.</param></small>  
<small>**position**: <param name="position">The anchor position to layout text around.</param></small>  
<small>**font**: <param name="font">The font to render with.</param></small>  
<small>**size**: <param name="size">The font size to render with.</param></small>  
<small>**align**: <param name="align">The text alignment.</param></small>  
<small>**callback**: <param name="callback">A callback for manipulating the style of the rendered text.</param></small>  

#### <a name="DRA6A2DA0DC"></a>DrawText(string text, in [Rectangle](../Heirloom.Math/Heirloom.Math.Rectangle.md) bounds, [Font](Heirloom.Drawing.Font.md) font, int size, [DrawTextCallback](Heirloom.Drawing.DrawTextCallback.md) callback) : void

Draws text to the current surface.

<small>**text**: <param name="text">The text to draw.</param></small>  
<small>**bounds**: <param name="bounds">The boundng region to layout text.</param></small>  
<small>**font**: <param name="font">The font to render with.</param></small>  
<small>**size**: <param name="size">The font size to render with.</param></small>  
<small>**callback**: <param name="callback">A callback for manipulating the style of the rendered text.</param></small>  

#### <a name="DRA58EE00B2"></a>DrawText(string text, in [Rectangle](../Heirloom.Math/Heirloom.Math.Rectangle.md) bounds, [Font](Heirloom.Drawing.Font.md) font, int size, [TextAlign](Heirloom.Drawing.TextAlign.md) align = Left, [DrawTextCallback](Heirloom.Drawing.DrawTextCallback.md) callback = null) : void

Draws text to the current surface.

<small>**text**: <param name="text">The text to draw.</param></small>  
<small>**bounds**: <param name="bounds">The boundng region to layout text.</param></small>  
<small>**font**: <param name="font">The font to render with.</param></small>  
<small>**size**: <param name="size">The font size to render with.</param></small>  
<small>**align**: <param name="align">The text alignment.</param></small>  
<small>**callback**: <param name="callback">A callback for manipulating the style of the rendered text.</param></small>  

