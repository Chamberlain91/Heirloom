# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Matrix (Struct)

> **Namespace**: [Heirloom][0]

A 2x3 transformation matrix.

```cs
public struct Matrix : IEquatable<Matrix>
```

### Inherits

IEquatable\<Matrix>

### Fields

[M0][1], [M1][2], [M2][3], [M3][4], [M4][5], [M5][6]

### Properties

[Indexer][7], [Inverted][8]

### Methods

[Deconstruct][9], [Equals][10], [GetAffineRotation][11], [GetAffineScale][12], [GetAffineTranslation][13], [GetHashCode][14], [Multiply][15], [MultiplyVector][16], [Set][17], [SetRotation][18], [SetScale][19], [SetShear][20], [SetTranslation][21]

### Static Fields

[Identity][22]

### Static Methods

[CreateRotation][23], [CreateScale][24], [CreateShear][25], [CreateTransform][26], [CreateTranslation][27], [Inverse][28], [Multiply][15], [MultiplyVector][16], [RectangleProjection][29]

## Fields

#### Instance

| Name    | Type    | Summary |
|---------|---------|---------|
| [M0][1] | `float` |         |
| [M1][2] | `float` |         |
| [M2][3] | `float` |         |
| [M3][4] | `float` |         |
| [M4][5] | `float` |         |
| [M5][6] | `float` |         |

#### Static

| Name           | Type         | Summary              |
|----------------|--------------|----------------------|
| [Identity][22] | [Matrix][30] | The identity matrix. |

## Properties

#### Instance

| Name          | Type         | Summary                          |
|---------------|--------------|----------------------------------|
| [Indexer][7]  | `float`      |                                  |
| [Indexer][7]  | `float`      |                                  |
| [Inverted][8] | [Matrix][30] | Gets the inverse of this matrix. |

## Methods

#### Instance

| Name                            | Return Type  | Summary                                                                |
|---------------------------------|--------------|------------------------------------------------------------------------|
| [Deconstruct(out Vector...][9]  | `void`       |                                                                        |
| [Equals(object)][10]            | `bool`       |                                                                        |
| [Equals(Matrix)][10]            | `bool`       |                                                                        |
| [GetAffineRotation()][11]       | `float`      | Extracts affine rotational component (the angle) from this matrix.     |
| [GetAffineScale()][12]          | [Vector][31] | Extracts affine scaling components from this matrix.                   |
| [GetAffineTranslation()][13]    | [Vector][31] | Extracts affine translational components from this matrix.             |
| [GetHashCode()][14]             | `int`        |                                                                        |
| [Multiply(in Vector)][15]       | [Vector][31] | Multiplies a vector against this matrix.                               |
| [MultiplyVector(in Vector)][16] | [Vector][31] | Multiplies a vector against this matrix ignoring the translational ... |
| [Set(float, float, floa...][17] | `void`       | Sets the components of this matrix.                                    |
| [SetRotation(float)][18]        | `void`       | Configures this matrix as a rotation matrix.                           |
| [SetScale(float, float)][19]    | `void`       | Configures this matrix as a scaling matrix.                            |
| [SetShear(float, float)][20]    | `void`       | Configures this matrix as a shearing matrix.                           |
| [SetTranslation(float, ...][21] | `void`       | Configures this matrix as a translation matrix.                        |

#### Static

| Name                            | Return Type  | Summary                                                                |
|---------------------------------|--------------|------------------------------------------------------------------------|
| [CreateRotation(float)][23]     | [Matrix][30] | Constructs a new rotation matrix.                                      |
| [CreateScale(float, float)][24] | [Matrix][30] | Constructs a new scaling matrix.                                       |
| [CreateScale(in Size)][24]      | [Matrix][30] | Constructs a new scaling matrix.                                       |
| [CreateScale(in Vector)][24]    | [Matrix][30] | Constructs a new scaling matrix.                                       |
| [CreateScale(float)][24]        | [Matrix][30] | Constructs a new uniform scaling matrix.                               |
| [CreateShear(in Vector)][25]    | [Matrix][30] | Constructs a new shearing matrix.                                      |
| [CreateShear(float, float)][25] | [Matrix][30] | Constructs a new shearing matrix.                                      |
| [CreateTransform(in flo...][26] | [Matrix][30] | Creates a transform matrix with postion, rotation and scale.           |
| [CreateTransform(in Vec...][26] | [Matrix][30] | Creates a transform matrix with postion, rotation and scale.           |
| [CreateTransform(in Vec...][26] | [Matrix][30] | Creates a transform matrix with postion, rotation and scale.           |
| [CreateTranslation(floa...][27] | [Matrix][30] | Constructs a new translation matrix.                                   |
| [CreateTranslation(in V...][27] | [Matrix][30] | Constructs a new translation matrix.                                   |
| [Inverse(in Matrix)][28]        | [Matrix][30] | Computes the inverse of this matrix.                                   |
| [Inverse(in Matrix, ref...][28] | `void`       | Computes the inverse of the matrix and stores the resulting matrix ... |
| [Multiply(in Matrix, in...][15] | `void`       | Multiply two matrices together and store the result in `dest` .        |
| [Multiply(in Matrix, in...][15] | [Matrix][30] | Multiply two matrices together.                                        |
| [Multiply(in Matrix, in...][15] | `void`       | Multiplies a vector and matrix together and stores the resulting ve... |
| [Multiply(in Matrix, in...][15] | [Vector][31] | Multiplies a vector and matrix together.                               |
| [MultiplyVector(in Matr...][16] | `void`       | Multiplies a vector and matrix together ignoring the translational ... |
| [MultiplyVector(in Matr...][16] | [Vector][31] | Multiplies a vector and matrix together ignoring the translational ... |
| [RectangleProjection(Re...][29] | [Matrix][30] | Constructs a matrix that transforms a rectangular region to normali... |
| [RectangleProjection(fl...][29] | [Matrix][30] | Constructs a matrix that transforms a rectangular region to normali... |

[0]: ../../Heirloom.Core.md
[1]: Matrix/M0.md
[2]: Matrix/M1.md
[3]: Matrix/M2.md
[4]: Matrix/M3.md
[5]: Matrix/M4.md
[6]: Matrix/M5.md
[7]: Matrix/Indexer.md
[8]: Matrix/Inverted.md
[9]: Matrix/Deconstruct.md
[10]: Matrix/Equals.md
[11]: Matrix/GetAffineRotation.md
[12]: Matrix/GetAffineScale.md
[13]: Matrix/GetAffineTranslation.md
[14]: Matrix/GetHashCode.md
[15]: Matrix/Multiply.md
[16]: Matrix/MultiplyVector.md
[17]: Matrix/Set.md
[18]: Matrix/SetRotation.md
[19]: Matrix/SetScale.md
[20]: Matrix/SetShear.md
[21]: Matrix/SetTranslation.md
[22]: Matrix/Identity.md
[23]: Matrix/CreateRotation.md
[24]: Matrix/CreateScale.md
[25]: Matrix/CreateShear.md
[26]: Matrix/CreateTransform.md
[27]: Matrix/CreateTranslation.md
[28]: Matrix/Inverse.md
[29]: Matrix/RectangleProjection.md
[30]: Matrix.md
[31]: Vector.md
