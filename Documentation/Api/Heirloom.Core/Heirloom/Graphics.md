# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Graphics (Class)

> **Namespace**: [Heirloom][0]

```cs
public abstract class Graphics
```

### Properties

[Blending][1], [Color][2], [CurrentFPS][3], [DefaultSurface][4], [GlobalTransform][5], [InverseGlobalTransform][6], [IsDisposed][7], [IsInitialized][8], [Performance][9], [Shader][10], [Surface][11], [Viewport][12]

### Methods

[Apply][13], [Blit][14], [Clear][15], [Commit][16], [Dispose][17], [DrawCircle][18], [DrawCircleOutline][19], [DrawCross][20], [DrawCurve][21], [DrawImage][22], [DrawLine][23], [DrawMesh][24], [DrawNineSlice][25], [DrawPolygon][26], [DrawPolygonOutline][27], [DrawRect][28], [DrawRectOutline][29], [DrawSprite][30], [DrawSubImage][31], [DrawText][32], [DrawTriangle][33], [DrawTriangleOutline][34], [EndFrame][35], [Flush][36], [GetDrawCounts][37], [GrabPixels][38], [PopState][39], [PushState][40], [RefreshScreen][41], [ResetState][42], [SetCameraTransform][43], [SwapBuffers][44]

## Properties

#### Instance

| Name                        | Type                     | Summary                                                                |
|-----------------------------|--------------------------|------------------------------------------------------------------------|
| [Blending][1]               | [Blending][45]           | Gets or sets the current blending mode.                                |
| [Color][2]                  | [Color][46]              | Gets or sets the current blending color.                               |
| [CurrentFPS][3]             | `float`                  | Gets how often the default surface is presented to the screen per s... |
| [DefaultSurface][4]         | [Surface][47]            | Gets the default surface (ie, window) of this render context.          |
| [GlobalTransform][5]        | [Matrix][48]             | Get or sets the global transform.                                      |
| [InverseGlobalTransform][6] | [Matrix][48]             | Gets the inverse of the current global transform.                      |
| [IsDisposed][7]             | `bool`                   | Gets a value determining if this Graphics was disposed.                |
| [IsInitialized][8]          | `bool`                   | Gets a value determining if this Graphics has been initialized.        |
| [Performance][9]            | [DrawingPerformance][49] | Gets drawing performance information.                                  |
| [Shader][10]                | [Shader][50]             | Gets or sets the active shader.                                        |
| [Surface][11]               | [Surface][47]            | Gets or sets the current surface.                                      |
| [Viewport][12]              | [IntRectangle][51]       | Gets or sets the viewport in pixel coordinates.                        |

## Methods

#### Instance

