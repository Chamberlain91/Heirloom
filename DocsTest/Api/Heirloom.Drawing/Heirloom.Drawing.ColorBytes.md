# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../Heirloom.Drawing/Heirloom.Drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## ColorBytes (Struct)
<small>**Namespace**: Heirloom.Drawing</sub></small>  
<small>**Interfaces**: IEquatable\<ColorBytes></small>  

Color encoded as 4 component bytes.

| Fields      | Summary                           |
|-------------|-----------------------------------|
| [R](#RCDCA) | The red component.                |
| [G](#GCDCA) | The green component.              |
| [B](#BCDCA) | The blue component.               |
| [A](#ACDCA) | The alpha/transparency component. |

| Properties               | Summary                                      |
|--------------------------|----------------------------------------------|
| [Luminosity](#LUMI2714)  | Computes a luminosity component (grayscale). |
| [Inverted](#INVEDE51)    | The inversion of this color.                 |
| [Red](#RED5F78)          |                                              |
| [Green](#GREEE861)       |                                              |
| [Blue](#BLUE5D46)        |                                              |
| [Yellow](#YELLCC91)      |                                              |
| [Cyan](#CYAN95C7)        |                                              |
| [Magenta](#MAGEEF2F)     |                                              |
| [White](#WHIT1AD9)       |                                              |
| [Black](#BLACF663)       |                                              |
| [Gray](#GRAY1F30)        |                                              |
| [DarkGray](#DARK69CE)    |                                              |
| [LightGray](#LIGH2E3D)   |                                              |
| [Orange](#ORAN78AD)      |                                              |
| [Indigo](#INDI5613)      |                                              |
| [Violet](#VIOLD948)      |                                              |
| [Pink](#PINKE1E2)        |                                              |
| [Transparent](#TRAN9621) |                                              |
| [Rainbow](#RAINC72A)     |                                              |
| [Random](#RANDE1E4)      |                                              |

| Methods               | Summary                                                                                                                                 |
|-----------------------|-----------------------------------------------------------------------------------------------------------------------------------------|
| [Parse](#PARSB2AE)    | Parses a hex-string representation of a color. May be formatted as 'RGB', 'RGBA', 'RRGGBB', 'RRGGBBAA' with or without a preceding '#'. |
| [TryParse](#TRYPB6E4) | Parses a hex-string representation of a color. May be formatted as 'RGB', 'RGBA', 'RRGGBB', 'RRGGBBAA' with or without a preceding '#'. |
| [Lerp](#LERP252E)     | Interpolate two colors together.                                                                                                        |
| [Multiply](#MULTD34B) | Multiplies two [ColorBytes](Heirloom.Drawing.ColorBytes.md) together. Behaves the same as [Color](Heirloom.Drawing.Color.md).           |

### Fields

#### <a name="RCDCA"></a> R :  byte

The red component.

#### <a name="GCDCA"></a> G :  byte

The green component.

#### <a name="BCDCA"></a> B :  byte

The blue component.

#### <a name="ACDCA"></a> A :  byte

The alpha/transparency component.

### Constructors

#### ColorBytes(byte r,  byte g,  byte b,  byte a = 255)

### Properties

#### <a name="LUMI2714"></a> Luminosity :  byte

<small>`Read Only`</small>

Computes a luminosity component (grayscale).

#### <a name="INVEDE51"></a> Inverted : [ColorBytes](Heirloom.Drawing.ColorBytes.md)

<small>`Read Only`</small>

The inversion of this color.

#### <a name="RED5F78"></a> Red : [ColorBytes](Heirloom.Drawing.ColorBytes.md)

<small>`Static`, `Read Only`</small>

#### <a name="GREEE861"></a> Green : [ColorBytes](Heirloom.Drawing.ColorBytes.md)

<small>`Static`, `Read Only`</small>

#### <a name="BLUE5D46"></a> Blue : [ColorBytes](Heirloom.Drawing.ColorBytes.md)

<small>`Static`, `Read Only`</small>

#### <a name="YELLCC91"></a> Yellow : [ColorBytes](Heirloom.Drawing.ColorBytes.md)

<small>`Static`, `Read Only`</small>

#### <a name="CYAN95C7"></a> Cyan : [ColorBytes](Heirloom.Drawing.ColorBytes.md)

<small>`Static`, `Read Only`</small>

#### <a name="MAGEEF2F"></a> Magenta : [ColorBytes](Heirloom.Drawing.ColorBytes.md)

<small>`Static`, `Read Only`</small>

#### <a name="WHIT1AD9"></a> White : [ColorBytes](Heirloom.Drawing.ColorBytes.md)

<small>`Static`, `Read Only`</small>

#### <a name="BLACF663"></a> Black : [ColorBytes](Heirloom.Drawing.ColorBytes.md)

<small>`Static`, `Read Only`</small>

#### <a name="GRAY1F30"></a> Gray : [ColorBytes](Heirloom.Drawing.ColorBytes.md)

<small>`Static`, `Read Only`</small>

#### <a name="DARK69CE"></a> DarkGray : [ColorBytes](Heirloom.Drawing.ColorBytes.md)

<small>`Static`, `Read Only`</small>

#### <a name="LIGH2E3D"></a> LightGray : [ColorBytes](Heirloom.Drawing.ColorBytes.md)

<small>`Static`, `Read Only`</small>

#### <a name="ORAN78AD"></a> Orange : [ColorBytes](Heirloom.Drawing.ColorBytes.md)

<small>`Static`, `Read Only`</small>

#### <a name="INDI5613"></a> Indigo : [ColorBytes](Heirloom.Drawing.ColorBytes.md)

<small>`Static`, `Read Only`</small>

#### <a name="VIOLD948"></a> Violet : [ColorBytes](Heirloom.Drawing.ColorBytes.md)

<small>`Static`, `Read Only`</small>

#### <a name="PINKE1E2"></a> Pink : [ColorBytes](Heirloom.Drawing.ColorBytes.md)

<small>`Static`, `Read Only`</small>

#### <a name="TRAN9621"></a> Transparent : [ColorBytes](Heirloom.Drawing.ColorBytes.md)

<small>`Static`, `Read Only`</small>

#### <a name="RAINC72A"></a> Rainbow : IReadOnlyList\<ColorBytes>

<small>`Static`, `Read Only`</small>

#### <a name="RANDE1E4"></a> Random : [ColorBytes](Heirloom.Drawing.ColorBytes.md)

<small>`Static`, `Read Only`</small>

### Methods

#### <a name="PARS50D6"></a> Parse(string color) : [ColorBytes](Heirloom.Drawing.ColorBytes.md)
<small>`Static`</small>

Parses a hex-string representation of a color. May be formatted as 'RGB', 'RGBA', 'RRGGBB', 'RRGGBBAA' with or without a preceding '#'.

<small>**color**: <param name="color">The hex-encoded string.</param></small>  

#### <a name="TRYP99E0"></a> TryParse(string color, out [ColorBytes](Heirloom.Drawing.ColorBytes.md) outColor) : bool
<small>`Static`</small>

Parses a hex-string representation of a color. May be formatted as 'RGB', 'RGBA', 'RRGGBB', 'RRGGBBAA' with or without a preceding '#'.

<small>**color**: <param name="color">The hex-encoded string.</param></small>  
<small>**outColor**: <param name="outColor">Outputs the parsed color.</param></small>  

#### <a name="LERPDF0B"></a> Lerp([ColorBytes](Heirloom.Drawing.ColorBytes.md) source, [ColorBytes](Heirloom.Drawing.ColorBytes.md) target, float factor) : [ColorBytes](Heirloom.Drawing.ColorBytes.md)
<small>`Static`</small>

Interpolate two colors together.

<small>**source**: <param name="source">Source color</param></small>  
<small>**target**: <param name="target">Target color.</param></small>  
<small>**factor**: <param name="factor">Blending factor (0.0 to 1.0)</param></small>  

#### <a name="MULTBEBF"></a> Multiply(in [ColorBytes](Heirloom.Drawing.ColorBytes.md) c1, in [ColorBytes](Heirloom.Drawing.ColorBytes.md) c2, ref [ColorBytes](Heirloom.Drawing.ColorBytes.md) target) : void
<small>`Static`</small>

Multiplies two [ColorBytes](Heirloom.Drawing.ColorBytes.md) together. Behaves the same as [Color](Heirloom.Drawing.Color.md).


