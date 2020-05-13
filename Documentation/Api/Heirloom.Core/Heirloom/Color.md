# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Color (Struct)

> **Namespace**: [Heirloom][0]

Color encoded as 4 component floats.

```cs
public struct Color : IEquatable<Color>
```

**See Also:** [ColorBytes][1]

### Inherits

IEquatable\<Color>

### Fields

[A][2], [B][3], [G][4], [R][5]

### Properties

[Brightness][6], [Hue][7], [Inverted][8], [Luminosity][9], [Saturation][10]

### Methods

[Equals][11], [GetHashCode][12], [Set][13], [ToHSV][14], [ToString][15]

### Static Properties

[Black][16], [Blue][17], [Cyan][18], [DarkGray][19], [Gray][20], [Green][21], [Indigo][22], [LightGray][23], [Magenta][24], [Orange][25], [Pink][26], [Red][27], [Transparent][28], [Violet][29], [White][30], [Yellow][31]

### Static Methods

[FromHSV][32], [Lerp][33], [Parse][34], [TryParse][35]

## Fields

#### Instance

| Name   | Type    | Summary                           |
|--------|---------|-----------------------------------|
| [A][2] | `float` | The alpha/transparency component. |
| [B][3] | `float` | The blue component.               |
| [G][4] | `float` | The green component.              |
| [R][5] | `float` | The red component.                |

## Properties

#### Instance

| Name             | Type        | Summary                                                |
|------------------|-------------|--------------------------------------------------------|
| [Brightness][6]  | `float`     | Gets or sets the (HSV) brightness value of this color. |
| [Hue][7]         | `float`     | Gets or sets the (HSV) hue of this color.              |
| [Inverted][8]    | [Color][36] | The inversion of this color.                           |
| [Luminosity][9]  | `float`     | Computes a luminosity component (grayscale).           |
| [Saturation][10] | `float`     | Gets or sets the (HSV) saturation of this color.       |

#### Static

| Name              | Type        | Summary                                  |
|-------------------|-------------|------------------------------------------|
| [Black][16]       | [Color][36] | The color black (#000000).               |
| [Blue][17]        | [Color][36] | The color blue (#0000FF).                |
| [Cyan][18]        | [Color][36] | The color cyan (#00FFFF).                |
| [DarkGray][19]    | [Color][36] | The color dark gray (#333333).           |
| [Gray][20]        | [Color][36] | The color gray (#999999).                |
| [Green][21]       | [Color][36] | The color green (#00FF00).               |
| [Indigo][22]      | [Color][36] | The color indigo (#4B0082).              |
| [LightGray][23]   | [Color][36] | The color light gray (#CCCCCC).          |
| [Magenta][24]     | [Color][36] | The color magenta (#FF00FF).             |
| [Orange][25]      | [Color][36] | The color orange (#FF8811).              |
| [Pink][26]        | [Color][36] | The color pink (#DD55AA).                |
| [Red][27]         | [Color][36] | The color red (#FF0000).                 |
| [Transparent][28] | [Color][36] | The color transparent black (#00000000). |
| [Violet][29]      | [Color][36] | The color violet (#8A2BE2).              |
| [White][30]       | [Color][36] | The color white (#FFFFFF).               |
| [Yellow][31]      | [Color][36] | The color yellow (#FFFF00).              |

## Methods

#### Instance

| Name                            | Return Type | Summary                                               |
|---------------------------------|-------------|-------------------------------------------------------|
| [Equals(object)][11]            | `bool`      | Compares this Color for equality with another object. |
| [Equals(Color)][11]             | `bool`      | Compares this Color for equality with another Color . |
| [GetHashCode()][12]             | `int`       | Returns the hash code for this instance of Color .    |
| [Set(float, float, floa...][13] | `void`      | Sets the components of this color.                    |
| [ToHSV(out float, out f...][14] | `void`      | Extracts the HSV values from this color.              |
| [ToString()][15]                | `string`    | Converts this Color into string representation.       |

#### Static

| Name                            | Return Type | Summary                                                                |
|---------------------------------|-------------|------------------------------------------------------------------------|
| [FromHSV(float, float, ...][32] | [Color][36] | Converts HSV values into a RGBA color.                                 |
| [Lerp(Color, Color, float)][33] | [Color][36] | Interpolate two colors together.                                       |
| [Parse(string)][34]             | [Color][36] | Parses a hex-string representation of a color. May be formatted as ... |
| [TryParse(string, out C...][35] | `bool`      | Parses a hex-string representation of a color. May be formatted as ... |

## Operators

| Name                            | Return Type     | Summary                                                              |
|---------------------------------|-----------------|----------------------------------------------------------------------|
| [Addition(Color, Color)][37]    | [Color][36]     | Performs a component-wise sum of two instances of Color .            |
| [Division(Color, Color)][38]    | [Color][36]     | Performs a component-wise division of two instances of Color .       |
| [Division(Color, float)][38]    | [Color][36]     | Performs a component-wise scale of a Color .                         |
| [Equality(Color, Color)][39]    | `bool`          | Compares two instances of Color for equality.                        |
| [Explicit(uint)][40]            | [Color][36]     |                                                                      |
| [Explicit(int)][40]             | [Color][36]     |                                                                      |
| [Implicit(Color)][41]           | [ColorBytes][1] |                                                                      |
| [Inequality(Color, Color)][42]  | `bool`          | Compares two instances of Color for inequality.                      |
| [Multiply(Color, Color)][43]    | [Color][36]     | Performs a component-wise multiplication of two instances of Color . |
| [Multiply(float, Color)][43]    | [Color][36]     | Performs a component-wise scale of a Color .                         |
| [Multiply(Color, float)][43]    | [Color][36]     | Performs a component-wise scale of a Color .                         |
| [Subtraction(Color, Color)][44] | [Color][36]     | Performs a component-wise difference of two instances of Color .     |

[0]: ../../Heirloom.Core.md
[1]: ColorBytes.md
[2]: Color/A.md
[3]: Color/B.md
[4]: Color/G.md
[5]: Color/R.md
[6]: Color/Brightness.md
[7]: Color/Hue.md
[8]: Color/Inverted.md
[9]: Color/Luminosity.md
[10]: Color/Saturation.md
[11]: Color/Equals.md
[12]: Color/GetHashCode.md
[13]: Color/Set.md
[14]: Color/ToHSV.md
[15]: Color/ToString.md
[16]: Color/Black.md
[17]: Color/Blue.md
[18]: Color/Cyan.md
[19]: Color/DarkGray.md
[20]: Color/Gray.md
[21]: Color/Green.md
[22]: Color/Indigo.md
[23]: Color/LightGray.md
[24]: Color/Magenta.md
[25]: Color/Orange.md
[26]: Color/Pink.md
[27]: Color/Red.md
[28]: Color/Transparent.md
[29]: Color/Violet.md
[30]: Color/White.md
[31]: Color/Yellow.md
[32]: Color/FromHSV.md
[33]: Color/Lerp.md
[34]: Color/Parse.md
[35]: Color/TryParse.md
[36]: Color.md
[37]: Color/op_Addition.md
[38]: Color/op_Division.md
[39]: Color/op_Equality.md
[40]: Color/op_Explicit.md
[41]: Color/op_Implicit.md
[42]: Color/op_Inequality.md
[43]: Color/op_Multiply.md
[44]: Color/op_Subtraction.md
