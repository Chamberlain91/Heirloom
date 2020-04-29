# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## TextDrawState (Struct)

> **Namespace**: [Heirloom][0]

Represents information of any particular glyph when drawing text.

```cs
public struct TextDrawState : IEquatable<TextDrawState>
```

### Inherits

IEquatable\<TextDrawState>

### Fields

[Color][1], [Position][2], [Transform][3]

## Fields

#### Instance

| Name           | Type        | Summary                                                                |
|----------------|-------------|------------------------------------------------------------------------|
| [Color][1]     | [Color][4]  | The color of the current glyph.                                        |
| [Position][2]  | [Vector][5] | The position of top left corner of the current glyph image.            |
| [Transform][3] | [Matrix][6] | The relative transform to apply to the current glyph image (set to ... |

[0]: ../../Heirloom.Core.md
[1]: TextDrawState/Color.md
[2]: TextDrawState/Position.md
[3]: TextDrawState/Transform.md
[4]: Color.md
[5]: Vector.md
[6]: Matrix.md
