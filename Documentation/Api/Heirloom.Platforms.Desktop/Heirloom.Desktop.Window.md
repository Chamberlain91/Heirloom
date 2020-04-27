# Heirloom.Platforms.Desktop

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Platforms.Desktop][0]  

## Window

> **Namespace**: [Heirloom.Desktop][0]  

```cs
public class Window
```

#### Properties

[IsDisposed][1], [IsClosed][2], [HasTransparentFramebuffer][3], [Graphics][4], [IsVisible][5], [IsDecorated][6], [IsResizable][7], [IsFloating][8], [Title][9], [Bounds][10], [Position][11], [Size][12], [FramebufferSize][13], [ContentScale][14], [State][15], [Monitor][16], [Icons][17]

#### Methods

[Dispose][18], [OnWindowResized][19], [OnFramebufferResized][20], [OnContentScaleChanged][21], [OnWindowMoved][22], [OnKeyPressed][23], [OnCharTyped][24], [OnMousePressed][25], [OnMouseMove][26], [OnMouseScroll][27], [OnClosing][28], [OnClosed][29], [Show][30], [Hide][31], [Close][32], [Focus][33], [Maximize][34], [Minimize][35], [Restore][36], [BeginFullscreen][37], [EndFullscreen][38], [MoveToCenter][39], [SetIcons][40], [SetIcon][41], [SetCursor][42]

#### Events

[Resized][43], [FramebufferResized][44], [ContentScaleChanged][45], [KeyPress][46], [KeyRelease][47], [KeyRepeat][48], [CharacterTyped][49], [MousePress][50], [MouseRelease][51], [MouseScroll][52], [MouseMove][53], [Closing][54], [Closed][55]

## Properties

| Name                           | Summary                                                                                          |
|--------------------------------|--------------------------------------------------------------------------------------------------|
| [IsDisposed][1]                | Gets a value that determines if this window been disposed.                                       |
| [IsClosed][2]                  | Gets a value that determines if this window been closed.                                         |
| [HasTransparentFramebuffer][3] | Gets a value that determines if this window supports a transparent framebuffer.                  |
| [Graphics][4]                  | Gets the graphics context associated with this window.                                           |
| [IsVisible][5]                 | Gets a value that determines if the window is visible.                                           |
| [IsDecorated][6]               | Gets a value that determines if the window is decorated.                                         |
| [IsResizable][7]               | Gets a value that determines if the window be resized.                                           |
| [IsFloating][8]                | Gets a value that determines if the window "always on top".                                      |
| [Title][9]                     | Gets or set the window title text.                                                               |
| [Bounds][10]                   | Gets or sets the window bounds in screen units.                                                  |
| [Position][11]                 | Gets or sets the window position in screen coordinates.                                          |
| [Size][12]                     | Gets or sets the window size in screen units.                                                    |
| [FramebufferSize][13]          | The size of the underlying framebuffer in pixels.                                                |
| [ContentScale][14]             | Gets the content scaling factor.                                                                 |
| [State][15]                    | Gets the current state of the window.                                                            |
| [Monitor][16]                  | Gets the monitor this window is positioned on by checking the center point of the window bounds. |
| [Icons][17]                    | Gets this windows icon set.                                                                      |

## Events

| Name                      | Summary |
|---------------------------|---------|
| [Resized][43]             |         |
| [FramebufferResized][44]  |         |
| [ContentScaleChanged][45] |         |
| [KeyPress][46]            |         |
| [KeyRelease][47]          |         |
| [KeyRepeat][48]           |         |
| [CharacterTyped][49]      |         |
| [MousePress][50]          |         |
| [MouseRelease][51]        |         |
| [MouseScroll][52]         |         |
| [MouseMove][53]           |         |
| [Closing][54]             |         |
| [Closed][55]              |         |

## Methods

