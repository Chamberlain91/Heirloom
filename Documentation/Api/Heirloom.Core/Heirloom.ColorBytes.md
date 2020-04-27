# ColorBytes

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  
> **Namespace**: [Heirloom][0]  

Color encoded as 4 component bytes.

```cs
public struct ColorBytes : IEquatable<ColorBytes>
```

--------------------------------------------------------------------------------

**Inherits**: IEquatable\<ColorBytes>

**Fields**: [R][1], [G][2], [B][3], [A][4]

**Properties**: [Luminosity][5], [Inverted][6]

**Methods**: [Set][7]

**Static Properties**: [Red][8], [Green][9], [Blue][10], [Yellow][11], [Cyan][12], [Magenta][13], [White][14], [Black][15], [Gray][16], [DarkGray][17], [LightGray][18], [Orange][19], [Indigo][20], [Violet][21], [Pink][22], [Transparent][23], [Rainbow][24], [Random][25]

**Static Methods**: [Parse][26], [TryParse][27], [Lerp][28], [Multiply][29]

--------------------------------------------------------------------------------

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

[0]: ../Heirloom.Core.md
[1]: Heirloom.ColorBytes.R.md
[2]: Heirloom.ColorBytes.G.md
[3]: Heirloom.ColorBytes.B.md
[4]: Heirloom.ColorBytes.A.md
[5]: Heirloom.ColorBytes.Luminosity.md
[6]: Heirloom.ColorBytes.Inverted.md
[7]: Heirloom.ColorBytes.Set.md
[8]: Heirloom.ColorBytes.Red.md
[9]: Heirloom.ColorBytes.Green.md
[10]: Heirloom.ColorBytes.Blue.md
[11]: Heirloom.ColorBytes.Yellow.md
[12]: Heirloom.ColorBytes.Cyan.md
[13]: Heirloom.ColorBytes.Magenta.md
[14]: Heirloom.ColorBytes.White.md
[15]: Heirloom.ColorBytes.Black.md
[16]: Heirloom.ColorBytes.Gray.md
[17]: Heirloom.ColorBytes.DarkGray.md
[18]: Heirloom.ColorBytes.LightGray.md
[19]: Heirloom.ColorBytes.Orange.md
[20]: Heirloom.ColorBytes.Indigo.md
[21]: Heirloom.ColorBytes.Violet.md
[22]: Heirloom.ColorBytes.Pink.md
[23]: Heirloom.ColorBytes.Transparent.md
[24]: Heirloom.ColorBytes.Rainbow.md
[25]: Heirloom.ColorBytes.Random.md
[26]: Heirloom.ColorBytes.Parse.md
[27]: Heirloom.ColorBytes.TryParse.md
[28]: Heirloom.ColorBytes.Lerp.md
[29]: Heirloom.ColorBytes.Multiply.md
[30]: Heirloom.ColorBytes.md
[31]: Heirloom.Color.md
