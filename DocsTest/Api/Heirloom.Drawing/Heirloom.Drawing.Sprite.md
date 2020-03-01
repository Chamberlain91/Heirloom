# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../Heirloom.Drawing/Heirloom.Drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## Sprite (Sealed Class)
<small>**Namespace**: Heirloom.Drawing</sub></small>  

A representation of single animated sprite. May also contains per-frame and animation sequence information for animating the sprite.

| Properties                    | Summary                                                   |
|-------------------------------|-----------------------------------------------------------|
| [Frames](#FRAM3895)           | Gets a read-only list of frames contained by this sprite. |
| [Animations](#ANIM1324)       | Gets the name of each known animation sequence.           |
| [DefaultAnimation](#DEFA62FA) | Gets the default animation.                               |

| Methods                   | Summary                             |
|---------------------------|-------------------------------------|
| [GetAnimation](#GETA5131) | Gets an animation sequence by name. |

### Constructors

#### Sprite([SpriteBuilder](Heirloom.Drawing.SpriteBuilder.md) builder)

#### Sprite(string path)

Constructs a new sprite from the specified file path resolved by `Heirloom.IO.Files.OpenStream(System.String)`.

#### Sprite(Stream stream)

Constructs a new sprite from a stream (ie, an Aseprite file or other supported format).

#### Sprite([Image](Heirloom.Drawing.Image.md) image)

Constructs a new sprite from a single image.

### Properties

#### <a name="FRAM3895"></a> Frames : IReadOnlyList\<Sprite.FrameInfo>

<small>`Read Only`</small>

Gets a read-only list of frames contained by this sprite.

#### <a name="ANIM1324"></a> Animations : IReadOnlyCollection\<string>

<small>`Read Only`</small>

Gets the name of each known animation sequence.

#### <a name="DEFA62FA"></a> DefaultAnimation : [Sprite.Animation](Heirloom.Drawing.Sprite.Animation.md)

<small>`Read Only`</small>

Gets the default animation.

### Methods

#### <a name="GETA6768"></a> GetAnimation(string name) : [Sprite.Animation](Heirloom.Drawing.Sprite.Animation.md)

Gets an animation sequence by name.