| Name                        | Summary                                                                             |
|-----------------------------|-------------------------------------------------------------------------------------|
| [Dispose][18]               |                                                                                     |
| [OnWindowResized][19]       |                                                                                     |
| [OnFramebufferResized][20]  |                                                                                     |
| [OnContentScaleChanged][21] |                                                                                     |
| [OnWindowMoved][22]         |                                                                                     |
| [OnKeyPressed][23]          |                                                                                     |
| [OnCharTyped][24]           |                                                                                     |
| [OnMousePressed][25]        |                                                                                     |
| [OnMouseMove][26]           |                                                                                     |
| [OnMouseScroll][27]         |                                                                                     |
| [OnClosing][28]             |                                                                                     |
| [OnClosed][29]              |                                                                                     |
| [Show][30]                  |                                                                                     |
| [Hide][31]                  |                                                                                     |
| [Close][32]                 |                                                                                     |
| [Focus][33]                 |                                                                                     |
| [Maximize][34]              | Sets the window to a maximized state.                                               |
| [Minimize][35]              | Sets the window to a minimized state.                                               |
| [Restore][36]               | Sets the window to a default size state.                                            |
| [BeginFullscreen][37]       | Puts the window into fullscreen using the nearest monitor and existing video mode.  |
| [BeginFullscreen][37]       | Sets the window to fullscreen using the specified monitor and existing video mode.  |
| [BeginFullscreen][37]       | Puts the window into fullscreen using the nearest monitor and specified video mode. |
| [BeginFullscreen][37]       | Sets the window to fullscreen using the specified monitor and video mode.           |
| [EndFullscreen][38]         | Disables fullscreen mode.                                                           |
| [MoveToCenter][39]          | Moves the window to the center of the nearest monitor.                              |
| [MoveToCenter][39]          | Moves the window to the center of the specified monitor.                            |
| [SetIcons][40]              |                                                                                     |
| [SetIcon][41]               | Assigns a new icon image to the window.                                             |
| [SetCursor][42]             | Changes the appearance of the cursor on this window.                                |
| [SetCursor][42]             | Changes the appearance of the cursor on this window.                                |
| [SetCursor][42]             | Changes the appearance of the cursor on this window.                                |
| [Dispose][18]               |                                                                                     |

[0]: ../Heirloom.Platforms.Desktop.md
[1]: Heirloom.Desktop.Window.IsDisposed.md
[2]: Heirloom.Desktop.Window.IsClosed.md
[3]: Heirloom.Desktop.Window.HasTransparentFramebuffer.md
[4]: Heirloom.Desktop.Window.Graphics.md
[5]: Heirloom.Desktop.Window.IsVisible.md
[6]: Heirloom.Desktop.Window.IsDecorated.md
[7]: Heirloom.Desktop.Window.IsResizable.md
[8]: Heirloom.Desktop.Window.IsFloating.md
[9]: Heirloom.Desktop.Window.Title.md
[10]: Heirloom.Desktop.Window.Bounds.md
[11]: Heirloom.Desktop.Window.Position.md
[12]: Heirloom.Desktop.Window.Size.md
[13]: Heirloom.Desktop.Window.FramebufferSize.md
[14]: Heirloom.Desktop.Window.ContentScale.md
[15]: Heirloom.Desktop.Window.State.md
[16]: Heirloom.Desktop.Window.Monitor.md
[17]: Heirloom.Desktop.Window.Icons.md
[18]: Heirloom.Desktop.Window.Dispose.md
[19]: Heirloom.Desktop.Window.OnWindowResized.md
[20]: Heirloom.Desktop.Window.OnFramebufferResized.md
[21]: Heirloom.Desktop.Window.OnContentScaleChanged.md
[22]: Heirloom.Desktop.Window.OnWindowMoved.md
[23]: Heirloom.Desktop.Window.OnKeyPressed.md
[24]: Heirloom.Desktop.Window.OnCharTyped.md
[25]: Heirloom.Desktop.Window.OnMousePressed.md
[26]: Heirloom.Desktop.Window.OnMouseMove.md
[27]: Heirloom.Desktop.Window.OnMouseScroll.md
[28]: Heirloom.Desktop.Window.OnClosing.md
[29]: Heirloom.Desktop.Window.OnClosed.md
[30]: Heirloom.Desktop.Window.Show.md
[31]: Heirloom.Desktop.Window.Hide.md
[32]: Heirloom.Desktop.Window.Close.md
[33]: Heirloom.Desktop.Window.Focus.md
[34]: Heirloom.Desktop.Window.Maximize.md
[35]: Heirloom.Desktop.Window.Minimize.md
[36]: Heirloom.Desktop.Window.Restore.md
[37]: Heirloom.Desktop.Window.BeginFullscreen.md
[38]: Heirloom.Desktop.Window.EndFullscreen.md
[39]: Heirloom.Desktop.Window.MoveToCenter.md
[40]: Heirloom.Desktop.Window.SetIcons.md
[41]: Heirloom.Desktop.Window.SetIcon.md
[42]: Heirloom.Desktop.Window.SetCursor.md
[43]: Heirloom.Desktop.Window.Resized.md
[44]: Heirloom.Desktop.Window.FramebufferResized.md
[45]: Heirloom.Desktop.Window.ContentScaleChanged.md
[46]: Heirloom.Desktop.Window.KeyPress.md
[47]: Heirloom.Desktop.Window.KeyRelease.md
[48]: Heirloom.Desktop.Window.KeyRepeat.md
[49]: Heirloom.Desktop.Window.CharacterTyped.md
[50]: Heirloom.Desktop.Window.MousePress.md
[51]: Heirloom.Desktop.Window.MouseRelease.md
[52]: Heirloom.Desktop.Window.MouseScroll.md
[53]: Heirloom.Desktop.Window.MouseMove.md
[54]: Heirloom.Desktop.Window.Closing.md
[55]: Heirloom.Desktop.Window.Closed.md
