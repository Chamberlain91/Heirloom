# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Mesh.AddTriangles (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Mesh][1]

### AddTriangles(params int[])

```cs
public void AddTriangles(params int[] indices)
```

| Name    | Type    | Summary |
|---------|---------|---------|
| indices | `int[]` |         |

> **Returns** - `void`

### AddTriangles(IReadOnlyList<int>)

Appends and defines multiple triangle faces to add to this mesh.

```cs
public void AddTriangles(IReadOnlyList<int> indices)
```

| Name    | Type                 | Summary |
|---------|----------------------|---------|
| indices | `IReadOnlyList<int>` |         |

> **Returns** - `void`

[0]: ../../../Heirloom.Core.md
[1]: ../Mesh.md
