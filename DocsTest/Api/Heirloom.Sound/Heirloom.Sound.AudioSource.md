# Heirloom.Sound

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Sound](../Heirloom.Sound/Heirloom.Sound.md)</small>  

## AudioSource (Sealed Class)
<small>**Namespace**: Heirloom.Sound</sub></small>  
<small>**Inherits**: [AudioNode](Heirloom.Sound.AudioNode.md)</small>  

An instance of playable audio.

| Properties                      | Summary                                                                                                                                                   |
|---------------------------------|-----------------------------------------------------------------------------------------------------------------------------------------------------------|
| [Group](#GROU12D8)              | Gets or sets which audio group this source is part of (default is `Heirloom.Sound.AudioGroup.Default`).                                                   |
| [IsLooping](#ISLOBA98)          | Should this clip loop when finished playing?                                                                                                              |
| [CanSeek](#CANS2744)            | Is it possible seek through this sources audio data to change playback position.                                                                          |
| [Time](#TIME9C93)               | Gets the current playback time (position) in seconds.                                                                                                     |
| [Duration](#DURAAF85)           | The duration of the audio in seconds. May report zero if the length of the source cannot be determined (ie, some streams or a format limitation).         |
| [Position](#POSIF46C)           | Gets the current playback position (time) in samples.                                                                                                     |
| [Length](#LENG6B36)             | The length of the audio source in PCM frames. May report zero if the length of the source cannot be determined (ie, some streams or a format limitation). |
| [IsPlaybackFinished](#ISPLD02E) | Gets a value that determines if playback has finished.                                                                                                    |

| Events                     | Summary                                                              |
|----------------------------|----------------------------------------------------------------------|
| [PlaybackEnded](#PLAY38F1) | An event invoked when this source reaches the end of playable audio. |

| Methods                     | Summary                                                                                                                                               |
|-----------------------------|-------------------------------------------------------------------------------------------------------------------------------------------------------|
| [Play](#PLAYD2A8)           | Begin playing audio.                                                                                                                                  |
| [Pause](#PAUSB2AE)          | Pause playing audio.                                                                                                                                  |
| [Stop](#STOPB303)           | Pause playing audio and seeks to the beginning in the audio data. If seek is not supported, this is equivalent to `Heirloom.Sound.AudioSource.Pause`. |
| [Seek](#SEEK5273)           | Seek playback position to some time in samples.                                                                                                       |
| [Seek](#SEEK5273)           | Seek playback position to some time in seconds.                                                                                                       |
| [PopulateBuffer](#POPU7E07) |                                                                                                                                                       |

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

#### <a name="GROU12D8"></a> Group : [AudioGroup](Heirloom.Sound.AudioGroup.md)


Gets or sets which audio group this source is part of (default is `Heirloom.Sound.AudioGroup.Default`).

#### <a name="ISLOBA98"></a> IsLooping : bool


Should this clip loop when finished playing?

#### <a name="CANS2744"></a> CanSeek : bool

<small>`Read Only`</small>

Is it possible seek through this sources audio data to change playback position.

#### <a name="TIME9C93"></a> Time : float

<small>`Read Only`</small>

Gets the current playback time (position) in seconds.

#### <a name="DURAAF85"></a> Duration : float

<small>`Read Only`</small>

The duration of the audio in seconds.   
 May report zero if the length of the source cannot be determined (ie, some streams or a format limitation).

#### <a name="POSIF46C"></a> Position : int

<small>`Read Only`</small>

Gets the current playback position (time) in samples.

#### <a name="LENG6B36"></a> Length : int

<small>`Read Only`</small>

The length of the audio source in PCM frames.   
 May report zero if the length of the source cannot be determined (ie, some streams or a format limitation).

#### <a name="ISPLD02E"></a> IsPlaybackFinished : bool

<small>`Read Only`</small>

Gets a value that determines if playback has finished.

### Events

#### PlaybackEnded

An event invoked when this source reaches the end of playable audio.
### Methods

#### <a name="PLAY2D77"></a> Play() : void

Begin playing audio.

#### <a name="PAUSC78D"></a> Pause() : void

Pause playing audio.

#### <a name="STOP4AE1"></a> Stop() : void

Pause playing audio and seeks to the beginning in the audio data. If seek is not supported, this is equivalent to `Heirloom.Sound.AudioSource.Pause`.

#### <a name="SEEKEF91"></a> Seek(int offset) : void

Seek playback position to some time in samples.


#### <a name="SEEK3F39"></a> Seek(float time) : void

Seek playback position to some time in seconds.


#### <a name="POPU1F4E"></a> PopulateBuffer(Span\<float> output) : void
<small>`Virtual`, `Protected`</small>


