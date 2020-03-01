# Heirloom.Sound

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Sound](../heirloom.sound/heirloom.sound.md)</small>  

## AudioGroup (Class)
<small>**Namespace**: Heirloom.Sound</sub></small>  
<small>**Inherits**: [AudioNode](heirloom.sound.audionode.md)</small>  

An [AudioNode](heirloom.sound.audionode.md) to mix and apply effects to a group of other nodes.

| Properties | Summary |
|------------|---------|
| [Parent](#PAR2197A792) | Gets or sets the parent audio group. |
| [Children](#CHIA28E397F) |  |
| [Default](#DEFCF6EDD47) | Gets the default audio group (ie, the speakers, headphones, etc). |

| Methods | Summary |
|---------|---------|
| [PopulateBuffer](#POP1F4E4746) |  |

### Constructors

#### AudioGroup()

Construct a new audio node that is connected to default audio group (ie, `Heirloom.Sound.AudioGroup.Default`).

#### AudioGroup([AudioGroup](heirloom.sound.audiogroup.md) parentGroup)

Construct a new audio group that is connected to the specified parent group.

#### AudioGroup([AudioGroup](heirloom.sound.audiogroup.md) parentGroup, bool allowOrphan)

### Properties

#### <a name="PAR2197A792"></a>Parent : [AudioGroup](heirloom.sound.audiogroup.md)


Gets or sets the parent audio group.

#### <a name="CHIA28E397F"></a>Children : IEnumerable\<AudioNode>


#### <a name="DEFCF6EDD47"></a>Default : [AudioGroup](heirloom.sound.audiogroup.md)

<small>`Static`, `Read Only`</small>

Gets the default audio group (ie, the speakers, headphones, etc).

### Methods

#### <a name="POP1F4E4746"></a>PopulateBuffer(Span\<float> output) : void

<small>`Virtual`, `Protected`</small>


