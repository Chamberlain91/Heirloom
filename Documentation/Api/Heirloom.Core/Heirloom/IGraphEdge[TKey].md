# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## IGraphEdge\<TKey> (Interface)

> **Namespace**: [Heirloom][0]

An edge between two vertices.

```cs
public interface IGraphEdge<TKey>
```

### Properties

[Source][1], [Target][2], [Weight][3]

### Methods

[GetOtherKey][4]

## Properties

#### Instance

| Name        | Type    | Summary                            |
|-------------|---------|------------------------------------|
| [Source][1] | `TKey`  | The name/key of the source vertex. |
| [Target][2] | `TKey`  | The name/key of the target vertex. |
| [Weight][3] | `float` | The cost/weight of this edge.      |

## Methods

#### Instance

| Name                   | Return Type | Summary                |
|------------------------|-------------|------------------------|
| [GetOtherKey(TKey)][4] | `TKey`      | Returns the other key. |

[0]: ../../Heirloom.Core.md
[1]: IGraphEdge[TKey]/Source.md
[2]: IGraphEdge[TKey]/Target.md
[3]: IGraphEdge[TKey]/Weight.md
[4]: IGraphEdge[TKey]/GetOtherKey.md
