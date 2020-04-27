# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]  

## TextLayout

> **Namespace**: [Heirloom][0]  

Utility to measure text and manually invoke the text layout function.   
 Internally used by [Graphics.DrawText][1] and its variants.

```cs
public static class TextLayout
```

#### Static Methods

[Measure][2], [PerformLayout][3]

## Methods

| Name               | Summary                                                                                                                         |
|--------------------|---------------------------------------------------------------------------------------------------------------------------------|
| [Measure][2]       | Computes the bounding box that the specified text will occupy within an infinite layout size.                                   |
| [Measure][2]       | Computes the bounding box that the specified text will occupy within the given layout size.                                     |
| [Measure][2]       | Computes the bounding box that the specified text will occupy within the given layout size.                                     |
| [PerformLayout][3] | Performs the layout of text around the given position with the specified font and size, invoking the callback at each location. |
| [PerformLayout][3] | Performs the layout of text within the given bounds with the specified font and size, invoking the callback at each location.   |

[0]: ../Heirloom.Core.md
[1]: Heirloom.Graphics.DrawText.md
[2]: Heirloom.TextLayout.Measure.md
[3]: Heirloom.TextLayout.PerformLayout.md
