# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Time.Convert (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Time][1]

### Convert(float, TimeUnit, TimeUnit)

Converts time from unit to another.

```cs
public static float Convert(float value, TimeUnit source, TimeUnit target)
```

| Name   | Type          | Summary                                          |
|--------|---------------|--------------------------------------------------|
| value  | `float`       | Some value of time in `source` units.            |
| source | [TimeUnit][2] | Some representation of units of time ( input ).  |
| target | [TimeUnit][2] | Some representation of units of time ( return ). |

> **Returns** - `float` - Some conversion from `source` to `target` units.

[0]: ../../../Heirloom.Core.md
[1]: ../Time.md
[2]: ../TimeUnit.md
