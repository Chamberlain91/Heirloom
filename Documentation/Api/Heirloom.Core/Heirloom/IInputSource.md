# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## IInputSource (Interface)

> **Namespace**: [Heirloom][0]

Represents the functionality of an input source.

```cs
public interface IInputSource
```

### Properties

[SupportsSoftwareKeyboard][1]

### Methods

[HideSoftwareKeyboard][2], [ShowSoftwareKeyboard][3], [TryGetButton][4], [TryGetKey][5]

### Events

[CharacterTyped][6], [KeyPressed][7], [KeyReleased][8], [KeyRepeat][9], [MouseMoved][10], [MousePressed][11], [MouseReleased][12], [MouseScrolled][13]

## Properties

#### Instance

| Name                          | Type   | Summary                                                                |
|-------------------------------|--------|------------------------------------------------------------------------|
| [SupportsSoftwareKeyboard][1] | `bool` | Gets a value that determines if a software keyboard is supported on... |

## Events

#### Instance

| Name                | Handler Type                       | Summary                                                       |
|---------------------|------------------------------------|---------------------------------------------------------------|
| [CharacterTyped][6] | `Action<Screen, CharacterEvent>`   | An event raised when a character has been typed.              |
| [KeyPressed][7]     | `Action<Screen, KeyEvent>`         | An event raised when a button on the keyboard was pressed.    |
| [KeyReleased][8]    | `Action<Screen, KeyEvent>`         | An event raised when a button on the keyboard was released.   |
| [KeyRepeat][9]      | `Action<Screen, KeyEvent>`         | An event raised when a button on the keyboard was 'repeated'. |
| [MouseMoved][10]    | `Action<Screen, MouseMoveEvent>`   | An event raised when mouse has been moved.                    |
| [MousePressed][11]  | `Action<Screen, MouseButtonEvent>` | An event raised when a button the mouse is pressed.           |
| [MouseReleased][12] | `Action<Screen, MouseButtonEvent>` | An event raised when a button the mouse is released.          |
| [MouseScrolled][13] | `Action<Screen, MouseScrollEvent>` | An event raised when mouse has been scrolled.                 |

## Methods

#### Instance

| Name                           | Return Type | Summary                                                       |
|--------------------------------|-------------|---------------------------------------------------------------|
| [HideSoftwareKeyboard()][2]    | `void`      | Hides the software keyboard.                                  |
| [ShowSoftwareKeyboard()][3]    | `void`      | Attempts to show the software keyboard.                       |
| [TryGetButton(MouseButt...][4] | `bool`      | Attempts to retreive the state of the specified mouse button. |
| [TryGetKey(Key, out But...][5] | `bool`      | Attempts to retreive the state of the specified key.          |

[0]: ../../Heirloom.Core.md
[1]: IInputSource/SupportsSoftwareKeyboard.md
[2]: IInputSource/HideSoftwareKeyboard.md
[3]: IInputSource/ShowSoftwareKeyboard.md
[4]: IInputSource/TryGetButton.md
[5]: IInputSource/TryGetKey.md
[6]: IInputSource/CharacterTyped.md
[7]: IInputSource/KeyPressed.md
[8]: IInputSource/KeyReleased.md
[9]: IInputSource/KeyRepeat.md
[10]: IInputSource/MouseMoved.md
[11]: IInputSource/MousePressed.md
[12]: IInputSource/MouseReleased.md
[13]: IInputSource/MouseScrolled.md
