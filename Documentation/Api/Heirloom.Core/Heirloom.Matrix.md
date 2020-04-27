# Matrix

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  
> **Namespace**: [Heirloom][0]  

A 2x3 transformation matrix.

```cs
public struct Matrix : IEquatable<Matrix>
```

--------------------------------------------------------------------------------

**Inherits**: IEquatable\<Matrix>

**Fields**: [M0][1], [M1][2], [M2][3], [M3][4], [M4][5], [M5][6]

**Properties**: [Item][7], [Inverted][8]

**Methods**: [Multiply][9], [MultiplyVector][10], [Set][11], [SetScale][12], [SetShear][13], [SetRotation][14], [SetTranslation][15], [Deconstruct][16], [GetAffineScale][17], [GetAffineTranslation][18], [GetAffineRotation][19]

**Static Fields**: [Identity][20]

**Static Methods**: [Inverse][21], [Multiply][9], [MultiplyVector][10], [CreateRotation][22], [CreateScale][23], [CreateShear][24], [CreateTranslation][25], [CreateTransform][26], [RectangleProjection][27]

--------------------------------------------------------------------------------

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
| [Item][7]     |                                  |
| [Item][7]     |                                  |
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

[0]: ../Heirloom.Core.md
[1]: Heirloom.Matrix.M0.md
[2]: Heirloom.Matrix.M1.md
[3]: Heirloom.Matrix.M2.md
[4]: Heirloom.Matrix.M3.md
[5]: Heirloom.Matrix.M4.md
[6]: Heirloom.Matrix.M5.md
[7]: Heirloom.Matrix.Item.md
[8]: Heirloom.Matrix.Inverted.md
[9]: Heirloom.Matrix.Multiply.md
[10]: Heirloom.Matrix.MultiplyVector.md
[11]: Heirloom.Matrix.Set.md
[12]: Heirloom.Matrix.SetScale.md
[13]: Heirloom.Matrix.SetShear.md
[14]: Heirloom.Matrix.SetRotation.md
[15]: Heirloom.Matrix.SetTranslation.md
[16]: Heirloom.Matrix.Deconstruct.md
[17]: Heirloom.Matrix.GetAffineScale.md
[18]: Heirloom.Matrix.GetAffineTranslation.md
[19]: Heirloom.Matrix.GetAffineRotation.md
[20]: Heirloom.Matrix.Identity.md
[21]: Heirloom.Matrix.Inverse.md
[22]: Heirloom.Matrix.CreateRotation.md
[23]: Heirloom.Matrix.CreateScale.md
[24]: Heirloom.Matrix.CreateShear.md
[25]: Heirloom.Matrix.CreateTranslation.md
[26]: Heirloom.Matrix.CreateTransform.md
[27]: Heirloom.Matrix.RectangleProjection.md
