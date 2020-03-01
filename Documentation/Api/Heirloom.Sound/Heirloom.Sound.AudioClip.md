# Heirloom.Sound

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Sound](../Heirloom.Sound/Heirloom.Sound.md)</small>  

## AudioClip (Sealed Class)
<small>**Namespace**: Heirloom.Sound</sub></small>  
<small>`DefaultMemberAttribute`</small>

An object to contain (and decode) audio data into raw samples.

| Properties               | Summary                                   |
|--------------------------|-------------------------------------------|
| [Item](#ITE8B5A2F95)     |                                           |
| [Duration](#DURAF856856) | Gets the duration of the clip in seconds. |
| [Length](#LEN6B366D7E)   | Gets the length of the clip in samples.   |

### Constructors

#### AudioClip(Stream stream)

Constructs a new audio clip from the given stream, fully decoding all samples.

#### AudioClip(byte file)

Constructs a new audio clip from the given in-memory file, fully decoding all samples.

#### AudioClip(short samples)

Constructs a new audio clip from existing samples decoded or generated elsewhere. The samples must be interleved to the number of channels in the device and at the sample rate of the device.

### Properties

#### <a name="ITE8B5A2F95"></a>Item : short

<small>`Read Only`</small>

#### <a name="DURAF856856"></a>Duration : float

<small>`Read Only`</small>

Gets the duration of the clip in seconds.

#### <a name="LEN6B366D7E"></a>Length : int

<small>`Read Only`</small>

Gets the length of the clip in samples.

