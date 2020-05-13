# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Color (Struct)

> **Namespace**: [Heirloom][0]

Color encoded as 4 component floats.

```cs
public struct Color : IEquatable<Color>
```

### Inherits

IEquatable\<Color>

### Fields

[A][1], [B][2], [G][3], [R][4]

### Properties

[Brightness][5], [Hue][6], [Inverted][7], [Luminosity][8], [Saturation][9]

### Methods

[Set][10], [ToHSV][11]

### Static Properties

[Black][12], [Blue][13], [Cyan][14], [DarkGray][15], [Gray][16], [Green][17], [Indigo][18], [LightGray][19], [Magenta][20], [Orange][21], [Pink][22], [Red][23], [Transparent][24], [Violet][25], [White][26], [Yellow][27]

### Static Methods

[FromHSV][28], [Lerp][29], [Parse][30], [TryParse][31]

## Fields

#### Instance

| Name   | Type    | Summary                           |
|--------|---------|-----------------------------------|
| [A][1] | `float` | The alpha/transparency component. |
| [B][2] | `float` | The blue component.               |
| [G][3] | `float` | The green component.              |
| [R][4] | `float` | The red component.                |

## Properties

#### Instance

| Name            | Type        | Summary                                                |
|-----------------|-------------|--------------------------------------------------------|
| [Brightness][5] | `float`     | Gets or sets the (HSV) brightness value of this color. |
| [Hue][6]        | `float`     | Gets or sets the (HSV) hue of this color.              |
| [Inverted][7]   | [Color][32] | The inversion of this color.                           |
| [Luminosity][8] | `float`     | Computes a luminosity component (grayscale).           |
| [Saturation][9] | `float`     | Gets or sets the (HSV) saturation of this color.       |

#### Static

| Name              | Type        | Summary                                  |
|-------------------|-------------|------------------------------------------|
| [Black][12]       | [Color][32] | The color black (#000000).               |
| [Blue][13]        | [Color][32] | The color blue (#0000FF).                |
| [Cyan][14]        | [Color][32] | The color cyan (#00FFFF).                |
| [DarkGray][15]    | [Color][32] | The color dark gray (#333333).           |
| [Gray][16]        | [Color][32] | The color gray (#999999).                |
| [Green][17]       | [Color][32] | The color green (#00FF00).               |
| [Indigo][18]      | [Color][32] | The color indigo (#4B0082).              |
| [LightGray][19]   | [Color][32] | The color light gray (#CCCCCC).          |
| [Magenta][20]     | [Color][32] | The color magenta (#FF00FF).             |
| [Orange][21]      | [Color][32] | The color orange (#FF8811).              |
| [Pink][22]        | [Color][32] | The color pink (#DD55AA).                |
| [Red][23]         | [Color][32] | The color red (#FF0000).                 |
| [Transparent][24] | [Color][32] | The color transparent black (#00000000). |
| [Violet][25]      | [Color][32] | The color violet (#8A2BE2).              |
| [White][26]       | [Color][32] | The color white (#FFFFFF).               |
| [Yellow][27]      | [Color][32] | The color yellow (#FFFF00).              |

## Methods

#### Instance

| Name                            | Return Type | Summary                                  |
|---------------------------------|-------------|------------------------------------------|
| [Set(float, float, floa...][10] | `void`      | Sets the components of this color.       |
| [ToHSV(out float, out f...][11] | `void`      | Extracts the HSV values from this color. |

#### Static

| Name                            | Return Type | Summary                                                                |
|---------------------------------|-------------|------------------------------------------------------------------------|
| [FromHSV(float, float, ...][28] | [Color][32] | Converts HSV values into a RGBA color.                                 |
| [Lerp(Color, Color, float)][29] | [Color][32] | Interpolate two colors together.                                       |
| [Parse(string)][30]             | [Color][32] | Parses a hex-string representation of a color. May be formatted as ... |
| [TryParse(string, out C...][31] | `bool`      | Parses a hex-string representation of a color. May be formatted as ... |

[0]: ../../Heirloom.Core.md
[1]: Color/A.md
[2]: Color/B.md
[3]: Color/G.md
[4]: Color/R.md
[5]: Color/Brightness.md
[6]: Color/Hue.md
[7]: Color/Inverted.md
[8]: Color/Luminosity.md
[9]: Color/Saturation.md
[10]: Color/Set.md
[11]: Color/ToHSV.md
[12]: Color/Black.md
[13]: Color/Blue.md
[14]: Color/Cyan.md
[15]: Color/DarkGray.md
[16]: Color/Gray.md
[17]: Color/Green.md
[18]: Color/Indigo.md
[19]: Color/LightGray.md
[20]: Color/Magenta.md
[21]: Color/Orange.md
[22]: Color/Pink.md
[23]: Color/Red.md
[24]: Color/Transparent.md
[25]: Color/Violet.md
[26]: Color/White.md
[27]: Color/Yellow.md
[28]: Color/FromHSV.md
[29]: Color/Lerp.md
[30]: Color/Parse.md
[31]: Color/TryParse.md
[32]: Color.md
