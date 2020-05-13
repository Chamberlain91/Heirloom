# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## LowPassFilter.Process (Method)

> **Namespace**: [Heirloom.Sound][0]  
> **Declaring Type**: [LowPassFilter][1]

### Process(float, int)

This function is called to alter a sample for some implementation of an effect.

```cs
public float Process(float sample, int channel)
```

| Name    | Type    | Summary                                    |
|---------|---------|--------------------------------------------|
| sample  | `float` | The incoming sample to alter.              |
| channel | `int`   | The channel number this sample belongs to. |

> **Returns** - `float` - The altered sample.

[0]: ../../../Heirloom.Core.md
[1]: ../LowPassFilter.md
