# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  

## IntVector

> **Namespace**: [Heirloom][0]  

Represents a vector with two integer values.

```cs
public struct IntVector : IEquatable<IntVector>
```

### Inherits

IEquatable\<IntVector>

#### Fields

[X][1], [Y][2]

#### Properties

[Length][3], [LengthSquared][4], [Perpendicular][5], [Indexer][6]

#### Methods

[Set][7], [Deconstruct][8]

#### Static Fields

[Zero][9], [One][10], [Right][11], [Up][12], [Left][13], [Down][14]

#### Static Methods

[GetMaxComponent][15], [GetMinComponent][16], [Min][17], [Max][18], [Abs][19], [Distance][20], [DistanceSquared][21], [ManhattanDistance][22]

## Fields

| Name        | Summary                          |
|-------------|----------------------------------|
| [X][1]      | The x-coordinate of this vector. |
| [Y][2]      | The y-coordinate of this vector. |
| [Zero][9]   | A vector with value (0, 0).      |
| [One][10]   | A vector with value (1, 1).      |
| [Right][11] | A vector with value (1, 0).      |
| [Up][12]    | A vector with value (0, -1).     |
| [Left][13]  | A vector with value (-1, 0).     |
| [Down][14]  | A vector with value (0, 1).      |

## Properties

| Name               | Summary                                    |
|--------------------|--------------------------------------------|
| [Length][3]        | Gets the magnitude of this vector.         |
| [LengthSquared][4] | Gets the squared magnitude of this vector. |
| [Perpendicular][5] | Gets a perpendicular copy of this vector.  |
| [Indexer][6]       |                                            |

## Methods

| Name                    | Summary                                                                                                 |
|-------------------------|---------------------------------------------------------------------------------------------------------|
| [Set][7]                | Sets the components of this vector.                                                                     |
| [Deconstruct][8]        |                                                                                                         |
| [GetMaxComponent][15]   | Gets the maximal component in the input vector.                                                         |
| [GetMinComponent][16]   | Gets the minimal component in the input vector.                                                         |
| [Min][17]               | Computes a new vector where each component is the minimum component in each respective input vector.    |
| [Max][18]               | Computes a new vector where each component is the maximum component in each respective input vector.    |
| [Abs][19]               | Computes a new vector where each component is the absolute value of each component in the input vector. |
| [Distance][20]          | Computes the euclidean distance between any two vectors.                                                |
| [DistanceSquared][21]   | Computes the squared euclidean distance between any two vectors.                                        |
| [ManhattanDistance][22] | Computes the manhattan distance between any two vectors.                                                |

[0]: ../../Heirloom.Core.md
[1]: IntVector/X.md
[2]: IntVector/Y.md
[3]: IntVector/Length.md
[4]: IntVector/LengthSquared.md
[5]: IntVector/Perpendicular.md
[6]: IntVector/Indexer.md
[7]: IntVector/Set.md
[8]: IntVector/Deconstruct.md
[9]: IntVector/Zero.md
[10]: IntVector/One.md
[11]: IntVector/Right.md
[12]: IntVector/Up.md
[13]: IntVector/Left.md
[14]: IntVector/Down.md
[15]: IntVector/GetMaxComponent.md
[16]: IntVector/GetMinComponent.md
[17]: IntVector/Min.md
[18]: IntVector/Max.md
[19]: IntVector/Abs.md
[20]: IntVector/Distance.md
[21]: IntVector/DistanceSquared.md
[22]: IntVector/ManhattanDistance.md
