# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## Graphics.DrawText (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [Graphics][1]

### DrawText(StyledText, in Vector, Font, int, TextAlign)

Draws rich text to the current surface.

```cs
public void DrawText(StyledText styledText, in Vector position, Font font, int size, TextAlign align = Left)
```

| Name       | Type            | Summary                                    |
|------------|-----------------|--------------------------------------------|
| styledText | [StyledText][2] | The rich text to draw.                     |
| position   | [Vector][3]     | The anchor position to layout text around. |
| font       | [Font][4]       | The font to render with.                   |
| size       | `int`           | The font size to render with.              |
| align      | [TextAlign][5]  | The text alignment.                        |

> **Returns** - `void`

### DrawText(StyledText, in Rectangle, Font, int, TextAlign)

Draws rich text to the current surface.

```cs
public void DrawText(StyledText styledText, in Rectangle bounds, Font font, int size, TextAlign align = Left)
```

| Name       | Type            | Summary                            |
|------------|-----------------|------------------------------------|
| styledText | [StyledText][2] | The rich text to draw.             |
| bounds     | [Rectangle][6]  | The boundng region to layout text. |
| font       | [Font][4]       | The font to render with.           |
| size       | `int`           | The font size to render with.      |
| align      | [TextAlign][5]  | The text alignment.                |

> **Returns** - `void`

### DrawText(string, in Vector, Font, int, DrawTextCallback)

Draws text to the current surface.

```cs
public void DrawText(string text, in Vector position, Font font, int size, DrawTextCallback callback)
```

| Name     | Type                  | Summary                                                     |
|----------|-----------------------|-------------------------------------------------------------|
| text     | `string`              | The text to draw.                                           |
| position | [Vector][3]           | The anchor position to layout text around.                  |
| font     | [Font][4]             | The font to render with.                                    |
| size     | `int`                 | The font size to render with.                               |
| callback | [DrawTextCallback][7] | A callback for manipulating the style of the rendered text. |

> **Returns** - `void`

### DrawText(string, in Vector, Font, int, TextAlign, DrawTextCallback)

Draws text to the current surface.

```cs
public void DrawText(string text, in Vector position, Font font, int size, TextAlign align = Left, DrawTextCallback callback = null)
```

| Name     | Type                  | Summary                                                     |
|----------|-----------------------|-------------------------------------------------------------|
| text     | `string`              | The text to draw.                                           |
| position | [Vector][3]           | The anchor position to layout text around.                  |
| font     | [Font][4]             | The font to render with.                                    |
| size     | `int`                 | The font size to render with.                               |
| align    | [TextAlign][5]        | The text alignment.                                         |
| callback | [DrawTextCallback][7] | A callback for manipulating the style of the rendered text. |

> **Returns** - `void`

### DrawText(string, in Rectangle, Font, int, DrawTextCallback)

Draws text to the current surface.

```cs
public void DrawText(string text, in Rectangle bounds, Font font, int size, DrawTextCallback callback)
```

| Name     | Type                  | Summary                                                     |
|----------|-----------------------|-------------------------------------------------------------|
| text     | `string`              | The text to draw.                                           |
| bounds   | [Rectangle][6]        | The boundng region to layout text.                          |
| font     | [Font][4]             | The font to render with.                                    |
| size     | `int`                 | The font size to render with.                               |
| callback | [DrawTextCallback][7] | A callback for manipulating the style of the rendered text. |

> **Returns** - `void`

### DrawText(string, in Rectangle, Font, int, TextAlign, DrawTextCallback)

Draws text to the current surface.

```cs
public void DrawText(string text, in Rectangle bounds, Font font, int size, TextAlign align = Left, DrawTextCallback callback = null)
```

| Name     | Type                  | Summary                                                     |
|----------|-----------------------|-------------------------------------------------------------|
| text     | `string`              | The text to draw.                                           |
| bounds   | [Rectangle][6]        | The boundng region to layout text.                          |
| font     | [Font][4]             | The font to render with.                                    |
| size     | `int`                 | The font size to render with.                               |
| align    | [TextAlign][5]        | The text alignment.                                         |
| callback | [DrawTextCallback][7] | A callback for manipulating the style of the rendered text. |

> **Returns** - `void`

[0]: ../../../Heirloom.Core.md
[1]: ../Graphics.md
[2]: ../StyledText.md
[3]: ../Vector.md
[4]: ../Font.md
[5]: ../TextAlign.md
[6]: ../Rectangle.md
[7]: ../DrawTextCallback.md
