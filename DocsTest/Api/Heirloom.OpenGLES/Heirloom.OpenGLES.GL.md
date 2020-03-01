# Heirloom.OpenGLES

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.OpenGLES](../Heirloom.OpenGLES/Heirloom.OpenGLES.md)</small>  

## GL (Static Class)
<small>**Namespace**: Heirloom.OpenGLES</sub></small>  

| Properties                      | Summary |
|---------------------------------|---------|
| [HasLoadedFunctions](#HASL3177) |         |

| Methods                                         | Summary                                                                                                  |
|-------------------------------------------------|----------------------------------------------------------------------------------------------------------|
| [WaitSync](#WAIT2CE5)                           |                                                                                                          |
| [DeleteSync](#DELECA1C)                         |                                                                                                          |
| [IsSync](#ISSY57D8)                             |                                                                                                          |
| [SetTextureParameter](#SETTD367)                |                                                                                                          |
| [SetTextureParameter](#SETTD367)                |                                                                                                          |
| [SetTextureParameter](#SETTD367)                |                                                                                                          |
| [SetTextureParameter](#SETTD367)                |                                                                                                          |
| [TexStorage2D](#TEXSB0F5)                       |                                                                                                          |
| [TexStorage3D](#TEXSB0F5)                       |                                                                                                          |
| [TexImage2D](#TEXI73C4)                         |                                                                                                          |
| [TexImage3D](#TEXI73C4)                         |                                                                                                          |
| [TexSubImage2D](#TEXSDD45)                      |                                                                                                          |
| [TexSubImage3D](#TEXS7FEC)                      |                                                                                                          |
| [CompressedTexImage2D](#COMP77B6)               |                                                                                                          |
| [CompressedTexImage3D](#COMP77B6)               |                                                                                                          |
| [CompressedTexSubImage2D](#COMPBC1B)            |                                                                                                          |
| [CompressedTexSubImage3D](#COMP1973)            |                                                                                                          |
| [TexImage2D<T>](#TEXI17F7)                      |                                                                                                          |
| [TexImage3D<T>](#TEXI17F7)                      |                                                                                                          |
| [TexSubImage2D<T>](#TEXSEDD6)                   |                                                                                                          |
| [TexSubImage3D<T>](#TEXSBDAF)                   |                                                                                                          |
| [GenerateMipmap](#GENE34C8)                     |                                                                                                          |
| [GenSampler](#GENS38E7)                         |                                                                                                          |
| [GenSamplers](#GENSCA32)                        |                                                                                                          |
| [DeleteSamplers](#DELEC25A)                     |                                                                                                          |
| [DeleteSampler](#DELE561D)                      |                                                                                                          |
| [DeleteSampler](#DELE561D)                      |                                                                                                          |
| [IsSampler](#ISSA4252)                          |                                                                                                          |
| [BindSampler](#BINDC0BD)                        |                                                                                                          |
| [GetSamplerParameters](#GETSBDC8)               |                                                                                                          |
| [GetSamplerParameters](#GETSBDC8)               |                                                                                                          |
| [SamplerParameter](#SAMP7C38)                   |                                                                                                          |
| [SamplerParameter](#SAMP7C38)                   |                                                                                                          |
| [SamplerParameter](#SAMP7C38)                   |                                                                                                          |
| [SamplerParameter](#SAMP7C38)                   |                                                                                                          |
| [Enable](#ENAB5319)                             |                                                                                                          |
| [Disable](#DISA130D)                            |                                                                                                          |
| [IsEnabled](#ISENCE12)                          |                                                                                                          |
| [Clear](#CLEA3BB2)                              | Clears the buffers of the currently bound framebuffer specified by the mask.                             |
| [SetViewport](#SETVA6E6)                        |                                                                                                          |
| [SetScissor](#SETS3023)                         |                                                                                                          |
| [FrontFace](#FRON1E7B)                          |                                                                                                          |
| [SetCullFace](#SETCC925)                        |                                                                                                          |
| [SetColorMask](#SETC1FF0)                       |                                                                                                          |
| [SetClearColor](#SETC9D41)                      |                                                                                                          |
| [SetClearColor](#SETC9D41)                      |                                                                                                          |
| [SetDepthMask](#SETD1FD0)                       |                                                                                                          |
| [SetDepthFunction](#SETD4D10)                   |                                                                                                          |
| [SetDepthRange](#SETDE9F5)                      |                                                                                                          |
| [SetClearDepth](#SETCCA1D)                      |                                                                                                          |
| [SetClearStencil](#SETC445B)                    |                                                                                                          |
| [SetLineWidth](#SETL6BCA)                       |                                                                                                          |
| [SetHint](#SETH8E8D)                            |                                                                                                          |
| [SetSampleCoverage](#SETSC78B)                  |                                                                                                          |
| [SetPolygonOffset](#SETPFF45)                   |                                                                                                          |
| [SetPixelStore](#SETPE856)                      |                                                                                                          |
| [SetReadBuffer](#SETR67B6)                      | Changes the read buffer ( where reading operations occur )                                               |
| [SetDrawBuffers](#SETDEF61)                     | Changes the read buffer ( where reading operations occur )                                               |
| [ReadPixels](#READFB48)                         | Reads a block of pixels from the frame buffer.                                                           |
| [ReadPixels](#READFB48)                         | Reads a block of pixels from the frame buffer.                                                           |
| [ReadPixels](#READFB48)                         | Reads a block of pixels from the frame buffer. ( ReadPixelsFormat.RGBA and ReadPixelsType.UnsignedByte ) |
| [Finish](#FINI4D10)                             |                                                                                                          |
| [Flush](#FLUSCBEB)                              |                                                                                                          |
| [SetBlendColor](#SETB4B40)                      |                                                                                                          |
| [SetBlendEquation](#SETB313E)                   |                                                                                                          |
| [SetBlendEquation](#SETB313E)                   |                                                                                                          |
| [SetBlendFunction](#SETBF0A6)                   |                                                                                                          |
| [SetBlendFunction](#SETBF0A6)                   |                                                                                                          |
| [StencilFunction](#STENB7C8)                    |                                                                                                          |
| [StencilFunction](#STENB7C8)                    |                                                                                                          |
| [SetStencilMask](#SETS78D0)                     |                                                                                                          |
| [SetStencilMask](#SETS78D0)                     |                                                                                                          |
| [StencilOperation](#STEN4698)                   |                                                                                                          |
| [StencilOperation](#STEN4698)                   |                                                                                                          |
| [GenQueries](#GENQE969)                         |                                                                                                          |
| [GenQuery](#GENQ786C)                           |                                                                                                          |
| [DeleteQueries](#DELE73D8)                      |                                                                                                          |
| [DeleteQuery](#DELEC41D)                        |                                                                                                          |
| [DeleteQuery](#DELEC41D)                        |                                                                                                          |
| [BeginQuery](#BEGIEB0C)                         |                                                                                                          |
| [EndQuery](#ENDQAEE9)                           |                                                                                                          |
| [GetQuery](#GETQ786D)                           |                                                                                                          |
| [GetQueryObject](#GETQBF85)                     |                                                                                                          |
| [IsQuery](#ISQUF35D)                            |                                                                                                          |
| [FenceSync](#FENC8869)                          |                                                                                                          |
| [UniformMatrix3x3](#UNIF9F3D)                   |                                                                                                          |
| [UniformMatrix3x4](#UNIF11D1)                   |                                                                                                          |
| [UniformMatrix4x2](#UNIF1A3B)                   |                                                                                                          |
| [UniformMatrix4x3](#UNIFBCE3)                   |                                                                                                          |
| [UniformMatrix4x4](#UNIF4A4F)                   |                                                                                                          |
| [GetString](#GETS3D05)                          |                                                                                                          |
| [GetBoolean](#GETBE898)                         |                                                                                                          |
| [GetBoolean](#GETBE898)                         |                                                                                                          |
| [GetInteger](#GETI6BE9)                         |                                                                                                          |
| [GetInternalformat](#GETI138F)                  |                                                                                                          |
| [GetIntegers](#GETIFE27)                        |                                                                                                          |
| [GetIntegers](#GETIFE27)                        |                                                                                                          |
| [GetFloat](#GETF4803)                           |                                                                                                          |
| [GetFloat](#GETF4803)                           |                                                                                                          |
| [DrawArrays](#DRAW2DAB)                         |                                                                                                          |
| [DrawArrays](#DRAW2DAB)                         |                                                                                                          |
| [DrawElements](#DRAWC4F7)                       |                                                                                                          |
| [DrawRangeElements](#DRAW517C)                  |                                                                                                          |
| [DrawArraysInstanced](#DRAW2C6E)                |                                                                                                          |
| [DrawArraysInstanced](#DRAW2C6E)                |                                                                                                          |
| [DrawElementsInstanced](#DRAW5D7A)              |                                                                                                          |
| [EnableVertexAttribArray](#ENABC6A0)            |                                                                                                          |
| [DisableVertexAttribArray](#DISAE9C0)           |                                                                                                          |
| [SetVertexAttribPointer](#SETV6B37)             |                                                                                                          |
| [SetVertexAttribPointer](#SETV6B37)             |                                                                                                          |
| [SetVertexAttribDivisor](#SETVA3B4)             |                                                                                                          |
| [GenBuffer](#GENB5CA9)                          |                                                                                                          |
| [GenBuffers](#GENB923E)                         |                                                                                                          |
| [DeleteBuffers](#DELE1B5C)                      |                                                                                                          |
| [DeleteBuffer](#DELEC557)                       |                                                                                                          |
| [DeleteBuffer](#DELEC557)                       |                                                                                                          |
| [IsBuffer](#ISBU5E37)                           |                                                                                                          |
| [BindBuffer](#BINDEE52)                         |                                                                                                          |
| [BindBufferBase](#BINDF855)                     |                                                                                                          |
| [GetBufferParameters](#GETBCC9F)                |                                                                                                          |
| [BufferData](#BUFFE60D)                         |                                                                                                          |
| [BufferSubData](#BUFFF7A2)                      |                                                                                                          |
| [BufferData<T>](#BUFF2CF9)                      |                                                                                                          |
| [BufferSubData<T>](#BUFFAD2D)                   |                                                                                                          |
| [MapBufferRange](#MAPB1DF4)                     |                                                                                                          |
| [UnmapBuffer](#UNMA1ED6)                        |                                                                                                          |
| [FlushMappedBufferRange](#FLUSFF4C)             |                                                                                                          |
| [GenVertexArray](#GENVDB30)                     |                                                                                                          |
| [GenVertexArrays](#GENVE2CC)                    |                                                                                                          |
| [DeleteVertexArrays](#DELE1FB0)                 |                                                                                                          |
| [DeleteVertexArray](#DELEE127)                  |                                                                                                          |
| [DeleteVertexArray](#DELEE127)                  |                                                                                                          |
| [IsVertexArray](#ISVE6B2A)                      |                                                                                                          |
| [BindVertexArray](#BIND1A28)                    |                                                                                                          |
| [GenFramebuffer](#GENF72FB)                     |                                                                                                          |
| [GenFramebuffers](#GENFA37F)                    |                                                                                                          |
| [DeleteFramebuffers](#DELEC770)                 |                                                                                                          |
| [DeleteFramebuffer](#DELEB99C)                  |                                                                                                          |
| [DeleteFramebuffer](#DELEB99C)                  |                                                                                                          |
| [IsFramebuffer](#ISFRA3B5)                      |                                                                                                          |
| [BindFramebuffer](#BIND57C2)                    |                                                                                                          |
| [GetFramebufferAttachmentParameters](#GETFCE2A) |                                                                                                          |
| [CheckFramebufferStatus](#CHECC7F9)             |                                                                                                          |
| [FramebufferTexture2D](#FRAM9A36)               |                                                                                                          |
| [FramebufferTextureLayer](#FRAM5EE6)            |                                                                                                          |
| [FramebufferRenderbuffer](#FRAM9943)            |                                                                                                          |
| [BlitFramebuffer](#BLIT9064)                    |                                                                                                          |
| [InvalidateFramebuffer](#INVAD426)              |                                                                                                          |
| [InvalidateSubFramebuffer](#INVA74D1)           |                                                                                                          |
| [GenRenderbuffer](#GENR8CA0)                    |                                                                                                          |
| [GenRenderbuffers](#GENRDE14)                   |                                                                                                          |
| [DeleteRenderbuffer](#DELE380E)                 |                                                                                                          |
| [DeleteRenderbuffer](#DELE380E)                 |                                                                                                          |
| [DeleteRenderbuffers](#DELEE8D3)                |                                                                                                          |
| [IsRenderbuffer](#ISRE3D45)                     |                                                                                                          |
| [RenderbufferStorage](#REND2C8E)                |                                                                                                          |
| [RenderbufferStorage](#REND2C8E)                |                                                                                                          |
| [GetRenderbufferParameter](#GETRC9D3)           |                                                                                                          |
| [BindRenderbuffer](#BIND5BD4)                   |                                                                                                          |
| [GenTexture](#GENTB343)                         |                                                                                                          |
| [GenTextures](#GENT448C)                        |                                                                                                          |
| [DeleteTextures](#DELED8EC)                     |                                                                                                          |
| [DeleteTexture](#DELE5836)                      |                                                                                                          |
| [DeleteTexture](#DELE5836)                      |                                                                                                          |
| [IsTexture](#ISTEFAE3)                          |                                                                                                          |
| [BindTexture](#BINDC2DA)                        |                                                                                                          |
| [ActiveTexture](#ACTIA9A8)                      |                                                                                                          |
| [GetTextureParameters](#GETT1879)               |                                                                                                          |
| [GetTextureParameters](#GETT1879)               |                                                                                                          |
| [LoadFunctions](#LOAD19B3)                      |                                                                                                          |
| [CreateShader](#CREA96E4)                       |                                                                                                          |
| [DeleteShader](#DELEC73B)                       |                                                                                                          |
| [CompileShader](#COMPFF13)                      |                                                                                                          |
| [ShaderSource](#SHADB590)                       |                                                                                                          |
| [GetShader](#GETS2DCF)                          |                                                                                                          |
| [GetShaderInfoLog](#GETS6D63)                   |                                                                                                          |
| [CreateProgram](#CREA9C46)                      |                                                                                                          |
| [DeleteProgram](#DELE892B)                      |                                                                                                          |
| [DeleteProgram](#DELE892B)                      |                                                                                                          |
| [IsProgram](#ISPR7D76)                          |                                                                                                          |
| [LinkProgram](#LINK7C20)                        |                                                                                                          |
| [ValidateProgram](#VALI1DAB)                    |                                                                                                          |
| [AttachShader](#ATTA881E)                       |                                                                                                          |
| [DetachShader](#DETAAF0F)                       |                                                                                                          |
| [UseProgram](#USEPCE1A)                         |                                                                                                          |
| [BindAttribLocation](#BINDC9B7)                 |                                                                                                          |
| [GetFragDataLocation](#GETF20AF)                |                                                                                                          |
| [GetProgram](#GETP6456)                         | Get a value about the given program from the GL implementation.                                          |
| [GetProgramInfoLog](#GETPF90E)                  |                                                                                                          |
| [GetActiveAttribute](#GETAC7CC)                 |                                                                                                          |
| [GetActiveAttributes](#GETA8604)                |                                                                                                          |
| [GetActiveUniform](#GETA74B3)                   |                                                                                                          |
| [GetActiveUniforms](#GETA91C4)                  |                                                                                                          |
| [GetAttribLocation](#GETAF3F1)                  |                                                                                                          |
| [GetUniformLocation](#GETUD38C)                 |                                                                                                          |
| [GetActiveUniformBlockName](#GETA12E8)          |                                                                                                          |
| [GetActiveUniformBlockIndex](#GETAE1FD)         |                                                                                                          |
| [GetActiveUniform](#GETA74B3)                   |                                                                                                          |
| [GetActiveUniformBlock](#GETA3548)              |                                                                                                          |
| [GetActiveUniformBlocks](#GETA55F4)             |                                                                                                          |
| [GetActiveUniformBlock](#GETA3548)              |                                                                                                          |
| [GetActiveUniformBlock](#GETA3548)              |                                                                                                          |
| [UniformBlockBinding](#UNIF3071)                |                                                                                                          |
| [Uniform1](#UNIFAD45)                           |                                                                                                          |
| [Uniform2](#UNIF4FED)                           |                                                                                                          |
| [Uniform3](#UNIFF294)                           |                                                                                                          |
| [Uniform4](#UNIF953C)                           |                                                                                                          |
| [Uniform1](#UNIFAD45)                           |                                                                                                          |
| [Uniform2](#UNIF4FED)                           |                                                                                                          |
| [Uniform3](#UNIFF294)                           |                                                                                                          |
| [Uniform4](#UNIF953C)                           |                                                                                                          |
| [Uniform1](#UNIFAD45)                           |                                                                                                          |
| [Uniform2](#UNIF4FED)                           |                                                                                                          |
| [Uniform3](#UNIFF294)                           |                                                                                                          |
| [Uniform4](#UNIF953C)                           |                                                                                                          |
| [Uniform1](#UNIFAD45)                           |                                                                                                          |
| [Uniform2](#UNIF4FED)                           |                                                                                                          |
| [Uniform3](#UNIFF294)                           |                                                                                                          |
| [Uniform4](#UNIF953C)                           |                                                                                                          |
| [Uniform1](#UNIFAD45)                           |                                                                                                          |
| [Uniform2](#UNIF4FED)                           |                                                                                                          |
| [Uniform3](#UNIFF294)                           |                                                                                                          |
| [Uniform4](#UNIF953C)                           |                                                                                                          |
| [Uniform1](#UNIFAD45)                           |                                                                                                          |
| [Uniform2](#UNIF4FED)                           |                                                                                                          |
| [Uniform3](#UNIFF294)                           |                                                                                                          |
| [Uniform4](#UNIF953C)                           |                                                                                                          |
| [Uniform1](#UNIFAD45)                           |                                                                                                          |
| [Uniform2](#UNIF4FED)                           |                                                                                                          |
| [Uniform3](#UNIFF294)                           |                                                                                                          |
| [Uniform4](#UNIF953C)                           |                                                                                                          |
| [Uniform1](#UNIFAD45)                           |                                                                                                          |
| [Uniform2](#UNIF4FED)                           |                                                                                                          |
| [Uniform3](#UNIFF294)                           |                                                                                                          |
| [Uniform4](#UNIF953C)                           |                                                                                                          |
| [Uniform1](#UNIFAD45)                           |                                                                                                          |
| [Uniform2](#UNIF4FED)                           |                                                                                                          |
| [Uniform3](#UNIFF294)                           |                                                                                                          |
| [Uniform4](#UNIF953C)                           |                                                                                                          |
| [UniformMatrix2x2](#UNIFA071)                   |                                                                                                          |
| [UniformMatrix2x3](#UNIF4319)                   |                                                                                                          |
| [UniformMatrix2x4](#UNIF705E)                   |                                                                                                          |
| [UniformMatrix3x2](#UNIF41E5)                   |                                                                                                          |
| [UniformMatrix3x3](#UNIF9F3D)                   |                                                                                                          |
| [UniformMatrix3x4](#UNIF11D1)                   |                                                                                                          |
| [UniformMatrix4x2](#UNIF1A3B)                   |                                                                                                          |
| [UniformMatrix4x3](#UNIFBCE3)                   |                                                                                                          |
| [UniformMatrix4x4](#UNIF4A4F)                   |                                                                                                          |
| [UniformMatrix2x2](#UNIFA071)                   |                                                                                                          |
| [UniformMatrix2x3](#UNIF4319)                   |                                                                                                          |
| [UniformMatrix2x4](#UNIF705E)                   |                                                                                                          |
| [UniformMatrix3x2](#UNIF41E5)                   |                                                                                                          |

### Properties

#### <a name="HASL3177"></a> HasLoadedFunctions : bool

<small>`Static`, `Read Only`</small>

### Methods

#### <a name="WAIT6009"></a> WaitSync(ulong sync, [WaitSyncFlags](Heirloom.OpenGLES.WaitSyncFlags.md) flags) : void
<small>`Static`</small>


#### <a name="DELE7FFE"></a> DeleteSync(ulong sync) : void
<small>`Static`</small>


#### <a name="ISSY7737"></a> IsSync(ulong sync) : bool
<small>`Static`</small>


#### <a name="SETTE290"></a> SetTextureParameter([TextureTarget](Heirloom.OpenGLES.TextureTarget.md) index, [TextureParameter](Heirloom.OpenGLES.TextureParameter.md) parameter, float value) : void
<small>`Static`</small>


#### <a name="SETT6689"></a> SetTextureParameter([TextureTarget](Heirloom.OpenGLES.TextureTarget.md) index, [TextureParameter](Heirloom.OpenGLES.TextureParameter.md) parameter, int value) : void
<small>`Static`</small>


#### <a name="SETTE290"></a> SetTextureParameter([TextureTarget](Heirloom.OpenGLES.TextureTarget.md) index, [TextureParameter](Heirloom.OpenGLES.TextureParameter.md) parameter, float value) : void
<small>`Static`</small>


#### <a name="SETT6689"></a> SetTextureParameter([TextureTarget](Heirloom.OpenGLES.TextureTarget.md) index, [TextureParameter](Heirloom.OpenGLES.TextureParameter.md) parameter, int value) : void
<small>`Static`</small>


#### <a name="TEXS7B8B"></a> TexStorage2D([TextureImageTarget](Heirloom.OpenGLES.TextureImageTarget.md) target, int levels, [TextureSizedFormat](Heirloom.OpenGLES.TextureSizedFormat.md) format, int width, int height) : void
<small>`Static`</small>


#### <a name="TEXS7B02"></a> TexStorage3D([TextureImageTarget](Heirloom.OpenGLES.TextureImageTarget.md) target, int levels, [TextureSizedFormat](Heirloom.OpenGLES.TextureSizedFormat.md) format, int width, int height, int depth) : void
<small>`Static`</small>


#### <a name="TEXIF815"></a> TexImage2D([TextureImageTarget](Heirloom.OpenGLES.TextureImageTarget.md) target, int level, [TextureSizedFormat](Heirloom.OpenGLES.TextureSizedFormat.md) internalFormat, int width, int height, [TexturePixelFormat](Heirloom.OpenGLES.TexturePixelFormat.md) format, [TexturePixelType](Heirloom.OpenGLES.TexturePixelType.md) pixelFormat, IntPtr data) : void
<small>`Static`</small>


#### <a name="TEXIC9EF"></a> TexImage3D([TextureImageTarget](Heirloom.OpenGLES.TextureImageTarget.md) target, int level, [TextureSizedFormat](Heirloom.OpenGLES.TextureSizedFormat.md) internalFormat, int width, int height, int depth, [TexturePixelFormat](Heirloom.OpenGLES.TexturePixelFormat.md) format, [TexturePixelType](Heirloom.OpenGLES.TexturePixelType.md) pixelFormat, IntPtr data) : void
<small>`Static`</small>


#### <a name="TEXSBEC6"></a> TexSubImage2D([TextureImageTarget](Heirloom.OpenGLES.TextureImageTarget.md) target, int level, int xoffset, int yoffset, int width, int height, [TexturePixelFormat](Heirloom.OpenGLES.TexturePixelFormat.md) format, [TexturePixelType](Heirloom.OpenGLES.TexturePixelType.md) pixelFormat, IntPtr data) : void
<small>`Static`</small>


#### <a name="TEXS9152"></a> TexSubImage3D([TextureImageTarget](Heirloom.OpenGLES.TextureImageTarget.md) target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, [TexturePixelFormat](Heirloom.OpenGLES.TexturePixelFormat.md) format, [TexturePixelType](Heirloom.OpenGLES.TexturePixelType.md) pixelFormat, IntPtr data) : void
<small>`Static`</small>


#### <a name="COMPD266"></a> CompressedTexImage2D([TextureImageTarget](Heirloom.OpenGLES.TextureImageTarget.md) target, int level, int width, int height, [TextureCompressedFormat](Heirloom.OpenGLES.TextureCompressedFormat.md) format, int size, IntPtr data) : void
<small>`Static`</small>


#### <a name="COMP9B77"></a> CompressedTexImage3D([TextureImageTarget](Heirloom.OpenGLES.TextureImageTarget.md) target, int level, int width, int height, int depth, [TextureCompressedFormat](Heirloom.OpenGLES.TextureCompressedFormat.md) format, int size, IntPtr data) : void
<small>`Static`</small>


#### <a name="COMPDED7"></a> CompressedTexSubImage2D([TextureImageTarget](Heirloom.OpenGLES.TextureImageTarget.md) target, int level, int xoffset, int yoffset, int width, int height, int size, IntPtr data) : void
<small>`Static`</small>


#### <a name="COMP5A4D"></a> CompressedTexSubImage3D([TextureImageTarget](Heirloom.OpenGLES.TextureImageTarget.md) target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, int size, IntPtr data) : void
<small>`Static`</small>


#### <a name="TEXIEE4A"></a> TexImage2D<T>([TextureImageTarget](Heirloom.OpenGLES.TextureImageTarget.md) target, int level, [TextureSizedFormat](Heirloom.OpenGLES.TextureSizedFormat.md) internalFormat, int width, int height, [TexturePixelFormat](Heirloom.OpenGLES.TexturePixelFormat.md) format, [TexturePixelType](Heirloom.OpenGLES.TexturePixelType.md) pixelFormat, T data) : void
<small>`Static`</small>


#### <a name="TEXI6D8C"></a> TexImage3D<T>([TextureImageTarget](Heirloom.OpenGLES.TextureImageTarget.md) target, int level, [TextureSizedFormat](Heirloom.OpenGLES.TextureSizedFormat.md) internalFormat, int width, int height, int depth, [TexturePixelFormat](Heirloom.OpenGLES.TexturePixelFormat.md) format, [TexturePixelType](Heirloom.OpenGLES.TexturePixelType.md) pixelFormat, T data) : void
<small>`Static`</small>


#### <a name="TEXSB6EC"></a> TexSubImage2D<T>([TextureImageTarget](Heirloom.OpenGLES.TextureImageTarget.md) target, int level, int xoffset, int yoffset, int width, int height, [TexturePixelFormat](Heirloom.OpenGLES.TexturePixelFormat.md) format, [TexturePixelType](Heirloom.OpenGLES.TexturePixelType.md) pixelFormat, T data) : void
<small>`Static`</small>


#### <a name="TEXS7116"></a> TexSubImage3D<T>([TextureImageTarget](Heirloom.OpenGLES.TextureImageTarget.md) target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, [TexturePixelFormat](Heirloom.OpenGLES.TexturePixelFormat.md) format, [TexturePixelType](Heirloom.OpenGLES.TexturePixelType.md) pixelFormat, T data) : void
<small>`Static`</small>


#### <a name="GENE20C6"></a> GenerateMipmap([TextureTarget](Heirloom.OpenGLES.TextureTarget.md) target) : void
<small>`Static`</small>


#### <a name="GENS4629"></a> GenSampler() : uint
<small>`Static`</small>

#### <a name="GENS8D0A"></a> GenSamplers(int n) : uint
<small>`Static`</small>


#### <a name="DELE351E"></a> DeleteSamplers(int n, uint sampler) : void
<small>`Static`</small>


#### <a name="DELEC60B"></a> DeleteSampler(uint sampler) : void
<small>`Static`</small>


#### <a name="DELEAA6C"></a> DeleteSampler(ref uint sampler) : void
<small>`Static`</small>


#### <a name="ISSABBFD"></a> IsSampler(uint sampler) : bool
<small>`Static`</small>


#### <a name="BINDE903"></a> BindSampler(uint unit, uint sampler) : void
<small>`Static`</small>


#### <a name="GETSD091"></a> GetSamplerParameters(uint sampler, [TextureParameter](Heirloom.OpenGLES.TextureParameter.md) parameter, float result) : void
<small>`Static`</small>


#### <a name="GETS1794"></a> GetSamplerParameters(uint sampler, [TextureParameter](Heirloom.OpenGLES.TextureParameter.md) parameter, int result) : void
<small>`Static`</small>


#### <a name="SAMPE9B5"></a> SamplerParameter(uint sampler, [TextureParameter](Heirloom.OpenGLES.TextureParameter.md) parameter, float value) : void
<small>`Static`</small>


#### <a name="SAMP8CD0"></a> SamplerParameter(uint sampler, [TextureParameter](Heirloom.OpenGLES.TextureParameter.md) parameter, int value) : void
<small>`Static`</small>


#### <a name="SAMPE9B5"></a> SamplerParameter(uint sampler, [TextureParameter](Heirloom.OpenGLES.TextureParameter.md) parameter, float value) : void
<small>`Static`</small>


#### <a name="SAMP8CD0"></a> SamplerParameter(uint sampler, [TextureParameter](Heirloom.OpenGLES.TextureParameter.md) parameter, int value) : void
<small>`Static`</small>


#### <a name="ENABFC76"></a> Enable([EnableCap](Heirloom.OpenGLES.EnableCap.md) enable) : void
<small>`Static`</small>


#### <a name="DISAC983"></a> Disable([EnableCap](Heirloom.OpenGLES.EnableCap.md) enable) : void
<small>`Static`</small>


#### <a name="ISENC214"></a> IsEnabled([EnableCap](Heirloom.OpenGLES.EnableCap.md) enable) : bool
<small>`Static`</small>


#### <a name="CLEA2F4D"></a> Clear([ClearMask](Heirloom.OpenGLES.ClearMask.md) mask) : void
<small>`Static`</small>

Clears the buffers of the currently bound framebuffer specified by the mask.


#### <a name="SETVBA21"></a> SetViewport(int x, int y, int width, int height) : void
<small>`Static`</small>


#### <a name="SETS65FF"></a> SetScissor(int x, int y, int width, int height) : void
<small>`Static`</small>


#### <a name="FRONE684"></a> FrontFace([FrontFaceMode](Heirloom.OpenGLES.FrontFaceMode.md) mode) : void
<small>`Static`</small>


#### <a name="SETCB017"></a> SetCullFace([Face](Heirloom.OpenGLES.Face.md) face) : void
<small>`Static`</small>


#### <a name="SETCBFE6"></a> SetColorMask(bool r, bool g, bool b, bool a) : void
<small>`Static`</small>


#### <a name="SETC9C46"></a> SetClearColor(float r, float g, float b, float a) : void
<small>`Static`</small>


#### <a name="SETC9793"></a> SetClearColor(uint c) : void
<small>`Static`</small>


#### <a name="SETD516F"></a> SetDepthMask(bool depth) : void
<small>`Static`</small>


#### <a name="SETD9167"></a> SetDepthFunction([DepthFunction](Heirloom.OpenGLES.DepthFunction.md) func) : void
<small>`Static`</small>


#### <a name="SETDBA1C"></a> SetDepthRange(float near, float far) : void
<small>`Static`</small>


#### <a name="SETCD0F3"></a> SetClearDepth(float depth) : void
<small>`Static`</small>


#### <a name="SETC2A09"></a> SetClearStencil(int stencil) : void
<small>`Static`</small>


#### <a name="SETL5271"></a> SetLineWidth(float width) : void
<small>`Static`</small>


#### <a name="SETHAB09"></a> SetHint([Hint](Heirloom.OpenGLES.Hint.md) hint, [HintMode](Heirloom.OpenGLES.HintMode.md) mode) : void
<small>`Static`</small>


#### <a name="SETS7821"></a> SetSampleCoverage(float val, bool invert) : void
<small>`Static`</small>


#### <a name="SETP48B4"></a> SetPolygonOffset(float factor, float units) : void
<small>`Static`</small>


#### <a name="SETP34D9"></a> SetPixelStore([PixelStoreParameter](Heirloom.OpenGLES.PixelStoreParameter.md) pname, int value) : void
<small>`Static`</small>


#### <a name="SETR9159"></a> SetReadBuffer([FramebufferBuffer](Heirloom.OpenGLES.FramebufferBuffer.md) mode) : void
<small>`Static`</small>

Changes the read buffer ( where reading operations occur )

<small>**mode**: <param name="mode"> The read mode/target </param></small>  

#### <a name="SETD72E8"></a> SetDrawBuffers([FramebufferBuffer[]](Heirloom.OpenGLES.FramebufferBuffer.md) mode) : void
<small>`Static`</small>

Changes the read buffer ( where reading operations occur )

<small>**mode**: <param name="mode"> The read mode/target </param></small>  

#### <a name="READ996E"></a> ReadPixels(int x, int y, int width, int height, [ReadPixelsFormat](Heirloom.OpenGLES.ReadPixelsFormat.md) format, [ReadPixelsType](Heirloom.OpenGLES.ReadPixelsType.md) type, void data) : void
<small>`Static`</small>

Reads a block of pixels from the frame buffer.


#### <a name="READAC07"></a> ReadPixels(int x, int y, int width, int height, [ReadPixelsFormat](Heirloom.OpenGLES.ReadPixelsFormat.md) format, [ReadPixelsType](Heirloom.OpenGLES.ReadPixelsType.md) type,  byte data) : void
<small>`Static`</small>

Reads a block of pixels from the frame buffer.


#### <a name="READ830C"></a> ReadPixels(int x, int y, int width, int height) : uint
<small>`Static`</small>

Reads a block of pixels from the frame buffer. ( ReadPixelsFormat.RGBA and ReadPixelsType.UnsignedByte )


#### <a name="FINI1852"></a> Finish() : void
<small>`Static`</small>

#### <a name="FLUS2F0E"></a> Flush() : void
<small>`Static`</small>

#### <a name="SETB4095"></a> SetBlendColor(float r, float g, float b, float a) : void
<small>`Static`</small>


#### <a name="SETBC5B8"></a> SetBlendEquation([BlendEquation](Heirloom.OpenGLES.BlendEquation.md) eq) : void
<small>`Static`</small>


#### <a name="SETB92EE"></a> SetBlendEquation([BlendEquation](Heirloom.OpenGLES.BlendEquation.md) eqColor, [BlendEquation](Heirloom.OpenGLES.BlendEquation.md) eqAlpha) : void
<small>`Static`</small>


#### <a name="SETBAC31"></a> SetBlendFunction([BlendFunction](Heirloom.OpenGLES.BlendFunction.md) source, [BlendFunction](Heirloom.OpenGLES.BlendFunction.md) destination) : void
<small>`Static`</small>


#### <a name="SETB7AC6"></a> SetBlendFunction([BlendFunction](Heirloom.OpenGLES.BlendFunction.md) sourceColor, [BlendFunction](Heirloom.OpenGLES.BlendFunction.md) destinationColor, [BlendFunction](Heirloom.OpenGLES.BlendFunction.md) sourceAlpha, [BlendFunction](Heirloom.OpenGLES.BlendFunction.md) destinationAlpha) : void
<small>`Static`</small>


#### <a name="STEN900C"></a> StencilFunction([StencilFunction](Heirloom.OpenGLES.StencilFunction.md) func, int refVal, uint mask) : void
<small>`Static`</small>


#### <a name="STEN90B0"></a> StencilFunction([Face](Heirloom.OpenGLES.Face.md) face, [StencilFunction](Heirloom.OpenGLES.StencilFunction.md) func, int refVal, uint mask) : void
<small>`Static`</small>


#### <a name="SETS61F9"></a> SetStencilMask(uint mask) : void
<small>`Static`</small>


#### <a name="SETSFBEC"></a> SetStencilMask([Face](Heirloom.OpenGLES.Face.md) face, uint mask) : void
<small>`Static`</small>


#### <a name="STENE861"></a> StencilOperation([StencilOperation](Heirloom.OpenGLES.StencilOperation.md) stencilFail, [StencilOperation](Heirloom.OpenGLES.StencilOperation.md) depthFail, [StencilOperation](Heirloom.OpenGLES.StencilOperation.md) depthPass) : void
<small>`Static`</small>


#### <a name="STENFAE9"></a> StencilOperation([Face](Heirloom.OpenGLES.Face.md) face, [StencilOperation](Heirloom.OpenGLES.StencilOperation.md) stencilFail, [StencilOperation](Heirloom.OpenGLES.StencilOperation.md) depthFail, [StencilOperation](Heirloom.OpenGLES.StencilOperation.md) depthPass) : void
<small>`Static`</small>


#### <a name="GENQE21F"></a> GenQueries(int n) : uint
<small>`Static`</small>


#### <a name="GENQE876"></a> GenQuery() : uint
<small>`Static`</small>

#### <a name="DELEB61E"></a> DeleteQueries(uint queries) : void
<small>`Static`</small>


#### <a name="DELEFBBF"></a> DeleteQuery(uint query) : void
<small>`Static`</small>


#### <a name="DELE8A27"></a> DeleteQuery(ref uint query) : void
<small>`Static`</small>


#### <a name="BEGIAFA1"></a> BeginQuery([QueryTarget](Heirloom.OpenGLES.QueryTarget.md) target, uint query) : void
<small>`Static`</small>


#### <a name="ENDQ572A"></a> EndQuery([QueryTarget](Heirloom.OpenGLES.QueryTarget.md) target) : void
<small>`Static`</small>


#### <a name="GETQ77B5"></a> GetQuery([QueryTarget](Heirloom.OpenGLES.QueryTarget.md) target, [QueryParameter](Heirloom.OpenGLES.QueryParameter.md) pname, int values) : void
<small>`Static`</small>


#### <a name="GETQ6607"></a> GetQueryObject(uint query, [QueryObjectParameter](Heirloom.OpenGLES.QueryObjectParameter.md) pname, out uint value) : void
<small>`Static`</small>


#### <a name="ISQUD379"></a> IsQuery(uint query) : bool
<small>`Static`</small>


#### <a name="FENC788B"></a> FenceSync([SyncFenceCondition](Heirloom.OpenGLES.SyncFenceCondition.md) condition, [SyncFenceFlags](Heirloom.OpenGLES.SyncFenceFlags.md) flags) : ulong
<small>`Static`</small>


#### <a name="UNIF351C"></a> UniformMatrix3x3(int location, int count, float ptr) : void
<small>`Static`</small>


#### <a name="UNIF8E0B"></a> UniformMatrix3x4(int location, int count, float ptr) : void
<small>`Static`</small>


#### <a name="UNIFCCA9"></a> UniformMatrix4x2(int location, int count, float ptr) : void
<small>`Static`</small>


#### <a name="UNIFC92E"></a> UniformMatrix4x3(int location, int count, float ptr) : void
<small>`Static`</small>


#### <a name="UNIF8CAA"></a> UniformMatrix4x4(int location, int count, float ptr) : void
<small>`Static`</small>


#### <a name="GETS5739"></a> GetString([StringParameter](Heirloom.OpenGLES.StringParameter.md) name) : string
<small>`Static`</small>


#### <a name="GETB7027"></a> GetBoolean([GetParameter](Heirloom.OpenGLES.GetParameter.md) name) : bool
<small>`Static`</small>


#### <a name="GETB28F6"></a> GetBoolean([GetParameter](Heirloom.OpenGLES.GetParameter.md) name, bool values) : void
<small>`Static`</small>


#### <a name="GETI5E8B"></a> GetInteger([GetParameter](Heirloom.OpenGLES.GetParameter.md) name) : int
<small>`Static`</small>


#### <a name="GETIB117"></a> GetInternalformat([RenderbufferFormat](Heirloom.OpenGLES.RenderbufferFormat.md) renderBufferFormat, [InternalFormatParameter](Heirloom.OpenGLES.InternalFormatParameter.md) pname, int bufferSize = 16) : int
<small>`Static`</small>


#### <a name="GETI62B5"></a> GetIntegers([GetParameter](Heirloom.OpenGLES.GetParameter.md) name, int values) : void
<small>`Static`</small>


#### <a name="GETI5A82"></a> GetIntegers([GetParameter](Heirloom.OpenGLES.GetParameter.md) name) : int
<small>`Static`</small>


#### <a name="GETF9F9B"></a> GetFloat([GetParameter](Heirloom.OpenGLES.GetParameter.md) name) : float
<small>`Static`</small>


#### <a name="GETFCC53"></a> GetFloat([GetParameter](Heirloom.OpenGLES.GetParameter.md) name, float values) : void
<small>`Static`</small>


#### <a name="DRAW9B3F"></a> DrawArrays([DrawMode](Heirloom.OpenGLES.DrawMode.md) mode, int count) : void
<small>`Static`</small>


#### <a name="DRAW8219"></a> DrawArrays([DrawMode](Heirloom.OpenGLES.DrawMode.md) mode, int first, int count) : void
<small>`Static`</small>


#### <a name="DRAW29B2"></a> DrawElements([DrawMode](Heirloom.OpenGLES.DrawMode.md) mode, int count, [DrawElementType](Heirloom.OpenGLES.DrawElementType.md) type, int offset = 0) : void
<small>`Static`</small>


#### <a name="DRAW9C33"></a> DrawRangeElements([DrawMode](Heirloom.OpenGLES.DrawMode.md) mode, int start, int end, int count, [DrawElementType](Heirloom.OpenGLES.DrawElementType.md) type, int offset = 0) : void
<small>`Static`</small>


#### <a name="DRAWBF95"></a> DrawArraysInstanced([DrawMode](Heirloom.OpenGLES.DrawMode.md) mode, int count, int primCount) : void
<small>`Static`</small>


#### <a name="DRAW51BC"></a> DrawArraysInstanced([DrawMode](Heirloom.OpenGLES.DrawMode.md) mode, int first, int count, int primCount) : void
<small>`Static`</small>


#### <a name="DRAW212D"></a> DrawElementsInstanced([DrawMode](Heirloom.OpenGLES.DrawMode.md) mode, int count, [DrawElementType](Heirloom.OpenGLES.DrawElementType.md) type, int primCount, int offset = 0) : void
<small>`Static`</small>


#### <a name="ENAB7ED7"></a> EnableVertexAttribArray(uint index) : void
<small>`Static`</small>


#### <a name="DISA2BC7"></a> DisableVertexAttribArray(uint index) : void
<small>`Static`</small>


#### <a name="SETV41F2"></a> SetVertexAttribPointer(uint index, int size, [VertexAttributeType](Heirloom.OpenGLES.VertexAttributeType.md) type, bool normalized, int stride, IntPtr pointer) : void
<small>`Static`</small>


#### <a name="SETV6031"></a> SetVertexAttribPointer(uint index, int size, [VertexAttributeType](Heirloom.OpenGLES.VertexAttributeType.md) type, bool normalized, int stride, uint bufferOffset) : void
<small>`Static`</small>


#### <a name="SETV80B8"></a> SetVertexAttribDivisor(uint index, int divisor) : void
<small>`Static`</small>


#### <a name="GENBDF64"></a> GenBuffer() : uint
<small>`Static`</small>

#### <a name="GENBB8D0"></a> GenBuffers(int n) : uint
<small>`Static`</small>


#### <a name="DELE9712"></a> DeleteBuffers(int n, uint buffers) : void
<small>`Static`</small>


#### <a name="DELEFD1B"></a> DeleteBuffer(uint buffer) : void
<small>`Static`</small>


#### <a name="DELE7799"></a> DeleteBuffer(ref uint buffer) : void
<small>`Static`</small>


#### <a name="ISBUB302"></a> IsBuffer(uint buffer) : bool
<small>`Static`</small>


#### <a name="BIND2A17"></a> BindBuffer([BufferTarget](Heirloom.OpenGLES.BufferTarget.md) target, uint buffer) : void
<small>`Static`</small>


#### <a name="BIND2BBF"></a> BindBufferBase([BufferTarget](Heirloom.OpenGLES.BufferTarget.md) target, uint index, uint buffer) : void
<small>`Static`</small>


#### <a name="GETB98A4"></a> GetBufferParameters([BufferTarget](Heirloom.OpenGLES.BufferTarget.md) index, [BufferParameter](Heirloom.OpenGLES.BufferParameter.md) parameter, int result) : void
<small>`Static`</small>


#### <a name="BUFF6DF5"></a> BufferData([BufferTarget](Heirloom.OpenGLES.BufferTarget.md) target, uint size, IntPtr data, [BufferUsage](Heirloom.OpenGLES.BufferUsage.md) usage = Static) : void
<small>`Static`</small>


#### <a name="BUFF64D0"></a> BufferSubData([BufferTarget](Heirloom.OpenGLES.BufferTarget.md) target, uint offset, uint size, IntPtr data) : void
<small>`Static`</small>


#### <a name="BUFFA5FD"></a> BufferData<T>([BufferTarget](Heirloom.OpenGLES.BufferTarget.md) target, T data, [BufferUsage](Heirloom.OpenGLES.BufferUsage.md) usage = Static) : void
<small>`Static`</small>


#### <a name="BUFFC3A6"></a> BufferSubData<T>([BufferTarget](Heirloom.OpenGLES.BufferTarget.md) target, uint offset, T data) : void
<small>`Static`</small>


#### <a name="MAPB5C24"></a> MapBufferRange([BufferTarget](Heirloom.OpenGLES.BufferTarget.md) target, int offset, int length, [MapBufferAccess](Heirloom.OpenGLES.MapBufferAccess.md) access) : void
<small>`Static`</small>


#### <a name="UNMAEBE9"></a> UnmapBuffer([BufferTarget](Heirloom.OpenGLES.BufferTarget.md) target) : bool
<small>`Static`</small>


#### <a name="FLUS9728"></a> FlushMappedBufferRange([BufferTarget](Heirloom.OpenGLES.BufferTarget.md) target, int offset, int size) : void
<small>`Static`</small>


#### <a name="GENV8B47"></a> GenVertexArray() : uint
<small>`Static`</small>

#### <a name="GENV29B3"></a> GenVertexArrays(int n) : uint
<small>`Static`</small>


#### <a name="DELE7794"></a> DeleteVertexArrays(int n, uint vaos) : void
<small>`Static`</small>


#### <a name="DELEECF8"></a> DeleteVertexArray(uint vao) : void
<small>`Static`</small>


#### <a name="DELEE11B"></a> DeleteVertexArray(ref uint vao) : void
<small>`Static`</small>


#### <a name="ISVE3864"></a> IsVertexArray(uint vao) : void
<small>`Static`</small>


#### <a name="BIND218F"></a> BindVertexArray(uint vao) : void
<small>`Static`</small>


#### <a name="GENFE0A8"></a> GenFramebuffer() : uint
<small>`Static`</small>

#### <a name="GENF64E5"></a> GenFramebuffers(int n) : uint
<small>`Static`</small>


#### <a name="DELE3D26"></a> DeleteFramebuffers(int n, uint buffers) : void
<small>`Static`</small>


#### <a name="DELE1DD8"></a> DeleteFramebuffer(uint buffer) : void
<small>`Static`</small>


#### <a name="DELEE285"></a> DeleteFramebuffer(ref uint buffer) : void
<small>`Static`</small>


#### <a name="ISFRBC21"></a> IsFramebuffer(uint buffer) : bool
<small>`Static`</small>


#### <a name="BIND9860"></a> BindFramebuffer([FramebufferTarget](Heirloom.OpenGLES.FramebufferTarget.md) target, uint buffer) : void
<small>`Static`</small>


#### <a name="GETF271E"></a> GetFramebufferAttachmentParameters([FramebufferTarget](Heirloom.OpenGLES.FramebufferTarget.md) target, [FramebufferAttachment](Heirloom.OpenGLES.FramebufferAttachment.md) attachment, [FramebufferAttachmentParameter](Heirloom.OpenGLES.FramebufferAttachmentParameter.md) parameter, int result) : void
<small>`Static`</small>


#### <a name="CHEC96E8"></a> CheckFramebufferStatus([FramebufferTarget](Heirloom.OpenGLES.FramebufferTarget.md) target) : [FramebufferStatus](Heirloom.OpenGLES.FramebufferStatus.md)
<small>`Static`</small>


#### <a name="FRAM1D14"></a> FramebufferTexture2D([FramebufferTarget](Heirloom.OpenGLES.FramebufferTarget.md) target, [FramebufferAttachment](Heirloom.OpenGLES.FramebufferAttachment.md) attachment, [TextureImageTarget](Heirloom.OpenGLES.TextureImageTarget.md) texTarget, uint tex, int mip) : void
<small>`Static`</small>


#### <a name="FRAM5CC5"></a> FramebufferTextureLayer([FramebufferTarget](Heirloom.OpenGLES.FramebufferTarget.md) target, [FramebufferAttachment](Heirloom.OpenGLES.FramebufferAttachment.md) attachment, uint tex, int mip, int layer) : void
<small>`Static`</small>


#### <a name="FRAM4B5C"></a> FramebufferRenderbuffer([FramebufferTarget](Heirloom.OpenGLES.FramebufferTarget.md) target, [FramebufferAttachment](Heirloom.OpenGLES.FramebufferAttachment.md) attachment, uint renderbuffer) : void
<small>`Static`</small>


#### <a name="BLIT79F5"></a> BlitFramebuffer(int srcX0, int srcY0, int srcX1, int srcY1, int dstX0, int dstY0, int dstX1, int dstY1, [FramebufferBlitMask](Heirloom.OpenGLES.FramebufferBlitMask.md) mask, [FramebufferBlitFilter](Heirloom.OpenGLES.FramebufferBlitFilter.md) filter) : void
<small>`Static`</small>


#### <a name="INVA8294"></a> InvalidateFramebuffer([FramebufferAttachment[]](Heirloom.OpenGLES.FramebufferAttachment.md) attachments) : void
<small>`Static`</small>


#### <a name="INVAB4C7"></a> InvalidateSubFramebuffer([FramebufferAttachment[]](Heirloom.OpenGLES.FramebufferAttachment.md) attachments, int x, int y, int width, int height) : void
<small>`Static`</small>


#### <a name="GENR947C"></a> GenRenderbuffer() : uint
<small>`Static`</small>

#### <a name="GENR6630"></a> GenRenderbuffers(int n) : uint
<small>`Static`</small>


#### <a name="DELE7FA5"></a> DeleteRenderbuffer(uint buffer) : void
<small>`Static`</small>


#### <a name="DELE55B9"></a> DeleteRenderbuffer(ref uint buffer) : void
<small>`Static`</small>


#### <a name="DELEC126"></a> DeleteRenderbuffers(int n, uint buffers) : void
<small>`Static`</small>


#### <a name="ISREF0CD"></a> IsRenderbuffer(uint buffer) : bool
<small>`Static`</small>


#### <a name="RENDD112"></a> RenderbufferStorage([RenderbufferFormat](Heirloom.OpenGLES.RenderbufferFormat.md) format, int width, int height) : void
<small>`Static`</small>


#### <a name="REND15CF"></a> RenderbufferStorage([RenderbufferFormat](Heirloom.OpenGLES.RenderbufferFormat.md) format, int width, int height, int samples = 0) : void
<small>`Static`</small>


#### <a name="GETR3E5A"></a> GetRenderbufferParameter([RenderbufferValue](Heirloom.OpenGLES.RenderbufferValue.md) parameter, int result) : void
<small>`Static`</small>


#### <a name="BIND58A7"></a> BindRenderbuffer(uint renderbuffer) : void
<small>`Static`</small>


#### <a name="GENT3743"></a> GenTexture() : uint
<small>`Static`</small>

#### <a name="GENT3EBC"></a> GenTextures(int n) : uint
<small>`Static`</small>


#### <a name="DELEE190"></a> DeleteTextures(int n, uint textures) : void
<small>`Static`</small>


#### <a name="DELE217B"></a> DeleteTexture(uint texture) : void
<small>`Static`</small>


#### <a name="DELEEB4A"></a> DeleteTexture(ref uint texture) : void
<small>`Static`</small>


#### <a name="ISTE7CB9"></a> IsTexture(uint texture) : bool
<small>`Static`</small>


#### <a name="BIND7115"></a> BindTexture([TextureTarget](Heirloom.OpenGLES.TextureTarget.md) target, uint texture) : void
<small>`Static`</small>


#### <a name="ACTI628B"></a> ActiveTexture(uint texture) : void
<small>`Static`</small>


#### <a name="GETT5B8C"></a> GetTextureParameters([TextureTarget](Heirloom.OpenGLES.TextureTarget.md) index, [TextureParameter](Heirloom.OpenGLES.TextureParameter.md) parameter, float result) : void
<small>`Static`</small>


#### <a name="GETT7C15"></a> GetTextureParameters([TextureTarget](Heirloom.OpenGLES.TextureTarget.md) index, [TextureParameter](Heirloom.OpenGLES.TextureParameter.md) parameter, int result) : void
<small>`Static`</small>


#### <a name="LOAD68F1"></a> LoadFunctions([GL.GetProcAddress](Heirloom.OpenGLES.GL.GetProcAddress.md) getProcAddress) : void
<small>`Static`</small>


#### <a name="CREA1483"></a> CreateShader([ShaderType](Heirloom.OpenGLES.ShaderType.md) type) : uint
<small>`Static`</small>


#### <a name="DELE21BC"></a> DeleteShader(uint shader) : void
<small>`Static`</small>


#### <a name="COMP1CBF"></a> CompileShader(uint shader) : void
<small>`Static`</small>


#### <a name="SHADFBB5"></a> ShaderSource(uint shader, string source) : void
<small>`Static`</small>


#### <a name="GETSEC4B"></a> GetShader(uint shader, [ShaderParameter](Heirloom.OpenGLES.ShaderParameter.md) name) : int
<small>`Static`</small>


#### <a name="GETSA56D"></a> GetShaderInfoLog(uint shader) : string
<small>`Static`</small>


#### <a name="CREA5993"></a> CreateProgram() : uint
<small>`Static`</small>

#### <a name="DELE832B"></a> DeleteProgram(uint program) : void
<small>`Static`</small>


#### <a name="DELE7565"></a> DeleteProgram(ref uint program) : void
<small>`Static`</small>


#### <a name="ISPR2861"></a> IsProgram(uint program) : bool
<small>`Static`</small>


#### <a name="LINKA06A"></a> LinkProgram(uint program) : void
<small>`Static`</small>


#### <a name="VALI2F63"></a> ValidateProgram(uint program) : void
<small>`Static`</small>


#### <a name="ATTA7E3D"></a> AttachShader(uint program, uint shader) : void
<small>`Static`</small>


#### <a name="DETA189E"></a> DetachShader(uint program, uint shader) : void
<small>`Static`</small>


#### <a name="USEP1B4A"></a> UseProgram(uint program) : void
<small>`Static`</small>


#### <a name="BINDAC86"></a> BindAttribLocation(uint program, uint index, string name) : void
<small>`Static`</small>


#### <a name="GETFB740"></a> GetFragDataLocation(uint program, string name) : int
<small>`Static`</small>


#### <a name="GETPDA25"></a> GetProgram(uint program, [ProgramParameter](Heirloom.OpenGLES.ProgramParameter.md) property) : int
<small>`Static`</small>

Get a value about the given program from the GL implementation.

<small>**program**: <param name="program"> A valid program name </param></small>  
<small>**property**: <param name="property"> The type of property to request </param></small>  

#### <a name="GETP6633"></a> GetProgramInfoLog(uint program) : string
<small>`Static`</small>


#### <a name="GETA47E2"></a> GetActiveAttribute(uint program, uint index) : [ActiveAttribute](Heirloom.OpenGLES.ActiveAttribute.md)
<small>`Static`</small>


#### <a name="GETA30E4"></a> GetActiveAttributes(uint program) : [ActiveAttribute[]](Heirloom.OpenGLES.ActiveAttribute.md)
<small>`Static`</small>


#### <a name="GETA22B8"></a> GetActiveUniform(uint program, uint index) : [ActiveUniform](Heirloom.OpenGLES.ActiveUniform.md)
<small>`Static`</small>


#### <a name="GETA99D0"></a> GetActiveUniforms(uint program) : [ActiveUniform[]](Heirloom.OpenGLES.ActiveUniform.md)
<small>`Static`</small>


#### <a name="GETAA410"></a> GetAttribLocation(uint program, string name) : int
<small>`Static`</small>


#### <a name="GETU3913"></a> GetUniformLocation(uint program, string name) : int
<small>`Static`</small>


#### <a name="GETAA72A"></a> GetActiveUniformBlockName(uint program, uint blockIndex) : string
<small>`Static`</small>


#### <a name="GETA2057"></a> GetActiveUniformBlockIndex(uint program, string name) : uint
<small>`Static`</small>


#### <a name="GETAC951"></a> GetActiveUniform(uint program, uint uniformIndices, [UniformParameter](Heirloom.OpenGLES.UniformParameter.md) pname, int result) : void
<small>`Static`</small>


#### <a name="GETAA821"></a> GetActiveUniformBlock(uint program, uint index) : [ActiveUniformBlock](Heirloom.OpenGLES.ActiveUniformBlock.md)
<small>`Static`</small>


#### <a name="GETA47BE"></a> GetActiveUniformBlocks(uint program) : [ActiveUniformBlock[]](Heirloom.OpenGLES.ActiveUniformBlock.md)
<small>`Static`</small>


#### <a name="GETA6907"></a> GetActiveUniformBlock(uint program, uint blockIndex, [UniformBlockParameter](Heirloom.OpenGLES.UniformBlockParameter.md) pname, int result) : void
<small>`Static`</small>


#### <a name="GETAE697"></a> GetActiveUniformBlock(uint program, uint blockIndex, [UniformBlockParameter](Heirloom.OpenGLES.UniformBlockParameter.md) pname, out int value) : void
<small>`Static`</small>


#### <a name="UNIFAE68"></a> UniformBlockBinding(uint program, uint index, uint binding) : void
<small>`Static`</small>


#### <a name="UNIFD38C"></a> Uniform1(int location, float v) : void
<small>`Static`</small>


#### <a name="UNIF676D"></a> Uniform2(int location, float v1, float v2) : void
<small>`Static`</small>


#### <a name="UNIFE807"></a> Uniform3(int location, float v1, float v2, float v3) : void
<small>`Static`</small>


#### <a name="UNIF9F0B"></a> Uniform4(int location, float v1, float v2, float v3, float v4) : void
<small>`Static`</small>


#### <a name="UNIFB2D0"></a> Uniform1(int location, int v) : void
<small>`Static`</small>


#### <a name="UNIF5991"></a> Uniform2(int location, int v1, int v2) : void
<small>`Static`</small>


#### <a name="UNIFBF93"></a> Uniform3(int location, int v1, int v2, int v3) : void
<small>`Static`</small>


#### <a name="UNIFBA3F"></a> Uniform4(int location, int v1, int v2, int v3, int v4) : void
<small>`Static`</small>


#### <a name="UNIF86BB"></a> Uniform1(int location, uint v) : void
<small>`Static`</small>


#### <a name="UNIF2CEB"></a> Uniform2(int location, uint v1, uint v2) : void
<small>`Static`</small>


#### <a name="UNIFA666"></a> Uniform3(int location, uint v1, uint v2, uint v3) : void
<small>`Static`</small>


#### <a name="UNIFC5A3"></a> Uniform4(int location, uint v1, uint v2, uint v3, uint v4) : void
<small>`Static`</small>


#### <a name="UNIFF5CA"></a> Uniform1(int location, int count, float arr) : void
<small>`Static`</small>


#### <a name="UNIF8B32"></a> Uniform2(int location, int count, float arr) : void
<small>`Static`</small>


#### <a name="UNIF60F2"></a> Uniform3(int location, int count, float arr) : void
<small>`Static`</small>


#### <a name="UNIF4A78"></a> Uniform4(int location, int count, float arr) : void
<small>`Static`</small>


#### <a name="UNIF40E1"></a> Uniform1(int location, float arr) : void
<small>`Static`</small>


#### <a name="UNIF5195"></a> Uniform2(int location, float arr) : void
<small>`Static`</small>


#### <a name="UNIFADE2"></a> Uniform3(int location, float arr) : void
<small>`Static`</small>


#### <a name="UNIFDDE9"></a> Uniform4(int location, float arr) : void
<small>`Static`</small>


#### <a name="UNIF4C38"></a> Uniform1(int location, int count, int arr) : void
<small>`Static`</small>


#### <a name="UNIFB5B9"></a> Uniform2(int location, int count, int arr) : void
<small>`Static`</small>


#### <a name="UNIF790D"></a> Uniform3(int location, int count, int arr) : void
<small>`Static`</small>


#### <a name="UNIFC506"></a> Uniform4(int location, int count, int arr) : void
<small>`Static`</small>


#### <a name="UNIF1154"></a> Uniform1(int location, int arr) : void
<small>`Static`</small>


#### <a name="UNIF206F"></a> Uniform2(int location, int arr) : void
<small>`Static`</small>


#### <a name="UNIFC3F5"></a> Uniform3(int location, int arr) : void
<small>`Static`</small>


#### <a name="UNIF50B8"></a> Uniform4(int location, int arr) : void
<small>`Static`</small>


#### <a name="UNIF24B5"></a> Uniform1(int location, int count, uint arr) : void
<small>`Static`</small>


#### <a name="UNIF62FD"></a> Uniform2(int location, int count, uint arr) : void
<small>`Static`</small>


#### <a name="UNIF463E"></a> Uniform3(int location, int count, uint arr) : void
<small>`Static`</small>


#### <a name="UNIF47E3"></a> Uniform4(int location, int count, uint arr) : void
<small>`Static`</small>


#### <a name="UNIFDF53"></a> Uniform1(int location, uint arr) : void
<small>`Static`</small>


#### <a name="UNIFB823"></a> Uniform2(int location, uint arr) : void
<small>`Static`</small>


#### <a name="UNIFBE85"></a> Uniform3(int location, uint arr) : void
<small>`Static`</small>


#### <a name="UNIFF9B3"></a> Uniform4(int location, uint arr) : void
<small>`Static`</small>


#### <a name="UNIFC3B4"></a> UniformMatrix2x2(int location, float values) : void
<small>`Static`</small>


#### <a name="UNIF11C3"></a> UniformMatrix2x3(int location, float values) : void
<small>`Static`</small>


#### <a name="UNIF62EE"></a> UniformMatrix2x4(int location, float values) : void
<small>`Static`</small>


#### <a name="UNIF8881"></a> UniformMatrix3x2(int location, float values) : void
<small>`Static`</small>


#### <a name="UNIF6408"></a> UniformMatrix3x3(int location, float values) : void
<small>`Static`</small>


#### <a name="UNIF8011"></a> UniformMatrix3x4(int location, float values) : void
<small>`Static`</small>


#### <a name="UNIFD2FB"></a> UniformMatrix4x2(int location, float values) : void
<small>`Static`</small>


#### <a name="UNIF1D78"></a> UniformMatrix4x3(int location, float values) : void
<small>`Static`</small>


#### <a name="UNIFA2D2"></a> UniformMatrix4x4(int location, float values) : void
<small>`Static`</small>


#### <a name="UNIF85E0"></a> UniformMatrix2x2(int location, int count, float ptr) : void
<small>`Static`</small>


#### <a name="UNIF1C09"></a> UniformMatrix2x3(int location, int count, float ptr) : void
<small>`Static`</small>


#### <a name="UNIF3C38"></a> UniformMatrix2x4(int location, int count, float ptr) : void
<small>`Static`</small>


#### <a name="UNIF7AA7"></a> UniformMatrix3x2(int location, int count, float ptr) : void
<small>`Static`</small>