| Name                            | Return Type               | Summary                                                                |
|---------------------------------|---------------------------|------------------------------------------------------------------------|
| [Apply(SurfaceEffect)][13]      | `void`                    | Applies the specified surface effect to the current surface.           |
| [Blit(ImageSource, Surf...][14] | `void`                    | Overwrites an image to target surface.                                 |
| [Clear(Color)][15]              | `void`                    | Clears the current surface with the specified color.                   |
| [Commit()][16]                  | `void`                    | Commits pending drawing operations, blocking until all operations c... |
| [Dispose(bool)][17]             | `void`                    | Dispose and cleanup resources.                                         |
| [Dispose()][17]                 | `void`                    | Dispose this graphics context, freeing any resources occupied by it.   |
| [DrawCircle(in Circle)][18]     | `void`                    | Draws a circle to the current surface.                                 |
| [DrawCircle(in Vector, ...][18] | `void`                    | Draws a circle to the current surface.                                 |
| [DrawCircleOutline(in C...][19] | `void`                    | Draws the outline of a circle to the current surface.                  |
| [DrawCircleOutline(in V...][19] | `void`                    | Draws the outline of a circle to the current surface.                  |
| [DrawCross(in Vector, f...][20] | `void`                    | Draws a simple axis aligned 'cross' or 'plus' shape, useful for deb... |
| [DrawCurve(in Vector, i...][21] | `void`                    | Draws a quadratic curve using three control points to the current s... |
| [DrawCurve(in Vector, i...][21] | `void`                    | Draws a cubic curve using four control points to the current surface.  |
| [DrawImage(ImageSource,...][22] | `void`                    | Draws an image to the current surface.                                 |
| [DrawImage(ImageSource,...][22] | `void`                    | Draws an image to the current surface.                                 |
| [DrawImage(ImageSource,...][22] | `void`                    | Draws an image to the current surface.                                 |
| [DrawImage(ImageSource,...][22] | `void`                    | Draws an image stretched to fill a rectangular region to the curren... |
| [DrawImage(ImageSource,...][22] | `void`                    | Draws an image to the current surface.                                 |
| [DrawLine(in Vector, in...][23] | `void`                    | Draws a line segment between two points to the current surface.        |
| [DrawMesh(ImageSource, ...][24] | `void`                    | Draws a mesh with the given image to the current surface.              |
| [DrawNineSlice(NineSlic...][25] | `void`                    |                                                                        |
| [DrawPolygon(in Vector,...][26] | `void`                    | Draws a regular polygon to the current surface.                        |
| [DrawPolygon(Polygon)][26]      | `void`                    | Draws a simple polygon to the current surface.                         |
| [DrawPolygon(Polygon, i...][26] | `void`                    | Draws a simple polygon to the current surface.                         |
| [DrawPolygon(IEnumerabl...][26] | `void`                    | Draws a simple polygon to the current surface.                         |
| [DrawPolygon(IEnumerabl...][26] | `void`                    | Draws a simple polygon to the current surface.                         |
| [DrawPolygonOutline(in ...][27] | `void`                    | Draws the outline of a regular polygon to the current surface.         |
| [DrawPolygonOutline(Pol...][27] | `void`                    | Draws the outline of a simple polygon to the current surface.          |
| [DrawPolygonOutline(Pol...][27] | `void`                    | Draws the outline of a simple polygon to the current surface.          |
| [DrawPolygonOutline(IEn...][27] | `void`                    | Draws the outline of a simple polygon to the current surface.          |
| [DrawPolygonOutline(IEn...][27] | `void`                    | Draws the outline of a simple polygon to the current surface.          |
| [DrawRect(in Rectangle)][28]    | `void`                    | Draws a rectangle to the current surface.                              |
| [DrawRectOutline(in Rec...][29] | `void`                    | Draws the outline of a rectangel to the current surface.               |
| [DrawSprite(Sprite, int...][30] | `void`                    | Draw a sprite to the current surface.                                  |
| [DrawSubImage(ImageSour...][31] | `void`                    | Draws a sub-region of an image stretched to fill a rectangular regi... |
| [DrawSubImage(ImageSour...][31] | `void`                    | Draws a sub-region of an image to the current surface ignoring the ... |
| [DrawSubImage(ImageSour...][31] | `void`                    | Draws a sub-region of an image to the current surface ignoring the ... |
| [DrawText(StyledText, i...][32] | `void`                    | Draws rich text to the current surface.                                |
| [DrawText(StyledText, i...][32] | `void`                    | Draws rich text to the current surface.                                |
| [DrawText(string, in Ve...][32] | `void`                    | Draws text to the current surface.                                     |
| [DrawText(string, in Ve...][32] | `void`                    | Draws text to the current surface.                                     |
| [DrawText(string, in Re...][32] | `void`                    | Draws text to the current surface.                                     |
| [DrawText(string, in Re...][32] | `void`                    | Draws text to the current surface.                                     |
| [DrawTriangle(in Triangle)][33] | `void`                    | Draw a triangle to the current surface.                                |
| [DrawTriangle(in Vector...][33] | `void`                    | Draw a triangle outline to the current surface.                        |
| [DrawTriangleOutline(in...][34] | `void`                    | Draw a triangle outline to the current surface.                        |
| [DrawTriangleOutline(in...][34] | `void`                    | Draw a triangle outline to the current surface.                        |
| [EndFrame()][35]                | `void`                    | Called at the end of frame to do any last minute work (resetting me... |
| [Flush(bool)][36]               | `void`                    | Submit all pending drawing operations, optionally blocking for comp... |
| [GetDrawCounts()][37]           | [Graphics.DrawCounts][52] | Populates and returns drawing metrics.                                 |
| [GrabPixels(IntRectangle)][38]  | [Image][53]               | Grab the pixels from a subregion of the current surface and return ... |
| [GrabPixels()][38]              | [Image][53]               | Grab the pixels from the current surface and return that image. (ie... |
| [PopState()][39]                | `void`                    | Restore the context state (pop from the state stack).                  |
| [PushState(bool)][40]           | `void`                    | Save the context state (push it on the state stack).                   |
| [RefreshScreen()][41]           | `void`                    | Present the drawing operations to the screen.                          |
| [ResetState()][42]              | `void`                    | Reset current context state to defaults (default surface, full view... |
| [SetCameraTransform(Vec...][43] | `void`                    | Sets GlobalTransform to mimic a 2D camera. The center of the camera... |
| [SwapBuffers()][44]             | `void`                    | Causes the back and front buffers to be swapped.                       |

[0]: ../../Heirloom.Core.md
[1]: Graphics/Blending.md
[2]: Graphics/Color.md
[3]: Graphics/CurrentFPS.md
[4]: Graphics/DefaultSurface.md
[5]: Graphics/GlobalTransform.md
[6]: Graphics/InverseGlobalTransform.md
[7]: Graphics/IsDisposed.md
[8]: Graphics/IsInitialized.md
[9]: Graphics/Performance.md
[10]: Graphics/Shader.md
[11]: Graphics/Surface.md
[12]: Graphics/Viewport.md
[13]: Graphics/Apply.md
[14]: Graphics/Blit.md
[15]: Graphics/Clear.md
[16]: Graphics/Commit.md
[17]: Graphics/Dispose.md
[18]: Graphics/DrawCircle.md
[19]: Graphics/DrawCircleOutline.md
[20]: Graphics/DrawCross.md
[21]: Graphics/DrawCurve.md
[22]: Graphics/DrawImage.md
[23]: Graphics/DrawLine.md
[24]: Graphics/DrawMesh.md
[25]: Graphics/DrawNineSlice.md
[26]: Graphics/DrawPolygon.md
[27]: Graphics/DrawPolygonOutline.md
[28]: Graphics/DrawRect.md
[29]: Graphics/DrawRectOutline.md
[30]: Graphics/DrawSprite.md
[31]: Graphics/DrawSubImage.md
[32]: Graphics/DrawText.md
[33]: Graphics/DrawTriangle.md
[34]: Graphics/DrawTriangleOutline.md
[35]: Graphics/EndFrame.md
[36]: Graphics/Flush.md
[37]: Graphics/GetDrawCounts.md
[38]: Graphics/GrabPixels.md
[39]: Graphics/PopState.md
[40]: Graphics/PushState.md
[41]: Graphics/RefreshScreen.md
[42]: Graphics/ResetState.md
[43]: Graphics/SetCameraTransform.md
[44]: Graphics/SwapBuffers.md
[45]: Blending.md
[46]: Color.md
[47]: Surface.md
[48]: Matrix.md
[49]: DrawingPerformance.md
[50]: Shader.md
[51]: IntRectangle.md
[52]: Graphics.DrawCounts.md
[53]: Image.md
