# Heirloom.Platforms.Desktop

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Platforms.Desktop][0]

## Window.SetCursor (Method)

> **Namespace**: [Heirloom.Desktop][0]  
> **Declaring Type**: [Window][1]

### SetCursor(StandardCursor)

Changes the appearance of the cursor on this window.

```cs
public void SetCursor(StandardCursor cursor)
```

| Name   | Type                | Summary |
|--------|---------------------|---------|
| cursor | [StandardCursor][2] |         |

> **Returns** - `void`

### SetCursor(Image)

Changes the appearance of the cursor on this window.

```cs
public void SetCursor(Image cursor)
```

| Name   | Type       | Summary |
|--------|------------|---------|
| cursor | [Image][3] |         |

> **Returns** - `void`

### SetCursor(Image, IntVector)

Changes the appearance of the cursor on this window.

```cs
public void SetCursor(Image cursor, IntVector hotspot)
```

| Name    | Type           | Summary |
|---------|----------------|---------|
| cursor  | [Image][3]     |         |
| hotspot | [IntVector][4] |         |

> **Returns** - `void`

[0]: ../../../Heirloom.Platforms.Desktop.md
[1]: ../Window.md
[2]: ../StandardCursor.md
[3]: ../../../Heirloom.Core/Heirloom/Image.md
[4]: ../../../Heirloom.Core/Heirloom/IntVector.md
