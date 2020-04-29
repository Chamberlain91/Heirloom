# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Vector.Project (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Vector][1]

### Project(in Vector, in Vector)

Projects the first vector onto the second.

```cs
public static float Project(in Vector u, in Vector v)
```

| Name | Type        | Summary            |
|------|-------------|--------------------|
| u    | [Vector][1] | The first vector.  |
| v    | [Vector][1] | The second vector. |

> **Returns** - `float` - The 'progress' along `v` .

### Project(in Vector, in Vector, in Vector, bool)

Projects a point onto a line segment.

```cs
public static float Project(in Vector start, in Vector end, in Vector point, bool clamp = True)
```

| Name  | Type        | Summary                                          |
|-------|-------------|--------------------------------------------------|
| start | [Vector][1] | Starting point of the line segment.              |
| end   | [Vector][1] | Ending point of the line segment.                |
| point | [Vector][1] | Point to project.                                |
| clamp | `bool`      | Should we clamp to the ends of the line segment? |

> **Returns** - `float` - The 'progress' along the line segment.

[0]: ../../../Heirloom.Core.md
[1]: ../Vector.md
