# ReverbEffect

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  
> **Namespace**: [Heirloom][0]  

An audio effect that implements a Schroeder reverb.

```cs
public class ReverbEffect : AudioEffect
```


Based on Freeverb

--------------------------------------------------------------------------------

**Inherits**: [AudioEffect][1]

**Properties**: [Damping][2], [RoomSize][3]

**Methods**: [Process][4]

--------------------------------------------------------------------------------

## Properties

| Name          | Summary                                                                 |
|---------------|-------------------------------------------------------------------------|
| [Damping][2]  | Gets or sets the damping value. Larger values soften the sound earlier. |
| [RoomSize][3] | Gets or sets the room size. Larger values mean longer reverb.           |

## Methods

| Name         | Summary |
|--------------|---------|
| [Process][4] |         |

[0]: ../Heirloom.Core.md
[1]: Heirloom.AudioEffect.md
[2]: Heirloom.ReverbEffect.Damping.md
[3]: Heirloom.ReverbEffect.RoomSize.md
[4]: Heirloom.ReverbEffect.Process.md
