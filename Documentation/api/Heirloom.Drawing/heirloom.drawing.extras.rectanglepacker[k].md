# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../heirloom.drawing/heirloom.drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## RectanglePacker\<K> (Sealed Class)
<small>**Namespace**: Heirloom.Drawing.Extras</sub></small>  

| Properties | Summary |
|------------|---------|
| [Bounds](#BOUBCFE829) | Gets the total bounds of the packed rectangles. |
| [Keys](#KEY3D37EC76) | Gets the identifiers of each packed rectangle. |

| Methods | Summary |
|---------|---------|
| [Insert](#INS6A48F93B) | Insert and pack a rectangle of the specified size. |
| [Contains](#CON5E610AA) | Does this packer have a rectangle with the specified key? |
| [GetRectangle](#GETCC5F191E) | Returns the packed rectangle for the specified key. |
| [Pack](#PAC4A100EC0) | Packs the given rectangle sizes and returns one-to-one ordering of their packed positions. |

### Constructors

#### RectanglePacker()

### Properties

#### <a name="BOUBCFE829"></a>Bounds : [IntRectangle](../heirloom.math/heirloom.math.intrectangle.md)

<small>`Read Only`</small>

Gets the total bounds of the packed rectangles.

#### <a name="KEY3D37EC76"></a>Keys : IEnumerable\<K>

<small>`Read Only`</small>

Gets the identifiers of each packed rectangle.

### Methods

#### <a name="INS6A48F93B"></a>Insert(K key, [IntSize](../heirloom.math/heirloom.math.intsize.md) size) : void


Insert and pack a rectangle of the specified size.

<small>**key**: <param name="key">Some key to identify the rectangle.</param>  
</small>
<small>**size**: <param name="size">The size of the rectangle to insert.</param>  
</small>

#### <a name="CON5E610AA"></a>Contains(K key) : bool


Does this packer have a rectangle with the specified key?


#### <a name="GETCC5F191E"></a>GetRectangle(K key) : [IntRectangle](../heirloom.math/heirloom.math.intrectangle.md)


Returns the packed rectangle for the specified key.


#### <a name="PAC4A100EC0"></a>Pack([IntSize[]](../heirloom.math/heirloom.math.intsize.md) rectangles) : [IntRectangle[]](../heirloom.math/heirloom.math.intrectangle.md)

<small>`Static`</small>

Packs the given rectangle sizes and returns one-to-one ordering of their packed positions.


