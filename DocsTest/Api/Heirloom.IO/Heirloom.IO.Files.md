# Heirloom.IO

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## Files (Static Class)
<small>**Namespace**: Heirloom.IO</sub></small>  

A utility to unify access of embedded files and files on disk.

| Methods                          | Summary                                                                          |
|----------------------------------|----------------------------------------------------------------------------------|
| [NormalizePath](#NORC3AEA446)    | Normalizes a path (forward slades, removing doubles, etc)                        |
| [OpenStream](#OPE8064E612)       | Opens a read-only stream to a file, first found by disk, then by embedded files. |
| [Exists](#EXIECE7266E)           | Checks if a file exists, first by disk, then by embedded files.                  |
| [GetEmbeddedFiles](#GETB847098D) | Gets all known embedded files.                                                   |
| [GetEmbeddedInfo](#GETE5A0DD4C)  | Gets information about the embedded file.                                        |
| [ReadText](#REA63B9B063)         | Reads all text in a given file.                                                  |
| [ReadBytes](#REAB3796EAB)        | Reads all bytes in a given file.                                                 |

### Methods

#### <a name="NOR9EC63B5C"></a>NormalizePath(string path) : string
<small>`Static`</small>

Normalizes a path (forward slades, removing doubles, etc)


#### <a name="OPEE2974FDB"></a>OpenStream(string path) : Stream
<small>`Static`</small>

Opens a read-only stream to a file, first found by disk, then by embedded files.


#### <a name="EXI579B48CD"></a>Exists(string path) : bool
<small>`Static`</small>

Checks if a file exists, first by disk, then by embedded files.


#### <a name="GET37B7E4B9"></a>GetEmbeddedFiles() : [EmbeddedFile[]](Heirloom.IO.EmbeddedFile.md)
<small>`Static`</small>

Gets all known embedded files.

#### <a name="GET942341FA"></a>GetEmbeddedInfo(string path) : [EmbeddedFile](Heirloom.IO.EmbeddedFile.md)
<small>`Static`</small>

Gets information about the embedded file.


#### <a name="REA5D446B3D"></a>ReadText(string path) : string
<small>`Static`</small>

Reads all text in a given file.

<small>**path**: <param name="path">A path to a file or embedded identifier.</param></small>  

#### <a name="REA7862D928"></a>ReadBytes(string path) :  byte
<small>`Static`</small>

Reads all bytes in a given file.

<small>**path**: <param name="path">A path to a file or embedded identifier.</param></small>  

