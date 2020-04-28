# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  

## ColorBytes

> **Namespace**: [Heirloom][0]  

Color encoded as 4 component bytes.

```cs
public struct ColorBytes : IEquatable<ColorBytes>
```

### Inherits

IEquatable\<ColorBytes>

#### Fields

[R][1], [G][2], [B][3], [A][4]

#### Properties

[Luminosity][5], [Inverted][6]

#### Methods

[Set][7]

#### Static Properties

[Red][8], [Green][9], [Blue][10], [Yellow][11], [Cyan][12], [Magenta][13], [White][14], [Black][15], [Gray][16], [DarkGray][17], [LightGray][18], [Orange][19], [Indigo][20], [Violet][21], [Pink][22], [Transparent][23], [Rainbow][24], [Random][25]

#### Static Methods

[Parse][26], [TryParse][27], [Lerp][28], [Multiply][29]

## Fields

| Name   | Summary                           |
|--------|-----------------------------------|
| [R][1] | The red component.                |
| [G][2] | The green component.              |
| [B][3] | The blue component.               |
| [A][4] | The alpha/transparency component. |

## Properties

| Name              | Summary                                      |
|-------------------|----------------------------------------------|
| [Luminosity][5]   | Computes a luminosity component (grayscale). |
| [Inverted][6]     | The inversion of this color.                 |
| [Red][8]          |                                              |
| [Green][9]        |                                              |
| [Blue][10]        |                                              |
| [Yellow][11]      |                                              |
| [Cyan][12]        |                                              |
| [Magenta][13]     |                                              |
| [White][14]       |                                              |
| [Black][15]       |                                              |
| [Gray][16]        |                                              |
| [DarkGray][17]    |                                              |
| [LightGray][18]   |                                              |
| [Orange][19]      |                                              |
| [Indigo][20]      |                                              |
| [Violet][21]      |                                              |
| [Pink][22]        |                                              |
| [Transparent][23] |                                              |
| [Rainbow][24]     |                                              |
| [Random][25]      |                                              |

## Methods

| Name           | Summary                                                                                                                                 |
|----------------|-----------------------------------------------------------------------------------------------------------------------------------------|
| [Set][7]       | Sets the components of this color.                                                                                                      |
| [Parse][26]    | Parses a hex-string representation of a color. May be formatted as 'RGB', 'RGBA', 'RRGGBB', 'RRGGBBAA' with or without a preceding '#'. |
| [TryParse][27] | Parses a hex-string representation of a color. May be formatted as 'RGB', 'RGBA', 'RRGGBB', 'RRGGBBAA' with or without a preceding '#'. |
| [Lerp][28]     | Interpolate two colors together.                                                                                                        |
| [Multiply][29] | Multiplies two [ColorBytes][30] together. Behaves the same as [Color][31] .                                                             |

[0]: ../../Heirloom.Core.md
[1]: ColorBytes/R.md
[2]: ColorBytes/G.md
[3]: ColorBytes/B.md
[4]: ColorBytes/A.md
[5]: ColorBytes/Luminosity.md
[6]: ColorBytes/Inverted.md
[7]: ColorBytes/Set.md
[8]: ColorBytes/Red.md
[9]: ColorBytes/Green.md
[10]: ColorBytes/Blue.md
[11]: ColorBytes/Yellow.md
[12]: ColorBytes/Cyan.md
[13]: ColorBytes/Magenta.md
[14]: ColorBytes/White.md
[15]: ColorBytes/Black.md
[16]: ColorBytes/Gray.md
[17]: ColorBytes/DarkGray.md
[18]: ColorBytes/LightGray.md
[19]: ColorBytes/Orange.md
[20]: ColorBytes/Indigo.md
[21]: ColorBytes/Violet.md
[22]: ColorBytes/Pink.md
[23]: ColorBytes/Transparent.md
[24]: ColorBytes/Rainbow.md
[25]: ColorBytes/Random.md
[26]: ColorBytes/Parse.md
[27]: ColorBytes/TryParse.md
[28]: ColorBytes/Lerp.md
[29]: ColorBytes/Multiply.md
[30]: ColorBytes.md
[31]: Color.md
