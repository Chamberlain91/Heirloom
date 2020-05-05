# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## LinearSpatialCollection\<T> (Class)

> **Namespace**: [Heirloom.Collections][0]

DO NOT USE!   
 This is incredibly slow, but useful for behaviour testing against more complex implementions of [ISpatialCollection\<T>][1] .   
 It is effectively implemented as list of shapes and does not operate on any spatial structure.

```cs
public sealed class LinearSpatialCollection<T> : ISpatialCollection<T>, IReadOnlySpatialCollection<T>, ISpatialQuery<T>, IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable
```

### Inherits

[ISpatialCollection\<T>][1], [IReadOnlySpatialCollection\<T>][2], [ISpatialQuery\<T>][3], IReadOnlyCollection\<T>, IEnumerable\<T>, IEnumerable

### Properties

[Count][4]

### Methods

[Add][5], [Clear][6], [Contains][7], [GetEnumerator][8], [Query][9], [Remove][10], [Update][11]

## Properties

#### Instance

| Name       | Type  | Summary |
|------------|-------|---------|
| [Count][4] | `int` |         |

## Methods

#### Instance

| Name                          | Return Type      | Summary |
|-------------------------------|------------------|---------|
| [Add(in T, in IShape)][5]     | `void`           |         |
| [Clear()][6]                  | `void`           |         |
| [Contains(in T)][7]           | `bool`           |         |
| [GetEnumerator()][8]          | `IEnumerator<T>` |         |
| [Query(IShape)][9]            | `IEnumerable<T>` |         |
| [Query(Vector)][9]            | `IEnumerable<T>` |         |
| [Query(Ray, float)][9]        | `IEnumerable<T>` |         |
| [Remove(in T)][10]            | `bool`           |         |
| [Update(in T, in IShape)][11] | `void`           |         |

[0]: ../../Heirloom.Core.md
[1]: ISpatialCollection[T].md
[2]: IReadOnlySpatialCollection[T].md
[3]: ISpatialQuery[T].md
[4]: LinearSpatialCollection[T]/Count.md
[5]: LinearSpatialCollection[T]/Add.md
[6]: LinearSpatialCollection[T]/Clear.md
[7]: LinearSpatialCollection[T]/Contains.md
[8]: LinearSpatialCollection[T]/GetEnumerator.md
[9]: LinearSpatialCollection[T]/Query.md
[10]: LinearSpatialCollection[T]/Remove.md
[11]: LinearSpatialCollection[T]/Update.md
