# Heirloom.OpenGLES

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.OpenGLES](../Heirloom.OpenGLES/Heirloom.OpenGLES.md)</small>  

## ActiveUniform (Class)
<small>**Namespace**: Heirloom.OpenGLES</sub></small>  

| Fields                | Summary                                   |
|-----------------------|-------------------------------------------|
| [Size](#SIZ9C9392F9)  | Number of components of the uniform type. |
| [Type](#TYP233312DE)  | The type of uniform.                      |
| [Index](#IND6E2E1836) | Index of this uniform.                    |
| [Name](#NAM5943D12B)  | Name of this uniform.                     |
| [Offset](#OFF1FA8EDD) | Offset of this uniform within a block.    |

### Fields

#### <a name="SIZ9C9392F9"></a>Size : int
<small>`Read Only`</small>

Number of components of the uniform type.

#### <a name="TYP233312DE"></a>Type : [ActiveUniformType](Heirloom.OpenGLES.ActiveUniformType.md)
<small>`Read Only`</small>

The type of uniform.

#### <a name="IND6E2E1836"></a>Index : uint
<small>`Read Only`</small>

Index of this uniform.

#### <a name="NAM5943D12B"></a>Name : string
<small>`Read Only`</small>

Name of this uniform.

#### <a name="OFF1FA8EDD"></a>Offset : int
<small>`Read Only`</small>

Offset of this uniform within a block.

### Constructors

#### ActiveUniform(string name, uint index, int size, [ActiveUniformType](Heirloom.OpenGLES.ActiveUniformType.md) type, int offset)

