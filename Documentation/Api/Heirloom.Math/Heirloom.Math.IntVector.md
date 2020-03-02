# Heirloom.Math

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md)</small>  

## IntVector (Struct)
<small>**Namespace**: Heirloom.Math</small>  
<small>**Interfaces**: IEquatable\<IntVector></small>  

Represents a vector with two integer values.

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

| Properties                    | Summary                                    |
|-------------------------------|--------------------------------------------|
| [Length](#LEN6B366D7E)        | Gets the magnitude of this vector.         |
| [LengthSquared](#LEN3BB93C25) | Gets the squared magnitude of this vector. |
| [Perpendicular](#PER94285E6A) | Gets a perpendicular copy of this vector.  |

| Methods                           | Summary                                                                                                 |
|-----------------------------------|---------------------------------------------------------------------------------------------------------|
| [Set](#SETB9EBD57E)               | Sets the components of this vector.                                                                     |
| [Deconstruct](#DEC47DB9CE)        |                                                                                                         |
| [GetMaxComponent](#GET47CD3B80)   | Gets the maximal component in the input vector.                                                         |
| [GetMinComponent](#GET20EAC022)   | Gets the minimal component in the input vector.                                                         |
| [Min](#MIN36492141)               | Computes a new vector where each component is the minimum component in each respective input vector.    |
| [Max](#MAXD88455D3)               | Computes a new vector where each component is the maximum component in each respective input vector.    |
| [Abs](#ABS7AA7B07B)               | Computes a new vector where each component is the absolute value of each component in the input vector. |
| [Distance](#DISED56852B)          | Computes the euclidean distance between any two vectors.                                                |
| [DistanceSquared](#DIS1DC98031)   | Computes the squared euclidean distance between any two vectors.                                        |
| [ManhattanDistance](#MAND397BB96) | Computes the manhattan distance between any two vectors.                                                |

### Fields

#### <a name="XCDCAB7F6"></a>X : int

The x-coordinate of this vector.

#### <a name="YCDCAB7F5"></a>Y : int

The y-coordinate of this vector.

#### <a name="ZERC7D5C0B8"></a>Zero : [IntVector](Heirloom.Math.IntVector.md)
<small>`Read Only`</small>

A vector with value (0, 0).

#### <a name="ONE62466566"></a>One : [IntVector](Heirloom.Math.IntVector.md)
<small>`Read Only`</small>

A vector with value (1, 1).

#### <a name="RIG1DA76FF8"></a>Right : [IntVector](Heirloom.Math.IntVector.md)
<small>`Read Only`</small>

A vector with value (1, 0).

#### <a name="UP52C519F9"></a>Up : [IntVector](Heirloom.Math.IntVector.md)
<small>`Read Only`</small>

A vector with value (0, -1).

#### <a name="LEF9A907773"></a>Left : [IntVector](Heirloom.Math.IntVector.md)
<small>`Read Only`</small>

A vector with value (-1, 0).

#### <a name="DOW1FFBB0EA"></a>Down : [IntVector](Heirloom.Math.IntVector.md)
<small>`Read Only`</small>

A vector with value (0, 1).

#### <a name="ZERC7D5C0B8"></a>Zero : [IntVector](Heirloom.Math.IntVector.md)
<small>`Static`, `Read Only`</small>

A vector with value (0, 0).

#### <a name="ONE62466566"></a>One : [IntVector](Heirloom.Math.IntVector.md)
<small>`Static`, `Read Only`</small>

A vector with value (1, 1).

#### <a name="RIG1DA76FF8"></a>Right : [IntVector](Heirloom.Math.IntVector.md)
<small>`Static`, `Read Only`</small>

A vector with value (1, 0).

#### <a name="UP52C519F9"></a>Up : [IntVector](Heirloom.Math.IntVector.md)
<small>`Static`, `Read Only`</small>

A vector with value (0, -1).

#### <a name="LEF9A907773"></a>Left : [IntVector](Heirloom.Math.IntVector.md)
<small>`Static`, `Read Only`</small>

A vector with value (-1, 0).

#### <a name="DOW1FFBB0EA"></a>Down : [IntVector](Heirloom.Math.IntVector.md)
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

#### <a name="PER94285E6A"></a>Perpendicular : [IntVector](Heirloom.Math.IntVector.md)

<small>`Read Only`</small>

Gets a perpendicular copy of this vector.

### Methods

#### <a name="SETB9EBD57E"></a>Set(int x, int y) : void

Sets the components of this vector.


#### <a name="DEC47DB9CE"></a>Deconstruct(out int x, out int y) : void


#### <a name="GET47CD3B80"></a>GetMaxComponent([IntVector](Heirloom.Math.IntVector.md) vec) : int
<small>`Static`</small>

Gets the maximal component in the input vector.


#### <a name="GET20EAC022"></a>GetMinComponent([IntVector](Heirloom.Math.IntVector.md) vec) : int
<small>`Static`</small>

Gets the minimal component in the input vector.


#### <a name="MIN36492141"></a>Min([IntVector](Heirloom.Math.IntVector.md) a, [IntVector](Heirloom.Math.IntVector.md) b) : [IntVector](Heirloom.Math.IntVector.md)
<small>`Static`</small>

Computes a new vector where each component is the minimum component in each respective input vector.


#### <a name="MAXD88455D3"></a>Max([IntVector](Heirloom.Math.IntVector.md) a, [IntVector](Heirloom.Math.IntVector.md) b) : [IntVector](Heirloom.Math.IntVector.md)
<small>`Static`</small>

Computes a new vector where each component is the maximum component in each respective input vector.


#### <a name="ABS7AA7B07B"></a>Abs([IntVector](Heirloom.Math.IntVector.md) vec) : [IntVector](Heirloom.Math.IntVector.md)
<small>`Static`</small>

Computes a new vector where each component is the absolute value of each component in the input vector.


#### <a name="DISED56852B"></a>Distance(in [IntVector](Heirloom.Math.IntVector.md) a, in [IntVector](Heirloom.Math.IntVector.md) b) : float
<small>`Static`</small>

Computes the euclidean distance between any two vectors.


#### <a name="DIS1DC98031"></a>DistanceSquared(in [IntVector](Heirloom.Math.IntVector.md) a, in [IntVector](Heirloom.Math.IntVector.md) b) : int
<small>`Static`</small>

Computes the squared euclidean distance between any two vectors.


#### <a name="MAND397BB96"></a>ManhattanDistance(in [IntVector](Heirloom.Math.IntVector.md) a, in [IntVector](Heirloom.Math.IntVector.md) b) : int
<small>`Static`</small>

Computes the manhattan distance between any two vectors.


