# Window

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Platforms.Desktop][0]  
> **Dependencies**: [Heirloom.Core][1], [Heirloom.OpenGLES][2], [Heirloom.MiniAudio][3]  
> **Namespace**: [Heirloom.Desktop][0]  

```cs
public class Window
```

--------------------------------------------------------------------------------

**Properties**: [IsDisposed][4], [IsClosed][5], [HasTransparentFramebuffer][6], [Graphics][7], [IsVisible][8], [IsDecorated][9], [IsResizable][10], [IsFloating][11], [Title][12], [Bounds][13], [Position][14], [Size][15], [FramebufferSize][16], [ContentScale][17], [State][18], [Monitor][19], [Icons][20]

**Methods**: [Dispose][21], [OnWindowResized][22], [OnFramebufferResized][23], [OnContentScaleChanged][24], [OnWindowMoved][25], [OnKeyPressed][26], [OnCharTyped][27], [OnMousePressed][28], [OnMouseMove][29], [OnMouseScroll][30], [OnClosing][31], [OnClosed][32], [Show][33], [Hide][34], [Close][35], [Focus][36], [Maximize][37], [Minimize][38], [Restore][39], [BeginFullscreen][40], [EndFullscreen][41], [MoveToCenter][42], [SetIcons][43], [SetIcon][44], [SetCursor][45]

**Events**: [Resized][46], [FramebufferResized][47], [ContentScaleChanged][48], [KeyPress][49], [KeyRelease][50], [KeyRepeat][51], [CharacterTyped][52], [MousePress][53], [MouseRelease][54], [MouseScroll][55], [MouseMove][56], [Closing][57], [Closed][58]

--------------------------------------------------------------------------------

## Properties

| Name                           | Summary                                                                                          |
|--------------------------------|--------------------------------------------------------------------------------------------------|
| [IsDisposed][4]                | Gets a value that determines if this window been disposed.                                       |
| [IsClosed][5]                  | Gets a value that determines if this window been closed.                                         |
| [HasTransparentFramebuffer][6] | Gets a value that determines if this window supports a transparent framebuffer.                  |
| [Graphics][7]                  | Gets the graphics context associated with this window.                                           |
| [IsVisible][8]                 | Gets a value that determines if the window is visible.                                           |
| [IsDecorated][9]               | Gets a value that determines if the window is decorated.                                         |
| [IsResizable][10]              | Gets a value that determines if the window be resized.                                           |
| [IsFloating][11]               | Gets a value that determines if the window "always on top".                                      |
| [Title][12]                    | Gets or set the window title text.                                                               |
| [Bounds][13]                   | Gets or sets the window bounds in screen units.                                                  |
| [Position][14]                 | Gets or sets the window position in screen coordinates.                                          |
| [Size][15]                     | Gets or sets the window size in screen units.                                                    |
| [FramebufferSize][16]          | The size of the underlying framebuffer in pixels.                                                |
| [ContentScale][17]             | Gets the content scaling factor.                                                                 |
| [State][18]                    | Gets the current state of the window.                                                            |
| [Monitor][19]                  | Gets the monitor this window is positioned on by checking the center point of the window bounds. |
| [Icons][20]                    | Gets this windows icon set.                                                                      |

## Events

| Name                      | Summary |
|---------------------------|---------|
| [Resized][46]             |         |
| [FramebufferResized][47]  |         |
| [ContentScaleChanged][48] |         |
| [KeyPress][49]            |         |
| [KeyRelease][50]          |         |
| [KeyRepeat][51]           |         |
| [CharacterTyped][52]      |         |
| [MousePress][53]          |         |
| [MouseRelease][54]        |         |
| [MouseScroll][55]         |         |
| [MouseMove][56]           |         |
| [Closing][57]             |         |
| [Closed][58]              |         |

## Methods

| Name                        | Summary                                                                             |
|-----------------------------|-------------------------------------------------------------------------------------|
| [Dispose][21]               |                                                                                     |
| [OnWindowResized][22]       |                                                                                     |
| [OnFramebufferResized][23]  |                                                                                     |
| [OnContentScaleChanged][24] |                                                                                     |
| [OnWindowMoved][25]         |                                                                                     |
| [OnKeyPressed][26]          |                                                                                     |
| [OnCharTyped][27]           |                                                                                     |
| [OnMousePressed][28]        |                                                                                     |
| [OnMouseMove][29]           |                                                                                     |
| [OnMouseScroll][30]         |                                                                                     |
| [OnClosing][31]             |                                                                                     |
| [OnClosed][32]              |                                                                                     |
| [Show][33]                  |                                                                                     |
| [Hide][34]                  |                                                                                     |
| [Close][35]                 |                                                                                     |
| [Focus][36]                 |                                                                                     |
| [Maximize][37]              | Sets the window to a maximized state.                                               |
| [Minimize][38]              | Sets the window to a minimized state.                                               |
| [Restore][39]               | Sets the window to a default size state.                                            |
| [BeginFullscreen][40]       | Puts the window into fullscreen using the nearest monitor and existing video mode.  |
| [BeginFullscreen][40]       | Sets the window to fullscreen using the specified monitor and existing video mode.  |
| [BeginFullscreen][40]       | Puts the window into fullscreen using the nearest monitor and specified video mode. |
| [BeginFullscreen][40]       | Sets the window to fullscreen using the specified monitor and video mode.           |
| [EndFullscreen][41]         | Disables fullscreen mode.                                                           |
| [MoveToCenter][42]          | Moves the window to the center of the nearest monitor.                              |
| [MoveToCenter][42]          | Moves the window to the center of the specified monitor.                            |
| [SetIcons][43]              |                                                                                     |
| [SetIcon][44]               | Assigns a new icon image to the window.                                             |
| [SetCursor][45]             | Changes the appearance of the cursor on this window.                                |
| [SetCursor][45]             | Changes the appearance of the cursor on this window.                                |
| [SetCursor][45]             | Changes the appearance of the cursor on this window.                                |
| [Dispose][21]               |                                                                                     |

