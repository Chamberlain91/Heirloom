# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## AudioNode (Class)

> **Namespace**: [Heirloom.Sound][0]

Represents a node in the audio mixing tree.

```cs
public abstract class AudioNode
```

### Properties

[Balance][1], [Effects][2], [Volume][3]

### Methods

[PopulateBuffer][4]

## Properties

#### Instance

| Name         | Type                | Summary                                                                |
|--------------|---------------------|------------------------------------------------------------------------|
| [Balance][1] | `float`             | Gets or sets the balance (panning) of the audio. (ie, -1.0 for left... |
| [Effects][2] | `List<AudioEffect>` | Gets the list of AudioEffect that affect the audio on this node.       |
| [Volume][3]  | `float`             | Gets or sets the volume (gain) of the audio.                           |

## Methods

#### Instance

| Name                           | Return Type | Summary |
|--------------------------------|-------------|---------|
| [PopulateBuffer(Span<fl...][4] | `void`      |         |

[0]: ../../Heirloom.Core.md
[1]: AudioNode/Balance.md
[2]: AudioNode/Effects.md
[3]: AudioNode/Volume.md
[4]: AudioNode/PopulateBuffer.md
