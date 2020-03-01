# Heirloom.Sound

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Sound](../Heirloom.Sound/Heirloom.Sound.md)</small>  

## ReverbEffect (Class)
<small>**Namespace**: Heirloom.Sound.Effects</sub></small>  
<small>**Inherits**: [AudioEffect](Heirloom.Sound.AudioEffect.md)</small>  

An audio effect that implements a Schroeder reverb.

Based on Freeverb

| Properties            | Summary                                                                 |
|-----------------------|-------------------------------------------------------------------------|
| [Damping](#DAMPCA57)  | Gets or sets the damping value. Larger values soften the sound earlier. |
| [RoomSize](#ROOM6318) | Gets or sets the room size. Larger values mean longer reverb.           |

| Methods              | Summary |
|----------------------|---------|
| [Process](#PROC9CD8) |         |

### Constructors

#### ReverbEffect(float roomSize = 0.5, float damping = 0.5)

### Properties

#### <a name="DAMPCA57"></a> Damping : float


Gets or sets the damping value. Larger values soften the sound earlier.

#### <a name="ROOM6318"></a> RoomSize : float


Gets or sets the room size. Larger values mean longer reverb.

### Methods

#### <a name="PROC1C94"></a> Process(float sample, int channel) : float
<small>`Virtual`</small>


