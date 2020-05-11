# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## SurfacePool.Request (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [SurfacePool][1]

### Request(int, int, MultisampleQuality)

Requests a temporary surface.

```cs
public static Surface Request(int width, int height, MultisampleQuality multisample = None)
```

| Name        | Type                    | Summary                                 |
|-------------|-------------------------|-----------------------------------------|
| width       | `int`                   | The width of the surface.               |
| height      | `int`                   | The height of the surface.              |
| multisample | [MultisampleQuality][2] | The multisample quality of the surface. |

> **Returns** - [Surface][3] - A surface owned by this pool.

### Request(IntSize, MultisampleQuality)

Requests a temporary surface.

```cs
public static Surface Request(IntSize size, MultisampleQuality multisample = None)
```

| Name        | Type                    | Summary                                 |
|-------------|-------------------------|-----------------------------------------|
| size        | [IntSize][4]            | The size of the surface.                |
| multisample | [MultisampleQuality][2] | The multisample quality of the surface. |

> **Returns** - [Surface][3] - A surface owned by this pool.

[0]: ../../../Heirloom.Core.md
[1]: ../SurfacePool.md
[2]: ../MultisampleQuality.md
[3]: ../Surface.md
[4]: ../IntSize.md
