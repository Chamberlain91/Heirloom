# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Grid\<T>.IsValidCoordinate (Method)

> **Namespace**: [Heirloom.Collections][0]  
> **Declaring Type**: [Grid\<T>][1]

### IsValidCoordinate(in int, in int)

Determines if the specified coordinate is a valid coordinate within the grid.

```cs
public bool IsValidCoordinate(in int x, in int y)
```

| Name | Type  | Summary |
|------|-------|---------|
| x    | `int` |         |
| y    | `int` |         |

> **Returns** - `bool`

### IsValidCoordinate(in IntVector)

Determines if the specified coordinate is a valid coordinate within the grid.

```cs
public bool IsValidCoordinate(in IntVector co)
```

| Name | Type           | Summary |
|------|----------------|---------|
| co   | [IntVector][2] |         |

> **Returns** - `bool`

[0]: ../../../Heirloom.Core.md
[1]: ../Grid[T].md
[2]: ../../Heirloom/IntVector.md
