# Heirloom.OpenGLES

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.OpenGLES](../Heirloom.OpenGLES/Heirloom.OpenGLES.md)</small>  

## ActiveUniformBlock (Class)
<small>**Namespace**: Heirloom.OpenGLES</sub></small>  

| Fields | Summary |
|-------|---------|
| [Name](#NAM5943D12B) | Name of this block. |
| [Index](#IND6E2E1836) | Index of this block. |
| [DataSize](#DATD0CC5249) | Size in bytes of this block. |
| [Uniforms](#UNI9C71E6B7) | Mapping of active uniforms within this block. |

### Fields

#### Name : string
<small>`Read Only`</small>

Name of this block.

#### Index : uint
<small>`Read Only`</small>

Index of this block.

#### DataSize : int
<small>`Read Only`</small>

Size in bytes of this block.

#### Uniforms : IReadOnlyDictionary\<string|ActiveUniform>
<small>`Read Only`</small>

Mapping of active uniforms within this block.

### Constructors

#### ActiveUniformBlock(string name, uint index, int dataSize, [ActiveUniform[]](Heirloom.OpenGLES.ActiveUniform.md) blockUniforms)

