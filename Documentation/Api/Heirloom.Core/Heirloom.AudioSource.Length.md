# AudioSource.Length

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  
> **Namespace**: [Heirloom][0]  
> **Type**: [AudioSource][1]  

--------------------------------------------------------------------------------

### Length

The length of the audio source in PCM frames.   
 May report zero if the length of the source cannot be determined (ie, some streams or a format limitation).

```cs
int Length { get; }
```

[0]: ..\Heirloom.Core.md
[1]: Heirloom.AudioSource.md