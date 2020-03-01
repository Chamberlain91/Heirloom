# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../heirloom.drawing/heirloom.drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## SpriteBuilder (Sealed Class)
<small>**Namespace**: Heirloom.Drawing</sub></small>  
<small>**Interfaces**: IEnumerable</small>  

Utility object for manually constructing a sprite and its animations from images.

| Methods | Summary |
|---------|---------|
| [Clear](#CLE4538C554) | Clears all frames and animations. |
| [Add](#ADDF4BE1182) | Add a single image animation. |
| [Add](#ADD12A6553B) | Add an animation from several images. |
| [Add](#ADDD4DD1480) | Adds a new animation to the builder from multiple images. |
| [Add](#ADDD51D209) | Adds a new animation to the builder from multiple images. |
| [Add](#ADD8DE18042) | Adds a new animation to the builder from multiple images. |
| [CreateSprite](#CRE6C307B4D) | Create a sprite the current state of the builder. |

### Constructors

#### SpriteBuilder()

Construct a new [SpriteBuilder](heirloom.drawing.spritebuilder.md).

### Methods

#### <a name="CLE4538C554"></a>Clear() : void


Clears all frames and animations.

#### <a name="ADDF4BE1182"></a>Add(string name, [Image](heirloom.drawing.image.md) frame) : void


Add a single image animation.

<small>**name**: <param name="name">The animation name.</param>  
</small>
<small>**frame**: <param name="frame">Some image.</param>  
</small>

#### <a name="ADD12A6553B"></a>Add(string name, float frameDelay, params [Image[]](heirloom.drawing.image.md) frames) : void


Add an animation from several images.

<small>**name**: <param name="name">The animation name.</param>  
</small>
<small>**frameDelay**: <param name="frameDelay">The delay between frames in seconds.</param>  
</small>
<small>**frames**: <param name="frames">The image sequence to animate with.</param>  
</small>

#### <a name="ADDD4DD1480"></a>Add(string name, float frameDelay, IEnumerable\<Image> frames) : void


Adds a new animation to the builder from multiple images.

<small>**name**: <param name="name">The animation name.</param>  
</small>
<small>**frameDelay**: <param name="frameDelay">The delay between frames in seconds.</param>  
</small>
<small>**frames**: <param name="frames">The image sequence to animate with.</param>  
</small>

#### <a name="ADDD51D209"></a>Add(string name, float frameDelay, [Sprite.Direction](heirloom.drawing.sprite.direction.md) direction, params [Image[]](heirloom.drawing.image.md) frames) : void


Adds a new animation to the builder from multiple images.

<small>**name**: <param name="name">The animation name.</param>  
</small>
<small>**frameDelay**: <param name="frameDelay">The delay between frames in seconds.</param>  
</small>
<small>**direction**: <param name="direction">Which way the images are cycled.</param>  
</small>
<small>**frames**: <param name="frames">The image sequence to animate with.</param>  
</small>

#### <a name="ADD8DE18042"></a>Add(string name, float frameDelay, [Sprite.Direction](heirloom.drawing.sprite.direction.md) direction, IEnumerable\<Image> frames) : void


Adds a new animation to the builder from multiple images.

<small>**name**: <param name="name">The animation name.</param>  
</small>
<small>**frameDelay**: <param name="frameDelay">The delay between frames in seconds.</param>  
</small>
<small>**direction**: <param name="direction">Which way the images are cycled.</param>  
</small>
<small>**frames**: <param name="frames">The image sequence to animate with.</param>  
</small>

#### <a name="CRE6C307B4D"></a>CreateSprite() : [Sprite](heirloom.drawing.sprite.md)


Create a sprite the current state of the builder.

