# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## AudioSource.Length (Property)

> **Namespace**: [Heirloom.Sound][0]  
> **Declaring Type**: [AudioSource][1]

### Length

The length of the audio source in PCM frames.   
 May report zero if the length of the source cannot be determined (ie, some streams or a format limitation).

```cs
public int Length { get; }
```

> **Returns**: `int`

**See Also:** [Time][2], [Duration][3], [Position][4]

[0]: ../../../Heirloom.Core.md
[1]: ../AudioSource.md
[2]: Time.md
[3]: Duration.md
[4]: Position.md
