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

### Methods

[Equals][4], [GetHashCode][5]

## Fields

#### Instance

| Name          | Type        | Summary                                                     |
|---------------|-------------|-------------------------------------------------------------|
| [Position][1] | [Vector][6] | The position of top left corner of the current glyph image. |

## Properties

#### Instance

| Name           | Type                  | Summary                                  |
|----------------|-----------------------|------------------------------------------|
| [Character][2] | [UnicodeCharacter][7] | The current character.                   |
| [Metrics][3]   | [GlyphMetrics][8]     | The metrics of the glyph being rendered. |

## Methods

#### Instance

| Name                         | Return Type | Summary                                                                |
|------------------------------|-------------|------------------------------------------------------------------------|
| [Equals(object)][4]          | `bool`      | Compares this TextLayoutState for equality with another object.        |
| [Equals(TextLayoutState)][4] | `bool`      | Compares this TextLayoutState for equality with another TextLayoutS... |
| [GetHashCode()][5]           | `int`       | Returns the hash code for this TextLayoutState .                       |

## Operators

| Name                            | Return Type | Summary                                                   |
|---------------------------------|-------------|-----------------------------------------------------------|
| [Equality(TextLayoutSta...][9]  | `bool`      | Compares two instances of TextLayoutState for equality.   |
| [Inequality(TextLayoutS...][10] | `bool`      | Compares two instances of TextLayoutState for inequality. |

[0]: ../../Heirloom.Core.md
[1]: TextLayoutState/Position.md
[2]: TextLayoutState/Character.md
[3]: TextLayoutState/Metrics.md
[4]: TextLayoutState/Equals.md
[5]: TextLayoutState/GetHashCode.md
[6]: Vector.md
[7]: UnicodeCharacter.md
[8]: GlyphMetrics.md
[9]: TextLayoutState/op_Equality.md
[10]: TextLayoutState/op_Inequality.md
