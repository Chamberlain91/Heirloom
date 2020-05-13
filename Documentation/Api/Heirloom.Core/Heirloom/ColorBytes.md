# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## ColorBytes (Struct)

> **Namespace**: [Heirloom][0]

Color encoded as 4 component bytes.

```cs
public struct ColorBytes : IEquatable<ColorBytes>
```

### Inherits

IEquatable\<ColorBytes>

### Fields

[A][1], [B][2], [G][3], [R][4]

### Properties

[Inverted][5], [Luminosity][6]

### Methods

[Set][7]

### Static Properties

[Black][8], [Blue][9], [Cyan][10], [DarkGray][11], [Gray][12], [Green][13], [Indigo][14], [LightGray][15], [Magenta][16], [Orange][17], [Pink][18], [Red][19], [Transparent][20], [Violet][21], [White][22], [Yellow][23]

### Static Methods

[Lerp][24], [Multiply][25], [Parse][26], [TryParse][27]

## Fields

#### Instance

| Name   | Type    | Summary                           |
|--------|---------|-----------------------------------|
| [A][1] | ` byte` | The alpha/transparency component. |
| [B][2] | ` byte` | The blue component.               |
| [G][3] | ` byte` | The green component.              |
| [R][4] | ` byte` | The red component.                |

## Properties

#### Instance

| Name            | Type             | Summary                                      |
|-----------------|------------------|----------------------------------------------|
| [Inverted][5]   | [ColorBytes][28] | The inversion of this color.                 |
| [Luminosity][6] | ` byte`          | Computes a luminosity component (grayscale). |

#### Static

| Name              | Type             | Summary                                  |
|-------------------|------------------|------------------------------------------|
| [Black][8]        | [ColorBytes][28] | The color black (#000000).               |
| [Blue][9]         | [ColorBytes][28] | The color blue (#0000FF).                |
| [Cyan][10]        | [ColorBytes][28] | The color cyan (#00FFFF).                |
| [DarkGray][11]    | [ColorBytes][28] | The color dark gray (#333333).           |
| [Gray][12]        | [ColorBytes][28] | The color gray (#999999).                |
| [Green][13]       | [ColorBytes][28] | The color green (#00FF00).               |
| [Indigo][14]      | [ColorBytes][28] | The color indigo (#4B0082).              |
| [LightGray][15]   | [ColorBytes][28] | The color light gray (#CCCCCC).          |
| [Magenta][16]     | [ColorBytes][28] | The color magenta (#FF00FF).             |
| [Orange][17]      | [ColorBytes][28] | The color orange (#FF8811).              |
| [Pink][18]        | [ColorBytes][28] | The color pink (#DD55AA).                |
| [Red][19]         | [ColorBytes][28] | The color red (#FF0000).                 |
| [Transparent][20] | [ColorBytes][28] | The color transparent black (#00000000). |
| [Violet][21]      | [ColorBytes][28] | The color violet (#8A2BE2).              |
| [White][22]       | [ColorBytes][28] | The color white (#FFFFFF).               |
| [Yellow][23]      | [ColorBytes][28] | The color yellow (#FFFF00).              |

## Methods

#### Instance

| Name                           | Return Type | Summary                            |
|--------------------------------|-------------|------------------------------------|
| [Set(byte, byte, byte, ...][7] | `void`      | Sets the components of this color. |

#### Static

| Name                            | Return Type      | Summary                                                                |
|---------------------------------|------------------|------------------------------------------------------------------------|
| [Lerp(ColorBytes, Color...][24] | [ColorBytes][28] | Interpolate two colors together.                                       |
| [Multiply(in ColorBytes...][25] | `void`           | Multiplies two ColorBytes together. Behaves the same as Color .        |
| [Parse(string)][26]             | [ColorBytes][28] | Parses a hex-string representation of a color. May be formatted as ... |
| [TryParse(string, out C...][27] | `bool`           | Parses a hex-string representation of a color. May be formatted as ... |

[0]: ../../Heirloom.Core.md
[1]: ColorBytes/A.md
[2]: ColorBytes/B.md
[3]: ColorBytes/G.md
[4]: ColorBytes/R.md
[5]: ColorBytes/Inverted.md
[6]: ColorBytes/Luminosity.md
[7]: ColorBytes/Set.md
[8]: ColorBytes/Black.md
[9]: ColorBytes/Blue.md
[10]: ColorBytes/Cyan.md
[11]: ColorBytes/DarkGray.md
[12]: ColorBytes/Gray.md
[13]: ColorBytes/Green.md
[14]: ColorBytes/Indigo.md
[15]: ColorBytes/LightGray.md
[16]: ColorBytes/Magenta.md
[17]: ColorBytes/Orange.md
[18]: ColorBytes/Pink.md
[19]: ColorBytes/Red.md
[20]: ColorBytes/Transparent.md
[21]: ColorBytes/Violet.md
[22]: ColorBytes/White.md
[23]: ColorBytes/Yellow.md
[24]: ColorBytes/Lerp.md
[25]: ColorBytes/Multiply.md
[26]: ColorBytes/Parse.md
[27]: ColorBytes/TryParse.md
[28]: ColorBytes.md
