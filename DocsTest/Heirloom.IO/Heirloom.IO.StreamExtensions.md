# Heirloom.IO

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## StreamExtensions (Static Class)
<small>**Namespace**: Heirloom.IO</sub></small>  

| Methods | Summary |
|---------|---------|
| [ReadAllText](#REA85A277A8) | Reads the entire contents of the stream as a block of text. |
| [ReadLines](#READ9B8AF9C) | Reads the entire contents of the stream line by line. |
| [ReadAllBytes](#REA83F9F21F) | Reads the entire contents of the stream as blob of bytes. |

### Methods

#### <a name="REA85A277A8"></a>ReadAllText(Stream stream) : string

<small>`Static`, `ExtensionAttribute`</small>

Reads the entire contents of the stream as a block of text.


#### <a name="READ9B8AF9C"></a>ReadLines(Stream stream) : IEnumerable\<string>

<small>`Static`, `IteratorStateMachineAttribute`, `ExtensionAttribute`</small>

Reads the entire contents of the stream line by line.


#### <a name="REA83F9F21F"></a>ReadAllBytes(Stream stream) :  byte

<small>`Static`, `ExtensionAttribute`</small>

Reads the entire contents of the stream as blob of bytes.


