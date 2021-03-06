# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## RepeatMode (Enum)

> **Namespace**: [Heirloom][0]

Represents the behaviour when sampling an image outside its natural bounds.

```cs
public enum RepeatMode : IComparable, IFormattable, IConvertible
```

| Name   | Summary                                                                   |
|--------|---------------------------------------------------------------------------|
| Blank  | Sampling coordinates outside image return transparent black.              |
| Clamp  | Sampling coordinates are clamped to image bounds.                         |
| Repeat | Sampling coordinates outside image bounds cause the image to be repeated. |

[0]: ../../Heirloom.Core.md
