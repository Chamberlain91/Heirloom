# Window.BeginFullscreen

> **Framework**: .NETStandard,Version=v2.1  
> **Assembly**: [Heirloom.Platforms.Desktop][0]  
> **Dependencies**: [Heirloom.Core][1], [Heirloom.OpenGLES][2], [Heirloom.MiniAudio][3]  
> **Namespace**: [Heirloom.Desktop][0]  
> **Type**: [Window][4]

--------------------------------------------------------------------------------

### BeginFullscreen()

Puts the window into fullscreen using the nearest monitor and existing video mode.

```cs
public void BeginFullscreen()
```

### BeginFullscreen(Monitor)

Sets the window to fullscreen using the specified monitor and existing video mode.

```cs
public void BeginFullscreen(Monitor monitor)
```

### BeginFullscreen(VideoMode)

Puts the window into fullscreen using the nearest monitor and specified video mode.

```cs
public void BeginFullscreen(VideoMode mode)
```

### BeginFullscreen(VideoMode, Monitor)

Sets the window to fullscreen using the specified monitor and video mode.

```cs
public void BeginFullscreen(VideoMode mode, Monitor monitor)
```

[0]: ../Heirloom.Platforms.Desktop.md
[1]: ../Heirloom.Core.md
[2]: ../Heirloom.OpenGLES.md
[3]: ../Heirloom.MiniAudio.md
[4]: Heirloom.Desktop.Window.md
