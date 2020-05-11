# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## GraphicsContext (Class)

> **Namespace**: [Heirloom][0]

```cs
public abstract class GraphicsContext
```

### Properties

[Blending][1], [Color][2], [CurrentFPS][3], [GlobalTransform][4], [InverseGlobalTransform][5], [IsDisposed][6], [IsInitialized][7], [Performance][8], [Screen][9], [Shader][10], [Surface][11], [Viewport][12]

### Methods

[Apply][13], [Blit][14], [Clear][15], [Commit][16], [ComputePerFrameStats][17], [Dispose][18], [DrawCircle][19], [DrawCircleOutline][20], [DrawCross][21], [DrawCurve][22], [DrawImage][23], [DrawLine][24], [DrawMesh][25], [DrawNineSlice][26], [DrawPolygon][27], [DrawPolygonOutline][28], [DrawRect][29], [DrawRectOutline][30], [DrawSubImage][31], [DrawText][32], [DrawTriangle][33], [DrawTriangleOutline][34], [Flush][35], [GetDrawCounts][36], [GrabPixels][37], [PopState][38], [PushState][39], [ResetState][40], [SetCameraTransform][41], [SwapBuffers][42]

## Properties

#### Instance

| Name                        | Type                                     | Summary                                                                |
|-----------------------------|------------------------------------------|------------------------------------------------------------------------|
| [Blending][1]               | [Blending][43]                           | Gets or sets the current blending mode.                                |
| [Color][2]                  | [Color][44]                              | Gets or sets the current blending color.                               |
| [CurrentFPS][3]             | `float`                                  | Gets how often the default surface is presented to the screen per s... |
| [GlobalTransform][4]        | [Matrix][45]                             | Get or sets the global transform.                                      |
| [InverseGlobalTransform][5] | [Matrix][45]                             | Gets the inverse of the current global transform.                      |
| [IsDisposed][6]             | `bool`                                   | Gets a value determining if this GraphicsContext was disposed.         |
| [IsInitialized][7]          | `bool`                                   | Gets a value determining if this GraphicsContext has been initialized. |
| [Performance][8]            | [GraphicsContext.PerformanceMetrics][46] | Gets drawing performance information.                                  |
| [Screen][9]                 | [Screen][47]                             | Gets the screen this graphics context is responsible for.              |
| [Shader][10]                | [Shader][48]                             | Gets or sets the active shader.                                        |
| [Surface][11]               | [Surface][49]                            | Gets or sets the current surface.                                      |
| [Viewport][12]              | [IntRectangle][50]                       | Gets or sets the viewport in pixel coordinates.                        |

## Methods

#### Instance

