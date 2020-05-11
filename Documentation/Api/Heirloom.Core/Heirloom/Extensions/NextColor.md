# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Extensions.NextColor (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Extensions][1]

### NextColor(Random, bool)

Returns a random RGB color (optionally RGBA).

```cs
public static Color NextColor(Random random, bool useAlpha = False)
```

`ExtensionAttribute`

| Name     | Type     | Summary                                                      |
|----------|----------|--------------------------------------------------------------|
| random   | `Random` | The instance this extension method operates on.              |
| useAlpha | `bool`   | Should this call randomize the alpha or use an alpha of 1.0? |

> **Returns** - [Color][2]

[0]: ../../../Heirloom.Core.md
[1]: ../Extensions.md
[2]: ../Color.md
