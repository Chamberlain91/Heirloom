# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  

## IGraphVertex\<TKey, TValue>

> **Namespace**: [Heirloom][0]  

A vertex representing a node on a graph.

```cs
public abstract interface IGraphVertex<TKey, TValue>
```

#### Properties

[Key][1], [Value][2], [IncomingEdges][3], [Edges][4]

## Properties

| Name               | Summary                                                                       |
|--------------------|-------------------------------------------------------------------------------|
| [Key][1]           | The name/key of this vertex.                                                  |
| [Value][2]         | The data/value of this vertex.                                                |
| [IncomingEdges][3] | The list incoming edges in a directed graph ( no edges when undirected ).     |
| [Edges][4]         | The list of outgoing edges in a directed graph ( all edges when undirected ). |

[0]: ../Heirloom.Core.md
[1]: Heirloom.IGraphVertex[TKey,TValue].Key.md
[2]: Heirloom.IGraphVertex[TKey,TValue].Value.md
[3]: Heirloom.IGraphVertex[TKey,TValue].IncomingEdges.md
[4]: Heirloom.IGraphVertex[TKey,TValue].Edges.md
