# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## ColorBytes.TryParse (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [ColorBytes][1]

### TryParse(string, out ColorBytes)

Parses a hex-string representation of a color. May be formatted as 'RGB', 'RGBA', 'RRGGBB', 'RRGGBBAA' with or without a preceding '#'.

```cs
public static bool TryParse(string color, out ColorBytes outColor)
```

| Name     | Type            | Summary                   |
|----------|-----------------|---------------------------|
| color    | `string`        | The hex-encoded string.   |
| outColor | [ColorBytes][1] | Outputs the parsed color. |

> **Returns** - `bool` - True if color was successfully parsed, otherwise false.

[0]: ../../../Heirloom.Core.md
[1]: ../ColorBytes.md
