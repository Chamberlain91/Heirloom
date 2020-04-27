# TypeDictionary\<T>

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  
> **Namespace**: [Heirloom][0]  

Manages objects by their type hierarchy up to the base type, allowing access and enumeration of objects by type.

```cs
public sealed class TypeDictionary<T> : ITypeDictionary<T>, IReadOnlyTypeDictionary<T>, IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable
```

--------------------------------------------------------------------------------

**Inherits**: [ITypeDictionary\<T>][1], [IReadOnlyTypeDictionary\<T>][2], IReadOnlyCollection\<T>, IEnumerable\<T>, IEnumerable

**Properties**: [Count][3]

**Methods**: [Add][4], [Remove][5], [Contains][6], [ContainsType\<X>][7], [GetItemsByType\<X>][8], [GetEnumerator][9]

--------------------------------------------------------------------------------

## Constructors

### TypeDictionary()

```cs
public TypeDictionary()
```

## Properties

| Name       | Summary |
|------------|---------|
| [Count][3] |         |

## Methods

| Name                    | Summary |
|-------------------------|---------|
| [Add][4]                |         |
| [Remove][5]             |         |
| [Contains][6]           |         |
| [ContainsType\<X>][7]   |         |
| [GetItemsByType\<X>][8] |         |
| [GetEnumerator][9]      |         |

[0]: ..\Heirloom.Core.md
[1]: Heirloom.ITypeDictionary[T].md
[2]: Heirloom.IReadOnlyTypeDictionary[T].md
[3]: Heirloom.TypeDictionary[T].Count.md
[4]: Heirloom.TypeDictionary[T].Add.md
[5]: Heirloom.TypeDictionary[T].Remove.md
[6]: Heirloom.TypeDictionary[T].Contains.md
[7]: Heirloom.TypeDictionary[T].ContainsType[X].md
[8]: Heirloom.TypeDictionary[T].GetItemsByType[X].md
[9]: Heirloom.TypeDictionary[T].GetEnumerator.md
