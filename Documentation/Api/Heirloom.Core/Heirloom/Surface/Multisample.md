# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Surface.Multisample (Property)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Surface][1]

### Multisample

Gets the multisampling quality set on this surface.

```cs
public MultisampleQuality Multisample { get; }
```

> **Returns**: [MultisampleQuality][2]

This wll be set to the value actually availble used to create the surface. Some platforms might not support all multisample levels.

[0]: ../../../Heirloom.Core.md
[1]: ../Surface.md
[2]: ../MultisampleQuality.md
