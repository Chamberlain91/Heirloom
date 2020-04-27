# Surface.Multisample

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  
> **Namespace**: [Heirloom][0]  
> **Type**: [Surface][1]

--------------------------------------------------------------------------------

### Multisample

Gets the multisampling quality set on this surface.

```cs
MultisampleQuality Multisample { get; }
```

This wll be set to the value actually availble used to create the surface. Some platforms might not support all multisample levels.

[0]: ../Heirloom.Core.md
[1]: Heirloom.Surface.md
