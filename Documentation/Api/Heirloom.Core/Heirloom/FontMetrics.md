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

### Methods

[Equals][6], [GetHashCode][7]

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

## Methods

#### Instance

| Name                | Return Type | Summary                                                     |
|---------------------|-------------|-------------------------------------------------------------|
| [Equals(object)][6] | `bool`      | Compares this FontMetrics for equality with another object. |
| [GetHashCode()][7]  | `int`       | Returns the hash code for this FontMetrics .                |

## Operators

| Name                           | Return Type | Summary                                               |
|--------------------------------|-------------|-------------------------------------------------------|
| [Equality(FontMetrics, ...][8] | `bool`      | Compares two instances of FontMetrics for equality.   |
| [Inequality(FontMetrics...][9] | `bool`      | Compares two instances of FontMetrics for inequality. |

[0]: ../../Heirloom.Core.md
[1]: FontMetrics/Ascent.md
[2]: FontMetrics/Descent.md
[3]: FontMetrics/LineGap.md
[4]: FontMetrics/Height.md
[5]: FontMetrics/LineAdvance.md
[6]: FontMetrics/Equals.md
[7]: FontMetrics/GetHashCode.md
[8]: FontMetrics/op_Equality.md
[9]: FontMetrics/op_Inequality.md
