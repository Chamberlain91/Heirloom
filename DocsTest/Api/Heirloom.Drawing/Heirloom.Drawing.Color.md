# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../Heirloom.Drawing/Heirloom.Drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## Color (Struct)
<small>**Namespace**: Heirloom.Drawing</sub></small>  
<small>**Interfaces**: IEquatable\<Color></small>  

Color encoded as 4 component floats.

| Fields          | Summary                           |
|-----------------|-----------------------------------|
| [R](#RCDCAB7F0) | The red component.                |
| [G](#GCDCAB7DB) | The green component.              |
| [B](#BCDCAB7E0) | The blue component.               |
| [A](#ACDCAB7DD) | The alpha/transparency component. |

| Properties                  | Summary                                                |
|-----------------------------|--------------------------------------------------------|
| [Luminosity](#LUM27143E8B)  | Computes a luminosity component (grayscale).           |
| [Inverted](#INVDE5124E3)    | The inversion of this color.                           |
| [Hue](#HUE35011EF8)         | Gets or sets the (HSV) hue of this color.              |
| [Brightness](#BRI84512DF5)  | Gets or sets the (HSV) brightness value of this color. |
| [Saturation](#SAT68C59AFE)  | Gets or sets the (HSV) saturation of this color.       |
| [Red](#RED5F786973)         |                                                        |
| [Green](#GREE8614423)       |                                                        |
| [Blue](#BLU5D4613EE)        |                                                        |
| [Yellow](#YELCC917366)      |                                                        |
| [Cyan](#CYA95C74181)        |                                                        |
| [Magenta](#MAGEF2FDA29)     |                                                        |
| [White](#WHI1AD967BB)       |                                                        |
| [Black](#BLAF663517F)       |                                                        |
| [Gray](#GRA1F30CD89)        |                                                        |
| [DarkGray](#DAR69CE9AE9)    |                                                        |
| [LightGray](#LIG2E3D3CFB)   |                                                        |
| [Orange](#ORA78ADA558)      |                                                        |
| [Indigo](#IND56133F0)       |                                                        |
| [Violet](#VIOD94877FD)      |                                                        |
| [Pink](#PINE1E27C5E)        |                                                        |
| [Transparent](#TRA962107FC) |                                                        |
| [Rainbow](#RAIC72A67E4)     |                                                        |
| [Random](#RANE1E4B317)      |                                                        |

| Methods                  | Summary                                                                                                                                 |
|--------------------------|-----------------------------------------------------------------------------------------------------------------------------------------|
| [ToHSV](#TOH70FC404C)    | Extracts the HSV values from this color.                                                                                                |
| [Parse](#PARB2AE6A55)    | Parses a hex-string representation of a color. May be formatted as 'RGB', 'RGBA', 'RRGGBB', 'RRGGBBAA' with or without a preceding '#'. |
| [TryParse](#TRYB6E4194A) | Parses a hex-string representation of a color. May be formatted as 'RGB', 'RGBA', 'RRGGBB', 'RRGGBBAA' with or without a preceding '#'. |
| [FromHSV](#FRO5B1AAFFF)  | Converts HSV values into a RGBA color.                                                                                                  |
| [Lerp](#LER252E49EB)     | Interpolate two colors together.                                                                                                        |

### Fields

#### <a name="RCDCAB7F0"></a>R : float

The red component.

#### <a name="GCDCAB7DB"></a>G : float

The green component.

#### <a name="BCDCAB7E0"></a>B : float

The blue component.

#### <a name="ACDCAB7DD"></a>A : float

The alpha/transparency component.

### Constructors

#### Color(float r, float g, float b, float a = 1)

### Properties

#### <a name="LUM27143E8B"></a>Luminosity : float

<small>`Read Only`</small>

Computes a luminosity component (grayscale).

#### <a name="INVDE5124E3"></a>Inverted : [Color](Heirloom.Drawing.Color.md)

<small>`Read Only`</small>

The inversion of this color.

#### <a name="HUE35011EF8"></a>Hue : float


Gets or sets the (HSV) hue of this color.

#### <a name="BRI84512DF5"></a>Brightness : float


Gets or sets the (HSV) brightness value of this color.

#### <a name="SAT68C59AFE"></a>Saturation : float


Gets or sets the (HSV) saturation of this color.

#### <a name="RED5F786973"></a>Red : [Color](Heirloom.Drawing.Color.md)

<small>`Static`, `Read Only`</small>

#### <a name="GREE8614423"></a>Green : [Color](Heirloom.Drawing.Color.md)

<small>`Static`, `Read Only`</small>

#### <a name="BLU5D4613EE"></a>Blue : [Color](Heirloom.Drawing.Color.md)

<small>`Static`, `Read Only`</small>

#### <a name="YELCC917366"></a>Yellow : [Color](Heirloom.Drawing.Color.md)

<small>`Static`, `Read Only`</small>

#### <a name="CYA95C74181"></a>Cyan : [Color](Heirloom.Drawing.Color.md)

<small>`Static`, `Read Only`</small>

#### <a name="MAGEF2FDA29"></a>Magenta : [Color](Heirloom.Drawing.Color.md)

<small>`Static`, `Read Only`</small>

#### <a name="WHI1AD967BB"></a>White : [Color](Heirloom.Drawing.Color.md)

<small>`Static`, `Read Only`</small>

#### <a name="BLAF663517F"></a>Black : [Color](Heirloom.Drawing.Color.md)

<small>`Static`, `Read Only`</small>

#### <a name="GRA1F30CD89"></a>Gray : [Color](Heirloom.Drawing.Color.md)

<small>`Static`, `Read Only`</small>

#### <a name="DAR69CE9AE9"></a>DarkGray : [Color](Heirloom.Drawing.Color.md)

<small>`Static`, `Read Only`</small>

#### <a name="LIG2E3D3CFB"></a>LightGray : [Color](Heirloom.Drawing.Color.md)

<small>`Static`, `Read Only`</small>

#### <a name="ORA78ADA558"></a>Orange : [Color](Heirloom.Drawing.Color.md)

<small>`Static`, `Read Only`</small>

#### <a name="IND56133F0"></a>Indigo : [Color](Heirloom.Drawing.Color.md)

<small>`Static`, `Read Only`</small>

#### <a name="VIOD94877FD"></a>Violet : [Color](Heirloom.Drawing.Color.md)

<small>`Static`, `Read Only`</small>

#### <a name="PINE1E27C5E"></a>Pink : [Color](Heirloom.Drawing.Color.md)

<small>`Static`, `Read Only`</small>

#### <a name="TRA962107FC"></a>Transparent : [Color](Heirloom.Drawing.Color.md)

<small>`Static`, `Read Only`</small>

#### <a name="RAIC72A67E4"></a>Rainbow : IReadOnlyList\<Color>

<small>`Static`, `Read Only`</small>

#### <a name="RANE1E4B317"></a>Random : [Color](Heirloom.Drawing.Color.md)

<small>`Static`, `Read Only`</small>

### Methods

#### <a name="TOH917B084C"></a>ToHSV(out float hue, out float saturation, out float value) : void

Extracts the HSV values from this color.

<small>**hue**: <param name="hue">The hue (0 to 360).</param></small>  
<small>**saturation**: <param name="saturation">The saturation (0.0 to 1.0).</param></small>  
<small>**value**: <param name="value">The value (0.0 to 1.0).</param></small>  

#### <a name="PARC735922D"></a>Parse(string color) : [Color](Heirloom.Drawing.Color.md)
<small>`Static`</small>

Parses a hex-string representation of a color. May be formatted as 'RGB', 'RGBA', 'RRGGBB', 'RRGGBBAA' with or without a preceding '#'.

<small>**color**: <param name="color">The hex-encoded string.</param></small>  

#### <a name="TRY24767EF5"></a>TryParse(string color, out [Color](Heirloom.Drawing.Color.md) outColor) : bool
<small>`Static`</small>

Parses a hex-string representation of a color. May be formatted as 'RGB', 'RGBA', 'RRGGBB', 'RRGGBBAA' with or without a preceding '#'.

<small>**color**: <param name="color">The hex-encoded string.</param></small>  
<small>**outColor**: <param name="outColor">Outputs the parsed color.</param></small>  

#### <a name="FRO114990E"></a>FromHSV(float hue, float saturation, float value, float alpha = 1) : [Color](Heirloom.Drawing.Color.md)
<small>`Static`</small>

Converts HSV values into a RGBA color.

<small>**hue**: <param name="hue">The hue (0 to 360).</param></small>  
<small>**saturation**: <param name="saturation">The saturation (0.0 to 1.0).</param></small>  
<small>**value**: <param name="value">The value (0.0 to 1.0).</param></small>  
<small>**alpha**: <param name="alpha">The opacity (0.0 to 1.0).</param></small>  

#### <a name="LER5DCDA93B"></a>Lerp([Color](Heirloom.Drawing.Color.md) source, [Color](Heirloom.Drawing.Color.md) target, float t) : [Color](Heirloom.Drawing.Color.md)
<small>`Static`</small>

Interpolate two colors together.

<small>**source**: <param name="source">Source color</param></small>  
<small>**target**: <param name="target">Target color.</param></small>  

