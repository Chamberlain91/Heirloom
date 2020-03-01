# Heirloom.Math

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md)</small>  

## IntVector (Struct)
<small>**Namespace**: Heirloom.Math</sub></small>  
<small>**Interfaces**: IEquatable\<IntVector></small>  

Represents a vector with two integer values.

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

| Properties                 | Summary                                    |
|----------------------------|--------------------------------------------|
| [Length](#LENG6B36)        | Gets the magnitude of this vector.         |
| [LengthSquared](#LENG3BB9) | Gets the squared magnitude of this vector. |
| [Perpendicular](#PERP9428) | Gets a perpendicular copy of this vector.  |

| Methods                        | Summary                                                                                                 |
|--------------------------------|---------------------------------------------------------------------------------------------------------|
| [Set](#SET5F78)                | Sets the components of this vector.                                                                     |
| [Deconstruct](#DECOC188)       |                                                                                                         |
| [GetMaxComponent](#GETM45DC)   | Gets the maximal component in the input vector.                                                         |
| [GetMinComponent](#GETMA14A)   | Gets the minimal component in the input vector.                                                         |
| [Min](#MINBF9E)                | Computes a new vector where each component is the minimum component in each respective input vector.    |
| [Max](#MAXD4DA)                | Computes a new vector where each component is the maximum component in each respective input vector.    |
| [Abs](#ABSECE4)                | Computes a new vector where each component is the absolute value of each component in the input vector. |
| [Distance](#DIST3A36)          | Computes the euclidean distance between any two vectors.                                                |
| [DistanceSquared](#DIST66C8)   | Computes the squared euclidean distance between any two vectors.                                        |
| [ManhattanDistance](#MANHC65C) | Computes the manhattan distance between any two vectors.                                                |

### Fields

#### <a name="XCDCA"></a> X : int

The x-coordinate of this vector.

#### <a name="YCDCA"></a> Y : int

The y-coordinate of this vector.

#### <a name="ZEROC7D5"></a> Zero : [IntVector](Heirloom.Math.IntVector.md)
<small>`Read Only`</small>

A vector with value (0, 0).

#### <a name="ONE6246"></a> One : [IntVector](Heirloom.Math.IntVector.md)
<small>`Read Only`</small>

A vector with value (1, 1).

#### <a name="RIGH1DA7"></a> Right : [IntVector](Heirloom.Math.IntVector.md)
<small>`Read Only`</small>

A vector with value (1, 0).

#### <a name="UP52C5"></a> Up : [IntVector](Heirloom.Math.IntVector.md)
<small>`Read Only`</small>

A vector with value (0, -1).

#### <a name="LEFT9A90"></a> Left : [IntVector](Heirloom.Math.IntVector.md)
<small>`Read Only`</small>

A vector with value (-1, 0).

#### <a name="DOWN1FFB"></a> Down : [IntVector](Heirloom.Math.IntVector.md)
<small>`Read Only`</small>

A vector with value (0, 1).

#### <a name="ZEROC7D5"></a> Zero : [IntVector](Heirloom.Math.IntVector.md)
<small>`Static`, `Read Only`</small>

A vector with value (0, 0).

#### <a name="ONE6246"></a> One : [IntVector](Heirloom.Math.IntVector.md)
<small>`Static`, `Read Only`</small>

A vector with value (1, 1).

#### <a name="RIGH1DA7"></a> Right : [IntVector](Heirloom.Math.IntVector.md)
<small>`Static`, `Read Only`</small>

A vector with value (1, 0).

#### <a name="UP52C5"></a> Up : [IntVector](Heirloom.Math.IntVector.md)
<small>`Static`, `Read Only`</small>

A vector with value (0, -1).

#### <a name="LEFT9A90"></a> Left : [IntVector](Heirloom.Math.IntVector.md)
<small>`Static`, `Read Only`</small>

A vector with value (-1, 0).

#### <a name="DOWN1FFB"></a> Down : [IntVector](Heirloom.Math.IntVector.md)
<small>`Static`, `Read Only`</small>

A vector with value (0, 1).

### Constructors

#### IntVector(int x, int y)

### Properties

#### <a name="LENG6B36"></a> Length : float

<small>`Read Only`</small>

Gets the magnitude of this vector.

#### <a name="LENG3BB9"></a> LengthSquared : float

<small>`Read Only`</small>

Gets the squared magnitude of this vector.

#### <a name="PERP9428"></a> Perpendicular : [IntVector](Heirloom.Math.IntVector.md)

<small>`Read Only`</small>

Gets a perpendicular copy of this vector.

### Methods

#### <a name="SET(B9EB"></a> Set(int x, int y) : void

Sets the components of this vector.


#### <a name="DECO47DB"></a> Deconstruct(out int x, out int y) : void


#### <a name="GETM47CD"></a> GetMaxComponent([IntVector](Heirloom.Math.IntVector.md) vec) : int
<small>`Static`</small>

Gets the maximal component in the input vector.


#### <a name="GETM20EA"></a> GetMinComponent([IntVector](Heirloom.Math.IntVector.md) vec) : int
<small>`Static`</small>

Gets the minimal component in the input vector.


#### <a name="MIN(3649"></a> Min([IntVector](Heirloom.Math.IntVector.md) a, [IntVector](Heirloom.Math.IntVector.md) b) : [IntVector](Heirloom.Math.IntVector.md)
<small>`Static`</small>

Computes a new vector where each component is the minimum component in each respective input vector.


#### <a name="MAX(D884"></a> Max([IntVector](Heirloom.Math.IntVector.md) a, [IntVector](Heirloom.Math.IntVector.md) b) : [IntVector](Heirloom.Math.IntVector.md)
<small>`Static`</small>

Computes a new vector where each component is the maximum component in each respective input vector.


#### <a name="ABS(7AA7"></a> Abs([IntVector](Heirloom.Math.IntVector.md) vec) : [IntVector](Heirloom.Math.IntVector.md)
<small>`Static`</small>

Computes a new vector where each component is the absolute value of each component in the input vector.


#### <a name="DISTED56"></a> Distance(in [IntVector](Heirloom.Math.IntVector.md) a, in [IntVector](Heirloom.Math.IntVector.md) b) : float
<small>`Static`</small>

Computes the euclidean distance between any two vectors.


#### <a name="DIST1DC9"></a> DistanceSquared(in [IntVector](Heirloom.Math.IntVector.md) a, in [IntVector](Heirloom.Math.IntVector.md) b) : int
<small>`Static`</small>

Computes the squared euclidean distance between any two vectors.


#### <a name="MANHD397"></a> ManhattanDistance(in [IntVector](Heirloom.Math.IntVector.md) a, in [IntVector](Heirloom.Math.IntVector.md) b) : int
<small>`Static`</small>

Computes the manhattan distance between any two vectors.


