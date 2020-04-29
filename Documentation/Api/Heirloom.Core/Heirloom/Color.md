# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  

## Color Struct

> **Namespace**: [Heirloom][0]  

Color encoded as 4 component floats.

```cs
public struct Color : IEquatable<Color>
```

### Inherits

IEquatable\<Color>

#### Fields

[R][1], [G][2], [B][3], [A][4]

#### Properties

[Luminosity][5], [Inverted][6], [Hue][7], [Brightness][8], [Saturation][9]

#### Methods

[Set][10], [ToHSV][11]

#### Static Properties

[Red][12], [Green][13], [Blue][14], [Yellow][15], [Cyan][16], [Magenta][17], [White][18], [Black][19], [Gray][20], [DarkGray][21], [LightGray][22], [Orange][23], [Indigo][24], [Violet][25], [Pink][26], [Transparent][27], [Rainbow][28], [Random][29]

#### Static Methods

[Parse][30], [TryParse][31], [FromHSV][32], [Lerp][33]

## Fields

| Name   | Summary                           |
|--------|-----------------------------------|
| [R][1] | The red component.                |
| [G][2] | The green component.              |
| [B][3] | The blue component.               |
| [A][4] | The alpha/transparency component. |

## Properties

| Name              | Summary                                                |
|-------------------|--------------------------------------------------------|
| [Luminosity][5]   | Computes a luminosity component (grayscale).           |
| [Inverted][6]     | The inversion of this color.                           |
| [Hue][7]          | Gets or sets the (HSV) hue of this color.              |
| [Brightness][8]   | Gets or sets the (HSV) brightness value of this color. |
| [Saturation][9]   | Gets or sets the (HSV) saturation of this color.       |
| [Red][12]         |                                                        |
| [Green][13]       |                                                        |
| [Blue][14]        |                                                        |
| [Yellow][15]      |                                                        |
| [Cyan][16]        |                                                        |
| [Magenta][17]     |                                                        |
| [White][18]       |                                                        |
| [Black][19]       |                                                        |
| [Gray][20]        |                                                        |
| [DarkGray][21]    |                                                        |
| [LightGray][22]   |                                                        |
| [Orange][23]      |                                                        |
| [Indigo][24]      |                                                        |
| [Violet][25]      |                                                        |
| [Pink][26]        |                                                        |
| [Transparent][27] |                                                        |
| [Rainbow][28]     |                                                        |
| [Random][29]      |                                                        |

## Methods

| Name           | Summary                                                                                                                                 |
|----------------|-----------------------------------------------------------------------------------------------------------------------------------------|
| [Set][10]      | Sets the components of this color.                                                                                                      |
| [ToHSV][11]    | Extracts the HSV values from this color.                                                                                                |
| [Parse][30]    | Parses a hex-string representation of a color. May be formatted as 'RGB', 'RGBA', 'RRGGBB', 'RRGGBBAA' with or without a preceding '#'. |
| [TryParse][31] | Parses a hex-string representation of a color. May be formatted as 'RGB', 'RGBA', 'RRGGBB', 'RRGGBBAA' with or without a preceding '#'. |
| [FromHSV][32]  | Converts HSV values into a RGBA color.                                                                                                  |
| [Lerp][33]     | Interpolate two colors together.                                                                                                        |

[0]: ../../Heirloom.Core.md
[1]: Color/R.md
[2]: Color/G.md
[3]: Color/B.md
[4]: Color/A.md
[5]: Color/Luminosity.md
[6]: Color/Inverted.md
[7]: Color/Hue.md
[8]: Color/Brightness.md
[9]: Color/Saturation.md
[10]: Color/Set.md
[11]: Color/ToHSV.md
[12]: Color/Red.md
[13]: Color/Green.md
[14]: Color/Blue.md
[15]: Color/Yellow.md
[16]: Color/Cyan.md
[17]: Color/Magenta.md
[18]: Color/White.md
[19]: Color/Black.md
[20]: Color/Gray.md
[21]: Color/DarkGray.md
[22]: Color/LightGray.md
[23]: Color/Orange.md
[24]: Color/Indigo.md
[25]: Color/Violet.md
[26]: Color/Pink.md
[27]: Color/Transparent.md
[28]: Color/Rainbow.md
[29]: Color/Random.md
[30]: Color/Parse.md
[31]: Color/TryParse.md
[32]: Color/FromHSV.md
[33]: Color/Lerp.md
