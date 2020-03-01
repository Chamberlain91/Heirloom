# Heirloom.IO

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## EmbeddedFile (Sealed Class)
<small>**Namespace**: Heirloom.IO</sub></small>  

Represents an embedded file.

| Properties               | Summary                                          |
|--------------------------|--------------------------------------------------|
| [Assembly](#ASSEBEC4)    | Which assembly did this embedded file originate? |
| [Path](#PATH5C11)        | The name of this file in the assembly manifest.  |
| [Identifiers](#IDEN5992) | The known transformed identifiers.               |

| Methods                 | Summary                              |
|-------------------------|--------------------------------------|
| [OpenStream](#OPEN8064) | Opens a stream to the embedded file. |

### Constructors

#### EmbeddedFile(Assembly assembly, string manifestName, IEnumerable\<string> identifiers)

### Properties

#### <a name="ASSEBEC4"></a> Assembly : Assembly

<small>`Read Only`</small>

Which assembly did this embedded file originate?

#### <a name="PATH5C11"></a> Path : string

<small>`Read Only`</small>

The name of this file in the assembly manifest.

#### <a name="IDEN5992"></a> Identifiers : IReadOnlyList\<string>

<small>`Read Only`</small>

The known transformed identifiers.

### Methods

#### <a name="OPENF0FC"></a> OpenStream() : Stream

Opens a stream to the embedded file.

