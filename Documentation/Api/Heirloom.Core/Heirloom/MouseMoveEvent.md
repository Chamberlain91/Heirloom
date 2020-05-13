# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## MouseMoveEvent (Struct)

> **Namespace**: [Heirloom][0]

Contains the data of an event when the mouse has been moved on some input source.

```cs
public struct MouseMoveEvent
```

`IsReadOnlyAttribute`

### Fields

[Delta][1], [Position][2]

## Fields

#### Instance

| Name          | Type        | Summary                                                               |
|---------------|-------------|-----------------------------------------------------------------------|
| [Delta][1]    | [Vector][3] | The difference in position of the mouse when the event was generated. |
| [Position][2] | [Vector][3] | The position of the mouse when the event was generated.               |

[0]: ../../Heirloom.Core.md
[1]: MouseMoveEvent/Delta.md
[2]: MouseMoveEvent/Position.md
[3]: Vector.md
