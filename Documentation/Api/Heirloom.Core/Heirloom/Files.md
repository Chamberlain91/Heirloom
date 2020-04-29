# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Files (Class)

> **Namespace**: [Heirloom][0]

A utility to unify access of embedded files and files on disk.

```cs
public static class Files
```

### Static Methods

[Exists][1], [GetEmbeddedFiles][2], [GetEmbeddedInfo][3], [NormalizePath][4], [OpenStream][5], [ReadBytes][6], [ReadText][7]

## Methods

| Name                         | Return Type         | Summary                                                                |
|------------------------------|---------------------|------------------------------------------------------------------------|
| [Exists(string)][1]          | `bool`              | Checks if a file exists, first by disk, then by embedded files.        |
| [GetEmbeddedFiles()][2]      | [EmbeddedFile[]][8] | Gets all known embedded files.                                         |
| [GetEmbeddedInfo(string)][3] | [EmbeddedFile][8]   | Gets information about the embedded file.                              |
| [NormalizePath(string)][4]   | `string`            | Normalizes a path (forward slades, removing doubles, etc)              |
| [OpenStream(string)][5]      | `Stream`            | Opens a read-only stream to a file, first found by disk, then by em... |
| [ReadBytes(string)][6]       | ` byte[]`           | Reads all bytes in a given file.                                       |
| [ReadText(string)][7]        | `string`            | Reads all text in a given file.                                        |

[0]: ../../Heirloom.Core.md
[1]: Files/Exists.md
[2]: Files/GetEmbeddedFiles.md
[3]: Files/GetEmbeddedInfo.md
[4]: Files/NormalizePath.md
[5]: Files/OpenStream.md
[6]: Files/ReadBytes.md
[7]: Files/ReadText.md
[8]: EmbeddedFile.md
