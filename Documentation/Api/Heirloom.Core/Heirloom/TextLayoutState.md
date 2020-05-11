# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## TextLayoutState (Struct)

> **Namespace**: [Heirloom][0]

Represents information of any particular glyph during text layout.

```cs
public struct TextLayoutState : IEquatable<TextLayoutState>
```

### Inherits

IEquatable\<TextLayoutState>

### Fields

[Position][1]

### Properties

[Character][2], [Metrics][3]

## Fields

#### Instance

| Name          | Type        | Summary                                                     |
|---------------|-------------|-------------------------------------------------------------|
| [Position][1] | [Vector][4] | The position of top left corner of the current glyph image. |

## Properties

#### Instance

| Name           | Type                  | Summary                                  |
|----------------|-----------------------|------------------------------------------|
| [Character][2] | [UnicodeCharacter][5] | The current character.                   |
| [Metrics][3]   | [GlyphMetrics][6]     | The metrics of the glyph being rendered. |

[0]: ../../Heirloom.Core.md
[1]: TextLayoutState/Position.md
[2]: TextLayoutState/Character.md
[3]: TextLayoutState/Metrics.md
[4]: Vector.md
[5]: UnicodeCharacter.md
[6]: GlyphMetrics.md
