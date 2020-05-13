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

[HighFrequency][2], [LowFrequency][3]

### Methods

[Process][4]

## Properties

#### Instance

| Name               | Type    | Summary                                          |
|--------------------|---------|--------------------------------------------------|
| [HighFrequency][2] | `float` | Gets or sets the high frequency cutoff in hertz. |
| [LowFrequency][3]  | `float` | Gets or sets the low frequency cutoff in hertz.  |

## Methods

#### Instance

| Name                     | Return Type | Summary                                                                |
|--------------------------|-------------|------------------------------------------------------------------------|
| [Process(float, int)][4] | `float`     | This function is called to alter a sample for some implementation o... |

[0]: ../../Heirloom.Core.md
[1]: AudioEffect.md
[2]: BandPassFilter/HighFrequency.md
[3]: BandPassFilter/LowFrequency.md
[4]: BandPassFilter/Process.md
