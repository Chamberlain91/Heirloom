# Heirloom.Drawing.OpenGLES

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing.OpenGLES](../heirloom.drawing.opengles/heirloom.drawing.opengles.md)</small>  
<small>**Dependancies**: [Heirloom.Drawing](../Heirloom.Drawing/Heirloom.Drawing.md), [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.OpenGLES](../Heirloom.OpenGLES/Heirloom.OpenGLES.md)</small>  

## RectanglePacker\<TElement> (Abstract Class)
<small>**Namespace**: Heirloom.Drawing.OpenGLES.Utilities</sub></small>  

| Properties | Summary |
|------------|---------|
| [Elements](#ELE97486ED1) |  |
| [Size](#SIZ9C9392F9) |  |

| Methods | Summary |
|---------|---------|
| [Clear](#CLE4538C554) |  |
| [Contains](#CONDA66F8F2) |  |
| [Add](#ADD48C1AB32) |  |
| [TryGetRectangle](#TRYDD957824) |  |
| [GetRectangle](#GETB4802E76) |  |
| [Insert](#INS96A3C4FA) |  |

### Constructors

#### RectanglePacker([IntSize](../heirloom.math/heirloom.math.intsize.md) size)

### Properties

#### <a name="ELE97486ED1"></a>Elements : IEnumerable\<TElement>

<small>`Read Only`</small>

#### <a name="SIZ9C9392F9"></a>Size : [IntSize](../heirloom.math/heirloom.math.intsize.md)

<small>`Read Only`</small>

### Methods

#### <a name="CLE4538C554"></a>Clear() : void

<small>`Virtual`</small>

#### <a name="CONDA66F8F2"></a>Contains(TElement element) : bool



#### <a name="ADD48C1AB32"></a>Add(TElement element, [IntSize](../heirloom.math/heirloom.math.intsize.md) itemSize) : bool



#### <a name="TRYDD957824"></a>TryGetRectangle(TElement element, out [IntRectangle](../heirloom.math/heirloom.math.intrectangle.md) rectangle) : bool



#### <a name="GETB4802E76"></a>GetRectangle(TElement element) : [IntRectangle](../heirloom.math/heirloom.math.intrectangle.md)



#### <a name="INS96A3C4FA"></a>Insert([IntSize](../heirloom.math/heirloom.math.intsize.md) size, out [IntRectangle](../heirloom.math/heirloom.math.intrectangle.md) rectangle) : bool

<small>`Abstract`, `Protected`</small>


