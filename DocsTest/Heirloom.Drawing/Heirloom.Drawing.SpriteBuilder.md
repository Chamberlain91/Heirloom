# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../Heirloom.Drawing/Heirloom.Drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## SpriteBuilder (Sealed Class)
<small>**Namespace**: Heirloom.Drawing</sub></small>  
<small>**Interfaces**: IEnumerable</small>  

Utility object for manually constructing a sprite and its animations from images.

| Methods | Summary |
|---------|---------|
| [Clear](#CLE4538C554) | Clears all frames and animations. |
| [Add](#ADD63BA3DA2) | Add a single image animation. |
| [Add](#ADDC795849B) | Add an animation from several images. |
| [Add](#ADDD4DD1480) | Adds a new animation to the builder from multiple images. |
| [Add](#ADD8E087869) | Adds a new animation to the builder from multiple images. |
| [Add](#ADD734B2382) | Adds a new animation to the builder from multiple images. |
| [CreateSprite](#CRE75443AD) | Create a sprite the current state of the builder. |

### Constructors

#### SpriteBuilder()

Construct a new [SpriteBuilder](Heirloom.Drawing.SpriteBuilder.md).

### Methods

#### <a name="CLE4538C554"></a>Clear() : void


Clears all frames and animations.

#### <a name="ADD63BA3DA2"></a>Add(string name, [Image](Heirloom.Drawing.Image.md) frame) : void


Add a single image animation.

<small>**name**: <param name="name">The animation name.</param>  
</small>
<small>**frame**: <param name="frame">Some image.</param>  
</small>

#### <a name="ADDC795849B"></a>Add(string name, float frameDelay, params [Image[]](Heirloom.Drawing.Image.md) frames) : void


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

#### <a name="ADD8E087869"></a>Add(string name, float frameDelay, [Sprite.Direction](Heirloom.Drawing.Sprite.Direction.md) direction, params [Image[]](Heirloom.Drawing.Image.md) frames) : void


Adds a new animation to the builder from multiple images.

<small>**name**: <param name="name">The animation name.</param>  
</small>
<small>**frameDelay**: <param name="frameDelay">The delay between frames in seconds.</param>  
</small>
<small>**direction**: <param name="direction">Which way the images are cycled.</param>  
</small>
<small>**frames**: <param name="frames">The image sequence to animate with.</param>  
</small>

#### <a name="ADD734B2382"></a>Add(string name, float frameDelay, [Sprite.Direction](Heirloom.Drawing.Sprite.Direction.md) direction, IEnumerable\<Image> frames) : void


Adds a new animation to the builder from multiple images.

<small>**name**: <param name="name">The animation name.</param>  
</small>
<small>**frameDelay**: <param name="frameDelay">The delay between frames in seconds.</param>  
</small>
<small>**direction**: <param name="direction">Which way the images are cycled.</param>  
</small>
<small>**frames**: <param name="frames">The image sequence to animate with.</param>  
</small>

#### <a name="CRE75443AD"></a>CreateSprite() : [Sprite](Heirloom.Drawing.Sprite.md)


Create a sprite the current state of the builder.

