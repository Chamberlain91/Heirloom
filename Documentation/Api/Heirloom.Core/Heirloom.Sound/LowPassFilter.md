# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## LowPassFilter (Class)

> **Namespace**: [Heirloom.Sound][0]

An audio effect that implements a low pass filter.

```cs
public class LowPassFilter : AudioEffect
```

### Inherits

[AudioEffect][1]

### Properties

[Frequency][2]

### Methods

[Process][3]

## Properties

#### Instance

| Name           | Type    | Summary                                     |
|----------------|---------|---------------------------------------------|
| [Frequency][2] | `float` | Gets or sets the frequency cutoff in hertz. |

## Methods

#### Instance

| Name                     | Return Type | Summary                                                                |
|--------------------------|-------------|------------------------------------------------------------------------|
| [Process(float, int)][3] | `float`     | This function is called to alter a sample for some implementation o... |

[0]: ../../Heirloom.Core.md
[1]: AudioEffect.md
[2]: LowPassFilter/Frequency.md
[3]: LowPassFilter/Process.md
