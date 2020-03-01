# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../Heirloom.Drawing/Heirloom.Drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## Shader (Sealed Class)
<small>**Namespace**: Heirloom.Drawing</sub></small>  
<small>**Interfaces**: IDisposable</small>  

Provides GLSL shader support for custom image effects and other visual processing.

| Properties            | Summary                                                                             |
|-----------------------|-------------------------------------------------------------------------------------|
| [Name](#NAME5943)     | The name of the shader (composed from names of input files) for debugging purposes. |
| [Uniforms](#UNIF9C71) | Enumerates the uniforms defined in this shader.                                     |
| [Default](#DEFACF6E)  | Gets the default (ie, "no effect") shader.                                          |

| Methods                    | Summary                                     |
|----------------------------|---------------------------------------------|
| [SetUniform](#SETUFC59)    | Updates one of the shader uniforms by name. |
| [SetUniform](#SETUFC59)    | Updates one of the shader uniforms by name. |
| [SetUniform](#SETUFC59)    | Updates one of the shader uniforms by name. |
| [SetUniform](#SETUFC59)    | Updates one of the shader uniforms by name. |
| [SetUniform<T>](#SETUE7B4) | Updates one of the shader uniforms by name. |
| [SetUniform](#SETUFC59)    | Updates one of the shader uniforms by name. |
| [Dispose](#DISP8A0D)       |                                             |

### Constructors

#### Shader(params string paths)

Constructs a new shader from either a vertex shader (.vert), a fragment shader (.frag) or both.

### Properties

#### <a name="NAME5943"></a> Name : string

<small>`Read Only`</small>

The name of the shader (composed from names of input files) for debugging purposes.

#### <a name="UNIF9C71"></a> Uniforms : IEnumerable\<UniformInfo>

<small>`Read Only`</small>

Enumerates the uniforms defined in this shader.

#### <a name="DEFACF6E"></a> Default : [Shader](Heirloom.Drawing.Shader.md)

<small>`Static`, `Read Only`</small>

Gets the default (ie, "no effect") shader.

### Methods

#### <a name="SETU888E"></a> SetUniform(string name, float arr) : void

Updates one of the shader uniforms by name.

<small>**name**: <param name="name">The name of the uniform.</param></small>  

#### <a name="SETU62D3"></a> SetUniform(string name, int arr) : void

Updates one of the shader uniforms by name.

<small>**name**: <param name="name">The name of the uniform.</param></small>  

#### <a name="SETU7BB5"></a> SetUniform(string name, uint arr) : void

Updates one of the shader uniforms by name.

<small>**name**: <param name="name">The name of the uniform.</param></small>  

#### <a name="SETU5E63"></a> SetUniform(string name, bool arr) : void

Updates one of the shader uniforms by name.

<small>**name**: <param name="name">The name of the uniform.</param></small>  

#### <a name="SETUB88E"></a> SetUniform<T>(string name, T value) : void

Updates one of the shader uniforms by name.

<small>**name**: <param name="name">The name of the uniform.</param></small>  
<small>**value**: <param name="value">The value to assign to the uniform.</param></small>  

#### <a name="SETUDB9B"></a> SetUniform(string name, [ImageSource](Heirloom.Drawing.ImageSource.md) image) : void

Updates one of the shader uniforms by name.

<small>**name**: <param name="name">The name of the uniform.</param></small>  
<small>**image**: <param name="image">An image to assign to the uniform.</param></small>  

#### <a name="DISP4E62"></a> Dispose() : void
<small>`Virtual`</small>