| Name                            | Return Type                      | Summary                                                                |
|---------------------------------|----------------------------------|------------------------------------------------------------------------|
| [Apply(SurfaceEffect)][13]      | `void`                           | Applies the specified surface effect to the current surface.           |
| [Blit(ImageSource, Surf...][14] | `void`                           | Overwrites an image to target surface.                                 |
| [Clear(Color)][15]              | `void`                           | Clears the current surface with the specified color.                   |
| [Commit()][16]                  | `void`                           | Commits pending drawing operations, blocking until all operations c... |
| [ComputePerFrameStats()][17]    | `void`                           | Computes end of frame statistics.                                      |
| [Dispose(bool)][18]             | `void`                           | Dispose and cleanup resources.                                         |
| [Dispose()][18]                 | `void`                           | Dispose this graphics context, freeing any resources occupied by it.   |
| [DrawCircle(in Circle)][19]     | `void`                           | Draws a circle to the current surface.                                 |
| [DrawCircle(in Vector, ...][19] | `void`                           | Draws a circle to the current surface.                                 |
| [DrawCircleOutline(in C...][20] | `void`                           | Draws the outline of a circle to the current surface.                  |
| [DrawCircleOutline(in V...][20] | `void`                           | Draws the outline of a circle to the current surface.                  |
| [DrawCross(in Vector, f...][21] | `void`                           | Draws a simple axis aligned 'cross' or 'plus' shape, useful for deb... |
| [DrawCurve(Vector, Vect...][22] | `void`                           | Draws a quadratic curve using three control points to the current s... |
| [DrawCurve(Vector, Vect...][22] | `void`                           | Draws a cubic curve using four control points to the current surface.  |
| [DrawCurve(Curve, float)][22]   | `void`                           |                                                                        |
| [DrawImage(ImageSource,...][23] | `void`                           | Draws an image to the current surface.                                 |
| [DrawImage(ImageSource,...][23] | `void`                           | Draws an image to the current surface.                                 |
| [DrawImage(ImageSource,...][23] | `void`                           | Draws an image to the current surface.                                 |
| [DrawImage(ImageSource,...][23] | `void`                           | Draws an image stretched to fill a rectangular region to the curren... |
| [DrawImage(ImageSource,...][23] | `void`                           | Draws an image to the current surface.                                 |
| [DrawLine(in Vector, in...][24] | `void`                           | Draws a line segment between two points to the current surface.        |
| [DrawMesh(ImageSource, ...][25] | `void`                           | Draws a mesh with the given image to the current surface.              |
| [DrawNineSlice(NineSlic...][26] | `void`                           |                                                                        |
| [DrawPolygon(in Vector,...][27] | `void`                           | Draws a regular polygon to the current surface.                        |
| [DrawPolygon(Polygon)][27]      | `void`                           | Draws a simple polygon to the current surface.                         |
| [DrawPolygon(Polygon, i...][27] | `void`                           | Draws a simple polygon to the current surface.                         |
| [DrawPolygon(IEnumerabl...][27] | `void`                           | Draws a simple polygon to the current surface.                         |
| [DrawPolygon(IEnumerabl...][27] | `void`                           | Draws a simple polygon to the current surface.                         |
| [DrawPolygonOutline(in ...][28] | `void`                           | Draws the outline of a regular polygon to the current surface.         |
| [DrawPolygonOutline(Pol...][28] | `void`                           | Draws the outline of a simple polygon to the current surface.          |
| [DrawPolygonOutline(Pol...][28] | `void`                           | Draws the outline of a simple polygon to the current surface.          |
| [DrawPolygonOutline(IEn...][28] | `void`                           | Draws the outline of a simple polygon to the current surface.          |
| [DrawPolygonOutline(IEn...][28] | `void`                           | Draws the outline of a simple polygon to the current surface.          |
| [DrawRect(in Rectangle)][29]    | `void`                           | Draws a rectangle to the current surface.                              |
| [DrawRectOutline(in Rec...][30] | `void`                           | Draws the outline of a rectangel to the current surface.               |
| [DrawSubImage(ImageSour...][31] | `void`                           | Draws a sub-region of an image stretched to fill a rectangular regi... |
| [DrawSubImage(ImageSour...][31] | `void`                           | Draws a sub-region of an image to the current surface ignoring the ... |
| [DrawSubImage(ImageSour...][31] | `void`                           | Draws a sub-region of an image to the current surface ignoring the ... |
| [DrawText(StyledText, i...][32] | `void`                           | Draws rich text to the current surface.                                |
| [DrawText(StyledText, i...][32] | `void`                           | Draws rich text to the current surface.                                |
| [DrawText(string, in Ve...][32] | `void`                           | Draws text to the current surface.                                     |
| [DrawText(string, in Ve...][32] | `void`                           | Draws text to the current surface.                                     |
| [DrawText(string, in Re...][32] | `void`                           | Draws text to the current surface.                                     |
| [DrawText(string, in Re...][32] | `void`                           | Draws text to the current surface.                                     |
| [DrawTriangle(in Triangle)][33] | `void`                           | Draw a triangle to the current surface.                                |
| [DrawTriangle(in Vector...][33] | `void`                           | Draw a triangle outline to the current surface.                        |
| [DrawTriangleOutline(in...][34] | `void`                           | Draw a triangle outline to the current surface.                        |
| [DrawTriangleOutline(in...][34] | `void`                           | Draw a triangle outline to the current surface.                        |
| [Flush(bool)][35]               | `void`                           | Submit all pending drawing operations, optionally blocking for comp... |
| [GetDrawCounts()][36]           | [GraphicsContext.DrawCounts][51] | Populates and returns drawing metrics.                                 |
| [GrabPixels(IntRectangle)][37]  | [Image][52]                      | Grab the pixels from a subregion of the current surface and return ... |
| [GrabPixels()][37]              | [Image][52]                      | Grab the pixels from the current surface and return that image. (ie... |
| [PopState()][38]                | `void`                           | Restore the context state (pop from the state stack).                  |
| [PushState(bool)][39]           | `void`                           | Save the context state (push it on the state stack).                   |
| [ResetState()][40]              | `void`                           | Reset current context state to defaults (default surface, full view... |
| [SetCameraTransform(Vec...][41] | `void`                           | Sets GlobalTransform to mimic a 2D camera. The center of the camera... |
| [SwapBuffers()][42]             | `void`                           | Causes the back and front buffers to be swapped.                       |

[0]: ../../Heirloom.Core.md
[1]: GraphicsContext/Blending.md
[2]: GraphicsContext/Color.md
[3]: GraphicsContext/CurrentFPS.md
[4]: GraphicsContext/GlobalTransform.md
[5]: GraphicsContext/InverseGlobalTransform.md
[6]: GraphicsContext/IsDisposed.md
[7]: GraphicsContext/IsInitialized.md
[8]: GraphicsContext/Performance.md
[9]: GraphicsContext/Screen.md
[10]: GraphicsContext/Shader.md
[11]: GraphicsContext/Surface.md
[12]: GraphicsContext/Viewport.md
[13]: GraphicsContext/Apply.md
[14]: GraphicsContext/Blit.md
[15]: GraphicsContext/Clear.md
[16]: GraphicsContext/Commit.md
[17]: GraphicsContext/ComputePerFrameStats.md
[18]: GraphicsContext/Dispose.md
[19]: GraphicsContext/DrawCircle.md
[20]: GraphicsContext/DrawCircleOutline.md
[21]: GraphicsContext/DrawCross.md
[22]: GraphicsContext/DrawCurve.md
[23]: GraphicsContext/DrawImage.md
[24]: GraphicsContext/DrawLine.md
[25]: GraphicsContext/DrawMesh.md
[26]: GraphicsContext/DrawNineSlice.md
[27]: GraphicsContext/DrawPolygon.md
[28]: GraphicsContext/DrawPolygonOutline.md
[29]: GraphicsContext/DrawRect.md
[30]: GraphicsContext/DrawRectOutline.md
[31]: GraphicsContext/DrawSubImage.md
[32]: GraphicsContext/DrawText.md
[33]: GraphicsContext/DrawTriangle.md
[34]: GraphicsContext/DrawTriangleOutline.md
[35]: GraphicsContext/Flush.md
[36]: GraphicsContext/GetDrawCounts.md
[37]: GraphicsContext/GrabPixels.md
[38]: GraphicsContext/PopState.md
[39]: GraphicsContext/PushState.md
[40]: GraphicsContext/ResetState.md
[41]: GraphicsContext/SetCameraTransform.md
[42]: GraphicsContext/SwapBuffers.md
[43]: Blending.md
[44]: Color.md
[45]: Matrix.md
[46]: GraphicsContext.PerformanceMetrics.md
[47]: Screen.md
[48]: Shader.md
[49]: Surface.md
[50]: IntRectangle.md
[51]: GraphicsContext.DrawCounts.md
[52]: Image.md