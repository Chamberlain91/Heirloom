# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Extensions.Choose\<T> (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Extensions][1]

### Choose<T>(Random, IReadOnlyList<T>)

Randomly select one of the specified items.

```cs
public static T Choose<T>(Random this, IReadOnlyList<T> items)
```

`ExtensionAttribute`

| Name  | Type               | Summary |
|-------|--------------------|---------|
| this  | `Random`           |         |
| items | `IReadOnlyList<T>` |         |

> **Returns** - `T`

### Choose<T>(Random, params T[])

```cs
public static T Choose<T>(Random this, params T[] items)
```

`ExtensionAttribute`

| Name  | Type     | Summary |
|-------|----------|---------|
| this  | `Random` |         |
| items | `T[]`    |         |

> **Returns** - `T`

[0]: ../../../Heirloom.Core.md
[1]: ../Extensions.md
