# Heirloom.OpenGLES

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.OpenGLES](../Heirloom.OpenGLES/Heirloom.OpenGLES.md)</small>  

## ActiveUniformBlock (Class)
<small>**Namespace**: Heirloom.OpenGLES</sub></small>  

| Fields                | Summary                                       |
|-----------------------|-----------------------------------------------|
| [Name](#NAME5943)     | Name of this block.                           |
| [Index](#INDE6E2E)    | Index of this block.                          |
| [DataSize](#DATAD0CC) | Size in bytes of this block.                  |
| [Uniforms](#UNIF9C71) | Mapping of active uniforms within this block. |

### Fields

#### <a name="NAME5943"></a> Name : string
<small>`Read Only`</small>

Name of this block.

#### <a name="INDE6E2E"></a> Index : uint
<small>`Read Only`</small>

Index of this block.

#### <a name="DATAD0CC"></a> DataSize : int
<small>`Read Only`</small>

Size in bytes of this block.

#### <a name="UNIF9C71"></a> Uniforms : IReadOnlyDictionary\<string|ActiveUniform>
<small>`Read Only`</small>

Mapping of active uniforms within this block.

### Constructors

#### ActiveUniformBlock(string name, uint index, int dataSize, [ActiveUniform[]](Heirloom.OpenGLES.ActiveUniform.md) blockUniforms)

