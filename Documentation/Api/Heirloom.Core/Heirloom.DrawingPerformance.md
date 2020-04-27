# DrawingPerformance

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  
> **Namespace**: [Heirloom][0]  

Contains information pertaining to draw performance.

```cs
public sealed class DrawingPerformance
```

--------------------------------------------------------------------------------

**Properties**: [OverlayMode][1], [BatchCount][2], [DrawCount][3], [TriangleCount][4], [FrameRate][5]

--------------------------------------------------------------------------------

## Constructors

### DrawingPerformance()

```cs
DrawingPerformance()
```

## Properties

| Name               | Summary                                                                           |
|--------------------|-----------------------------------------------------------------------------------|
| [OverlayMode][1]   | Gets or sets a value that will enable or disable drawing the performance overlay. |
| [BatchCount][2]    | Statistics of the number of batches.                                              |
| [DrawCount][3]     | Statistics of the number of 'things' drawn.                                       |
| [TriangleCount][4] | Statistics of the number of triangles.                                            |
| [FrameRate][5]     | Statistics of the frame rate.                                                     |

[0]: ..\Heirloom.Core.md
[1]: Heirloom.DrawingPerformance.OverlayMode.md
[2]: Heirloom.DrawingPerformance.BatchCount.md
[3]: Heirloom.DrawingPerformance.DrawCount.md
[4]: Heirloom.DrawingPerformance.TriangleCount.md
[5]: Heirloom.DrawingPerformance.FrameRate.md