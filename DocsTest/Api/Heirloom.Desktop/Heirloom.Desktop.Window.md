# Heirloom.Desktop

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Drawing](../Heirloom.Drawing/Heirloom.Drawing.md), [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.OpenGLES](../Heirloom.OpenGLES/Heirloom.OpenGLES.md)</small>  

## Window (Class)
<small>**Namespace**: Heirloom.Desktop</sub></small>  

| Properties                                | Summary                                                                                          |
|-------------------------------------------|--------------------------------------------------------------------------------------------------|
| [Handle](#HANF7203C68)                    | Gets the handle to the underlying GLFW window.                                                   |
| [IsDisposed](#ISD61874DA9)                | Gets a value that determines if this window been disposed.                                       |
| [IsClosed](#ISC5A0C7626)                  | Gets a value that determines if this window been closed.                                         |
| [HasTransparentFramebuffer](#HAS8E497DA9) | Gets a value that determines if this window supports a transparent framebuffer.                  |
| [Multisample](#MULD8F2787)                | Gets the multisampling level configured on this window.                                          |
| [Graphics](#GRAD884C619)                  | Gets the graphics context associated with this window.                                           |
| [IsVisible](#ISV702E9010)                 | Gets a value that determines if the window is visible.                                           |
| [IsDecorated](#ISDC15C19C1)               | Gets a value that determines if the window is decorated.                                         |
| [IsResizable](#ISRBFA3A4A3)               | Gets a value that determines if the window be resized.                                           |
| [IsFloating](#ISF24F55C8)                 | Gets a value that determines if the window "always on top".                                      |
| [Title](#TITA8453900)                     | Gets or set the window title text.                                                               |
| [Bounds](#BOUBCFE829)                     | Gets or sets the window bounds in screen units.                                                  |
| [Position](#POSF46C3C91)                  | Gets or sets the window position in screen coordinates.                                          |
| [Size](#SIZ9C9392F9)                      | Gets or sets the window size in screen units.                                                    |
| [FramebufferSize](#FRA3448CAE4)           | The size of the underlying framebuffer in pixels.                                                |
| [ContentScale](#CON84D7B879)              | Gets the content scaling factor.                                                                 |
| [State](#STA7C34464B)                     | Gets the current state of the window.                                                            |
| [Monitor](#MON81ABB7DC)                   | Gets the monitor this window is positioned on by checking the center point of the window bounds. |
| [Icons](#ICO3CE68C98)                     | Gets this windows icon set.                                                                      |

| Events                              | Summary |
|-------------------------------------|---------|
| [Resized](#RESCAAB4756)             |         |
| [FramebufferResized](#FRA76DD8E63)  |         |
| [ContentScaleChanged](#CON39B27FAB) |         |
| [KeyPress](#KEY4F9DD9D4)            |         |
| [KeyRelease](#KEYFF4274E6)          |         |
| [KeyRepeat](#KEYE9BBCBD2)           |         |
| [CharacterTyped](#CHA3F6FF4D9)      |         |
| [MousePress](#MOU55930A34)          |         |
| [MouseRelease](#MOUC789DA)          |         |
| [MouseScroll](#MOU48DDBDFC)         |         |
| [MouseMove](#MOUA5BB8DC)            |         |
| [Closing](#CLO6E48E3EB)             |         |
| [Closed](#CLO4624E582)              |         |

| Methods                               | Summary                                                                                                       |
|---------------------------------------|---------------------------------------------------------------------------------------------------------------|
| [Dispose](#DIS8A0D80C3)               |                                                                                                               |
| [Dispose](#DIS8A0D80C3)               |                                                                                                               |
| [OnWindowResized](#ONW697E5F5B)       |                                                                                                               |
| [OnFramebufferResized](#ONF1CBA31F2)  |                                                                                                               |
| [OnContentScaleChanged](#ONC5E0948AA) |                                                                                                               |
| [OnWindowMoved](#ONW49815EEA)         |                                                                                                               |
| [OnKeyPressed](#ONKD4A20838)          |                                                                                                               |
| [OnCharTyped](#ONC89C6B529)           |                                                                                                               |
| [OnMousePressed](#ONM2CAD721C)        |                                                                                                               |
| [OnMouseMove](#ONMFD91C99)            |                                                                                                               |
| [OnMouseScroll](#ONM7AE82679)         |                                                                                                               |
| [OnClosing](#ONC1A4DA92A)             |                                                                                                               |
| [OnClosed](#ONCF3B87D33)              |                                                                                                               |
| [Show](#SHO2463681B)                  |                                                                                                               |
| [Hide](#HID9C939216)                  |                                                                                                               |
| [Close](#CLOCBEC0C4E)                 |                                                                                                               |
| [Focus](#FOC4C20A86E)                 |                                                                                                               |
| [Maximize](#MAXF775118C)              | Sets the window to a maximized state.                                                                         |
| [Minimize](#MIN79C535BE)              | Sets the window to a minimized state.                                                                         |
| [Restore](#RES4A7A846C)               | Sets the window to a default size state.                                                                      |
| [SetFullscreen](#SET261B2C99)         | Sets the window to fullscreen using the nearest monitor and existing video mode.                              |
| [SetFullscreen](#SET261B2C99)         | Sets the window to fullscreen using the nearest monitor and specified video mode.                             |
| [SetFullscreen](#SET261B2C99)         | Sets the window to fullscreen using the specified monitor and existing video mode.                            |
| [SetFullscreen](#SET261B2C99)         | Sets the window to fullscreen using the specified monitor and video mode.                                     |
| [MoveToCenter](#MOV67538631)          | Moves the window to the center of the nearest monitor.                                                        |
| [MoveToCenter](#MOV67538631)          | Moves the window to the center of the specified monitor.                                                      |
| [SetIcons](#SET4A959498)              | Assigns a set of icon images to the window (the image with the most desireable szie by the system is chosen). |
| [SetIcon](#SETE37C600D)               | Assigns a new icon image to the window.                                                                       |
| [SetCursor](#SET93B0CD6C)             | Changes the appearance of the cursor on this window.                                                          |
| [SetCursor](#SET93B0CD6C)             | Changes the appearance of the cursor on this window.                                                          |
| [SetCursor](#SET93B0CD6C)             | Changes the appearance of the cursor on this window.                                                          |

### Constructors

#### Window(string title, bool vsync = True, bool transparent = False)

Constructs a new window.

#### Window(string title, [MultisampleQuality](../Heirloom.Drawing/Heirloom.Drawing.MultisampleQuality.md) multisample, bool vsync = True, bool transparent = False)

Constructs a new window.

### Properties

#### <a name="HANF7203C68"></a>Handle : [WindowHandle](Heirloom.Desktop.WindowHandle.md)


Gets the handle to the underlying GLFW window.

#### <a name="ISD61874DA9"></a>IsDisposed : bool

<small>`Read Only`</small>

Gets a value that determines if this window been disposed.

#### <a name="ISC5A0C7626"></a>IsClosed : bool

<small>`Read Only`</small>

Gets a value that determines if this window been closed.

#### <a name="HAS8E497DA9"></a>HasTransparentFramebuffer : bool

<small>`Read Only`</small>

Gets a value that determines if this window supports a transparent framebuffer.

#### <a name="MULD8F2787"></a>Multisample : [MultisampleQuality](../Heirloom.Drawing/Heirloom.Drawing.MultisampleQuality.md)

<small>`Read Only`</small>

Gets the multisampling level configured on this window.

#### <a name="GRAD884C619"></a>Graphics : [Graphics](../Heirloom.Drawing/Heirloom.Drawing.Graphics.md)

<small>`Read Only`</small>

Gets the graphics context associated with this window.

#### <a name="ISV702E9010"></a>IsVisible : bool

<small>`Read Only`</small>

Gets a value that determines if the window is visible.

#### <a name="ISDC15C19C1"></a>IsDecorated : bool


Gets a value that determines if the window is decorated.

#### <a name="ISRBFA3A4A3"></a>IsResizable : bool


Gets a value that determines if the window be resized.

#### <a name="ISF24F55C8"></a>IsFloating : bool


Gets a value that determines if the window "always on top".

#### <a name="TITA8453900"></a>Title : string


Gets or set the window title text.

#### <a name="BOUBCFE829"></a>Bounds : [IntRectangle](../Heirloom.Math/Heirloom.Math.IntRectangle.md)


Gets or sets the window bounds in screen units.

#### <a name="POSF46C3C91"></a>Position : [IntVector](../Heirloom.Math/Heirloom.Math.IntVector.md)


Gets or sets the window position in screen coordinates.

#### <a name="SIZ9C9392F9"></a>Size : [IntSize](../Heirloom.Math/Heirloom.Math.IntSize.md)


Gets or sets the window size in screen units.

#### <a name="FRA3448CAE4"></a>FramebufferSize : [IntSize](../Heirloom.Math/Heirloom.Math.IntSize.md)

<small>`Read Only`</small>

The size of the underlying framebuffer in pixels.

#### <a name="CON84D7B879"></a>ContentScale : [Vector](../Heirloom.Math/Heirloom.Math.Vector.md)

<small>`Read Only`</small>

Gets the content scaling factor.

#### <a name="STA7C34464B"></a>State : [WindowState](Heirloom.Desktop.WindowState.md)

<small>`Read Only`</small>

Gets the current state of the window.

#### <a name="MON81ABB7DC"></a>Monitor : [Monitor](Heirloom.Desktop.Monitor.md)

<small>`Read Only`</small>

Gets the monitor this window is positioned on by checking the center point of the window bounds.

#### <a name="ICO3CE68C98"></a>Icons : IReadOnlyList\<Image>

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

#### <a name="DISFDE72264"></a>Dispose(bool disposeManaged) : void
<small>`Virtual`, `Protected`</small>


#### <a name="DIS4E62D250"></a>Dispose() : void

#### <a name="ONW7DF3BE07"></a>OnWindowResized(int w, int h) : void
<small>`Virtual`, `Protected`</small>


#### <a name="ONFB8A80CBE"></a>OnFramebufferResized(int w, int h) : void
<small>`Virtual`, `Protected`</small>


#### <a name="ONC23CF4CE6"></a>OnContentScaleChanged(float xScale, float yScale) : void
<small>`Virtual`, `Protected`</small>


#### <a name="ONW215EF2BA"></a>OnWindowMoved(int x, int y) : void
<small>`Virtual`, `Protected`</small>


#### <a name="ONKA5B288DA"></a>OnKeyPressed([Key](Heirloom.Desktop.Key.md) key, int scanCode, [ButtonAction](Heirloom.Desktop.ButtonAction.md) action, [KeyModifiers](Heirloom.Desktop.KeyModifiers.md) modifiers) : void
<small>`Virtual`, `Protected`</small>


#### <a name="ONC36938009"></a>OnCharTyped([UnicodeCharacter](../Heirloom.Drawing/Heirloom.Drawing.UnicodeCharacter.md) character) : void
<small>`Virtual`, `Protected`</small>


#### <a name="ONMEEB7474C"></a>OnMousePressed(int button, [ButtonAction](Heirloom.Desktop.ButtonAction.md) action, [KeyModifiers](Heirloom.Desktop.KeyModifiers.md) modifiers) : void
<small>`Virtual`, `Protected`</small>


#### <a name="ONMB2BE1B4B"></a>OnMouseMove(float x, float y) : void
<small>`Virtual`, `Protected`</small>


#### <a name="ONM7CADC1BF"></a>OnMouseScroll(float x, float y) : void
<small>`Virtual`, `Protected`</small>


#### <a name="ONC8411C28B"></a>OnClosing() : bool
<small>`Virtual`, `Protected`</small>

#### <a name="ONCC72060AC"></a>OnClosed() : void
<small>`Virtual`, `Protected`</small>

#### <a name="SHO1079DE88"></a>Show() : void

#### <a name="HID3FBBD103"></a>Hide() : void

#### <a name="CLO859B0F7D"></a>Close() : void

#### <a name="FOC5146D98B"></a>Focus() : void

#### <a name="MAXEC7F5085"></a>Maximize() : void

Sets the window to a maximized state.

#### <a name="MINDD275FAB"></a>Minimize() : void

Sets the window to a minimized state.

#### <a name="RESFDC25B1B"></a>Restore() : void

Sets the window to a default size state.

#### <a name="SETFFC84E20"></a>SetFullscreen() : void

Sets the window to fullscreen using the nearest monitor and existing video mode.

#### <a name="SETFA033BCA"></a>SetFullscreen([VideoMode](Heirloom.Desktop.VideoMode.md) mode) : void

Sets the window to fullscreen using the nearest monitor and specified video mode.


#### <a name="SET7FFA35E3"></a>SetFullscreen([Monitor](Heirloom.Desktop.Monitor.md) monitor) : void

Sets the window to fullscreen using the specified monitor and existing video mode.


#### <a name="SET784B1995"></a>SetFullscreen([Monitor](Heirloom.Desktop.Monitor.md) monitor, [VideoMode](Heirloom.Desktop.VideoMode.md) mode) : void

Sets the window to fullscreen using the specified monitor and video mode.


#### <a name="MOVB25EA6CC"></a>MoveToCenter() : void

Moves the window to the center of the nearest monitor.

#### <a name="MOV2D6F769B"></a>MoveToCenter([Monitor](Heirloom.Desktop.Monitor.md) monitor) : void

Moves the window to the center of the specified monitor.


#### <a name="SET2CD64DF9"></a>SetIcons([Image[]](../Heirloom.Drawing/Heirloom.Drawing.Image.md) icons) : void

Assigns a set of icon images to the window (the image with the most desireable szie by the system is chosen).


#### <a name="SET54FD11A3"></a>SetIcon([Image](../Heirloom.Drawing/Heirloom.Drawing.Image.md) icon) : void

Assigns a new icon image to the window.


#### <a name="SETE04E5BD2"></a>SetCursor([StandardCursor](Heirloom.Desktop.StandardCursor.md) cursor) : void

Changes the appearance of the cursor on this window.


#### <a name="SETB32EF7A3"></a>SetCursor([Image](../Heirloom.Drawing/Heirloom.Drawing.Image.md) cursor) : void

Changes the appearance of the cursor on this window.


#### <a name="SET691F1218"></a>SetCursor([Image](../Heirloom.Drawing/Heirloom.Drawing.Image.md) cursor, [IntVector](../Heirloom.Math/Heirloom.Math.IntVector.md) hotspot) : void

Changes the appearance of the cursor on this window.


