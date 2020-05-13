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

[BeginFullscreen][17], [Close][18], [Dispose][19], [EndFullscreen][20], [Finalize][21], [Focus][22], [Hide][23], [HideSoftwareKeyboard][24], [Maximize][25], [Minimize][26], [MoveToCenter][27], [Restore][28], [SetCursor][29], [SetIcon][30], [SetIcons][31], [Show][32], [ShowSoftwareKeyboard][33]

## Properties

#### Instance

| Name                           | Type                   | Summary                                                                |
|--------------------------------|------------------------|------------------------------------------------------------------------|
| [Bounds][3]                    | [IntRectangle][34]     | Gets or sets the window bounds in screen units.                        |
| [ContentScale][4]              | [Vector][35]           | Gets the content scaling factor.                                       |
| [HasTransparentFramebuffer][5] | `bool`                 | Gets a value that determines if this window supports a transparent ... |
| [Icons][6]                     | `IReadOnlyList<Image>` | Gets this windows icon set.                                            |
| [IsDecorated][7]               | `bool`                 | Gets a value that determines if the window is decorated.               |
| [IsFloating][8]                | `bool`                 | Gets a value that determines if the window "always on top".            |
| [IsResizable][9]               | `bool`                 | Gets a value that determines if the window be resized.                 |
| [IsVisible][10]                | `bool`                 | Gets a value that determines if the window is visible.                 |
| [Monitor][11]                  | [Monitor][36]          | Gets the monitor this window is positioned on by checking the cente... |
| [Position][12]                 | [IntVector][37]        | Gets or sets the window position in screen coordinates.                |
| [Size][13]                     | [IntSize][38]          | Gets or sets the window size in screen units.                          |
| [State][14]                    | [WindowState][39]      | Gets the current state of the window.                                  |
| [SupportsSoftwareKeyboard][15] | `bool`                 | Gets a value that determines if a software keyboard is supported on... |
| [Title][16]                    | `string`               | Gets or set the window title text.                                     |

## Methods

#### Instance

| Name                            | Return Type | Summary                                                                |
|---------------------------------|-------------|------------------------------------------------------------------------|
| [BeginFullscreen()][17]         | `void`      | Puts the window into fullscreen using the nearest monitor and exist... |
| [BeginFullscreen(Monitor)][17]  | `void`      | Sets the window to fullscreen using the specified monitor and exist... |
| [BeginFullscreen(VideoM...][17] | `void`      | Puts the window into fullscreen using the nearest monitor and speci... |
| [BeginFullscreen(VideoM...][17] | `void`      | Sets the window to fullscreen using the specified monitor and video... |
| [Close()][18]                   | `void`      | Closes this window.                                                    |
| [Dispose(bool)][19]             | `void`      |                                                                        |
| [EndFullscreen()][20]           | `void`      | Disables fullscreen mode.                                              |
| [Finalize()][21]                | `void`      | Performs final cleanup of Window before garbase collection.            |
| [Focus()][22]                   | `void`      | Brings focus to this window.                                           |
| [Hide()][23]                    | `void`      | Hides the window, minimizing it.                                       |
| [HideSoftwareKeyboard()][24]    | `void`      | Hides the software keyboard.                                           |
| [Maximize()][25]                | `void`      | Sets the window to a maximized state.                                  |
| [Minimize()][26]                | `void`      | Sets the window to a minimized state.                                  |
| [MoveToCenter()][27]            | `void`      | Moves the window to the center of the nearest monitor.                 |
| [MoveToCenter(Monitor)][27]     | `void`      | Moves the window to the center of the specified monitor.               |
| [Restore()][28]                 | `void`      | Sets the window to a default size state.                               |
| [SetCursor(StandardCursor)][29] | `void`      | Changes the appearance of the cursor on this window.                   |
| [SetCursor(Image)][29]          | `void`      | Changes the appearance of the cursor on this window.                   |
| [SetCursor(Image, IntVe...][29] | `void`      | Changes the appearance of the cursor on this window.                   |
| [SetIcon(Image)][30]            | `void`      | Assigns a new icon image to the window.                                |
| [SetIcons(Image[])][31]         | `void`      |                                                                        |
| [Show()][32]                    | `void`      | Shows the window, making it visible.                                   |
| [ShowSoftwareKeyboard()][33]    | `void`      | Attempts to show the software keyboard.                                |

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
[21]: Window/Finalize.md
[22]: Window/Focus.md
[23]: Window/Hide.md
[24]: Window/HideSoftwareKeyboard.md
[25]: Window/Maximize.md
[26]: Window/Minimize.md
[27]: Window/MoveToCenter.md
[28]: Window/Restore.md
[29]: Window/SetCursor.md
[30]: Window/SetIcon.md
[31]: Window/SetIcons.md
[32]: Window/Show.md
[33]: Window/ShowSoftwareKeyboard.md
[34]: ../../Heirloom.Core/Heirloom/IntRectangle.md
[35]: ../../Heirloom.Core/Heirloom/Vector.md
[36]: Monitor.md
[37]: ../../Heirloom.Core/Heirloom/IntVector.md
[38]: ../../Heirloom.Core/Heirloom/IntSize.md
[39]: WindowState.md
