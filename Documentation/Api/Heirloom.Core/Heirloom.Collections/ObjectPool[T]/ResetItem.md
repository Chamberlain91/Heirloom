# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## ObjectPool\<T>.ResetItem (Method)

> **Namespace**: [Heirloom.Collections][0]  
> **Declaring Type**: [ObjectPool\<T>][1]

### ResetItem(T)

When [ObjectPool\<T>][1] is subclassed, this can be overriden to clear state on recycled objects.

```cs
protected void ResetItem(T item)
```

| Name | Type | Summary            |
|------|------|--------------------|
| item | `T`  | A recycled object. |

> **Returns** - `void`

[0]: ../../../Heirloom.Core.md
[1]: ../ObjectPool[T].md
