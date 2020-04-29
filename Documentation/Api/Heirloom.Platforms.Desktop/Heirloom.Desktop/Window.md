# Heirloom.Platforms.Desktop

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Platforms.Desktop][0]  

## Window Class

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

[0]: ../../Heirloom.Platforms.Desktop.md
[1]: Window/IsDisposed.md
[2]: Window/IsClosed.md
[3]: Window/HasTransparentFramebuffer.md
[4]: Window/Graphics.md
[5]: Window/IsVisible.md
[6]: Window/IsDecorated.md
[7]: Window/IsResizable.md
[8]: Window/IsFloating.md
[9]: Window/Title.md
[10]: Window/Bounds.md
[11]: Window/Position.md
[12]: Window/Size.md
[13]: Window/FramebufferSize.md
[14]: Window/ContentScale.md
[15]: Window/State.md
[16]: Window/Monitor.md
[17]: Window/Icons.md
[18]: Window/Dispose.md
[19]: Window/OnWindowResized.md
[20]: Window/OnFramebufferResized.md
[21]: Window/OnContentScaleChanged.md
[22]: Window/OnWindowMoved.md
[23]: Window/OnKeyPressed.md
[24]: Window/OnCharTyped.md
[25]: Window/OnMousePressed.md
[26]: Window/OnMouseMove.md
[27]: Window/OnMouseScroll.md
[28]: Window/OnClosing.md
[29]: Window/OnClosed.md
[30]: Window/Show.md
[31]: Window/Hide.md
[32]: Window/Close.md
[33]: Window/Focus.md
[34]: Window/Maximize.md
[35]: Window/Minimize.md
[36]: Window/Restore.md
[37]: Window/BeginFullscreen.md
[38]: Window/EndFullscreen.md
[39]: Window/MoveToCenter.md
[40]: Window/SetIcons.md
[41]: Window/SetIcon.md
[42]: Window/SetCursor.md
[43]: Window/Resized.md
[44]: Window/FramebufferResized.md
[45]: Window/ContentScaleChanged.md
[46]: Window/KeyPress.md
[47]: Window/KeyRelease.md
[48]: Window/KeyRepeat.md
[49]: Window/CharacterTyped.md
[50]: Window/MousePress.md
[51]: Window/MouseRelease.md
[52]: Window/MouseScroll.md
[53]: Window/MouseMove.md
[54]: Window/Closing.md
[55]: Window/Closed.md
