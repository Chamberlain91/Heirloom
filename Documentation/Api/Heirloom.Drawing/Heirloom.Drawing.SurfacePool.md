# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../Heirloom.Drawing/Heirloom.Drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## SurfacePool (Static Class)
<small>**Namespace**: Heirloom.Drawing</small>  

Provides a mechanism for requesting temporary surfaces and recycling them for reuse later.

| Methods                 | Summary                                                                                                      |
|-------------------------|--------------------------------------------------------------------------------------------------------------|
| [Request](#REQC434E9CF) | Requests a temporary surface.                                                                                |
| [Request](#REQBC5CFB55) | Requests a temporary surface.                                                                                |
| [Recycle](#REC9468E0B0) | Recycle a surface back into the pool for reuse. It is assumed the surface is no longer used after this call. |
| [Clean](#CLE44E86438)   | Removes surfaces currently existing in the pool.                                                             |

### Methods

#### <a name="REQC434E9CF"></a>Request(int width, int height, [MultisampleQuality](Heirloom.Drawing.MultisampleQuality.md) multisample = None) : [Surface](Heirloom.Drawing.Surface.md)
<small>`Static`</small>

Requests a temporary surface.

<small>**width**: <param name="width">The width of the surface.</param></small>  
<small>**height**: <param name="height">The height of the surface.</param></small>  
<small>**multisample**: <param name="multisample">The multisample quality of the surface.</param></small>  

#### <a name="REQBC5CFB55"></a>Request([IntSize](../Heirloom.Math/Heirloom.Math.IntSize.md) size, [MultisampleQuality](Heirloom.Drawing.MultisampleQuality.md) multisample = None) : [Surface](Heirloom.Drawing.Surface.md)
<small>`Static`</small>

Requests a temporary surface.

<small>**size**: <param name="size">The size of the surface.</param></small>  
<small>**multisample**: <param name="multisample">The multisample quality of the surface.</param></small>  

#### <a name="REC9468E0B0"></a>Recycle([Surface](Heirloom.Drawing.Surface.md) surface) : void
<small>`Static`</small>

Recycle a surface back into the pool for reuse. It is assumed the surface is no longer used after this call.

<small>**surface**: <param name="surface">Some surface owned by this pool.</param></small>  

#### <a name="CLE44E86438"></a>Clean() : void
<small>`Static`</small>

Removes surfaces currently existing in the pool.

