# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Font (Class)

> **Namespace**: [Heirloom][0]

```cs
public class Font : IDisposable
```

### Inherits

IDisposable

### Methods

[Dispose][1], [GetGlyph][2], [GetKerning][3], [GetMetrics][4]

### Static Properties

[Default][5]

## Properties

| Name         | Type      | Summary                                                                |
|--------------|-----------|------------------------------------------------------------------------|
| [Default][5] | [Font][6] | A default pixel font for easily rendering text to debug, show metri... |

## Methods

#### Instance

| Name                           | Return Type      | Summary                                                               |
|--------------------------------|------------------|-----------------------------------------------------------------------|
| [Dispose(bool)][1]             | `void`           |                                                                       |
| [Dispose()][1]                 | `void`           |                                                                       |
| [GetGlyph(char)][2]            | [Glyph][7]       | Gets the information about a particular glyph in this font.           |
| [GetGlyph(UnicodeCharac...][2] | [Glyph][7]       | Gets the information about a particular glyph in this font.           |
| [GetKerning(UnicodeChar...][3] | `float`          | Gets the spacing adjustment (ie, kerning) between any two characters. |
| [GetMetrics(float)][4]         | [FontMetrics][8] | Get the vertical metrics of the this font at the specified size.      |

[0]: ../../Heirloom.Core.md
[1]: Font/Dispose.md
[2]: Font/GetGlyph.md
[3]: Font/GetKerning.md
[4]: Font/GetMetrics.md
[5]: Font/Default.md
[6]: Font.md
[7]: Glyph.md
[8]: FontMetrics.md
