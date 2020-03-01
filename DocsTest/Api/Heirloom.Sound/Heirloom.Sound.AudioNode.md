# Heirloom.Sound

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Sound](../Heirloom.Sound/Heirloom.Sound.md)</small>  

## AudioNode (Abstract Class)
<small>**Namespace**: Heirloom.Sound</sub></small>  

Represents a node in the audio mixing tree.

| Properties           | Summary                                                                                           |
|----------------------|---------------------------------------------------------------------------------------------------|
| [Effects](#EFFEE7FD) | Gets the list of [AudioEffect](Heirloom.Sound.AudioEffect.md) that affect the audio on this node. |
| [Volume](#VOLU84D3)  | Gets or sets the volume (gain) of the audio.                                                      |
| [Balance](#BALA2345) | Gets or sets the balance (panning) of the audio. (ie, -1.0 for left, and +1.0 for right )         |

| Methods                     | Summary |
|-----------------------------|---------|
| [PopulateBuffer](#POPU7E07) |         |

### Constructors

#### AudioNode()

### Properties

#### <a name="EFFEE7FD"></a> Effects : List\<AudioEffect>

<small>`Read Only`</small>

Gets the list of [AudioEffect](Heirloom.Sound.AudioEffect.md) that affect the audio on this node.

#### <a name="VOLU84D3"></a> Volume : float


Gets or sets the volume (gain) of the audio.

#### <a name="BALA2345"></a> Balance : float


Gets or sets the balance (panning) of the audio. (ie, -1.0 for left, and +1.0 for right )

### Methods

#### <a name="POPU1FA5"></a> PopulateBuffer(Span\<float> buffer) : void
<small>`Abstract`, `Protected`</small>


