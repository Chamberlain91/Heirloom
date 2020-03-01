# Heirloom.Sound

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Sound](../Heirloom.Sound/Heirloom.Sound.md)</small>  

## AudioClip (Sealed Class)
<small>**Namespace**: Heirloom.Sound</sub></small>  

An object to contain (and decode) audio data into raw samples.

| Properties            | Summary                                   |
|-----------------------|-------------------------------------------|
| [Item](#ITEM8B5A)     |                                           |
| [Duration](#DURAAF85) | Gets the duration of the clip in seconds. |
| [Length](#LENG6B36)   | Gets the length of the clip in samples.   |

### Constructors

#### AudioClip(Stream stream)

Constructs a new audio clip from the given stream, fully decoding all samples.

#### AudioClip(byte file)

Constructs a new audio clip from the given in-memory file, fully decoding all samples.

#### AudioClip(short samples)

Constructs a new audio clip from existing samples decoded or generated elsewhere. The samples must be interleved to the number of channels in the device and at the sample rate of the device.

### Properties

#### <a name="ITEM8B5A"></a> Item : short

<small>`Read Only`</small>

#### <a name="DURAAF85"></a> Duration : float

<small>`Read Only`</small>

Gets the duration of the clip in seconds.

#### <a name="LENG6B36"></a> Length : int

<small>`Read Only`</small>

Gets the length of the clip in samples.

