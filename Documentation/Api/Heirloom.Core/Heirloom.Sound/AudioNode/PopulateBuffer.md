# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## AudioNode.PopulateBuffer (Method)

> **Namespace**: [Heirloom.Sound][0]  
> **Declaring Type**: [AudioNode][1]

### PopulateBuffer(Span<float>)

This function is called when more samples are needed for playback.

```cs
protected abstract void PopulateBuffer(Span<float> buffer)
```

| Name   | Type          | Summary                                            |
|--------|---------------|----------------------------------------------------|
| buffer | `Span<float>` | The buffer to copy the next block of samples into. |

> **Returns** - `void`

Copy the next block of samples into `buffer` . The number of samples to copy length of the `Span<T>` .   
 Note: The number of samples may not be consistent across subsequent calls.

[0]: ../../../Heirloom.Core.md
[1]: ../AudioNode.md
