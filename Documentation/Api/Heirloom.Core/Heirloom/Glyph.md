# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Glyph (Class)

> **Namespace**: [Heirloom][0]

A glyph represents the metrics and rendering of a character from the associated [Font][1] .

```cs
public sealed class Glyph
```

### Properties

[CanBeRendered][2], [Character][3], [Font][1]

### Methods

[GetMetrics][4], [RenderGlyph][5], [RenderTo][6]

## Properties

#### Instance

| Name               | Type                  | Summary                                                    |
|--------------------|-----------------------|------------------------------------------------------------|
| [CanBeRendered][2] | `bool`                | Get a value that determines if this glyph can be rendered. |
| [Character][3]     | [UnicodeCharacter][7] | Gets the character this glyph represents.                  |
| [Font][1]          | [Font][8]             | Gets the associated font.                                  |

## Methods

#### Instance

| Name                           | Return Type       | Summary                                                             |
|--------------------------------|-------------------|---------------------------------------------------------------------|
| [GetMetrics(float)][4]         | [GlyphMetrics][9] | Get the horizontal metrics of the this glyph at the specified size. |
| [RenderGlyph(float)][5]        | [Image][10]       | Renders the glyph into an image.                                    |
| [RenderTo(Image, int, i...][6] | `void`            | Renders the glyph into an image.                                    |

[0]: ../../Heirloom.Core.md
[1]: Glyph/Font.md
[2]: Glyph/CanBeRendered.md
[3]: Glyph/Character.md
[4]: Glyph/GetMetrics.md
[5]: Glyph/RenderGlyph.md
[6]: Glyph/RenderTo.md
[7]: UnicodeCharacter.md
[8]: Font.md
[9]: GlyphMetrics.md
[10]: Image.md
