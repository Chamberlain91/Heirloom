# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## AudioEffect (Class)

> **Namespace**: [Heirloom.Sound][0]

An abstarct representation of an audio effect. Implementations of this class mutate the audio for various effects.

```cs
public abstract class AudioEffect
```

**See Also:** [LowPassFilter][1], [HighPassFilter][2], [BandPassFilter][3], [ReverbEffect][4]

### Methods

[Process][5]

## Methods

#### Instance

| Name                     | Return Type | Summary                                                                |
|--------------------------|-------------|------------------------------------------------------------------------|
| [Process(float, int)][5] | `float`     | This function is called to alter a sample for some implementation o... |

[0]: ../../Heirloom.Core.md
[1]: LowPassFilter.md
[2]: HighPassFilter.md
[3]: BandPassFilter.md
[4]: ReverbEffect.md
[5]: AudioEffect/Process.md
