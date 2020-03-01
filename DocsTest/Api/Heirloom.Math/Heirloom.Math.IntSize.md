# Heirloom.Math

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md)</small>  

## IntSize (Struct)
<small>**Namespace**: Heirloom.Math</sub></small>  
<small>**Interfaces**: IEquatable\<IntSize>, IComparable\<IntSize></small>  

Represents a size or dimensions defined with integer fields.

| Fields                 | Summary                                  |
|------------------------|------------------------------------------|
| [Width](#WID68924896)  | The width (horizontal size measure).     |
| [Height](#HEIE098AAEB) | The height (vertical size measure).      |
| [Max](#MAXD4DA94E4)    | The maximum representable size possible. |
| [Zero](#ZERC7D5C0B8)   | A 0x0 size.                              |
| [One](#ONE62466566)    | A 1x1 size.                              |

| Properties             | Summary                                                            |
|------------------------|--------------------------------------------------------------------|
| [Area](#ARE9F5286F)    | Gets the area of this size as if it was a rectangle at the origin. |
| [Aspect](#ASP31635C5A) | Gets the aspect ratio of this size.                                |

| Methods                     | Summary |
|-----------------------------|---------|
| [Deconstruct](#DEC55B0AADE) |         |
| [CompareTo](#COMC972259B)   |         |

### Fields

#### <a name="WID68924896"></a>Width : int

The width (horizontal size measure).

#### <a name="HEIE098AAEB"></a>Height : int

The height (vertical size measure).

#### <a name="MAXD4DA94E4"></a>Max : [IntSize](Heirloom.Math.IntSize.md)
<small>`Read Only`</small>

The maximum representable size possible.

#### <a name="ZERC7D5C0B8"></a>Zero : [IntSize](Heirloom.Math.IntSize.md)
<small>`Read Only`</small>

A 0x0 size.

#### <a name="ONE62466566"></a>One : [IntSize](Heirloom.Math.IntSize.md)
<small>`Read Only`</small>

A 1x1 size.

#### <a name="MAXD4DA94E4"></a>Max : [IntSize](Heirloom.Math.IntSize.md)
<small>`Static`, `Read Only`</small>

The maximum representable size possible.

#### <a name="ZERC7D5C0B8"></a>Zero : [IntSize](Heirloom.Math.IntSize.md)
<small>`Static`, `Read Only`</small>

A 0x0 size.

#### <a name="ONE62466566"></a>One : [IntSize](Heirloom.Math.IntSize.md)
<small>`Static`, `Read Only`</small>

A 1x1 size.

### Constructors

#### IntSize(int width, int height)

### Properties

#### <a name="ARE9F5286F"></a>Area : int

<small>`Read Only`</small>

Gets the area of this size as if it was a rectangle at the origin.

#### <a name="ASP31635C5A"></a>Aspect : float

<small>`Read Only`</small>

Gets the aspect ratio of this size.

### Methods

#### <a name="DEC55B0AADE"></a>Deconstruct(out int width, out int height) : void


#### <a name="COMC972259B"></a>CompareTo([IntSize](Heirloom.Math.IntSize.md) other) : int
<small>`Virtual`</small>


