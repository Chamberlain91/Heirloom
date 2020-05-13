# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## ButtonState (Enum)

> **Namespace**: [Heirloom][0]

Represents the state of a button.

```cs
public enum ButtonState : IComparable, IFormattable, IConvertible
```

`FlagsAttribute`

| Name     | Summary                          |
|----------|----------------------------------|
| Down     | Button is held.                  |
| Now      | Button state changed this frame. |
| Pressed  | Button was pressed this frame.   |
| Released | Button was pressed this frame.   |
| Up       | Button is released.              |

[0]: ../../Heirloom.Core.md
