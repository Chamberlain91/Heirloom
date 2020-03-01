# Heirloom.Math

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Math](../heirloom.math/heirloom.math.md)</small>  

## Vector (Struct)
<small>**Namespace**: Heirloom.Math</sub></small>  
<small>**Interfaces**: IEquatable\<Vector></small>  

Represents a vector with two single-precision floating-point values.

| Fields | Summary |
|-------|---------|
| [X](#XCDCAB7F6) | The x-coordinate of this vector. |
| [Y](#YCDCAB7F5) | The y-coordinate of this vector. |
| [Zero](#ZERC7D5C0B8) | A vector with value (0, 0). |
| [One](#ONE62466566) | A vector with value (1, 1). |
| [Right](#RIG1DA76FF8) | A vector with value (1, 0). |
| [Up](#UP52C519F9) | A vector with value (0, -1). |
| [Left](#LEF9A907773) | A vector with value (-1, 0). |
| [Down](#DOW1FFBB0EA) | A vector with value (0, 1). |

| Properties | Summary |
|------------|---------|
| [Length](#LEN6B366D7E) | Gets the magnitude of this vector. |
| [LengthSquared](#LEN3BB93C25) | Gets the squared magnitude of this vector. |
| [Normalized](#NOR9634A03D) | Gets a normalized copy of this vector. |
| [Perpendicular](#PER94285E6A) | Gets a perpendicular copy of this vector. |
| [Angle](#ANGE0C22609) | Gets the angle this vector is pointing with reference to `Heirloom.Math.Vector.Right`. |

| Methods | Summary |
|---------|---------|
| [Set](#SET61D5DB24) | Sets the components of this vector. |
| [Normalize](#NORF1B1D952) | Normalize this vector. |
| [Deconstruct](#DECC77F132C) |  |
| [GetMaxComponent](#GETEE48C8D1) | Gets the maximal component in the input vector. |
| [GetMinComponent](#GET4001D543) | Gets the minimal component in the input vector. |
| [Min](#MIN10D1F641) | Computes a new vector where each component is the minimum component in each respective input vector. |
| [Max](#MAX65B445E3) | Computes a new vector where each component is the maximum component in each respective input vector. |
| [Abs](#ABS5BBB213B) | Computes a new vector where each component is the absolute value of each component in the input vector. |
| [Distance](#DISBDE7EF4B) | Computes the euclidean distance between any two vectors. |
| [DistanceSquared](#DISF9D16F62) | Computes the squared euclidean distance between any two vectors. |
| [ManhattanDistance](#MAN7C53BFFB) | Computes the manhattan distance between any two vectors. |
| [Normalize](#NORCB79680E) | Normalizes the input vector and return it. |
| [Normalize](#NOR35FB39AE) | Normalizes the input vector. |
| [Lerp](#LERC07DC0F6) | Interpolate two vectors. |
| [FromAngle](#FROC8045460) | Creates a unit length vector with the given angle from the x-axis. |
| [AngleBetween](#ANGB6E02F7) | Computes the angle (in radians) between two vectors (using dot product). |
| [Rotate](#ROTC98E36E1) | Rotates a vector by the specified angle. |
| [Dot](#DOTD39E4FD3) | Computes the dot-product of two vectors. |
| [Cross](#CROD68FED42) | Computes the cross-product of two vectors. |
| [Cross](#CRO89D41722) | Computes the cross-product of a vector and a magnitude. |
| [Project](#PRO98E838A1) | Projects the first vector onto the second. |
| [Project](#PRO4E7CD797) | Projects a point onto a line segment. |
| [Reflect](#REF1E11E310) | Computes the reflection of the input vector about the specified axis. |
| [Floor](#FLOBE57D9D1) | Computes a new vector with the floor of each component of the input vector. |
| [Ceil](#CEID4AD6584) | Computes a new vector with the ceiling of each component of the input vector. |
| [Round](#ROUEF8816EF) | Computes a new vector with the rounded value of each component of the input vector. |
| [Fraction](#FRAF6056EF) | Computes a new vector with the fractional portion of each component of the input vector. |

### Fields

#### <a name="XCDCAB7F6"></a>X : float

The x-coordinate of this vector.

#### <a name="YCDCAB7F5"></a>Y : float

The y-coordinate of this vector.

#### <a name="ZERC7D5C0B8"></a>Zero : [Vector](heirloom.math.vector.md)
<small>`Read Only`</small>

A vector with value (0, 0).

#### <a name="ONE62466566"></a>One : [Vector](heirloom.math.vector.md)
<small>`Read Only`</small>

A vector with value (1, 1).

#### <a name="RIG1DA76FF8"></a>Right : [Vector](heirloom.math.vector.md)
<small>`Read Only`</small>

A vector with value (1, 0).

#### <a name="UP52C519F9"></a>Up : [Vector](heirloom.math.vector.md)
<small>`Read Only`</small>

A vector with value (0, -1).

#### <a name="LEF9A907773"></a>Left : [Vector](heirloom.math.vector.md)
<small>`Read Only`</small>

A vector with value (-1, 0).

#### <a name="DOW1FFBB0EA"></a>Down : [Vector](heirloom.math.vector.md)
<small>`Read Only`</small>

A vector with value (0, 1).

#### <a name="ZERC7D5C0B8"></a>Zero : [Vector](heirloom.math.vector.md)
<small>`Static`, `Read Only`</small>

A vector with value (0, 0).

#### <a name="ONE62466566"></a>One : [Vector](heirloom.math.vector.md)
<small>`Static`, `Read Only`</small>

A vector with value (1, 1).

#### <a name="RIG1DA76FF8"></a>Right : [Vector](heirloom.math.vector.md)
<small>`Static`, `Read Only`</small>

A vector with value (1, 0).

#### <a name="UP52C519F9"></a>Up : [Vector](heirloom.math.vector.md)
<small>`Static`, `Read Only`</small>

A vector with value (0, -1).

#### <a name="LEF9A907773"></a>Left : [Vector](heirloom.math.vector.md)
<small>`Static`, `Read Only`</small>

A vector with value (-1, 0).

#### <a name="DOW1FFBB0EA"></a>Down : [Vector](heirloom.math.vector.md)
<small>`Static`, `Read Only`</small>

A vector with value (0, 1).

### Constructors

#### Vector(float x, float y)

### Properties

#### <a name="LEN6B366D7E"></a>Length : float

<small>`Read Only`</small>

Gets the magnitude of this vector.

#### <a name="LEN3BB93C25"></a>LengthSquared : float

<small>`Read Only`</small>

Gets the squared magnitude of this vector.

#### <a name="NOR9634A03D"></a>Normalized : [Vector](heirloom.math.vector.md)

<small>`Read Only`</small>

Gets a normalized copy of this vector.

#### <a name="PER94285E6A"></a>Perpendicular : [Vector](heirloom.math.vector.md)

<small>`Read Only`</small>

Gets a perpendicular copy of this vector.

#### <a name="ANGE0C22609"></a>Angle : float

<small>`Read Only`</small>

Gets the angle this vector is pointing with reference to `Heirloom.Math.Vector.Right`.

### Methods

#### <a name="SET61D5DB24"></a>Set(float x, float y) : void


Sets the components of this vector.


#### <a name="NORF1B1D952"></a>Normalize() : void


Normalize this vector.

#### <a name="DECC77F132C"></a>Deconstruct(out float x, out float y) : void



#### <a name="GETEE48C8D1"></a>GetMaxComponent([Vector](heirloom.math.vector.md) vec) : float

<small>`Static`</small>

Gets the maximal component in the input vector.


#### <a name="GET4001D543"></a>GetMinComponent([Vector](heirloom.math.vector.md) vec) : float

<small>`Static`</small>

Gets the minimal component in the input vector.


#### <a name="MIN10D1F641"></a>Min([Vector](heirloom.math.vector.md) a, [Vector](heirloom.math.vector.md) b) : [Vector](heirloom.math.vector.md)

<small>`Static`</small>

Computes a new vector where each component is the minimum component in each respective input vector.


#### <a name="MAX65B445E3"></a>Max([Vector](heirloom.math.vector.md) a, [Vector](heirloom.math.vector.md) b) : [Vector](heirloom.math.vector.md)

<small>`Static`</small>

Computes a new vector where each component is the maximum component in each respective input vector.


#### <a name="ABS5BBB213B"></a>Abs([Vector](heirloom.math.vector.md) vec) : [Vector](heirloom.math.vector.md)

<small>`Static`</small>

Computes a new vector where each component is the absolute value of each component in the input vector.


#### <a name="DISBDE7EF4B"></a>Distance(in [Vector](heirloom.math.vector.md) a, in [Vector](heirloom.math.vector.md) b) : float

<small>`Static`</small>

Computes the euclidean distance between any two vectors.


#### <a name="DISF9D16F62"></a>DistanceSquared(in [Vector](heirloom.math.vector.md) a, in [Vector](heirloom.math.vector.md) b) : float

<small>`Static`</small>

Computes the squared euclidean distance between any two vectors.


#### <a name="MAN7C53BFFB"></a>ManhattanDistance(in [Vector](heirloom.math.vector.md) a, in [Vector](heirloom.math.vector.md) b) : float

<small>`Static`</small>

Computes the manhattan distance between any two vectors.


#### <a name="NORCB79680E"></a>Normalize([Vector](heirloom.math.vector.md) vec) : [Vector](heirloom.math.vector.md)

<small>`Static`</small>

Normalizes the input vector and return it.


#### <a name="NOR35FB39AE"></a>Normalize(ref [Vector](heirloom.math.vector.md) vec) : void

<small>`Static`</small>

Normalizes the input vector.


#### <a name="LERC07DC0F6"></a>Lerp([Vector](heirloom.math.vector.md) from, [Vector](heirloom.math.vector.md) to, float t) : [Vector](heirloom.math.vector.md)

<small>`Static`</small>

Interpolate two vectors.


#### <a name="FROC8045460"></a>FromAngle(float angle) : [Vector](heirloom.math.vector.md)

<small>`Static`</small>

Creates a unit length vector with the given angle from the x-axis.


#### <a name="ANGB6E02F7"></a>AngleBetween([Vector](heirloom.math.vector.md) a, [Vector](heirloom.math.vector.md) b) : float

<small>`Static`</small>

Computes the angle (in radians) between two vectors (using dot product).


#### <a name="ROTC98E36E1"></a>Rotate([Vector](heirloom.math.vector.md) v, float angle) : [Vector](heirloom.math.vector.md)

<small>`Static`</small>

Rotates a vector by the specified angle.


#### <a name="DOTD39E4FD3"></a>Dot(in [Vector](heirloom.math.vector.md) a, in [Vector](heirloom.math.vector.md) b) : float

<small>`Static`</small>

Computes the dot-product of two vectors.


#### <a name="CROD68FED42"></a>Cross(in [Vector](heirloom.math.vector.md) a, in [Vector](heirloom.math.vector.md) b) : float

<small>`Static`</small>

Computes the cross-product of two vectors.


#### <a name="CRO89D41722"></a>Cross(in [Vector](heirloom.math.vector.md) a, float s) : [Vector](heirloom.math.vector.md)

<small>`Static`</small>

Computes the cross-product of a vector and a magnitude.


#### <a name="PRO98E838A1"></a>Project(in [Vector](heirloom.math.vector.md) u, in [Vector](heirloom.math.vector.md) v) : float

<small>`Static`</small>

Projects the first vector onto the second.

<small>**u**: <param name="u"> The first vector. </param>  
</small>
<small>**v**: <param name="v"> The second vector. </param>  
</small>

#### <a name="PRO4E7CD797"></a>Project(in [Vector](heirloom.math.vector.md) start, in [Vector](heirloom.math.vector.md) end, in [Vector](heirloom.math.vector.md) point, bool clamp = True) : float

<small>`Static`</small>

Projects a point onto a line segment.

<small>**start**: <param name="start">Starting point of the line segment.</param>  
</small>
<small>**end**: <param name="end">Ending point of the line segment.</param>  
</small>
<small>**point**: <param name="point">Point to project.</param>  
</small>
<small>**clamp**: <param name="clamp">Should we clamp to the ends of the line segment?</param>  
</small>

#### <a name="REF1E11E310"></a>Reflect(in [Vector](heirloom.math.vector.md) v, in [Vector](heirloom.math.vector.md) axis) : [Vector](heirloom.math.vector.md)

<small>`Static`</small>

Computes the reflection of the input vector about the specified axis.


#### <a name="FLOBE57D9D1"></a>Floor([Vector](heirloom.math.vector.md) v) : [Vector](heirloom.math.vector.md)

<small>`Static`</small>

Computes a new vector with the floor of each component of the input vector.


#### <a name="CEID4AD6584"></a>Ceil([Vector](heirloom.math.vector.md) v) : [Vector](heirloom.math.vector.md)

<small>`Static`</small>

Computes a new vector with the ceiling of each component of the input vector.


#### <a name="ROUEF8816EF"></a>Round([Vector](heirloom.math.vector.md) v) : [Vector](heirloom.math.vector.md)

<small>`Static`</small>

Computes a new vector with the rounded value of each component of the input vector.


#### <a name="FRAF6056EF"></a>Fraction([Vector](heirloom.math.vector.md) v) : [Vector](heirloom.math.vector.md)

<small>`Static`</small>

Computes a new vector with the fractional portion of each component of the input vector.


