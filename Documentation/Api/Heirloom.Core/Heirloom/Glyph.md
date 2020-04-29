# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  

## Glyph Class

> **Namespace**: [Heirloom][0]  

A glyph represents the metrics and rendering of a character from the associated `!:Drawing.Font` .

```cs
public class Glyph
```

#### Properties

[Font][1], [Character][2], [CanBeRendered][3]

#### Methods

[GetMetrics][4], [RenderTo][5]

## Properties

| Name               | Summary                                                    |
|--------------------|------------------------------------------------------------|
| [Font][1]          | Gets the associated font.                                  |
| [Character][2]     | Gets the character this glyph represents.                  |
| [CanBeRendered][3] | Get a value that determines if this glyph can be rendered. |

## Methods

| Name            | Summary                                                             |
|-----------------|---------------------------------------------------------------------|
| [GetMetrics][4] | Get the horizontal metrics of the this glyph at the specified size. |
| [RenderTo][5]   | Renders the glyph into the image.                                   |

[0]: ../../Heirloom.Core.md
[1]: Glyph/Font.md
[2]: Glyph/Character.md
[3]: Glyph/CanBeRendered.md
[4]: Glyph/GetMetrics.md
[5]: Glyph/RenderTo.md
