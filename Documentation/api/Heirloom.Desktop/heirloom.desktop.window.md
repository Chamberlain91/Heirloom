# Heirloom.Desktop

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Desktop](../heirloom.desktop/heirloom.desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Drawing](../Heirloom.Drawing/Heirloom.Drawing.md), [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.OpenGLES](../Heirloom.OpenGLES/Heirloom.OpenGLES.md)</small>  

## Window (Class)
<small>**Namespace**: Heirloom.Desktop</sub></small>  

| Properties | Summary |
|------------|---------|
| [Handle](#HANF7203C68) | Gets the handle to the underlying GLFW window. |
| [IsDisposed](#ISD61874DA9) | Gets a value that determines if this window been disposed. |
| [IsClosed](#ISC5A0C7626) | Gets a value that determines if this window been closed. |
| [HasTransparentFramebuffer](#HAS8E497DA9) | Gets a value that determines if this window supports a transparent framebuffer. |
| [Multisample](#MULD8F2787) | Gets the multisampling level configured on this window. |
| [Graphics](#GRAD884C619) | Gets the graphics context associated with this window. |
| [IsVisible](#ISV702E9010) | Gets a value that determines if the window is visible. |
| [IsDecorated](#ISDC15C19C1) | Gets a value that determines if the window is decorated. |
| [IsResizable](#ISRBFA3A4A3) | Gets a value that determines if the window be resized. |
| [IsFloating](#ISF24F55C8) | Gets a value that determines if the window "always on top". |
| [Title](#TITA8453900) | Gets or set the window title text. |
| [Bounds](#BOUBCFE829) | Gets or sets the window bounds in screen units. |
| [Position](#POSF46C3C91) | Gets or sets the window position in screen coordinates. |
| [Size](#SIZ9C9392F9) | Gets or sets the window size in screen units. |
| [FramebufferSize](#FRA3448CAE4) | The size of the underlying framebuffer in pixels. |
| [ContentScale](#CON84D7B879) | Gets the content scaling factor. |
| [State](#STA7C34464B) | Gets the current state of the window. |
| [Monitor](#MON81ABB7DC) | Gets the monitor this window is positioned on by checking the center point of the window bounds. |
| [Icons](#ICO3CE68C98) | Gets this windows icon set. |

| Events | Summary |
|--------|---------|
| [Resized](#RESCAAB4756) |  |
| [FramebufferResized](#FRA76DD8E63) |  |
| [ContentScaleChanged](#CON39B27FAB) |  |
| [KeyPress](#KEY4F9DD9D4) |  |
| [KeyRelease](#KEYFF4274E6) |  |
| [KeyRepeat](#KEYE9BBCBD2) |  |
| [CharacterTyped](#CHA3F6FF4D9) |  |
| [MousePress](#MOU55930A34) |  |
| [MouseRelease](#MOUC789DA) |  |
| [MouseScroll](#MOU48DDBDFC) |  |
| [MouseMove](#MOUA5BB8DC) |  |
| [Closing](#CLO6E48E3EB) |  |
| [Closed](#CLO4624E582) |  |

| Methods | Summary |
|---------|---------|
| [Dispose](#DISFDE72264) |  |
| [Dispose](#DIS4E62D250) |  |
| [OnWindowResized](#ONW7DF3BE07) |  |
| [OnFramebufferResized](#ONFB8A80CBE) |  |
| [OnContentScaleChanged](#ONC23CF4CE6) |  |
| [OnWindowMoved](#ONW215EF2BA) |  |
| [OnKeyPressed](#ONK3D3D1A7A) |  |
| [OnCharTyped](#ONC450734C9) |  |
| [OnMousePressed](#ONM1A9F978C) |  |
| [OnMouseMove](#ONMB2BE1B4B) |  |
| [OnMouseScroll](#ONM7CADC1BF) |  |
| [OnClosing](#ONC8411C28B) |  |
| [OnClosed](#ONCC72060AC) |  |
| [Show](#SHO1079DE88) |  |
| [Hide](#HID3FBBD103) |  |
| [Close](#CLO859B0F7D) |  |
| [Focus](#FOC5146D98B) |  |
| [Maximize](#MAXEC7F5085) | Sets the window to a maximized state. |
| [Minimize](#MINDD275FAB) | Sets the window to a minimized state. |
| [Restore](#RESFDC25B1B) | Sets the window to a default size state. |
| [SetFullscreen](#SETFFC84E20) | Sets the window to fullscreen using the nearest monitor and existing video mode. |
| [SetFullscreen](#SET6592968A) | Sets the window to fullscreen using the nearest monitor and specified video mode. |
| [SetFullscreen](#SET73D8AD83) | Sets the window to fullscreen using the specified monitor and existing video mode. |
| [SetFullscreen](#SETC80701F5) | Sets the window to fullscreen using the specified monitor and video mode. |
| [MoveToCenter](#MOVB25EA6CC) | Moves the window to the center of the nearest monitor. |
| [MoveToCenter](#MOV4AFD66BB) | Moves the window to the center of the specified monitor. |
| [SetIcons](#SET3F9A2C99) | Assigns a set of icon images to the window (the image with the most desireable szie by the system is chosen). |
| [SetIcon](#SET53C0F7C3) | Assigns a new icon image to the window. |
| [SetCursor](#SET9E642612) | Changes the appearance of the cursor on this window. |
| [SetCursor](#SET5B6D5C83) | Changes the appearance of the cursor on this window. |
| [SetCursor](#SET5C9F5E78) | Changes the appearance of the cursor on this window. |

### Constructors

#### Window(string title, bool vsync = True, bool transparent = False)

Constructs a new window.

#### Window(string title, [MultisampleQuality](../heirloom.drawing/heirloom.drawing.multisamplequality.md) multisample, bool vsync = True, bool transparent = False)

Constructs a new window.

### Properties

#### <a name="HANF7203C68"></a>Handle : [WindowHandle](heirloom.desktop.windowhandle.md)


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

#### <a name="MULD8F2787"></a>Multisample : [MultisampleQuality](../heirloom.drawing/heirloom.drawing.multisamplequality.md)

<small>`Read Only`</small>

Gets the multisampling level configured on this window.

#### <a name="GRAD884C619"></a>Graphics : [Graphics](../heirloom.drawing/heirloom.drawing.graphics.md)

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

#### <a name="BOUBCFE829"></a>Bounds : [IntRectangle](../heirloom.math/heirloom.math.intrectangle.md)


Gets or sets the window bounds in screen units.

#### <a name="POSF46C3C91"></a>Position : [IntVector](../heirloom.math/heirloom.math.intvector.md)


Gets or sets the window position in screen coordinates.

#### <a name="SIZ9C9392F9"></a>Size : [IntSize](../heirloom.math/heirloom.math.intsize.md)


Gets or sets the window size in screen units.

#### <a name="FRA3448CAE4"></a>FramebufferSize : [IntSize](../heirloom.math/heirloom.math.intsize.md)

<small>`Read Only`</small>

The size of the underlying framebuffer in pixels.

#### <a name="CON84D7B879"></a>ContentScale : [Vector](../heirloom.math/heirloom.math.vector.md)

<small>`Read Only`</small>

Gets the content scaling factor.

#### <a name="STA7C34464B"></a>State : [WindowState](heirloom.desktop.windowstate.md)

<small>`Read Only`</small>

Gets the current state of the window.

#### <a name="MON81ABB7DC"></a>Monitor : [Monitor](heirloom.desktop.monitor.md)

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


#### <a name="ONK3D3D1A7A"></a>OnKeyPressed([Key](heirloom.desktop.key.md) key, int scanCode, [ButtonAction](heirloom.desktop.buttonaction.md) action, [KeyModifiers](heirloom.desktop.keymodifiers.md) modifiers) : void

<small>`Virtual`, `Protected`</small>


#### <a name="ONC450734C9"></a>OnCharTyped([UnicodeCharacter](../heirloom.drawing/heirloom.drawing.unicodecharacter.md) character) : void

<small>`Virtual`, `Protected`</small>


#### <a name="ONM1A9F978C"></a>OnMousePressed(int button, [ButtonAction](heirloom.desktop.buttonaction.md) action, [KeyModifiers](heirloom.desktop.keymodifiers.md) modifiers) : void

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

#### <a name="SET6592968A"></a>SetFullscreen([VideoMode](heirloom.desktop.videomode.md) mode) : void


Sets the window to fullscreen using the nearest monitor and specified video mode.


#### <a name="SET73D8AD83"></a>SetFullscreen([Monitor](heirloom.desktop.monitor.md) monitor) : void


Sets the window to fullscreen using the specified monitor and existing video mode.


#### <a name="SETC80701F5"></a>SetFullscreen([Monitor](heirloom.desktop.monitor.md) monitor, [VideoMode](heirloom.desktop.videomode.md) mode) : void


Sets the window to fullscreen using the specified monitor and video mode.


#### <a name="MOVB25EA6CC"></a>MoveToCenter() : void


Moves the window to the center of the nearest monitor.

#### <a name="MOV4AFD66BB"></a>MoveToCenter([Monitor](heirloom.desktop.monitor.md) monitor) : void


Moves the window to the center of the specified monitor.


#### <a name="SET3F9A2C99"></a>SetIcons([Image[]](../heirloom.drawing/heirloom.drawing.image.md) icons) : void


Assigns a set of icon images to the window (the image with the most desireable szie by the system is chosen).


#### <a name="SET53C0F7C3"></a>SetIcon([Image](../heirloom.drawing/heirloom.drawing.image.md) icon) : void


Assigns a new icon image to the window.


#### <a name="SET9E642612"></a>SetCursor([StandardCursor](heirloom.desktop.standardcursor.md) cursor) : void


Changes the appearance of the cursor on this window.


#### <a name="SET5B6D5C83"></a>SetCursor([Image](../heirloom.drawing/heirloom.drawing.image.md) cursor) : void


Changes the appearance of the cursor on this window.


#### <a name="SET5C9F5E78"></a>SetCursor([Image](../heirloom.drawing/heirloom.drawing.image.md) cursor, [IntVector](../heirloom.math/heirloom.math.intvector.md) hotspot) : void


Changes the appearance of the cursor on this window.


