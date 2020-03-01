# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../Heirloom.Drawing/Heirloom.Drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## Color (Struct)
<small>**Namespace**: Heirloom.Drawing</sub></small>  
<small>**Interfaces**: IEquatable\<Color></small>  

Color encoded as 4 component floats.

| Fields      | Summary                           |
|-------------|-----------------------------------|
| [R](#RCDCA) | The red component.                |
| [G](#GCDCA) | The green component.              |
| [B](#BCDCA) | The blue component.               |
| [A](#ACDCA) | The alpha/transparency component. |

| Properties               | Summary                                                |
|--------------------------|--------------------------------------------------------|
| [Luminosity](#LUMI2714)  | Computes a luminosity component (grayscale).           |
| [Inverted](#INVEDE51)    | The inversion of this color.                           |
| [Hue](#HUE3501)          | Gets or sets the (HSV) hue of this color.              |
| [Brightness](#BRIG8451)  | Gets or sets the (HSV) brightness value of this color. |
| [Saturation](#SATU68C5)  | Gets or sets the (HSV) saturation of this color.       |
| [Red](#RED5F78)          |                                                        |
| [Green](#GREEE861)       |                                                        |
| [Blue](#BLUE5D46)        |                                                        |
| [Yellow](#YELLCC91)      |                                                        |
| [Cyan](#CYAN95C7)        |                                                        |
| [Magenta](#MAGEEF2F)     |                                                        |
| [White](#WHIT1AD9)       |                                                        |
| [Black](#BLACF663)       |                                                        |
| [Gray](#GRAY1F30)        |                                                        |
| [DarkGray](#DARK69CE)    |                                                        |
| [LightGray](#LIGH2E3D)   |                                                        |
| [Orange](#ORAN78AD)      |                                                        |
| [Indigo](#INDI5613)      |                                                        |
| [Violet](#VIOLD948)      |                                                        |
| [Pink](#PINKE1E2)        |                                                        |
| [Transparent](#TRAN9621) |                                                        |
| [Rainbow](#RAINC72A)     |                                                        |
| [Random](#RANDE1E4)      |                                                        |

| Methods               | Summary                                                                                                                                 |
|-----------------------|-----------------------------------------------------------------------------------------------------------------------------------------|
| [ToHSV](#TOHS70FC)    | Extracts the HSV values from this color.                                                                                                |
| [Parse](#PARSB2AE)    | Parses a hex-string representation of a color. May be formatted as 'RGB', 'RGBA', 'RRGGBB', 'RRGGBBAA' with or without a preceding '#'. |
| [TryParse](#TRYPB6E4) | Parses a hex-string representation of a color. May be formatted as 'RGB', 'RGBA', 'RRGGBB', 'RRGGBBAA' with or without a preceding '#'. |
| [FromHSV](#FROM5B1A)  | Converts HSV values into a RGBA color.                                                                                                  |
| [Lerp](#LERP252E)     | Interpolate two colors together.                                                                                                        |

### Fields

#### <a name="RCDCA"></a> R : float

The red component.

#### <a name="GCDCA"></a> G : float

The green component.

#### <a name="BCDCA"></a> B : float

The blue component.

#### <a name="ACDCA"></a> A : float

The alpha/transparency component.

### Constructors

#### Color(float r, float g, float b, float a = 1)

### Properties

#### <a name="LUMI2714"></a> Luminosity : float

<small>`Read Only`</small>

Computes a luminosity component (grayscale).

#### <a name="INVEDE51"></a> Inverted : [Color](Heirloom.Drawing.Color.md)

<small>`Read Only`</small>

The inversion of this color.

#### <a name="HUE3501"></a> Hue : float


Gets or sets the (HSV) hue of this color.

#### <a name="BRIG8451"></a> Brightness : float


Gets or sets the (HSV) brightness value of this color.

#### <a name="SATU68C5"></a> Saturation : float


Gets or sets the (HSV) saturation of this color.

#### <a name="RED5F78"></a> Red : [Color](Heirloom.Drawing.Color.md)

<small>`Static`, `Read Only`</small>

#### <a name="GREEE861"></a> Green : [Color](Heirloom.Drawing.Color.md)

<small>`Static`, `Read Only`</small>

#### <a name="BLUE5D46"></a> Blue : [Color](Heirloom.Drawing.Color.md)

<small>`Static`, `Read Only`</small>

#### <a name="YELLCC91"></a> Yellow : [Color](Heirloom.Drawing.Color.md)

<small>`Static`, `Read Only`</small>

#### <a name="CYAN95C7"></a> Cyan : [Color](Heirloom.Drawing.Color.md)

<small>`Static`, `Read Only`</small>

#### <a name="MAGEEF2F"></a> Magenta : [Color](Heirloom.Drawing.Color.md)

<small>`Static`, `Read Only`</small>

#### <a name="WHIT1AD9"></a> White : [Color](Heirloom.Drawing.Color.md)

<small>`Static`, `Read Only`</small>

#### <a name="BLACF663"></a> Black : [Color](Heirloom.Drawing.Color.md)

<small>`Static`, `Read Only`</small>

#### <a name="GRAY1F30"></a> Gray : [Color](Heirloom.Drawing.Color.md)

<small>`Static`, `Read Only`</small>

#### <a name="DARK69CE"></a> DarkGray : [Color](Heirloom.Drawing.Color.md)

<small>`Static`, `Read Only`</small>

#### <a name="LIGH2E3D"></a> LightGray : [Color](Heirloom.Drawing.Color.md)

<small>`Static`, `Read Only`</small>

#### <a name="ORAN78AD"></a> Orange : [Color](Heirloom.Drawing.Color.md)

<small>`Static`, `Read Only`</small>

#### <a name="INDI5613"></a> Indigo : [Color](Heirloom.Drawing.Color.md)

<small>`Static`, `Read Only`</small>

#### <a name="VIOLD948"></a> Violet : [Color](Heirloom.Drawing.Color.md)

<small>`Static`, `Read Only`</small>

#### <a name="PINKE1E2"></a> Pink : [Color](Heirloom.Drawing.Color.md)

<small>`Static`, `Read Only`</small>

#### <a name="TRAN9621"></a> Transparent : [Color](Heirloom.Drawing.Color.md)

<small>`Static`, `Read Only`</small>

#### <a name="RAINC72A"></a> Rainbow : IReadOnlyList\<Color>

<small>`Static`, `Read Only`</small>

#### <a name="RANDE1E4"></a> Random : [Color](Heirloom.Drawing.Color.md)

<small>`Static`, `Read Only`</small>

### Methods

#### <a name="TOHS917B"></a> ToHSV(out float hue, out float saturation, out float value) : void

Extracts the HSV values from this color.

<small>**hue**: <param name="hue">The hue (0 to 360).</param></small>  
<small>**saturation**: <param name="saturation">The saturation (0.0 to 1.0).</param></small>  
<small>**value**: <param name="value">The value (0.0 to 1.0).</param></small>  

#### <a name="PARSC735"></a> Parse(string color) : [Color](Heirloom.Drawing.Color.md)
<small>`Static`</small>

Parses a hex-string representation of a color. May be formatted as 'RGB', 'RGBA', 'RRGGBB', 'RRGGBBAA' with or without a preceding '#'.

<small>**color**: <param name="color">The hex-encoded string.</param></small>  

#### <a name="TRYP2476"></a> TryParse(string color, out [Color](Heirloom.Drawing.Color.md) outColor) : bool
<small>`Static`</small>

Parses a hex-string representation of a color. May be formatted as 'RGB', 'RGBA', 'RRGGBB', 'RRGGBBAA' with or without a preceding '#'.

<small>**color**: <param name="color">The hex-encoded string.</param></small>  
<small>**outColor**: <param name="outColor">Outputs the parsed color.</param></small>  

#### <a name="FROM1149"></a> FromHSV(float hue, float saturation, float value, float alpha = 1) : [Color](Heirloom.Drawing.Color.md)
<small>`Static`</small>

Converts HSV values into a RGBA color.

<small>**hue**: <param name="hue">The hue (0 to 360).</param></small>  
<small>**saturation**: <param name="saturation">The saturation (0.0 to 1.0).</param></small>  
<small>**value**: <param name="value">The value (0.0 to 1.0).</param></small>  
<small>**alpha**: <param name="alpha">The opacity (0.0 to 1.0).</param></small>  

#### <a name="LERP5DCD"></a> Lerp([Color](Heirloom.Drawing.Color.md) source, [Color](Heirloom.Drawing.Color.md) target, float t) : [Color](Heirloom.Drawing.Color.md)
<small>`Static`</small>

Interpolate two colors together.

<small>**source**: <param name="source">Source color</param></small>  
<small>**target**: <param name="target">Target color.</param></small>  

