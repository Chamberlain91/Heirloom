# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  

## BoundingTreeSpatialCollection\<T>

> **Namespace**: [Heirloom][0]  

A spatial collection to store and query elements in 2D space, implemented as a BVH style tree and has infinite bounds.

```cs
public sealed class BoundingTreeSpatialCollection<T> : ISpatialCollection<T>, IReadOnlySpatialCollection<T>, ISpatialQuery<T>, IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable
```

### Inherits

[ISpatialCollection\<T>][1], [IReadOnlySpatialCollection\<T>][2], [ISpatialQuery\<T>][3], IReadOnlyCollection\<T>, IEnumerable\<T>, IEnumerable

#### Properties

[Count][4]

#### Methods

[Clear][5], [Add][6], [Update][7], [Remove][8], [Contains][9], [Query][10], [GetEnumerator][11]

## Properties

| Name       | Summary                                                |
|------------|--------------------------------------------------------|
| [Count][4] | Gets the number of elements stored in this collection. |

## Methods

| Name                | Summary                                                                                                   |
|---------------------|-----------------------------------------------------------------------------------------------------------|
| [Clear][5]          | Clears all elements from this spatial collection.                                                         |
| [Add][6]            | Adds an element with rectangle bounds into this spatial collection.                                       |
| [Update][7]         | Updates an exising element with new bounds in the collection.                                             |
| [Remove][8]         | Removes an element from this spatial collection.                                                          |
| [Contains][9]       | Determines if the specified element exists in this collection.                                            |
| [Query][10]         | Queries the spatial collection and returns the elements with bounds that overlap the specified point.     |
| [Query][10]         | Queries the spatial collection and returns the elements with bounds that overlap the specified rectangle. |
| [Query][10]         | Queries the spatial collection and returns the elements with bounds that intersect the specified ray.     |
| [GetEnumerator][11] |                                                                                                           |

[0]: ../../Heirloom.Core.md
[1]: ISpatialCollection[T].md
[2]: IReadOnlySpatialCollection[T].md
[3]: ISpatialQuery[T].md
[4]: BoundingTreeSpatialCollection[T]/Count.md
[5]: BoundingTreeSpatialCollection[T]/Clear.md
[6]: BoundingTreeSpatialCollection[T]/Add.md
[7]: BoundingTreeSpatialCollection[T]/Update.md
[8]: BoundingTreeSpatialCollection[T]/Remove.md
[9]: BoundingTreeSpatialCollection[T]/Contains.md
[10]: BoundingTreeSpatialCollection[T]/Query.md
[11]: BoundingTreeSpatialCollection[T]/GetEnumerator.md
