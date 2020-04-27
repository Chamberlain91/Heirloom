# ISpatialCollection\<T>

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  
> **Namespace**: [Heirloom][0]  

A spatial collection to store and query elements in 2D space.

```cs
public abstract interface ISpatialCollection<T> : IReadOnlySpatialCollection<T>, ISpatialQuery<T>, IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable
```

--------------------------------------------------------------------------------

**Inherits**: [IReadOnlySpatialCollection\<T>][1], [ISpatialQuery\<T>][2], IReadOnlyCollection\<T>, IEnumerable\<T>, IEnumerable

**Methods**: [Clear][3], [Add][4], [Update][5], [Remove][6]

--------------------------------------------------------------------------------

## Methods

| Name        | Summary                                                             |
|-------------|---------------------------------------------------------------------|
| [Clear][3]  | Clears all elements from this spatial collection.                   |
| [Add][4]    | Adds an element with rectangle bounds into this spatial collection. |
| [Update][5] | Updates an exising element with new bounds in the collection.       |
| [Remove][6] | Removes an element from this spatial collection.                    |

[0]: ..\Heirloom.Core.md
[1]: Heirloom.IReadOnlySpatialCollection[T].md
[2]: Heirloom.ISpatialQuery[T].md
[3]: Heirloom.ISpatialCollection[T].Clear.md
[4]: Heirloom.ISpatialCollection[T].Add.md
[5]: Heirloom.ISpatialCollection[T].Update.md
[6]: Heirloom.ISpatialCollection[T].Remove.md
