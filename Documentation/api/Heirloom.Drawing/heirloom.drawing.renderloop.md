# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../heirloom.drawing/heirloom.drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## RenderLoop (Abstract Class)
<small>**Namespace**: Heirloom.Drawing</sub></small>  

Provides a thread to manage invoking a render/update function continuously.

| Properties | Summary |
|------------|---------|
| [Graphics](#GRAD884C619) | Gets the associated render context. |
| [IsRunning](#ISRECDE47CD) | Is the render thread active? |

| Methods | Summary |
|---------|---------|
| [Update](#UPD29B0A88C) |  |
| [Start](#STADBEC304F) | Start the render thread. This thread will automatically terminate when the associated graphics object is disposed. |
| [Stop](#STO4AE17E3B) | Stop the render thread. |
| [Create](#CRE8781335C) | Creates a render loop instance from the given context and method reference. |

### Constructors

#### RenderLoop([Graphics](heirloom.drawing.graphics.md) graphics)

### Properties

#### <a name="GRAD884C619"></a>Graphics : [Graphics](heirloom.drawing.graphics.md)

<small>`Read Only`</small>

Gets the associated render context.

#### <a name="ISRECDE47CD"></a>IsRunning : bool

<small>`Read Only`</small>

Is the render thread active?

### Methods

#### <a name="UPD29B0A88C"></a>Update([Graphics](heirloom.drawing.graphics.md) gfx, float dt) : void

<small>`Abstract`, `Protected`</small>


#### <a name="STADBEC304F"></a>Start() : void


Start the render thread. This thread will automatically terminate when the associated graphics object is disposed.

#### <a name="STO4AE17E3B"></a>Stop() : void


Stop the render thread.

#### <a name="CRE8781335C"></a>Create([Graphics](heirloom.drawing.graphics.md) gfx, [RenderLoop.UpdateFunction](heirloom.drawing.renderloop.updatefunction.md) update) : [RenderLoop](heirloom.drawing.renderloop.md)

<small>`Static`</small>

Creates a render loop instance from the given context and method reference.

<small>**gfx**: <param name="gfx">The relevant graphics context.</param>  
</small>
<small>**update**: <param name="update">The relevant update function.</param>  
</small>

