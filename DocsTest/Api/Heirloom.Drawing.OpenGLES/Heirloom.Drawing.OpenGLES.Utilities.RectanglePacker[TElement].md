# Heirloom.Drawing.OpenGLES

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md)</small>  
<small>**Dependancies**: [Heirloom.Drawing](../Heirloom.Drawing/Heirloom.Drawing.md), [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.OpenGLES](../Heirloom.OpenGLES/Heirloom.OpenGLES.md)</small>  

## RectanglePacker\<TElement> (Abstract Class)
<small>**Namespace**: Heirloom.Drawing.OpenGLES.Utilities</sub></small>  

| Properties               | Summary |
|--------------------------|---------|
| [Elements](#ELE97486ED1) |         |
| [Size](#SIZ9C9392F9)     |         |

| Methods                        | Summary |
|--------------------------------|---------|
| [Clear](#CLE3BB23EF9)          |         |
| [Contains](#COND0AE797B)       |         |
| [Add](#ADDBCD0F225)            |         |
| [TryGetRectangle](#TRY2475D72) |         |
| [GetRectangle](#GET3189F53F)   |         |
| [Insert](#INSC7B161AF)         |         |

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


