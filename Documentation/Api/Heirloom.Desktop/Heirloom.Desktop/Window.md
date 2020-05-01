# Heirloom.Desktop

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Desktop][0]

## Window (Class)

> **Namespace**: [Heirloom.Desktop][0]

```cs
public class Window
```

### Properties

[Bounds][1], [ContentScale][2], [FramebufferSize][3], [Graphics][4], [HasTransparentFramebuffer][5], [Icons][6], [IsClosed][7], [IsDecorated][8], [IsDisposed][9], [IsFloating][10], [IsResizable][11], [IsVisible][12], [Monitor][13], [Position][14], [Size][15], [State][16], [Title][17]

### Methods

[BeginFullscreen][18], [Close][19], [Dispose][20], [EndFullscreen][21], [Focus][22], [Hide][23], [Maximize][24], [Minimize][25], [MoveToCenter][26], [OnCharTyped][27], [OnClosed][28], [OnClosing][29], [OnContentScaleChanged][30], [OnFramebufferResized][31], [OnKeyPressed][32], [OnMouseMove][33], [OnMousePressed][34], [OnMouseScroll][35], [OnWindowMoved][36], [OnWindowResized][37], [Restore][38], [SetCursor][39], [SetIcon][40], [SetIcons][41], [Show][42]

### Events

[CharacterTyped][43], [Closed][44], [Closing][45], [ContentScaleChanged][46], [FramebufferResized][47], [KeyPress][48], [KeyRelease][49], [KeyRepeat][50], [MouseMove][51], [MousePress][52], [MouseRelease][53], [MouseScroll][54], [Resized][55]

## Properties

#### Instance

| Name                           | Type                    | Summary                                                                |
|--------------------------------|-------------------------|------------------------------------------------------------------------|
| [Bounds][1]                    | [IntRectangle][56]      | Gets or sets the window bounds in screen units.                        |
| [ContentScale][2]              | [Vector][57]            | Gets the content scaling factor.                                       |
| [FramebufferSize][3]           | [IntSize][58]           | The size of the underlying framebuffer in pixels.                      |
| [Graphics][4]                  | [Graphics][59]          | Gets the graphics context associated with this window.                 |
| [HasTransparentFramebuffer][5] | `bool`                  | Gets a value that determines if this window supports a transparent ... |
| [Icons][6]                     | `IReadOnlyList\<Image>` | Gets this windows icon set.                                            |
| [IsClosed][7]                  | `bool`                  | Gets a value that determines if this window been closed.               |
| [IsDecorated][8]               | `bool`                  | Gets a value that determines if the window is decorated.               |
| [IsDisposed][9]                | `bool`                  | Gets a value that determines if this window been disposed.             |
| [IsFloating][10]               | `bool`                  | Gets a value that determines if the window "always on top".            |
| [IsResizable][11]              | `bool`                  | Gets a value that determines if the window be resized.                 |
| [IsVisible][12]                | `bool`                  | Gets a value that determines if the window is visible.                 |
| [Monitor][13]                  | [Monitor][60]           | Gets the monitor this window is positioned on by checking the cente... |
| [Position][14]                 | [IntVector][61]         | Gets or sets the window position in screen coordinates.                |
| [Size][15]                     | [IntSize][58]           | Gets or sets the window size in screen units.                          |
| [State][16]                    | [WindowState][62]       | Gets the current state of the window.                                  |
| [Title][17]                    | `string`                | Gets or set the window title text.                                     |

## Events

#### Instance

| Name                      | Handler Type                        | Summary |
|---------------------------|-------------------------------------|---------|
| [CharacterTyped][43]      | `Action\<Window, CharacterEvent>`   |         |
| [Closed][44]              | `Action\<Window>`                   |         |
| [Closing][45]             | `Func\<Window, bool>`               |         |
| [ContentScaleChanged][46] | `Action\<Window, WindowEvents>`     |         |
| [FramebufferResized][47]  | `Action\<Window>`                   |         |
| [KeyPress][48]            | `Action\<Window, KeyEvent>`         |         |
| [KeyRelease][49]          | `Action\<Window, KeyEvent>`         |         |
| [KeyRepeat][50]           | `Action\<Window, KeyEvent>`         |         |
| [MouseMove][51]           | `Action\<Window, MouseMoveEvent>`   |         |
| [MousePress][52]          | `Action\<Window, MouseButtonEvent>` |         |
| [MouseRelease][53]        | `Action\<Window, MouseButtonEvent>` |         |
| [MouseScroll][54]         | `Action\<Window, MouseScrollEvent>` |         |
| [Resized][55]             | `Action\<Window>`                   |         |

## Methods

#### Instance

