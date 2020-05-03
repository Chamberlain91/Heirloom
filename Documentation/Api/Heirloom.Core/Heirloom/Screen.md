# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Screen (Class)

> **Namespace**: [Heirloom][0]

An abstract representation of the screen (ie, window, view, etc).

```cs
public abstract class Screen : IInputSource, IDisposable
```

### Inherits

[IInputSource][1], IDisposable

### Properties

[Graphics][2], [Height][3], [IsClosed][4], [IsDisposed][5], [Size][6], [SupportsSoftwareKeyboard][7], [Surface][8], [Width][9]

### Methods

[Close][10], [Dispose][11], [HideSoftwareKeyboard][12], [OnCharacterTyped][13], [OnClosed][14], [OnClosing][15], [OnContentScaleChanged][16], [OnFocusChanged][17], [OnFramebufferResized][18], [OnKeyPressed][19], [OnKeyReleased][20], [OnKeyRepeat][21], [OnMouseMoved][22], [OnMousePressed][23], [OnMouseReleased][24], [OnMouseScrolled][25], [OnResized][26], [Refresh][27], [ShowSoftwareKeyboard][28]

### Events

[CharacterTyped][29], [Closed][30], [Closing][31], [ContentScaleChanged][32], [FocusChanged][33], [FramebufferResized][34], [KeyPressed][35], [KeyReleased][36], [KeyRepeat][37], [MouseMoved][38], [MousePressed][39], [MouseReleased][40], [MouseScrolled][41], [Resized][42]

## Properties

#### Instance

| Name                          | Type           | Summary                                                      |
|-------------------------------|----------------|--------------------------------------------------------------|
| [Graphics][2]                 | [Graphics][43] | Gets the graphics context that can draw on this screen.      |
| [Height][3]                   | `int`          | Gets the height of the screen.                               |
| [IsClosed][4]                 | `bool`         | Gets a value that determines if this screen has been closed. |
| [IsDisposed][5]               | `bool`         | Gets a value that determines if this window been disposed.   |
| [Size][6]                     | [IntSize][44]  | Gets the size of the screen.                                 |
| [SupportsSoftwareKeyboard][7] | `bool`         |                                                              |
| [Surface][8]                  | [Surface][45]  | Gets surface that represents this screen.                    |
| [Width][9]                    | `int`          | Gets the width of the screen.                                |

## Events

#### Instance

| Name                      | Handler Type                        | Summary                                                                |
|---------------------------|-------------------------------------|------------------------------------------------------------------------|
| [CharacterTyped][29]      | `Action\<Screen, CharacterEvent>`   |                                                                        |
| [Closed][30]              | `Action\<Screen>`                   | Event called when the screen has closed.                               |
| [Closing][31]             | `Func\<Screen, bool>`               | Event called when the screen is trying to close. Returning false wi... |
| [ContentScaleChanged][32] | `Action\<Screen, Vector>`           | Event called when the content scaling of this screen changes.          |
| [FocusChanged][33]        | `Action\<Screen, bool>`             | Event called when the focused state of this screen changes.            |
| [FramebufferResized][34]  | `Action\<Screen, IntSize>`          | Event called when the screen surface is resized. On certain platfor... |
| [KeyPressed][35]          | `Action\<Screen, KeyEvent>`         |                                                                        |
| [KeyReleased][36]         | `Action\<Screen, KeyEvent>`         |                                                                        |
| [KeyRepeat][37]           | `Action\<Screen, KeyEvent>`         |                                                                        |
| [MouseMoved][38]          | `Action\<Screen, MouseMoveEvent>`   |                                                                        |
| [MousePressed][39]        | `Action\<Screen, MouseButtonEvent>` |                                                                        |
| [MouseReleased][40]       | `Action\<Screen, MouseButtonEvent>` |                                                                        |
| [MouseScrolled][41]       | `Action\<Screen, MouseScrollEvent>` |                                                                        |
| [Resized][42]             | `Action\<Screen, IntSize>`          | Event called when the screen is resized. On certain platforms, the ... |

## Methods

#### Instance

