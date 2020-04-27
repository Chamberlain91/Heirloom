# ColorBytes.TryParse

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  
> **Namespace**: [Heirloom][0]  
> **Type**: [ColorBytes][1]  

--------------------------------------------------------------------------------

### TryParse(string, out ColorBytes)

Parses a hex-string representation of a color. May be formatted as 'RGB', 'RGBA', 'RRGGBB', 'RRGGBBAA' with or without a preceding '#'.

```cs
public bool TryParse(string color, out ColorBytes outColor)
```

[0]: ..\Heirloom.Core.md
[1]: Heirloom.ColorBytes.md