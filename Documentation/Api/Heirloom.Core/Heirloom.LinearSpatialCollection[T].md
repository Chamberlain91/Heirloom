# LinearSpatialCollection\<T>

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  
> **Namespace**: [Heirloom][0]  

DO NOT USE!   
 This is incredibly slow, but useful for behaviour testing against more complex implementions of [ISpatialCollection\<T>][1] .   
 It is effectively implemented as list of shapes and does not operate on any spatial structure.

```cs
public sealed class LinearSpatialCollection<T> : ISpatialCollection<T>, IReadOnlySpatialCollection<T>, ISpatialQuery<T>, IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable
```

--------------------------------------------------------------------------------

**Inherits**: [ISpatialCollection\<T>][1], [IReadOnlySpatialCollection\<T>][2], [ISpatialQuery\<T>][3], IReadOnlyCollection\<T>, IEnumerable\<T>, IEnumerable

**Properties**: [Count][4]

**Methods**: [Clear][5], [Add][6], [Update][7], [Remove][8], [Contains][9], [Query][10], [GetEnumerator][11]

--------------------------------------------------------------------------------

## Properties

| Name       | Summary |
|------------|---------|
| [Count][4] |         |

## Methods

| Name                | Summary |
|---------------------|---------|
| [Clear][5]          |         |
| [Add][6]            |         |
| [Update][7]         |         |
| [Remove][8]         |         |
| [Contains][9]       |         |
| [Query][10]         |         |
| [Query][10]         |         |
| [Query][10]         |         |
| [GetEnumerator][11] |         |

[0]: ..\Heirloom.Core.md
[1]: Heirloom.ISpatialCollection[T].md
[2]: Heirloom.IReadOnlySpatialCollection[T].md
[3]: Heirloom.ISpatialQuery[T].md
[4]: Heirloom.LinearSpatialCollection[T].Count.md
[5]: Heirloom.LinearSpatialCollection[T].Clear.md
[6]: Heirloom.LinearSpatialCollection[T].Add.md
[7]: Heirloom.LinearSpatialCollection[T].Update.md
[8]: Heirloom.LinearSpatialCollection[T].Remove.md
[9]: Heirloom.LinearSpatialCollection[T].Contains.md
[10]: Heirloom.LinearSpatialCollection[T].Query.md
[11]: Heirloom.LinearSpatialCollection[T].GetEnumerator.md
