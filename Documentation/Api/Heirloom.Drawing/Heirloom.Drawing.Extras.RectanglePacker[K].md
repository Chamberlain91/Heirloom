# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../Heirloom.Drawing/Heirloom.Drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## RectanglePacker\<K> (Sealed Class)
<small>**Namespace**: Heirloom.Drawing.Extras</sub></small>  

| Properties            | Summary                                         |
|-----------------------|-------------------------------------------------|
| [Bounds](#BOUBCFE829) | Gets the total bounds of the packed rectangles. |
| [Keys](#KEY3D37EC76)  | Gets the identifiers of each packed rectangle.  |

| Methods                      | Summary                                                                                    |
|------------------------------|--------------------------------------------------------------------------------------------|
| [Insert](#INSA0D098FB)       | Insert and pack a rectangle of the specified size.                                         |
| [Contains](#CON5E610AA)      | Does this packer have a rectangle with the specified key?                                  |
| [GetRectangle](#GETA721B7DE) | Returns the packed rectangle for the specified key.                                        |
| [Pack](#PAC1A0E4940)         | Packs the given rectangle sizes and returns one-to-one ordering of their packed positions. |

### Constructors

#### RectanglePacker()

### Properties

#### <a name="BOUBCFE829"></a>Bounds : [IntRectangle](../Heirloom.Math/Heirloom.Math.IntRectangle.md)

<small>`Read Only`</small>

Gets the total bounds of the packed rectangles.

#### <a name="KEY3D37EC76"></a>Keys : IEnumerable\<K>

<small>`Read Only`</small>

Gets the identifiers of each packed rectangle.

### Methods

#### <a name="INSA0D098FB"></a>Insert(K key, [IntSize](../Heirloom.Math/Heirloom.Math.IntSize.md) size) : void

Insert and pack a rectangle of the specified size.

<small>**key**: <param name="key">Some key to identify the rectangle.</param></small>  
<small>**size**: <param name="size">The size of the rectangle to insert.</param></small>  

#### <a name="CON5E610AA"></a>Contains(K key) : bool

Does this packer have a rectangle with the specified key?


#### <a name="GETA721B7DE"></a>GetRectangle(K key) : [IntRectangle](../Heirloom.Math/Heirloom.Math.IntRectangle.md)

Returns the packed rectangle for the specified key.


#### <a name="PAC1A0E4940"></a>Pack([IntSize[]](../Heirloom.Math/Heirloom.Math.IntSize.md) rectangles) : [IntRectangle[]](../Heirloom.Math/Heirloom.Math.IntRectangle.md)
<small>`Static`</small>

Packs the given rectangle sizes and returns one-to-one ordering of their packed positions.


