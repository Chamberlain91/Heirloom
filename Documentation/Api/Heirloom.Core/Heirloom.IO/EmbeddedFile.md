# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## EmbeddedFile (Class)

> **Namespace**: [Heirloom.IO][0]

Represents an embedded file.

```cs
public sealed class EmbeddedFile
```

### Properties

[Assembly][1], [Identifiers][2], [Path][3]

### Methods

[OpenStream][4]

## Properties

#### Instance

| Name             | Type                     | Summary                                          |
|------------------|--------------------------|--------------------------------------------------|
| [Assembly][1]    | `Assembly`               | Which assembly did this embedded file originate? |
| [Identifiers][2] | `IReadOnlyList\<string>` | The known transformed identifiers.               |
| [Path][3]        | `string`                 | The name of this file in the assembly manifest.  |

## Methods

#### Instance

| Name              | Return Type | Summary                              |
|-------------------|-------------|--------------------------------------|
| [OpenStream()][4] | `Stream`    | Opens a stream to the embedded file. |

[0]: ../../Heirloom.Core.md
[1]: EmbeddedFile/Assembly.md
[2]: EmbeddedFile/Identifiers.md
[3]: EmbeddedFile/Path.md
[4]: EmbeddedFile/OpenStream.md
