# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Graphics.GetDrawCounts (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Graphics][1]

### GetDrawCounts()

Populates and returns drawing metrics.

```cs
protected abstract Graphics.DrawCounts GetDrawCounts()
```

> **Returns** - [Graphics.DrawCounts][2]

Counts should be reset in the implementation of `M:Heirloom.Graphics.EndFrame` .

[0]: ../../../Heirloom.Core.md
[1]: ../Graphics.md
[2]: ../Graphics.DrawCounts.md
