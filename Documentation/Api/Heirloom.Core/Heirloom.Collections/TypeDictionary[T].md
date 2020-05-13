# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## TypeDictionary\<T> (Class)

> **Namespace**: [Heirloom.Collections][0]

Manages objects by their type hierarchy up to the base type, allowing access and enumeration of objects by type.

```cs
public sealed class TypeDictionary<T> : ITypeDictionary<T>, IReadOnlyTypeDictionary<T>, IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable
```

### Inherits

[ITypeDictionary\<T>][1], [IReadOnlyTypeDictionary\<T>][2], IReadOnlyCollection\<T>, IEnumerable\<T>, IEnumerable

### Properties

[Count][3]

### Methods

[Add][4], [Contains][5], [ContainsType\<X>][6], [GetEnumerator][7], [GetItemsByType\<X>][8], [Remove][9]

## Properties

#### Instance

| Name       | Type  | Summary |
|------------|-------|---------|
| [Count][3] | `int` |         |

## Methods

#### Instance

| Name                     | Return Type      | Summary                                                                |
|--------------------------|------------------|------------------------------------------------------------------------|
| [Add(T)][4]              | `bool`           | Add a new object to the type dictionary.                               |
| [Contains(T)][5]         | `bool`           | Does this type dictionary contain this object?                         |
| [ContainsType<X>()][6]   | `bool`           | Does the dictionary contain any object that inherits from the speci... |
| [GetEnumerator()][7]     | `IEnumerator<T>` |                                                                        |
| [GetItemsByType<X>()][8] | `IEnumerable<X>` | Enumerates any object that inherits from the specified type.           |
| [Remove(T)][9]           | `bool`           | Remove an object from the type dictionary.                             |

[0]: ../../Heirloom.Core.md
[1]: ITypeDictionary[T].md
[2]: IReadOnlyTypeDictionary[T].md
[3]: TypeDictionary[T]/Count.md
[4]: TypeDictionary[T]/Add.md
[5]: TypeDictionary[T]/Contains.md
[6]: TypeDictionary[T]/ContainsType[X].md
[7]: TypeDictionary[T]/GetEnumerator.md
[8]: TypeDictionary[T]/GetItemsByType[X].md
[9]: TypeDictionary[T]/Remove.md
