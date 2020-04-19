# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../Heirloom.Drawing/Heirloom.Drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## RectanglePacker\<TElement> (Abstract Class)
<small>**Namespace**: Heirloom.Drawing.Utilities</small>  

| Properties               | Summary |
|--------------------------|---------|
| [Elements](#ELE97486ED1) |         |
| [Size](#SIZ9C9392F9)     |         |

| Methods                         | Summary |
|---------------------------------|---------|
| [Clear](#CLE4538C554)           |         |
| [Contains](#CONDA66F8F2)        |         |
| [Add](#ADD23C591B2)             |         |
| [TryGetRectangle](#TRY1EA416E4) |         |
| [GetRectangle](#GETF916A676)    |         |
| [Insert](#INSFD6B51FA)          |         |

### Constructors

#### RectanglePacker([IntSize](../Heirloom.Math/Heirloom.Math.IntSize.md) size)

### Properties

#### <a name="ELE97486ED1"></a>Elements : IEnumerable\<TElement>

<small>`Read Only`</small>

#### <a name="SIZ9C9392F9"></a>Size : [IntSize](../Heirloom.Math/Heirloom.Math.IntSize.md)

<small>`Read Only`</small>

### Methods

#### <a name="CLE4538C554"></a>Clear() : void
<small>`Virtual`</small>

#### <a name="CONDA66F8F2"></a>Contains(TElement element) : bool


#### <a name="ADD23C591B2"></a>Add(TElement element, [IntSize](../Heirloom.Math/Heirloom.Math.IntSize.md) itemSize) : bool


#### <a name="TRY1EA416E4"></a>TryGetRectangle(TElement element, out [IntRectangle](../Heirloom.Math/Heirloom.Math.IntRectangle.md) rectangle) : bool


#### <a name="GETF916A676"></a>GetRectangle(TElement element) : [IntRectangle](../Heirloom.Math/Heirloom.Math.IntRectangle.md)


#### <a name="INSFD6B51FA"></a>Insert([IntSize](../Heirloom.Math/Heirloom.Math.IntSize.md) size, out [IntRectangle](../Heirloom.Math/Heirloom.Math.IntRectangle.md) rectangle) : bool
<small>`Abstract`, `Protected`</small>


