# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Vector (Struct)

> **Namespace**: [Heirloom][0]

Represents a vector with two single-precision floating-point values.

```cs
public struct Vector : IEquatable<Vector>
```

### Inherits

IEquatable\<Vector>

### Fields

[X][1], [Y][2]

### Properties

[Angle][3], [Indexer][4], [Length][5], [LengthSquared][6], [Normalized][7], [Perpendicular][8]

### Methods

[Deconstruct][9], [Equals][10], [GetHashCode][11], [Normalize][12], [Set][13], [ToString][14]

### Static Fields

[Down][15], [Left][16], [One][17], [Right][18], [Up][19], [Zero][20]

### Static Methods

[Abs][21], [AngleBetween][22], [Ceil][23], [Cross][24], [Distance][25], [DistanceSquared][26], [Dot][27], [Floor][28], [Fraction][29], [FromAngle][30], [GetMaxComponent][31], [GetMinComponent][32], [Lerp][33], [ManhattanDistance][34], [Max][35], [Min][36], [Normalize][12], [Project][37], [Reflect][38], [Rotate][39], [Round][40]

## Fields

#### Instance

| Name   | Type    | Summary                          |
|--------|---------|----------------------------------|
| [X][1] | `float` | The x-coordinate of this vector. |
| [Y][2] | `float` | The y-coordinate of this vector. |

#### Static

| Name        | Type         | Summary                      |
|-------------|--------------|------------------------------|
| [Down][15]  | [Vector][41] | A vector with value (0, 1).  |
| [Left][16]  | [Vector][41] | A vector with value (-1, 0). |
| [One][17]   | [Vector][41] | A vector with value (1, 1).  |
| [Right][18] | [Vector][41] | A vector with value (1, 0).  |
| [Up][19]    | [Vector][41] | A vector with value (0, -1). |
| [Zero][20]  | [Vector][41] | A vector with value (0, 0).  |

## Properties

#### Instance

| Name               | Type         | Summary                                                          |
|--------------------|--------------|------------------------------------------------------------------|
| [Angle][3]         | `float`      | Gets the angle this vector is pointing with reference to Right . |
| [Indexer][4]       | `float`      |                                                                  |
| [Length][5]        | `float`      | Gets the magnitude of this vector.                               |
| [LengthSquared][6] | `float`      | Gets the squared magnitude of this vector.                       |
| [Normalized][7]    | [Vector][41] | Gets a normalized copy of this vector.                           |
| [Perpendicular][8] | [Vector][41] | Gets a perpendicular copy of this vector.                        |

## Methods

#### Instance

| Name                           | Return Type | Summary                             |
|--------------------------------|-------------|-------------------------------------|
| [Deconstruct(out float,...][9] | `void`      |                                     |
| [Equals(object)][10]           | `bool`      |                                     |
| [Equals(Vector)][10]           | `bool`      |                                     |
| [GetHashCode()][11]            | `int`       |                                     |
| [Normalize()][12]              | `void`      | Normalize this vector.              |
| [Set(float, float)][13]        | `void`      | Sets the components of this vector. |
| [ToString()][14]               | `string`    |                                     |

#### Static

| Name                            | Return Type  | Summary                                                                |
|---------------------------------|--------------|------------------------------------------------------------------------|
| [Abs(Vector)][21]               | [Vector][41] | Computes a new vector where each component is the absolute value of... |
| [AngleBetween(Vector, V...][22] | `float`      | Computes the angle (in radians) between two vectors (using dot prod... |
| [Ceil(Vector)][23]              | [Vector][41] | Computes a new vector with the ceiling of each component of the inp... |
| [Cross(in Vector, in Ve...][24] | `float`      | Computes the cross-product of two vectors.                             |
| [Cross(in Vector, float)][24]   | [Vector][41] | Computes the cross-product of a vector and a magnitude.                |
| [Distance(in Vector, in...][25] | `float`      | Computes the euclidean distance between any two vectors.               |
| [DistanceSquared(in Vec...][26] | `float`      | Computes the squared euclidean distance between any two vectors.       |
| [Dot(in Vector, in Vector)][27] | `float`      | Computes the dot-product of two vectors.                               |
| [Floor(Vector)][28]             | [Vector][41] | Computes a new vector with the floor of each component of the input... |
| [Fraction(Vector)][29]          | [Vector][41] | Computes a new vector with the fractional portion of each component... |
| [FromAngle(float)][30]          | [Vector][41] | Creates a unit length vector with the given angle from the x-axis.     |
| [GetMaxComponent(Vector)][31]   | `float`      | Gets the maximal component in the input vector.                        |
| [GetMinComponent(Vector)][32]   | `float`      | Gets the minimal component in the input vector.                        |
| [Lerp(Vector, Vector, f...][33] | [Vector][41] | Interpolate two vectors.                                               |
| [ManhattanDistance(in V...][34] | `float`      | Computes the manhattan distance between any two vectors.               |
| [Max(Vector, Vector)][35]       | [Vector][41] | Computes a new vector where each component is the maximum component... |
| [Min(Vector, Vector)][36]       | [Vector][41] | Computes a new vector where each component is the minimum component... |
| [Normalize(Vector)][12]         | [Vector][41] | Normalizes the input vector and return it.                             |
| [Normalize(ref Vector)][12]     | `void`       | Normalizes the input vector.                                           |
| [Project(in Vector, in ...][37] | `float`      | Projects the first vector onto the second.                             |
| [Project(in Vector, in ...][37] | `float`      | Projects a point onto a line segment.                                  |
| [Reflect(in Vector, in ...][38] | [Vector][41] | Computes the reflection of the input vector about the specified axis.  |
| [Rotate(Vector, float)][39]     | [Vector][41] | Rotates a vector by the specified angle.                               |
| [Round(Vector)][40]             | [Vector][41] | Computes a new vector with the rounded value of each component of t... |

[0]: ../../Heirloom.Core.md
[1]: Vector/X.md
[2]: Vector/Y.md
[3]: Vector/Angle.md
[4]: Vector/Indexer.md
[5]: Vector/Length.md
[6]: Vector/LengthSquared.md
[7]: Vector/Normalized.md
[8]: Vector/Perpendicular.md
[9]: Vector/Deconstruct.md
[10]: Vector/Equals.md
[11]: Vector/GetHashCode.md
[12]: Vector/Normalize.md
[13]: Vector/Set.md
[14]: Vector/ToString.md
[15]: Vector/Down.md
[16]: Vector/Left.md
[17]: Vector/One.md
[18]: Vector/Right.md
[19]: Vector/Up.md
[20]: Vector/Zero.md
[21]: Vector/Abs.md
[22]: Vector/AngleBetween.md
[23]: Vector/Ceil.md
[24]: Vector/Cross.md
[25]: Vector/Distance.md
[26]: Vector/DistanceSquared.md
[27]: Vector/Dot.md
[28]: Vector/Floor.md
[29]: Vector/Fraction.md
[30]: Vector/FromAngle.md
[31]: Vector/GetMaxComponent.md
[32]: Vector/GetMinComponent.md
[33]: Vector/Lerp.md
[34]: Vector/ManhattanDistance.md
[35]: Vector/Max.md
[36]: Vector/Min.md
[37]: Vector/Project.md
[38]: Vector/Reflect.md
[39]: Vector/Rotate.md
[40]: Vector/Round.md
[41]: Vector.md
