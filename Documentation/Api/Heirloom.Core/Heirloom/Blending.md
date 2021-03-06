# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Blending (Enum)

> **Namespace**: [Heirloom][0]

Controls how drawing operations are blended into existing pixels.

```cs
public enum Blending : IComparable, IFormattable, IConvertible
```

| Name        | Summary                                                                                     |
|-------------|---------------------------------------------------------------------------------------------|
| Additive    | Drawn pixels are additively blended based on their alpha values with existing pixels.       |
| Alpha       | Draw pixels are blended based on their alpha values with existing pixels.                   |
| Invert      | Drawn pixels act like an inversion filter with existing pixels.                             |
| Multiply    | Drawn pixels are multiplicatively blended based on their alpha values with existing pixels. |
| Opaque      | Drawn pixels are fully opaque and will replace existing pixels.                             |
| Subtractive | Drawn pixels are subtractively blended based on their alpha values with existing pixels.    |

[0]: ../../Heirloom.Core.md
