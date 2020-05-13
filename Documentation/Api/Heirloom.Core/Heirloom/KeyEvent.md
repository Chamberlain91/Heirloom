# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## KeyEvent (Struct)

> **Namespace**: [Heirloom][0]

Contains the data of an event when a key has been pressed or released on some input source.

```cs
public struct KeyEvent
```

`IsReadOnlyAttribute`

### Fields

[Key][1], [Modifiers][2], [ScanCode][3], [State][4]

## Fields

#### Instance

| Name           | Type              | Summary                                                 |
|----------------|-------------------|---------------------------------------------------------|
| [Key][1]       | [Key][5]          | The standard Key enum associated with this event.       |
| [Modifiers][2] | [KeyModifiers][6] | The modifier keys pressed when the event was generated. |
| [ScanCode][3]  | `int`             | The scan code associated with some key.                 |
| [State][4]     | [ButtonState][7]  | The state of the key when the event was generated.      |

[0]: ../../Heirloom.Core.md
[1]: KeyEvent/Key.md
[2]: KeyEvent/Modifiers.md
[3]: KeyEvent/ScanCode.md
[4]: KeyEvent/State.md
[5]: Key.md
[6]: KeyModifiers.md
[7]: ButtonState.md
