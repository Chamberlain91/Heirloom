# Heirloom.IO

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## Files (Static Class)
<small>**Namespace**: Heirloom.IO</sub></small>  

A utility to unify access of embedded files and files on disk.

| Methods                       | Summary                                                                          |
|-------------------------------|----------------------------------------------------------------------------------|
| [NormalizePath](#NORMC3AE)    | Normalizes a path (forward slades, removing doubles, etc)                        |
| [OpenStream](#OPEN8064)       | Opens a read-only stream to a file, first found by disk, then by embedded files. |
| [Exists](#EXISECE7)           | Checks if a file exists, first by disk, then by embedded files.                  |
| [GetEmbeddedFiles](#GETEB847) | Gets all known embedded files.                                                   |
| [GetEmbeddedInfo](#GETEE5A0)  | Gets information about the embedded file.                                        |
| [ReadText](#READ63B9)         | Reads all text in a given file.                                                  |
| [ReadBytes](#READB379)        | Reads all bytes in a given file.                                                 |

### Methods

#### <a name="NORM9EC6"></a> NormalizePath(string path) : string
<small>`Static`</small>

Normalizes a path (forward slades, removing doubles, etc)


#### <a name="OPENE297"></a> OpenStream(string path) : Stream
<small>`Static`</small>

Opens a read-only stream to a file, first found by disk, then by embedded files.


#### <a name="EXIS579B"></a> Exists(string path) : bool
<small>`Static`</small>

Checks if a file exists, first by disk, then by embedded files.


#### <a name="GETE37B7"></a> GetEmbeddedFiles() : [EmbeddedFile[]](Heirloom.IO.EmbeddedFile.md)
<small>`Static`</small>

Gets all known embedded files.

#### <a name="GETE9423"></a> GetEmbeddedInfo(string path) : [EmbeddedFile](Heirloom.IO.EmbeddedFile.md)
<small>`Static`</small>

Gets information about the embedded file.


#### <a name="READ5D44"></a> ReadText(string path) : string
<small>`Static`</small>

Reads all text in a given file.

<small>**path**: <param name="path">A path to a file or embedded identifier.</param></small>  

#### <a name="READ7862"></a> ReadBytes(string path) :  byte
<small>`Static`</small>

Reads all bytes in a given file.

<small>**path**: <param name="path">A path to a file or embedded identifier.</param></small>  

