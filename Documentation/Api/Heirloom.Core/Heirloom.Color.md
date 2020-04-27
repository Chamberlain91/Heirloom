# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  

## Color

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

[0]: ../Heirloom.Core.md
[1]: Heirloom.Color.R.md
[2]: Heirloom.Color.G.md
[3]: Heirloom.Color.B.md
[4]: Heirloom.Color.A.md
[5]: Heirloom.Color.Luminosity.md
[6]: Heirloom.Color.Inverted.md
[7]: Heirloom.Color.Hue.md
[8]: Heirloom.Color.Brightness.md
[9]: Heirloom.Color.Saturation.md
[10]: Heirloom.Color.Set.md
[11]: Heirloom.Color.ToHSV.md
[12]: Heirloom.Color.Red.md
[13]: Heirloom.Color.Green.md
[14]: Heirloom.Color.Blue.md
[15]: Heirloom.Color.Yellow.md
[16]: Heirloom.Color.Cyan.md
[17]: Heirloom.Color.Magenta.md
[18]: Heirloom.Color.White.md
[19]: Heirloom.Color.Black.md
[20]: Heirloom.Color.Gray.md
[21]: Heirloom.Color.DarkGray.md
[22]: Heirloom.Color.LightGray.md
[23]: Heirloom.Color.Orange.md
[24]: Heirloom.Color.Indigo.md
[25]: Heirloom.Color.Violet.md
[26]: Heirloom.Color.Pink.md
[27]: Heirloom.Color.Transparent.md
[28]: Heirloom.Color.Rainbow.md
[29]: Heirloom.Color.Random.md
[30]: Heirloom.Color.Parse.md
[31]: Heirloom.Color.TryParse.md
[32]: Heirloom.Color.FromHSV.md
[33]: Heirloom.Color.Lerp.md
