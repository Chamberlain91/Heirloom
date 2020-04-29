# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## SparseGrid\<T>.HasValue (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [SparseGrid\<T>][1]

### HasValue(in int, in int)

Determines if a value has been set on this cell of the sparse grid.

```cs
public bool HasValue(in int x, in int y)
```

| Name | Type  | Summary |
|------|-------|---------|
| x    | `int` |         |
| y    | `int` |         |

> **Returns** - `bool`

### HasValue(in IntVector)

Determines if a value has been set on this cell of the sparse grid.

```cs
public bool HasValue(in IntVector co)
```

| Name | Type           | Summary |
|------|----------------|---------|
| co   | [IntVector][2] |         |

> **Returns** - `bool`

[0]: ../../../Heirloom.Core.md
[1]: ../SparseGrid[T].md
[2]: ../IntVector.md
