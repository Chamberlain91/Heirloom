# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## SurfacePool (Class)

> **Namespace**: [Heirloom][0]

Provides a mechanism for requesting temporary surfaces and recycling them for reuse later.

```cs
public static class SurfacePool
```

### Static Methods

[Clean][1], [Recycle][2], [Request][3]

## Methods

| Name                           | Return Type  | Summary                                                                |
|--------------------------------|--------------|------------------------------------------------------------------------|
| [Clean()][1]                   | `void`       | Removes surfaces currently existing in the pool.                       |
| [Recycle(Surface)][2]          | `void`       | Recycle a surface back into the pool for reuse. It is assumed the s... |
| [Request(int, int, Mult...][3] | [Surface][4] | Requests a temporary surface.                                          |
| [Request(IntSize, Multi...][3] | [Surface][4] | Requests a temporary surface.                                          |

[0]: ../../Heirloom.Core.md
[1]: SurfacePool/Clean.md
[2]: SurfacePool/Recycle.md
[3]: SurfacePool/Request.md
[4]: Surface.md
