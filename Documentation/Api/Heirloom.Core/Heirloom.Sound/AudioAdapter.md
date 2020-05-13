# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## AudioAdapter (Class)

> **Namespace**: [Heirloom.Sound][0]

The abstraction of a low level audio system.

```cs
public abstract class AudioAdapter : IDisposable
```

### Inherits

IDisposable

### Constants

[DefaultSampleRate][1]

### Properties

[IsInitialized][2]

### Methods

[Dispose][3]

### Static Properties

[Channels][4], [InverseSampleRate][5], [IsAudioCaptureEnabled][6], [SampleRate][7]

### Static Events

[AudioCaptured][8]

## Fields

| Name                   | Type  | Summary                                                   |
|------------------------|-------|-----------------------------------------------------------|
| [DefaultSampleRate][1] | `int` | The default sample rate used unless configured otherwise. |

## Properties

#### Instance

| Name               | Type   | Summary                                                                |
|--------------------|--------|------------------------------------------------------------------------|
| [IsInitialized][2] | `bool` | Gets a value that determines of the AudioAdapter (and associated au... |

#### Static

| Name                       | Type    | Summary                                                                |
|----------------------------|---------|------------------------------------------------------------------------|
| [Channels][4]              | `int`   | Gets the number of configured audio channels.                          |
| [InverseSampleRate][5]     | `float` | Gets the the inverse of the configured sample rate (ie, seconds per... |
| [IsAudioCaptureEnabled][6] | `bool`  | Gets a value determining if audio capture (ie, microphone) has been... |
| [SampleRate][7]            | `int`   | Gets the configured sample rate (ie, samples per second).              |

## Events

| Name               | Handler Type              | Summary                                                               |
|--------------------|---------------------------|-----------------------------------------------------------------------|
| [AudioCaptured][8] | [AudioCaptureCallback][9] | This event is raised when samples are captured from the input device. |

## Methods

#### Instance

| Name               | Return Type | Summary                                                                |
|--------------------|-------------|------------------------------------------------------------------------|
| [Dispose(bool)][3] | `void`      | Implements the dispose pattern to selectively dispose managed resou... |
| [Dispose()][3]     | `void`      | Disposes and performs a clean up of any unmanaged resources.           |

[0]: ../../Heirloom.Core.md
[1]: AudioAdapter/DefaultSampleRate.md
[2]: AudioAdapter/IsInitialized.md
[3]: AudioAdapter/Dispose.md
[4]: AudioAdapter/Channels.md
[5]: AudioAdapter/InverseSampleRate.md
[6]: AudioAdapter/IsAudioCaptureEnabled.md
[7]: AudioAdapter/SampleRate.md
[8]: AudioAdapter/AudioCaptured.md
[9]: AudioCaptureCallback.md
