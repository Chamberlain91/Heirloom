# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## GraphicsContext.PerformanceMetrics (Class)

> **Namespace**: [Heirloom][0]

Contains information pertaining to draw performance.

```cs
sealed class GraphicsContext.PerformanceMetrics
```

### Properties

[BatchCount][1], [DrawCount][2], [FrameRate][3], [OverlayMode][4], [TriangleCount][5]

## Properties

#### Instance

| Name               | Type                        | Summary                                                                |
|--------------------|-----------------------------|------------------------------------------------------------------------|
| [BatchCount][1]    | [Statistics][6]             | Statistics of the number of batches.                                   |
| [DrawCount][2]     | [Statistics][6]             | Statistics of the number of 'things' drawn.                            |
| [FrameRate][3]     | [Statistics][6]             | Statistics of the frame rate.                                          |
| [OverlayMode][4]   | [PerformanceOverlayMode][7] | Gets or sets a value that will enable or disable drawing the perfor... |
| [TriangleCount][5] | [Statistics][6]             | Statistics of the number of triangles.                                 |

[0]: ../../Heirloom.Core.md
[1]: GraphicsContext.PerformanceMetrics/BatchCount.md
[2]: GraphicsContext.PerformanceMetrics/DrawCount.md
[3]: GraphicsContext.PerformanceMetrics/FrameRate.md
[4]: GraphicsContext.PerformanceMetrics/OverlayMode.md
[5]: GraphicsContext.PerformanceMetrics/TriangleCount.md
[6]: Statistics.md
[7]: PerformanceOverlayMode.md
