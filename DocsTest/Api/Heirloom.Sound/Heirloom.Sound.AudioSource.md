# Heirloom.Sound

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Sound](../Heirloom.Sound/Heirloom.Sound.md)</small>  

## AudioSource (Sealed Class)
<small>**Namespace**: Heirloom.Sound</sub></small>  
<small>**Inherits**: [AudioNode](Heirloom.Sound.AudioNode.md)</small>  

An instance of playable audio.

| Properties                         | Summary                                                                                                                                                   |
|------------------------------------|-----------------------------------------------------------------------------------------------------------------------------------------------------------|
| [Group](#GRO12D88E27)              | Gets or sets which audio group this source is part of (default is `Heirloom.Sound.AudioGroup.Default`).                                                   |
| [IsLooping](#ISLBA98A384)          | Should this clip loop when finished playing?                                                                                                              |
| [CanSeek](#CAN2744B0C0)            | Is it possible seek through this sources audio data to change playback position.                                                                          |
| [Time](#TIM9C9392A9)               | Gets the current playback time (position) in seconds.                                                                                                     |
| [Duration](#DURAF856856)           | The duration of the audio in seconds. May report zero if the length of the source cannot be determined (ie, some streams or a format limitation).         |
| [Position](#POSF46C3C91)           | Gets the current playback position (time) in samples.                                                                                                     |
| [Length](#LEN6B366D7E)             | The length of the audio source in PCM frames. May report zero if the length of the source cannot be determined (ie, some streams or a format limitation). |
| [IsPlaybackFinished](#ISPD02EF1FB) | Gets a value that determines if playback has finished.                                                                                                    |

| Events                        | Summary                                                              |
|-------------------------------|----------------------------------------------------------------------|
| [PlaybackEnded](#PLA38F157E5) | An event invoked when this source reaches the end of playable audio. |

| Methods                        | Summary                                                                                                                                               |
|--------------------------------|-------------------------------------------------------------------------------------------------------------------------------------------------------|
| [Play](#PLA2D77C0A3)           | Begin playing audio.                                                                                                                                  |
| [Pause](#PAUC78D044D)          | Pause playing audio.                                                                                                                                  |
| [Stop](#STO4AE17E3B)           | Pause playing audio and seeks to the beginning in the audio data. If seek is not supported, this is equivalent to `Heirloom.Sound.AudioSource.Pause`. |
| [Seek](#SEEEF91C34D)           | Seek playback position to some time in samples.                                                                                                       |
| [Seek](#SEE3F393E40)           | Seek playback position to some time in seconds.                                                                                                       |
| [PopulateBuffer](#POP1F4E4746) |                                                                                                                                                       |

### Constructors

#### AudioSource([AudioClip](Heirloom.Sound.AudioClip.md) clip)

Create an audio source for the given clip in the default audio group (ie, `Heirloom.Sound.AudioGroup.Default`).

#### AudioSource([AudioClip](Heirloom.Sound.AudioClip.md) clip, [AudioGroup](Heirloom.Sound.AudioGroup.md) group)

Create an audio source for the given clip in the specified audio group.

#### AudioSource(Stream stream)

Create an audio source for the given stream in the default audio group (ie, `Heirloom.Sound.AudioGroup.Default`).

#### AudioSource(Stream stream, [AudioGroup](Heirloom.Sound.AudioGroup.md) group)

Create an audio source for the given stream in the specified audio group.

#### AudioSource([IAudioProvider](Heirloom.Sound.IAudioProvider.md) provider, [AudioGroup](Heirloom.Sound.AudioGroup.md) group)

### Properties

#### <a name="GRO12D88E27"></a>Group : [AudioGroup](Heirloom.Sound.AudioGroup.md)


Gets or sets which audio group this source is part of (default is `Heirloom.Sound.AudioGroup.Default`).

#### <a name="ISLBA98A384"></a>IsLooping : bool


Should this clip loop when finished playing?

#### <a name="CAN2744B0C0"></a>CanSeek : bool

<small>`Read Only`</small>

Is it possible seek through this sources audio data to change playback position.

#### <a name="TIM9C9392A9"></a>Time : float

<small>`Read Only`</small>

Gets the current playback time (position) in seconds.

#### <a name="DURAF856856"></a>Duration : float

<small>`Read Only`</small>

The duration of the audio in seconds.   
 May report zero if the length of the source cannot be determined (ie, some streams or a format limitation).

#### <a name="POSF46C3C91"></a>Position : int

<small>`Read Only`</small>

Gets the current playback position (time) in samples.

#### <a name="LEN6B366D7E"></a>Length : int

<small>`Read Only`</small>

The length of the audio source in PCM frames.   
 May report zero if the length of the source cannot be determined (ie, some streams or a format limitation).

#### <a name="ISPD02EF1FB"></a>IsPlaybackFinished : bool

<small>`Read Only`</small>

Gets a value that determines if playback has finished.

### Events

#### PlaybackEnded

An event invoked when this source reaches the end of playable audio.
### Methods

#### <a name="PLA2D77C0A3"></a>Play() : void

Begin playing audio.

#### <a name="PAUC78D044D"></a>Pause() : void

Pause playing audio.

#### <a name="STO4AE17E3B"></a>Stop() : void

Pause playing audio and seeks to the beginning in the audio data. If seek is not supported, this is equivalent to `Heirloom.Sound.AudioSource.Pause`.

#### <a name="SEEEF91C34D"></a>Seek(int offset) : void

Seek playback position to some time in samples.


#### <a name="SEE3F393E40"></a>Seek(float time) : void

Seek playback position to some time in seconds.


#### <a name="POP1F4E4746"></a>PopulateBuffer(Span\<float> output) : void
<small>`Virtual`, `Protected`</small>


