# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  

## Graphics

> **Namespace**: [Heirloom][0]  

```cs
public abstract class Graphics
```

#### Properties

[CurrentFPS][1], [Performance][2], [IsDisposed][3], [IsInitialized][4], [DefaultSurface][5], [Surface][6], [Shader][7], [Viewport][8], [GlobalTransform][9], [InverseGlobalTransform][10], [Blending][11], [Color][12]

#### Methods

[ResetState][13], [PushState][14], [PopState][15], [Clear][16], [DrawMesh][17], [GrabPixels][18], [GetDrawCounts][19], [RefreshScreen][20], [SwapBuffers][21], [EndFrame][22], [Flush][23], [Commit][24], [Dispose][25], [SetCameraTransform][26], [DrawImage][27], [DrawSubImage][28], [DrawSprite][29], [DrawNineSlice][30], [Apply][31], [Blit][32], [DrawLine][33], [DrawCurve][34], [DrawRect][35], [DrawRectOutline][36], [DrawCross][37], [DrawCircle][38], [DrawCircleOutline][39], [DrawTriangle][40], [DrawTriangleOutline][41], [DrawPolygon][42], [DrawPolygonOutline][43], [DrawText][44]

## Properties

| Name                         | Summary                                                                   |
|------------------------------|---------------------------------------------------------------------------|
| [CurrentFPS][1]              | Gets how often the default surface is presented to the screen per second. |
| [Performance][2]             | Gets drawing performance information.                                     |
| [IsDisposed][3]              | Gets a value determining if this [Graphics][45] was disposed.             |
| [IsInitialized][4]           | Gets a value determining if this [Graphics][45] has been initialized.     |
| [DefaultSurface][5]          | Gets the default surface (ie, window) of this render context.             |
| [Surface][6]                 | Gets or sets the current surface.                                         |
| [Shader][7]                  | Gets or sets the active shader.                                           |
| [Viewport][8]                | Gets or sets the viewport in pixel coordinates.                           |
| [GlobalTransform][9]         | Get or sets the global transform.                                         |
| [InverseGlobalTransform][10] | Gets the inverse of the current global transform.                         |
| [Blending][11]               | Gets or sets the current blending mode.                                   |
| [Color][12]                  | Gets or sets the current blending color.                                  |

## Methods

