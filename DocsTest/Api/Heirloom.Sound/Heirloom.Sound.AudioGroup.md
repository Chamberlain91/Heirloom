# Heirloom.Sound

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Sound](../Heirloom.Sound/Heirloom.Sound.md)</small>  

## AudioGroup (Class)
<small>**Namespace**: Heirloom.Sound</sub></small>  
<small>**Inherits**: [AudioNode](Heirloom.Sound.AudioNode.md)</small>  

An [AudioNode](Heirloom.Sound.AudioNode.md) to mix and apply effects to a group of other nodes.

| Properties            | Summary                                                           |
|-----------------------|-------------------------------------------------------------------|
| [Parent](#PARE2197)   | Gets or sets the parent audio group.                              |
| [Children](#CHILA28E) |                                                                   |
| [Default](#DEFACF6E)  | Gets the default audio group (ie, the speakers, headphones, etc). |

| Methods                     | Summary |
|-----------------------------|---------|
| [PopulateBuffer](#POPU7E07) |         |

### Constructors

#### AudioGroup()

Construct a new audio node that is connected to default audio group (ie, `Heirloom.Sound.AudioGroup.Default`).

#### AudioGroup([AudioGroup](Heirloom.Sound.AudioGroup.md) parentGroup)

Construct a new audio group that is connected to the specified parent group.

#### AudioGroup([AudioGroup](Heirloom.Sound.AudioGroup.md) parentGroup, bool allowOrphan)

### Properties

#### <a name="PARE2197"></a> Parent : [AudioGroup](Heirloom.Sound.AudioGroup.md)


Gets or sets the parent audio group.

#### <a name="CHILA28E"></a> Children : IEnumerable\<AudioNode>


#### <a name="DEFACF6E"></a> Default : [AudioGroup](Heirloom.Sound.AudioGroup.md)

<small>`Static`, `Read Only`</small>

Gets the default audio group (ie, the speakers, headphones, etc).

### Methods

#### <a name="POPU1F4E"></a> PopulateBuffer(Span\<float> output) : void
<small>`Virtual`, `Protected`</small>


