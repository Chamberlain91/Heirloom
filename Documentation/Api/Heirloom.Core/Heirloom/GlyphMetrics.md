# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## GlyphMetrics (Struct)

> **Namespace**: [Heirloom][0]

Contains information about a glyph (ie, the horizontal metrics).

```cs
public struct GlyphMetrics
```

`IsReadOnlyAttribute`

### Fields

[AdvanceWidth][1], [Bearing][2]

### Properties

[Offset][3], [Size][4]

## Fields

#### Instance

| Name              | Type    | Summary                                                                |
|-------------------|---------|------------------------------------------------------------------------|
| [AdvanceWidth][1] | `float` | The advance width of the glyph. This is the spacing between the gly... |
| [Bearing][2]      | `float` | The bearing of this glyph.                                             |

## Properties

#### Instance

| Name        | Type           | Summary                                 |
|-------------|----------------|-----------------------------------------|
| [Offset][3] | [IntVector][5] | The glyph offset from the pen position. |
| [Size][4]   | [IntSize][6]   | The glyph bounds size.                  |

[0]: ../../Heirloom.Core.md
[1]: GlyphMetrics/AdvanceWidth.md
[2]: GlyphMetrics/Bearing.md
[3]: GlyphMetrics/Offset.md
[4]: GlyphMetrics/Size.md
[5]: IntVector.md
[6]: IntSize.md
