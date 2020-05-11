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

[Black][8], [Blue][9], [Cyan][10], [DarkGray][11], [Gray][12], [Green][13], [Indigo][14], [LightGray][15], [Magenta][16], [Orange][17], [Pink][18], [Rainbow][19], [Random][20], [Red][21], [Transparent][22], [Violet][23], [White][24], [Yellow][25]

### Static Methods

[Lerp][26], [Multiply][27], [Parse][28], [TryParse][29]

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
| [Inverted][5]   | [ColorBytes][30] | The inversion of this color.                 |
| [Luminosity][6] | ` byte`          | Computes a luminosity component (grayscale). |

#### Static

| Name              | Type                        | Summary |
|-------------------|-----------------------------|---------|
| [Black][8]        | [ColorBytes][30]            |         |
| [Blue][9]         | [ColorBytes][30]            |         |
| [Cyan][10]        | [ColorBytes][30]            |         |
| [DarkGray][11]    | [ColorBytes][30]            |         |
| [Gray][12]        | [ColorBytes][30]            |         |
| [Green][13]       | [ColorBytes][30]            |         |
| [Indigo][14]      | [ColorBytes][30]            |         |
| [LightGray][15]   | [ColorBytes][30]            |         |
| [Magenta][16]     | [ColorBytes][30]            |         |
| [Orange][17]      | [ColorBytes][30]            |         |
| [Pink][18]        | [ColorBytes][30]            |         |
| [Rainbow][19]     | `IReadOnlyList<ColorBytes>` |         |
| [Random][20]      | [ColorBytes][30]            |         |
| [Red][21]         | [ColorBytes][30]            |         |
| [Transparent][22] | [ColorBytes][30]            |         |
| [Violet][23]      | [ColorBytes][30]            |         |
| [White][24]       | [ColorBytes][30]            |         |
| [Yellow][25]      | [ColorBytes][30]            |         |

## Methods

#### Instance

| Name                           | Return Type | Summary                            |
|--------------------------------|-------------|------------------------------------|
| [Set(byte, byte, byte, ...][7] | `void`      | Sets the components of this color. |

#### Static

| Name                            | Return Type      | Summary                                                                |
|---------------------------------|------------------|------------------------------------------------------------------------|
| [Lerp(ColorBytes, Color...][26] | [ColorBytes][30] | Interpolate two colors together.                                       |
| [Multiply(in ColorBytes...][27] | `void`           | Multiplies two ColorBytes together. Behaves the same as Color .        |
| [Parse(string)][28]             | [ColorBytes][30] | Parses a hex-string representation of a color. May be formatted as ... |
| [TryParse(string, out C...][29] | `bool`           | Parses a hex-string representation of a color. May be formatted as ... |

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
[19]: ColorBytes/Rainbow.md
[20]: ColorBytes/Random.md
[21]: ColorBytes/Red.md
[22]: ColorBytes/Transparent.md
[23]: ColorBytes/Violet.md
[24]: ColorBytes/White.md
[25]: ColorBytes/Yellow.md
[26]: ColorBytes/Lerp.md
[27]: ColorBytes/Multiply.md
[28]: ColorBytes/Parse.md
[29]: ColorBytes/TryParse.md
[30]: ColorBytes.md
