# Heirloom.Math

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Math](../heirloom.math/heirloom.math.md)</small>  

## IntVector (Struct)
<small>**Namespace**: Heirloom.Math</sub></small>  
<small>**Interfaces**: IEquatable\<IntVector></small>  

Represents a vector with two integer values.

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
| [Perpendicular](#PER94285E6A) | Gets a perpendicular copy of this vector. |

| Methods | Summary |
|---------|---------|
| [Set](#SETB9EBD57E) | Sets the components of this vector. |
| [Deconstruct](#DEC47DB9CE) |  |
| [GetMaxComponent](#GET21F2DB40) | Gets the maximal component in the input vector. |
| [GetMinComponent](#GETFB105FE2) | Gets the minimal component in the input vector. |
| [Min](#MIN18BF7501) | Computes a new vector where each component is the minimum component in each respective input vector. |
| [Max](#MAXF540C893) | Computes a new vector where each component is the maximum component in each respective input vector. |
| [Abs](#ABS2DEB9FB) | Computes a new vector where each component is the absolute value of each component in the input vector. |
| [Distance](#DIS816FD2B) | Computes the euclidean distance between any two vectors. |
| [DistanceSquared](#DIS68C2E231) | Computes the squared euclidean distance between any two vectors. |
| [ManhattanDistance](#MANF9F9CD96) | Computes the manhattan distance between any two vectors. |

### Fields

#### <a name="XCDCAB7F6"></a>X : int

The x-coordinate of this vector.

#### <a name="YCDCAB7F5"></a>Y : int

The y-coordinate of this vector.

#### <a name="ZERC7D5C0B8"></a>Zero : [IntVector](heirloom.math.intvector.md)
<small>`Read Only`</small>

A vector with value (0, 0).

#### <a name="ONE62466566"></a>One : [IntVector](heirloom.math.intvector.md)
<small>`Read Only`</small>

A vector with value (1, 1).

#### <a name="RIG1DA76FF8"></a>Right : [IntVector](heirloom.math.intvector.md)
<small>`Read Only`</small>

A vector with value (1, 0).

#### <a name="UP52C519F9"></a>Up : [IntVector](heirloom.math.intvector.md)
<small>`Read Only`</small>

A vector with value (0, -1).

#### <a name="LEF9A907773"></a>Left : [IntVector](heirloom.math.intvector.md)
<small>`Read Only`</small>

A vector with value (-1, 0).

#### <a name="DOW1FFBB0EA"></a>Down : [IntVector](heirloom.math.intvector.md)
<small>`Read Only`</small>

A vector with value (0, 1).

#### <a name="ZERC7D5C0B8"></a>Zero : [IntVector](heirloom.math.intvector.md)
<small>`Static`, `Read Only`</small>

A vector with value (0, 0).

#### <a name="ONE62466566"></a>One : [IntVector](heirloom.math.intvector.md)
<small>`Static`, `Read Only`</small>

A vector with value (1, 1).

#### <a name="RIG1DA76FF8"></a>Right : [IntVector](heirloom.math.intvector.md)
<small>`Static`, `Read Only`</small>

A vector with value (1, 0).

#### <a name="UP52C519F9"></a>Up : [IntVector](heirloom.math.intvector.md)
<small>`Static`, `Read Only`</small>

A vector with value (0, -1).

#### <a name="LEF9A907773"></a>Left : [IntVector](heirloom.math.intvector.md)
<small>`Static`, `Read Only`</small>

A vector with value (-1, 0).

#### <a name="DOW1FFBB0EA"></a>Down : [IntVector](heirloom.math.intvector.md)
<small>`Static`, `Read Only`</small>

A vector with value (0, 1).

### Constructors

#### IntVector(int x, int y)

### Properties

#### <a name="LEN6B366D7E"></a>Length : float

<small>`Read Only`</small>

Gets the magnitude of this vector.

#### <a name="LEN3BB93C25"></a>LengthSquared : float

<small>`Read Only`</small>

Gets the squared magnitude of this vector.

#### <a name="PER94285E6A"></a>Perpendicular : [IntVector](heirloom.math.intvector.md)

<small>`Read Only`</small>

Gets a perpendicular copy of this vector.

### Methods

#### <a name="SETB9EBD57E"></a>Set(int x, int y) : void


Sets the components of this vector.


#### <a name="DEC47DB9CE"></a>Deconstruct(out int x, out int y) : void



#### <a name="GET21F2DB40"></a>GetMaxComponent([IntVector](heirloom.math.intvector.md) vec) : int

<small>`Static`</small>

Gets the maximal component in the input vector.


#### <a name="GETFB105FE2"></a>GetMinComponent([IntVector](heirloom.math.intvector.md) vec) : int

<small>`Static`</small>

Gets the minimal component in the input vector.


#### <a name="MIN18BF7501"></a>Min([IntVector](heirloom.math.intvector.md) a, [IntVector](heirloom.math.intvector.md) b) : [IntVector](heirloom.math.intvector.md)

<small>`Static`</small>

Computes a new vector where each component is the minimum component in each respective input vector.


#### <a name="MAXF540C893"></a>Max([IntVector](heirloom.math.intvector.md) a, [IntVector](heirloom.math.intvector.md) b) : [IntVector](heirloom.math.intvector.md)

<small>`Static`</small>

Computes a new vector where each component is the maximum component in each respective input vector.


#### <a name="ABS2DEB9FB"></a>Abs([IntVector](heirloom.math.intvector.md) vec) : [IntVector](heirloom.math.intvector.md)

<small>`Static`</small>

Computes a new vector where each component is the absolute value of each component in the input vector.


#### <a name="DIS816FD2B"></a>Distance(in [IntVector](heirloom.math.intvector.md) a, in [IntVector](heirloom.math.intvector.md) b) : float

<small>`Static`</small>

Computes the euclidean distance between any two vectors.


#### <a name="DIS68C2E231"></a>DistanceSquared(in [IntVector](heirloom.math.intvector.md) a, in [IntVector](heirloom.math.intvector.md) b) : int

<small>`Static`</small>

Computes the squared euclidean distance between any two vectors.


#### <a name="MANF9F9CD96"></a>ManhattanDistance(in [IntVector](heirloom.math.intvector.md) a, in [IntVector](heirloom.math.intvector.md) b) : int

<small>`Static`</small>

Computes the manhattan distance between any two vectors.


