# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## IntSize (Struct)

> **Namespace**: [Heirloom][0]

Represents two dimensional size by a measure in each axis.

```cs
public struct IntSize : IEquatable<IntSize>
```

### Inherits

IEquatable\<IntSize>

### Fields

[Height][1], [Width][2]

### Properties

[Area][3], [Aspect][4], [Indexer][5]

### Methods

[Deconstruct][6], [Equals][7], [GetHashCode][8], [Set][9], [ToString][10]

### Static Fields

[Max][11], [One][12], [Zero][13]

## Fields

#### Instance

| Name        | Type  | Summary                              |
|-------------|-------|--------------------------------------|
| [Height][1] | `int` | The height (vertical size measure).  |
| [Width][2]  | `int` | The width (horizontal size measure). |

#### Static

| Name       | Type          | Summary                                  |
|------------|---------------|------------------------------------------|
| [Max][11]  | [IntSize][14] | The maximum representable size possible. |
| [One][12]  | [IntSize][14] | A 1x1 size.                              |
| [Zero][13] | [IntSize][14] | A 0x0 size.                              |

## Properties

#### Instance

| Name         | Type    | Summary                                                            |
|--------------|---------|--------------------------------------------------------------------|
| [Area][3]    | `int`   | Gets the area of this size as if it was a rectangle at the origin. |
| [Aspect][4]  | `float` | Gets the aspect ratio of this size.                                |
| [Indexer][5] | `int`   |                                                                    |

## Methods

#### Instance

| Name                           | Return Type | Summary                                                   |
|--------------------------------|-------------|-----------------------------------------------------------|
| [Deconstruct(out int, o...][6] | `void`      | Deconstructs this IntSize int constituent components.     |
| [Equals(object)][7]            | `bool`      | Compares this IntSize for equality with another object.   |
| [Equals(IntSize)][7]           | `bool`      | Compares this IntSize for equality with another IntSize . |
| [GetHashCode()][8]             | `int`       | Returns the hash code for this IntSize .                  |
| [Set(int, int)][9]             | `void`      | Sets the components of this size.                         |
| [ToString()][10]               | `string`    | Returns the string representation of this IntSize .       |

## Operators

| Name                            | Return Type     | Summary                                                               |
|---------------------------------|-----------------|-----------------------------------------------------------------------|
| [Addition(IntSize, IntS...][15] | [IntSize][14]   | Performs the addition of two size structures.                         |
| [Division(IntSize, IntS...][16] | [IntSize][14]   | Performs the component-wise division of two size structures.          |
| [Division(IntSize, int)][16]    | [IntSize][14]   | Performs the component-wise scaling of a size structure via division. |
| [Division(IntSize, float)][16]  | [Size][17]      | Performs the component-wise scaling of a size structure via division. |
| [Division(int, IntSize)][16]    | [IntSize][14]   | Performs the component-wise scaling of a size structure via division. |
| [Division(float, IntSize)][16]  | [Size][17]      | Performs the component-wise scaling of a size structure via division. |
| [Equality(IntSize, IntS...][18] | `bool`          | Compares two instances of IntSize for equality.                       |
| [Explicit(IntSize)][19]         | [IntVector][20] |                                                                       |
| [Explicit(IntSize)][19]         | [Vector][21]    |                                                                       |
| [Implicit(IntSize)][22]         | [Size][17]      |                                                                       |
| [Implicit(ValueTuple<in...][22] | [IntSize][14]   |                                                                       |
| [Inequality(IntSize, In...][23] | `bool`          | Compares two instances of IntSize for inequality.                     |
| [Multiply(IntSize, IntS...][24] | [IntSize][14]   | Performs the component-wise multiplication of two size structures.    |
| [Multiply(IntSize, float)][24]  | [Size][17]      | Performs the component-wise scaling of a size structure.              |
| [Multiply(IntSize, int)][24]    | [IntSize][14]   | Performs the component-wise scaling of a size structure.              |
| [Multiply(int, IntSize)][24]    | [IntSize][14]   | Performs the component-wise scaling of a size structure.              |
| [Multiply(float, IntSize)][24]  | [Size][17]      | Performs the component-wise scaling of a size structure.              |
| [Subtraction(IntSize, I...][25] | [IntSize][14]   | Performs the subtraction of two size structures.                      |
| [UnaryNegation(IntSize)][26]    | [IntSize][14]   | Returns the negated version of a size structure.                      |

[0]: ../../Heirloom.Core.md
[1]: IntSize/Height.md
[2]: IntSize/Width.md
[3]: IntSize/Area.md
[4]: IntSize/Aspect.md
[5]: IntSize/Indexer.md
[6]: IntSize/Deconstruct.md
[7]: IntSize/Equals.md
[8]: IntSize/GetHashCode.md
[9]: IntSize/Set.md
[10]: IntSize/ToString.md
[11]: IntSize/Max.md
[12]: IntSize/One.md
[13]: IntSize/Zero.md
[14]: IntSize.md
[15]: IntSize/op_Addition.md
[16]: IntSize/op_Division.md
[17]: Size.md
[18]: IntSize/op_Equality.md
[19]: IntSize/op_Explicit.md
[20]: IntVector.md
[21]: Vector.md
[22]: IntSize/op_Implicit.md
[23]: IntSize/op_Inequality.md
[24]: IntSize/op_Multiply.md
[25]: IntSize/op_Subtraction.md
[26]: IntSize/op_UnaryNegation.md
