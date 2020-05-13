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

### Methods

[Equals][5], [GetHashCode][6]

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
| [Offset][3] | [IntVector][7] | The glyph offset from the pen position. |
| [Size][4]   | [IntSize][8]   | The glyph bounds size.                  |

## Methods

#### Instance

| Name                | Return Type | Summary                                                      |
|---------------------|-------------|--------------------------------------------------------------|
| [Equals(object)][5] | `bool`      | Compares this GlyphMetrics for equality with another object. |
| [GetHashCode()][6]  | `int`       | Returns the hash code this GlyphMetrics .                    |

## Operators

| Name                            | Return Type | Summary                                                |
|---------------------------------|-------------|--------------------------------------------------------|
| [Equality(GlyphMetrics,...][9]  | `bool`      | Compares two instances of GlyphMetrics for equality.   |
| [Inequality(GlyphMetric...][10] | `bool`      | Compares two instances of GlyphMetrics for inequality. |

[0]: ../../Heirloom.Core.md
[1]: GlyphMetrics/AdvanceWidth.md
[2]: GlyphMetrics/Bearing.md
[3]: GlyphMetrics/Offset.md
[4]: GlyphMetrics/Size.md
[5]: GlyphMetrics/Equals.md
[6]: GlyphMetrics/GetHashCode.md
[7]: IntVector.md
[8]: IntSize.md
[9]: GlyphMetrics/op_Equality.md
[10]: GlyphMetrics/op_Inequality.md
