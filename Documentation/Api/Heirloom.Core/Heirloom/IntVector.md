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

[Deconstruct][7], [Set][8]

### Static Fields

[Down][9], [Left][10], [One][11], [Right][12], [Up][13], [Zero][14]

### Static Methods

[Abs][15], [Distance][16], [DistanceSquared][17], [GetMaxComponent][18], [GetMinComponent][19], [ManhattanDistance][20], [Max][21], [Min][22]

## Fields

#### Instance

| Name   | Type  | Summary                          |
|--------|-------|----------------------------------|
| [X][1] | `int` | The x-coordinate of this vector. |
| [Y][2] | `int` | The y-coordinate of this vector. |

#### Static

| Name        | Type            | Summary                      |
|-------------|-----------------|------------------------------|
| [Down][9]   | [IntVector][23] | A vector with value (0, 1).  |
| [Left][10]  | [IntVector][23] | A vector with value (-1, 0). |
| [One][11]   | [IntVector][23] | A vector with value (1, 1).  |
| [Right][12] | [IntVector][23] | A vector with value (1, 0).  |
| [Up][13]    | [IntVector][23] | A vector with value (0, -1). |
| [Zero][14]  | [IntVector][23] | A vector with value (0, 0).  |

## Properties

#### Instance

| Name               | Type            | Summary                                    |
|--------------------|-----------------|--------------------------------------------|
| [Indexer][3]       | `int`           |                                            |
| [Length][4]        | `float`         | Gets the magnitude of this vector.         |
| [LengthSquared][5] | `float`         | Gets the squared magnitude of this vector. |
| [Perpendicular][6] | [IntVector][23] | Gets a perpendicular copy of this vector.  |

## Methods

#### Instance

| Name                           | Return Type | Summary                             |
|--------------------------------|-------------|-------------------------------------|
| [Deconstruct(out int, o...][7] | `void`      |                                     |
| [Set(int, int)][8]             | `void`      | Sets the components of this vector. |

#### Static

| Name                            | Return Type     | Summary                                                                |
|---------------------------------|-----------------|------------------------------------------------------------------------|
| [Abs(IntVector)][15]            | [IntVector][23] | Computes a new vector where each component is the absolute value of... |
| [Distance(in IntVector,...][16] | `float`         | Computes the euclidean distance between any two vectors.               |
| [DistanceSquared(in Int...][17] | `int`           | Computes the squared euclidean distance between any two vectors.       |
| [GetMaxComponent(IntVec...][18] | `int`           | Gets the maximal component in the input vector.                        |
| [GetMinComponent(IntVec...][19] | `int`           | Gets the minimal component in the input vector.                        |
| [ManhattanDistance(in I...][20] | `int`           | Computes the manhattan distance between any two vectors.               |
| [Max(IntVector, IntVector)][21] | [IntVector][23] | Computes a new vector where each component is the maximum component... |
| [Min(IntVector, IntVector)][22] | [IntVector][23] | Computes a new vector where each component is the minimum component... |

[0]: ../../Heirloom.Core.md
[1]: IntVector/X.md
[2]: IntVector/Y.md
[3]: IntVector/Indexer.md
[4]: IntVector/Length.md
[5]: IntVector/LengthSquared.md
[6]: IntVector/Perpendicular.md
[7]: IntVector/Deconstruct.md
[8]: IntVector/Set.md
[9]: IntVector/Down.md
[10]: IntVector/Left.md
[11]: IntVector/One.md
[12]: IntVector/Right.md
[13]: IntVector/Up.md
[14]: IntVector/Zero.md
[15]: IntVector/Abs.md
[16]: IntVector/Distance.md
[17]: IntVector/DistanceSquared.md
[18]: IntVector/GetMaxComponent.md
[19]: IntVector/GetMinComponent.md
[20]: IntVector/ManhattanDistance.md
[21]: IntVector/Max.md
[22]: IntVector/Min.md
[23]: IntVector.md
