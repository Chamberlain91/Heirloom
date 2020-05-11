# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## BvhSpatialCollection\<T> (Class)

> **Namespace**: [Heirloom.Collections][0]

A spatial collection to store and query elements in 2D space, implemented as a BVH style tree and has infinite bounds.

```cs
public sealed class BvhSpatialCollection<T> : ISpatialCollection<T>, IReadOnlySpatialCollection<T>, ISpatialQuery<T>, IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable
```

### Inherits

[ISpatialCollection\<T>][1], [IReadOnlySpatialCollection\<T>][2], [ISpatialQuery\<T>][3], IReadOnlyCollection\<T>, IEnumerable\<T>, IEnumerable

### Properties

[Count][4]

### Methods

[Add][5], [Clear][6], [Contains][7], [GetEnumerator][8], [Query][9], [Remove][10], [Update][11]

## Properties

#### Instance

| Name       | Type  | Summary                                                |
|------------|-------|--------------------------------------------------------|
| [Count][4] | `int` | Gets the number of elements stored in this collection. |

## Methods

#### Instance

| Name                          | Return Type      | Summary                                                                |
|-------------------------------|------------------|------------------------------------------------------------------------|
| [Add(in T, in IShape)][5]     | `void`           | Adds an element with rectangle bounds into this spatial collection.    |
| [Clear()][6]                  | `void`           | Clears all elements from this spatial collection.                      |
| [Contains(in T)][7]           | `bool`           | Determines if the specified element exists in this collection.         |
| [GetEnumerator()][8]          | `IEnumerator<T>` |                                                                        |
| [Query(Vector)][9]            | `IEnumerable<T>` | Queries the spatial collection and returns the elements with bounds... |
| [Query(IShape)][9]            | `IEnumerable<T>` | Queries the spatial collection and returns the elements with bounds... |
| [Query(Ray, float)][9]        | `IEnumerable<T>` | Queries the spatial collection and returns the elements with bounds... |
| [Remove(in T)][10]            | `bool`           | Removes an element from this spatial collection.                       |
| [Update(in T, in IShape)][11] | `void`           | Updates an exising element with new bounds in the collection.          |

[0]: ../../Heirloom.Core.md
[1]: ISpatialCollection[T].md
[2]: IReadOnlySpatialCollection[T].md
[3]: ISpatialQuery[T].md
[4]: BvhSpatialCollection[T]/Count.md
[5]: BvhSpatialCollection[T]/Add.md
[6]: BvhSpatialCollection[T]/Clear.md
[7]: BvhSpatialCollection[T]/Contains.md
[8]: BvhSpatialCollection[T]/GetEnumerator.md
[9]: BvhSpatialCollection[T]/Query.md
[10]: BvhSpatialCollection[T]/Remove.md
[11]: BvhSpatialCollection[T]/Update.md
