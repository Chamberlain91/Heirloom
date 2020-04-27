# Glyph

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  
> **Namespace**: [Heirloom][0]  

A glyph represents the metrics and rendering of a character from the associated `!:Drawing.Font` .

```cs
public class Glyph
```

--------------------------------------------------------------------------------

**Properties**: [Font][1], [Character][2], [CanBeRendered][3]

**Methods**: [GetMetrics][4], [RenderTo][5]

--------------------------------------------------------------------------------

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

[0]: ..\Heirloom.Core.md
[1]: Heirloom.Glyph.Font.md
[2]: Heirloom.Glyph.Character.md
[3]: Heirloom.Glyph.CanBeRendered.md
[4]: Heirloom.Glyph.GetMetrics.md
[5]: Heirloom.Glyph.RenderTo.md
