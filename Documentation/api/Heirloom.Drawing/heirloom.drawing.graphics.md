# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../heirloom.drawing/heirloom.drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## Graphics (Abstract Class)
<small>**Namespace**: Heirloom.Drawing</sub></small>  

| Properties | Summary |
|------------|---------|
| [Capabilities](#CAPE7660DA4) | Gets the queried capabilities (ie, limits) for the current device. |
| [CurrentFPS](#CUR70FDB632) | Gets how often the default surface is presented to the screen per second. |
| [Performance](#PERD985221C) | Gets drawing performance information. |
| [IsDisposed](#ISD61874DA9) | Gets a value determining if this [Graphics](heirloom.drawing.graphics.md) was disposed. |
| [DefaultSurface](#DEF465EF61E) | Gets the default surface (ie, window) of this render context. |
| [Surface](#SUR40785EE9) | Gets or sets the current surface. |
| [Shader](#SHA5D122CB9) | Gets or sets the active shader. |
| [Viewport](#VIE365B3434) | Gets or sets the viewport in normalized coordinates. |
| [ViewportScreen](#VIE9EEFEE58) | Gets the size of viewport in pixel coordinates. |
| [GlobalTransform](#GLO9D3F3F33) | Get or sets the global transform. |
| [InverseGlobalTransform](#INV9F065FB7) | Gets the inverse of the current global transform. |
| [Blending](#BLEF02A3CD5) | Gets or sets the current blending mode. |
| [Color](#COLD1229651) | Gets or sets the current blending color. |

| Methods | Summary |
|---------|---------|
| [ResetState](#RESA7048869) | Reset current context state to defaults (default surface, full viewport, no transform, alpha and white). |
| [PushState](#PUSDD336044) | Save the context state (push it on the state stack). |
| [PopState](#POP630D10FB) | Restore the context state (pop from the state stack). |
| [Clear](#CLE34E7AECC) | Clears the current surface with the specified color. |
| [DrawMesh](#DRAAF0AEF1) | Draws a mesh with the given image to the current surface. |
| [GrabPixels](#GRAF86FCD83) | Grab the pixels from a subregion of the current surface and return that image. (ie, a screenshot) |
| [GrabPixels](#GRAF1483889) | Grab the pixels from the current surface and return that image. (ie, a screenshot) |
| [GetDrawCounts](#GETBA906748) |  |
| [RefreshScreen](#REFB28DEE96) | Present the drawing operations to the screen. |
| [SwapBuffers](#SWABF25FF09) |  |
| [EndFrame](#ENDE20271D1) |  |
| [Flush](#FLU2F0EB18F) | Force any pending drawing operations to complete. |
| [Dispose](#DISD833FA7A) |  |
| [Dispose](#DIS4E62D250) | Dispose this graphics context, freeing any resources occupied by it. |
| [SetCameraTransform](#SETB12B2875) | Sets `Heirloom.Drawing.Graphics.GlobalTransform` to mimic a 2D camera. |
| [DrawImage](#DRAC2E366) | Draws an image to the current surface. |
| [DrawImage](#DRAD633A69E) | Draws an image to the current surface. |
| [DrawImage](#DRA424A37D1) | Draws an image to the current surface. |
| [DrawImage](#DRA6852F87C) | Draws an image stretched to fill a rectangular region to the current surface ignoring the image origin offset. |
| [DrawImage](#DRAEF5F7A71) | Draws an image to the current surface. |
| [DrawSprite](#DRAA4FF1762) | Draw a sprite to the current surface. |
| [DrawLine](#DRAF3BF7918) | Draws a line segment between two points to the current surface. |
| [DrawCurve](#DRAC0DF3294) | Draws a quadratic curve using three control points to the current surface. |
| [DrawCurve](#DRA80187F08) | Draws a cubic curve using four control points to the current surface. |
| [DrawRect](#DRA3B97A8B3) | Draws a rectangle to the current surface. |
| [DrawRectOutline](#DRA2705A75D) | Draws the outline of a rectangel to the current surface. |
| [DrawCross](#DRA24DFB43) | Draws a simple axis aligned 'cross' or 'plus' shape, useful for debugging positions. |
| [DrawCircle](#DRA670793A6) | Draws a circle to the current surface. |
| [DrawCircle](#DRA3D334749) | Draws a circle to the current surface. |
| [DrawCircleOutline](#DRAAD650C20) | Draws the outline of a circle to the current surface. |
| [DrawCircleOutline](#DRAF45EDC8B) | Draws the outline of a circle to the current surface. |
| [DrawTriangle](#DRA1A8B9DE6) | Draw a triangle to the current surface. |
| [DrawTriangle](#DRADBC38A80) | Draw a triangle outline to the current surface. |
| [DrawTriangleOutline](#DRA23E5C748) | Draw a triangle outline to the current surface. |
| [DrawTriangleOutline](#DRA378F642E) | Draw a triangle outline to the current surface. |
| [DrawPolygon](#DRAE93A728) | Draws a regular polygon to the current surface. |
| [DrawPolygonOutline](#DRA3C696336) | Draws the outline of a regular polygon to the current surface. |
| [DrawPolygon](#DRABD5A3BCD) | Draws a simple polygon to the current surface. |
| [DrawPolygon](#DRA1B52F1E8) | Draws a simple polygon to the current surface. |
| [DrawPolygon](#DRA2980A4EB) | Draws a simple polygon to the current surface. |
| [DrawPolygon](#DRA8F19084E) | Draws a simple polygon to the current surface. |
| [DrawPolygonOutline](#DRA2ED9F46F) | Draws the outline of a simple polygon to the current surface. |
| [DrawPolygonOutline](#DRA813864CE) | Draws the outline of a simple polygon to the current surface. |
| [DrawPolygonOutline](#DRA1EDFEE5) | Draws the outline of a simple polygon to the current surface. |
| [DrawPolygonOutline](#DRAA0746100) | Draws the outline of a simple polygon to the current surface. |
| [DrawText](#DRA1A3115E4) | Draws rich text to the current surface. |
| [DrawText](#DRAD92FC516) | Draws rich text to the current surface. |
| [DrawText](#DRA4E2326AE) | Draws text to the current surface. |
| [DrawText](#DRAB39D5DE6) | Draws text to the current surface. |
| [DrawText](#DRA862A32BC) | Draws text to the current surface. |
| [DrawText](#DRAAB0F18) | Draws text to the current surface. |

### Constructors

#### Graphics([MultisampleQuality](heirloom.drawing.multisamplequality.md) multisample)

### Properties

#### <a name="CAPE7660DA4"></a>Capabilities : [GraphicsCapabilities](heirloom.drawing.graphicscapabilities.md)

<small>`Read Only`</small>

Gets the queried capabilities (ie, limits) for the current device.

#### <a name="CUR70FDB632"></a>CurrentFPS : float

<small>`Read Only`</small>

Gets how often the default surface is presented to the screen per second.

#### <a name="PERD985221C"></a>Performance : [DrawingPerformance](heirloom.drawing.drawingperformance.md)

<small>`Read Only`</small>

Gets drawing performance information.

#### <a name="ISD61874DA9"></a>IsDisposed : bool

<small>`Read Only`</small>

Gets a value determining if this [Graphics](heirloom.drawing.graphics.md) was disposed.

#### <a name="DEF465EF61E"></a>DefaultSurface : [Surface](heirloom.drawing.surface.md)

<small>`Read Only`</small>

Gets the default surface (ie, window) of this render context.

#### <a name="SUR40785EE9"></a>Surface : [Surface](heirloom.drawing.surface.md)


Gets or sets the current surface.

#### <a name="SHA5D122CB9"></a>Shader : [Shader](heirloom.drawing.shader.md)


Gets or sets the active shader.

#### <a name="VIE365B3434"></a>Viewport : [Rectangle](../heirloom.math/heirloom.math.rectangle.md)


Gets or sets the viewport in normalized coordinates.

#### <a name="VIE9EEFEE58"></a>ViewportScreen : [IntRectangle](../heirloom.math/heirloom.math.intrectangle.md)


Gets the size of viewport in pixel coordinates.

#### <a name="GLO9D3F3F33"></a>GlobalTransform : [Matrix](../heirloom.math/heirloom.math.matrix.md)


Get or sets the global transform.

#### <a name="INV9F065FB7"></a>InverseGlobalTransform : [Matrix](../heirloom.math/heirloom.math.matrix.md)

<small>`Read Only`</small>

Gets the inverse of the current global transform.

#### <a name="BLEF02A3CD5"></a>Blending : [Blending](heirloom.drawing.blending.md)


Gets or sets the current blending mode.

#### <a name="COLD1229651"></a>Color : [Color](heirloom.drawing.color.md)


Gets or sets the current blending color.

### Methods

#### <a name="RESA7048869"></a>ResetState() : void


Reset current context state to defaults (default surface, full viewport, no transform, alpha and white).

#### <a name="PUSDD336044"></a>PushState() : void


Save the context state (push it on the state stack).

#### <a name="POP630D10FB"></a>PopState() : void


Restore the context state (pop from the state stack).

#### <a name="CLE34E7AECC"></a>Clear([Color](heirloom.drawing.color.md) color) : void

<small>`Abstract`</small>

Clears the current surface with the specified color.


#### <a name="DRAAF0AEF1"></a>DrawMesh([ImageSource](heirloom.drawing.imagesource.md) image, [Mesh](heirloom.drawing.mesh.md) mesh, in [Matrix](../heirloom.math/heirloom.math.matrix.md) transform) : void

<small>`Abstract`</small>

Draws a mesh with the given image to the current surface.

<small>**image**: <param name="image">Some image.</param>  
</small>
<small>**mesh**: <param name="mesh">Some mesh.</param>  
</small>
<small>**transform**: <param name="transform">Some transform.</param>  
</small>

#### <a name="GRAF86FCD83"></a>GrabPixels([IntRectangle](../heirloom.math/heirloom.math.intrectangle.md) region) : [Image](heirloom.drawing.image.md)

<small>`Abstract`</small>

Grab the pixels from a subregion of the current surface and return that image. (ie, a screenshot)

<small>**region**: <param name="region">A region within the currently set surface.</param>  
</small>

#### <a name="GRAF1483889"></a>GrabPixels() : [Image](heirloom.drawing.image.md)


Grab the pixels from the current surface and return that image. (ie, a screenshot)

#### <a name="GETBA906748"></a>GetDrawCounts() : [Graphics.DrawCounts](heirloom.drawing.graphics.drawcounts.md)

<small>`Abstract`, `Protected`</small>

#### <a name="REFB28DEE96"></a>RefreshScreen() : void


Present the drawing operations to the screen.

#### <a name="SWABF25FF09"></a>SwapBuffers() : void

<small>`Abstract`, `Protected`</small>

#### <a name="ENDE20271D1"></a>EndFrame() : void

<small>`Abstract`, `Protected`</small>

#### <a name="FLU2F0EB18F"></a>Flush() : void

<small>`Abstract`</small>

Force any pending drawing operations to complete.

#### <a name="DISD833FA7A"></a>Dispose(bool disposing) : void

<small>`Virtual`, `Protected`</small>


#### <a name="DIS4E62D250"></a>Dispose() : void


Dispose this graphics context, freeing any resources occupied by it.

#### <a name="SETB12B2875"></a>SetCameraTransform([Vector](../heirloom.math/heirloom.math.vector.md) center, float scale = 1) : void


Sets `Heirloom.Drawing.Graphics.GlobalTransform` to mimic a 2D camera.


#### <a name="DRAC2E366"></a>DrawImage([ImageSource](heirloom.drawing.imagesource.md) image, in [Vector](../heirloom.math/heirloom.math.vector.md) position) : void


Draws an image to the current surface.

<small>**image**: <param name="image">Some image.</param>  
</small>
<small>**position**: <param name="position">The position of the image.</param>  
</small>

#### <a name="DRAD633A69E"></a>DrawImage([ImageSource](heirloom.drawing.imagesource.md) image, in [Vector](../heirloom.math/heirloom.math.vector.md) position, float rotation) : void


Draws an image to the current surface.

<small>**image**: <param name="image">Some image.</param>  
</small>
<small>**position**: <param name="position">The position of the image.</param>  
</small>
<small>**rotation**: <param name="rotation">The rotation applied to the image.</param>  
</small>

#### <a name="DRA424A37D1"></a>DrawImage([ImageSource](heirloom.drawing.imagesource.md) image, in [Vector](../heirloom.math/heirloom.math.vector.md) position, float rotation, in [Vector](../heirloom.math/heirloom.math.vector.md) scale) : void


Draws an image to the current surface.

<small>**image**: <param name="image">Some image.</param>  
</small>
<small>**position**: <param name="position">The position of the image.</param>  
</small>
<small>**rotation**: <param name="rotation">The rotation applied to the image.</param>  
</small>
<small>**scale**: <param name="scale">The scale applied to the image.</param>  
</small>

#### <a name="DRA6852F87C"></a>DrawImage([ImageSource](heirloom.drawing.imagesource.md) image, in [Rectangle](../heirloom.math/heirloom.math.rectangle.md) rectangle) : void


Draws an image stretched to fill a rectangular region to the current surface ignoring the image origin offset.

<small>**image**: <param name="image">Some image.</param>  
</small>
<small>**rectangle**: <param name="rectangle">The bounds of the drawn image.</param>  
</small>

#### <a name="DRAEF5F7A71"></a>DrawImage([ImageSource](heirloom.drawing.imagesource.md) image, in [Matrix](../heirloom.math/heirloom.math.matrix.md) transform) : void


Draws an image to the current surface.

<small>**image**: <param name="image">Some image.</param>  
</small>
<small>**transform**: <param name="transform">Some transform.</param>  
</small>

#### <a name="DRAA4FF1762"></a>DrawSprite([Sprite](heirloom.drawing.sprite.md) sprite, int index, in [Matrix](../heirloom.math/heirloom.math.matrix.md) transform) : void


Draw a sprite to the current surface.

<small>**sprite**: <param name="sprite">Some sprite.</param>  
</small>
<small>**index**: <param name="index">Some valid frame number within the sprite.</param>  
</small>
<small>**transform**: <param name="transform">Some transform to draw the sprite.</param>  
</small>

#### <a name="DRAF3BF7918"></a>DrawLine(in [Vector](../heirloom.math/heirloom.math.vector.md) p0, in [Vector](../heirloom.math/heirloom.math.vector.md) p1, float width = 1) : void


Draws a line segment between two points to the current surface.

<small>**p0**: <param name="p0">The start point.</param>  
</small>
<small>**p1**: <param name="p1">The end point.</param>  
</small>
<small>**width**: <param name="width">The thickness of the line in pixels.</param>  
</small>

#### <a name="DRAC0DF3294"></a>DrawCurve(in [Vector](../heirloom.math/heirloom.math.vector.md) p0, in [Vector](../heirloom.math/heirloom.math.vector.md) p1, in [Vector](../heirloom.math/heirloom.math.vector.md) p2, float width = 1) : void


Draws a quadratic curve using three control points to the current surface.

<small>**p0**: <param name="p0">The first control point.</param>  
</small>
<small>**p1**: <param name="p1">The second control point.</param>  
</small>
<small>**p2**: <param name="p2">The third control point.</param>  
</small>
<small>**width**: <param name="width">The thickness of the line in pixels.</param>  
</small>

#### <a name="DRA80187F08"></a>DrawCurve(in [Vector](../heirloom.math/heirloom.math.vector.md) p0, in [Vector](../heirloom.math/heirloom.math.vector.md) p1, in [Vector](../heirloom.math/heirloom.math.vector.md) p2, in [Vector](../heirloom.math/heirloom.math.vector.md) p3, float width = 1) : void


Draws a cubic curve using four control points to the current surface.

<small>**p0**: <param name="p0">The first control point.</param>  
</small>
<small>**p1**: <param name="p1">The second control point.</param>  
</small>
<small>**p2**: <param name="p2">The third control point.</param>  
</small>
<small>**p3**: <param name="p3">The fourth control point.</param>  
</small>
<small>**width**: <param name="width">The thickness of the line in pixels.</param>  
</small>

#### <a name="DRA3B97A8B3"></a>DrawRect(in [Rectangle](../heirloom.math/heirloom.math.rectangle.md) rectangle) : void


Draws a rectangle to the current surface.

<small>**rectangle**: <param name="rectangle">The rectangular region of the rectangle.</param>  
</small>

#### <a name="DRA2705A75D"></a>DrawRectOutline(in [Rectangle](../heirloom.math/heirloom.math.rectangle.md) rectangle, float width = 1) : void


Draws the outline of a rectangel to the current surface.

<small>**rectangle**: <param name="rectangle">The rectangular region of the rectangle.</param>  
</small>
<small>**width**: <param name="width">Width of the outline in pixels.</param>  
</small>

#### <a name="DRA24DFB43"></a>DrawCross(in [Vector](../heirloom.math/heirloom.math.vector.md) center, float size = 2, float width = 1) : void


Draws a simple axis aligned 'cross' or 'plus' shape, useful for debugging positions.

<small>**center**: <param name="center">The position of the cross.</param>  
</small>
<small>**size**: <param name="size">Size in screen pixels (not world space).</param>  
</small>
<small>**width**: <param name="width">Width of the lines screen pixels (not world space).</param>  
</small>

#### <a name="DRA670793A6"></a>DrawCircle(in [Circle](../heirloom.math/heirloom.math.circle.md) circle) : void


Draws a circle to the current surface.

<small>**circle**: <param name="circle">The circle to draw.</param>  
</small>

#### <a name="DRA3D334749"></a>DrawCircle(in [Vector](../heirloom.math/heirloom.math.vector.md) position, float radius) : void


Draws a circle to the current surface.

<small>**position**: <param name="position">The center of the circle.</param>  
</small>
<small>**radius**: <param name="radius">The radius of the circle.</param>  
</small>

#### <a name="DRAAD650C20"></a>DrawCircleOutline(in [Circle](../heirloom.math/heirloom.math.circle.md) circle, float width = 1) : void


Draws the outline of a circle to the current surface.

<small>**circle**: <param name="circle">The circle to draw.</param>  
</small>
<small>**width**: <param name="width">Width of the outline in pixels.</param>  
</small>

#### <a name="DRAF45EDC8B"></a>DrawCircleOutline(in [Vector](../heirloom.math/heirloom.math.vector.md) position, float radius, float width = 1) : void


Draws the outline of a circle to the current surface.

<small>**position**: <param name="position">The centr of the circle.</param>  
</small>
<small>**radius**: <param name="radius">The radius of the circle.</param>  
</small>
<small>**width**: <param name="width">Width of the outline in pixels.</param>  
</small>

#### <a name="DRA1A8B9DE6"></a>DrawTriangle(in [Triangle](../heirloom.math/heirloom.math.triangle.md) triangle) : void


Draw a triangle to the current surface.

<small>**triangle**: <param name="triangle">The triangle to draw.</param>  
</small>

#### <a name="DRADBC38A80"></a>DrawTriangle(in [Vector](../heirloom.math/heirloom.math.vector.md) a, in [Vector](../heirloom.math/heirloom.math.vector.md) b, in [Vector](../heirloom.math/heirloom.math.vector.md) c) : void


Draw a triangle outline to the current surface.

<small>**a**: <param name="a">The first point.</param>  
</small>
<small>**b**: <param name="b">The second point.</param>  
</small>
<small>**c**: <param name="c">The third point.</param>  
</small>

#### <a name="DRA23E5C748"></a>DrawTriangleOutline(in [Triangle](../heirloom.math/heirloom.math.triangle.md) triangle, float width = 1) : void


Draw a triangle outline to the current surface.

<small>**triangle**: <param name="triangle">The triangle to draw.</param>  
</small>
<small>**width**: <param name="width">The thickness of the line in pixels.</param>  
</small>

#### <a name="DRA378F642E"></a>DrawTriangleOutline(in [Vector](../heirloom.math/heirloom.math.vector.md) a, in [Vector](../heirloom.math/heirloom.math.vector.md) b, in [Vector](../heirloom.math/heirloom.math.vector.md) c, float width = 1) : void


Draw a triangle outline to the current surface.

<small>**a**: <param name="a">The first point.</param>  
</small>
<small>**b**: <param name="b">The second point.</param>  
</small>
<small>**c**: <param name="c">The third point.</param>  
</small>
<small>**width**: <param name="width">The thickness of the line in pixels.</param>  
</small>

#### <a name="DRAE93A728"></a>DrawPolygon(in [Vector](../heirloom.math/heirloom.math.vector.md) position, int sides, float radius) : void


Draws a regular polygon to the current surface.

<small>**sides**: <param name="sides">The number of sides in the regular polygon.</param>  
</small>
<small>**radius**: <param name="radius">The radius of the regular polygon.</param>  
</small>

#### <a name="DRA3C696336"></a>DrawPolygonOutline(in [Vector](../heirloom.math/heirloom.math.vector.md) position, int sides, float radius, float width = 1) : void


Draws the outline of a regular polygon to the current surface.

<small>**sides**: <param name="sides">The number of sides in the regular polygon.</param>  
</small>
<small>**radius**: <param name="radius">The radius of the regular polygon.</param>  
</small>
<small>**width**: <param name="width">Width of the outline in pixels.</param>  
</small>

#### <a name="DRABD5A3BCD"></a>DrawPolygon([Polygon](../heirloom.math/heirloom.math.polygon.md) polygon) : void


Draws a simple polygon to the current surface.

<small>**polygon**: <param name="polygon">Some polygon.</param>  
</small>

#### <a name="DRA1B52F1E8"></a>DrawPolygon([Polygon](../heirloom.math/heirloom.math.polygon.md) polygon, in [Matrix](../heirloom.math/heirloom.math.matrix.md) transform) : void


Draws a simple polygon to the current surface.

<small>**polygon**: <param name="polygon">Some polygon.</param>  
</small>
<small>**transform**: <param name="transform">Some transform.</param>  
</small>

#### <a name="DRA2980A4EB"></a>DrawPolygon(IEnumerable\<Vector> polygon) : void


Draws a simple polygon to the current surface.

<small>**polygon**: <param name="polygon">Some polygon.</param>  
</small>

#### <a name="DRA8F19084E"></a>DrawPolygon(IEnumerable\<Vector> polygon, in [Matrix](../heirloom.math/heirloom.math.matrix.md) transform) : void


Draws a simple polygon to the current surface.

<small>**polygon**: <param name="polygon">Some polygon.</param>  
</small>
<small>**transform**: <param name="transform">Some transform.</param>  
</small>

#### <a name="DRA2ED9F46F"></a>DrawPolygonOutline([Polygon](../heirloom.math/heirloom.math.polygon.md) polygon, float width = 1) : void


Draws the outline of a simple polygon to the current surface.

<small>**polygon**: <param name="polygon">Some polygon.</param>  
</small>
<small>**width**: <param name="width">Width of the outline in pixels.</param>  
</small>

#### <a name="DRA813864CE"></a>DrawPolygonOutline([Polygon](../heirloom.math/heirloom.math.polygon.md) polygon, in [Matrix](../heirloom.math/heirloom.math.matrix.md) transform, float width = 1) : void


Draws the outline of a simple polygon to the current surface.

<small>**polygon**: <param name="polygon">Some polygon.</param>  
</small>
<small>**transform**: <param name="transform">Some transform.</param>  
</small>
<small>**width**: <param name="width">Width of the outline in pixels.</param>  
</small>

#### <a name="DRA1EDFEE5"></a>DrawPolygonOutline(IEnumerable\<Vector> polygon, float width = 1) : void


Draws the outline of a simple polygon to the current surface.

<small>**polygon**: <param name="polygon">Some polygon.</param>  
</small>
<small>**width**: <param name="width">Width of the outline in pixels.</param>  
</small>

#### <a name="DRAA0746100"></a>DrawPolygonOutline(IEnumerable\<Vector> polygon, in [Matrix](../heirloom.math/heirloom.math.matrix.md) transform, float width = 1) : void


Draws the outline of a simple polygon to the current surface.

<small>**polygon**: <param name="polygon">Some polygon.</param>  
</small>
<small>**transform**: <param name="transform">Some transform.</param>  
</small>
<small>**width**: <param name="width">Width of the outline in pixels.</param>  
</small>

#### <a name="DRA1A3115E4"></a>DrawText([StyledText](heirloom.drawing.styledtext.md) styledText, in [Vector](../heirloom.math/heirloom.math.vector.md) position, [Font](heirloom.drawing.font.md) font, int size, [TextAlign](heirloom.drawing.textalign.md) align = 0) : void


Draws rich text to the current surface.

<small>**styledText**: <param name="styledText">The rich text to draw.</param>  
</small>
<small>**position**: <param name="position">The anchor position to layout text around.</param>  
</small>
<small>**font**: <param name="font">The font to render with.</param>  
</small>
<small>**size**: <param name="size">The font size to render with.</param>  
</small>
<small>**align**: <param name="align">The text alignment.</param>  
</small>

#### <a name="DRAD92FC516"></a>DrawText([StyledText](heirloom.drawing.styledtext.md) styledText, in [Rectangle](../heirloom.math/heirloom.math.rectangle.md) bounds, [Font](heirloom.drawing.font.md) font, int size, [TextAlign](heirloom.drawing.textalign.md) align = 0) : void


Draws rich text to the current surface.

<small>**styledText**: <param name="styledText">The rich text to draw.</param>  
</small>
<small>**bounds**: <param name="bounds">The boundng region to layout text.</param>  
</small>
<small>**font**: <param name="font">The font to render with.</param>  
</small>
<small>**size**: <param name="size">The font size to render with.</param>  
</small>
<small>**align**: <param name="align">The text alignment.</param>  
</small>

#### <a name="DRA4E2326AE"></a>DrawText(string text, in [Vector](../heirloom.math/heirloom.math.vector.md) position, [Font](heirloom.drawing.font.md) font, int size, [DrawTextCallback](heirloom.drawing.drawtextcallback.md) callback) : void


Draws text to the current surface.

<small>**text**: <param name="text">The text to draw.</param>  
</small>
<small>**position**: <param name="position">The anchor position to layout text around.</param>  
</small>
<small>**font**: <param name="font">The font to render with.</param>  
</small>
<small>**size**: <param name="size">The font size to render with.</param>  
</small>
<small>**callback**: <param name="callback">A callback for manipulating the style of the rendered text.</param>  
</small>

#### <a name="DRAB39D5DE6"></a>DrawText(string text, in [Vector](../heirloom.math/heirloom.math.vector.md) position, [Font](heirloom.drawing.font.md) font, int size, [TextAlign](heirloom.drawing.textalign.md) align = 0, [DrawTextCallback](heirloom.drawing.drawtextcallback.md) callback =) : void


Draws text to the current surface.

<small>**text**: <param name="text">The text to draw.</param>  
</small>
<small>**position**: <param name="position">The anchor position to layout text around.</param>  
</small>
<small>**font**: <param name="font">The font to render with.</param>  
</small>
<small>**size**: <param name="size">The font size to render with.</param>  
</small>
<small>**align**: <param name="align">The text alignment.</param>  
</small>
<small>**callback**: <param name="callback">A callback for manipulating the style of the rendered text.</param>  
</small>

#### <a name="DRA862A32BC"></a>DrawText(string text, in [Rectangle](../heirloom.math/heirloom.math.rectangle.md) bounds, [Font](heirloom.drawing.font.md) font, int size, [DrawTextCallback](heirloom.drawing.drawtextcallback.md) callback) : void


Draws text to the current surface.

<small>**text**: <param name="text">The text to draw.</param>  
</small>
<small>**bounds**: <param name="bounds">The boundng region to layout text.</param>  
</small>
<small>**font**: <param name="font">The font to render with.</param>  
</small>
<small>**size**: <param name="size">The font size to render with.</param>  
</small>
<small>**callback**: <param name="callback">A callback for manipulating the style of the rendered text.</param>  
</small>

#### <a name="DRAAB0F18"></a>DrawText(string text, in [Rectangle](../heirloom.math/heirloom.math.rectangle.md) bounds, [Font](heirloom.drawing.font.md) font, int size, [TextAlign](heirloom.drawing.textalign.md) align = 0, [DrawTextCallback](heirloom.drawing.drawtextcallback.md) callback =) : void


Draws text to the current surface.

<small>**text**: <param name="text">The text to draw.</param>  
</small>
<small>**bounds**: <param name="bounds">The boundng region to layout text.</param>  
</small>
<small>**font**: <param name="font">The font to render with.</param>  
</small>
<small>**size**: <param name="size">The font size to render with.</param>  
</small>
<small>**align**: <param name="align">The text alignment.</param>  
</small>
<small>**callback**: <param name="callback">A callback for manipulating the style of the rendered text.</param>  
</small>

