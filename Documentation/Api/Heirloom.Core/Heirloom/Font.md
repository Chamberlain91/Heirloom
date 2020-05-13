# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Font (Class)

> **Namespace**: [Heirloom][0]

An object to represent a truetype font. Provides functionality to query and measure aspects of the font.

```cs
public class Font : IDisposable
```

### Inherits

IDisposable

### Methods

[Dispose][1], [Finalize][2], [GetGlyph][3], [GetKerning][4], [GetMetrics][5]

### Static Properties

[Default][6]

## Properties

| Name         | Type      | Summary                                                                |
|--------------|-----------|------------------------------------------------------------------------|
| [Default][6] | [Font][7] | A default pixel font for easily rendering text to debug, show metri... |

## Methods

#### Instance

| Name                           | Return Type      | Summary                                                               |
|--------------------------------|------------------|-----------------------------------------------------------------------|
| [Dispose(bool)][1]             | `void`           |                                                                       |
| [Dispose()][1]                 | `void`           | Dispose the current font, freeing unmanaged resources.                |
| [Finalize()][2]                | `void`           | Performs cleanup of unmanaged resources before garbage collection.    |
| [GetGlyph(char)][3]            | [Glyph][8]       | Gets the information about a particular glyph in this font.           |
| [GetGlyph(UnicodeCharac...][3] | [Glyph][8]       | Gets the information about a particular glyph in this font.           |
| [GetKerning(UnicodeChar...][4] | `float`          | Gets the spacing adjustment (ie, kerning) between any two characters. |
| [GetMetrics(float)][5]         | [FontMetrics][9] | Get the vertical metrics of the this font at the specified size.      |

[0]: ../../Heirloom.Core.md
[1]: Font/Dispose.md
[2]: Font/Finalize.md
[3]: Font/GetGlyph.md
[4]: Font/GetKerning.md
[5]: Font/GetMetrics.md
[6]: Font/Default.md
[7]: Font.md
[8]: Glyph.md
[9]: FontMetrics.md
