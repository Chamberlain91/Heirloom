# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../Heirloom.Drawing/Heirloom.Drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## RectanglePacker\<K> (Sealed Class)
<small>**Namespace**: Heirloom.Drawing.Extras</sub></small>  

| Properties          | Summary                                         |
|---------------------|-------------------------------------------------|
| [Bounds](#BOUNBCFE) | Gets the total bounds of the packed rectangles. |
| [Keys](#KEYS3D37)   | Gets the identifiers of each packed rectangle.  |

| Methods                   | Summary                                                                                    |
|---------------------------|--------------------------------------------------------------------------------------------|
| [Insert](#INSEC7B1)       | Insert and pack a rectangle of the specified size.                                         |
| [Contains](#CONTD0AE)     | Does this packer have a rectangle with the specified key?                                  |
| [GetRectangle](#GETR3189) | Returns the packed rectangle for the specified key.                                        |
| [Pack](#PACK741B)         | Packs the given rectangle sizes and returns one-to-one ordering of their packed positions. |

### Constructors

#### RectanglePacker()

### Properties

#### <a name="BOUNBCFE"></a> Bounds : [IntRectangle](../Heirloom.Math/Heirloom.Math.IntRectangle.md)

<small>`Read Only`</small>

Gets the total bounds of the packed rectangles.

#### <a name="KEYS3D37"></a> Keys : IEnumerable\<K>

<small>`Read Only`</small>

Gets the identifiers of each packed rectangle.

### Methods

#### <a name="INSEA0D0"></a> Insert(K key, [IntSize](../Heirloom.Math/Heirloom.Math.IntSize.md) size) : void

Insert and pack a rectangle of the specified size.

<small>**key**: <param name="key">Some key to identify the rectangle.</param></small>  
<small>**size**: <param name="size">The size of the rectangle to insert.</param></small>  

#### <a name="CONT5E61"></a> Contains(K key) : bool

Does this packer have a rectangle with the specified key?


#### <a name="GETRA721"></a> GetRectangle(K key) : [IntRectangle](../Heirloom.Math/Heirloom.Math.IntRectangle.md)

Returns the packed rectangle for the specified key.


#### <a name="PACK1A0E"></a> Pack([IntSize[]](../Heirloom.Math/Heirloom.Math.IntSize.md) rectangles) : [IntRectangle[]](../Heirloom.Math/Heirloom.Math.IntRectangle.md)
<small>`Static`</small>

Packs the given rectangle sizes and returns one-to-one ordering of their packed positions.


