# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Input (Class)

> **Namespace**: [Heirloom][0]

Provides a centralized query style input layer. This is useful for implementing games.

```cs
public static class Input
```

### Static Properties

[MouseDelta][1], [MousePosition][2], [SupportsSoftwareKeyboard][3]

### Static Methods

[AddInputSource][4], [CheckKey][5], [CheckMouse][6], [GetButton][7], [GetKey][8], [HideSoftKeyboard][9], [RemoveInputSource][10], [ShowSoftKeyboard][11]

### Static Events

[CharacterTyped][12], [KeyPressed][13], [KeyReleased][14], [KeyRepeat][15], [MouseMoved][16], [MousePressed][17], [MouseReleased][18], [MouseScrolled][19]

## Properties

| Name                          | Type         | Summary                                                   |
|-------------------------------|--------------|-----------------------------------------------------------|
| [MouseDelta][1]               | [Vector][20] | Gets the latest mouse position delta.                     |
| [MousePosition][2]            | [Vector][20] | Gets the latest mouse position.                           |
| [SupportsSoftwareKeyboard][3] | `bool`       | Is the keyboard emulated by software? (ie mobile deviecs) |

## Events

| Name                 | Handler Type                        | Summary |
|----------------------|-------------------------------------|---------|
| [CharacterTyped][12] | `Action\<Screen, CharacterEvent>`   |         |
| [KeyPressed][13]     | `Action\<Screen, KeyEvent>`         |         |
| [KeyReleased][14]    | `Action\<Screen, KeyEvent>`         |         |
| [KeyRepeat][15]      | `Action\<Screen, KeyEvent>`         |         |
| [MouseMoved][16]     | `Action\<Screen, MouseMoveEvent>`   |         |
| [MousePressed][17]   | `Action\<Screen, MouseButtonEvent>` |         |
| [MouseReleased][18]  | `Action\<Screen, MouseButtonEvent>` |         |
| [MouseScrolled][19]  | `Action\<Screen, MouseScrollEvent>` |         |

## Methods

| Name                            | Return Type       | Summary                                                                |
|---------------------------------|-------------------|------------------------------------------------------------------------|
| [AddInputSource(IInputS...][4]  | `void`            | Adds and begins tracking input from an input source.                   |
| [CheckKey(Key, ButtonSt...][5]  | `bool`            | Checks if the lastest state of a button on the keyboard matcheas th... |
| [CheckMouse(MouseButton...][6]  | `bool`            | Checks if the lastest state of a mouse button matcheas the desired ... |
| [GetButton(MouseButton)][7]     | [ButtonState][21] | Gets the latest state of a mouse button.                               |
| [GetKey(Key)][8]                | [ButtonState][21] | Gets the latest state of a button the keyboard.                        |
| [HideSoftKeyboard()][9]         | `void`            | Hides the software keyboard.                                           |
| [RemoveInputSource(IInp...][10] | `void`            | Removes and tops tracking input from an input source.                  |
| [ShowSoftKeyboard()][11]        | `void`            | Shows the software keyboard.                                           |

[0]: ../../Heirloom.Core.md
[1]: Input/MouseDelta.md
[2]: Input/MousePosition.md
[3]: Input/SupportsSoftwareKeyboard.md
[4]: Input/AddInputSource.md
[5]: Input/CheckKey.md
[6]: Input/CheckMouse.md
[7]: Input/GetButton.md
[8]: Input/GetKey.md
[9]: Input/HideSoftKeyboard.md
[10]: Input/RemoveInputSource.md
[11]: Input/ShowSoftKeyboard.md
[12]: Input/CharacterTyped.md
[13]: Input/KeyPressed.md
[14]: Input/KeyReleased.md
[15]: Input/KeyRepeat.md
[16]: Input/MouseMoved.md
[17]: Input/MousePressed.md
[18]: Input/MouseReleased.md
[19]: Input/MouseScrolled.md
[20]: Vector.md
[21]: ButtonState.md
