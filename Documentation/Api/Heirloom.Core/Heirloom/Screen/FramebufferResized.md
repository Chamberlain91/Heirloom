# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Screen.FramebufferResized (Event)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Screen][1]

#### FramebufferResized

Event called when the screen surface is resized. On certain platforms, the screen size and surface may not be equal.

```cs
public Action<Screen, IntSize> FramebufferResized { add; remove; }
```

Type: `Action<Screen, IntSize>`

[0]: ../../../Heirloom.Core.md
[1]: ../Screen.md
