# Mesh.AddIndices

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  
> **Namespace**: [Heirloom][0]  
> **Type**: [Mesh][1]  

--------------------------------------------------------------------------------

### AddIndices(params int[])

```cs
public void AddIndices(params int[] indices)
```

### AddIndices(IEnumerable<int>)

Appends a triangle index to this mesh. Until [Clear][2] is called, this mesh becomes indexed.

```cs
public void AddIndices(IEnumerable<int> indices)
```

[0]: ..\Heirloom.Core.md
[1]: Heirloom.Mesh.md
[2]: Heirloom.Mesh.Clear.md
