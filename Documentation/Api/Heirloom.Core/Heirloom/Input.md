# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Input (Class)

> **Namespace**: [Heirloom][0]

Provides a centralized query style input layer. This is useful for implementing games.

```cs
public static class Input
```

The following is an example checking for button on the keyboard: ```cs
if(Input.CheckButton(Key.A, ButtonState.Pressed)) { Log.Info("Pressed A Key"); }
```

### Static Properties

[MouseDelta][1], [MousePosition][2], [SupportsSoftwareKeyboard][3]

### Static Methods

[AddInputSource][4], [CheckButton][5], [CheckMouse][6], [GetButton][7], [HideSoftKeyboard][8], [RemoveInputSource][9], [ShowSoftKeyboard][10]

### Static Events

[CharacterTyped][11], [KeyPressed][12], [KeyReleased][13], [KeyRepeat][14], [MouseMoved][15], [MousePressed][16], [MouseReleased][17], [MouseScrolled][18]

## Properties

| Name                          | Type         | Summary                                                   |
|-------------------------------|--------------|-----------------------------------------------------------|
| [MouseDelta][1]               | [Vector][19] | Gets the latest mouse position delta.                     |
| [MousePosition][2]            | [Vector][19] | Gets the latest mouse position.                           |
| [SupportsSoftwareKeyboard][3] | `bool`       | Is the keyboard emulated by software? (ie mobile deviecs) |

## Events

| Name                 | Handler Type                        | Summary |
|----------------------|-------------------------------------|---------|
| [CharacterTyped][11] | `Action\<Screen, CharacterEvent>`   |         |
| [KeyPressed][12]     | `Action\<Screen, KeyEvent>`         |         |
| [KeyReleased][13]    | `Action\<Screen, KeyEvent>`         |         |
| [KeyRepeat][14]      | `Action\<Screen, KeyEvent>`         |         |
| [MouseMoved][15]     | `Action\<Screen, MouseMoveEvent>`   |         |
| [MousePressed][16]   | `Action\<Screen, MouseButtonEvent>` |         |
| [MouseReleased][17]  | `Action\<Screen, MouseButtonEvent>` |         |
| [MouseScrolled][18]  | `Action\<Screen, MouseScrollEvent>` |         |

## Methods

| Name                           | Return Type       | Summary                                                                |
|--------------------------------|-------------------|------------------------------------------------------------------------|
| [AddInputSource(IInputS...][4] | `void`            | Adds and begins tracking input from an input source.                   |
| [CheckButton(Key, Butto...][5] | `bool`            | Checks if the lastest state of a button on the keyboard matcheas th... |
| [CheckMouse(MouseButton...][6] | `bool`            | Checks if the lastest state of a mouse button matcheas the desired ... |
| [GetButton(Key)][7]            | [ButtonState][20] | Gets the latest state of a button the keyboard.                        |
| [GetButton(MouseButton)][7]    | [ButtonState][20] | Gets the latest state of a mouse button.                               |
| [HideSoftKeyboard()][8]        | `void`            | Hides the software keyboard.                                           |
| [RemoveInputSource(IInp...][9] | `void`            | Removes and tops tracking input from an input source.                  |
| [ShowSoftKeyboard()][10]       | `void`            | Shows the software keyboard.                                           |

[0]: ../../Heirloom.Core.md
[1]: Input/MouseDelta.md
[2]: Input/MousePosition.md
[3]: Input/SupportsSoftwareKeyboard.md
[4]: Input/AddInputSource.md
[5]: Input/CheckButton.md
[6]: Input/CheckMouse.md
[7]: Input/GetButton.md
[8]: Input/HideSoftKeyboard.md
[9]: Input/RemoveInputSource.md
[10]: Input/ShowSoftKeyboard.md
[11]: Input/CharacterTyped.md
[12]: Input/KeyPressed.md
[13]: Input/KeyReleased.md
[14]: Input/KeyRepeat.md
[15]: Input/MouseMoved.md
[16]: Input/MousePressed.md
[17]: Input/MouseReleased.md
[18]: Input/MouseScrolled.md
[19]: Vector.md
[20]: ButtonState.md
