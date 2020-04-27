# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  

## TextLayoutState

> **Namespace**: [Heirloom][0]  

Represents information of any particular glyph during text layout.

```cs
public struct TextLayoutState : IEquatable<TextLayoutState>
```

### Inherits

IEquatable\<TextLayoutState>

#### Fields

[Position][1]

#### Properties

[Character][2], [Metrics][3]

## Fields

| Name          | Summary                                                     |
|---------------|-------------------------------------------------------------|
| [Position][1] | The position of top left corner of the current glyph image. |

## Properties

| Name           | Summary                                  |
|----------------|------------------------------------------|
| [Character][2] | The current character.                   |
| [Metrics][3]   | The metrics of the glyph being rendered. |

[0]: ../Heirloom.Core.md
[1]: Heirloom.TextLayoutState.Position.md
[2]: Heirloom.TextLayoutState.Character.md
[3]: Heirloom.TextLayoutState.Metrics.md
