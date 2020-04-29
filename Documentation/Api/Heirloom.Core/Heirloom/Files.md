# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  

## Files Class

> **Namespace**: [Heirloom][0]  

A utility to unify access of embedded files and files on disk.

```cs
public static class Files
```

#### Static Methods

[NormalizePath][1], [OpenStream][2], [Exists][3], [GetEmbeddedFiles][4], [GetEmbeddedInfo][5], [ReadText][6], [ReadBytes][7]

## Methods

| Name                  | Summary                                                                          |
|-----------------------|----------------------------------------------------------------------------------|
| [NormalizePath][1]    | Normalizes a path (forward slades, removing doubles, etc)                        |
| [OpenStream][2]       | Opens a read-only stream to a file, first found by disk, then by embedded files. |
| [Exists][3]           | Checks if a file exists, first by disk, then by embedded files.                  |
| [GetEmbeddedFiles][4] | Gets all known embedded files.                                                   |
| [GetEmbeddedInfo][5]  | Gets information about the embedded file.                                        |
| [ReadText][6]         | Reads all text in a given file.                                                  |
| [ReadBytes][7]        | Reads all bytes in a given file.                                                 |

[0]: ../../Heirloom.Core.md
[1]: Files/NormalizePath.md
[2]: Files/OpenStream.md
[3]: Files/Exists.md
[4]: Files/GetEmbeddedFiles.md
[5]: Files/GetEmbeddedInfo.md
[6]: Files/ReadText.md
[7]: Files/ReadBytes.md
