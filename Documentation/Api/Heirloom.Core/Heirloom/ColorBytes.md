# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## ColorBytes (Struct)

> **Namespace**: [Heirloom][0]

Color encoded as 4 component bytes.

```cs
public struct ColorBytes : IEquatable<ColorBytes>
```

**See Also:** [Color][1]

### Inherits

IEquatable\<ColorBytes>

### Fields

[A][2], [B][3], [G][4], [R][5]

### Properties

[Inverted][6], [Luminosity][7]

### Methods

[Equals][8], [GetHashCode][9], [Set][10], [ToString][11]

### Static Properties

[Black][12], [Blue][13], [Cyan][14], [DarkGray][15], [Gray][16], [Green][17], [Indigo][18], [LightGray][19], [Magenta][20], [Orange][21], [Pink][22], [Red][23], [Transparent][24], [Violet][25], [White][26], [Yellow][27]

### Static Methods

[Lerp][28], [Multiply][29], [Parse][30], [TryParse][31]

## Fields

#### Instance

| Name   | Type    | Summary                           |
|--------|---------|-----------------------------------|
| [A][2] | ` byte` | The alpha/transparency component. |
| [B][3] | ` byte` | The blue component.               |
| [G][4] | ` byte` | The green component.              |
| [R][5] | ` byte` | The red component.                |

## Properties

#### Instance

| Name            | Type             | Summary                                      |
|-----------------|------------------|----------------------------------------------|
| [Inverted][6]   | [ColorBytes][32] | The inversion of this color.                 |
| [Luminosity][7] | ` byte`          | Computes a luminosity component (grayscale). |

#### Static

| Name              | Type             | Summary                                  |
|-------------------|------------------|------------------------------------------|
| [Black][12]       | [ColorBytes][32] | The color black (#000000).               |
| [Blue][13]        | [ColorBytes][32] | The color blue (#0000FF).                |
| [Cyan][14]        | [ColorBytes][32] | The color cyan (#00FFFF).                |
| [DarkGray][15]    | [ColorBytes][32] | The color dark gray (#333333).           |
| [Gray][16]        | [ColorBytes][32] | The color gray (#999999).                |
| [Green][17]       | [ColorBytes][32] | The color green (#00FF00).               |
| [Indigo][18]      | [ColorBytes][32] | The color indigo (#4B0082).              |
| [LightGray][19]   | [ColorBytes][32] | The color light gray (#CCCCCC).          |
| [Magenta][20]     | [ColorBytes][32] | The color magenta (#FF00FF).             |
| [Orange][21]      | [ColorBytes][32] | The color orange (#FF8811).              |
| [Pink][22]        | [ColorBytes][32] | The color pink (#DD55AA).                |
| [Red][23]         | [ColorBytes][32] | The color red (#FF0000).                 |
| [Transparent][24] | [ColorBytes][32] | The color transparent black (#00000000). |
| [Violet][25]      | [ColorBytes][32] | The color violet (#8A2BE2).              |
| [White][26]       | [ColorBytes][32] | The color white (#FFFFFF).               |
| [Yellow][27]      | [ColorBytes][32] | The color yellow (#FFFF00).              |

## Methods

#### Instance

| Name                            | Return Type | Summary                                                    |
|---------------------------------|-------------|------------------------------------------------------------|
| [Equals(object)][8]             | `bool`      | Compares this Color for equality with another object.      |
| [Equals(ColorBytes)][8]         | `bool`      | Compares this Color for equality with another ColorBytes . |
| [GetHashCode()][9]              | `int`       | Returns the hash code for this instance of ColorBytes .    |
| [Set(byte, byte, byte, ...][10] | `void`      | Sets the components of this color.                         |
| [ToString()][11]                | `string`    | Converts this ColorBytes into string representation.       |

#### Static

| Name                            | Return Type      | Summary                                                                |
|---------------------------------|------------------|------------------------------------------------------------------------|
| [Lerp(ColorBytes, Color...][28] | [ColorBytes][32] | Interpolate two colors together.                                       |
| [Multiply(in ColorBytes...][29] | `void`           | Multiplies two ColorBytes together. Behaves the same as Color .        |
| [Parse(string)][30]             | [ColorBytes][32] | Parses a hex-string representation of a color. May be formatted as ... |
| [TryParse(string, out C...][31] | `bool`           | Parses a hex-string representation of a color. May be formatted as ... |

## Operators

| Name                            | Return Type      | Summary                                                                |
|---------------------------------|------------------|------------------------------------------------------------------------|
| [Addition(ColorBytes, C...][33] | [ColorBytes][32] | Performs a component-wise sum of two instances of ColorBytes .         |
| [Equality(ColorBytes, C...][34] | `bool`           | Compares two instances of ColorBytes for equality.                     |
| [Explicit(ColorBytes)][35]      | `uint`           |                                                                        |
| [Explicit(ColorBytes)][35]      | `int`            |                                                                        |
| [Explicit(uint)][35]            | [ColorBytes][32] |                                                                        |
| [Explicit(int)][35]             | [ColorBytes][32] |                                                                        |
| [Implicit(ColorBytes)][36]      | [Color][1]       |                                                                        |
| [Inequality(ColorBytes,...][37] | `bool`           | Compares two instances of ColorBytes for inequality.                   |
| [Multiply(ColorBytes, C...][38] | [ColorBytes][32] | Performs a component-wise multiplication of two instances of ColorB... |
| [Subtraction(ColorBytes...][39] | [ColorBytes][32] | Performs a component-wise difference of two instances of ColorBytes .  |

[0]: ../../Heirloom.Core.md
[1]: Color.md
[2]: ColorBytes/A.md
[3]: ColorBytes/B.md
[4]: ColorBytes/G.md
[5]: ColorBytes/R.md
[6]: ColorBytes/Inverted.md
[7]: ColorBytes/Luminosity.md
[8]: ColorBytes/Equals.md
[9]: ColorBytes/GetHashCode.md
[10]: ColorBytes/Set.md
[11]: ColorBytes/ToString.md
[12]: ColorBytes/Black.md
[13]: ColorBytes/Blue.md
[14]: ColorBytes/Cyan.md
[15]: ColorBytes/DarkGray.md
[16]: ColorBytes/Gray.md
[17]: ColorBytes/Green.md
[18]: ColorBytes/Indigo.md
[19]: ColorBytes/LightGray.md
[20]: ColorBytes/Magenta.md
[21]: ColorBytes/Orange.md
[22]: ColorBytes/Pink.md
[23]: ColorBytes/Red.md
[24]: ColorBytes/Transparent.md
[25]: ColorBytes/Violet.md
[26]: ColorBytes/White.md
[27]: ColorBytes/Yellow.md
[28]: ColorBytes/Lerp.md
[29]: ColorBytes/Multiply.md
[30]: ColorBytes/Parse.md
[31]: ColorBytes/TryParse.md
[32]: ColorBytes.md
[33]: ColorBytes/op_Addition.md
[34]: ColorBytes/op_Equality.md
[35]: ColorBytes/op_Explicit.md
[36]: ColorBytes/op_Implicit.md
[37]: ColorBytes/op_Inequality.md
[38]: ColorBytes/op_Multiply.md
[39]: ColorBytes/op_Subtraction.md
