# IGraphEdge\<TKey>

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  
> **Namespace**: [Heirloom][0]  

An edge between two vertices.

```cs
public abstract interface IGraphEdge<TKey>
```

--------------------------------------------------------------------------------

**Properties**: [Source][1], [Target][2], [Weight][3]

**Methods**: [GetOtherKey][4]

--------------------------------------------------------------------------------

## Properties

| Name        | Summary                            |
|-------------|------------------------------------|
| [Source][1] | The name/key of the source vertex. |
| [Target][2] | The name/key of the target vertex. |
| [Weight][3] | The cost/weight of this edge.      |

## Methods

| Name             | Summary                |
|------------------|------------------------|
| [GetOtherKey][4] | Returns the other key. |

[0]: ..\Heirloom.Core.md
[1]: Heirloom.IGraphEdge[TKey].Source.md
[2]: Heirloom.IGraphEdge[TKey].Target.md
[3]: Heirloom.IGraphEdge[TKey].Weight.md
[4]: Heirloom.IGraphEdge[TKey].GetOtherKey.md
