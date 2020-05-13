# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## SparseGridSpatialCollection\<T> (Class)

> **Namespace**: [Heirloom.Collections][0]

Implements [ISpatialCollection\<T>][1] using a [SparseGrid\<T>][2] .

```cs
public sealed class SparseGridSpatialCollection<T> : ISpatialCollection<T>, IReadOnlySpatialCollection<T>, ISpatialQuery<T>, IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable
```

### Inherits

[ISpatialCollection\<T>][1], [IReadOnlySpatialCollection\<T>][3], [ISpatialQuery\<T>][4], IReadOnlyCollection\<T>, IEnumerable\<T>, IEnumerable

### Properties

[Count][5]

### Methods

[Add][6], [Clear][7], [Contains][8], [GetEnumerator][9], [Query][10], [Remove][11], [Update][12]

## Properties

#### Instance

| Name       | Type  | Summary |
|------------|-------|---------|
| [Count][5] | `int` |         |

## Methods

#### Instance

| Name                          | Return Type      | Summary                                                             |
|-------------------------------|------------------|---------------------------------------------------------------------|
| [Add(in T, in IShape)][6]     | `void`           | Adds an element with rectangle bounds into this spatial collection. |
| [Clear()][7]                  | `void`           | Clears all elements from this spatial collection.                   |
| [Contains(in T)][8]           | `bool`           | Determines if the specified element exists in this collection.      |
| [GetEnumerator()][9]          | `IEnumerator<T>` |                                                                     |
| [Query(Vector)][10]           | `IEnumerable<T>` | Finds spatial elements that overlap the specified point.            |
| [Query(IShape)][10]           | `IEnumerable<T>` | Finds spatial elements that overlap the specified rectangle.        |
| [Query(Ray, float)][10]       | `IEnumerable<T>` | Finds spatial elements that intersect the specified ray.            |
| [Remove(in T)][11]            | `bool`           | Removes an element from this spatial collection.                    |
| [Update(in T, in IShape)][12] | `void`           | Updates an exising element with new bounds in the collection.       |

[0]: ../../Heirloom.Core.md
[1]: ISpatialCollection[T].md
[2]: SparseGrid[T].md
[3]: IReadOnlySpatialCollection[T].md
[4]: ISpatialQuery[T].md
[5]: SparseGridSpatialCollection[T]/Count.md
[6]: SparseGridSpatialCollection[T]/Add.md
[7]: SparseGridSpatialCollection[T]/Clear.md
[8]: SparseGridSpatialCollection[T]/Contains.md
[9]: SparseGridSpatialCollection[T]/GetEnumerator.md
[10]: SparseGridSpatialCollection[T]/Query.md
[11]: SparseGridSpatialCollection[T]/Remove.md
[12]: SparseGridSpatialCollection[T]/Update.md
