# Heirloom.Sound

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Sound](../Heirloom.Sound/Heirloom.Sound.md)</small>  

## AudioContext (Abstract Class)
<small>**Namespace**: Heirloom.Sound</sub></small>  
<small>**Interfaces**: IDisposable</small>  

| Properties                         | Summary                                                                                                       |
|------------------------------------|---------------------------------------------------------------------------------------------------------------|
| [IsAudioCaptureEnabled](#ISAU38A6) | Gets a value determining if audio capture (ie, microphone) has been enabled.                                  |
| [SampleRate](#SAMPECB1)            | Gets the configured sample rate (ie, samples per second).                                                     |
| [InverseSampleRate](#INVEFD21)     | Gets the the inverse of the configured sample rate (ie, seconds per sample)                                   |
| [Channels](#CHAN9746)              | Gets the number of configured channels.                                                                       |
| [Instance](#INST4FAA)              | Gets the audio context instance. This will initialize with defaults if not explicitly initialized beforehand. |

| Events                     | Summary                                                                 |
|----------------------------|-------------------------------------------------------------------------|
| [AudioCaptured](#AUDID74F) | Event invoked when a chunk of audio data is captured by the microphone. |

| Methods                 | Summary                                                                                           |
|-------------------------|---------------------------------------------------------------------------------------------------|
| [Dispose](#DISP8A0D)    |                                                                                                   |
| [Dispose](#DISP8A0D)    |                                                                                                   |
| [Initialize](#INITDC05) | Initialize the audio system with a sample rate of 44100 and optionally enabling audio capture.    |
| [Initialize](#INITDC05) | Initialize the audio system with the specified sample rate and optionally enabling audio capture. |

### Constructors

#### AudioContext(int sampleRate, bool enableAudioCapture)

### Properties

#### <a name="ISAU38A6"></a> IsAudioCaptureEnabled : bool

<small>`Static`, `Read Only`</small>

Gets a value determining if audio capture (ie, microphone) has been enabled.

#### <a name="SAMPECB1"></a> SampleRate : int

<small>`Static`, `Read Only`</small>

Gets the configured sample rate (ie, samples per second).

#### <a name="INVEFD21"></a> InverseSampleRate : float

<small>`Static`, `Read Only`</small>

Gets the the inverse of the configured sample rate (ie, seconds per sample)

#### <a name="CHAN9746"></a> Channels : int

<small>`Static`, `Read Only`</small>

Gets the number of configured channels.

#### <a name="INST4FAA"></a> Instance : [AudioContext](Heirloom.Sound.AudioContext.md)

<small>`Static`</small>

Gets the audio context instance. This will initialize with defaults if not explicitly initialized beforehand.

### Events

#### AudioCaptured

Event invoked when a chunk of audio data is captured by the microphone.
### Methods

#### <a name="DISPD833"></a> Dispose(bool disposing) : void
<small>`Virtual`, `Protected`</small>


#### <a name="DISP4E62"></a> Dispose() : void
<small>`Virtual`</small>

#### <a name="INIT50E8"></a> Initialize(bool enableAudioCapture) : void
<small>`Static`</small>

Initialize the audio system with a sample rate of 44100 and optionally enabling audio capture.

<small>**enableAudioCapture**: <param name="enableAudioCapture">Should we enable audio capture?</param></small>  

#### <a name="INIT6EC6"></a> Initialize(int sampleRate, bool enableAudioCapture) : void
<small>`Static`</small>

Initialize the audio system with the specified sample rate and optionally enabling audio capture.


