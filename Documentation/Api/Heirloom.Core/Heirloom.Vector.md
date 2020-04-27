# Vector

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  
> **Namespace**: [Heirloom][0]  

Represents a vector with two single-precision floating-point values.

```cs
public struct Vector : IEquatable<Vector>
```

--------------------------------------------------------------------------------

**Inherits**: IEquatable\<Vector>

**Fields**: [X][1], [Y][2]

**Properties**: [Length][3], [LengthSquared][4], [Normalized][5], [Perpendicular][6], [Angle][7], [Item][8]

**Methods**: [Set][9], [Normalize][10], [Deconstruct][11]

**Static Fields**: [Zero][12], [One][13], [Right][14], [Up][15], [Left][16], [Down][17]

**Static Methods**: [GetMaxComponent][18], [GetMinComponent][19], [Min][20], [Max][21], [Abs][22], [Distance][23], [DistanceSquared][24], [ManhattanDistance][25], [Normalize][10], [Lerp][26], [FromAngle][27], [AngleBetween][28], [Rotate][29], [Dot][30], [Cross][31], [Project][32], [Reflect][33], [Floor][34], [Ceil][35], [Round][36], [Fraction][37]

--------------------------------------------------------------------------------

## Fields

| Name        | Summary                          |
|-------------|----------------------------------|
| [X][1]      | The x-coordinate of this vector. |
| [Y][2]      | The y-coordinate of this vector. |
| [Zero][12]  | A vector with value (0, 0).      |
| [One][13]   | A vector with value (1, 1).      |
| [Right][14] | A vector with value (1, 0).      |
| [Up][15]    | A vector with value (0, -1).     |
| [Left][16]  | A vector with value (-1, 0).     |
| [Down][17]  | A vector with value (0, 1).      |

## Properties

| Name               | Summary                                                                |
|--------------------|------------------------------------------------------------------------|
| [Length][3]        | Gets the magnitude of this vector.                                     |
| [LengthSquared][4] | Gets the squared magnitude of this vector.                             |
| [Normalized][5]    | Gets a normalized copy of this vector.                                 |
| [Perpendicular][6] | Gets a perpendicular copy of this vector.                              |
| [Angle][7]         | Gets the angle this vector is pointing with reference to [Right][14] . |
| [Item][8]          |                                                                        |

## Methods

| Name                    | Summary                                                                                                 |
|-------------------------|---------------------------------------------------------------------------------------------------------|
| [Set][9]                | Sets the components of this vector.                                                                     |
| [Normalize][10]         | Normalize this vector.                                                                                  |
| [Deconstruct][11]       |                                                                                                         |
| [GetMaxComponent][18]   | Gets the maximal component in the input vector.                                                         |
| [GetMinComponent][19]   | Gets the minimal component in the input vector.                                                         |
| [Min][20]               | Computes a new vector where each component is the minimum component in each respective input vector.    |
| [Max][21]               | Computes a new vector where each component is the maximum component in each respective input vector.    |
| [Abs][22]               | Computes a new vector where each component is the absolute value of each component in the input vector. |
| [Distance][23]          | Computes the euclidean distance between any two vectors.                                                |
| [DistanceSquared][24]   | Computes the squared euclidean distance between any two vectors.                                        |
| [ManhattanDistance][25] | Computes the manhattan distance between any two vectors.                                                |
| [Normalize][10]         | Normalizes the input vector and return it.                                                              |
| [Normalize][10]         | Normalizes the input vector.                                                                            |
| [Lerp][26]              | Interpolate two vectors.                                                                                |
| [FromAngle][27]         | Creates a unit length vector with the given angle from the x-axis.                                      |
| [AngleBetween][28]      | Computes the angle (in radians) between two vectors (using dot product).                                |
| [Rotate][29]            | Rotates a vector by the specified angle.                                                                |
| [Dot][30]               | Computes the dot-product of two vectors.                                                                |
| [Cross][31]             | Computes the cross-product of two vectors.                                                              |
| [Cross][31]             | Computes the cross-product of a vector and a magnitude.                                                 |
| [Project][32]           | Projects the first vector onto the second.                                                              |
| [Project][32]           | Projects a point onto a line segment.                                                                   |
| [Reflect][33]           | Computes the reflection of the input vector about the specified axis.                                   |
| [Floor][34]             | Computes a new vector with the floor of each component of the input vector.                             |
| [Ceil][35]              | Computes a new vector with the ceiling of each component of the input vector.                           |
| [Round][36]             | Computes a new vector with the rounded value of each component of the input vector.                     |
| [Fraction][37]          | Computes a new vector with the fractional portion of each component of the input vector.                |

[0]: ..\Heirloom.Core.md
[1]: Heirloom.Vector.X.md
[2]: Heirloom.Vector.Y.md
[3]: Heirloom.Vector.Length.md
[4]: Heirloom.Vector.LengthSquared.md
[5]: Heirloom.Vector.Normalized.md
[6]: Heirloom.Vector.Perpendicular.md
[7]: Heirloom.Vector.Angle.md
[8]: Heirloom.Vector.Item.md
[9]: Heirloom.Vector.Set.md
[10]: Heirloom.Vector.Normalize.md
[11]: Heirloom.Vector.Deconstruct.md
[12]: Heirloom.Vector.Zero.md
[13]: Heirloom.Vector.One.md
[14]: Heirloom.Vector.Right.md
[15]: Heirloom.Vector.Up.md
[16]: Heirloom.Vector.Left.md
[17]: Heirloom.Vector.Down.md
[18]: Heirloom.Vector.GetMaxComponent.md
[19]: Heirloom.Vector.GetMinComponent.md
[20]: Heirloom.Vector.Min.md
[21]: Heirloom.Vector.Max.md
[22]: Heirloom.Vector.Abs.md
[23]: Heirloom.Vector.Distance.md
[24]: Heirloom.Vector.DistanceSquared.md
[25]: Heirloom.Vector.ManhattanDistance.md
[26]: Heirloom.Vector.Lerp.md
[27]: Heirloom.Vector.FromAngle.md
[28]: Heirloom.Vector.AngleBetween.md
[29]: Heirloom.Vector.Rotate.md
[30]: Heirloom.Vector.Dot.md
[31]: Heirloom.Vector.Cross.md
[32]: Heirloom.Vector.Project.md
[33]: Heirloom.Vector.Reflect.md
[34]: Heirloom.Vector.Floor.md
[35]: Heirloom.Vector.Ceil.md
[36]: Heirloom.Vector.Round.md
[37]: Heirloom.Vector.Fraction.md
