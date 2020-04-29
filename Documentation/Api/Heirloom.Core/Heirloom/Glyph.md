# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Glyph (Class)

> **Namespace**: [Heirloom][0]

A glyph represents the metrics and rendering of a character from the associated `!:Drawing.Font` .

```cs
public class Glyph
```

### Properties

[CanBeRendered][1], [Character][2], [Font][3]

### Methods

[GetMetrics][4], [RenderTo][5]

## Properties

#### Instance

| Name               | Type                  | Summary                                                    |
|--------------------|-----------------------|------------------------------------------------------------|
| [CanBeRendered][1] | `bool`                | Get a value that determines if this glyph can be rendered. |
| [Character][2]     | [UnicodeCharacter][6] | Gets the character this glyph represents.                  |
| [Font][3]          | [Font][7]             | Gets the associated font.                                  |

## Methods

#### Instance

| Name                           | Return Type       | Summary                                                             |
|--------------------------------|-------------------|---------------------------------------------------------------------|
| [GetMetrics(float)][4]         | [GlyphMetrics][8] | Get the horizontal metrics of the this glyph at the specified size. |
| [RenderTo(Image, int, i...][5] | `void`            | Renders the glyph into the image.                                   |

[0]: ../../Heirloom.Core.md
[1]: Glyph/CanBeRendered.md
[2]: Glyph/Character.md
[3]: Glyph/Font.md
[4]: Glyph/GetMetrics.md
[5]: Glyph/RenderTo.md
[6]: UnicodeCharacter.md
[7]: Font.md
[8]: GlyphMetrics.md
