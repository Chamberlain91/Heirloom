# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  

## TextLayout.PerformLayout

> **Namespace**: [Heirloom][0]  
> **Type**: [TextLayout][1]  

### PerformLayout(string, Vector, Font, int, TextAlign, TextLayoutCallback)

Performs the layout of text around the given position with the specified font and size, invoking the callback at each location.

```cs
public static void PerformLayout(string text, Vector position, Font font, int size, TextAlign align, TextLayoutCallback layoutCallback)
```

### PerformLayout(string, Rectangle, Font, int, TextAlign, TextLayoutCallback)

Performs the layout of text within the given bounds with the specified font and size, invoking the callback at each location.

```cs
public static void PerformLayout(string text, Rectangle bounds, Font font, int size, TextAlign align, TextLayoutCallback layoutCallback)
```

[0]: ../../../Heirloom.Core.md
[1]: ../TextLayout.md