| Name                            | Return Type | Summary                                                                |
|---------------------------------|-------------|------------------------------------------------------------------------|
| [Close()][10]                   | `void`      | Attempts to close this screen.                                         |
| [Dispose(bool)][11]             | `void`      | Disposes this screen, freeing any unmanaged resources.                 |
| [Dispose()][11]                 | `void`      | Dispose this screen, freeing any unmanaged resources.                  |
| [HideSoftwareKeyboard()][12]    | `void`      |                                                                        |
| [OnCharacterTyped(Chara...][13] | `void`      | Call this function raise the E:Heirloom.Screen.CharacterTyped event.   |
| [OnClosed()][14]                | `void`      | Call this function raise the E:Heirloom.Screen.Closed event.           |
| [OnClosing()][15]               | `bool`      | Call this function raise the E:Heirloom.Screen.Closing event.          |
| [OnContentScaleChanged(...][16] | `void`      | Call this function raise the E:Heirloom.Screen.ContentScaleChanged ... |
| [OnFocusChanged(bool)][17]      | `void`      | Call this function raise the E:Heirloom.Screen.FocusChanged event.     |
| [OnFramebufferResized(I...][18] | `void`      | Call this function raise the E:Heirloom.Screen.FramebufferResized e... |
| [OnKeyPressed(KeyEvent)][19]    | `void`      | Call this function raise the E:Heirloom.Screen.KeyPressed event.       |
| [OnKeyReleased(KeyEvent)][20]   | `void`      | Call this function raise the E:Heirloom.Screen.KeyReleased event.      |
| [OnKeyRepeat(KeyEvent)][21]     | `void`      | Call this function raise the E:Heirloom.Screen.KeyRepeat event.        |
| [OnMouseMoved(MouseMove...][22] | `void`      | Call this function raise the E:Heirloom.Screen.MouseMoved event.       |
| [OnMousePressed(MouseBu...][23] | `void`      | Call this function raise the E:Heirloom.Screen.MousePressed event.     |
| [OnMouseReleased(MouseB...][24] | `void`      | Call this function raise the E:Heirloom.Screen.MouseReleased event.    |
| [OnMouseScrolled(MouseS...][25] | `void`      | Call this function raise the E:Heirloom.Screen.MouseScrolled event.    |
| [OnResized(IntSize)][26]        | `void`      | Call this function raise the E:Heirloom.Screen.Resized event.          |
| [Refresh()][27]                 | `void`      | Refresh the screen, presenting rendered graphics.                      |
| [ShowSoftwareKeyboard()][28]    | `void`      |                                                                        |

[0]: ../../Heirloom.Core.md
[1]: IInputSource.md
[2]: Screen/Graphics.md
[3]: Screen/Height.md
[4]: Screen/IsClosed.md
[5]: Screen/IsDisposed.md
[6]: Screen/Size.md
[7]: Screen/SupportsSoftwareKeyboard.md
[8]: Screen/Surface.md
[9]: Screen/Width.md
[10]: Screen/Close.md
[11]: Screen/Dispose.md
[12]: Screen/HideSoftwareKeyboard.md
[13]: Screen/OnCharacterTyped.md
[14]: Screen/OnClosed.md
[15]: Screen/OnClosing.md
[16]: Screen/OnContentScaleChanged.md
[17]: Screen/OnFocusChanged.md
[18]: Screen/OnFramebufferResized.md
[19]: Screen/OnKeyPressed.md
[20]: Screen/OnKeyReleased.md
[21]: Screen/OnKeyRepeat.md
[22]: Screen/OnMouseMoved.md
[23]: Screen/OnMousePressed.md
[24]: Screen/OnMouseReleased.md
[25]: Screen/OnMouseScrolled.md
[26]: Screen/OnResized.md
[27]: Screen/Refresh.md
[28]: Screen/ShowSoftwareKeyboard.md
[29]: Screen/CharacterTyped.md
[30]: Screen/Closed.md
[31]: Screen/Closing.md
[32]: Screen/ContentScaleChanged.md
[33]: Screen/FocusChanged.md
[34]: Screen/FramebufferResized.md
[35]: Screen/KeyPressed.md
[36]: Screen/KeyReleased.md
[37]: Screen/KeyRepeat.md
[38]: Screen/MouseMoved.md
[39]: Screen/MousePressed.md
[40]: Screen/MouseReleased.md
[41]: Screen/MouseScrolled.md
[42]: Screen/Resized.md
[43]: Graphics.md
[44]: IntSize.md
[45]: Surface.md
