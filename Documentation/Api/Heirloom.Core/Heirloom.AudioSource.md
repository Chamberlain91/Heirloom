# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  

## AudioSource

> **Namespace**: [Heirloom][0]  

An instance of playable audio.

```cs
public sealed class AudioSource : AudioNode
```

### Inherits

[AudioNode][1]

#### Properties

[Group][2], [IsLooping][3], [CanSeek][4], [Time][5], [Duration][6], [Position][7], [Length][8], [IsPlaybackFinished][9]

#### Methods

[Play][10], [Pause][11], [Stop][12], [Seek][13], [PopulateBuffer][14]

#### Events

[PlaybackEnded][15]

## Properties

| Name                    | Summary                                                                                                                                                   |
|-------------------------|-----------------------------------------------------------------------------------------------------------------------------------------------------------|
| [Group][2]              | Gets or sets which audio group this source is part of (default is [AudioGroup.Default][16] ).                                                             |
| [IsLooping][3]          | Should this clip loop when finished playing?                                                                                                              |
| [CanSeek][4]            | Is it possible seek through this sources audio data to change playback position.                                                                          |
| [Time][5]               | Gets the current playback time (position) in seconds.                                                                                                     |
| [Duration][6]           | The duration of the audio in seconds. May report zero if the length of the source cannot be determined (ie, some streams or a format limitation).         |
| [Position][7]           | Gets the current playback position (time) in samples.                                                                                                     |
| [Length][8]             | The length of the audio source in PCM frames. May report zero if the length of the source cannot be determined (ie, some streams or a format limitation). |
| [IsPlaybackFinished][9] | Gets a value that determines if playback has finished.                                                                                                    |

## Events

| Name                | Summary                                                              |
|---------------------|----------------------------------------------------------------------|
| [PlaybackEnded][15] | An event invoked when this source reaches the end of playable audio. |

## Methods

| Name                 | Summary                                                                                                                         |
|----------------------|---------------------------------------------------------------------------------------------------------------------------------|
| [Play][10]           | Begin playing audio.                                                                                                            |
| [Pause][11]          | Pause playing audio.                                                                                                            |
| [Stop][12]           | Pause playing audio and seeks to the beginning in the audio data. If seek is not supported, this is equivalent to [Pause][11] . |
| [Seek][13]           | Seek playback position to some time in samples.                                                                                 |
| [Seek][13]           | Seek playback position to some time in seconds.                                                                                 |
| [PopulateBuffer][14] |                                                                                                                                 |

[0]: ../Heirloom.Core.md
[1]: Heirloom.AudioNode.md
[2]: Heirloom.AudioSource.Group.md
[3]: Heirloom.AudioSource.IsLooping.md
[4]: Heirloom.AudioSource.CanSeek.md
[5]: Heirloom.AudioSource.Time.md
[6]: Heirloom.AudioSource.Duration.md
[7]: Heirloom.AudioSource.Position.md
[8]: Heirloom.AudioSource.Length.md
[9]: Heirloom.AudioSource.IsPlaybackFinished.md
[10]: Heirloom.AudioSource.Play.md
[11]: Heirloom.AudioSource.Pause.md
[12]: Heirloom.AudioSource.Stop.md
[13]: Heirloom.AudioSource.Seek.md
[14]: Heirloom.AudioSource.PopulateBuffer.md
[15]: Heirloom.AudioSource.PlaybackEnded.md
[16]: Heirloom.AudioGroup.Default.md
