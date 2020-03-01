# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../heirloom.drawing/heirloom.drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## Sprite (Sealed Class)
<small>**Namespace**: Heirloom.Drawing</sub></small>  

A representation of single animated sprite. May also contains per-frame and animation sequence information for animating the sprite.

| Properties | Summary |
|------------|---------|
| [Frames](#FRA38951E74) | Gets a read-only list of frames contained by this sprite. |
| [Animations](#ANI13247B31) | Gets the name of each known animation sequence. |
| [DefaultAnimation](#DEF62FAA24D) | Gets the default animation. |

| Methods | Summary |
|---------|---------|
| [GetAnimation](#GETFC5278AE) | Gets an animation sequence by name. |

### Constructors

#### Sprite([SpriteBuilder](heirloom.drawing.spritebuilder.md) builder)

#### Sprite(string path)

Constructs a new sprite from the specified file path resolved by `Heirloom.IO.Files.OpenStream(System.String)`.

#### Sprite(Stream stream)

Constructs a new sprite from a stream (ie, an Aseprite file or other supported format).

#### Sprite([Image](heirloom.drawing.image.md) image)

Constructs a new sprite from a single image.

### Properties

#### <a name="FRA38951E74"></a>Frames : IReadOnlyList\<Sprite.FrameInfo>

<small>`Read Only`</small>

Gets a read-only list of frames contained by this sprite.

#### <a name="ANI13247B31"></a>Animations : IReadOnlyCollection\<string>

<small>`Read Only`</small>

Gets the name of each known animation sequence.

#### <a name="DEF62FAA24D"></a>DefaultAnimation : [Sprite.Animation](heirloom.drawing.sprite.animation.md)

<small>`Read Only`</small>

Gets the default animation.

### Methods

#### <a name="GETFC5278AE"></a>GetAnimation(string name) : [Sprite.Animation](heirloom.drawing.sprite.animation.md)


Gets an animation sequence by name.


