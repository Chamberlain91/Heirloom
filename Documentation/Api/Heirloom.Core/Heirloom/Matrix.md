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

[Deconstruct][9], [GetAffineRotation][10], [GetAffineScale][11], [GetAffineTranslation][12], [Multiply][13], [MultiplyVector][14], [Set][15], [SetRotation][16], [SetScale][17], [SetShear][18], [SetTranslation][19]

### Static Fields

[Identity][20]

### Static Methods

[CreateRotation][21], [CreateScale][22], [CreateShear][23], [CreateTransform][24], [CreateTranslation][25], [Inverse][26], [Multiply][13], [MultiplyVector][14], [RectangleProjection][27]

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
| [Identity][20] | [Matrix][28] | The identity matrix. |

## Properties

#### Instance

| Name          | Type         | Summary                          |
|---------------|--------------|----------------------------------|
| [Indexer][7]  | `float`      |                                  |
| [Indexer][7]  | `float`      |                                  |
| [Inverted][8] | [Matrix][28] | Gets the inverse of this matrix. |

## Methods

#### Instance

| Name                            | Return Type  | Summary                                                                |
|---------------------------------|--------------|------------------------------------------------------------------------|
| [Deconstruct(out Vector...][9]  | `void`       |                                                                        |
| [GetAffineRotation()][10]       | `float`      | Extracts affine rotational component (the angle) from this matrix.     |
| [GetAffineScale()][11]          | [Vector][29] | Extracts affine scaling components from this matrix.                   |
| [GetAffineTranslation()][12]    | [Vector][29] | Extracts affine translational components from this matrix.             |
| [Multiply(in Vector)][13]       | [Vector][29] | Multiplies a vector against this matrix.                               |
| [MultiplyVector(in Vector)][14] | [Vector][29] | Multiplies a vector against this matrix ignoring the translational ... |
| [Set(float, float, floa...][15] | `void`       | Sets the components of this matrix.                                    |
| [SetRotation(float)][16]        | `void`       | Configures this matrix as a rotation matrix.                           |
| [SetScale(float, float)][17]    | `void`       | Configures this matrix as a scaling matrix.                            |
| [SetShear(float, float)][18]    | `void`       | Configures this matrix as a shearing matrix.                           |
| [SetTranslation(float, ...][19] | `void`       | Configures this matrix as a translation matrix.                        |

#### Static

| Name                            | Return Type  | Summary                                                                |
|---------------------------------|--------------|------------------------------------------------------------------------|
| [CreateRotation(float)][21]     | [Matrix][28] | Constructs a new rotation matrix.                                      |
| [CreateScale(float, float)][22] | [Matrix][28] | Constructs a new scaling matrix.                                       |
| [CreateScale(in Size)][22]      | [Matrix][28] | Constructs a new scaling matrix.                                       |
| [CreateScale(in Vector)][22]    | [Matrix][28] | Constructs a new scaling matrix.                                       |
| [CreateScale(float)][22]        | [Matrix][28] | Constructs a new uniform scaling matrix.                               |
| [CreateShear(in Vector)][23]    | [Matrix][28] | Constructs a new shearing matrix.                                      |
| [CreateShear(float, float)][23] | [Matrix][28] | Constructs a new shearing matrix.                                      |
| [CreateTransform(in flo...][24] | [Matrix][28] | Creates a transform matrix with postion, rotation and scale.           |
| [CreateTransform(in Vec...][24] | [Matrix][28] | Creates a transform matrix with postion, rotation and scale.           |
| [CreateTransform(in Vec...][24] | [Matrix][28] | Creates a transform matrix with postion, rotation and scale.           |
| [CreateTranslation(floa...][25] | [Matrix][28] | Constructs a new translation matrix.                                   |
| [CreateTranslation(in V...][25] | [Matrix][28] | Constructs a new translation matrix.                                   |
| [Inverse(in Matrix)][26]        | [Matrix][28] | Computes the inverse of this matrix.                                   |
| [Inverse(in Matrix, ref...][26] | `void`       | Computes the inverse of the matrix and stores the resulting matrix ... |
| [Multiply(in Matrix, in...][13] | `void`       | Multiply two matrices together and store the result in `dest` .        |
| [Multiply(in Matrix, in...][13] | [Matrix][28] | Multiply two matrices together.                                        |
| [Multiply(in Matrix, in...][13] | `void`       | Multiplies a vector and matrix together and stores the resulting ve... |
| [Multiply(in Matrix, in...][13] | [Vector][29] | Multiplies a vector and matrix together.                               |
| [MultiplyVector(in Matr...][14] | `void`       | Multiplies a vector and matrix together ignoring the translational ... |
| [MultiplyVector(in Matr...][14] | [Vector][29] | Multiplies a vector and matrix together ignoring the translational ... |
| [RectangleProjection(Re...][27] | [Matrix][28] | Constructs a matrix that transforms a rectangular region to normali... |
| [RectangleProjection(fl...][27] | [Matrix][28] | Constructs a matrix that transforms a rectangular region to normali... |

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
[10]: Matrix/GetAffineRotation.md
[11]: Matrix/GetAffineScale.md
[12]: Matrix/GetAffineTranslation.md
[13]: Matrix/Multiply.md
[14]: Matrix/MultiplyVector.md
[15]: Matrix/Set.md
[16]: Matrix/SetRotation.md
[17]: Matrix/SetScale.md
[18]: Matrix/SetShear.md
[19]: Matrix/SetTranslation.md
[20]: Matrix/Identity.md
[21]: Matrix/CreateRotation.md
[22]: Matrix/CreateScale.md
[23]: Matrix/CreateShear.md
[24]: Matrix/CreateTransform.md
[25]: Matrix/CreateTranslation.md
[26]: Matrix/Inverse.md
[27]: Matrix/RectangleProjection.md
[28]: Matrix.md
[29]: Vector.md
