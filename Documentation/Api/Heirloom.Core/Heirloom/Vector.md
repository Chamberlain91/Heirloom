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

[Deconstruct][9], [Normalize][10], [Set][11]

### Static Fields

[Down][12], [Left][13], [One][14], [Right][15], [Up][16], [Zero][17]

### Static Methods

[Abs][18], [AngleBetween][19], [Ceil][20], [Cross][21], [Distance][22], [DistanceSquared][23], [Dot][24], [Floor][25], [Fraction][26], [FromAngle][27], [GetMaxComponent][28], [GetMinComponent][29], [Lerp][30], [ManhattanDistance][31], [Max][32], [Min][33], [Normalize][10], [Project][34], [Reflect][35], [Rotate][36], [Round][37]

## Fields

#### Instance

| Name   | Type    | Summary                         |
|--------|---------|---------------------------------|
| [X][1] | `float` | The x-component of this vector. |
| [Y][2] | `float` | The y-component of this vector. |

#### Static

| Name        | Type         | Summary                      |
|-------------|--------------|------------------------------|
| [Down][12]  | [Vector][38] | A vector with value (0, 1).  |
| [Left][13]  | [Vector][38] | A vector with value (-1, 0). |
| [One][14]   | [Vector][38] | A vector with value (1, 1).  |
| [Right][15] | [Vector][38] | A vector with value (1, 0).  |
| [Up][16]    | [Vector][38] | A vector with value (0, -1). |
| [Zero][17]  | [Vector][38] | A vector with value (0, 0).  |

## Properties

#### Instance

| Name               | Type         | Summary                                                          |
|--------------------|--------------|------------------------------------------------------------------|
| [Angle][3]         | `float`      | Gets the angle this vector is pointing with reference to Right . |
| [Indexer][4]       | `float`      |                                                                  |
| [Length][5]        | `float`      | Gets the magnitude of this vector.                               |
| [LengthSquared][6] | `float`      | Gets the squared magnitude of this vector.                       |
| [Normalized][7]    | [Vector][38] | Gets a normalized copy of this vector.                           |
| [Perpendicular][8] | [Vector][38] | Gets a perpendicular copy of this vector.                        |

## Methods

#### Instance

| Name                           | Return Type | Summary                                               |
|--------------------------------|-------------|-------------------------------------------------------|
| [Deconstruct(out float,...][9] | `void`      | Deconstructs this Vector into constituent components. |
| [Normalize()][10]              | `void`      | Normalize this vector.                                |
| [Set(float, float)][11]        | `void`      | Sets the components of this vector.                   |

#### Static

| Name                            | Return Type  | Summary                                                                |
|---------------------------------|--------------|------------------------------------------------------------------------|
| [Abs(Vector)][18]               | [Vector][38] | Computes a new vector where each component is the absolute value of... |
| [AngleBetween(Vector, V...][19] | `float`      | Computes the angle (in radians) between two vectors (using dot prod... |
| [Ceil(Vector)][20]              | [Vector][38] | Computes a new vector with the ceiling of each component of the inp... |
| [Cross(in Vector, in Ve...][21] | `float`      | Computes the cross-product of two vectors.                             |
| [Cross(in Vector, float)][21]   | [Vector][38] | Computes the cross-product of a vector and a magnitude.                |
| [Cross(float, in Vector)][21]   | [Vector][38] | Computes the cross-product of a vector and a magnitude.                |
| [Distance(in Vector, in...][22] | `float`      | Computes the euclidean distance between any two vectors.               |
| [DistanceSquared(in Vec...][23] | `float`      | Computes the squared euclidean distance between any two vectors.       |
| [Dot(in Vector, in Vector)][24] | `float`      | Computes the dot-product of two vectors.                               |
| [Floor(Vector)][25]             | [Vector][38] | Computes a new vector with the floor of each component of the input... |
| [Fraction(Vector)][26]          | [Vector][38] | Computes a new vector with the fractional portion of each component... |
| [FromAngle(float)][27]          | [Vector][38] | Creates a unit length vector with the given angle from the x-axis.     |
| [GetMaxComponent(Vector)][28]   | `float`      | Gets the maximal component in the input vector.                        |
| [GetMinComponent(Vector)][29]   | `float`      | Gets the minimal component in the input vector.                        |
| [Lerp(Vector, Vector, f...][30] | [Vector][38] | Interpolate two vectors.                                               |
| [ManhattanDistance(in V...][31] | `float`      | Computes the manhattan distance between any two vectors.               |
| [Max(Vector, Vector)][32]       | [Vector][38] | Computes a new vector where each component is the maximum component... |
| [Min(Vector, Vector)][33]       | [Vector][38] | Computes a new vector where each component is the minimum component... |
| [Normalize(Vector)][10]         | [Vector][38] | Normalizes the input vector and return it.                             |
| [Normalize(ref Vector)][10]     | `void`       | Normalizes the input vector.                                           |
| [Project(in Vector, in ...][34] | `float`      | Projects the first vector onto the second.                             |
| [Project(in Vector, in ...][34] | `float`      | Projects a point onto a line segment.                                  |
| [Reflect(in Vector, in ...][35] | [Vector][38] | Computes the reflection of the input vector about the specified axis.  |
| [Rotate(Vector, float)][36]     | [Vector][38] | Rotates a vector by the specified angle.                               |
| [Round(Vector)][37]             | [Vector][38] | Computes a new vector with the rounded value of each component of t... |

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
[10]: Vector/Normalize.md
[11]: Vector/Set.md
[12]: Vector/Down.md
[13]: Vector/Left.md
[14]: Vector/One.md
[15]: Vector/Right.md
[16]: Vector/Up.md
[17]: Vector/Zero.md
[18]: Vector/Abs.md
[19]: Vector/AngleBetween.md
[20]: Vector/Ceil.md
[21]: Vector/Cross.md
[22]: Vector/Distance.md
[23]: Vector/DistanceSquared.md
[24]: Vector/Dot.md
[25]: Vector/Floor.md
[26]: Vector/Fraction.md
[27]: Vector/FromAngle.md
[28]: Vector/GetMaxComponent.md
[29]: Vector/GetMinComponent.md
[30]: Vector/Lerp.md
[31]: Vector/ManhattanDistance.md
[32]: Vector/Max.md
[33]: Vector/Min.md
[34]: Vector/Project.md
[35]: Vector/Reflect.md
[36]: Vector/Rotate.md
[37]: Vector/Round.md
[38]: Vector.md
