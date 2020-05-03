# Heirloom.Desktop

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Desktop][0]

## Window (Class)

> **Namespace**: [Heirloom.Desktop][0]

Represents a physical window on a desktop platform, implements [Screen][1] .

```cs
public sealed class Window : Screen, IInputSource, IDisposable
```

### Inherits

[Screen][1], [IInputSource][2], IDisposable

### Properties

[Bounds][3], [ContentScale][4], [HasTransparentFramebuffer][5], [Icons][6], [IsDecorated][7], [IsFloating][8], [IsResizable][9], [IsVisible][10], [Monitor][11], [Position][12], [Size][13], [State][14], [SupportsSoftwareKeyboard][15], [Title][16]

### Methods

[BeginFullscreen][17], [Close][18], [Dispose][19], [EndFullscreen][20], [Focus][21], [Hide][22], [HideSoftwareKeyboard][23], [Maximize][24], [Minimize][25], [MoveToCenter][26], [Restore][27], [SetCursor][28], [SetIcon][29], [SetIcons][30], [Show][31], [ShowSoftwareKeyboard][32]

## Properties

#### Instance

| Name                           | Type                    | Summary                                                                |
|--------------------------------|-------------------------|------------------------------------------------------------------------|
| [Bounds][3]                    | [IntRectangle][33]      | Gets or sets the window bounds in screen units.                        |
| [ContentScale][4]              | [Vector][34]            | Gets the content scaling factor.                                       |
| [HasTransparentFramebuffer][5] | `bool`                  | Gets a value that determines if this window supports a transparent ... |
| [Icons][6]                     | `IReadOnlyList\<Image>` | Gets this windows icon set.                                            |
| [IsDecorated][7]               | `bool`                  | Gets a value that determines if the window is decorated.               |
| [IsFloating][8]                | `bool`                  | Gets a value that determines if the window "always on top".            |
| [IsResizable][9]               | `bool`                  | Gets a value that determines if the window be resized.                 |
| [IsVisible][10]                | `bool`                  | Gets a value that determines if the window is visible.                 |
| [Monitor][11]                  | [Monitor][35]           | Gets the monitor this window is positioned on by checking the cente... |
| [Position][12]                 | [IntVector][36]         | Gets or sets the window position in screen coordinates.                |
| [Size][13]                     | [IntSize][37]           | Gets or sets the window size in screen units.                          |
| [State][14]                    | [WindowState][38]       | Gets the current state of the window.                                  |
| [SupportsSoftwareKeyboard][15] | `bool`                  |                                                                        |
| [Title][16]                    | `string`                | Gets or set the window title text.                                     |

## Methods

#### Instance

| Name                            | Return Type | Summary                                                                |
|---------------------------------|-------------|------------------------------------------------------------------------|
| [BeginFullscreen()][17]         | `void`      | Puts the window into fullscreen using the nearest monitor and exist... |
| [BeginFullscreen(Monitor)][17]  | `void`      | Sets the window to fullscreen using the specified monitor and exist... |
| [BeginFullscreen(VideoM...][17] | `void`      | Puts the window into fullscreen using the nearest monitor and speci... |
| [BeginFullscreen(VideoM...][17] | `void`      | Sets the window to fullscreen using the specified monitor and video... |
| [Close()][18]                   | `void`      |                                                                        |
| [Dispose(bool)][19]             | `void`      |                                                                        |
| [EndFullscreen()][20]           | `void`      | Disables fullscreen mode.                                              |
| [Focus()][21]                   | `void`      |                                                                        |
| [Hide()][22]                    | `void`      |                                                                        |
| [HideSoftwareKeyboard()][23]    | `void`      |                                                                        |
| [Maximize()][24]                | `void`      | Sets the window to a maximized state.                                  |
| [Minimize()][25]                | `void`      | Sets the window to a minimized state.                                  |
| [MoveToCenter()][26]            | `void`      | Moves the window to the center of the nearest monitor.                 |
| [MoveToCenter(Monitor)][26]     | `void`      | Moves the window to the center of the specified monitor.               |
| [Restore()][27]                 | `void`      | Sets the window to a default size state.                               |
| [SetCursor(StandardCursor)][28] | `void`      | Changes the appearance of the cursor on this window.                   |
| [SetCursor(Image)][28]          | `void`      | Changes the appearance of the cursor on this window.                   |
| [SetCursor(Image, IntVe...][28] | `void`      | Changes the appearance of the cursor on this window.                   |
| [SetIcon(Image)][29]            | `void`      | Assigns a new icon image to the window.                                |
| [SetIcons(Image[])][30]         | `void`      |                                                                        |
| [Show()][31]                    | `void`      |                                                                        |
| [ShowSoftwareKeyboard()][32]    | `void`      |                                                                        |

[0]: ../../Heirloom.Desktop.md
[1]: ../../Heirloom.Core/Heirloom/Screen.md
[2]: ../../Heirloom.Core/Heirloom/IInputSource.md
[3]: Window/Bounds.md
[4]: Window/ContentScale.md
[5]: Window/HasTransparentFramebuffer.md
[6]: Window/Icons.md
[7]: Window/IsDecorated.md
[8]: Window/IsFloating.md
[9]: Window/IsResizable.md
[10]: Window/IsVisible.md
[11]: Window/Monitor.md
[12]: Window/Position.md
[13]: Window/Size.md
[14]: Window/State.md
[15]: Window/SupportsSoftwareKeyboard.md
[16]: Window/Title.md
[17]: Window/BeginFullscreen.md
[18]: Window/Close.md
[19]: Window/Dispose.md
[20]: Window/EndFullscreen.md
[21]: Window/Focus.md
[22]: Window/Hide.md
[23]: Window/HideSoftwareKeyboard.md
[24]: Window/Maximize.md
[25]: Window/Minimize.md
[26]: Window/MoveToCenter.md
[27]: Window/Restore.md
[28]: Window/SetCursor.md
[29]: Window/SetIcon.md
[30]: Window/SetIcons.md
[31]: Window/Show.md
[32]: Window/ShowSoftwareKeyboard.md
[33]: ../../Heirloom.Core/Heirloom/IntRectangle.md
[34]: ../../Heirloom.Core/Heirloom/Vector.md
[35]: Monitor.md
[36]: ../../Heirloom.Core/Heirloom/IntVector.md
[37]: ../../Heirloom.Core/Heirloom/IntSize.md
[38]: WindowState.md
