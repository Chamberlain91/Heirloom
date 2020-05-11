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

[Equals][10], [GetHashCode][11], [Set][12], [ToHSV][13], [ToString][14]

### Static Properties

[Black][15], [Blue][16], [Cyan][17], [DarkGray][18], [Gray][19], [Green][20], [Indigo][21], [LightGray][22], [Magenta][23], [Orange][24], [Pink][25], [Rainbow][26], [Random][27], [Red][28], [Transparent][29], [Violet][30], [White][31], [Yellow][32]

### Static Methods

[FromHSV][33], [Lerp][34], [Parse][35], [TryParse][36]

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
| [Inverted][7]   | [Color][37] | The inversion of this color.                           |
| [Luminosity][8] | `float`     | Computes a luminosity component (grayscale).           |
| [Saturation][9] | `float`     | Gets or sets the (HSV) saturation of this color.       |

#### Static

| Name              | Type                   | Summary |
|-------------------|------------------------|---------|
| [Black][15]       | [Color][37]            |         |
| [Blue][16]        | [Color][37]            |         |
| [Cyan][17]        | [Color][37]            |         |
| [DarkGray][18]    | [Color][37]            |         |
| [Gray][19]        | [Color][37]            |         |
| [Green][20]       | [Color][37]            |         |
| [Indigo][21]      | [Color][37]            |         |
| [LightGray][22]   | [Color][37]            |         |
| [Magenta][23]     | [Color][37]            |         |
| [Orange][24]      | [Color][37]            |         |
| [Pink][25]        | [Color][37]            |         |
| [Rainbow][26]     | `IReadOnlyList<Color>` |         |
| [Random][27]      | [Color][37]            |         |
| [Red][28]         | [Color][37]            |         |
| [Transparent][29] | [Color][37]            |         |
| [Violet][30]      | [Color][37]            |         |
| [White][31]       | [Color][37]            |         |
| [Yellow][32]      | [Color][37]            |         |

## Methods

#### Instance

| Name                            | Return Type | Summary                                  |
|---------------------------------|-------------|------------------------------------------|
| [Equals(object)][10]            | `bool`      |                                          |
| [Equals(Color)][10]             | `bool`      |                                          |
| [GetHashCode()][11]             | `int`       |                                          |
| [Set(float, float, floa...][12] | `void`      | Sets the components of this color.       |
| [ToHSV(out float, out f...][13] | `void`      | Extracts the HSV values from this color. |
| [ToString()][14]                | `string`    |                                          |

#### Static

| Name                            | Return Type | Summary                                                                |
|---------------------------------|-------------|------------------------------------------------------------------------|
| [FromHSV(float, float, ...][33] | [Color][37] | Converts HSV values into a RGBA color.                                 |
| [Lerp(Color, Color, float)][34] | [Color][37] | Interpolate two colors together.                                       |
| [Parse(string)][35]             | [Color][37] | Parses a hex-string representation of a color. May be formatted as ... |
| [TryParse(string, out C...][36] | `bool`      | Parses a hex-string representation of a color. May be formatted as ... |

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
[10]: Color/Equals.md
[11]: Color/GetHashCode.md
[12]: Color/Set.md
[13]: Color/ToHSV.md
[14]: Color/ToString.md
[15]: Color/Black.md
[16]: Color/Blue.md
[17]: Color/Cyan.md
[18]: Color/DarkGray.md
[19]: Color/Gray.md
[20]: Color/Green.md
[21]: Color/Indigo.md
[22]: Color/LightGray.md
[23]: Color/Magenta.md
[24]: Color/Orange.md
[25]: Color/Pink.md
[26]: Color/Rainbow.md
[27]: Color/Random.md
[28]: Color/Red.md
[29]: Color/Transparent.md
[30]: Color/Violet.md
[31]: Color/White.md
[32]: Color/Yellow.md
[33]: Color/FromHSV.md
[34]: Color/Lerp.md
[35]: Color/Parse.md
[36]: Color/TryParse.md
[37]: Color.md
