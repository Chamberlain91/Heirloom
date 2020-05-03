# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Calc.Swap\<T> (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Calc][1]

### Swap<T>(ref T, ref T)

Swaps two references.

```cs
public static void Swap<T>(ref T a, ref T b)
```

| Name | Type   | Summary |
|------|--------|---------|
| a    | [T][2] |         |
| b    | [T][2] |         |

> **Returns** - `void`

### Swap<T>(IList<T>, int, int)

Swaps two positions within the given list.

```cs
public static void Swap<T>(IList<T> list, int a, int b)
```

`ExtensionAttribute`

| Name | Type        | Summary |
|------|-------------|---------|
| list | `IList\<T>` |         |
| a    | `int`       |         |
| b    | `int`       |         |

> **Returns** - `void`

[0]: ../../../Heirloom.Core.md
[1]: ../Calc.md
[2]: ../T.md
