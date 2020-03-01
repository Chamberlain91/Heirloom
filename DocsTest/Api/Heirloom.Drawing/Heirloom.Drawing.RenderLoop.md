# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../Heirloom.Drawing/Heirloom.Drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## RenderLoop (Abstract Class)
<small>**Namespace**: Heirloom.Drawing</sub></small>  

Provides a thread to manage invoking a render/update function continuously.

| Properties                | Summary                             |
|---------------------------|-------------------------------------|
| [Graphics](#GRAD884C619)  | Gets the associated render context. |
| [IsRunning](#ISRECDE47CD) | Is the render thread active?        |

| Methods                | Summary                                                                                                            |
|------------------------|--------------------------------------------------------------------------------------------------------------------|
| [Update](#UPDD1771A75) |                                                                                                                    |
| [Start](#STAC1832F72)  | Start the render thread. This thread will automatically terminate when the associated graphics object is disposed. |
| [Stop](#STOB3037DBE)   | Stop the render thread.                                                                                            |
| [Create](#CRE31C4F336) | Creates a render loop instance from the given context and method reference.                                        |

### Constructors

#### RenderLoop([Graphics](Heirloom.Drawing.Graphics.md) graphics)

### Properties

#### <a name="GRAD884C619"></a>Graphics : [Graphics](Heirloom.Drawing.Graphics.md)

<small>`Read Only`</small>

Gets the associated render context.

#### <a name="ISRECDE47CD"></a>IsRunning : bool

<small>`Read Only`</small>

Is the render thread active?

### Methods

#### <a name="UPD833E6A6C"></a>Update([Graphics](Heirloom.Drawing.Graphics.md) gfx, float dt) : void
<small>`Abstract`, `Protected`</small>


#### <a name="STADBEC304F"></a>Start() : void

Start the render thread. This thread will automatically terminate when the associated graphics object is disposed.

#### <a name="STO4AE17E3B"></a>Stop() : void

Stop the render thread.

#### <a name="CREE4D21C3C"></a>Create([Graphics](Heirloom.Drawing.Graphics.md) gfx, [RenderLoop.UpdateFunction](Heirloom.Drawing.RenderLoop.UpdateFunction.md) update) : [RenderLoop](Heirloom.Drawing.RenderLoop.md)
<small>`Static`</small>

Creates a render loop instance from the given context and method reference.

<small>**gfx**: <param name="gfx">The relevant graphics context.</param></small>  
<small>**update**: <param name="update">The relevant update function.</param></small>  