[0]: ../Heirloom.Platforms.Desktop.md
[1]: ../Heirloom.Core.md
[2]: ../Heirloom.OpenGLES.md
[3]: ../Heirloom.MiniAudio.md
[4]: Heirloom.Desktop.Window.IsDisposed.md
[5]: Heirloom.Desktop.Window.IsClosed.md
[6]: Heirloom.Desktop.Window.HasTransparentFramebuffer.md
[7]: Heirloom.Desktop.Window.Graphics.md
[8]: Heirloom.Desktop.Window.IsVisible.md
[9]: Heirloom.Desktop.Window.IsDecorated.md
[10]: Heirloom.Desktop.Window.IsResizable.md
[11]: Heirloom.Desktop.Window.IsFloating.md
[12]: Heirloom.Desktop.Window.Title.md
[13]: Heirloom.Desktop.Window.Bounds.md
[14]: Heirloom.Desktop.Window.Position.md
[15]: Heirloom.Desktop.Window.Size.md
[16]: Heirloom.Desktop.Window.FramebufferSize.md
[17]: Heirloom.Desktop.Window.ContentScale.md
[18]: Heirloom.Desktop.Window.State.md
[19]: Heirloom.Desktop.Window.Monitor.md
[20]: Heirloom.Desktop.Window.Icons.md
[21]: Heirloom.Desktop.Window.Dispose.md
[22]: Heirloom.Desktop.Window.OnWindowResized.md
[23]: Heirloom.Desktop.Window.OnFramebufferResized.md
[24]: Heirloom.Desktop.Window.OnContentScaleChanged.md
[25]: Heirloom.Desktop.Window.OnWindowMoved.md
[26]: Heirloom.Desktop.Window.OnKeyPressed.md
[27]: Heirloom.Desktop.Window.OnCharTyped.md
[28]: Heirloom.Desktop.Window.OnMousePressed.md
[29]: Heirloom.Desktop.Window.OnMouseMove.md
[30]: Heirloom.Desktop.Window.OnMouseScroll.md
[31]: Heirloom.Desktop.Window.OnClosing.md
[32]: Heirloom.Desktop.Window.OnClosed.md
[33]: Heirloom.Desktop.Window.Show.md
[34]: Heirloom.Desktop.Window.Hide.md
[35]: Heirloom.Desktop.Window.Close.md
[36]: Heirloom.Desktop.Window.Focus.md
[37]: Heirloom.Desktop.Window.Maximize.md
[38]: Heirloom.Desktop.Window.Minimize.md
[39]: Heirloom.Desktop.Window.Restore.md
[40]: Heirloom.Desktop.Window.BeginFullscreen.md
[41]: Heirloom.Desktop.Window.EndFullscreen.md
[42]: Heirloom.Desktop.Window.MoveToCenter.md
[43]: Heirloom.Desktop.Window.SetIcons.md
[44]: Heirloom.Desktop.Window.SetIcon.md
[45]: Heirloom.Desktop.Window.SetCursor.md
[46]: Heirloom.Desktop.Window.Resized.md
[47]: Heirloom.Desktop.Window.FramebufferResized.md
[48]: Heirloom.Desktop.Window.ContentScaleChanged.md
[49]: Heirloom.Desktop.Window.KeyPress.md
[50]: Heirloom.Desktop.Window.KeyRelease.md
[51]: Heirloom.Desktop.Window.KeyRepeat.md
[52]: Heirloom.Desktop.Window.CharacterTyped.md
[53]: Heirloom.Desktop.Window.MousePress.md
[54]: Heirloom.Desktop.Window.MouseRelease.md
[55]: Heirloom.Desktop.Window.MouseScroll.md
[56]: Heirloom.Desktop.Window.MouseMove.md
[57]: Heirloom.Desktop.Window.Closing.md
[58]: Heirloom.Desktop.Window.Closed.md
