# AudioNode

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  
> **Namespace**: [Heirloom][0]  

Represents a node in the audio mixing tree.

```cs
public abstract class AudioNode
```

--------------------------------------------------------------------------------

**Properties**: [Effects][1], [Volume][2], [Balance][3]

**Methods**: [PopulateBuffer][4]

--------------------------------------------------------------------------------

## Constructors

### AudioNode()

```cs
protected AudioNode()
```

## Properties

| Name         | Summary                                                                                   |
|--------------|-------------------------------------------------------------------------------------------|
| [Effects][1] | Gets the list of [AudioEffect][5] that affect the audio on this node.                     |
| [Volume][2]  | Gets or sets the volume (gain) of the audio.                                              |
| [Balance][3] | Gets or sets the balance (panning) of the audio. (ie, -1.0 for left, and +1.0 for right ) |

## Methods

| Name                | Summary |
|---------------------|---------|
| [PopulateBuffer][4] |         |

[0]: ..\Heirloom.Core.md
[1]: Heirloom.AudioNode.Effects.md
[2]: Heirloom.AudioNode.Volume.md
[3]: Heirloom.AudioNode.Balance.md
[4]: Heirloom.AudioNode.PopulateBuffer.md
[5]: Heirloom.AudioEffect.md
