# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## HighPassFilter (Class)

> **Namespace**: [Heirloom.Sound][0]

An audio effect that implements a high pass filter.

```cs
public class HighPassFilter : AudioEffect
```

### Inherits

[AudioEffect][1]

### Properties

[Frequency][2]

### Methods

[Process][3]

## Properties

#### Instance

| Name           | Type    | Summary                                            |
|----------------|---------|----------------------------------------------------|
| [Frequency][2] | `float` | Gets or sets the filter cutoff frequency in hertz. |

## Methods

#### Instance

| Name                     | Return Type | Summary |
|--------------------------|-------------|---------|
| [Process(float, int)][3] | `float`     |         |

[0]: ../../Heirloom.Core.md
[1]: AudioEffect.md
[2]: HighPassFilter/Frequency.md
[3]: HighPassFilter/Process.md
