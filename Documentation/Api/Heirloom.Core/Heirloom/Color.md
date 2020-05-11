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

[Black][12], [Blue][13], [Cyan][14], [DarkGray][15], [Gray][16], [Green][17], [Indigo][18], [LightGray][19], [Magenta][20], [Orange][21], [Pink][22], [Rainbow][23], [Random][24], [Red][25], [Transparent][26], [Violet][27], [White][28], [Yellow][29]

### Static Methods

[FromHSV][30], [Lerp][31], [Parse][32], [TryParse][33]

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
| [Inverted][7]   | [Color][34] | The inversion of this color.                           |
| [Luminosity][8] | `float`     | Computes a luminosity component (grayscale).           |
| [Saturation][9] | `float`     | Gets or sets the (HSV) saturation of this color.       |

#### Static

| Name              | Type                   | Summary |
|-------------------|------------------------|---------|
| [Black][12]       | [Color][34]            |         |
| [Blue][13]        | [Color][34]            |         |
| [Cyan][14]        | [Color][34]            |         |
| [DarkGray][15]    | [Color][34]            |         |
| [Gray][16]        | [Color][34]            |         |
| [Green][17]       | [Color][34]            |         |
| [Indigo][18]      | [Color][34]            |         |
| [LightGray][19]   | [Color][34]            |         |
| [Magenta][20]     | [Color][34]            |         |
| [Orange][21]      | [Color][34]            |         |
| [Pink][22]        | [Color][34]            |         |
| [Rainbow][23]     | `IReadOnlyList<Color>` |         |
| [Random][24]      | [Color][34]            |         |
| [Red][25]         | [Color][34]            |         |
| [Transparent][26] | [Color][34]            |         |
| [Violet][27]      | [Color][34]            |         |
| [White][28]       | [Color][34]            |         |
| [Yellow][29]      | [Color][34]            |         |

## Methods

#### Instance

| Name                            | Return Type | Summary                                  |
|---------------------------------|-------------|------------------------------------------|
| [Set(float, float, floa...][10] | `void`      | Sets the components of this color.       |
| [ToHSV(out float, out f...][11] | `void`      | Extracts the HSV values from this color. |

#### Static

| Name                            | Return Type | Summary                                                                |
|---------------------------------|-------------|------------------------------------------------------------------------|
| [FromHSV(float, float, ...][30] | [Color][34] | Converts HSV values into a RGBA color.                                 |
| [Lerp(Color, Color, float)][31] | [Color][34] | Interpolate two colors together.                                       |
| [Parse(string)][32]             | [Color][34] | Parses a hex-string representation of a color. May be formatted as ... |
| [TryParse(string, out C...][33] | `bool`      | Parses a hex-string representation of a color. May be formatted as ... |

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
[23]: Color/Rainbow.md
[24]: Color/Random.md
[25]: Color/Red.md
[26]: Color/Transparent.md
[27]: Color/Violet.md
[28]: Color/White.md
[29]: Color/Yellow.md
[30]: Color/FromHSV.md
[31]: Color/Lerp.md
[32]: Color/Parse.md
[33]: Color/TryParse.md
[34]: Color.md
