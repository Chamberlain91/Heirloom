# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## FontMetrics (Struct)

> **Namespace**: [Heirloom][0]

Contains information about a font (ie, the vertical metrics).

```cs
public struct FontMetrics
```

`IsReadOnlyAttribute`

### Fields

[Ascent][1], [Descent][2], [LineGap][3]

### Properties

[Height][4], [LineAdvance][5]

## Fields

#### Instance

| Name         | Type    | Summary                                                   |
|--------------|---------|-----------------------------------------------------------|
| [Ascent][1]  | `float` | The measure from the baseline to the top of any glyphs.   |
| [Descent][2] | `float` | The measure from the baseline to the bottom of any glyph. |
| [LineGap][3] | `float` | The spacing between lines.                                |

## Properties

#### Instance

| Name             | Type    | Summary                                          |
|------------------|---------|--------------------------------------------------|
| [Height][4]      | `float` | The height of the line.                          |
| [LineAdvance][5] | `float` | The spacing between baselines of multiline text. |

[0]: ../../Heirloom.Core.md
[1]: FontMetrics/Ascent.md
[2]: FontMetrics/Descent.md
[3]: FontMetrics/LineGap.md
[4]: FontMetrics/Height.md
[5]: FontMetrics/LineAdvance.md
