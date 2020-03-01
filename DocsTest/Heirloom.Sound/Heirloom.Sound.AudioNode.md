# Heirloom.Sound

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Sound](../Heirloom.Sound/Heirloom.Sound.md)</small>  

## AudioNode (Abstract Class)
<small>**Namespace**: Heirloom.Sound</sub></small>  

Represents a node in the audio mixing tree.

| Properties | Summary |
|------------|---------|
| [Effects](#EFFE7FDACB0) | Gets the list of [AudioEffect](Heirloom.Sound.AudioEffect.md) that affect the audio on this node. |
| [Volume](#VOL84D30C54) | Gets or sets the volume (gain) of the audio. |
| [Balance](#BAL2345F2DE) | Gets or sets the balance (panning) of the audio. (ie, -1.0 for left, and +1.0 for right ) |

| Methods | Summary |
|---------|---------|
| [PopulateBuffer](#POP1FA582B7) |  |

### Constructors

#### AudioNode()

### Properties

#### <a name="EFFE7FDACB0"></a>Effects : List\<AudioEffect>

<small>`Read Only`</small>

Gets the list of [AudioEffect](Heirloom.Sound.AudioEffect.md) that affect the audio on this node.

#### <a name="VOL84D30C54"></a>Volume : float


Gets or sets the volume (gain) of the audio.

#### <a name="BAL2345F2DE"></a>Balance : float


Gets or sets the balance (panning) of the audio. (ie, -1.0 for left, and +1.0 for right )

### Methods

#### <a name="POP1FA582B7"></a>PopulateBuffer(Span\<float> buffer) : void

<small>`Abstract`, `Protected`</small>


