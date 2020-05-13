# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Size (Struct)

> **Namespace**: [Heirloom][0]

Represents two dimensional size by a measure in each axis.

```cs
public struct Size : IEquatable<Size>
```

### Inherits

IEquatable\<Size>

### Fields

[Height][1], [Width][2]

### Properties

[Area][3], [Aspect][4], [Indexer][5]

### Methods

[Deconstruct][6], [Equals][7], [GetHashCode][8], [Set][9], [ToString][10]

### Static Fields

[Infinite][11], [Max][12], [One][13], [Zero][14]

## Fields

#### Instance

| Name        | Type    | Summary                              |
|-------------|---------|--------------------------------------|
| [Height][1] | `float` | The height (vertical size measure).  |
| [Width][2]  | `float` | The width (horizontal size measure). |

#### Static

| Name           | Type       | Summary                                  |
|----------------|------------|------------------------------------------|
| [Infinite][11] | [Size][15] | An infinite size.                        |
| [Max][12]      | [Size][15] | The maximum representable size possible. |
| [One][13]      | [Size][15] | A 1x1 size.                              |
| [Zero][14]     | [Size][15] | A 0x0 size.                              |

## Properties

#### Instance

| Name         | Type    | Summary                                                            |
|--------------|---------|--------------------------------------------------------------------|
| [Area][3]    | `float` | Gets the area of this size as if it was a rectangle at the origin. |
| [Aspect][4]  | `float` | Gets the aspect ratio of this size.                                |
| [Indexer][5] | `float` |                                                                    |

## Methods

#### Instance

| Name                           | Return Type | Summary                                                 |
|--------------------------------|-------------|---------------------------------------------------------|
| [Deconstruct(out float,...][6] | `void`      | Deconstructs this Size int constituent components.      |
| [Equals(object)][7]            | `bool`      | Compares this IntSize for equality with another object. |
| [Equals(Size)][7]              | `bool`      | Compares this Size for equality with another Size .     |
| [GetHashCode()][8]             | `int`       | Returns the hash code for this Size .                   |
| [Set(float, float)][9]         | `void`      | Sets the components of this size.                       |
| [ToString()][10]               | `string`    | Returns the string representation of this Size .        |

## Operators

| Name                            | Return Type     | Summary                                                               |
|---------------------------------|-----------------|-----------------------------------------------------------------------|
| [Addition(Size, Size)][16]      | [Size][15]      | Performs the addition of two size structures.                         |
| [Division(Size, Size)][17]      | [Size][15]      | Performs the component-wise division of two size structures.          |
| [Division(Size, float)][17]     | [Size][15]      | Performs the component-wise scaling of a size structure via division. |
| [Division(float, Size)][17]     | [Size][15]      | Performs the component-wise scaling of a size structure via division. |
| [Equality(Size, Size)][18]      | `bool`          | Compares two instances of Size for equality.                          |
| [Explicit(Size)][19]            | [Vector][20]    |                                                                       |
| [Explicit(Size)][19]            | [IntVector][21] |                                                                       |
| [Explicit(Size)][19]            | [IntSize][22]   |                                                                       |
| [Implicit(ValueTuple<fl...][23] | [Size][15]      |                                                                       |
| [Inequality(Size, Size)][24]    | `bool`          | Compares two instances of Size for inequality.                        |
| [Multiply(Size, Size)][25]      | [Size][15]      | Performs the component-wise multiplication of two size structures.    |
| [Multiply(Size, float)][25]     | [Size][15]      | Performs the component-wise scaling of a size structure.              |
| [Multiply(float, Size)][25]     | [Size][15]      | Performs the component-wise scaling of a size structure.              |
| [Subtraction(Size, Size)][26]   | [Size][15]      | Performs the subtraction of two size structures.                      |
| [UnaryNegation(Size)][27]       | [Size][15]      | Returns the negated version of a size structure.                      |

[0]: ../../Heirloom.Core.md
[1]: Size/Height.md
[2]: Size/Width.md
[3]: Size/Area.md
[4]: Size/Aspect.md
[5]: Size/Indexer.md
[6]: Size/Deconstruct.md
[7]: Size/Equals.md
[8]: Size/GetHashCode.md
[9]: Size/Set.md
[10]: Size/ToString.md
[11]: Size/Infinite.md
[12]: Size/Max.md
[13]: Size/One.md
[14]: Size/Zero.md
[15]: Size.md
[16]: Size/op_Addition.md
[17]: Size/op_Division.md
[18]: Size/op_Equality.md
[19]: Size/op_Explicit.md
[20]: Vector.md
[21]: IntVector.md
[22]: IntSize.md
[23]: Size/op_Implicit.md
[24]: Size/op_Inequality.md
[25]: Size/op_Multiply.md
[26]: Size/op_Subtraction.md
[27]: Size/op_UnaryNegation.md
