# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Mesh.AddIndices (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Mesh][1]

### AddIndices(params int[])

```cs
public void AddIndices(params int[] indices)
```

| Name    | Type    | Summary |
|---------|---------|---------|
| indices | `int[]` |         |

> **Returns** - `void`

### AddIndices(IEnumerable<int>)

Appends a triangle index to this mesh. Until [Clear][2] is called, this mesh becomes indexed.

```cs
public void AddIndices(IEnumerable<int> indices)
```

| Name    | Type                | Summary |
|---------|---------------------|---------|
| indices | `IEnumerable\<int>` |         |

> **Returns** - `void`

[0]: ../../../Heirloom.Core.md
[1]: ../Mesh.md
[2]: Clear.md
