# Heirloom.Desktop

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Drawing](../Heirloom.Drawing/Heirloom.Drawing.md), [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.OpenGLES](../Heirloom.OpenGLES/Heirloom.OpenGLES.md)</small>  

## Window (Class)
<small>**Namespace**: Heirloom.Desktop</sub></small>  

| Properties                             | Summary                                                                                          |
|----------------------------------------|--------------------------------------------------------------------------------------------------|
| [Handle](#HANDF720)                    | Gets the handle to the underlying GLFW window.                                                   |
| [IsDisposed](#ISDI6187)                | Gets a value that determines if this window been disposed.                                       |
| [IsClosed](#ISCL5A0C)                  | Gets a value that determines if this window been closed.                                         |
| [HasTransparentFramebuffer](#HAST8E49) | Gets a value that determines if this window supports a transparent framebuffer.                  |
| [Multisample](#MULTD8F2)               | Gets the multisampling level configured on this window.                                          |
| [Graphics](#GRAPD884)                  | Gets the graphics context associated with this window.                                           |
| [IsVisible](#ISVI702E)                 | Gets a value that determines if the window is visible.                                           |
| [IsDecorated](#ISDEC15C)               | Gets a value that determines if the window is decorated.                                         |
| [IsResizable](#ISREBFA3)               | Gets a value that determines if the window be resized.                                           |
| [IsFloating](#ISFL24F5)                | Gets a value that determines if the window "always on top".                                      |
| [Title](#TITLA845)                     | Gets or set the window title text.                                                               |
| [Bounds](#BOUNBCFE)                    | Gets or sets the window bounds in screen units.                                                  |
| [Position](#POSIF46C)                  | Gets or sets the window position in screen coordinates.                                          |
| [Size](#SIZE9C93)                      | Gets or sets the window size in screen units.                                                    |
| [FramebufferSize](#FRAM3448)           | The size of the underlying framebuffer in pixels.                                                |
| [ContentScale](#CONT84D7)              | Gets the content scaling factor.                                                                 |
| [State](#STAT7C34)                     | Gets the current state of the window.                                                            |
| [Monitor](#MONI81AB)                   | Gets the monitor this window is positioned on by checking the center point of the window bounds. |
| [Icons](#ICON3CE6)                     | Gets this windows icon set.                                                                      |

| Events                           | Summary |
|----------------------------------|---------|
| [Resized](#RESICAAB)             |         |
| [FramebufferResized](#FRAM76DD)  |         |
| [ContentScaleChanged](#CONT39B2) |         |
| [KeyPress](#KEYP4F9D)            |         |
| [KeyRelease](#KEYRFF42)          |         |
| [KeyRepeat](#KEYRE9BB)           |         |
| [CharacterTyped](#CHAR3F6F)      |         |
| [MousePress](#MOUS5593)          |         |
| [MouseRelease](#MOUSC789)        |         |
| [MouseScroll](#MOUS48DD)         |         |
| [MouseMove](#MOUSA5BB)           |         |
| [Closing](#CLOS6E48)             |         |
| [Closed](#CLOS4624)              |         |

| Methods                            | Summary                                                                                                       |
|------------------------------------|---------------------------------------------------------------------------------------------------------------|
| [Dispose](#DISP8A0D)               |                                                                                                               |
| [Dispose](#DISP8A0D)               |                                                                                                               |
| [OnWindowResized](#ONWI697E)       |                                                                                                               |
| [OnFramebufferResized](#ONFR1CBA)  |                                                                                                               |
| [OnContentScaleChanged](#ONCO5E09) |                                                                                                               |
| [OnWindowMoved](#ONWI4981)         |                                                                                                               |
| [OnKeyPressed](#ONKED4A2)          |                                                                                                               |
| [OnCharTyped](#ONCH89C6)           |                                                                                                               |
| [OnMousePressed](#ONMO2CAD)        |                                                                                                               |
| [OnMouseMove](#ONMOFD91)           |                                                                                                               |
| [OnMouseScroll](#ONMO7AE8)         |                                                                                                               |
| [OnClosing](#ONCL1A4D)             |                                                                                                               |
| [OnClosed](#ONCLF3B8)              |                                                                                                               |
| [Show](#SHOW2463)                  |                                                                                                               |
| [Hide](#HIDE9C93)                  |                                                                                                               |
| [Close](#CLOSCBEC)                 |                                                                                                               |
| [Focus](#FOCU4C20)                 |                                                                                                               |
| [Maximize](#MAXIF775)              | Sets the window to a maximized state.                                                                         |
| [Minimize](#MINI79C5)              | Sets the window to a minimized state.                                                                         |
| [Restore](#REST4A7A)               | Sets the window to a default size state.                                                                      |
| [SetFullscreen](#SETF261B)         | Sets the window to fullscreen using the nearest monitor and existing video mode.                              |
| [SetFullscreen](#SETF261B)         | Sets the window to fullscreen using the nearest monitor and specified video mode.                             |
| [SetFullscreen](#SETF261B)         | Sets the window to fullscreen using the specified monitor and existing video mode.                            |
| [SetFullscreen](#SETF261B)         | Sets the window to fullscreen using the specified monitor and video mode.                                     |
| [MoveToCenter](#MOVE6753)          | Moves the window to the center of the nearest monitor.                                                        |
| [MoveToCenter](#MOVE6753)          | Moves the window to the center of the specified monitor.                                                      |
| [SetIcons](#SETI4A95)              | Assigns a set of icon images to the window (the image with the most desireable szie by the system is chosen). |
| [SetIcon](#SETIE37C)               | Assigns a new icon image to the window.                                                                       |
| [SetCursor](#SETC93B0)             | Changes the appearance of the cursor on this window.                                                          |
| [SetCursor](#SETC93B0)             | Changes the appearance of the cursor on this window.                                                          |
| [SetCursor](#SETC93B0)             | Changes the appearance of the cursor on this window.                                                          |

### Constructors

#### Window(string title, bool vsync = True, bool transparent = False)

Constructs a new window.

#### Window(string title, [MultisampleQuality](../Heirloom.Drawing/Heirloom.Drawing.MultisampleQuality.md) multisample, bool vsync = True, bool transparent = False)

Constructs a new window.

### Properties

#### <a name="HANDF720"></a> Handle : [WindowHandle](Heirloom.Desktop.WindowHandle.md)


Gets the handle to the underlying GLFW window.

#### <a name="ISDI6187"></a> IsDisposed : bool

<small>`Read Only`</small>

Gets a value that determines if this window been disposed.

#### <a name="ISCL5A0C"></a> IsClosed : bool

<small>`Read Only`</small>

Gets a value that determines if this window been closed.

#### <a name="HAST8E49"></a> HasTransparentFramebuffer : bool

<small>`Read Only`</small>

Gets a value that determines if this window supports a transparent framebuffer.

#### <a name="MULTD8F2"></a> Multisample : [MultisampleQuality](../Heirloom.Drawing/Heirloom.Drawing.MultisampleQuality.md)

<small>`Read Only`</small>

Gets the multisampling level configured on this window.

#### <a name="GRAPD884"></a> Graphics : [Graphics](../Heirloom.Drawing/Heirloom.Drawing.Graphics.md)

<small>`Read Only`</small>

Gets the graphics context associated with this window.

#### <a name="ISVI702E"></a> IsVisible : bool

<small>`Read Only`</small>

Gets a value that determines if the window is visible.

#### <a name="ISDEC15C"></a> IsDecorated : bool


Gets a value that determines if the window is decorated.

#### <a name="ISREBFA3"></a> IsResizable : bool


Gets a value that determines if the window be resized.

#### <a name="ISFL24F5"></a> IsFloating : bool


Gets a value that determines if the window "always on top".

#### <a name="TITLA845"></a> Title : string


Gets or set the window title text.

#### <a name="BOUNBCFE"></a> Bounds : [IntRectangle](../Heirloom.Math/Heirloom.Math.IntRectangle.md)


Gets or sets the window bounds in screen units.

#### <a name="POSIF46C"></a> Position : [IntVector](../Heirloom.Math/Heirloom.Math.IntVector.md)


Gets or sets the window position in screen coordinates.

#### <a name="SIZE9C93"></a> Size : [IntSize](../Heirloom.Math/Heirloom.Math.IntSize.md)


Gets or sets the window size in screen units.

#### <a name="FRAM3448"></a> FramebufferSize : [IntSize](../Heirloom.Math/Heirloom.Math.IntSize.md)

<small>`Read Only`</small>

The size of the underlying framebuffer in pixels.

#### <a name="CONT84D7"></a> ContentScale : [Vector](../Heirloom.Math/Heirloom.Math.Vector.md)

<small>`Read Only`</small>

Gets the content scaling factor.

#### <a name="STAT7C34"></a> State : [WindowState](Heirloom.Desktop.WindowState.md)

<small>`Read Only`</small>

Gets the current state of the window.

#### <a name="MONI81AB"></a> Monitor : [Monitor](Heirloom.Desktop.Monitor.md)

<small>`Read Only`</small>

Gets the monitor this window is positioned on by checking the center point of the window bounds.

#### <a name="ICON3CE6"></a> Icons : IReadOnlyList\<Image>

<small>`Read Only`</small>

Gets this windows icon set.

### Events

#### Resized
#### FramebufferResized
#### ContentScaleChanged
#### KeyPress
#### KeyRelease
#### KeyRepeat
#### CharacterTyped
#### MousePress
#### MouseRelease
#### MouseScroll
#### MouseMove
#### Closing
#### Closed
### Methods

#### <a name="DISPFDE7"></a> Dispose(bool disposeManaged) : void
<small>`Virtual`, `Protected`</small>


#### <a name="DISP4E62"></a> Dispose() : void

#### <a name="ONWI7DF3"></a> OnWindowResized(int w, int h) : void
<small>`Virtual`, `Protected`</small>


#### <a name="ONFRB8A8"></a> OnFramebufferResized(int w, int h) : void
<small>`Virtual`, `Protected`</small>


#### <a name="ONCO23CF"></a> OnContentScaleChanged(float xScale, float yScale) : void
<small>`Virtual`, `Protected`</small>


#### <a name="ONWI215E"></a> OnWindowMoved(int x, int y) : void
<small>`Virtual`, `Protected`</small>


#### <a name="ONKEA5B2"></a> OnKeyPressed([Key](Heirloom.Desktop.Key.md) key, int scanCode, [ButtonAction](Heirloom.Desktop.ButtonAction.md) action, [KeyModifiers](Heirloom.Desktop.KeyModifiers.md) modifiers) : void
<small>`Virtual`, `Protected`</small>


#### <a name="ONCH3693"></a> OnCharTyped([UnicodeCharacter](../Heirloom.Drawing/Heirloom.Drawing.UnicodeCharacter.md) character) : void
<small>`Virtual`, `Protected`</small>


#### <a name="ONMOEEB7"></a> OnMousePressed(int button, [ButtonAction](Heirloom.Desktop.ButtonAction.md) action, [KeyModifiers](Heirloom.Desktop.KeyModifiers.md) modifiers) : void
<small>`Virtual`, `Protected`</small>


#### <a name="ONMOB2BE"></a> OnMouseMove(float x, float y) : void
<small>`Virtual`, `Protected`</small>


#### <a name="ONMO7CAD"></a> OnMouseScroll(float x, float y) : void
<small>`Virtual`, `Protected`</small>


#### <a name="ONCL8411"></a> OnClosing() : bool
<small>`Virtual`, `Protected`</small>

#### <a name="ONCLC720"></a> OnClosed() : void
<small>`Virtual`, `Protected`</small>

#### <a name="SHOW1079"></a> Show() : void

#### <a name="HIDE3FBB"></a> Hide() : void

#### <a name="CLOS859B"></a> Close() : void

#### <a name="FOCU5146"></a> Focus() : void

#### <a name="MAXIEC7F"></a> Maximize() : void

Sets the window to a maximized state.

#### <a name="MINIDD27"></a> Minimize() : void

Sets the window to a minimized state.

#### <a name="RESTFDC2"></a> Restore() : void

Sets the window to a default size state.

#### <a name="SETFFFC8"></a> SetFullscreen() : void

Sets the window to fullscreen using the nearest monitor and existing video mode.

#### <a name="SETFFA03"></a> SetFullscreen([VideoMode](Heirloom.Desktop.VideoMode.md) mode) : void

Sets the window to fullscreen using the nearest monitor and specified video mode.


#### <a name="SETF7FFA"></a> SetFullscreen([Monitor](Heirloom.Desktop.Monitor.md) monitor) : void

Sets the window to fullscreen using the specified monitor and existing video mode.


#### <a name="SETF784B"></a> SetFullscreen([Monitor](Heirloom.Desktop.Monitor.md) monitor, [VideoMode](Heirloom.Desktop.VideoMode.md) mode) : void

Sets the window to fullscreen using the specified monitor and video mode.


#### <a name="MOVEB25E"></a> MoveToCenter() : void

Moves the window to the center of the nearest monitor.

#### <a name="MOVE2D6F"></a> MoveToCenter([Monitor](Heirloom.Desktop.Monitor.md) monitor) : void

Moves the window to the center of the specified monitor.


#### <a name="SETI2CD6"></a> SetIcons([Image[]](../Heirloom.Drawing/Heirloom.Drawing.Image.md) icons) : void

Assigns a set of icon images to the window (the image with the most desireable szie by the system is chosen).


#### <a name="SETI54FD"></a> SetIcon([Image](../Heirloom.Drawing/Heirloom.Drawing.Image.md) icon) : void

Assigns a new icon image to the window.


#### <a name="SETCE04E"></a> SetCursor([StandardCursor](Heirloom.Desktop.StandardCursor.md) cursor) : void

Changes the appearance of the cursor on this window.


#### <a name="SETCB32E"></a> SetCursor([Image](../Heirloom.Drawing/Heirloom.Drawing.Image.md) cursor) : void

Changes the appearance of the cursor on this window.


#### <a name="SETC691F"></a> SetCursor([Image](../Heirloom.Drawing/Heirloom.Drawing.Image.md) cursor, [IntVector](../Heirloom.Math/Heirloom.Math.IntVector.md) hotspot) : void

Changes the appearance of the cursor on this window.


