# Heirloom.OpenGLES

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.OpenGLES](../Heirloom.OpenGLES/Heirloom.OpenGLES.md)</small>  

## ActiveUniform (Class)
<small>**Namespace**: Heirloom.OpenGLES</sub></small>  

| Fields              | Summary                                   |
|---------------------|-------------------------------------------|
| [Size](#SIZE9C93)   | Number of components of the uniform type. |
| [Type](#TYPE2333)   | The type of uniform.                      |
| [Index](#INDE6E2E)  | Index of this uniform.                    |
| [Name](#NAME5943)   | Name of this uniform.                     |
| [Offset](#OFFS1FA8) | Offset of this uniform within a block.    |

### Fields

#### <a name="SIZE9C93"></a> Size : int
<small>`Read Only`</small>

Number of components of the uniform type.

#### <a name="TYPE2333"></a> Type : [ActiveUniformType](Heirloom.OpenGLES.ActiveUniformType.md)
<small>`Read Only`</small>

The type of uniform.

#### <a name="INDE6E2E"></a> Index : uint
<small>`Read Only`</small>

Index of this uniform.

#### <a name="NAME5943"></a> Name : string
<small>`Read Only`</small>

Name of this uniform.

#### <a name="OFFS1FA8"></a> Offset : int
<small>`Read Only`</small>

Offset of this uniform within a block.

### Constructors

#### ActiveUniform(string name, uint index, int size, [ActiveUniformType](Heirloom.OpenGLES.ActiveUniformType.md) type, int offset)

