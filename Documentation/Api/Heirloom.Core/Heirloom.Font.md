# Font

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  
> **Namespace**: [Heirloom][0]  

```cs
public class Font : IDisposable
```

--------------------------------------------------------------------------------

**Inherits**: IDisposable

**Methods**: [GetMetrics][1], [GetKerning][2], [GetGlyph][3], [Dispose][4]

**Static Properties**: [Default][5]

--------------------------------------------------------------------------------

## Constructors

### Font(string)

Loads a font specified by path resolved by [Files.OpenStream][6] .

```cs
public Font(string path)
```

### Font(Stream)

Loads a font from a stream.

```cs
public Font(Stream stream)
```

### Font(byte[])

```cs
public Font(byte[] file)
```

## Properties

| Name         | Summary                                                                                               |
|--------------|-------------------------------------------------------------------------------------------------------|
| [Default][5] | A default pixel font for easily rendering text to debug, show metrics, etc. Recommended size is 16px. |

## Methods

| Name            | Summary                                                               |
|-----------------|-----------------------------------------------------------------------|
| [GetMetrics][1] | Get the vertical metrics of the this font at the specified size.      |
| [GetKerning][2] | Gets the spacing adjustment (ie, kerning) between any two characters. |
| [GetGlyph][3]   | Gets the information about a particular glyph in this font.           |
| [GetGlyph][3]   | Gets the information about a particular glyph in this font.           |
| [Dispose][4]    |                                                                       |
| [Dispose][4]    |                                                                       |

[0]: ..\Heirloom.Core.md
[1]: Heirloom.Font.GetMetrics.md
[2]: Heirloom.Font.GetKerning.md
[3]: Heirloom.Font.GetGlyph.md
[4]: Heirloom.Font.Dispose.md
[5]: Heirloom.Font.Default.md
[6]: Heirloom.Files.OpenStream.md
