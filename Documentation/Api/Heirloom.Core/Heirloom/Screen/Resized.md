# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Screen.Resized (Event)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Screen][1]

#### Resized

Event called when the screen is resized. On certain platforms, the screen size and surface may not be equal.

```cs
public Action<Screen, IntSize> Resized { add; remove; }
```

Type: `Action<Screen, IntSize>`

**See Also:** `E:Heirloom.Screen.ContentScaleChanged`, [Surface][2]

[0]: ../../../Heirloom.Core.md
[1]: ../Screen.md
[2]: Surface.md
