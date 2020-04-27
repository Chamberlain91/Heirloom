# GlyphMetrics

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  
> **Namespace**: [Heirloom][0]  

Contains information about a glyph (ie, the horizontal metrics).

```cs
public struct GlyphMetrics
```

--------------------------------------------------------------------------------

**Fields**: [AdvanceWidth][1], [Bearing][2]

**Properties**: [Offset][3], [Size][4]

--------------------------------------------------------------------------------

## Fields

| Name              | Summary                                                                                               |
|-------------------|-------------------------------------------------------------------------------------------------------|
| [AdvanceWidth][1] | The advance width of the glyph. This is the spacing between the glyph's left edge and the next glyph. |
| [Bearing][2]      | The bearing of this glyph.                                                                            |

## Properties

| Name        | Summary                                 |
|-------------|-----------------------------------------|
| [Offset][3] | The glyph offset from the pen position. |
| [Size][4]   | The glyph bounds size.                  |

[0]: ..\Heirloom.Core.md
[1]: Heirloom.GlyphMetrics.AdvanceWidth.md
[2]: Heirloom.GlyphMetrics.Bearing.md
[3]: Heirloom.GlyphMetrics.Offset.md
[4]: Heirloom.GlyphMetrics.Size.md
