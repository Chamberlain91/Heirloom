# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## MergeSort.StableSort\<T> (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [MergeSort][1]

### StableSort<T>(IList<T>)

Sorts the elements of the list using a stable sort.

```cs
public static void StableSort<T>(IList<T> list)
```

`ExtensionAttribute`

| Name | Type        | Summary |
|------|-------------|---------|
| list | `IList\<T>` |         |

> **Returns** - `void`

### StableSort<T>(IList<T>, Comparison<T>)

Sorts the elements of the list using a stable sort.

```cs
public static void StableSort<T>(IList<T> items, Comparison<T> comparison)
```

`ExtensionAttribute`

| Name       | Type             | Summary |
|------------|------------------|---------|
| items      | `IList\<T>`      |         |
| comparison | `Comparison\<T>` |         |

> **Returns** - `void`

[0]: ../../../Heirloom.Core.md
[1]: ../MergeSort.md
