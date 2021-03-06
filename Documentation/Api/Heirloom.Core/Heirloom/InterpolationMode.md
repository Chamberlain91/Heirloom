# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## InterpolationMode (Enum)

> **Namespace**: [Heirloom][0]

Represents the behaviour when sampling an image on a non-integer coordinates.

```cs
public enum InterpolationMode : IComparable, IFormattable, IConvertible
```

| Name    | Summary                                                     |
|---------|-------------------------------------------------------------|
| Linear  | Color is sampled using interpolation across nearest pixels. |
| Nearest | Color is sampled using rounding to the nearest pixel.       |

[0]: ../../Heirloom.Core.md
