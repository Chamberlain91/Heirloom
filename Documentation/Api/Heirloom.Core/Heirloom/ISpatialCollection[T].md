# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## ISpatialCollection\<T> (Interface)

> **Namespace**: [Heirloom][0]

A spatial collection to store and query elements in 2D space.

```cs
public interface ISpatialCollection<T> : IReadOnlySpatialCollection<T>, ISpatialQuery<T>, IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable
```

### Inherits

[IReadOnlySpatialCollection\<T>][1], [ISpatialQuery\<T>][2], IReadOnlyCollection\<T>, IEnumerable\<T>, IEnumerable

### Methods

[Add][3], [Clear][4], [Remove][5], [Update][6]

## Methods

#### Instance

| Name                         | Return Type | Summary                                                             |
|------------------------------|-------------|---------------------------------------------------------------------|
| [Add(in T, in IShape)][3]    | `void`      | Adds an element with rectangle bounds into this spatial collection. |
| [Clear()][4]                 | `void`      | Clears all elements from this spatial collection.                   |
| [Remove(in T)][5]            | `bool`      | Removes an element from this spatial collection.                    |
| [Update(in T, in IShape)][6] | `void`      | Updates an exising element with new bounds in the collection.       |

[0]: ../../Heirloom.Core.md
[1]: IReadOnlySpatialCollection[T].md
[2]: ISpatialQuery[T].md
[3]: ISpatialCollection[T]/Add.md
[4]: ISpatialCollection[T]/Clear.md
[5]: ISpatialCollection[T]/Remove.md
[6]: ISpatialCollection[T]/Update.md
