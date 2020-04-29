# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## ReverbEffect (Class)

> **Namespace**: [Heirloom][0]

An audio effect that implements a Schroeder reverb.

```cs
public class ReverbEffect : AudioEffect
```

Based on Freeverb

### Inherits

[AudioEffect][1]

### Properties

[Damping][2], [RoomSize][3]

### Methods

[Process][4]

## Properties

#### Instance

| Name          | Type    | Summary                                                                |
|---------------|---------|------------------------------------------------------------------------|
| [Damping][2]  | `float` | Gets or sets the damping value. Larger values soften the sound earl... |
| [RoomSize][3] | `float` | Gets or sets the room size. Larger values mean longer reverb.          |

## Methods

#### Instance

| Name                     | Return Type | Summary |
|--------------------------|-------------|---------|
| [Process(float, int)][4] | `float`     |         |

[0]: ../../Heirloom.Core.md
[1]: AudioEffect.md
[2]: ReverbEffect/Damping.md
[3]: ReverbEffect/RoomSize.md
[4]: ReverbEffect/Process.md
