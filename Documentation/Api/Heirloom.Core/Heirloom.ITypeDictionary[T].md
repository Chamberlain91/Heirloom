# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  

## ITypeDictionary\<T>

> **Namespace**: [Heirloom][0]  

Manages objects by their type hierarchy up to the base type, allowing access by enumeration of objects by type.

```cs
public abstract interface ITypeDictionary<T> : IReadOnlyTypeDictionary<T>, IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable
```

### Inherits

[IReadOnlyTypeDictionary\<T>][1], IReadOnlyCollection\<T>, IEnumerable\<T>, IEnumerable

#### Methods

[Add][2], [Remove][3]

## Methods

| Name        | Summary                                    |
|-------------|--------------------------------------------|
| [Add][2]    | Add a new object to the type dictionary.   |
| [Remove][3] | Remove an object from the type dictionary. |

[0]: ../Heirloom.Core.md
[1]: Heirloom.IReadOnlyTypeDictionary[T].md
[2]: Heirloom.ITypeDictionary[T].Add.md
[3]: Heirloom.ITypeDictionary[T].Remove.md
