# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## ITypeDictionary\<T> (Interface)

> **Namespace**: [Heirloom.Collections][0]

Manages objects by their type hierarchy up to the base type, allowing access by enumeration of objects by type.

```cs
public interface ITypeDictionary<T> : IReadOnlyTypeDictionary<T>, IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable
```

### Inherits

[IReadOnlyTypeDictionary\<T>][1], IReadOnlyCollection\<T>, IEnumerable\<T>, IEnumerable

### Methods

[Add][2], [Remove][3]

## Methods

#### Instance

| Name           | Return Type | Summary                                    |
|----------------|-------------|--------------------------------------------|
| [Add(T)][2]    | `bool`      | Add a new object to the type dictionary.   |
| [Remove(T)][3] | `bool`      | Remove an object from the type dictionary. |

[0]: ../../Heirloom.Core.md
[1]: IReadOnlyTypeDictionary[T].md
[2]: ITypeDictionary[T]/Add.md
[3]: ITypeDictionary[T]/Remove.md
