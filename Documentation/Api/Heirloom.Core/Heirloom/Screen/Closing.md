# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Screen.Closing (Event)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Screen][1]

#### Closing

Event called when the screen is trying to close. Returning `false` will prevent the screen from closing, if possible.

```cs
public Func<Screen, bool> Closing { add; remove; }
```

Type: `Func<Screen, bool>`

[0]: ../../../Heirloom.Core.md
[1]: ../Screen.md
