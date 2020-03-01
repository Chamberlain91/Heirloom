# Heirloom.Math

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Math](../heirloom.math/heirloom.math.md)</small>  

## Matrix (Struct)
<small>**Namespace**: Heirloom.Math</sub></small>  
<small>**Interfaces**: IEquatable\<Matrix></small>  

A 2x3 transformation matrix.

| Fields | Summary |
|-------|---------|
| [M0](#M0FCA240B1) |  |
| [M1](#M19F49B54C) |  |
| [M2](#M2B753577B) |  |
| [M3](#M359FACC16) |  |
| [M4](#M48740131D) |  |
| [M5](#M529E787B8) |  |
| [Identity](#IDE5E3BFAE4) | The identity matrix. |

| Properties | Summary |
|------------|---------|
| [Item](#ITE8B5A2F95) |  |
| [Item](#ITE8B5A2F95) |  |
| [Inverted](#INVDE5124E3) | Gets the inverse of this matrix. |

| Methods | Summary |
|---------|---------|
| [Multiply](#MULF1A535E6) | Multiplies a vector against this matrix. |
| [MultiplyVector](#MULBD66CB0B) | Multiplies a vector against this matrix ignoring the translational components. |
| [SetScale](#SETB8F3AE82) | Configures this matrix as a scaling matrix. |
| [SetShear](#SET1455197D) | Configures this matrix as a shearing matrix. |
| [SetRotation](#SET629A8C2C) | Configures this matrix as a rotation matrix. |
| [SetTranslation](#SET9C40E643) | Configures this matrix as a translation matrix. |
| [Deconstruct](#DECDA09DB4) |  |
| [GetAffineScale](#GETF370F101) | Extracts affine scaling components from this matrix. |
| [GetAffineTranslation](#GET9166DFB8) | Extracts affine translational components from this matrix. |
| [GetAffineRotation](#GETFC5A2BF0) | Extracts affine rotational component (the angle) from this matrix. |
| [Inverse](#INVA3959221) | Computes the inverse of this matrix. |
| [Inverse](#INV39363B2A) | Computes the inverse of the matrix and stores the resulting matrix into `dest`. |
| [Multiply](#MUL6F6CA0FA) | Multiply two matrices together and store the result in `dest`. |
| [Multiply](#MULFA2C665B) | Multiply two matrices together. |
| [Multiply](#MUL4B603006) | Multiplies a vector and matrix together and stores the resulting vector into `dest`. |
| [MultiplyVector](#MUL10CE7BED) | Multiplies a vector and matrix together ignoring the translational components and stores the resulting vector into `dest`. |
| [Multiply](#MUL9D1CD84F) | Multiplies a vector and matrix together. |
| [MultiplyVector](#MUL9DDA022A) | Multiplies a vector and matrix together ignoring the translational components. |
| [CreateRotation](#CRE299084F9) | Constructs a new rotation matrix. |
| [CreateScale](#CRE48319ED) | Constructs a new scaling matrix. |
| [CreateScale](#CRE40B02744) | Constructs a new scaling matrix. |
| [CreateScale](#CRE4E0AA024) | Constructs a new scaling matrix. |
| [CreateScale](#CREFD2C4CA8) | Constructs a new uniform scaling matrix. |
| [CreateShear](#CRE7DEB3462) | Constructs a new shearing matrix. |
| [CreateShear](#CRE79D8FBFE) | Constructs a new shearing matrix. |
| [CreateTranslation](#CRE3ED3B69C) | Constructs a new translation matrix. |
| [CreateTranslation](#CRE2A4A27CB) | Constructs a new translation matrix. |
| [CreateTransform](#CRE528DFAE2) | Creates a transform matrix with postion, rotation and scale. |
| [CreateTransform](#CRED8080FC4) | Creates a transform matrix with postion, rotation and scale. |
| [CreateTransform](#CRE7AE824EB) | Creates a transform matrix with postion, rotation and scale. |
| [RectangleProjection](#REC96D3F4C4) | Constructs a matrix that transforms a rectangular region to normalized screen coordinates. |
| [RectangleProjection](#REC70792FE3) | Constructs a matrix that transforms a rectangular region to normalized screen coordinates. |

### Fields

#### <a name="M0FCA240B1"></a>M0 : float

#### <a name="M19F49B54C"></a>M1 : float

#### <a name="M2B753577B"></a>M2 : float

#### <a name="M359FACC16"></a>M3 : float

#### <a name="M48740131D"></a>M4 : float

#### <a name="M529E787B8"></a>M5 : float

#### <a name="IDE5E3BFAE4"></a>Identity : [Matrix](heirloom.math.matrix.md)
<small>`Read Only`</small>

The identity matrix.

#### <a name="IDE5E3BFAE4"></a>Identity : [Matrix](heirloom.math.matrix.md)
<small>`Static`, `Read Only`</small>

The identity matrix.

### Constructors

#### Matrix(float m0, float m1, float m2, float m3, float m4, float m5)

### Properties

#### <a name="ITE8B5A2F95"></a>Item : float


#### <a name="ITE8B5A2F95"></a>Item : float


#### <a name="INVDE5124E3"></a>Inverted : [Matrix](heirloom.math.matrix.md)

<small>`Read Only`</small>

Gets the inverse of this matrix.

### Methods

#### <a name="MULF1A535E6"></a>Multiply(in [Vector](heirloom.math.vector.md) v) : [Vector](heirloom.math.vector.md)


Multiplies a vector against this matrix.


#### <a name="MULBD66CB0B"></a>MultiplyVector(in [Vector](heirloom.math.vector.md) v) : [Vector](heirloom.math.vector.md)


Multiplies a vector against this matrix ignoring the translational components.


#### <a name="SETB8F3AE82"></a>SetScale(float sx, float sy) : void


Configures this matrix as a scaling matrix.


#### <a name="SET1455197D"></a>SetShear(float sx, float sy) : void


Configures this matrix as a shearing matrix.


#### <a name="SET629A8C2C"></a>SetRotation(float angle) : void


Configures this matrix as a rotation matrix.


#### <a name="SET9C40E643"></a>SetTranslation(float x, float y) : void


Configures this matrix as a translation matrix.


#### <a name="DECDA09DB4"></a>Deconstruct(out [Vector](heirloom.math.vector.md) position, out float rotation, out [Vector](heirloom.math.vector.md) scale) : void



#### <a name="GETF370F101"></a>GetAffineScale() : [Vector](heirloom.math.vector.md)


Extracts affine scaling components from this matrix.

#### <a name="GET9166DFB8"></a>GetAffineTranslation() : [Vector](heirloom.math.vector.md)


Extracts affine translational components from this matrix.

#### <a name="GETFC5A2BF0"></a>GetAffineRotation() : float


Extracts affine rotational component (the angle) from this matrix.

#### <a name="INVA3959221"></a>Inverse(in [Matrix](heirloom.math.matrix.md) a) : [Matrix](heirloom.math.matrix.md)

<small>`Static`</small>

Computes the inverse of this matrix.


#### <a name="INV39363B2A"></a>Inverse(in [Matrix](heirloom.math.matrix.md) a, ref [Matrix](heirloom.math.matrix.md) dest) : void

<small>`Static`</small>

Computes the inverse of the matrix and stores the resulting matrix into `dest`.


#### <a name="MUL6F6CA0FA"></a>Multiply(in [Matrix](heirloom.math.matrix.md) a, in [Matrix](heirloom.math.matrix.md) b, ref [Matrix](heirloom.math.matrix.md) dest) : void

<small>`Static`</small>

Multiply two matrices together and store the result in `dest`.


#### <a name="MULFA2C665B"></a>Multiply(in [Matrix](heirloom.math.matrix.md) a, in [Matrix](heirloom.math.matrix.md) b) : [Matrix](heirloom.math.matrix.md)

<small>`Static`</small>

Multiply two matrices together.


#### <a name="MUL4B603006"></a>Multiply(in [Matrix](heirloom.math.matrix.md) a, in [Vector](heirloom.math.vector.md) v, ref [Vector](heirloom.math.vector.md) dest) : void

<small>`Static`</small>

Multiplies a vector and matrix together and stores the resulting vector into `dest`.


#### <a name="MUL10CE7BED"></a>MultiplyVector(in [Matrix](heirloom.math.matrix.md) a, in [Vector](heirloom.math.vector.md) v, ref [Vector](heirloom.math.vector.md) r) : void

<small>`Static`</small>

Multiplies a vector and matrix together ignoring the translational components and stores the resulting vector into `dest`.


#### <a name="MUL9D1CD84F"></a>Multiply(in [Matrix](heirloom.math.matrix.md) a, in [Vector](heirloom.math.vector.md) v) : [Vector](heirloom.math.vector.md)

<small>`Static`</small>

Multiplies a vector and matrix together.


#### <a name="MUL9DDA022A"></a>MultiplyVector(in [Matrix](heirloom.math.matrix.md) a, in [Vector](heirloom.math.vector.md) v) : [Vector](heirloom.math.vector.md)

<small>`Static`</small>

Multiplies a vector and matrix together ignoring the translational components.


#### <a name="CRE299084F9"></a>CreateRotation(float angle) : [Matrix](heirloom.math.matrix.md)

<small>`Static`</small>

Constructs a new rotation matrix.


#### <a name="CRE48319ED"></a>CreateScale(float sx, float sy) : [Matrix](heirloom.math.matrix.md)

<small>`Static`</small>

Constructs a new scaling matrix.


#### <a name="CRE40B02744"></a>CreateScale(in [Size](heirloom.math.size.md) scale) : [Matrix](heirloom.math.matrix.md)

<small>`Static`</small>

Constructs a new scaling matrix.


#### <a name="CRE4E0AA024"></a>CreateScale(in [Vector](heirloom.math.vector.md) scale) : [Matrix](heirloom.math.matrix.md)

<small>`Static`</small>

Constructs a new scaling matrix.


#### <a name="CREFD2C4CA8"></a>CreateScale(float scale) : [Matrix](heirloom.math.matrix.md)

<small>`Static`</small>

Constructs a new uniform scaling matrix.


#### <a name="CRE7DEB3462"></a>CreateShear(in [Vector](heirloom.math.vector.md) shear) : [Matrix](heirloom.math.matrix.md)

<small>`Static`</small>

Constructs a new shearing matrix.


#### <a name="CRE79D8FBFE"></a>CreateShear(float sx, float sy) : [Matrix](heirloom.math.matrix.md)

<small>`Static`</small>

Constructs a new shearing matrix.


#### <a name="CRE3ED3B69C"></a>CreateTranslation(float x, float y) : [Matrix](heirloom.math.matrix.md)

<small>`Static`</small>

Constructs a new translation matrix.


#### <a name="CRE2A4A27CB"></a>CreateTranslation(in [Vector](heirloom.math.vector.md) vec) : [Matrix](heirloom.math.matrix.md)

<small>`Static`</small>

Constructs a new translation matrix.


#### <a name="CRE528DFAE2"></a>CreateTransform(in float tx, in float ty, in float angle, in float sx, in float sy) : [Matrix](heirloom.math.matrix.md)

<small>`Static`</small>

Creates a transform matrix with postion, rotation and scale.


#### <a name="CRED8080FC4"></a>CreateTransform(in [Vector](heirloom.math.vector.md) position, float angle, in [Vector](heirloom.math.vector.md) scale) : [Matrix](heirloom.math.matrix.md)

<small>`Static`</small>

Creates a transform matrix with postion, rotation and scale.


#### <a name="CRE7AE824EB"></a>CreateTransform(in [Vector](heirloom.math.vector.md) position, float angle, in float scale) : [Matrix](heirloom.math.matrix.md)

<small>`Static`</small>

Creates a transform matrix with postion, rotation and scale.


#### <a name="REC96D3F4C4"></a>RectangleProjection([Rectangle](heirloom.math.rectangle.md) rectangle) : [Matrix](heirloom.math.matrix.md)

<small>`Static`</small>

Constructs a matrix that transforms a rectangular region to normalized screen coordinates.


#### <a name="REC70792FE3"></a>RectangleProjection(float left, float top, float right, float bottom) : [Matrix](heirloom.math.matrix.md)

<small>`Static`</small>

Constructs a matrix that transforms a rectangular region to normalized screen coordinates.


