# Heirloom.Math

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md)</small>  

## Size (Struct)
<small>**Namespace**: Heirloom.Math</sub></small>  
<small>**Interfaces**: IEquatable\<Size>, IComparable\<Size></small>  

| Fields                  | Summary                                  |
|-------------------------|------------------------------------------|
| [Width](#WID68924896)   | The width (horizontal size measure).     |
| [Height](#HEIE098AAEB)  | The height (vertical size measure).      |
| [Infinite](#INFDABEDF6) | An infinite size.                        |
| [Max](#MAXD4DA94E4)     | The maximum representable size possible. |
| [Zero](#ZERC7D5C0B8)    | A 0x0 size.                              |
| [One](#ONE62466566)     | A 1x1 size.                              |

| Properties             | Summary                                                            |
|------------------------|--------------------------------------------------------------------|
| [Area](#ARE9F5286F)    | Gets the area of this size as if it was a rectangle at the origin. |
| [Aspect](#ASP31635C5A) | Gets the aspect ratio of this size.                                |

| Methods                     | Summary |
|-----------------------------|---------|
| [Deconstruct](#DECC1884FDA) |         |
| [CompareTo](#COMD590B82C)   |         |

### Fields

#### <a name="WID68924896"></a>Width : float

The width (horizontal size measure).

#### <a name="HEIE098AAEB"></a>Height : float

The height (vertical size measure).

#### <a name="INFDABEDF6"></a>Infinite : [Size](Heirloom.Math.Size.md)
<small>`Read Only`</small>

An infinite size.

#### <a name="MAXD4DA94E4"></a>Max : [Size](Heirloom.Math.Size.md)
<small>`Read Only`</small>

The maximum representable size possible.

#### <a name="ZERC7D5C0B8"></a>Zero : [Size](Heirloom.Math.Size.md)
<small>`Read Only`</small>

A 0x0 size.

#### <a name="ONE62466566"></a>One : [Size](Heirloom.Math.Size.md)
<small>`Read Only`</small>

A 1x1 size.

#### <a name="INFDABEDF6"></a>Infinite : [Size](Heirloom.Math.Size.md)
<small>`Static`, `Read Only`</small>

An infinite size.

#### <a name="MAXD4DA94E4"></a>Max : [Size](Heirloom.Math.Size.md)
<small>`Static`, `Read Only`</small>

The maximum representable size possible.

#### <a name="ZERC7D5C0B8"></a>Zero : [Size](Heirloom.Math.Size.md)
<small>`Static`, `Read Only`</small>

A 0x0 size.

#### <a name="ONE62466566"></a>One : [Size](Heirloom.Math.Size.md)
<small>`Static`, `Read Only`</small>

A 1x1 size.

### Constructors

#### Size(float width, float height)

### Properties

#### <a name="ARE9F5286F"></a>Area : float

<small>`Read Only`</small>

Gets the area of this size as if it was a rectangle at the origin.

#### <a name="ASP31635C5A"></a>Aspect : float

<small>`Read Only`</small>

Gets the aspect ratio of this size.

### Methods

#### <a name="DEC9B367A2C"></a>Deconstruct(out float width, out float height) : void


#### <a name="COM474EB48B"></a>CompareTo([Size](Heirloom.Math.Size.md) other) : int
<small>`Virtual`</small>


