# Heirloom.Drawing

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.Drawing](../heirloom.drawing/heirloom.drawing.md)</small>  
<small>**Internals Visible To**: [Heirloom.Drawing.OpenGLES](../Heirloom.Drawing.OpenGLES/Heirloom.Drawing.OpenGLES.md), [Heirloom.Desktop](../Heirloom.Desktop/Heirloom.Desktop.md)</small>  
<small>**Dependancies**: [Heirloom.Math](../Heirloom.Math/Heirloom.Math.md), [Heirloom.IO](../Heirloom.IO/Heirloom.IO.md)</small>  

## Shader (Sealed Class)
<small>**Namespace**: Heirloom.Drawing</sub></small>  
<small>**Interfaces**: IDisposable</small>  

Provides GLSL shader support for custom image effects and other visual processing.

| Properties | Summary |
|------------|---------|
| [Name](#NAM5943D12B) | The name of the shader (composed from names of input files) for debugging purposes. |
| [Uniforms](#UNI9C71E6B7) | Enumerates the uniforms defined in this shader. |
| [Default](#DEFCF6EDD47) | Gets the default (ie, "no effect") shader. |

| Methods | Summary |
|---------|---------|
| [SetUniform](#SET888E387E) | Updates one of the shader uniforms by name. |
| [SetUniform](#SET62D3E459) | Updates one of the shader uniforms by name. |
| [SetUniform](#SET7BB5BD90) | Updates one of the shader uniforms by name. |
| [SetUniform](#SET5E636ABC) | Updates one of the shader uniforms by name. |
| [SetUniform<T>](#SETB88E5AFC) | Updates one of the shader uniforms by name. |
| [SetUniform](#SET8384AD4B) | Updates one of the shader uniforms by name. |
| [Dispose](#DIS4E62D250) |  |

### Constructors

#### Shader(params string paths)

Constructs a new shader from either a vertex shader (.vert), a fragment shader (.frag) or both.

### Properties

#### <a name="NAM5943D12B"></a>Name : string

<small>`Read Only`</small>

The name of the shader (composed from names of input files) for debugging purposes.

#### <a name="UNI9C71E6B7"></a>Uniforms : IEnumerable\<UniformInfo>

<small>`Read Only`</small>

Enumerates the uniforms defined in this shader.

#### <a name="DEFCF6EDD47"></a>Default : [Shader](heirloom.drawing.shader.md)

<small>`Static`, `Read Only`</small>

Gets the default (ie, "no effect") shader.

### Methods

#### <a name="SET888E387E"></a>SetUniform(string name, float arr) : void


Updates one of the shader uniforms by name.

<small>**name**: <param name="name">The name of the uniform.</param>  
</small>

#### <a name="SET62D3E459"></a>SetUniform(string name, int arr) : void


Updates one of the shader uniforms by name.

<small>**name**: <param name="name">The name of the uniform.</param>  
</small>

#### <a name="SET7BB5BD90"></a>SetUniform(string name, uint arr) : void


Updates one of the shader uniforms by name.

<small>**name**: <param name="name">The name of the uniform.</param>  
</small>

#### <a name="SET5E636ABC"></a>SetUniform(string name, bool arr) : void


Updates one of the shader uniforms by name.

<small>**name**: <param name="name">The name of the uniform.</param>  
</small>

#### <a name="SETB88E5AFC"></a>SetUniform<T>(string name, T value) : void


Updates one of the shader uniforms by name.

<small>**name**: <param name="name">The name of the uniform.</param>  
</small>
<small>**value**: <param name="value">The value to assign to the uniform.</param>  
</small>

#### <a name="SET8384AD4B"></a>SetUniform(string name, [ImageSource](heirloom.drawing.imagesource.md) image) : void


Updates one of the shader uniforms by name.

<small>**name**: <param name="name">The name of the uniform.</param>  
</small>
<small>**image**: <param name="image">An image to assign to the uniform.</param>  
</small>

#### <a name="DIS4E62D250"></a>Dispose() : void

<small>`Virtual`</small>

