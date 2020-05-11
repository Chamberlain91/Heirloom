# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## IReadOnlyGrid\<T>.IsValidCoordinate (Method)

> **Namespace**: [Heirloom.Collections][0]  
> **Declaring Type**: [IReadOnlyGrid\<T>][1]

### IsValidCoordinate(in int, in int)

Is the specified coordinate valid on this grid?

```cs
public abstract bool IsValidCoordinate(in int x, in int y)
```

| Name | Type  | Summary |
|------|-------|---------|
| x    | `int` |         |
| y    | `int` |         |

> **Returns** - `bool`

### IsValidCoordinate(in IntVector)

Is the specified coordinate valid on this grid?

```cs
public abstract bool IsValidCoordinate(in IntVector co)
```

| Name | Type           | Summary |
|------|----------------|---------|
| co   | [IntVector][2] |         |

> **Returns** - `bool`

[0]: ../../../Heirloom.Core.md
[1]: ../IReadOnlyGrid[T].md
[2]: ../../Heirloom/IntVector.md
