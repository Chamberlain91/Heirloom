# Heirloom.Desktop

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Desktop][0]

## Window.BeginFullscreen (Method)

> **Namespace**: [Heirloom.Desktop][0]  
> **Declaring Type**: [Window][1]

### BeginFullscreen()

Puts the window into fullscreen using the nearest monitor and existing video mode.

```cs
public void BeginFullscreen()
```

> **Returns** - `void`

### BeginFullscreen(Monitor)

Sets the window to fullscreen using the specified monitor and existing video mode.

```cs
public void BeginFullscreen(Monitor monitor)
```

| Name    | Type         | Summary |
|---------|--------------|---------|
| monitor | [Monitor][2] |         |

> **Returns** - `void`

### BeginFullscreen(VideoMode)

Puts the window into fullscreen using the nearest monitor and specified video mode.

```cs
public void BeginFullscreen(VideoMode mode)
```

| Name | Type           | Summary |
|------|----------------|---------|
| mode | [VideoMode][3] |         |

> **Returns** - `void`

### BeginFullscreen(VideoMode, Monitor)

Sets the window to fullscreen using the specified monitor and video mode.

```cs
public void BeginFullscreen(VideoMode mode, Monitor monitor)
```

| Name    | Type           | Summary |
|---------|----------------|---------|
| mode    | [VideoMode][3] |         |
| monitor | [Monitor][2]   |         |

> **Returns** - `void`

[0]: ../../../Heirloom.Desktop.md
[1]: ../Window.md
[2]: ../Monitor.md
[3]: ../VideoMode.md
