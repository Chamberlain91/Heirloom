# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## IGraphVertex\<TKey, TValue> (Interface)

> **Namespace**: [Heirloom][0]

A vertex representing a node on a graph.

```cs
public interface IGraphVertex<TKey, TValue>
```

### Properties

[Edges][1], [IncomingEdges][2], [Key][3], [Value][4]

## Properties

#### Instance

| Name               | Type                                | Summary                                                                |
|--------------------|-------------------------------------|------------------------------------------------------------------------|
| [Edges][1]         | `IReadOnlyList\<IGraphEdge\<TKey>>` | The list of outgoing edges in a directed graph ( all edges when und... |
| [IncomingEdges][2] | `IReadOnlyList\<IGraphEdge\<TKey>>` | The list incoming edges in a directed graph ( no edges when undirec... |
| [Key][3]           | `TKey`                              | The name/key of this vertex.                                           |
| [Value][4]         | `TValue`                            | The data/value of this vertex.                                         |

[0]: ../../Heirloom.Core.md
[1]: IGraphVertex[TKey,TValue]/Edges.md
[2]: IGraphVertex[TKey,TValue]/IncomingEdges.md
[3]: IGraphVertex[TKey,TValue]/Key.md
[4]: IGraphVertex[TKey,TValue]/Value.md
