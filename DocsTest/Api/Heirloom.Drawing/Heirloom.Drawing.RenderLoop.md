# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../Heirloom.Drawing/Heirloom.Drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## RenderLoop (Abstract Class)
<small>**Namespace**: Heirloom.Drawing</sub></small>  

Provides a thread to manage invoking a render/update function continuously.

| Properties             | Summary                             |
|------------------------|-------------------------------------|
| [Graphics](#GRAPD884)  | Gets the associated render context. |
| [IsRunning](#ISRUECDE) | Is the render thread active?        |

| Methods             | Summary                                                                                                            |
|---------------------|--------------------------------------------------------------------------------------------------------------------|
| [Update](#UPDAD177) |                                                                                                                    |
| [Start](#STARC183)  | Start the render thread. This thread will automatically terminate when the associated graphics object is disposed. |
| [Stop](#STOPB303)   | Stop the render thread.                                                                                            |
| [Create](#CREA31C4) | Creates a render loop instance from the given context and method reference.                                        |

### Constructors

#### RenderLoop([Graphics](Heirloom.Drawing.Graphics.md) graphics)

### Properties

#### <a name="GRAPD884"></a> Graphics : [Graphics](Heirloom.Drawing.Graphics.md)

<small>`Read Only`</small>

Gets the associated render context.

#### <a name="ISRUECDE"></a> IsRunning : bool

<small>`Read Only`</small>

Is the render thread active?

### Methods

#### <a name="UPDA833E"></a> Update([Graphics](Heirloom.Drawing.Graphics.md) gfx, float dt) : void
<small>`Abstract`, `Protected`</small>


#### <a name="STARDBEC"></a> Start() : void

Start the render thread. This thread will automatically terminate when the associated graphics object is disposed.

#### <a name="STOP4AE1"></a> Stop() : void

Stop the render thread.

#### <a name="CREAE4D2"></a> Create([Graphics](Heirloom.Drawing.Graphics.md) gfx, [RenderLoop.UpdateFunction](Heirloom.Drawing.RenderLoop.UpdateFunction.md) update) : [RenderLoop](Heirloom.Drawing.RenderLoop.md)
<small>`Static`</small>

Creates a render loop instance from the given context and method reference.

<small>**gfx**: <param name="gfx">The relevant graphics context.</param></small>  
<small>**update**: <param name="update">The relevant update function.</param></small>  

