# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## IAudioDecoder.Decode (Method)

> **Namespace**: [Heirloom.Sound][0]  
> **Declaring Type**: [IAudioDecoder][1]

### Decode(Span<short>)

Decodes the next block of samples, writing samples into `samples` .

```cs
public abstract int Decode(Span<short> samples)
```

| Name    | Type          | Summary                                   |
|---------|---------------|-------------------------------------------|
| samples | `Span<short>` | The buffer to write decoded samples into. |

> **Returns** - `int` - The actual number of samples decoded, will be less than or equal to...

[0]: ../../../Heirloom.Core.md
[1]: ../IAudioDecoder.md
