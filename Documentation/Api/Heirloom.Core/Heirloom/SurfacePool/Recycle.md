# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## SurfacePool.Recycle (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [SurfacePool][1]

### Recycle(Surface)

Recycle a surface back into the pool for reuse. It is assumed the surface is no longer used after this call.

```cs
public static void Recycle(Surface surface)
```

| Name    | Type         | Summary                          |
|---------|--------------|----------------------------------|
| surface | [Surface][2] | Some surface owned by this pool. |

> **Returns** - `void`

[0]: ../../../Heirloom.Core.md
[1]: ../SurfacePool.md
[2]: ../Surface.md
