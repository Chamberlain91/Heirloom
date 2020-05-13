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

[Dispose][3], [Finalize][4]

### Static Properties

[Channels][5], [InverseSampleRate][6], [IsAudioCaptureEnabled][7], [SampleRate][8]

### Static Events

[AudioCaptured][9]

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
| [Channels][5]              | `int`   | Gets the number of configured audio channels.                          |
| [InverseSampleRate][6]     | `float` | Gets the the inverse of the configured sample rate (ie, seconds per... |
| [IsAudioCaptureEnabled][7] | `bool`  | Gets a value determining if audio capture (ie, microphone) has been... |
| [SampleRate][8]            | `int`   | Gets the configured sample rate (ie, samples per second).              |

## Events

| Name               | Handler Type               | Summary                                                               |
|--------------------|----------------------------|-----------------------------------------------------------------------|
| [AudioCaptured][9] | [AudioCaptureCallback][10] | This event is raised when samples are captured from the input device. |

## Methods

#### Instance

| Name               | Return Type | Summary                                                                |
|--------------------|-------------|------------------------------------------------------------------------|
| [Dispose(bool)][3] | `void`      | Implements the dispose pattern to selectively dispose managed resou... |
| [Dispose()][3]     | `void`      | Disposes and performs a clean up of any unmanaged resources.           |
| [Finalize()][4]    | `void`      | Cleans up any resources before this object gets collected.             |

[0]: ../../Heirloom.Core.md
[1]: AudioAdapter/DefaultSampleRate.md
[2]: AudioAdapter/IsInitialized.md
[3]: AudioAdapter/Dispose.md
[4]: AudioAdapter/Finalize.md
[5]: AudioAdapter/Channels.md
[6]: AudioAdapter/InverseSampleRate.md
[7]: AudioAdapter/IsAudioCaptureEnabled.md
[8]: AudioAdapter/SampleRate.md
[9]: AudioAdapter/AudioCaptured.md
[10]: AudioCaptureCallback.md
