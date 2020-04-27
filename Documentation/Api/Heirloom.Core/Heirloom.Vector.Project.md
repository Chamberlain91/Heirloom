# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  

## Vector.Project

> **Namespace**: [Heirloom][0]  
> **Type**: [Vector][1]  

### Project(in Vector, in Vector)

Projects the first vector onto the second.

```cs
public static float Project(in Vector u, in Vector v)
```

### Project(in Vector, in Vector, in Vector, bool)

Projects a point onto a line segment.

```cs
public static float Project(in Vector start, in Vector end, in Vector point, bool clamp = True)
```

[0]: ../Heirloom.Core.md
[1]: Heirloom.Vector.md
