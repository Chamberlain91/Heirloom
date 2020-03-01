# Heirloom.Math

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md)</small>  

## Size (Struct)
<small>**Namespace**: Heirloom.Math</sub></small>  
<small>**Interfaces**: IEquatable\<Size>, IComparable\<Size></small>  

| Fields                | Summary                                  |
|-----------------------|------------------------------------------|
| [Width](#WIDT6892)    | The width (horizontal size measure).     |
| [Height](#HEIGE098)   | The height (vertical size measure).      |
| [Infinite](#INFIDABE) | An infinite size.                        |
| [Max](#MAXD4DA)       | The maximum representable size possible. |
| [Zero](#ZEROC7D5)     | A 0x0 size.                              |
| [One](#ONE6246)       | A 1x1 size.                              |

| Properties          | Summary                                                            |
|---------------------|--------------------------------------------------------------------|
| [Area](#AREA9F52)   | Gets the area of this size as if it was a rectangle at the origin. |
| [Aspect](#ASPE3163) | Gets the aspect ratio of this size.                                |

| Methods                  | Summary |
|--------------------------|---------|
| [Deconstruct](#DECOC188) |         |
| [CompareTo](#COMPD590)   |         |

### Fields

#### <a name="WIDT6892"></a> Width : float

The width (horizontal size measure).

#### <a name="HEIGE098"></a> Height : float

The height (vertical size measure).

#### <a name="INFIDABE"></a> Infinite : [Size](Heirloom.Math.Size.md)
<small>`Read Only`</small>

An infinite size.

#### <a name="MAXD4DA"></a> Max : [Size](Heirloom.Math.Size.md)
<small>`Read Only`</small>

The maximum representable size possible.

#### <a name="ZEROC7D5"></a> Zero : [Size](Heirloom.Math.Size.md)
<small>`Read Only`</small>

A 0x0 size.

#### <a name="ONE6246"></a> One : [Size](Heirloom.Math.Size.md)
<small>`Read Only`</small>

A 1x1 size.

#### <a name="INFIDABE"></a> Infinite : [Size](Heirloom.Math.Size.md)
<small>`Static`, `Read Only`</small>

An infinite size.

#### <a name="MAXD4DA"></a> Max : [Size](Heirloom.Math.Size.md)
<small>`Static`, `Read Only`</small>

The maximum representable size possible.

#### <a name="ZEROC7D5"></a> Zero : [Size](Heirloom.Math.Size.md)
<small>`Static`, `Read Only`</small>

A 0x0 size.

#### <a name="ONE6246"></a> One : [Size](Heirloom.Math.Size.md)
<small>`Static`, `Read Only`</small>

A 1x1 size.

### Constructors

#### Size(float width, float height)

### Properties

#### <a name="AREA9F52"></a> Area : float

<small>`Read Only`</small>

Gets the area of this size as if it was a rectangle at the origin.

#### <a name="ASPE3163"></a> Aspect : float

<small>`Read Only`</small>

Gets the aspect ratio of this size.

### Methods

#### <a name="DECO9B36"></a> Deconstruct(out float width, out float height) : void


#### <a name="COMP474E"></a> CompareTo([Size](Heirloom.Math.Size.md) other) : int
<small>`Virtual`</small>


