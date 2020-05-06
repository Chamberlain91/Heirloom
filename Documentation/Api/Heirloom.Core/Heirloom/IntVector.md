# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## IntVector (Struct)

> **Namespace**: [Heirloom][0]

Represents a vector with two integer values.

```cs
public struct IntVector : IEquatable<IntVector>
```

### Inherits

IEquatable\<IntVector>

### Fields

[X][1], [Y][2]

### Properties

[Indexer][3], [Length][4], [LengthSquared][5], [Perpendicular][6]

### Methods

[Deconstruct][7], [Equals][8], [GetHashCode][9], [Set][10], [ToString][11]

### Static Fields

[Down][12], [Left][13], [One][14], [Right][15], [Up][16], [Zero][17]

### Static Methods

[Abs][18], [Distance][19], [DistanceSquared][20], [GetMaxComponent][21], [GetMinComponent][22], [ManhattanDistance][23], [Max][24], [Min][25]

## Fields

#### Instance

| Name   | Type  | Summary                          |
|--------|-------|----------------------------------|
| [X][1] | `int` | The x-coordinate of this vector. |
| [Y][2] | `int` | The y-coordinate of this vector. |

#### Static

| Name        | Type            | Summary                      |
|-------------|-----------------|------------------------------|
| [Down][12]  | [IntVector][26] | A vector with value (0, 1).  |
| [Left][13]  | [IntVector][26] | A vector with value (-1, 0). |
| [One][14]   | [IntVector][26] | A vector with value (1, 1).  |
| [Right][15] | [IntVector][26] | A vector with value (1, 0).  |
| [Up][16]    | [IntVector][26] | A vector with value (0, -1). |
| [Zero][17]  | [IntVector][26] | A vector with value (0, 0).  |

## Properties

#### Instance

| Name               | Type            | Summary                                    |
|--------------------|-----------------|--------------------------------------------|
| [Indexer][3]       | `int`           |                                            |
| [Length][4]        | `float`         | Gets the magnitude of this vector.         |
| [LengthSquared][5] | `float`         | Gets the squared magnitude of this vector. |
| [Perpendicular][6] | [IntVector][26] | Gets a perpendicular copy of this vector.  |

## Methods

#### Instance

| Name                           | Return Type | Summary                             |
|--------------------------------|-------------|-------------------------------------|
| [Deconstruct(out int, o...][7] | `void`      |                                     |
| [Equals(object)][8]            | `bool`      |                                     |
| [Equals(IntVector)][8]         | `bool`      |                                     |
| [GetHashCode()][9]             | `int`       |                                     |
| [Set(int, int)][10]            | `void`      | Sets the components of this vector. |
| [ToString()][11]               | `string`    |                                     |

#### Static

| Name                            | Return Type     | Summary                                                                |
|---------------------------------|-----------------|------------------------------------------------------------------------|
| [Abs(IntVector)][18]            | [IntVector][26] | Computes a new vector where each component is the absolute value of... |
| [Distance(in Vector, in...][19] | `float`         | Computes the euclidean distance between any two vectors.               |
| [DistanceSquared(in Vec...][20] | `float`         | Computes the squared euclidean distance between any two vectors.       |
| [GetMaxComponent(IntVec...][21] | `int`           | Gets the maximal component in the input vector.                        |
| [GetMinComponent(IntVec...][22] | `int`           | Gets the minimal component in the input vector.                        |
| [ManhattanDistance(in V...][23] | `float`         | Computes the manhattan distance between any two vectors.               |
| [Max(IntVector, IntVector)][24] | [IntVector][26] | Computes a new vector where each component is the maximum component... |
| [Min(IntVector, IntVector)][25] | [IntVector][26] | Computes a new vector where each component is the minimum component... |

[0]: ../../Heirloom.Core.md
[1]: IntVector/X.md
[2]: IntVector/Y.md
[3]: IntVector/Indexer.md
[4]: IntVector/Length.md
[5]: IntVector/LengthSquared.md
[6]: IntVector/Perpendicular.md
[7]: IntVector/Deconstruct.md
[8]: IntVector/Equals.md
[9]: IntVector/GetHashCode.md
[10]: IntVector/Set.md
[11]: IntVector/ToString.md
[12]: IntVector/Down.md
[13]: IntVector/Left.md
[14]: IntVector/One.md
[15]: IntVector/Right.md
[16]: IntVector/Up.md
[17]: IntVector/Zero.md
[18]: IntVector/Abs.md
[19]: IntVector/Distance.md
[20]: IntVector/DistanceSquared.md
[21]: IntVector/GetMaxComponent.md
[22]: IntVector/GetMinComponent.md
[23]: IntVector/ManhattanDistance.md
[24]: IntVector/Max.md
[25]: IntVector/Min.md
[26]: IntVector.md
