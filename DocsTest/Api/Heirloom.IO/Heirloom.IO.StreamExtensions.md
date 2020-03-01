# Heirloom.IO

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## StreamExtensions (Static Class)
<small>**Namespace**: Heirloom.IO</sub></small>  

| Methods                   | Summary                                                     |
|---------------------------|-------------------------------------------------------------|
| [ReadAllText](#READ7C4D)  | Reads the entire contents of the stream as a block of text. |
| [ReadLines](#READ2CD9)    | Reads the entire contents of the stream line by line.       |
| [ReadAllBytes](#READ5A9E) | Reads the entire contents of the stream as blob of bytes.   |

### Methods

#### <a name="READ85A2"></a> ReadAllText(Stream stream) : string
<small>`Static`, `ExtensionAttribute`</small>

Reads the entire contents of the stream as a block of text.


#### <a name="READD9B8"></a> ReadLines(Stream stream) : IEnumerable\<string>
<small>`Static`, `IteratorStateMachineAttribute`, `ExtensionAttribute`</small>

Reads the entire contents of the stream line by line.


#### <a name="READ83F9"></a> ReadAllBytes(Stream stream) :  byte
<small>`Static`, `ExtensionAttribute`</small>

Reads the entire contents of the stream as blob of bytes.


