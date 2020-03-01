# Heirloom.Math

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md)</small>  

## Vector (Struct)
<small>**Namespace**: Heirloom.Math</sub></small>  
<small>**Interfaces**: IEquatable\<Vector></small>  

Represents a vector with two single-precision floating-point values.

| Fields             | Summary                          |
|--------------------|----------------------------------|
| [X](#XCDCA)        | The x-coordinate of this vector. |
| [Y](#YCDCA)        | The y-coordinate of this vector. |
| [Zero](#ZEROC7D5)  | A vector with value (0, 0).      |
| [One](#ONE6246)    | A vector with value (1, 1).      |
| [Right](#RIGH1DA7) | A vector with value (1, 0).      |
| [Up](#UP52C5)      | A vector with value (0, -1).     |
| [Left](#LEFT9A90)  | A vector with value (-1, 0).     |
| [Down](#DOWN1FFB)  | A vector with value (0, 1).      |

| Properties                 | Summary                                                                                |
|----------------------------|----------------------------------------------------------------------------------------|
| [Length](#LENG6B36)        | Gets the magnitude of this vector.                                                     |
| [LengthSquared](#LENG3BB9) | Gets the squared magnitude of this vector.                                             |
| [Normalized](#NORM9634)    | Gets a normalized copy of this vector.                                                 |
| [Perpendicular](#PERP9428) | Gets a perpendicular copy of this vector.                                              |
| [Angle](#ANGLE0C2)         | Gets the angle this vector is pointing with reference to `Heirloom.Math.Vector.Right`. |

| Methods                        | Summary                                                                                                 |
|--------------------------------|---------------------------------------------------------------------------------------------------------|
| [Set](#SET5F78)                | Sets the components of this vector.                                                                     |
| [Normalize](#NORM155E)         | Normalize this vector.                                                                                  |
| [Deconstruct](#DECOC188)       |                                                                                                         |
| [GetMaxComponent](#GETM45DC)   | Gets the maximal component in the input vector.                                                         |
| [GetMinComponent](#GETMA14A)   | Gets the minimal component in the input vector.                                                         |
| [Min](#MINBF9E)                | Computes a new vector where each component is the minimum component in each respective input vector.    |
| [Max](#MAXD4DA)                | Computes a new vector where each component is the maximum component in each respective input vector.    |
| [Abs](#ABSECE4)                | Computes a new vector where each component is the absolute value of each component in the input vector. |
| [Distance](#DIST3A36)          | Computes the euclidean distance between any two vectors.                                                |
| [DistanceSquared](#DIST66C8)   | Computes the squared euclidean distance between any two vectors.                                        |
| [ManhattanDistance](#MANHC65C) | Computes the manhattan distance between any two vectors.                                                |
| [Normalize](#NORM155E)         | Normalizes the input vector and return it.                                                              |
| [Normalize](#NORM155E)         | Normalizes the input vector.                                                                            |
| [Lerp](#LERP252E)              | Interpolate two vectors.                                                                                |
| [FromAngle](#FROM9C6B)         | Creates a unit length vector with the given angle from the x-axis.                                      |
| [AngleBetween](#ANGLC045)      | Computes the angle (in radians) between two vectors (using dot product).                                |
| [Rotate](#ROTAC750)            | Rotates a vector by the specified angle.                                                                |
| [Dot](#DOT4EDD)                | Computes the dot-product of two vectors.                                                                |
| [Cross](#CROSCD89)             | Computes the cross-product of two vectors.                                                              |
| [Cross](#CROSCD89)             | Computes the cross-product of a vector and a magnitude.                                                 |
| [Project](#PROJAD47)           | Projects the first vector onto the second.                                                              |
| [Project](#PROJAD47)           | Projects a point onto a line segment.                                                                   |
| [Reflect](#REFL3A0C)           | Computes the reflection of the input vector about the specified axis.                                   |
| [Floor](#FLOO8101)             | Computes a new vector with the floor of each component of the input vector.                             |
| [Ceil](#CEILAFCC)              | Computes a new vector with the ceiling of each component of the input vector.                           |
| [Round](#ROUN73CA)             | Computes a new vector with the rounded value of each component of the input vector.                     |
| [Fraction](#FRAC1E83)          | Computes a new vector with the fractional portion of each component of the input vector.                |

### Fields

#### <a name="XCDCA"></a> X : float

The x-coordinate of this vector.

#### <a name="YCDCA"></a> Y : float

The y-coordinate of this vector.

#### <a name="ZEROC7D5"></a> Zero : [Vector](Heirloom.Math.Vector.md)
<small>`Read Only`</small>

A vector with value (0, 0).

#### <a name="ONE6246"></a> One : [Vector](Heirloom.Math.Vector.md)
<small>`Read Only`</small>

A vector with value (1, 1).

#### <a name="RIGH1DA7"></a> Right : [Vector](Heirloom.Math.Vector.md)
<small>`Read Only`</small>

A vector with value (1, 0).

#### <a name="UP52C5"></a> Up : [Vector](Heirloom.Math.Vector.md)
<small>`Read Only`</small>

A vector with value (0, -1).

#### <a name="LEFT9A90"></a> Left : [Vector](Heirloom.Math.Vector.md)
<small>`Read Only`</small>

A vector with value (-1, 0).

#### <a name="DOWN1FFB"></a> Down : [Vector](Heirloom.Math.Vector.md)
<small>`Read Only`</small>

A vector with value (0, 1).

#### <a name="ZEROC7D5"></a> Zero : [Vector](Heirloom.Math.Vector.md)
<small>`Static`, `Read Only`</small>

A vector with value (0, 0).

#### <a name="ONE6246"></a> One : [Vector](Heirloom.Math.Vector.md)
<small>`Static`, `Read Only`</small>

A vector with value (1, 1).

#### <a name="RIGH1DA7"></a> Right : [Vector](Heirloom.Math.Vector.md)
<small>`Static`, `Read Only`</small>

A vector with value (1, 0).

#### <a name="UP52C5"></a> Up : [Vector](Heirloom.Math.Vector.md)
<small>`Static`, `Read Only`</small>

A vector with value (0, -1).

#### <a name="LEFT9A90"></a> Left : [Vector](Heirloom.Math.Vector.md)
<small>`Static`, `Read Only`</small>

A vector with value (-1, 0).

#### <a name="DOWN1FFB"></a> Down : [Vector](Heirloom.Math.Vector.md)
<small>`Static`, `Read Only`</small>

A vector with value (0, 1).

### Constructors

#### Vector(float x, float y)

### Properties

#### <a name="LENG6B36"></a> Length : float

<small>`Read Only`</small>

Gets the magnitude of this vector.

#### <a name="LENG3BB9"></a> LengthSquared : float

<small>`Read Only`</small>

Gets the squared magnitude of this vector.

#### <a name="NORM9634"></a> Normalized : [Vector](Heirloom.Math.Vector.md)

<small>`Read Only`</small>

Gets a normalized copy of this vector.

#### <a name="PERP9428"></a> Perpendicular : [Vector](Heirloom.Math.Vector.md)

<small>`Read Only`</small>

Gets a perpendicular copy of this vector.

#### <a name="ANGLE0C2"></a> Angle : float

<small>`Read Only`</small>

Gets the angle this vector is pointing with reference to `Heirloom.Math.Vector.Right`.

### Methods

#### <a name="SET(61D5"></a> Set(float x, float y) : void

Sets the components of this vector.


#### <a name="NORMF1B1"></a> Normalize() : void

Normalize this vector.

#### <a name="DECOC77F"></a> Deconstruct(out float x, out float y) : void


#### <a name="GETM2269"></a> GetMaxComponent([Vector](Heirloom.Math.Vector.md) vec) : float
<small>`Static`</small>

Gets the maximal component in the input vector.


#### <a name="GETM5870"></a> GetMinComponent([Vector](Heirloom.Math.Vector.md) vec) : float
<small>`Static`</small>

Gets the minimal component in the input vector.


#### <a name="MIN(FA9D"></a> Min([Vector](Heirloom.Math.Vector.md) a, [Vector](Heirloom.Math.Vector.md) b) : [Vector](Heirloom.Math.Vector.md)
<small>`Static`</small>

Computes a new vector where each component is the minimum component in each respective input vector.


#### <a name="MAX(90EC"></a> Max([Vector](Heirloom.Math.Vector.md) a, [Vector](Heirloom.Math.Vector.md) b) : [Vector](Heirloom.Math.Vector.md)
<small>`Static`</small>

Computes a new vector where each component is the maximum component in each respective input vector.


#### <a name="ABS(6876"></a> Abs([Vector](Heirloom.Math.Vector.md) vec) : [Vector](Heirloom.Math.Vector.md)
<small>`Static`</small>

Computes a new vector where each component is the absolute value of each component in the input vector.


#### <a name="DIST8C9D"></a> Distance(in [Vector](Heirloom.Math.Vector.md) a, in [Vector](Heirloom.Math.Vector.md) b) : float
<small>`Static`</small>

Computes the euclidean distance between any two vectors.


#### <a name="DISTBA95"></a> DistanceSquared(in [Vector](Heirloom.Math.Vector.md) a, in [Vector](Heirloom.Math.Vector.md) b) : float
<small>`Static`</small>

Computes the squared euclidean distance between any two vectors.


#### <a name="MANH9EDD"></a> ManhattanDistance(in [Vector](Heirloom.Math.Vector.md) a, in [Vector](Heirloom.Math.Vector.md) b) : float
<small>`Static`</small>

Computes the manhattan distance between any two vectors.


#### <a name="NORME4E6"></a> Normalize([Vector](Heirloom.Math.Vector.md) vec) : [Vector](Heirloom.Math.Vector.md)
<small>`Static`</small>

Normalizes the input vector and return it.


#### <a name="NORM3BED"></a> Normalize(ref [Vector](Heirloom.Math.Vector.md) vec) : void
<small>`Static`</small>

Normalizes the input vector.


#### <a name="LERPD952"></a> Lerp([Vector](Heirloom.Math.Vector.md) from, [Vector](Heirloom.Math.Vector.md) to, float t) : [Vector](Heirloom.Math.Vector.md)
<small>`Static`</small>

Interpolate two vectors.


#### <a name="FROME114"></a> FromAngle(float angle) : [Vector](Heirloom.Math.Vector.md)
<small>`Static`</small>

Creates a unit length vector with the given angle from the x-axis.


#### <a name="ANGLFB6C"></a> AngleBetween([Vector](Heirloom.Math.Vector.md) a, [Vector](Heirloom.Math.Vector.md) b) : float
<small>`Static`</small>

Computes the angle (in radians) between two vectors (using dot product).


#### <a name="ROTAE095"></a> Rotate([Vector](Heirloom.Math.Vector.md) v, float angle) : [Vector](Heirloom.Math.Vector.md)
<small>`Static`</small>

Rotates a vector by the specified angle.


#### <a name="DOT(31B7"></a> Dot(in [Vector](Heirloom.Math.Vector.md) a, in [Vector](Heirloom.Math.Vector.md) b) : float
<small>`Static`</small>

Computes the dot-product of two vectors.


#### <a name="CROSE54D"></a> Cross(in [Vector](Heirloom.Math.Vector.md) a, in [Vector](Heirloom.Math.Vector.md) b) : float
<small>`Static`</small>

Computes the cross-product of two vectors.


#### <a name="CROS2759"></a> Cross(in [Vector](Heirloom.Math.Vector.md) a, float s) : [Vector](Heirloom.Math.Vector.md)
<small>`Static`</small>

Computes the cross-product of a vector and a magnitude.


#### <a name="PROJ9EB2"></a> Project(in [Vector](Heirloom.Math.Vector.md) u, in [Vector](Heirloom.Math.Vector.md) v) : float
<small>`Static`</small>

Projects the first vector onto the second.

<small>**u**: <param name="u"> The first vector. </param></small>  
<small>**v**: <param name="v"> The second vector. </param></small>  

#### <a name="PROJ9DBF"></a> Project(in [Vector](Heirloom.Math.Vector.md) start, in [Vector](Heirloom.Math.Vector.md) end, in [Vector](Heirloom.Math.Vector.md) point, bool clamp = True) : float
<small>`Static`</small>

Projects a point onto a line segment.

<small>**start**: <param name="start">Starting point of the line segment.</param></small>  
<small>**end**: <param name="end">Ending point of the line segment.</param></small>  
<small>**point**: <param name="point">Point to project.</param></small>  
<small>**clamp**: <param name="clamp">Should we clamp to the ends of the line segment?</param></small>  

#### <a name="REFL8680"></a> Reflect(in [Vector](Heirloom.Math.Vector.md) v, in [Vector](Heirloom.Math.Vector.md) axis) : [Vector](Heirloom.Math.Vector.md)
<small>`Static`</small>

Computes the reflection of the input vector about the specified axis.


#### <a name="FLOO4BDB"></a> Floor([Vector](Heirloom.Math.Vector.md) v) : [Vector](Heirloom.Math.Vector.md)
<small>`Static`</small>

Computes a new vector with the floor of each component of the input vector.


#### <a name="CEIL7F61"></a> Ceil([Vector](Heirloom.Math.Vector.md) v) : [Vector](Heirloom.Math.Vector.md)
<small>`Static`</small>

Computes a new vector with the ceiling of each component of the input vector.


#### <a name="ROUNBDFB"></a> Round([Vector](Heirloom.Math.Vector.md) v) : [Vector](Heirloom.Math.Vector.md)
<small>`Static`</small>

Computes a new vector with the rounded value of each component of the input vector.


#### <a name="FRACFD17"></a> Fraction([Vector](Heirloom.Math.Vector.md) v) : [Vector](Heirloom.Math.Vector.md)
<small>`Static`</small>

Computes a new vector with the fractional portion of each component of the input vector.


