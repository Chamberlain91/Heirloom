# Heirloom.Math

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md)</small>  

## Matrix (Struct)
<small>**Namespace**: Heirloom.Math</sub></small>  
<small>**Interfaces**: IEquatable\<Matrix></small>  

A 2x3 transformation matrix.

| Fields                | Summary              |
|-----------------------|----------------------|
| [M0](#M0FCA2)         |                      |
| [M1](#M19F49)         |                      |
| [M2](#M2B753)         |                      |
| [M3](#M359FA)         |                      |
| [M4](#M48740)         |                      |
| [M5](#M529E7)         |                      |
| [Identity](#IDEN5E3B) | The identity matrix. |

| Properties            | Summary                          |
|-----------------------|----------------------------------|
| [Item](#ITEM8B5A)     |                                  |
| [Item](#ITEM8B5A)     |                                  |
| [Inverted](#INVEDE51) | Gets the inverse of this matrix. |

| Methods                           | Summary                                                                                                                    |
|-----------------------------------|----------------------------------------------------------------------------------------------------------------------------|
| [Multiply](#MULTD34B)             | Multiplies a vector against this matrix.                                                                                   |
| [MultiplyVector](#MULTA8AB)       | Multiplies a vector against this matrix ignoring the translational components.                                             |
| [SetScale](#SETSAD4C)             | Configures this matrix as a scaling matrix.                                                                                |
| [SetShear](#SETS6BFF)             | Configures this matrix as a shearing matrix.                                                                               |
| [SetRotation](#SETR3CEB)          | Configures this matrix as a rotation matrix.                                                                               |
| [SetTranslation](#SETT8706)       | Configures this matrix as a translation matrix.                                                                            |
| [Deconstruct](#DECOC188)          |                                                                                                                            |
| [GetAffineScale](#GETAC250)       | Extracts affine scaling components from this matrix.                                                                       |
| [GetAffineTranslation](#GETA86F7) | Extracts affine translational components from this matrix.                                                                 |
| [GetAffineRotation](#GETAD96D)    | Extracts affine rotational component (the angle) from this matrix.                                                         |
| [Inverse](#INVEAD55)              | Computes the inverse of this matrix.                                                                                       |
| [Inverse](#INVEAD55)              | Computes the inverse of the matrix and stores the resulting matrix into `dest`.                                            |
| [Multiply](#MULTD34B)             | Multiply two matrices together and store the result in `dest`.                                                             |
| [Multiply](#MULTD34B)             | Multiply two matrices together.                                                                                            |
| [Multiply](#MULTD34B)             | Multiplies a vector and matrix together and stores the resulting vector into `dest`.                                       |
| [MultiplyVector](#MULTA8AB)       | Multiplies a vector and matrix together ignoring the translational components and stores the resulting vector into `dest`. |
| [Multiply](#MULTD34B)             | Multiplies a vector and matrix together.                                                                                   |
| [MultiplyVector](#MULTA8AB)       | Multiplies a vector and matrix together ignoring the translational components.                                             |
| [CreateRotation](#CREAF592)       | Constructs a new rotation matrix.                                                                                          |
| [CreateScale](#CREA3529)          | Constructs a new scaling matrix.                                                                                           |
| [CreateScale](#CREA3529)          | Constructs a new scaling matrix.                                                                                           |
| [CreateScale](#CREA3529)          | Constructs a new scaling matrix.                                                                                           |
| [CreateScale](#CREA3529)          | Constructs a new uniform scaling matrix.                                                                                   |
| [CreateShear](#CREA2A5B)          | Constructs a new shearing matrix.                                                                                          |
| [CreateShear](#CREA2A5B)          | Constructs a new shearing matrix.                                                                                          |
| [CreateTranslation](#CREAFC0C)    | Constructs a new translation matrix.                                                                                       |
| [CreateTranslation](#CREAFC0C)    | Constructs a new translation matrix.                                                                                       |
| [CreateTransform](#CREA8DAF)      | Creates a transform matrix with postion, rotation and scale.                                                               |
| [CreateTransform](#CREA8DAF)      | Creates a transform matrix with postion, rotation and scale.                                                               |
| [CreateTransform](#CREA8DAF)      | Creates a transform matrix with postion, rotation and scale.                                                               |
| [RectangleProjection](#RECT6922)  | Constructs a matrix that transforms a rectangular region to normalized screen coordinates.                                 |
| [RectangleProjection](#RECT6922)  | Constructs a matrix that transforms a rectangular region to normalized screen coordinates.                                 |

### Fields

#### <a name="M0FCA2"></a> M0 : float

#### <a name="M19F49"></a> M1 : float

#### <a name="M2B753"></a> M2 : float

#### <a name="M359FA"></a> M3 : float

#### <a name="M48740"></a> M4 : float

#### <a name="M529E7"></a> M5 : float

#### <a name="IDEN5E3B"></a> Identity : [Matrix](Heirloom.Math.Matrix.md)
<small>`Read Only`</small>

The identity matrix.

#### <a name="IDEN5E3B"></a> Identity : [Matrix](Heirloom.Math.Matrix.md)
<small>`Static`, `Read Only`</small>

The identity matrix.

### Constructors

#### Matrix(float m0, float m1, float m2, float m3, float m4, float m5)

### Properties

#### <a name="ITEM8B5A"></a> Item : float


#### <a name="ITEM8B5A"></a> Item : float


#### <a name="INVEDE51"></a> Inverted : [Matrix](Heirloom.Math.Matrix.md)

<small>`Read Only`</small>

Gets the inverse of this matrix.

### Methods

#### <a name="MULTA01B"></a> Multiply(in [Vector](Heirloom.Math.Vector.md) v) : [Vector](Heirloom.Math.Vector.md)

Multiplies a vector against this matrix.


#### <a name="MULT344E"></a> MultiplyVector(in [Vector](Heirloom.Math.Vector.md) v) : [Vector](Heirloom.Math.Vector.md)

Multiplies a vector against this matrix ignoring the translational components.


#### <a name="SETSB8F3"></a> SetScale(float sx, float sy) : void

Configures this matrix as a scaling matrix.


#### <a name="SETS1455"></a> SetShear(float sx, float sy) : void

Configures this matrix as a shearing matrix.


#### <a name="SETR629A"></a> SetRotation(float angle) : void

Configures this matrix as a rotation matrix.


#### <a name="SETT9C40"></a> SetTranslation(float x, float y) : void

Configures this matrix as a translation matrix.


#### <a name="DECOE7BF"></a> Deconstruct(out [Vector](Heirloom.Math.Vector.md) position, out float rotation, out [Vector](Heirloom.Math.Vector.md) scale) : void


#### <a name="GETA6E6D"></a> GetAffineScale() : [Vector](Heirloom.Math.Vector.md)

Extracts affine scaling components from this matrix.

#### <a name="GETAA310"></a> GetAffineTranslation() : [Vector](Heirloom.Math.Vector.md)

Extracts affine translational components from this matrix.

#### <a name="GETAFC5A"></a> GetAffineRotation() : float

Extracts affine rotational component (the angle) from this matrix.

#### <a name="INVE15CA"></a> Inverse(in [Matrix](Heirloom.Math.Matrix.md) a) : [Matrix](Heirloom.Math.Matrix.md)
<small>`Static`</small>

Computes the inverse of this matrix.


#### <a name="INVED9F2"></a> Inverse(in [Matrix](Heirloom.Math.Matrix.md) a, ref [Matrix](Heirloom.Math.Matrix.md) dest) : void
<small>`Static`</small>

Computes the inverse of the matrix and stores the resulting matrix into `dest`.


#### <a name="MULT2EE9"></a> Multiply(in [Matrix](Heirloom.Math.Matrix.md) a, in [Matrix](Heirloom.Math.Matrix.md) b, ref [Matrix](Heirloom.Math.Matrix.md) dest) : void
<small>`Static`</small>

Multiply two matrices together and store the result in `dest`.


#### <a name="MULTE554"></a> Multiply(in [Matrix](Heirloom.Math.Matrix.md) a, in [Matrix](Heirloom.Math.Matrix.md) b) : [Matrix](Heirloom.Math.Matrix.md)
<small>`Static`</small>

Multiply two matrices together.


#### <a name="MULTA47B"></a> Multiply(in [Matrix](Heirloom.Math.Matrix.md) a, in [Vector](Heirloom.Math.Vector.md) v, ref [Vector](Heirloom.Math.Vector.md) dest) : void
<small>`Static`</small>

Multiplies a vector and matrix together and stores the resulting vector into `dest`.


#### <a name="MULT97F9"></a> MultiplyVector(in [Matrix](Heirloom.Math.Matrix.md) a, in [Vector](Heirloom.Math.Vector.md) v, ref [Vector](Heirloom.Math.Vector.md) r) : void
<small>`Static`</small>

Multiplies a vector and matrix together ignoring the translational components and stores the resulting vector into `dest`.


#### <a name="MULTF160"></a> Multiply(in [Matrix](Heirloom.Math.Matrix.md) a, in [Vector](Heirloom.Math.Vector.md) v) : [Vector](Heirloom.Math.Vector.md)
<small>`Static`</small>

Multiplies a vector and matrix together.


#### <a name="MULTC942"></a> MultiplyVector(in [Matrix](Heirloom.Math.Matrix.md) a, in [Vector](Heirloom.Math.Vector.md) v) : [Vector](Heirloom.Math.Vector.md)
<small>`Static`</small>

Multiplies a vector and matrix together ignoring the translational components.


#### <a name="CREAFEEB"></a> CreateRotation(float angle) : [Matrix](Heirloom.Math.Matrix.md)
<small>`Static`</small>

Constructs a new rotation matrix.


#### <a name="CREAD808"></a> CreateScale(float sx, float sy) : [Matrix](Heirloom.Math.Matrix.md)
<small>`Static`</small>

Constructs a new scaling matrix.


#### <a name="CREAAED1"></a> CreateScale(in [Size](Heirloom.Math.Size.md) scale) : [Matrix](Heirloom.Math.Matrix.md)
<small>`Static`</small>

Constructs a new scaling matrix.


#### <a name="CREAA991"></a> CreateScale(in [Vector](Heirloom.Math.Vector.md) scale) : [Matrix](Heirloom.Math.Matrix.md)
<small>`Static`</small>

Constructs a new scaling matrix.


#### <a name="CREAD844"></a> CreateScale(float scale) : [Matrix](Heirloom.Math.Matrix.md)
<small>`Static`</small>

Constructs a new uniform scaling matrix.


#### <a name="CREA4499"></a> CreateShear(in [Vector](Heirloom.Math.Vector.md) shear) : [Matrix](Heirloom.Math.Matrix.md)
<small>`Static`</small>

Constructs a new shearing matrix.


#### <a name="CREA9A8A"></a> CreateShear(float sx, float sy) : [Matrix](Heirloom.Math.Matrix.md)
<small>`Static`</small>

Constructs a new shearing matrix.


#### <a name="CREA5F85"></a> CreateTranslation(float x, float y) : [Matrix](Heirloom.Math.Matrix.md)
<small>`Static`</small>

Constructs a new translation matrix.


#### <a name="CREAAB8C"></a> CreateTranslation(in [Vector](Heirloom.Math.Vector.md) vec) : [Matrix](Heirloom.Math.Matrix.md)
<small>`Static`</small>

Constructs a new translation matrix.


#### <a name="CREA453B"></a> CreateTransform(in float tx, in float ty, in float angle, in float sx, in float sy) : [Matrix](Heirloom.Math.Matrix.md)
<small>`Static`</small>

Creates a transform matrix with postion, rotation and scale.


#### <a name="CREAA464"></a> CreateTransform(in [Vector](Heirloom.Math.Vector.md) position, float angle, in [Vector](Heirloom.Math.Vector.md) scale) : [Matrix](Heirloom.Math.Matrix.md)
<small>`Static`</small>

Creates a transform matrix with postion, rotation and scale.


#### <a name="CREAC6CA"></a> CreateTransform(in [Vector](Heirloom.Math.Vector.md) position, float angle, in float scale) : [Matrix](Heirloom.Math.Matrix.md)
<small>`Static`</small>

Creates a transform matrix with postion, rotation and scale.


#### <a name="RECT4A6C"></a> RectangleProjection([Rectangle](Heirloom.Math.Rectangle.md) rectangle) : [Matrix](Heirloom.Math.Matrix.md)
<small>`Static`</small>

Constructs a matrix that transforms a rectangular region to normalized screen coordinates.


#### <a name="RECT1B4A"></a> RectangleProjection(float left, float top, float right, float bottom) : [Matrix](Heirloom.Math.Matrix.md)
<small>`Static`</small>

Constructs a matrix that transforms a rectangular region to normalized screen coordinates.


