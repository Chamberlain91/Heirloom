# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## TextLayout (Class)

> **Namespace**: [Heirloom][0]

Utility to measure text and manually invoke the text layout function.   
 Internally used by [DrawText][1] and its variants.

```cs
public static class TextLayout
```

### Static Methods

[Measure][2], [PerformLayout][3]

## Methods

| Name                           | Return Type    | Summary                                                                |
|--------------------------------|----------------|------------------------------------------------------------------------|
| [Measure(string, Font, ...][2] | [Rectangle][4] | Computes the bounding box that the specified text will occupy withi... |
| [Measure(string, in Siz...][2] | [Rectangle][4] | Computes the bounding box that the specified text will occupy withi... |
| [Measure(string, in Rec...][2] | [Rectangle][4] | Computes the bounding box that the specified text will occupy withi... |
| [PerformLayout(string, ...][3] | `void`         | Performs the layout of text around the given position with the spec... |
| [PerformLayout(string, ...][3] | `void`         | Performs the layout of text within the given bounds with the specif... |

[0]: ../../Heirloom.Core.md
[1]: GraphicsContext/DrawText.md
[2]: TextLayout/Measure.md
[3]: TextLayout/PerformLayout.md
[4]: Rectangle.md
