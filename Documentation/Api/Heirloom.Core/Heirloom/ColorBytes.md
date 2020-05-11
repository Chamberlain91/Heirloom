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

[Equals][7], [GetHashCode][8], [Set][9], [ToString][10]

### Static Properties

[Black][11], [Blue][12], [Cyan][13], [DarkGray][14], [Gray][15], [Green][16], [Indigo][17], [LightGray][18], [Magenta][19], [Orange][20], [Pink][21], [Rainbow][22], [Random][23], [Red][24], [Transparent][25], [Violet][26], [White][27], [Yellow][28]

### Static Methods

[Lerp][29], [Multiply][30], [Parse][31], [TryParse][32]

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
| [Inverted][5]   | [ColorBytes][33] | The inversion of this color.                 |
| [Luminosity][6] | ` byte`          | Computes a luminosity component (grayscale). |

#### Static

| Name              | Type                        | Summary |
|-------------------|-----------------------------|---------|
| [Black][11]       | [ColorBytes][33]            |         |
| [Blue][12]        | [ColorBytes][33]            |         |
| [Cyan][13]        | [ColorBytes][33]            |         |
| [DarkGray][14]    | [ColorBytes][33]            |         |
| [Gray][15]        | [ColorBytes][33]            |         |
| [Green][16]       | [ColorBytes][33]            |         |
| [Indigo][17]      | [ColorBytes][33]            |         |
| [LightGray][18]   | [ColorBytes][33]            |         |
| [Magenta][19]     | [ColorBytes][33]            |         |
| [Orange][20]      | [ColorBytes][33]            |         |
| [Pink][21]        | [ColorBytes][33]            |         |
| [Rainbow][22]     | `IReadOnlyList<ColorBytes>` |         |
| [Random][23]      | [ColorBytes][33]            |         |
| [Red][24]         | [ColorBytes][33]            |         |
| [Transparent][25] | [ColorBytes][33]            |         |
| [Violet][26]      | [ColorBytes][33]            |         |
| [White][27]       | [ColorBytes][33]            |         |
| [Yellow][28]      | [ColorBytes][33]            |         |

## Methods

#### Instance

| Name                           | Return Type | Summary                            |
|--------------------------------|-------------|------------------------------------|
| [Equals(object)][7]            | `bool`      |                                    |
| [Equals(ColorBytes)][7]        | `bool`      |                                    |
| [GetHashCode()][8]             | `int`       |                                    |
| [Set(byte, byte, byte, ...][9] | `void`      | Sets the components of this color. |
| [ToString()][10]               | `string`    |                                    |

#### Static

| Name                            | Return Type      | Summary                                                                |
|---------------------------------|------------------|------------------------------------------------------------------------|
| [Lerp(ColorBytes, Color...][29] | [ColorBytes][33] | Interpolate two colors together.                                       |
| [Multiply(in ColorBytes...][30] | `void`           | Multiplies two ColorBytes together. Behaves the same as Color .        |
| [Parse(string)][31]             | [ColorBytes][33] | Parses a hex-string representation of a color. May be formatted as ... |
| [TryParse(string, out C...][32] | `bool`           | Parses a hex-string representation of a color. May be formatted as ... |

[0]: ../../Heirloom.Core.md
[1]: ColorBytes/A.md
[2]: ColorBytes/B.md
[3]: ColorBytes/G.md
[4]: ColorBytes/R.md
[5]: ColorBytes/Inverted.md
[6]: ColorBytes/Luminosity.md
[7]: ColorBytes/Equals.md
[8]: ColorBytes/GetHashCode.md
[9]: ColorBytes/Set.md
[10]: ColorBytes/ToString.md
[11]: ColorBytes/Black.md
[12]: ColorBytes/Blue.md
[13]: ColorBytes/Cyan.md
[14]: ColorBytes/DarkGray.md
[15]: ColorBytes/Gray.md
[16]: ColorBytes/Green.md
[17]: ColorBytes/Indigo.md
[18]: ColorBytes/LightGray.md
[19]: ColorBytes/Magenta.md
[20]: ColorBytes/Orange.md
[21]: ColorBytes/Pink.md
[22]: ColorBytes/Rainbow.md
[23]: ColorBytes/Random.md
[24]: ColorBytes/Red.md
[25]: ColorBytes/Transparent.md
[26]: ColorBytes/Violet.md
[27]: ColorBytes/White.md
[28]: ColorBytes/Yellow.md
[29]: ColorBytes/Lerp.md
[30]: ColorBytes/Multiply.md
[31]: ColorBytes/Parse.md
[32]: ColorBytes/TryParse.md
[33]: ColorBytes.md
