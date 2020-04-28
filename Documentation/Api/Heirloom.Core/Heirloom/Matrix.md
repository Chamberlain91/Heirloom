# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  

## Matrix

> **Namespace**: [Heirloom][0]  

A 2x3 transformation matrix.

```cs
public struct Matrix : IEquatable<Matrix>
```

### Inherits

IEquatable\<Matrix>

#### Fields

[M0][1], [M1][2], [M2][3], [M3][4], [M4][5], [M5][6]

#### Properties

[Indexer][7], [Inverted][8]

#### Methods

[Multiply][9], [MultiplyVector][10], [Set][11], [SetScale][12], [SetShear][13], [SetRotation][14], [SetTranslation][15], [Deconstruct][16], [GetAffineScale][17], [GetAffineTranslation][18], [GetAffineRotation][19]

#### Static Fields

[Identity][20]

#### Static Methods

[Inverse][21], [Multiply][9], [MultiplyVector][10], [CreateRotation][22], [CreateScale][23], [CreateShear][24], [CreateTranslation][25], [CreateTransform][26], [RectangleProjection][27]

## Fields

| Name           | Summary              |
|----------------|----------------------|
| [M0][1]        |                      |
| [M1][2]        |                      |
| [M2][3]        |                      |
| [M3][4]        |                      |
| [M4][5]        |                      |
| [M5][6]        |                      |
| [Identity][20] | The identity matrix. |

## Properties

| Name          | Summary                          |
|---------------|----------------------------------|
| [Indexer][7]  |                                  |
| [Indexer][7]  |                                  |
| [Inverted][8] | Gets the inverse of this matrix. |

## Methods

| Name                       | Summary                                                                                                                     |
|----------------------------|-----------------------------------------------------------------------------------------------------------------------------|
| [Multiply][9]              | Multiplies a vector against this matrix.                                                                                    |
| [MultiplyVector][10]       | Multiplies a vector against this matrix ignoring the translational components.                                              |
| [Set][11]                  | Sets the components of this matrix.                                                                                         |
| [SetScale][12]             | Configures this matrix as a scaling matrix.                                                                                 |
| [SetShear][13]             | Configures this matrix as a shearing matrix.                                                                                |
| [SetRotation][14]          | Configures this matrix as a rotation matrix.                                                                                |
| [SetTranslation][15]       | Configures this matrix as a translation matrix.                                                                             |
| [Deconstruct][16]          |                                                                                                                             |
| [GetAffineScale][17]       | Extracts affine scaling components from this matrix.                                                                        |
| [GetAffineTranslation][18] | Extracts affine translational components from this matrix.                                                                  |
| [GetAffineRotation][19]    | Extracts affine rotational component (the angle) from this matrix.                                                          |
| [Inverse][21]              | Computes the inverse of this matrix.                                                                                        |
| [Inverse][21]              | Computes the inverse of the matrix and stores the resulting matrix into `dest` .                                            |
| [Multiply][9]              | Multiply two matrices together and store the result in `dest` .                                                             |
| [Multiply][9]              | Multiply two matrices together.                                                                                             |
| [Multiply][9]              | Multiplies a vector and matrix together and stores the resulting vector into `dest` .                                       |
| [MultiplyVector][10]       | Multiplies a vector and matrix together ignoring the translational components and stores the resulting vector into `dest` . |
| [Multiply][9]              | Multiplies a vector and matrix together.                                                                                    |
| [MultiplyVector][10]       | Multiplies a vector and matrix together ignoring the translational components.                                              |
| [CreateRotation][22]       | Constructs a new rotation matrix.                                                                                           |
| [CreateScale][23]          | Constructs a new scaling matrix.                                                                                            |
| [CreateScale][23]          | Constructs a new scaling matrix.                                                                                            |
| [CreateScale][23]          | Constructs a new scaling matrix.                                                                                            |
| [CreateScale][23]          | Constructs a new uniform scaling matrix.                                                                                    |
| [CreateShear][24]          | Constructs a new shearing matrix.                                                                                           |
| [CreateShear][24]          | Constructs a new shearing matrix.                                                                                           |
| [CreateTranslation][25]    | Constructs a new translation matrix.                                                                                        |
| [CreateTranslation][25]    | Constructs a new translation matrix.                                                                                        |
| [CreateTransform][26]      | Creates a transform matrix with postion, rotation and scale.                                                                |
| [CreateTransform][26]      | Creates a transform matrix with postion, rotation and scale.                                                                |
| [CreateTransform][26]      | Creates a transform matrix with postion, rotation and scale.                                                                |
| [RectangleProjection][27]  | Constructs a matrix that transforms a rectangular region to normalized screen coordinates.                                  |
| [RectangleProjection][27]  | Constructs a matrix that transforms a rectangular region to normalized screen coordinates.                                  |

[0]: ../../Heirloom.Core.md
[1]: Matrix/M0.md
[2]: Matrix/M1.md
[3]: Matrix/M2.md
[4]: Matrix/M3.md
[5]: Matrix/M4.md
[6]: Matrix/M5.md
[7]: Matrix/Indexer.md
[8]: Matrix/Inverted.md
[9]: Matrix/Multiply.md
[10]: Matrix/MultiplyVector.md
[11]: Matrix/Set.md
[12]: Matrix/SetScale.md
[13]: Matrix/SetShear.md
[14]: Matrix/SetRotation.md
[15]: Matrix/SetTranslation.md
[16]: Matrix/Deconstruct.md
[17]: Matrix/GetAffineScale.md
[18]: Matrix/GetAffineTranslation.md
[19]: Matrix/GetAffineRotation.md
[20]: Matrix/Identity.md
[21]: Matrix/Inverse.md
[22]: Matrix/CreateRotation.md
[23]: Matrix/CreateScale.md
[24]: Matrix/CreateShear.md
[25]: Matrix/CreateTranslation.md
[26]: Matrix/CreateTransform.md
[27]: Matrix/RectangleProjection.md