| Name                            | Return Type | Summary                                                                |
|---------------------------------|-------------|------------------------------------------------------------------------|
| [BeginFullscreen()][18]         | `void`      | Puts the window into fullscreen using the nearest monitor and exist... |
| [BeginFullscreen(Monitor)][18]  | `void`      | Sets the window to fullscreen using the specified monitor and exist... |
| [BeginFullscreen(VideoM...][18] | `void`      | Puts the window into fullscreen using the nearest monitor and speci... |
| [BeginFullscreen(VideoM...][18] | `void`      | Sets the window to fullscreen using the specified monitor and video... |
| [Close()][19]                   | `void`      |                                                                        |
| [Dispose()][20]                 | `void`      |                                                                        |
| [Dispose(bool)][20]             | `void`      |                                                                        |
| [EndFullscreen()][21]           | `void`      | Disables fullscreen mode.                                              |
| [Focus()][22]                   | `void`      |                                                                        |
| [Hide()][23]                    | `void`      |                                                                        |
| [Maximize()][24]                | `void`      | Sets the window to a maximized state.                                  |
| [Minimize()][25]                | `void`      | Sets the window to a minimized state.                                  |
| [MoveToCenter()][26]            | `void`      | Moves the window to the center of the nearest monitor.                 |
| [MoveToCenter(Monitor)][26]     | `void`      | Moves the window to the center of the specified monitor.               |
| [OnCharTyped(UnicodeCha...][27] | `void`      |                                                                        |
| [OnClosed()][28]                | `void`      |                                                                        |
| [OnClosing()][29]               | `bool`      |                                                                        |
| [OnContentScaleChanged(...][30] | `void`      |                                                                        |
| [OnFramebufferResized(i...][31] | `void`      |                                                                        |
| [OnKeyPressed(Key, int,...][32] | `void`      |                                                                        |
| [OnMouseMove(float, float)][33] | `void`      |                                                                        |
| [OnMousePressed(int, Bu...][34] | `void`      |                                                                        |
| [OnMouseScroll(float, f...][35] | `void`      |                                                                        |
| [OnWindowMoved(int, int)][36]   | `void`      |                                                                        |
| [OnWindowResized(int, int)][37] | `void`      |                                                                        |
| [Restore()][38]                 | `void`      | Sets the window to a default size state.                               |
| [SetCursor(StandardCursor)][39] | `void`      | Changes the appearance of the cursor on this window.                   |
| [SetCursor(Image)][39]          | `void`      | Changes the appearance of the cursor on this window.                   |
| [SetCursor(Image, IntVe...][39] | `void`      | Changes the appearance of the cursor on this window.                   |
| [SetIcon(Image)][40]            | `void`      | Assigns a new icon image to the window.                                |
| [SetIcons(Image[])][41]         | `void`      |                                                                        |
| [Show()][42]                    | `void`      |                                                                        |

[0]: ../../Heirloom.Desktop.md
[1]: Window/Bounds.md
[2]: Window/ContentScale.md
[3]: Window/FramebufferSize.md
[4]: Window/Graphics.md
[5]: Window/HasTransparentFramebuffer.md
[6]: Window/Icons.md
[7]: Window/IsClosed.md
[8]: Window/IsDecorated.md
[9]: Window/IsDisposed.md
[10]: Window/IsFloating.md
[11]: Window/IsResizable.md
[12]: Window/IsVisible.md
[13]: Window/Monitor.md
[14]: Window/Position.md
[15]: Window/Size.md
[16]: Window/State.md
[17]: Window/Title.md
[18]: Window/BeginFullscreen.md
[19]: Window/Close.md
[20]: Window/Dispose.md
[21]: Window/EndFullscreen.md
[22]: Window/Focus.md
[23]: Window/Hide.md
[24]: Window/Maximize.md
[25]: Window/Minimize.md
[26]: Window/MoveToCenter.md
[27]: Window/OnCharTyped.md
[28]: Window/OnClosed.md
[29]: Window/OnClosing.md
[30]: Window/OnContentScaleChanged.md
[31]: Window/OnFramebufferResized.md
[32]: Window/OnKeyPressed.md
[33]: Window/OnMouseMove.md
[34]: Window/OnMousePressed.md
[35]: Window/OnMouseScroll.md
[36]: Window/OnWindowMoved.md
[37]: Window/OnWindowResized.md
[38]: Window/Restore.md
[39]: Window/SetCursor.md
[40]: Window/SetIcon.md
[41]: Window/SetIcons.md
[42]: Window/Show.md
[43]: Window/CharacterTyped.md
[44]: Window/Closed.md
[45]: Window/Closing.md
[46]: Window/ContentScaleChanged.md
[47]: Window/FramebufferResized.md
[48]: Window/KeyPress.md
[49]: Window/KeyRelease.md
[50]: Window/KeyRepeat.md
[51]: Window/MouseMove.md
[52]: Window/MousePress.md
[53]: Window/MouseRelease.md
[54]: Window/MouseScroll.md
[55]: Window/Resized.md
[56]: ../../Heirloom.Core/Heirloom/IntRectangle.md
[57]: ../../Heirloom.Core/Heirloom/Vector.md
[58]: ../../Heirloom.Core/Heirloom/IntSize.md
[59]: ../../Heirloom.Core/Heirloom/Graphics.md
[60]: Monitor.md
[61]: ../../Heirloom.Core/Heirloom/IntVector.md
[62]: WindowState.md
