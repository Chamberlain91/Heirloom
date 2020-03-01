# Heirloom.Math

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md)</small>  

## Vector (Struct)
<small>**Namespace**: Heirloom.Math</sub></small>  
<small>**Interfaces**: IEquatable\<Vector></small>  

Represents a vector with two single-precision floating-point values.

| Fields                | Summary                          |
|-----------------------|----------------------------------|
| [X](#XCDCAB7F6)       | The x-coordinate of this vector. |
| [Y](#YCDCAB7F5)       | The y-coordinate of this vector. |
| [Zero](#ZERC7D5C0B8)  | A vector with value (0, 0).      |
| [One](#ONE62466566)   | A vector with value (1, 1).      |
| [Right](#RIG1DA76FF8) | A vector with value (1, 0).      |
| [Up](#UP52C519F9)     | A vector with value (0, -1).     |
| [Left](#LEF9A907773)  | A vector with value (-1, 0).     |
| [Down](#DOW1FFBB0EA)  | A vector with value (0, 1).      |

| Properties                    | Summary                                                                                |
|-------------------------------|----------------------------------------------------------------------------------------|
| [Length](#LEN6B366D7E)        | Gets the magnitude of this vector.                                                     |
| [LengthSquared](#LEN3BB93C25) | Gets the squared magnitude of this vector.                                             |
| [Normalized](#NOR9634A03D)    | Gets a normalized copy of this vector.                                                 |
| [Perpendicular](#PER94285E6A) | Gets a perpendicular copy of this vector.                                              |
| [Angle](#ANGE0C22609)         | Gets the angle this vector is pointing with reference to `Heirloom.Math.Vector.Right`. |

| Methods                           | Summary                                                                                                 |
|-----------------------------------|---------------------------------------------------------------------------------------------------------|
| [Set](#SET5F786982)               | Sets the components of this vector.                                                                     |
| [Normalize](#NOR155E4A29)         | Normalize this vector.                                                                                  |
| [Deconstruct](#DECC1884FDA)       |                                                                                                         |
| [GetMaxComponent](#GET45DCC999)   | Gets the maximal component in the input vector.                                                         |
| [GetMinComponent](#GETA14AFEBB)   | Gets the minimal component in the input vector.                                                         |
| [Min](#MINBF9EF002)               | Computes a new vector where each component is the minimum component in each respective input vector.    |
| [Max](#MAXD4DA94E4)               | Computes a new vector where each component is the maximum component in each respective input vector.    |
| [Abs](#ABSECE4369A)               | Computes a new vector where each component is the absolute value of each component in the input vector. |
| [Distance](#DIS3A367EAF)          | Computes the euclidean distance between any two vectors.                                                |
| [DistanceSquared](#DIS66C8121E)   | Computes the squared euclidean distance between any two vectors.                                        |
| [ManhattanDistance](#MANC65C6C67) | Computes the manhattan distance between any two vectors.                                                |
| [Normalize](#NOR155E4A29)         | Normalizes the input vector and return it.                                                              |
| [Normalize](#NOR155E4A29)         | Normalizes the input vector.                                                                            |
| [Lerp](#LER252E49EB)              | Interpolate two vectors.                                                                                |
| [FromAngle](#FRO9C6B7947)         | Creates a unit length vector with the given angle from the x-axis.                                      |
| [AngleBetween](#ANGC045F7D3)      | Computes the angle (in radians) between two vectors (using dot product).                                |
| [Rotate](#ROTC750141D)            | Rotates a vector by the specified angle.                                                                |
| [Dot](#DOT4EDD927)                | Computes the dot-product of two vectors.                                                                |
| [Cross](#CROCD89B4EE)             | Computes the cross-product of two vectors.                                                              |
| [Cross](#CROCD89B4EE)             | Computes the cross-product of a vector and a magnitude.                                                 |
| [Project](#PROAD473221)           | Projects the first vector onto the second.                                                              |
| [Project](#PROAD473221)           | Projects a point onto a line segment.                                                                   |
| [Reflect](#REF3A0C346B)           | Computes the reflection of the input vector about the specified axis.                                   |
| [Floor](#FLO810114BC)             | Computes a new vector with the floor of each component of the input vector.                             |
| [Ceil](#CEIAFCC1BAB)              | Computes a new vector with the ceiling of each component of the input vector.                           |
| [Round](#ROU73CA46FA)             | Computes a new vector with the rounded value of each component of the input vector.                     |
| [Fraction](#FRA1E836A58)          | Computes a new vector with the fractional portion of each component of the input vector.                |

### Fields

#### <a name="XCDCAB7F6"></a>X : float

The x-coordinate of this vector.

#### <a name="YCDCAB7F5"></a>Y : float

The y-coordinate of this vector.

#### <a name="ZERC7D5C0B8"></a>Zero : [Vector](Heirloom.Math.Vector.md)
<small>`Read Only`</small>

A vector with value (0, 0).

#### <a name="ONE62466566"></a>One : [Vector](Heirloom.Math.Vector.md)
<small>`Read Only`</small>

A vector with value (1, 1).

#### <a name="RIG1DA76FF8"></a>Right : [Vector](Heirloom.Math.Vector.md)
<small>`Read Only`</small>

A vector with value (1, 0).

#### <a name="UP52C519F9"></a>Up : [Vector](Heirloom.Math.Vector.md)
<small>`Read Only`</small>

A vector with value (0, -1).

#### <a name="LEF9A907773"></a>Left : [Vector](Heirloom.Math.Vector.md)
<small>`Read Only`</small>

A vector with value (-1, 0).

#### <a name="DOW1FFBB0EA"></a>Down : [Vector](Heirloom.Math.Vector.md)
<small>`Read Only`</small>

A vector with value (0, 1).

#### <a name="ZERC7D5C0B8"></a>Zero : [Vector](Heirloom.Math.Vector.md)
<small>`Static`, `Read Only`</small>

A vector with value (0, 0).

#### <a name="ONE62466566"></a>One : [Vector](Heirloom.Math.Vector.md)
<small>`Static`, `Read Only`</small>

A vector with value (1, 1).

#### <a name="RIG1DA76FF8"></a>Right : [Vector](Heirloom.Math.Vector.md)
<small>`Static`, `Read Only`</small>

A vector with value (1, 0).

#### <a name="UP52C519F9"></a>Up : [Vector](Heirloom.Math.Vector.md)
<small>`Static`, `Read Only`</small>

A vector with value (0, -1).

#### <a name="LEF9A907773"></a>Left : [Vector](Heirloom.Math.Vector.md)
<small>`Static`, `Read Only`</small>

A vector with value (-1, 0).

#### <a name="DOW1FFBB0EA"></a>Down : [Vector](Heirloom.Math.Vector.md)
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

#### <a name="NOR9634A03D"></a>Normalized : [Vector](Heirloom.Math.Vector.md)

<small>`Read Only`</small>

Gets a normalized copy of this vector.

#### <a name="PER94285E6A"></a>Perpendicular : [Vector](Heirloom.Math.Vector.md)

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


#### <a name="GET22695431"></a>GetMaxComponent([Vector](Heirloom.Math.Vector.md) vec) : float
<small>`Static`</small>

Gets the maximal component in the input vector.


#### <a name="GET5870AAA3"></a>GetMinComponent([Vector](Heirloom.Math.Vector.md) vec) : float
<small>`Static`</small>

Gets the minimal component in the input vector.


#### <a name="MINFA9DC5A1"></a>Min([Vector](Heirloom.Math.Vector.md) a, [Vector](Heirloom.Math.Vector.md) b) : [Vector](Heirloom.Math.Vector.md)
<small>`Static`</small>

Computes a new vector where each component is the minimum component in each respective input vector.


#### <a name="MAX90EC9843"></a>Max([Vector](Heirloom.Math.Vector.md) a, [Vector](Heirloom.Math.Vector.md) b) : [Vector](Heirloom.Math.Vector.md)
<small>`Static`</small>

Computes a new vector where each component is the maximum component in each respective input vector.


#### <a name="ABS68763C3B"></a>Abs([Vector](Heirloom.Math.Vector.md) vec) : [Vector](Heirloom.Math.Vector.md)
<small>`Static`</small>

Computes a new vector where each component is the absolute value of each component in the input vector.


#### <a name="DIS8C9D050B"></a>Distance(in [Vector](Heirloom.Math.Vector.md) a, in [Vector](Heirloom.Math.Vector.md) b) : float
<small>`Static`</small>

Computes the euclidean distance between any two vectors.


#### <a name="DISBA9532A2"></a>DistanceSquared(in [Vector](Heirloom.Math.Vector.md) a, in [Vector](Heirloom.Math.Vector.md) b) : float
<small>`Static`</small>

Computes the squared euclidean distance between any two vectors.


#### <a name="MAN9EDD023B"></a>ManhattanDistance(in [Vector](Heirloom.Math.Vector.md) a, in [Vector](Heirloom.Math.Vector.md) b) : float
<small>`Static`</small>

Computes the manhattan distance between any two vectors.


#### <a name="NORE4E65A8E"></a>Normalize([Vector](Heirloom.Math.Vector.md) vec) : [Vector](Heirloom.Math.Vector.md)
<small>`Static`</small>

Normalizes the input vector and return it.


#### <a name="NOR3BED518E"></a>Normalize(ref [Vector](Heirloom.Math.Vector.md) vec) : void
<small>`Static`</small>

Normalizes the input vector.


#### <a name="LERD952CD56"></a>Lerp([Vector](Heirloom.Math.Vector.md) from, [Vector](Heirloom.Math.Vector.md) to, float t) : [Vector](Heirloom.Math.Vector.md)
<small>`Static`</small>

Interpolate two vectors.


#### <a name="FROE114BB80"></a>FromAngle(float angle) : [Vector](Heirloom.Math.Vector.md)
<small>`Static`</small>

Creates a unit length vector with the given angle from the x-axis.


#### <a name="ANGFB6C8DB7"></a>AngleBetween([Vector](Heirloom.Math.Vector.md) a, [Vector](Heirloom.Math.Vector.md) b) : float
<small>`Static`</small>

Computes the angle (in radians) between two vectors (using dot product).


#### <a name="ROTE095D261"></a>Rotate([Vector](Heirloom.Math.Vector.md) v, float angle) : [Vector](Heirloom.Math.Vector.md)
<small>`Static`</small>

Rotates a vector by the specified angle.


#### <a name="DOT31B7F993"></a>Dot(in [Vector](Heirloom.Math.Vector.md) a, in [Vector](Heirloom.Math.Vector.md) b) : float
<small>`Static`</small>

Computes the dot-product of two vectors.


#### <a name="CROE54DA882"></a>Cross(in [Vector](Heirloom.Math.Vector.md) a, in [Vector](Heirloom.Math.Vector.md) b) : float
<small>`Static`</small>

Computes the cross-product of two vectors.


#### <a name="CRO2759AEA2"></a>Cross(in [Vector](Heirloom.Math.Vector.md) a, float s) : [Vector](Heirloom.Math.Vector.md)
<small>`Static`</small>

Computes the cross-product of a vector and a magnitude.


#### <a name="PRO9EB265E1"></a>Project(in [Vector](Heirloom.Math.Vector.md) u, in [Vector](Heirloom.Math.Vector.md) v) : float
<small>`Static`</small>

Projects the first vector onto the second.

<small>**u**: <param name="u"> The first vector. </param></small>  
<small>**v**: <param name="v"> The second vector. </param></small>  

#### <a name="PRO9DBF61B7"></a>Project(in [Vector](Heirloom.Math.Vector.md) start, in [Vector](Heirloom.Math.Vector.md) end, in [Vector](Heirloom.Math.Vector.md) point, bool clamp = True) : float
<small>`Static`</small>

Projects a point onto a line segment.

<small>**start**: <param name="start">Starting point of the line segment.</param></small>  
<small>**end**: <param name="end">Ending point of the line segment.</param></small>  
<small>**point**: <param name="point">Point to project.</param></small>  
<small>**clamp**: <param name="clamp">Should we clamp to the ends of the line segment?</param></small>  

#### <a name="REF86804C70"></a>Reflect(in [Vector](Heirloom.Math.Vector.md) v, in [Vector](Heirloom.Math.Vector.md) axis) : [Vector](Heirloom.Math.Vector.md)
<small>`Static`</small>

Computes the reflection of the input vector about the specified axis.


#### <a name="FLO4BDBEB51"></a>Floor([Vector](Heirloom.Math.Vector.md) v) : [Vector](Heirloom.Math.Vector.md)
<small>`Static`</small>

Computes a new vector with the floor of each component of the input vector.


#### <a name="CEI7F611184"></a>Ceil([Vector](Heirloom.Math.Vector.md) v) : [Vector](Heirloom.Math.Vector.md)
<small>`Static`</small>

Computes a new vector with the ceiling of each component of the input vector.


#### <a name="ROUBDFB082F"></a>Round([Vector](Heirloom.Math.Vector.md) v) : [Vector](Heirloom.Math.Vector.md)
<small>`Static`</small>

Computes a new vector with the rounded value of each component of the input vector.


#### <a name="FRAFD1789EF"></a>Fraction([Vector](Heirloom.Math.Vector.md) v) : [Vector](Heirloom.Math.Vector.md)
<small>`Static`</small>

Computes a new vector with the fractional portion of each component of the input vector.


