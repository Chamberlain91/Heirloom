# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  

## IReadOnlyTypeDictionary\<T>

> **Namespace**: [Heirloom][0]  

A read-only view of [ITypeDictionary\<T>][1] .

```cs
public abstract interface IReadOnlyTypeDictionary<T> : IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable
```

### Inherits

IReadOnlyCollection\<T>, IEnumerable\<T>, IEnumerable

#### Methods

[Contains][2], [ContainsType\<X>][3], [GetItemsByType\<X>][4]

## Methods

| Name                    | Summary                                                                       |
|-------------------------|-------------------------------------------------------------------------------|
| [Contains][2]           | Does this type dictionary contain this object?                                |
| [ContainsType\<X>][3]   | Does the dictionary contain any object that inherits from the specified type. |
| [GetItemsByType\<X>][4] | Enumerates any object that inherits from the specified type.                  |

[0]: ../../Heirloom.Core.md
[1]: ITypeDictionary[T].md
[2]: IReadOnlyTypeDictionary[T]/Contains.md
[3]: IReadOnlyTypeDictionary[T]/ContainsType[X].md
[4]: IReadOnlyTypeDictionary[T]/GetItemsByType[X].md
