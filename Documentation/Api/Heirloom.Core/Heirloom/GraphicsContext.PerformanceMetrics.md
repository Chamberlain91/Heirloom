# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## GraphicsContext.PerformanceMetrics (Class)

> **Namespace**: [Heirloom][0]

Contains information pertaining to draw performance.

```cs
sealed class GraphicsContext.PerformanceMetrics
```

**See Also:** [Performance][1]

### Properties

[BatchCount][2], [DrawCount][3], [FrameRate][4], [OverlayMode][5], [TriangleCount][6]

## Properties

#### Instance

| Name               | Type                        | Summary                                                                |
|--------------------|-----------------------------|------------------------------------------------------------------------|
| [BatchCount][2]    | [Statistics][7]             | Statistics of the number of batches.                                   |
| [DrawCount][3]     | [Statistics][7]             | Statistics of the number of 'things' drawn.                            |
| [FrameRate][4]     | [Statistics][7]             | Statistics of the frame rate.                                          |
| [OverlayMode][5]   | [PerformanceOverlayMode][8] | Gets or sets a value that will enable or disable drawing the perfor... |
| [TriangleCount][6] | [Statistics][7]             | Statistics of the number of triangles.                                 |

[0]: ../../Heirloom.Core.md
[1]: GraphicsContext/Performance.md
[2]: GraphicsContext.PerformanceMetrics/BatchCount.md
[3]: GraphicsContext.PerformanceMetrics/DrawCount.md
[4]: GraphicsContext.PerformanceMetrics/FrameRate.md
[5]: GraphicsContext.PerformanceMetrics/OverlayMode.md
[6]: GraphicsContext.PerformanceMetrics/TriangleCount.md
[7]: Statistics.md
[8]: PerformanceOverlayMode.md
