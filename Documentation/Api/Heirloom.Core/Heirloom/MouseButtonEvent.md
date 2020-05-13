# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## MouseButtonEvent (Struct)

> **Namespace**: [Heirloom][0]

Contains the data of an event when a mouse button has been pressed or released on some input source.

```cs
public struct MouseButtonEvent
```

`IsReadOnlyAttribute`

### Fields

[Button][1], [Modifiers][2], [Position][3], [State][4]

## Fields

#### Instance

| Name           | Type              | Summary                                                     |
|----------------|-------------------|-------------------------------------------------------------|
| [Button][1]    | [MouseButton][5]  | The mouse button associated with the event.                 |
| [Modifiers][2] | [KeyModifiers][6] | The modifier keys pressed when the event was generated.     |
| [Position][3]  | [Vector][7]       | The position of the mouse when the event was generated.     |
| [State][4]     | [ButtonState][8]  | The state of the mouse button when the event was generated. |

[0]: ../../Heirloom.Core.md
[1]: MouseButtonEvent/Button.md
[2]: MouseButtonEvent/Modifiers.md
[3]: MouseButtonEvent/Position.md
[4]: MouseButtonEvent/State.md
[5]: MouseButton.md
[6]: KeyModifiers.md
[7]: Vector.md
[8]: ButtonState.md
