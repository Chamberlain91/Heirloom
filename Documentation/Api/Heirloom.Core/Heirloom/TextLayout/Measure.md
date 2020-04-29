# Heirloom.Core

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Core][0]

## TextLayout.Measure (Method)

> **Namespace**: [Heirloom][0]  
> **Declaring Type**: [TextLayout][1]

### Measure(string, Font, int)

Computes the bounding box that the specified text will occupy within an infinite layout size.

```cs
public static Rectangle Measure(string text, Font font, int fontSize)
```

| Name     | Type      | Summary                         |
|----------|-----------|---------------------------------|
| text     | `string`  | The text to layout and measure. |
| font     | [Font][2] |                                 |
| fontSize | `int`     | The font size to use.           |

> **Returns** - [Rectangle][3]

### Measure(string, in Size, Font, int)

Computes the bounding box that the specified text will occupy within the given layout size.

```cs
public static Rectangle Measure(string text, in Size layoutSize, Font font, int fontSize)
```

| Name       | Type      | Summary                         |
|------------|-----------|---------------------------------|
| text       | `string`  | The text to layout and measure. |
| layoutSize | [Size][4] | The size of the layout box.     |
| font       | [Font][2] |                                 |
| fontSize   | `int`     | The font size to use.           |

> **Returns** - [Rectangle][3]

### Measure(string, in Rectangle, Font, int)

Computes the bounding box that the specified text will occupy within the given layout size.

```cs
public static Rectangle Measure(string text, in Rectangle layoutBox, Font font, int fontSize)
```

| Name      | Type           | Summary                         |
|-----------|----------------|---------------------------------|
| text      | `string`       | The text to layout and measure. |
| layoutBox | [Rectangle][3] |                                 |
| font      | [Font][2]      |                                 |
| fontSize  | `int`          | The font size to use.           |

> **Returns** - [Rectangle][3]

[0]: ../../../Heirloom.Core.md
[1]: ../TextLayout.md
[2]: ../Font.md
[3]: ../Rectangle.md
[4]: ../Size.md
