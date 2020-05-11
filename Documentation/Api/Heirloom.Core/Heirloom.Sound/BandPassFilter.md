# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## BandPassFilter (Class)

> **Namespace**: [Heirloom.Sound][0]

An audio effect that implements a band pass filter.

```cs
public class BandPassFilter : AudioEffect
```

### Inherits

[AudioEffect][1]

### Properties

[Cutoff][2], [MinFrequency][3]

### Methods

[Process][4]

## Properties

#### Instance

| Name              | Type    | Summary                                  |
|-------------------|---------|------------------------------------------|
| [Cutoff][2]       | `float` | Gets or sets the filter cutoff in hertz. |
| [MinFrequency][3] | `float` | Gets or sets the filter cutoff in hertz. |

## Methods

#### Instance

| Name                     | Return Type | Summary |
|--------------------------|-------------|---------|
| [Process(float, int)][4] | `float`     |         |

[0]: ../../Heirloom.Core.md
[1]: AudioEffect.md
[2]: BandPassFilter/Cutoff.md
[3]: BandPassFilter/MinFrequency.md
[4]: BandPassFilter/Process.md
