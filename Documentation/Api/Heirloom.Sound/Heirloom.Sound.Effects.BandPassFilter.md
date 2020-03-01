# Heirloom.Sound

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Sound](../Heirloom.Sound/Heirloom.Sound.md)</small>  

## BandPassFilter (Class)
<small>**Namespace**: Heirloom.Sound.Effects</sub></small>  
<small>**Inherits**: [AudioEffect](Heirloom.Sound.AudioEffect.md)</small>  

An audio effect that implements a band pass filter.

| Properties                   | Summary                                  |
|------------------------------|------------------------------------------|
| [MinFrequency](#MINDFD88324) | Gets or sets the filter cutoff in hertz. |
| [Cutoff](#CUTEE3E05B1)       | Gets or sets the filter cutoff in hertz. |

| Methods                 | Summary |
|-------------------------|---------|
| [Process](#PRO1C94C308) |         |

### Constructors

#### BandPassFilter(float low, float high)

### Properties

#### <a name="MINDFD88324"></a>MinFrequency : float


Gets or sets the filter cutoff in hertz.

#### <a name="CUTEE3E05B1"></a>Cutoff : float


Gets or sets the filter cutoff in hertz.

### Methods

#### <a name="PRO1C94C308"></a>Process(float sample, int channel) : float
<small>`Virtual`</small>


