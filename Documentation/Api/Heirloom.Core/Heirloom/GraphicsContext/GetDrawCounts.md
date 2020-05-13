# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## GraphicsContext.GetDrawCounts (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [GraphicsContext][1]

### GetDrawCounts()

Populates and returns drawing metrics.

```cs
protected abstract GraphicsContext.DrawCounts GetDrawCounts()
```

> **Returns** - [GraphicsContext.DrawCounts][2]

Counts should be reset in the implementation of `M:Heirloom.GraphicsContext.EndFrame` .

**See Also:** `M:Heirloom.GraphicsContext.EndFrame`

[0]: ../../../Heirloom.Core.md
[1]: ../GraphicsContext.md
[2]: ../GraphicsContext.DrawCounts.md
