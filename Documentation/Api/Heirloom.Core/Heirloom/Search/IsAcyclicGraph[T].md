# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Search.IsAcyclicGraph\<T> (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Search][1]

### IsAcyclicGraph<T>(T, Func<T, IEnumerable<T>>)

```cs
public static bool IsAcyclicGraph<T>(T start, Func<T, IEnumerable<T>> getSuccessors)
```

| Name          | Type                        | Summary |
|---------------|-----------------------------|---------|
| start         | `T`                         |         |
| getSuccessors | `Func\<T, IEnumerable\<T>>` |         |

> **Returns** - `bool`

[0]: ../../../Heirloom.Core.md
[1]: ../Search.md
