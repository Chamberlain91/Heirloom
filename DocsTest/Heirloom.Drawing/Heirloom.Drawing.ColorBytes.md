# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../Heirloom.Drawing/Heirloom.Drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## ColorBytes (Struct)
<small>**Namespace**: Heirloom.Drawing</sub></small>  
<small>**Interfaces**: IEquatable\<ColorBytes></small>  

Color encoded as 4 component bytes.

| Fields | Summary |
|-------|---------|
| [R](#RCDCAB7F0) | The red component. |
| [G](#GCDCAB7DB) | The green component. |
| [B](#BCDCAB7E0) | The blue component. |
| [A](#ACDCAB7DD) | The alpha/transparency component. |

| Properties | Summary |
|------------|---------|
| [Luminosity](#LUM27143E8B) | Computes a luminosity component (grayscale). |
| [Inverted](#INVDE5124E3) | The inversion of this color. |
| [Red](#RED5F786973) |  |
| [Green](#GREE8614423) |  |
| [Blue](#BLU5D4613EE) |  |
| [Yellow](#YELCC917366) |  |
| [Cyan](#CYA95C74181) |  |
| [Magenta](#MAGEF2FDA29) |  |
| [White](#WHI1AD967BB) |  |
| [Black](#BLAF663517F) |  |
| [Gray](#GRA1F30CD89) |  |
| [DarkGray](#DAR69CE9AE9) |  |
| [LightGray](#LIG2E3D3CFB) |  |
| [Orange](#ORA78ADA558) |  |
| [Indigo](#IND56133F0) |  |
| [Violet](#VIOD94877FD) |  |
| [Pink](#PINE1E27C5E) |  |
| [Transparent](#TRA962107FC) |  |
| [Rainbow](#RAIC72A67E4) |  |
| [Random](#RANE1E4B317) |  |

| Methods | Summary |
|---------|---------|
| [Parse](#PAR50D6FF39) | Parses a hex-string representation of a color. May be formatted as 'RGB', 'RGBA', 'RRGGBB', 'RRGGBBAA' with or without a preceding '#'. |
| [TryParse](#TRY99E0F751) | Parses a hex-string representation of a color. May be formatted as 'RGB', 'RGBA', 'RRGGBB', 'RRGGBBAA' with or without a preceding '#'. |
| [Lerp](#LERDF0BF6B4) | Interpolate two colors together. |
| [Multiply](#MULBEBFA083) | Multiplies two [ColorBytes](Heirloom.Drawing.ColorBytes.md) together. Behaves the same as [Color](Heirloom.Drawing.Color.md). |

### Fields

#### R :  byte

The red component.

#### G :  byte

The green component.

#### B :  byte

The blue component.

#### A :  byte

The alpha/transparency component.

### Constructors

#### ColorBytes(byte r,  byte g,  byte b,  byte a = 255)

### Properties

#### <a name="LUM27143E8B"></a>Luminosity :  byte

<small>`Read Only`</small>

Computes a luminosity component (grayscale).

#### <a name="INVDE5124E3"></a>Inverted : [ColorBytes](Heirloom.Drawing.ColorBytes.md)

<small>`Read Only`</small>

The inversion of this color.

#### <a name="RED5F786973"></a>Red : [ColorBytes](Heirloom.Drawing.ColorBytes.md)

<small>`Static`, `Read Only`</small>

#### <a name="GREE8614423"></a>Green : [ColorBytes](Heirloom.Drawing.ColorBytes.md)

<small>`Static`, `Read Only`</small>

#### <a name="BLU5D4613EE"></a>Blue : [ColorBytes](Heirloom.Drawing.ColorBytes.md)

<small>`Static`, `Read Only`</small>

#### <a name="YELCC917366"></a>Yellow : [ColorBytes](Heirloom.Drawing.ColorBytes.md)

<small>`Static`, `Read Only`</small>

#### <a name="CYA95C74181"></a>Cyan : [ColorBytes](Heirloom.Drawing.ColorBytes.md)

<small>`Static`, `Read Only`</small>

#### <a name="MAGEF2FDA29"></a>Magenta : [ColorBytes](Heirloom.Drawing.ColorBytes.md)

<small>`Static`, `Read Only`</small>

#### <a name="WHI1AD967BB"></a>White : [ColorBytes](Heirloom.Drawing.ColorBytes.md)

<small>`Static`, `Read Only`</small>

#### <a name="BLAF663517F"></a>Black : [ColorBytes](Heirloom.Drawing.ColorBytes.md)

<small>`Static`, `Read Only`</small>

#### <a name="GRA1F30CD89"></a>Gray : [ColorBytes](Heirloom.Drawing.ColorBytes.md)

<small>`Static`, `Read Only`</small>

#### <a name="DAR69CE9AE9"></a>DarkGray : [ColorBytes](Heirloom.Drawing.ColorBytes.md)

<small>`Static`, `Read Only`</small>

#### <a name="LIG2E3D3CFB"></a>LightGray : [ColorBytes](Heirloom.Drawing.ColorBytes.md)

<small>`Static`, `Read Only`</small>

#### <a name="ORA78ADA558"></a>Orange : [ColorBytes](Heirloom.Drawing.ColorBytes.md)

<small>`Static`, `Read Only`</small>

#### <a name="IND56133F0"></a>Indigo : [ColorBytes](Heirloom.Drawing.ColorBytes.md)

<small>`Static`, `Read Only`</small>

#### <a name="VIOD94877FD"></a>Violet : [ColorBytes](Heirloom.Drawing.ColorBytes.md)

<small>`Static`, `Read Only`</small>

#### <a name="PINE1E27C5E"></a>Pink : [ColorBytes](Heirloom.Drawing.ColorBytes.md)

<small>`Static`, `Read Only`</small>

#### <a name="TRA962107FC"></a>Transparent : [ColorBytes](Heirloom.Drawing.ColorBytes.md)

<small>`Static`, `Read Only`</small>

#### <a name="RAIC72A67E4"></a>Rainbow : IReadOnlyList\<ColorBytes>

<small>`Static`, `Read Only`</small>

#### <a name="RANE1E4B317"></a>Random : [ColorBytes](Heirloom.Drawing.ColorBytes.md)

<small>`Static`, `Read Only`</small>

### Methods

#### <a name="PAR50D6FF39"></a>Parse(string color) : [ColorBytes](Heirloom.Drawing.ColorBytes.md)

<small>`Static`</small>

Parses a hex-string representation of a color. May be formatted as 'RGB', 'RGBA', 'RRGGBB', 'RRGGBBAA' with or without a preceding '#'.

<small>**color**: <param name="color">The hex-encoded string.</param>  
</small>

#### <a name="TRY99E0F751"></a>TryParse(string color, out [ColorBytes](Heirloom.Drawing.ColorBytes.md) outColor) : bool

<small>`Static`</small>

Parses a hex-string representation of a color. May be formatted as 'RGB', 'RGBA', 'RRGGBB', 'RRGGBBAA' with or without a preceding '#'.

<small>**color**: <param name="color">The hex-encoded string.</param>  
</small>
<small>**outColor**: <param name="outColor">Outputs the parsed color.</param>  
</small>

#### <a name="LERDF0BF6B4"></a>Lerp([ColorBytes](Heirloom.Drawing.ColorBytes.md) source, [ColorBytes](Heirloom.Drawing.ColorBytes.md) target, float factor) : [ColorBytes](Heirloom.Drawing.ColorBytes.md)

<small>`Static`</small>

Interpolate two colors together.

<small>**source**: <param name="source">Source color</param>  
</small>
<small>**target**: <param name="target">Target color.</param>  
</small>
<small>**factor**: <param name="factor">Blending factor (0.0 to 1.0)</param>  
</small>

#### <a name="MULBEBFA083"></a>Multiply(in [ColorBytes](Heirloom.Drawing.ColorBytes.md) c1, in [ColorBytes](Heirloom.Drawing.ColorBytes.md) c2, ref [ColorBytes](Heirloom.Drawing.ColorBytes.md) target) : void

<small>`Static`</small>

Multiplies two [ColorBytes](Heirloom.Drawing.ColorBytes.md) together. Behaves the same as [Color](Heirloom.Drawing.Color.md).


