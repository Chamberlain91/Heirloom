# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## AudioGroup (Class)

> **Namespace**: [Heirloom][0]

An [AudioNode][1] to mix and apply effects to a group of other nodes.

```cs
public class AudioGroup : AudioNode
```

### Inherits

[AudioNode][1]

### Properties

[Parent][2]

### Methods

[PopulateBuffer][3]

### Static Properties

[Default][4]

## Properties

#### Instance

| Name        | Type            | Summary                              |
|-------------|-----------------|--------------------------------------|
| [Parent][2] | [AudioGroup][5] | Gets or sets the parent audio group. |

#### Static

| Name         | Type            | Summary                                                           |
|--------------|-----------------|-------------------------------------------------------------------|
| [Default][4] | [AudioGroup][5] | Gets the default audio group (ie, the speakers, headphones, etc). |

## Methods

#### Instance

| Name                           | Return Type | Summary |
|--------------------------------|-------------|---------|
| [PopulateBuffer(Span<fl...][3] | `void`      |         |

[0]: ../../Heirloom.Core.md
[1]: AudioNode.md
[2]: AudioGroup/Parent.md
[3]: AudioGroup/PopulateBuffer.md
[4]: AudioGroup/Default.md
[5]: AudioGroup.md