| Name                      | Summary                                                                                                                        |
|---------------------------|--------------------------------------------------------------------------------------------------------------------------------|
| [ResetState][13]          | Reset current context state to defaults (default surface, full viewport, no transform, alpha and white).                       |
| [PushState][14]           | Save the context state (push it on the state stack).                                                                           |
| [PopState][15]            | Restore the context state (pop from the state stack).                                                                          |
| [Clear][16]               | Clears the current surface with the specified color.                                                                           |
| [DrawMesh][17]            | Draws a mesh with the given image to the current surface.                                                                      |
| [GrabPixels][18]          | Grab the pixels from a subregion of the current surface and return that image. (ie, a screenshot)                              |
| [GrabPixels][18]          | Grab the pixels from the current surface and return that image. (ie, a screenshot)                                             |
| [GetDrawCounts][19]       | Populates and returns drawing metrics.                                                                                         |
| [RefreshScreen][20]       | Present the drawing operations to the screen.                                                                                  |
| [SwapBuffers][21]         | Causes the back and front buffers to be swapped.                                                                               |
| [EndFrame][22]            | Called at the end of frame to do any last minute work (resetting metrics, buffers, etc).                                       |
| [Flush][23]               | Submit all pending drawing operations, optionally blocking for completion.                                                     |
| [Commit][24]              | Commits pending drawing operations, blocking until all operations complete.                                                    |
| [Dispose][25]             | Dispose and cleanup resources.                                                                                                 |
| [Dispose][25]             | Dispose this graphics context, freeing any resources occupied by it.                                                           |
| [SetCameraTransform][26]  | Sets [GlobalTransform][9] to mimic a 2D camera. The center of the camera is set to `center` .                                  |
| [DrawImage][27]           | Draws an image to the current surface.                                                                                         |
| [DrawImage][27]           | Draws an image to the current surface.                                                                                         |
| [DrawImage][27]           | Draws an image to the current surface.                                                                                         |
| [DrawImage][27]           | Draws an image stretched to fill a rectangular region to the current surface ignoring the image origin offset.                 |
| [DrawImage][27]           | Draws an image to the current surface.                                                                                         |
| [DrawSubImage][28]        | Draws a sub-region of an image stretched to fill a rectangular region to the current surface ignoring the image origin offset. |
| [DrawSubImage][28]        | Draws a sub-region of an image to the current surface ignoring the image origin offset.                                        |
| [DrawSubImage][28]        | Draws a sub-region of an image to the current surface ignoring the image origin offset.                                        |
| [DrawSprite][29]          | Draw a sprite to the current surface.                                                                                          |
| [DrawNineSlice][30]       |                                                                                                                                |
| [Apply][31]               | Applies the specified surface effect to the current surface.                                                                   |
| [Blit][32]                | Overwrites an image to target surface.                                                                                         |
| [DrawLine][33]            | Draws a line segment between two points to the current surface.                                                                |
| [DrawCurve][34]           | Draws a quadratic curve using three control points to the current surface.                                                     |
| [DrawCurve][34]           | Draws a cubic curve using four control points to the current surface.                                                          |
| [DrawRect][35]            | Draws a rectangle to the current surface.                                                                                      |
| [DrawRectOutline][36]     | Draws the outline of a rectangel to the current surface.                                                                       |
| [DrawCross][37]           | Draws a simple axis aligned 'cross' or 'plus' shape, useful for debugging positions.                                           |
| [DrawCircle][38]          | Draws a circle to the current surface.                                                                                         |
| [DrawCircle][38]          | Draws a circle to the current surface.                                                                                         |
| [DrawCircleOutline][39]   | Draws the outline of a circle to the current surface.                                                                          |
| [DrawCircleOutline][39]   | Draws the outline of a circle to the current surface.                                                                          |
| [DrawTriangle][40]        | Draw a triangle to the current surface.                                                                                        |
| [DrawTriangle][40]        | Draw a triangle outline to the current surface.                                                                                |
| [DrawTriangleOutline][41] | Draw a triangle outline to the current surface.                                                                                |
| [DrawTriangleOutline][41] | Draw a triangle outline to the current surface.                                                                                |
| [DrawPolygon][42]         | Draws a regular polygon to the current surface.                                                                                |
| [DrawPolygonOutline][43]  | Draws the outline of a regular polygon to the current surface.                                                                 |
| [DrawPolygon][42]         | Draws a simple polygon to the current surface.                                                                                 |
| [DrawPolygon][42]         | Draws a simple polygon to the current surface.                                                                                 |
| [DrawPolygon][42]         | Draws a simple polygon to the current surface.                                                                                 |
| [DrawPolygon][42]         | Draws a simple polygon to the current surface.                                                                                 |
| [DrawPolygonOutline][43]  | Draws the outline of a simple polygon to the current surface.                                                                  |
| [DrawPolygonOutline][43]  | Draws the outline of a simple polygon to the current surface.                                                                  |
| [DrawPolygonOutline][43]  | Draws the outline of a simple polygon to the current surface.                                                                  |
| [DrawPolygonOutline][43]  | Draws the outline of a simple polygon to the current surface.                                                                  |
| [DrawText][44]            | Draws rich text to the current surface.                                                                                        |
| [DrawText][44]            | Draws rich text to the current surface.                                                                                        |
| [DrawText][44]            | Draws text to the current surface.                                                                                             |
| [DrawText][44]            | Draws text to the current surface.                                                                                             |
| [DrawText][44]            | Draws text to the current surface.                                                                                             |
| [DrawText][44]            | Draws text to the current surface.                                                                                             |

[0]: ../../Heirloom.Core.md
[1]: Graphics/CurrentFPS.md
[2]: Graphics/Performance.md
[3]: Graphics/IsDisposed.md
[4]: Graphics/IsInitialized.md
[5]: Graphics/DefaultSurface.md
[6]: Graphics/Surface.md
[7]: Graphics/Shader.md
[8]: Graphics/Viewport.md
[9]: Graphics/GlobalTransform.md
[10]: Graphics/InverseGlobalTransform.md
[11]: Graphics/Blending.md
[12]: Graphics/Color.md
[13]: Graphics/ResetState.md
[14]: Graphics/PushState.md
[15]: Graphics/PopState.md
[16]: Graphics/Clear.md
[17]: Graphics/DrawMesh.md
[18]: Graphics/GrabPixels.md
[19]: Graphics/GetDrawCounts.md
[20]: Graphics/RefreshScreen.md
[21]: Graphics/SwapBuffers.md
[22]: Graphics/EndFrame.md
[23]: Graphics/Flush.md
[24]: Graphics/Commit.md
[25]: Graphics/Dispose.md
[26]: Graphics/SetCameraTransform.md
[27]: Graphics/DrawImage.md
[28]: Graphics/DrawSubImage.md
[29]: Graphics/DrawSprite.md
[30]: Graphics/DrawNineSlice.md
[31]: Graphics/Apply.md
[32]: Graphics/Blit.md
[33]: Graphics/DrawLine.md
[34]: Graphics/DrawCurve.md
[35]: Graphics/DrawRect.md
[36]: Graphics/DrawRectOutline.md
[37]: Graphics/DrawCross.md
[38]: Graphics/DrawCircle.md
[39]: Graphics/DrawCircleOutline.md
[40]: Graphics/DrawTriangle.md
[41]: Graphics/DrawTriangleOutline.md
[42]: Graphics/DrawPolygon.md
[43]: Graphics/DrawPolygonOutline.md
[44]: Graphics/DrawText.md
[45]: Graphics.md
