# Heirloom.Sound

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Sound](../Heirloom.Sound/Heirloom.Sound.md)</small>  

## ReverbEffect (Class)
<small>**Namespace**: Heirloom.Sound.Effects</sub></small>  
<small>**Inherits**: [AudioEffect](Heirloom.Sound.AudioEffect.md)</small>  

An audio effect that implements a Schroeder reverb.

Based on Freeverb

| Properties               | Summary                                                                 |
|--------------------------|-------------------------------------------------------------------------|
| [Damping](#DAMCA57E844)  | Gets or sets the damping value. Larger values soften the sound earlier. |
| [RoomSize](#ROO6318FE08) | Gets or sets the room size. Larger values mean longer reverb.           |

| Methods                 | Summary |
|-------------------------|---------|
| [Process](#PRO9CD8B3AF) |         |

### Constructors

#### ReverbEffect(float roomSize = 0.5, float damping = 0.5)

### Properties

#### <a name="DAMCA57E844"></a>Damping : float


Gets or sets the damping value. Larger values soften the sound earlier.

#### <a name="ROO6318FE08"></a>RoomSize : float


Gets or sets the room size. Larger values mean longer reverb.

### Methods

#### <a name="PRO1C94C308"></a>Process(float sample, int channel) : float
<small>`Virtual`</small>


