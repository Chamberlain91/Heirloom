# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../Heirloom.Drawing/Heirloom.Drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## UniformInfo (Sealed Class)
<small>**Namespace**: Heirloom.Drawing</small>  

Contains information of a uniform from a [Shader](Heirloom.Drawing.Shader.md).

| Properties                 | Summary                         |
|----------------------------|---------------------------------|
| [Name](#NAM5943D12B)       | The name of this uniform.       |
| [Type](#TYP233312DE)       | The type of this uniform.       |
| [Dimensions](#DIMA3278683) | The dimensions of this uniform. |
| [ArraySize](#ARRE1891CFE)  | The array size of this uniform. |
| [IsVector](#ISV79DF967F)   | Is this uniform a vector?       |
| [IsMatrix](#ISM973708CD)   | Is this uniform a matrix?       |
| [IsArray](#ISAE8254ADF)    | Is this uniform an array?       |

### Constructors

#### UniformInfo(string name, [UniformType](Heirloom.Drawing.UniformType.md) type, [IntSize](../Heirloom.Math/Heirloom.Math.IntSize.md) dimensions, int arraySize)

### Properties

#### <a name="NAM5943D12B"></a>Name : string

<small>`Read Only`</small>

The name of this uniform.

#### <a name="TYP233312DE"></a>Type : [UniformType](Heirloom.Drawing.UniformType.md)

<small>`Read Only`</small>

The type of this uniform.

#### <a name="DIMA3278683"></a>Dimensions : [IntSize](../Heirloom.Math/Heirloom.Math.IntSize.md)

<small>`Read Only`</small>

The dimensions of this uniform.

#### <a name="ARRE1891CFE"></a>ArraySize : int

<small>`Read Only`</small>

The array size of this uniform.

#### <a name="ISV79DF967F"></a>IsVector : bool

<small>`Read Only`</small>

Is this uniform a vector?

#### <a name="ISM973708CD"></a>IsMatrix : bool

<small>`Read Only`</small>

Is this uniform a matrix?

#### <a name="ISAE8254ADF"></a>IsArray : bool

<small>`Read Only`</small>

Is this uniform an array?

