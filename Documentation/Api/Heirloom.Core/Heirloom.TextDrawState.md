# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  

## TextDrawState

> **Namespace**: [Heirloom][0]  

Represents information of any particular glyph when drawing text.

```cs
public struct TextDrawState : IEquatable<TextDrawState>
```

### Inherits

IEquatable\<TextDrawState>

#### Fields

[Transform][1], [Position][2], [Color][3]

## Fields

| Name           | Summary                                                                                              |
|----------------|------------------------------------------------------------------------------------------------------|
| [Transform][1] | The relative transform to apply to the current glyph image (set to [Matrix.Identity][4] by default). |
| [Position][2]  | The position of top left corner of the current glyph image.                                          |
| [Color][3]     | The color of the current glyph.                                                                      |

[0]: ../Heirloom.Core.md
[1]: Heirloom.TextDrawState.Transform.md
[2]: Heirloom.TextDrawState.Position.md
[3]: Heirloom.TextDrawState.Color.md
[4]: Heirloom.Matrix.Identity.md
