# Heirloom.Sound

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Sound](../Heirloom.Sound/Heirloom.Sound.md)</small>  

## AudioContext (Abstract Class)
<small>**Namespace**: Heirloom.Sound</small>  
<small>**Interfaces**: IDisposable</small>  

| Properties                            | Summary                                                                                                       |
|---------------------------------------|---------------------------------------------------------------------------------------------------------------|
| [IsAudioCaptureEnabled](#ISA38A6274D) | Gets a value determining if audio capture (ie, microphone) has been enabled.                                  |
| [SampleRate](#SAMECB101A)             | Gets the configured sample rate (ie, samples per second).                                                     |
| [InverseSampleRate](#INVFD218F4A)     | Gets the the inverse of the configured sample rate (ie, seconds per sample)                                   |
| [Channels](#CHA97465DEE)              | Gets the number of configured channels.                                                                       |
| [Instance](#INS4FAA4721)              | Gets the audio context instance. This will initialize with defaults if not explicitly initialized beforehand. |

| Events                        | Summary                                                                 |
|-------------------------------|-------------------------------------------------------------------------|
| [AudioCaptured](#AUDD74F000E) | Event invoked when a chunk of audio data is captured by the microphone. |

| Methods                    | Summary                                                                                           |
|----------------------------|---------------------------------------------------------------------------------------------------|
| [Dispose](#DISD833FA7A)    |                                                                                                   |
| [Dispose](#DIS4E62D250)    |                                                                                                   |
| [Initialize](#INI50E87BC2) | Initialize the audio system with a sample rate of 44100 and optionally enabling audio capture.    |
| [Initialize](#INI6EC6B529) | Initialize the audio system with the specified sample rate and optionally enabling audio capture. |

### Constructors

#### AudioContext(int sampleRate, bool enableAudioCapture)

### Properties

#### <a name="ISA38A6274D"></a>IsAudioCaptureEnabled : bool

<small>`Static`, `Read Only`</small>

Gets a value determining if audio capture (ie, microphone) has been enabled.

#### <a name="SAMECB101A"></a>SampleRate : int

<small>`Static`, `Read Only`</small>

Gets the configured sample rate (ie, samples per second).

#### <a name="INVFD218F4A"></a>InverseSampleRate : float

<small>`Static`, `Read Only`</small>

Gets the the inverse of the configured sample rate (ie, seconds per sample)

#### <a name="CHA97465DEE"></a>Channels : int

<small>`Static`, `Read Only`</small>

Gets the number of configured channels.

#### <a name="INS4FAA4721"></a>Instance : [AudioContext](Heirloom.Sound.AudioContext.md)

<small>`Static`</small>

Gets the audio context instance. This will initialize with defaults if not explicitly initialized beforehand.

### Events

#### AudioCaptured

Event invoked when a chunk of audio data is captured by the microphone.
### Methods

#### <a name="DISD833FA7A"></a>Dispose(bool disposing) : void
<small>`Virtual`, `Protected`</small>


#### <a name="DIS4E62D250"></a>Dispose() : void
<small>`Virtual`</small>

#### <a name="INI50E87BC2"></a>Initialize(bool enableAudioCapture) : void
<small>`Static`</small>

Initialize the audio system with a sample rate of 44100 and optionally enabling audio capture.

<small>**enableAudioCapture**: <param name="enableAudioCapture">Should we enable audio capture?</param></small>  

#### <a name="INI6EC6B529"></a>Initialize(int sampleRate, bool enableAudioCapture) : void
<small>`Static`</small>

Initialize the audio system with the specified sample rate and optionally enabling audio capture.


