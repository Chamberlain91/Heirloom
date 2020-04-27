# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  

## SurfacePool

> **Namespace**: [Heirloom][0]  

Provides a mechanism for requesting temporary surfaces and recycling them for reuse later.

```cs
public static class SurfacePool
```

#### Static Methods

[Request][1], [Recycle][2], [Clean][3]

## Methods

| Name         | Summary                                                                                                      |
|--------------|--------------------------------------------------------------------------------------------------------------|
| [Request][1] | Requests a temporary surface.                                                                                |
| [Request][1] | Requests a temporary surface.                                                                                |
| [Recycle][2] | Recycle a surface back into the pool for reuse. It is assumed the surface is no longer used after this call. |
| [Clean][3]   | Removes surfaces currently existing in the pool.                                                             |

[0]: ../Heirloom.Core.md
[1]: Heirloom.SurfacePool.Request.md
[2]: Heirloom.SurfacePool.Recycle.md
[3]: Heirloom.SurfacePool.Clean.md
