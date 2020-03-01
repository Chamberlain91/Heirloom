# Heirloom.Sound

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Sound](../Heirloom.Sound/Heirloom.Sound.md)</small>  

## BandPassFilter (Class)
<small>**Namespace**: Heirloom.Sound.Effects</sub></small>  
<small>**Inherits**: [AudioEffect](Heirloom.Sound.AudioEffect.md)</small>  

An audio effect that implements a band pass filter.

| Properties                | Summary                                  |
|---------------------------|------------------------------------------|
| [MinFrequency](#MINFDFD8) | Gets or sets the filter cutoff in hertz. |
| [Cutoff](#CUTOEE3E)       | Gets or sets the filter cutoff in hertz. |

| Methods              | Summary |
|----------------------|---------|
| [Process](#PROC9CD8) |         |

### Constructors

#### BandPassFilter(float low, float high)

### Properties

#### <a name="MINFDFD8"></a> MinFrequency : float


Gets or sets the filter cutoff in hertz.

#### <a name="CUTOEE3E"></a> Cutoff : float


Gets or sets the filter cutoff in hertz.

### Methods

#### <a name="PROC1C94"></a> Process(float sample, int channel) : float
<small>`Virtual`</small>


