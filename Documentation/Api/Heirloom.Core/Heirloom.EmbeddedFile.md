# EmbeddedFile

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  
> **Namespace**: [Heirloom][0]  

Represents an embedded file.

```cs
public sealed class EmbeddedFile
```

--------------------------------------------------------------------------------

**Properties**: [Assembly][1], [Path][2], [Identifiers][3]

**Methods**: [OpenStream][4]

--------------------------------------------------------------------------------

## Properties

| Name             | Summary                                          |
|------------------|--------------------------------------------------|
| [Assembly][1]    | Which assembly did this embedded file originate? |
| [Path][2]        | The name of this file in the assembly manifest.  |
| [Identifiers][3] | The known transformed identifiers.               |

## Methods

| Name            | Summary                              |
|-----------------|--------------------------------------|
| [OpenStream][4] | Opens a stream to the embedded file. |

[0]: ../Heirloom.Core.md
[1]: Heirloom.EmbeddedFile.Assembly.md
[2]: Heirloom.EmbeddedFile.Path.md
[3]: Heirloom.EmbeddedFile.Identifiers.md
[4]: Heirloom.EmbeddedFile.OpenStream.md
