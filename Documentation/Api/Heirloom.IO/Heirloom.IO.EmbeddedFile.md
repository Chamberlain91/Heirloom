# Heirloom.IO

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## EmbeddedFile (Sealed Class)
<small>**Namespace**: Heirloom.IO</small>  

Represents an embedded file.

| Properties                  | Summary                                          |
|-----------------------------|--------------------------------------------------|
| [Assembly](#ASSBEC4387E)    | Which assembly did this embedded file originate? |
| [Path](#PAT5C11D03D)        | The name of this file in the assembly manifest.  |
| [Identifiers](#IDE599211FE) | The known transformed identifiers.               |

| Methods                    | Summary                              |
|----------------------------|--------------------------------------|
| [OpenStream](#OPEF0FC28BF) | Opens a stream to the embedded file. |

### Constructors

#### EmbeddedFile(Assembly assembly, string manifestName, IEnumerable\<string> identifiers)

### Properties

#### <a name="ASSBEC4387E"></a>Assembly : Assembly

<small>`Read Only`</small>

Which assembly did this embedded file originate?

#### <a name="PAT5C11D03D"></a>Path : string

<small>`Read Only`</small>

The name of this file in the assembly manifest.

#### <a name="IDE599211FE"></a>Identifiers : IReadOnlyList\<string>

<small>`Read Only`</small>

The known transformed identifiers.

### Methods

#### <a name="OPEF0FC28BF"></a>OpenStream() : Stream

Opens a stream to the embedded file.

