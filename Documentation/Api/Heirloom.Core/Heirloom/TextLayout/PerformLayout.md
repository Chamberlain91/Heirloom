# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## TextLayout.PerformLayout (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [TextLayout][1]

### PerformLayout(string, Vector, Font, int, TextAlign, TextLayoutCallback)

Performs the layout of text around the given position with the specified font and size, invoking the callback at each location.

```cs
public static void PerformLayout(string text, Vector position, Font font, int size, TextAlign align, TextLayoutCallback layoutCallback)
```

| Name           | Type                    | Summary |
|----------------|-------------------------|---------|
| text           | `string`                |         |
| position       | [Vector][2]             |         |
| font           | [Font][3]               |         |
| size           | `int`                   |         |
| align          | [TextAlign][4]          |         |
| layoutCallback | [TextLayoutCallback][5] |         |

> **Returns** - `void`

### PerformLayout(string, Rectangle, Font, int, TextAlign, TextLayoutCallback)

Performs the layout of text within the given bounds with the specified font and size, invoking the callback at each location.

```cs
public static void PerformLayout(string text, Rectangle bounds, Font font, int size, TextAlign align, TextLayoutCallback layoutCallback)
```

| Name           | Type                    | Summary |
|----------------|-------------------------|---------|
| text           | `string`                |         |
| bounds         | [Rectangle][6]          |         |
| font           | [Font][3]               |         |
| size           | `int`                   |         |
| align          | [TextAlign][4]          |         |
| layoutCallback | [TextLayoutCallback][5] |         |

> **Returns** - `void`

[0]: ../../../Heirloom.Core.md
[1]: ../TextLayout.md
[2]: ../Vector.md
[3]: ../Font.md
[4]: ../TextAlign.md
[5]: ../TextLayoutCallback.md
[6]: ../Rectangle.md
