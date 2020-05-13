# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## IAudioDecoder (Interface)

> **Namespace**: [Heirloom.Sound][0]

An interface representing an audio decoder.

```cs
public interface IAudioDecoder : IDisposable
```

### Inherits

IDisposable

### Properties

[IsDisposed][1], [Length][2]

### Methods

[Decode][3], [Seek][4]

## Properties

#### Instance

| Name            | Type   | Summary                                                                |
|-----------------|--------|------------------------------------------------------------------------|
| [IsDisposed][1] | `bool` | Gets tha value that determines if this decoder been disposed. Once ... |
| [Length][2]     | `int`  | Gets the length of the pcm frames known to the decoder. May be zero... |

## Methods

#### Instance

| Name                     | Return Type | Summary                                                                |
|--------------------------|-------------|------------------------------------------------------------------------|
| [Decode(Span<short>)][3] | `int`       | Decodes the next block of samples, writing samples into `samples` .    |
| [Seek(int)][4]           | `bool`      | Seeks the decoder to a desired sample position from the beginning o... |

[0]: ../../Heirloom.Core.md
[1]: IAudioDecoder/IsDisposed.md
[2]: IAudioDecoder/Length.md
[3]: IAudioDecoder/Decode.md
[4]: IAudioDecoder/Seek.md
