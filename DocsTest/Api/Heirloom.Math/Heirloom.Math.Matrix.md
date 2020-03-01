# Heirloom.Math

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md)</small>  

## Matrix (Struct)
<small>**Namespace**: Heirloom.Math</sub></small>  
<small>**Interfaces**: IEquatable\<Matrix></small>  

A 2x3 transformation matrix.

| Fields                   | Summary              |
|--------------------------|----------------------|
| [M0](#M0FCA240B1)        |                      |
| [M1](#M19F49B54C)        |                      |
| [M2](#M2B753577B)        |                      |
| [M3](#M359FACC16)        |                      |
| [M4](#M48740131D)        |                      |
| [M5](#M529E787B8)        |                      |
| [Identity](#IDE5E3BFAE4) | The identity matrix. |

| Properties               | Summary                          |
|--------------------------|----------------------------------|
| [Item](#ITE8B5A2F95)     |                                  |
| [Item](#ITE8B5A2F95)     |                                  |
| [Inverted](#INVDE5124E3) | Gets the inverse of this matrix. |

| Methods                              | Summary                                                                                                                    |
|--------------------------------------|----------------------------------------------------------------------------------------------------------------------------|
| [Multiply](#MULD34B4AA2)             | Multiplies a vector against this matrix.                                                                                   |
| [MultiplyVector](#MULA8AB5E1)        | Multiplies a vector against this matrix ignoring the translational components.                                             |
| [SetScale](#SETAD4C1420)             | Configures this matrix as a scaling matrix.                                                                                |
| [SetShear](#SET6BFF6EFF)             | Configures this matrix as a shearing matrix.                                                                               |
| [SetRotation](#SET3CEB9E5C)          | Configures this matrix as a rotation matrix.                                                                               |
| [SetTranslation](#SET8706F609)       | Configures this matrix as a translation matrix.                                                                            |
| [Deconstruct](#DECC1884FDA)          |                                                                                                                            |
| [GetAffineScale](#GETC2509749)       | Extracts affine scaling components from this matrix.                                                                       |
| [GetAffineTranslation](#GET86F730A4) | Extracts affine translational components from this matrix.                                                                 |
| [GetAffineRotation](#GETD96D4299)    | Extracts affine rotational component (the angle) from this matrix.                                                         |
| [Inverse](#INVAD55C89E)              | Computes the inverse of this matrix.                                                                                       |
| [Inverse](#INVAD55C89E)              | Computes the inverse of the matrix and stores the resulting matrix into `dest`.                                            |
| [Multiply](#MULD34B4AA2)             | Multiply two matrices together and store the result in `dest`.                                                             |
| [Multiply](#MULD34B4AA2)             | Multiply two matrices together.                                                                                            |
| [Multiply](#MULD34B4AA2)             | Multiplies a vector and matrix together and stores the resulting vector into `dest`.                                       |
| [MultiplyVector](#MULA8AB5E1)        | Multiplies a vector and matrix together ignoring the translational components and stores the resulting vector into `dest`. |
| [Multiply](#MULD34B4AA2)             | Multiplies a vector and matrix together.                                                                                   |
| [MultiplyVector](#MULA8AB5E1)        | Multiplies a vector and matrix together ignoring the translational components.                                             |
| [CreateRotation](#CREF5925A8C)       | Constructs a new rotation matrix.                                                                                          |
| [CreateScale](#CRE3529FB8C)          | Constructs a new scaling matrix.                                                                                           |
| [CreateScale](#CRE3529FB8C)          | Constructs a new scaling matrix.                                                                                           |
| [CreateScale](#CRE3529FB8C)          | Constructs a new scaling matrix.                                                                                           |
| [CreateScale](#CRE3529FB8C)          | Constructs a new uniform scaling matrix.                                                                                   |
| [CreateShear](#CRE2A5B6875)          | Constructs a new shearing matrix.                                                                                          |
| [CreateShear](#CRE2A5B6875)          | Constructs a new shearing matrix.                                                                                          |
| [CreateTranslation](#CREFC0CD68F)    | Constructs a new translation matrix.                                                                                       |
| [CreateTranslation](#CREFC0CD68F)    | Constructs a new translation matrix.                                                                                       |
| [CreateTransform](#CRE8DAF75FA)      | Creates a transform matrix with postion, rotation and scale.                                                               |
| [CreateTransform](#CRE8DAF75FA)      | Creates a transform matrix with postion, rotation and scale.                                                               |
| [CreateTransform](#CRE8DAF75FA)      | Creates a transform matrix with postion, rotation and scale.                                                               |
| [RectangleProjection](#REC6922F69E)  | Constructs a matrix that transforms a rectangular region to normalized screen coordinates.                                 |
| [RectangleProjection](#REC6922F69E)  | Constructs a matrix that transforms a rectangular region to normalized screen coordinates.                                 |

### Fields

#### <a name="M0FCA240B1"></a>M0 : float

#### <a name="M19F49B54C"></a>M1 : float

#### <a name="M2B753577B"></a>M2 : float

#### <a name="M359FACC16"></a>M3 : float

#### <a name="M48740131D"></a>M4 : float

#### <a name="M529E787B8"></a>M5 : float

#### <a name="IDE5E3BFAE4"></a>Identity : [Matrix](Heirloom.Math.Matrix.md)
<small>`Read Only`</small>

The identity matrix.

#### <a name="IDE5E3BFAE4"></a>Identity : [Matrix](Heirloom.Math.Matrix.md)
<small>`Static`, `Read Only`</small>

The identity matrix.

### Constructors

#### Matrix(float m0, float m1, float m2, float m3, float m4, float m5)

### Properties

#### <a name="ITE8B5A2F95"></a>Item : float


#### <a name="ITE8B5A2F95"></a>Item : float


#### <a name="INVDE5124E3"></a>Inverted : [Matrix](Heirloom.Math.Matrix.md)

<small>`Read Only`</small>

Gets the inverse of this matrix.

### Methods

#### <a name="MULA01B3C26"></a>Multiply(in [Vector](Heirloom.Math.Vector.md) v) : [Vector](Heirloom.Math.Vector.md)

Multiplies a vector against this matrix.


#### <a name="MUL344EEFCB"></a>MultiplyVector(in [Vector](Heirloom.Math.Vector.md) v) : [Vector](Heirloom.Math.Vector.md)

Multiplies a vector against this matrix ignoring the translational components.


#### <a name="SETB8F3AE82"></a>SetScale(float sx, float sy) : void

Configures this matrix as a scaling matrix.


#### <a name="SET1455197D"></a>SetShear(float sx, float sy) : void

Configures this matrix as a shearing matrix.


#### <a name="SET629A8C2C"></a>SetRotation(float angle) : void

Configures this matrix as a rotation matrix.


#### <a name="SET9C40E643"></a>SetTranslation(float x, float y) : void

Configures this matrix as a translation matrix.


#### <a name="DECE7BF1534"></a>Deconstruct(out [Vector](Heirloom.Math.Vector.md) position, out float rotation, out [Vector](Heirloom.Math.Vector.md) scale) : void


#### <a name="GET6E6D9161"></a>GetAffineScale() : [Vector](Heirloom.Math.Vector.md)

Extracts affine scaling components from this matrix.

#### <a name="GETA3108D8"></a>GetAffineTranslation() : [Vector](Heirloom.Math.Vector.md)

Extracts affine translational components from this matrix.

#### <a name="GETFC5A2BF0"></a>GetAffineRotation() : float

Extracts affine rotational component (the angle) from this matrix.

#### <a name="INV15CA99E1"></a>Inverse(in [Matrix](Heirloom.Math.Matrix.md) a) : [Matrix](Heirloom.Math.Matrix.md)
<small>`Static`</small>

Computes the inverse of this matrix.


#### <a name="INVD9F20D2A"></a>Inverse(in [Matrix](Heirloom.Math.Matrix.md) a, ref [Matrix](Heirloom.Math.Matrix.md) dest) : void
<small>`Static`</small>

Computes the inverse of the matrix and stores the resulting matrix into `dest`.


#### <a name="MUL2EE9291A"></a>Multiply(in [Matrix](Heirloom.Math.Matrix.md) a, in [Matrix](Heirloom.Math.Matrix.md) b, ref [Matrix](Heirloom.Math.Matrix.md) dest) : void
<small>`Static`</small>

Multiply two matrices together and store the result in `dest`.


#### <a name="MULE5544DFB"></a>Multiply(in [Matrix](Heirloom.Math.Matrix.md) a, in [Matrix](Heirloom.Math.Matrix.md) b) : [Matrix](Heirloom.Math.Matrix.md)
<small>`Static`</small>

Multiply two matrices together.


#### <a name="MULA47B6226"></a>Multiply(in [Matrix](Heirloom.Math.Matrix.md) a, in [Vector](Heirloom.Math.Vector.md) v, ref [Vector](Heirloom.Math.Vector.md) dest) : void
<small>`Static`</small>

Multiplies a vector and matrix together and stores the resulting vector into `dest`.


#### <a name="MUL97F9D4D"></a>MultiplyVector(in [Matrix](Heirloom.Math.Matrix.md) a, in [Vector](Heirloom.Math.Vector.md) v, ref [Vector](Heirloom.Math.Vector.md) r) : void
<small>`Static`</small>

Multiplies a vector and matrix together ignoring the translational components and stores the resulting vector into `dest`.


#### <a name="MULF160C86F"></a>Multiply(in [Matrix](Heirloom.Math.Matrix.md) a, in [Vector](Heirloom.Math.Vector.md) v) : [Vector](Heirloom.Math.Vector.md)
<small>`Static`</small>

Multiplies a vector and matrix together.


#### <a name="MULC9427D0A"></a>MultiplyVector(in [Matrix](Heirloom.Math.Matrix.md) a, in [Vector](Heirloom.Math.Vector.md) v) : [Vector](Heirloom.Math.Vector.md)
<small>`Static`</small>

Multiplies a vector and matrix together ignoring the translational components.


#### <a name="CREFEEBBF99"></a>CreateRotation(float angle) : [Matrix](Heirloom.Math.Matrix.md)
<small>`Static`</small>

Constructs a new rotation matrix.


#### <a name="CRED808600D"></a>CreateScale(float sx, float sy) : [Matrix](Heirloom.Math.Matrix.md)
<small>`Static`</small>

Constructs a new scaling matrix.


#### <a name="CREAED1C584"></a>CreateScale(in [Size](Heirloom.Math.Size.md) scale) : [Matrix](Heirloom.Math.Matrix.md)
<small>`Static`</small>

Constructs a new scaling matrix.


#### <a name="CREA9915924"></a>CreateScale(in [Vector](Heirloom.Math.Vector.md) scale) : [Matrix](Heirloom.Math.Matrix.md)
<small>`Static`</small>

Constructs a new scaling matrix.


#### <a name="CRED84425C8"></a>CreateScale(float scale) : [Matrix](Heirloom.Math.Matrix.md)
<small>`Static`</small>

Constructs a new uniform scaling matrix.


#### <a name="CRE449942A2"></a>CreateShear(in [Vector](Heirloom.Math.Vector.md) shear) : [Matrix](Heirloom.Math.Matrix.md)
<small>`Static`</small>

Constructs a new shearing matrix.


#### <a name="CRE9A8AF65E"></a>CreateShear(float sx, float sy) : [Matrix](Heirloom.Math.Matrix.md)
<small>`Static`</small>

Constructs a new shearing matrix.


#### <a name="CRE5F85B0FC"></a>CreateTranslation(float x, float y) : [Matrix](Heirloom.Math.Matrix.md)
<small>`Static`</small>

Constructs a new translation matrix.


#### <a name="CREAB8C7C8B"></a>CreateTranslation(in [Vector](Heirloom.Math.Vector.md) vec) : [Matrix](Heirloom.Math.Matrix.md)
<small>`Static`</small>

Constructs a new translation matrix.


#### <a name="CRE453BC4C2"></a>CreateTransform(in float tx, in float ty, in float angle, in float sx, in float sy) : [Matrix](Heirloom.Math.Matrix.md)
<small>`Static`</small>

Creates a transform matrix with postion, rotation and scale.


#### <a name="CREA464B8A4"></a>CreateTransform(in [Vector](Heirloom.Math.Vector.md) position, float angle, in [Vector](Heirloom.Math.Vector.md) scale) : [Matrix](Heirloom.Math.Matrix.md)
<small>`Static`</small>

Creates a transform matrix with postion, rotation and scale.


#### <a name="CREC6CA49EB"></a>CreateTransform(in [Vector](Heirloom.Math.Vector.md) position, float angle, in float scale) : [Matrix](Heirloom.Math.Matrix.md)
<small>`Static`</small>

Creates a transform matrix with postion, rotation and scale.


#### <a name="REC4A6C6C04"></a>RectangleProjection([Rectangle](Heirloom.Math.Rectangle.md) rectangle) : [Matrix](Heirloom.Math.Matrix.md)
<small>`Static`</small>

Constructs a matrix that transforms a rectangular region to normalized screen coordinates.


#### <a name="REC1B4A2103"></a>RectangleProjection(float left, float top, float right, float bottom) : [Matrix](Heirloom.Math.Matrix.md)
<small>`Static`</small>

Constructs a matrix that transforms a rectangular region to normalized screen coordinates.


