# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## IAudioDecoder.Seek (Method)

> **Namespace**: [Heirloom.Sound][0]  
> **Declaring Type**: [IAudioDecoder][1]

### Seek(int)

Seeks the decoder to a desired sample position from the beginning of the audio data.

```cs
public abstract bool Seek(int offset)
```

| Name   | Type  | Summary                 |
|--------|-------|-------------------------|
| offset | `int` | Some offset in samples. |

> **Returns** - `bool` - True, if seeking is possible and was successful.

[0]: ../../../Heirloom.Core.md
[1]: ../IAudioDecoder.md
