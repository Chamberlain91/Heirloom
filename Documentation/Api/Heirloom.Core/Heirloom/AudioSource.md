# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## AudioSource (Class)

> **Namespace**: [Heirloom][0]

An instance of playable audio.

```cs
public sealed class AudioSource : AudioNode
```

### Inherits

[AudioNode][1]

### Properties

[CanSeek][2], [Duration][3], [Group][4], [IsLooping][5], [IsPlaybackFinished][6], [Length][7], [Position][8], [Time][9]

### Methods

[Pause][10], [Play][11], [PopulateBuffer][12], [Seek][13], [Stop][14]

### Events

[PlaybackEnded][15]

## Properties

#### Instance

| Name                    | Type             | Summary                                                                |
|-------------------------|------------------|------------------------------------------------------------------------|
| [CanSeek][2]            | `bool`           | Is it possible seek through this sources audio data to change playb... |
| [Duration][3]           | `float`          | The duration of the audio in seconds. May report zero if the length... |
| [Group][4]              | [AudioGroup][16] | Gets or sets which audio group this source is part of (default is A... |
| [IsLooping][5]          | `bool`           | Should this clip loop when finished playing?                           |
| [IsPlaybackFinished][6] | `bool`           | Gets a value that determines if playback has finished.                 |
| [Length][7]             | `int`            | The length of the audio source in PCM frames. May report zero if th... |
| [Position][8]           | `int`            | Gets the current playback position (time) in samples.                  |
| [Time][9]               | `float`          | Gets the current playback time (position) in seconds.                  |

## Events

#### Instance

| Name                | Handler Type | Summary                                                              |
|---------------------|--------------|----------------------------------------------------------------------|
| [PlaybackEnded][15] | `Action`     | An event invoked when this source reaches the end of playable audio. |

## Methods

#### Instance

| Name                            | Return Type | Summary                                                                |
|---------------------------------|-------------|------------------------------------------------------------------------|
| [Pause()][10]                   | `void`      | Pause playing audio.                                                   |
| [Play()][11]                    | `void`      | Begin playing audio.                                                   |
| [PopulateBuffer(Span<fl...][12] | `void`      |                                                                        |
| [Seek(int)][13]                 | `void`      | Seek playback position to some time in samples.                        |
| [Seek(float)][13]               | `void`      | Seek playback position to some time in seconds.                        |
| [Stop()][14]                    | `void`      | Pause playing audio and seeks to the beginning in the audio data. I... |

[0]: ../../Heirloom.Core.md
[1]: AudioNode.md
[2]: AudioSource/CanSeek.md
[3]: AudioSource/Duration.md
[4]: AudioSource/Group.md
[5]: AudioSource/IsLooping.md
[6]: AudioSource/IsPlaybackFinished.md
[7]: AudioSource/Length.md
[8]: AudioSource/Position.md
[9]: AudioSource/Time.md
[10]: AudioSource/Pause.md
[11]: AudioSource/Play.md
[12]: AudioSource/PopulateBuffer.md
[13]: AudioSource/Seek.md
[14]: AudioSource/Stop.md
[15]: AudioSource/PlaybackEnded.md
[16]: AudioGroup.md
