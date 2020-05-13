# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## TextDrawState (Struct)

> **Namespace**: [Heirloom][0]

Represents information of any particular glyph when drawing text.

```cs
public struct TextDrawState : IEquatable<TextDrawState>
```

### Inherits

IEquatable\<TextDrawState>

### Fields

[Color][1], [Position][2], [Transform][3]

### Methods

[Equals][4], [GetHashCode][5]

## Fields

#### Instance

| Name           | Type        | Summary                                                                |
|----------------|-------------|------------------------------------------------------------------------|
| [Color][1]     | [Color][6]  | The color of the current glyph.                                        |
| [Position][2]  | [Vector][7] | The position of top left corner of the current glyph image.            |
| [Transform][3] | [Matrix][8] | The relative transform to apply to the current glyph image (set to ... |

## Methods

#### Instance

| Name                       | Return Type | Summary                                                               |
|----------------------------|-------------|-----------------------------------------------------------------------|
| [Equals(object)][4]        | `bool`      | Compares this TextDrawState for equality with another object.         |
| [Equals(TextDrawState)][4] | `bool`      | Compares this TextDrawState for equality with another TextDrawState . |
| [GetHashCode()][5]         | `int`       | Returns the hash code for this TextDrawState .                        |

## Operators

| Name                            | Return Type | Summary                                                 |
|---------------------------------|-------------|---------------------------------------------------------|
| [Equality(TextDrawState...][9]  | `bool`      | Compares two instances of TextDrawState for equality.   |
| [Inequality(TextDrawSta...][10] | `bool`      | Compares two instances of TextDrawState for inequality. |

[0]: ../../Heirloom.Core.md
[1]: TextDrawState/Color.md
[2]: TextDrawState/Position.md
[3]: TextDrawState/Transform.md
[4]: TextDrawState/Equals.md
[5]: TextDrawState/GetHashCode.md
[6]: Color.md
[7]: Vector.md
[8]: Matrix.md
[9]: TextDrawState/op_Equality.md
[10]: TextDrawState/op_Inequality.md
