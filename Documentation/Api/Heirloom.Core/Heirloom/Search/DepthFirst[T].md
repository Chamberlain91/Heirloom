# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Search.DepthFirst\<T> (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Search][1]

### DepthFirst<T>(T, Func<T, IEnumerable<T>>)

```cs
public static IEnumerable<T> DepthFirst<T>(T start, Func<T, IEnumerable<T>> getSuccessors)
```

`IteratorStateMachineAttribute`

| Name          | Type                        | Summary |
|---------------|-----------------------------|---------|
| start         | `T`                         |         |
| getSuccessors | `Func\<T, IEnumerable\<T>>` |         |

> **Returns** - `IEnumerable\<T>`

[0]: ../../../Heirloom.Core.md
[1]: ../Search.md
