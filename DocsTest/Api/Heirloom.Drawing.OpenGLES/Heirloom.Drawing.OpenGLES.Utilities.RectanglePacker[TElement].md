# Heirloom.Drawing.OpenGLES

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md)</small>  
<small>**Dependancies**: [Heirloom.Drawing](../Heirloom.Drawing/Heirloom.Drawing.md), [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.OpenGLES](../Heirloom.OpenGLES/Heirloom.OpenGLES.md)</small>  

## RectanglePacker\<TElement> (Abstract Class)
<small>**Namespace**: Heirloom.Drawing.OpenGLES.Utilities</sub></small>  

| Properties            | Summary |
|-----------------------|---------|
| [Elements](#ELEM9748) |         |
| [Size](#SIZE9C93)     |         |

| Methods                      | Summary |
|------------------------------|---------|
| [Clear](#CLEA3BB2)           |         |
| [Contains](#CONTD0AE)        |         |
| [Add](#ADDBCD0)              |         |
| [TryGetRectangle](#TRYG2475) |         |
| [GetRectangle](#GETR3189)    |         |
| [Insert](#INSEC7B1)          |         |

### Constructors

#### RectanglePacker([IntSize](../Heirloom.Math/Heirloom.Math.IntSize.md) size)

### Properties

#### <a name="ELEM9748"></a> Elements : IEnumerable\<TElement>

<small>`Read Only`</small>

#### <a name="SIZE9C93"></a> Size : [IntSize](../Heirloom.Math/Heirloom.Math.IntSize.md)

<small>`Read Only`</small>

### Methods

#### <a name="CLEA4538"></a> Clear() : void
<small>`Virtual`</small>

#### <a name="CONTDA66"></a> Contains(TElement element) : bool


#### <a name="ADD(23C5"></a> Add(TElement element, [IntSize](../Heirloom.Math/Heirloom.Math.IntSize.md) itemSize) : bool


#### <a name="TRYG1EA4"></a> TryGetRectangle(TElement element, out [IntRectangle](../Heirloom.Math/Heirloom.Math.IntRectangle.md) rectangle) : bool


#### <a name="GETRF916"></a> GetRectangle(TElement element) : [IntRectangle](../Heirloom.Math/Heirloom.Math.IntRectangle.md)


#### <a name="INSEFD6B"></a> Insert([IntSize](../Heirloom.Math/Heirloom.Math.IntSize.md) size, out [IntRectangle](../Heirloom.Math/Heirloom.Math.IntRectangle.md) rectangle) : bool
<small>`Abstract`, `Protected`</small>


