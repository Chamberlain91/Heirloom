# Heirloom.Math

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md)</small>  

## IntSize (Struct)
<small>**Namespace**: Heirloom.Math</sub></small>  
<small>**Interfaces**: IEquatable\<IntSize>, IComparable\<IntSize></small>  

Represents a size or dimensions defined with integer fields.

| Fields              | Summary                                  |
|---------------------|------------------------------------------|
| [Width](#WIDT6892)  | The width (horizontal size measure).     |
| [Height](#HEIGE098) | The height (vertical size measure).      |
| [Max](#MAXD4DA)     | The maximum representable size possible. |
| [Zero](#ZEROC7D5)   | A 0x0 size.                              |
| [One](#ONE6246)     | A 1x1 size.                              |

| Properties          | Summary                                                            |
|---------------------|--------------------------------------------------------------------|
| [Area](#AREA9F52)   | Gets the area of this size as if it was a rectangle at the origin. |
| [Aspect](#ASPE3163) | Gets the aspect ratio of this size.                                |

| Methods                  | Summary |
|--------------------------|---------|
| [Deconstruct](#DECOC188) |         |
| [CompareTo](#COMPD590)   |         |

### Fields

#### <a name="WIDT6892"></a> Width : int

The width (horizontal size measure).

#### <a name="HEIGE098"></a> Height : int

The height (vertical size measure).

#### <a name="MAXD4DA"></a> Max : [IntSize](Heirloom.Math.IntSize.md)
<small>`Read Only`</small>

The maximum representable size possible.

#### <a name="ZEROC7D5"></a> Zero : [IntSize](Heirloom.Math.IntSize.md)
<small>`Read Only`</small>

A 0x0 size.

#### <a name="ONE6246"></a> One : [IntSize](Heirloom.Math.IntSize.md)
<small>`Read Only`</small>

A 1x1 size.

#### <a name="MAXD4DA"></a> Max : [IntSize](Heirloom.Math.IntSize.md)
<small>`Static`, `Read Only`</small>

The maximum representable size possible.

#### <a name="ZEROC7D5"></a> Zero : [IntSize](Heirloom.Math.IntSize.md)
<small>`Static`, `Read Only`</small>

A 0x0 size.

#### <a name="ONE6246"></a> One : [IntSize](Heirloom.Math.IntSize.md)
<small>`Static`, `Read Only`</small>

A 1x1 size.

### Constructors

#### IntSize(int width, int height)

### Properties

#### <a name="AREA9F52"></a> Area : int

<small>`Read Only`</small>

Gets the area of this size as if it was a rectangle at the origin.

#### <a name="ASPE3163"></a> Aspect : float

<small>`Read Only`</small>

Gets the aspect ratio of this size.

### Methods

#### <a name="DECO55B0"></a> Deconstruct(out int width, out int height) : void


#### <a name="COMPC972"></a> CompareTo([IntSize](Heirloom.Math.IntSize.md) other) : int
<small>`Virtual`</small>


