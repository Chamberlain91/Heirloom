# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## UnicodeCharacter (Struct)

> **Namespace**: [Heirloom][0]

Represents a single 32 bit Unicode character.

```cs
public struct UnicodeCharacter : IComparable<UnicodeCharacter>, IEquatable<UnicodeCharacter>
```

`IsReadOnlyAttribute`

### Inherits

IComparable\<UnicodeCharacter>, IEquatable\<UnicodeCharacter>

### Methods

[CompareTo][1], [Equals][2], [GetHashCode][3], [ToString][4]

## Methods

#### Instance

| Name                           | Return Type | Summary                                                                |
|--------------------------------|-------------|------------------------------------------------------------------------|
| [CompareTo(UnicodeChara...][1] | `int`       | Compares this instance to the specified value.                         |
| [Equals(UnicodeCharacter)][2]  | `bool`      | Compares this unicode character for equality with another unicode c... |
| [Equals(object)][2]            | `bool`      | Compares this unicode character for equality with another object.      |
| [GetHashCode()][3]             | `int`       | Returns the hash code for this UnicodeCharacter .                      |
| [ToString()][4]                | `string`    | Returns the string representation of this UnicodeCharacter .           |

## Operators

| Name                            | Return Type           | Summary                                           |
|---------------------------------|-----------------------|---------------------------------------------------|
| [Equality(UnicodeCharac...][5]  | `bool`                | Implements the equality operator.                 |
| [Explicit(int)][6]              | [UnicodeCharacter][7] |                                                   |
| [Explicit(UnicodeCharac...][6]  | `char`                |                                                   |
| [Explicit(UnicodeCharac...][6]  | `string`              |                                                   |
| [GreaterThan(UnicodeCha...][8]  | `bool`                | Implements the greater-than operator.             |
| [GreaterThanOrEqual(Uni...][9]  | `bool`                | Implements the greater-than-or-equal-to operator. |
| [Implicit(char)][10]            | [UnicodeCharacter][7] |                                                   |
| [Inequality(UnicodeChar...][11] | `bool`                | Implements the inequality operator.               |
| [LessThan(UnicodeCharac...][12] | `bool`                | Implements the less-than operator.                |
| [LessThanOrEqual(Unicod...][13] | `bool`                | Implements the less-than-or-equal-to operator.    |

[0]: ../../Heirloom.Core.md
[1]: UnicodeCharacter/CompareTo.md
[2]: UnicodeCharacter/Equals.md
[3]: UnicodeCharacter/GetHashCode.md
[4]: UnicodeCharacter/ToString.md
[5]: UnicodeCharacter/op_Equality.md
[6]: UnicodeCharacter/op_Explicit.md
[7]: UnicodeCharacter.md
[8]: UnicodeCharacter/op_GreaterThan.md
[9]: UnicodeCharacter/op_GreaterThanOrEqual.md
[10]: UnicodeCharacter/op_Implicit.md
[11]: UnicodeCharacter/op_Inequality.md
[12]: UnicodeCharacter/op_LessThan.md
[13]: UnicodeCharacter/op_LessThanOrEqual.md
