# Heirloom.OpenGLES

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.OpenGLES](../Heirloom.OpenGLES/Heirloom.OpenGLES.md)</small>  

## GL (Static Class)
<small>**Namespace**: Heirloom.OpenGLES</small>  

| Properties                         | Summary |
|------------------------------------|---------|
| [HasLoadedFunctions](#HAS31777ABC) |         |

| Methods                                           | Summary                                                                                                  |
|---------------------------------------------------|----------------------------------------------------------------------------------------------------------|
| [WaitSync](#WAI6009FC0C)                          |                                                                                                          |
| [DeleteSync](#DEL7FFEDED)                         |                                                                                                          |
| [IsSync](#ISS7737C2DC)                            |                                                                                                          |
| [SetTextureParameter](#SETE2904307)               |                                                                                                          |
| [SetTextureParameter](#SET6689C4B6)               |                                                                                                          |
| [SetTextureParameter](#SETE2904307)               |                                                                                                          |
| [SetTextureParameter](#SET6689C4B6)               |                                                                                                          |
| [TexStorage2D](#TEX7B8B5A4)                       |                                                                                                          |
| [TexStorage3D](#TEX7B027FE7)                      |                                                                                                          |
| [TexImage2D](#TEXF815EECF)                        |                                                                                                          |
| [TexImage3D](#TEXC9EFA3EE)                        |                                                                                                          |
| [TexSubImage2D](#TEXBEC6CD18)                     |                                                                                                          |
| [TexSubImage3D](#TEX9152BB0B)                     |                                                                                                          |
| [CompressedTexImage2D](#COMD26623AD)              |                                                                                                          |
| [CompressedTexImage3D](#COM9B77D19E)              |                                                                                                          |
| [CompressedTexSubImage2D](#COMDED737E9)           |                                                                                                          |
| [CompressedTexSubImage3D](#COM5A4D4608)           |                                                                                                          |
| [TexImage2D<T>](#TEXEE4AB2E)                      |                                                                                                          |
| [TexImage3D<T>](#TEX6D8C3DFF)                     |                                                                                                          |
| [TexSubImage2D<T>](#TEXB6ECF99F)                  |                                                                                                          |
| [TexSubImage3D<T>](#TEX7116320C)                  |                                                                                                          |
| [GenerateMipmap](#GEN20C6671F)                    |                                                                                                          |
| [GenSampler](#GEN462995)                          |                                                                                                          |
| [GenSamplers](#GEN8D0AA7CB)                       |                                                                                                          |
| [DeleteSamplers](#DEL351E55A)                     |                                                                                                          |
| [DeleteSampler](#DELC60B2B6C)                     |                                                                                                          |
| [DeleteSampler](#DELAA6C8679)                     |                                                                                                          |
| [IsSampler](#ISSBBFD1881)                         |                                                                                                          |
| [BindSampler](#BINE903EA60)                       |                                                                                                          |
| [GetSamplerParameters](#GETD091D38F)              |                                                                                                          |
| [GetSamplerParameters](#GET17940340)              |                                                                                                          |
| [SamplerParameter](#SAME9B596FE)                  |                                                                                                          |
| [SamplerParameter](#SAM8CD00BAF)                  |                                                                                                          |
| [SamplerParameter](#SAME9B596FE)                  |                                                                                                          |
| [SamplerParameter](#SAM8CD00BAF)                  |                                                                                                          |
| [Enable](#ENAFC761EE9)                            |                                                                                                          |
| [Disable](#DISC983ECBA)                           |                                                                                                          |
| [IsEnabled](#ISEC214EC05)                         |                                                                                                          |
| [Clear](#CLE2F4D7932)                             | Clears the buffers of the currently bound framebuffer specified by the mask.                             |
| [SetViewport](#SETBA21462D)                       |                                                                                                          |
| [SetScissor](#SET65FFEF41)                        |                                                                                                          |
| [FrontFace](#FROE6841878)                         |                                                                                                          |
| [SetCullFace](#SETB017B3C5)                       |                                                                                                          |
| [SetColorMask](#SETBFE6F35C)                      |                                                                                                          |
| [SetClearColor](#SET9C46BD97)                     |                                                                                                          |
| [SetClearColor](#SET9793B0D2)                     |                                                                                                          |
| [SetDepthMask](#SET516FF28F)                      |                                                                                                          |
| [SetDepthFunction](#SET9167C8D8)                  |                                                                                                          |
| [SetDepthRange](#SETBA1C5DE)                      |                                                                                                          |
| [SetClearDepth](#SETD0F3CAA)                      |                                                                                                          |
| [SetClearStencil](#SET2A097CEF)                   |                                                                                                          |
| [SetLineWidth](#SET527194B)                       |                                                                                                          |
| [SetHint](#SETAB0978A)                            |                                                                                                          |
| [SetSampleCoverage](#SET782157EC)                 |                                                                                                          |
| [SetPolygonOffset](#SET48B4C01C)                  |                                                                                                          |
| [SetPixelStore](#SET34D9C33)                      |                                                                                                          |
| [SetReadBuffer](#SET91598148)                     | Changes the read buffer ( where reading operations occur )                                               |
| [SetDrawBuffers](#SET72E849C5)                    |                                                                                                          |
| [ReadPixels](#REA996E2379)                        |                                                                                                          |
| [ReadPixels](#REAAC076033)                        |                                                                                                          |
| [ReadPixels](#REA830C853A)                        | Reads a block of pixels from the frame buffer. ( ReadPixelsFormat.RGBA and ReadPixelsType.UnsignedByte ) |
| [Finish](#FIN18529F62)                            |                                                                                                          |
| [Flush](#FLU2F0EB18F)                             |                                                                                                          |
| [SetBlendColor](#SET4095588B)                     |                                                                                                          |
| [SetBlendEquation](#SETC5B84860)                  |                                                                                                          |
| [SetBlendEquation](#SET92EE3E55)                  |                                                                                                          |
| [SetBlendFunction](#SETAC31AAB3)                  |                                                                                                          |
| [SetBlendFunction](#SET7AC66090)                  |                                                                                                          |
| [StencilFunction](#STE900CCB7C)                   |                                                                                                          |
| [StencilFunction](#STE90B04867)                   |                                                                                                          |
| [SetStencilMask](#SET61F916D5)                    |                                                                                                          |
| [SetStencilMask](#SETFBECD6A)                     |                                                                                                          |
| [StencilOperation](#STEE8611B85)                  |                                                                                                          |
| [StencilOperation](#STEFAE9CB2E)                  |                                                                                                          |
| [GenQueries](#GENE21F0AA0)                        |                                                                                                          |
| [GenQuery](#GENE8768477)                          |                                                                                                          |
| [DeleteQueries](#DELB61E69FC)                     |                                                                                                          |
| [DeleteQuery](#DELFBBF1CA8)                       |                                                                                                          |
| [DeleteQuery](#DEL8A27C005)                       |                                                                                                          |
| [BeginQuery](#BEGAFA1146D)                        |                                                                                                          |
| [EndQuery](#END572AC7E9)                          |                                                                                                          |
| [GetQuery](#GET77B5960E)                          |                                                                                                          |
| [GetQueryObject](#GET66078AC)                     |                                                                                                          |
| [IsQuery](#ISQD3799C15)                           |                                                                                                          |
| [FenceSync](#FEN788BC3F4)                         |                                                                                                          |
| [UniformMatrix3x3](#UNI351C4D70)                  |                                                                                                          |
| [UniformMatrix3x4](#UNI8E0B2B53)                  |                                                                                                          |
| [UniformMatrix4x2](#UNICCA976E)                   |                                                                                                          |
| [UniformMatrix4x3](#UNIC92EAC33)                  |                                                                                                          |
| [UniformMatrix4x4](#UNI8CAA1ED0)                  |                                                                                                          |
| [GetString](#GET5739D3E4)                         |                                                                                                          |
| [GetBoolean](#GET7027547C)                        |                                                                                                          |
| [GetBoolean](#GET28F6EEAE)                        |                                                                                                          |
| [GetInteger](#GET5E8BC805)                        |                                                                                                          |
| [GetInternalformat](#GETB1176DDA)                 |                                                                                                          |
| [GetIntegers](#GET62B51A3A)                       |                                                                                                          |
| [GetIntegers](#GET5A8286F2)                       |                                                                                                          |
| [GetFloat](#GET9F9B5C84)                          |                                                                                                          |
| [GetFloat](#GETCC53E7E4)                          |                                                                                                          |
| [DrawArrays](#DRA9B3FC09A)                        |                                                                                                          |
| [DrawArrays](#DRA82193519)                        |                                                                                                          |
| [DrawElements](#DRA29B2BB76)                      |                                                                                                          |
| [DrawRangeElements](#DRA9C339EB6)                 |                                                                                                          |
| [DrawArraysInstanced](#DRABF955897)               |                                                                                                          |
| [DrawArraysInstanced](#DRA51BCCCDE)               |                                                                                                          |
| [DrawElementsInstanced](#DRA212D8B1)              |                                                                                                          |
| [EnableVertexAttribArray](#ENA7ED71A03)           |                                                                                                          |
| [DisableVertexAttribArray](#DIS2BC74CD2)          |                                                                                                          |
| [SetVertexAttribPointer](#SET41F2F1D9)            |                                                                                                          |
| [SetVertexAttribPointer](#SET60318C54)            |                                                                                                          |
| [SetVertexAttribDivisor](#SET80B8FA60)            |                                                                                                          |
| [GenBuffer](#GENDF648929)                         |                                                                                                          |
| [GenBuffers](#GENB8D06BEF)                        |                                                                                                          |
| [DeleteBuffers](#DEL9712FD6D)                     |                                                                                                          |
| [DeleteBuffer](#DELFD1B65F0)                      |                                                                                                          |
| [DeleteBuffer](#DEL77999AF9)                      |                                                                                                          |
| [IsBuffer](#ISBB302646D)                          |                                                                                                          |
| [BindBuffer](#BIN2A172B77)                        |                                                                                                          |
| [BindBufferBase](#BIN2BBFCE18)                    |                                                                                                          |
| [GetBufferParameters](#GET98A47B5C)               |                                                                                                          |
| [BufferData](#BUF6DF5024A)                        |                                                                                                          |
| [BufferSubData](#BUF64D089C5)                     |                                                                                                          |
| [BufferData<T>](#BUFA5FDB53C)                     |                                                                                                          |
| [BufferSubData<T>](#BUFC3A60331)                  |                                                                                                          |
| [MapBufferRange](#MAP5C24FD46)                    |                                                                                                          |
| [UnmapBuffer](#UNMEBE941BD)                       |                                                                                                          |
| [FlushMappedBufferRange](#FLU97281B0E)            |                                                                                                          |
| [GenVertexArray](#GEN8B47E6F2)                    |                                                                                                          |
| [GenVertexArrays](#GEN29B3B4C4)                   |                                                                                                          |
| [DeleteVertexArrays](#DEL7794F3D4)                |                                                                                                          |
| [DeleteVertexArray](#DELECF8CDD5)                 |                                                                                                          |
| [DeleteVertexArray](#DELE11B9748)                 |                                                                                                          |
| [IsVertexArray](#ISV38642E68)                     |                                                                                                          |
| [BindVertexArray](#BIN218FCA7D)                   |                                                                                                          |
| [GenFramebuffer](#GENE0A8E3DE)                    |                                                                                                          |
| [GenFramebuffers](#GEN64E5C1B0)                   |                                                                                                          |
| [DeleteFramebuffers](#DEL3D26BE68)                |                                                                                                          |
| [DeleteFramebuffer](#DEL1DD82D49)                 |                                                                                                          |
| [DeleteFramebuffer](#DELE285B76E)                 |                                                                                                          |
| [IsFramebuffer](#ISFBC215260)                     |                                                                                                          |
| [BindFramebuffer](#BIN98604BBC)                   |                                                                                                          |
| [GetFramebufferAttachmentParameters](#GET271E4E6) |                                                                                                          |
| [CheckFramebufferStatus](#CHE96E86D25)            |                                                                                                          |
| [FramebufferTexture2D](#FRA1D14E791)              |                                                                                                          |
| [FramebufferTextureLayer](#FRA5CC51B96)           |                                                                                                          |
| [FramebufferRenderbuffer](#FRA4B5C45AC)           |                                                                                                          |
| [BlitFramebuffer](#BLI79F590BD)                   |                                                                                                          |
| [InvalidateFramebuffer](#INV82945495)             |                                                                                                          |
| [InvalidateSubFramebuffer](#INVB4C7B719)          |                                                                                                          |
| [GenRenderbuffer](#GEN947CF017)                   |                                                                                                          |
| [GenRenderbuffers](#GEN66301189)                  |                                                                                                          |
| [DeleteRenderbuffer](#DEL7FA54C8)                 |                                                                                                          |
| [DeleteRenderbuffer](#DEL55B962EF)                |                                                                                                          |
| [DeleteRenderbuffers](#DELC12674AB)               |                                                                                                          |
| [IsRenderbuffer](#ISRF0CD12EF)                    |                                                                                                          |
| [RenderbufferStorage](#REND112FFF6)               |                                                                                                          |
| [RenderbufferStorage](#REN15CFF63B)               |                                                                                                          |
| [GetRenderbufferParameter](#GET3E5A075F)          |                                                                                                          |
| [BindRenderbuffer](#BIN58A7A0E8)                  |                                                                                                          |
| [GenTexture](#GEN37433AFE)                        |                                                                                                          |
| [GenTextures](#GEN3EBCE22C)                       |                                                                                                          |
| [DeleteTextures](#DELE1902C35)                    |                                                                                                          |
| [DeleteTexture](#DEL217BDF6)                      |                                                                                                          |
| [DeleteTexture](#DELEB4ADE79)                     |                                                                                                          |
| [IsTexture](#IST7CB95371)                         |                                                                                                          |
| [BindTexture](#BIN71154183)                       |                                                                                                          |
| [ActiveTexture](#ACT628B5865)                     |                                                                                                          |
| [GetTextureParameters](#GET5B8C26B8)              |                                                                                                          |
| [GetTextureParameters](#GET7C1592F5)              |                                                                                                          |
| [LoadFunctions](#LOA68F1E672)                     |                                                                                                          |
| [CreateShader](#CRE1483BD0C)                      |                                                                                                          |
| [DeleteShader](#DEL21BC8470)                      |                                                                                                          |
| [CompileShader](#COM1CBF07C2)                     |                                                                                                          |
| [ShaderSource](#SHAFBB5810E)                      |                                                                                                          |
| [GetShader](#GETEC4B8CFD)                         |                                                                                                          |
| [GetShaderInfoLog](#GETA56DD7D4)                  |                                                                                                          |
| [CreateProgram](#CRE59931C79)                     |                                                                                                          |
| [DeleteProgram](#DEL832B292C)                     |                                                                                                          |
| [DeleteProgram](#DEL756563F9)                     |                                                                                                          |
| [IsProgram](#ISP28618AC1)                         |                                                                                                          |
| [LinkProgram](#LINA06AEF85)                       |                                                                                                          |
| [ValidateProgram](#VAL2F630AFB)                   |                                                                                                          |
| [AttachShader](#ATT7E3D38E)                       |                                                                                                          |
| [DetachShader](#DET189EDDF4)                      |                                                                                                          |
| [UseProgram](#USE1B4AB48)                         |                                                                                                          |
| [BindAttribLocation](#BINAC8699CB)                |                                                                                                          |
| [GetFragDataLocation](#GETB740DDD1)               |                                                                                                          |
| [GetProgram](#GETDA25DA0D)                        | Get a value about the given program from the GL implementation.                                          |
| [GetProgramInfoLog](#GET6633CB5A)                 |                                                                                                          |
| [GetActiveAttribute](#GET47E2B245)                |                                                                                                          |
| [GetActiveAttributes](#GET30E4D7C0)               |                                                                                                          |
| [GetActiveUniform](#GET22B856E9)                  |                                                                                                          |
| [GetActiveUniforms](#GET99D0186C)                 |                                                                                                          |
| [GetAttribLocation](#GETA4109E8F)                 |                                                                                                          |
| [GetUniformLocation](#GET39135FAF)                |                                                                                                          |
| [GetActiveUniformBlockName](#GETA72AEAA1)         |                                                                                                          |
| [GetActiveUniformBlockIndex](#GET20570024)        |                                                                                                          |
| [GetActiveUniform](#GETC9511261)                  |                                                                                                          |
| [GetActiveUniformBlock](#GETA82108AA)             |                                                                                                          |
| [GetActiveUniformBlocks](#GET47BE8CA5)            |                                                                                                          |
| [GetActiveUniformBlock](#GET690765A)              |                                                                                                          |
| [GetActiveUniformBlock](#GETE6975292)             |                                                                                                          |
| [UniformBlockBinding](#UNIAE68321C)               |                                                                                                          |
| [Uniform1](#UNID38C1B8C)                          |                                                                                                          |
| [Uniform2](#UNI676DEA4)                           |                                                                                                          |
| [Uniform3](#UNIE807AF28)                          |                                                                                                          |
| [Uniform4](#UNI9F0BFE21)                          |                                                                                                          |
| [Uniform1](#UNIB2D07BCD)                          |                                                                                                          |
| [Uniform2](#UNI59918B04)                          |                                                                                                          |
| [Uniform3](#UNIBF938047)                          |                                                                                                          |
| [Uniform4](#UNIBA3FEEA1)                          |                                                                                                          |
| [Uniform1](#UNI86BB5AA0)                          |                                                                                                          |
| [Uniform2](#UNI2CEB812A)                          |                                                                                                          |
| [Uniform3](#UNIA6665920)                          |                                                                                                          |
| [Uniform4](#UNIC5A3E35)                           |                                                                                                          |
| [Uniform1](#UNIF5CAC555)                          |                                                                                                          |
| [Uniform2](#UNI8B32D506)                          |                                                                                                          |
| [Uniform3](#UNI60F2064B)                          |                                                                                                          |
| [Uniform4](#UNI4A7878FC)                          |                                                                                                          |
| [Uniform1](#UNI40E12FED)                          |                                                                                                          |
| [Uniform2](#UNI51956B08)                          |                                                                                                          |
| [Uniform3](#UNIADE2A23)                           |                                                                                                          |
| [Uniform4](#UNIDDE9B43E)                          |                                                                                                          |
| [Uniform1](#UNI4C389086)                          |                                                                                                          |
| [Uniform2](#UNIB5B904F5)                          |                                                                                                          |
| [Uniform3](#UNI790D9AD0)                          |                                                                                                          |
| [Uniform4](#UNIC5068D17)                          |                                                                                                          |
| [Uniform1](#UNI11547E8A)                          |                                                                                                          |
| [Uniform2](#UNI206F1085)                          |                                                                                                          |
| [Uniform3](#UNIC3F54C80)                          |                                                                                                          |
| [Uniform4](#UNI50B8087B)                          |                                                                                                          |
| [Uniform1](#UNI24B5B267)                          |                                                                                                          |
| [Uniform2](#UNI62FDEF2C)                          |                                                                                                          |
| [Uniform3](#UNI463EB5F1)                          |                                                                                                          |
| [Uniform4](#UNI47E306B6)                          |                                                                                                          |
| [Uniform1](#UNIDF535B17)                          |                                                                                                          |
| [Uniform2](#UNIB8239BD2)                          |                                                                                                          |
| [Uniform3](#UNIBE85690D)                          |                                                                                                          |
| [Uniform4](#UNIF9B322C8)                          |                                                                                                          |
| [UniformMatrix2x2](#UNIC3B4E83A)                  |                                                                                                          |
| [UniformMatrix2x3](#UNI11C380FF)                  |                                                                                                          |
| [UniformMatrix2x4](#UNI62EE6D30)                  |                                                                                                          |
| [UniformMatrix3x2](#UNI8881B35F)                  |                                                                                                          |
| [UniformMatrix3x3](#UNI64089A9A)                  |                                                                                                          |
| [UniformMatrix3x4](#UNI80118255)                  |                                                                                                          |
| [UniformMatrix4x2](#UNID2FBBBF0)                  |                                                                                                          |
| [UniformMatrix4x3](#UNI1D78C5B5)                  |                                                                                                          |
| [UniformMatrix4x4](#UNIA2D2E5FA)                  |                                                                                                          |
| [UniformMatrix2x2](#UNI85E0DB10)                  |                                                                                                          |
| [UniformMatrix2x3](#UNI1C0939D5)                  |                                                                                                          |
| [UniformMatrix2x4](#UNI3C3893AE)                  |                                                                                                          |
| [UniformMatrix3x2](#UNI7AA7C3B5)                  |                                                                                                          |

### Properties

#### <a name="HAS31777ABC"></a>HasLoadedFunctions : bool

<small>`Static`, `Read Only`</small>

### Methods

#### <a name="WAI6009FC0C"></a>WaitSync(ulong sync, [WaitSyncFlags](Heirloom.OpenGLES.WaitSyncFlags.md) flags) : void
<small>`Static`</small>


#### <a name="DEL7FFEDED"></a>DeleteSync(ulong sync) : void
<small>`Static`</small>


#### <a name="ISS7737C2DC"></a>IsSync(ulong sync) : bool
<small>`Static`</small>


#### <a name="SETE2904307"></a>SetTextureParameter([TextureTarget](Heirloom.OpenGLES.TextureTarget.md) index, [TextureParameter](Heirloom.OpenGLES.TextureParameter.md) parameter, float value) : void
<small>`Static`</small>


#### <a name="SET6689C4B6"></a>SetTextureParameter([TextureTarget](Heirloom.OpenGLES.TextureTarget.md) index, [TextureParameter](Heirloom.OpenGLES.TextureParameter.md) parameter, int value) : void
<small>`Static`</small>


#### <a name="SETE2904307"></a>SetTextureParameter([TextureTarget](Heirloom.OpenGLES.TextureTarget.md) index, [TextureParameter](Heirloom.OpenGLES.TextureParameter.md) parameter, float value) : void
<small>`Static`</small>


#### <a name="SET6689C4B6"></a>SetTextureParameter([TextureTarget](Heirloom.OpenGLES.TextureTarget.md) index, [TextureParameter](Heirloom.OpenGLES.TextureParameter.md) parameter, int value) : void
<small>`Static`</small>


#### <a name="TEX7B8B5A4"></a>TexStorage2D([TextureImageTarget](Heirloom.OpenGLES.TextureImageTarget.md) target, int levels, [TextureSizedFormat](Heirloom.OpenGLES.TextureSizedFormat.md) format, int width, int height) : void
<small>`Static`</small>


#### <a name="TEX7B027FE7"></a>TexStorage3D([TextureImageTarget](Heirloom.OpenGLES.TextureImageTarget.md) target, int levels, [TextureSizedFormat](Heirloom.OpenGLES.TextureSizedFormat.md) format, int width, int height, int depth) : void
<small>`Static`</small>


#### <a name="TEXF815EECF"></a>TexImage2D([TextureImageTarget](Heirloom.OpenGLES.TextureImageTarget.md) target, int level, [TextureSizedFormat](Heirloom.OpenGLES.TextureSizedFormat.md) internalFormat, int width, int height, [TexturePixelFormat](Heirloom.OpenGLES.TexturePixelFormat.md) format, [TexturePixelType](Heirloom.OpenGLES.TexturePixelType.md) pixelFormat, IntPtr data) : void
<small>`Static`</small>


#### <a name="TEXC9EFA3EE"></a>TexImage3D([TextureImageTarget](Heirloom.OpenGLES.TextureImageTarget.md) target, int level, [TextureSizedFormat](Heirloom.OpenGLES.TextureSizedFormat.md) internalFormat, int width, int height, int depth, [TexturePixelFormat](Heirloom.OpenGLES.TexturePixelFormat.md) format, [TexturePixelType](Heirloom.OpenGLES.TexturePixelType.md) pixelFormat, IntPtr data) : void
<small>`Static`</small>


#### <a name="TEXBEC6CD18"></a>TexSubImage2D([TextureImageTarget](Heirloom.OpenGLES.TextureImageTarget.md) target, int level, int xoffset, int yoffset, int width, int height, [TexturePixelFormat](Heirloom.OpenGLES.TexturePixelFormat.md) format, [TexturePixelType](Heirloom.OpenGLES.TexturePixelType.md) pixelFormat, IntPtr data) : void
<small>`Static`</small>


#### <a name="TEX9152BB0B"></a>TexSubImage3D([TextureImageTarget](Heirloom.OpenGLES.TextureImageTarget.md) target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, [TexturePixelFormat](Heirloom.OpenGLES.TexturePixelFormat.md) format, [TexturePixelType](Heirloom.OpenGLES.TexturePixelType.md) pixelFormat, IntPtr data) : void
<small>`Static`</small>


#### <a name="COMD26623AD"></a>CompressedTexImage2D([TextureImageTarget](Heirloom.OpenGLES.TextureImageTarget.md) target, int level, int width, int height, [TextureCompressedFormat](Heirloom.OpenGLES.TextureCompressedFormat.md) format, int size, IntPtr data) : void
<small>`Static`</small>


#### <a name="COM9B77D19E"></a>CompressedTexImage3D([TextureImageTarget](Heirloom.OpenGLES.TextureImageTarget.md) target, int level, int width, int height, int depth, [TextureCompressedFormat](Heirloom.OpenGLES.TextureCompressedFormat.md) format, int size, IntPtr data) : void
<small>`Static`</small>


#### <a name="COMDED737E9"></a>CompressedTexSubImage2D([TextureImageTarget](Heirloom.OpenGLES.TextureImageTarget.md) target, int level, int xoffset, int yoffset, int width, int height, int size, IntPtr data) : void
<small>`Static`</small>


#### <a name="COM5A4D4608"></a>CompressedTexSubImage3D([TextureImageTarget](Heirloom.OpenGLES.TextureImageTarget.md) target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, int size, IntPtr data) : void
<small>`Static`</small>


#### <a name="TEXEE4AB2E"></a>TexImage2D<T>([TextureImageTarget](Heirloom.OpenGLES.TextureImageTarget.md) target, int level, [TextureSizedFormat](Heirloom.OpenGLES.TextureSizedFormat.md) internalFormat, int width, int height, [TexturePixelFormat](Heirloom.OpenGLES.TexturePixelFormat.md) format, [TexturePixelType](Heirloom.OpenGLES.TexturePixelType.md) pixelFormat, T data) : void
<small>`Static`</small>


#### <a name="TEX6D8C3DFF"></a>TexImage3D<T>([TextureImageTarget](Heirloom.OpenGLES.TextureImageTarget.md) target, int level, [TextureSizedFormat](Heirloom.OpenGLES.TextureSizedFormat.md) internalFormat, int width, int height, int depth, [TexturePixelFormat](Heirloom.OpenGLES.TexturePixelFormat.md) format, [TexturePixelType](Heirloom.OpenGLES.TexturePixelType.md) pixelFormat, T data) : void
<small>`Static`</small>


#### <a name="TEXB6ECF99F"></a>TexSubImage2D<T>([TextureImageTarget](Heirloom.OpenGLES.TextureImageTarget.md) target, int level, int xoffset, int yoffset, int width, int height, [TexturePixelFormat](Heirloom.OpenGLES.TexturePixelFormat.md) format, [TexturePixelType](Heirloom.OpenGLES.TexturePixelType.md) pixelFormat, T data) : void
<small>`Static`</small>


#### <a name="TEX7116320C"></a>TexSubImage3D<T>([TextureImageTarget](Heirloom.OpenGLES.TextureImageTarget.md) target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, [TexturePixelFormat](Heirloom.OpenGLES.TexturePixelFormat.md) format, [TexturePixelType](Heirloom.OpenGLES.TexturePixelType.md) pixelFormat, T data) : void
<small>`Static`</small>


#### <a name="GEN20C6671F"></a>GenerateMipmap([TextureTarget](Heirloom.OpenGLES.TextureTarget.md) target) : void
<small>`Static`</small>


#### <a name="GEN462995"></a>GenSampler() : uint
<small>`Static`</small>

#### <a name="GEN8D0AA7CB"></a>GenSamplers(int n) : uint
<small>`Static`</small>


#### <a name="DEL351E55A"></a>DeleteSamplers(int n, uint sampler) : void
<small>`Static`</small>


#### <a name="DELC60B2B6C"></a>DeleteSampler(uint sampler) : void
<small>`Static`</small>


#### <a name="DELAA6C8679"></a>DeleteSampler(ref uint sampler) : void
<small>`Static`</small>


#### <a name="ISSBBFD1881"></a>IsSampler(uint sampler) : bool
<small>`Static`</small>


#### <a name="BINE903EA60"></a>BindSampler(uint unit, uint sampler) : void
<small>`Static`</small>


#### <a name="GETD091D38F"></a>GetSamplerParameters(uint sampler, [TextureParameter](Heirloom.OpenGLES.TextureParameter.md) parameter, float result) : void
<small>`Static`</small>


#### <a name="GET17940340"></a>GetSamplerParameters(uint sampler, [TextureParameter](Heirloom.OpenGLES.TextureParameter.md) parameter, int result) : void
<small>`Static`</small>


#### <a name="SAME9B596FE"></a>SamplerParameter(uint sampler, [TextureParameter](Heirloom.OpenGLES.TextureParameter.md) parameter, float value) : void
<small>`Static`</small>


#### <a name="SAM8CD00BAF"></a>SamplerParameter(uint sampler, [TextureParameter](Heirloom.OpenGLES.TextureParameter.md) parameter, int value) : void
<small>`Static`</small>


#### <a name="SAME9B596FE"></a>SamplerParameter(uint sampler, [TextureParameter](Heirloom.OpenGLES.TextureParameter.md) parameter, float value) : void
<small>`Static`</small>


#### <a name="SAM8CD00BAF"></a>SamplerParameter(uint sampler, [TextureParameter](Heirloom.OpenGLES.TextureParameter.md) parameter, int value) : void
<small>`Static`</small>


#### <a name="ENAFC761EE9"></a>Enable([EnableCap](Heirloom.OpenGLES.EnableCap.md) enable) : void
<small>`Static`</small>


#### <a name="DISC983ECBA"></a>Disable([EnableCap](Heirloom.OpenGLES.EnableCap.md) enable) : void
<small>`Static`</small>


#### <a name="ISEC214EC05"></a>IsEnabled([EnableCap](Heirloom.OpenGLES.EnableCap.md) enable) : bool
<small>`Static`</small>


#### <a name="CLE2F4D7932"></a>Clear([ClearMask](Heirloom.OpenGLES.ClearMask.md) mask) : void
<small>`Static`</small>

Clears the buffers of the currently bound framebuffer specified by the mask.


#### <a name="SETBA21462D"></a>SetViewport(int x, int y, int width, int height) : void
<small>`Static`</small>


#### <a name="SET65FFEF41"></a>SetScissor(int x, int y, int width, int height) : void
<small>`Static`</small>


#### <a name="FROE6841878"></a>FrontFace([FrontFaceMode](Heirloom.OpenGLES.FrontFaceMode.md) mode) : void
<small>`Static`</small>


#### <a name="SETB017B3C5"></a>SetCullFace([Face](Heirloom.OpenGLES.Face.md) face) : void
<small>`Static`</small>


#### <a name="SETBFE6F35C"></a>SetColorMask(bool r, bool g, bool b, bool a) : void
<small>`Static`</small>


#### <a name="SET9C46BD97"></a>SetClearColor(float r, float g, float b, float a) : void
<small>`Static`</small>


#### <a name="SET9793B0D2"></a>SetClearColor(uint c) : void
<small>`Static`</small>


#### <a name="SET516FF28F"></a>SetDepthMask(bool depth) : void
<small>`Static`</small>


#### <a name="SET9167C8D8"></a>SetDepthFunction([DepthFunction](Heirloom.OpenGLES.DepthFunction.md) func) : void
<small>`Static`</small>


#### <a name="SETBA1C5DE"></a>SetDepthRange(float near, float far) : void
<small>`Static`</small>


#### <a name="SETD0F3CAA"></a>SetClearDepth(float depth) : void
<small>`Static`</small>


#### <a name="SET2A097CEF"></a>SetClearStencil(int stencil) : void
<small>`Static`</small>


#### <a name="SET527194B"></a>SetLineWidth(float width) : void
<small>`Static`</small>


#### <a name="SETAB0978A"></a>SetHint([Hint](Heirloom.OpenGLES.Hint.md) hint, [HintMode](Heirloom.OpenGLES.HintMode.md) mode) : void
<small>`Static`</small>


#### <a name="SET782157EC"></a>SetSampleCoverage(float val, bool invert) : void
<small>`Static`</small>


#### <a name="SET48B4C01C"></a>SetPolygonOffset(float factor, float units) : void
<small>`Static`</small>


#### <a name="SET34D9C33"></a>SetPixelStore([PixelStoreParameter](Heirloom.OpenGLES.PixelStoreParameter.md) pname, int value) : void
<small>`Static`</small>


#### <a name="SET91598148"></a>SetReadBuffer([FramebufferBuffer](Heirloom.OpenGLES.FramebufferBuffer.md) mode) : void
<small>`Static`</small>

Changes the read buffer ( where reading operations occur )

<small>**mode**: <param name="mode"> The read mode/target </param></small>  

#### <a name="SET72E849C5"></a>SetDrawBuffers([FramebufferBuffer[]](Heirloom.OpenGLES.FramebufferBuffer.md) mode) : void
<small>`Static`</small>


#### <a name="REA996E2379"></a>ReadPixels(int x, int y, int width, int height, [ReadPixelsFormat](Heirloom.OpenGLES.ReadPixelsFormat.md) format, [ReadPixelsType](Heirloom.OpenGLES.ReadPixelsType.md) type, void data) : void
<small>`Static`</small>


#### <a name="REAAC076033"></a>ReadPixels(int x, int y, int width, int height, [ReadPixelsFormat](Heirloom.OpenGLES.ReadPixelsFormat.md) format, [ReadPixelsType](Heirloom.OpenGLES.ReadPixelsType.md) type,  byte data) : void
<small>`Static`</small>


#### <a name="REA830C853A"></a>ReadPixels(int x, int y, int width, int height) : uint
<small>`Static`</small>

Reads a block of pixels from the frame buffer. ( ReadPixelsFormat.RGBA and ReadPixelsType.UnsignedByte )


#### <a name="FIN18529F62"></a>Finish() : void
<small>`Static`</small>

#### <a name="FLU2F0EB18F"></a>Flush() : void
<small>`Static`</small>

#### <a name="SET4095588B"></a>SetBlendColor(float r, float g, float b, float a) : void
<small>`Static`</small>


#### <a name="SETC5B84860"></a>SetBlendEquation([BlendEquation](Heirloom.OpenGLES.BlendEquation.md) eq) : void
<small>`Static`</small>


#### <a name="SET92EE3E55"></a>SetBlendEquation([BlendEquation](Heirloom.OpenGLES.BlendEquation.md) eqColor, [BlendEquation](Heirloom.OpenGLES.BlendEquation.md) eqAlpha) : void
<small>`Static`</small>


#### <a name="SETAC31AAB3"></a>SetBlendFunction([BlendFunction](Heirloom.OpenGLES.BlendFunction.md) source, [BlendFunction](Heirloom.OpenGLES.BlendFunction.md) destination) : void
<small>`Static`</small>


#### <a name="SET7AC66090"></a>SetBlendFunction([BlendFunction](Heirloom.OpenGLES.BlendFunction.md) sourceColor, [BlendFunction](Heirloom.OpenGLES.BlendFunction.md) destinationColor, [BlendFunction](Heirloom.OpenGLES.BlendFunction.md) sourceAlpha, [BlendFunction](Heirloom.OpenGLES.BlendFunction.md) destinationAlpha) : void
<small>`Static`</small>


#### <a name="STE900CCB7C"></a>StencilFunction([StencilFunction](Heirloom.OpenGLES.StencilFunction.md) func, int refVal, uint mask) : void
<small>`Static`</small>


#### <a name="STE90B04867"></a>StencilFunction([Face](Heirloom.OpenGLES.Face.md) face, [StencilFunction](Heirloom.OpenGLES.StencilFunction.md) func, int refVal, uint mask) : void
<small>`Static`</small>


#### <a name="SET61F916D5"></a>SetStencilMask(uint mask) : void
<small>`Static`</small>


#### <a name="SETFBECD6A"></a>SetStencilMask([Face](Heirloom.OpenGLES.Face.md) face, uint mask) : void
<small>`Static`</small>


#### <a name="STEE8611B85"></a>StencilOperation([StencilOperation](Heirloom.OpenGLES.StencilOperation.md) stencilFail, [StencilOperation](Heirloom.OpenGLES.StencilOperation.md) depthFail, [StencilOperation](Heirloom.OpenGLES.StencilOperation.md) depthPass) : void
<small>`Static`</small>


#### <a name="STEFAE9CB2E"></a>StencilOperation([Face](Heirloom.OpenGLES.Face.md) face, [StencilOperation](Heirloom.OpenGLES.StencilOperation.md) stencilFail, [StencilOperation](Heirloom.OpenGLES.StencilOperation.md) depthFail, [StencilOperation](Heirloom.OpenGLES.StencilOperation.md) depthPass) : void
<small>`Static`</small>


#### <a name="GENE21F0AA0"></a>GenQueries(int n) : uint
<small>`Static`</small>


#### <a name="GENE8768477"></a>GenQuery() : uint
<small>`Static`</small>

#### <a name="DELB61E69FC"></a>DeleteQueries(uint queries) : void
<small>`Static`</small>


#### <a name="DELFBBF1CA8"></a>DeleteQuery(uint query) : void
<small>`Static`</small>


#### <a name="DEL8A27C005"></a>DeleteQuery(ref uint query) : void
<small>`Static`</small>


#### <a name="BEGAFA1146D"></a>BeginQuery([QueryTarget](Heirloom.OpenGLES.QueryTarget.md) target, uint query) : void
<small>`Static`</small>


#### <a name="END572AC7E9"></a>EndQuery([QueryTarget](Heirloom.OpenGLES.QueryTarget.md) target) : void
<small>`Static`</small>


#### <a name="GET77B5960E"></a>GetQuery([QueryTarget](Heirloom.OpenGLES.QueryTarget.md) target, [QueryParameter](Heirloom.OpenGLES.QueryParameter.md) pname, int values) : void
<small>`Static`</small>


#### <a name="GET66078AC"></a>GetQueryObject(uint query, [QueryObjectParameter](Heirloom.OpenGLES.QueryObjectParameter.md) pname, out uint value) : void
<small>`Static`</small>


#### <a name="ISQD3799C15"></a>IsQuery(uint query) : bool
<small>`Static`</small>


#### <a name="FEN788BC3F4"></a>FenceSync([SyncFenceCondition](Heirloom.OpenGLES.SyncFenceCondition.md) condition, [SyncFenceFlags](Heirloom.OpenGLES.SyncFenceFlags.md) flags) : ulong
<small>`Static`</small>


#### <a name="UNI351C4D70"></a>UniformMatrix3x3(int location, int count, float ptr) : void
<small>`Static`</small>


#### <a name="UNI8E0B2B53"></a>UniformMatrix3x4(int location, int count, float ptr) : void
<small>`Static`</small>


#### <a name="UNICCA976E"></a>UniformMatrix4x2(int location, int count, float ptr) : void
<small>`Static`</small>


#### <a name="UNIC92EAC33"></a>UniformMatrix4x3(int location, int count, float ptr) : void
<small>`Static`</small>


#### <a name="UNI8CAA1ED0"></a>UniformMatrix4x4(int location, int count, float ptr) : void
<small>`Static`</small>


#### <a name="GET5739D3E4"></a>GetString([StringParameter](Heirloom.OpenGLES.StringParameter.md) name) : string
<small>`Static`</small>


#### <a name="GET7027547C"></a>GetBoolean([GetParameter](Heirloom.OpenGLES.GetParameter.md) name) : bool
<small>`Static`</small>


#### <a name="GET28F6EEAE"></a>GetBoolean([GetParameter](Heirloom.OpenGLES.GetParameter.md) name, bool values) : void
<small>`Static`</small>


#### <a name="GET5E8BC805"></a>GetInteger([GetParameter](Heirloom.OpenGLES.GetParameter.md) name) : int
<small>`Static`</small>


#### <a name="GETB1176DDA"></a>GetInternalformat([RenderbufferFormat](Heirloom.OpenGLES.RenderbufferFormat.md) renderBufferFormat, [InternalFormatParameter](Heirloom.OpenGLES.InternalFormatParameter.md) pname, int bufferSize = 16) : int
<small>`Static`</small>


#### <a name="GET62B51A3A"></a>GetIntegers([GetParameter](Heirloom.OpenGLES.GetParameter.md) name, int values) : void
<small>`Static`</small>


#### <a name="GET5A8286F2"></a>GetIntegers([GetParameter](Heirloom.OpenGLES.GetParameter.md) name) : int
<small>`Static`</small>


#### <a name="GET9F9B5C84"></a>GetFloat([GetParameter](Heirloom.OpenGLES.GetParameter.md) name) : float
<small>`Static`</small>


#### <a name="GETCC53E7E4"></a>GetFloat([GetParameter](Heirloom.OpenGLES.GetParameter.md) name, float values) : void
<small>`Static`</small>


#### <a name="DRA9B3FC09A"></a>DrawArrays([DrawMode](Heirloom.OpenGLES.DrawMode.md) mode, int count) : void
<small>`Static`</small>


#### <a name="DRA82193519"></a>DrawArrays([DrawMode](Heirloom.OpenGLES.DrawMode.md) mode, int first, int count) : void
<small>`Static`</small>


#### <a name="DRA29B2BB76"></a>DrawElements([DrawMode](Heirloom.OpenGLES.DrawMode.md) mode, int count, [DrawElementType](Heirloom.OpenGLES.DrawElementType.md) type, int offset = 0) : void
<small>`Static`</small>


#### <a name="DRA9C339EB6"></a>DrawRangeElements([DrawMode](Heirloom.OpenGLES.DrawMode.md) mode, int start, int end, int count, [DrawElementType](Heirloom.OpenGLES.DrawElementType.md) type, int offset = 0) : void
<small>`Static`</small>


#### <a name="DRABF955897"></a>DrawArraysInstanced([DrawMode](Heirloom.OpenGLES.DrawMode.md) mode, int count, int primCount) : void
<small>`Static`</small>


#### <a name="DRA51BCCCDE"></a>DrawArraysInstanced([DrawMode](Heirloom.OpenGLES.DrawMode.md) mode, int first, int count, int primCount) : void
<small>`Static`</small>


#### <a name="DRA212D8B1"></a>DrawElementsInstanced([DrawMode](Heirloom.OpenGLES.DrawMode.md) mode, int count, [DrawElementType](Heirloom.OpenGLES.DrawElementType.md) type, int primCount, int offset = 0) : void
<small>`Static`</small>


#### <a name="ENA7ED71A03"></a>EnableVertexAttribArray(uint index) : void
<small>`Static`</small>


#### <a name="DIS2BC74CD2"></a>DisableVertexAttribArray(uint index) : void
<small>`Static`</small>


#### <a name="SET41F2F1D9"></a>SetVertexAttribPointer(uint index, int size, [VertexAttributeType](Heirloom.OpenGLES.VertexAttributeType.md) type, bool normalized, int stride, IntPtr pointer) : void
<small>`Static`</small>


#### <a name="SET60318C54"></a>SetVertexAttribPointer(uint index, int size, [VertexAttributeType](Heirloom.OpenGLES.VertexAttributeType.md) type, bool normalized, int stride, uint bufferOffset) : void
<small>`Static`</small>


#### <a name="SET80B8FA60"></a>SetVertexAttribDivisor(uint index, int divisor) : void
<small>`Static`</small>


#### <a name="GENDF648929"></a>GenBuffer() : uint
<small>`Static`</small>

#### <a name="GENB8D06BEF"></a>GenBuffers(int n) : uint
<small>`Static`</small>


#### <a name="DEL9712FD6D"></a>DeleteBuffers(int n, uint buffers) : void
<small>`Static`</small>


#### <a name="DELFD1B65F0"></a>DeleteBuffer(uint buffer) : void
<small>`Static`</small>


#### <a name="DEL77999AF9"></a>DeleteBuffer(ref uint buffer) : void
<small>`Static`</small>


#### <a name="ISBB302646D"></a>IsBuffer(uint buffer) : bool
<small>`Static`</small>


#### <a name="BIN2A172B77"></a>BindBuffer([BufferTarget](Heirloom.OpenGLES.BufferTarget.md) target, uint buffer) : void
<small>`Static`</small>


#### <a name="BIN2BBFCE18"></a>BindBufferBase([BufferTarget](Heirloom.OpenGLES.BufferTarget.md) target, uint index, uint buffer) : void
<small>`Static`</small>


#### <a name="GET98A47B5C"></a>GetBufferParameters([BufferTarget](Heirloom.OpenGLES.BufferTarget.md) index, [BufferParameter](Heirloom.OpenGLES.BufferParameter.md) parameter, int result) : void
<small>`Static`</small>


#### <a name="BUF6DF5024A"></a>BufferData([BufferTarget](Heirloom.OpenGLES.BufferTarget.md) target, uint size, IntPtr data, [BufferUsage](Heirloom.OpenGLES.BufferUsage.md) usage = Static) : void
<small>`Static`</small>


#### <a name="BUF64D089C5"></a>BufferSubData([BufferTarget](Heirloom.OpenGLES.BufferTarget.md) target, uint offset, uint size, IntPtr data) : void
<small>`Static`</small>


#### <a name="BUFA5FDB53C"></a>BufferData<T>([BufferTarget](Heirloom.OpenGLES.BufferTarget.md) target, T data, [BufferUsage](Heirloom.OpenGLES.BufferUsage.md) usage = Static) : void
<small>`Static`</small>


#### <a name="BUFC3A60331"></a>BufferSubData<T>([BufferTarget](Heirloom.OpenGLES.BufferTarget.md) target, uint offset, T data) : void
<small>`Static`</small>


#### <a name="MAP5C24FD46"></a>MapBufferRange([BufferTarget](Heirloom.OpenGLES.BufferTarget.md) target, int offset, int length, [MapBufferAccess](Heirloom.OpenGLES.MapBufferAccess.md) access) : void
<small>`Static`</small>


#### <a name="UNMEBE941BD"></a>UnmapBuffer([BufferTarget](Heirloom.OpenGLES.BufferTarget.md) target) : bool
<small>`Static`</small>


#### <a name="FLU97281B0E"></a>FlushMappedBufferRange([BufferTarget](Heirloom.OpenGLES.BufferTarget.md) target, int offset, int size) : void
<small>`Static`</small>


#### <a name="GEN8B47E6F2"></a>GenVertexArray() : uint
<small>`Static`</small>

#### <a name="GEN29B3B4C4"></a>GenVertexArrays(int n) : uint
<small>`Static`</small>


#### <a name="DEL7794F3D4"></a>DeleteVertexArrays(int n, uint vaos) : void
<small>`Static`</small>


#### <a name="DELECF8CDD5"></a>DeleteVertexArray(uint vao) : void
<small>`Static`</small>


#### <a name="DELE11B9748"></a>DeleteVertexArray(ref uint vao) : void
<small>`Static`</small>


#### <a name="ISV38642E68"></a>IsVertexArray(uint vao) : void
<small>`Static`</small>


#### <a name="BIN218FCA7D"></a>BindVertexArray(uint vao) : void
<small>`Static`</small>


#### <a name="GENE0A8E3DE"></a>GenFramebuffer() : uint
<small>`Static`</small>

#### <a name="GEN64E5C1B0"></a>GenFramebuffers(int n) : uint
<small>`Static`</small>


#### <a name="DEL3D26BE68"></a>DeleteFramebuffers(int n, uint buffers) : void
<small>`Static`</small>


#### <a name="DEL1DD82D49"></a>DeleteFramebuffer(uint buffer) : void
<small>`Static`</small>


#### <a name="DELE285B76E"></a>DeleteFramebuffer(ref uint buffer) : void
<small>`Static`</small>


#### <a name="ISFBC215260"></a>IsFramebuffer(uint buffer) : bool
<small>`Static`</small>


#### <a name="BIN98604BBC"></a>BindFramebuffer([FramebufferTarget](Heirloom.OpenGLES.FramebufferTarget.md) target, uint buffer) : void
<small>`Static`</small>


#### <a name="GET271E4E6"></a>GetFramebufferAttachmentParameters([FramebufferTarget](Heirloom.OpenGLES.FramebufferTarget.md) target, [FramebufferAttachment](Heirloom.OpenGLES.FramebufferAttachment.md) attachment, [FramebufferAttachmentParameter](Heirloom.OpenGLES.FramebufferAttachmentParameter.md) parameter, int result) : void
<small>`Static`</small>


#### <a name="CHE96E86D25"></a>CheckFramebufferStatus([FramebufferTarget](Heirloom.OpenGLES.FramebufferTarget.md) target) : [FramebufferStatus](Heirloom.OpenGLES.FramebufferStatus.md)
<small>`Static`</small>


#### <a name="FRA1D14E791"></a>FramebufferTexture2D([FramebufferTarget](Heirloom.OpenGLES.FramebufferTarget.md) target, [FramebufferAttachment](Heirloom.OpenGLES.FramebufferAttachment.md) attachment, [TextureImageTarget](Heirloom.OpenGLES.TextureImageTarget.md) texTarget, uint tex, int mip) : void
<small>`Static`</small>


#### <a name="FRA5CC51B96"></a>FramebufferTextureLayer([FramebufferTarget](Heirloom.OpenGLES.FramebufferTarget.md) target, [FramebufferAttachment](Heirloom.OpenGLES.FramebufferAttachment.md) attachment, uint tex, int mip, int layer) : void
<small>`Static`</small>


#### <a name="FRA4B5C45AC"></a>FramebufferRenderbuffer([FramebufferTarget](Heirloom.OpenGLES.FramebufferTarget.md) target, [FramebufferAttachment](Heirloom.OpenGLES.FramebufferAttachment.md) attachment, uint renderbuffer) : void
<small>`Static`</small>


#### <a name="BLI79F590BD"></a>BlitFramebuffer(int srcX0, int srcY0, int srcX1, int srcY1, int dstX0, int dstY0, int dstX1, int dstY1, [FramebufferBlitMask](Heirloom.OpenGLES.FramebufferBlitMask.md) mask, [FramebufferBlitFilter](Heirloom.OpenGLES.FramebufferBlitFilter.md) filter) : void
<small>`Static`</small>


#### <a name="INV82945495"></a>InvalidateFramebuffer([FramebufferAttachment[]](Heirloom.OpenGLES.FramebufferAttachment.md) attachments) : void
<small>`Static`</small>


#### <a name="INVB4C7B719"></a>InvalidateSubFramebuffer([FramebufferAttachment[]](Heirloom.OpenGLES.FramebufferAttachment.md) attachments, int x, int y, int width, int height) : void
<small>`Static`</small>


#### <a name="GEN947CF017"></a>GenRenderbuffer() : uint
<small>`Static`</small>

#### <a name="GEN66301189"></a>GenRenderbuffers(int n) : uint
<small>`Static`</small>


#### <a name="DEL7FA54C8"></a>DeleteRenderbuffer(uint buffer) : void
<small>`Static`</small>


#### <a name="DEL55B962EF"></a>DeleteRenderbuffer(ref uint buffer) : void
<small>`Static`</small>


#### <a name="DELC12674AB"></a>DeleteRenderbuffers(int n, uint buffers) : void
<small>`Static`</small>


#### <a name="ISRF0CD12EF"></a>IsRenderbuffer(uint buffer) : bool
<small>`Static`</small>


#### <a name="REND112FFF6"></a>RenderbufferStorage([RenderbufferFormat](Heirloom.OpenGLES.RenderbufferFormat.md) format, int width, int height) : void
<small>`Static`</small>


#### <a name="REN15CFF63B"></a>RenderbufferStorage([RenderbufferFormat](Heirloom.OpenGLES.RenderbufferFormat.md) format, int width, int height, int samples = 0) : void
<small>`Static`</small>


#### <a name="GET3E5A075F"></a>GetRenderbufferParameter([RenderbufferValue](Heirloom.OpenGLES.RenderbufferValue.md) parameter, int result) : void
<small>`Static`</small>


#### <a name="BIN58A7A0E8"></a>BindRenderbuffer(uint renderbuffer) : void
<small>`Static`</small>


#### <a name="GEN37433AFE"></a>GenTexture() : uint
<small>`Static`</small>

#### <a name="GEN3EBCE22C"></a>GenTextures(int n) : uint
<small>`Static`</small>


#### <a name="DELE1902C35"></a>DeleteTextures(int n, uint textures) : void
<small>`Static`</small>


#### <a name="DEL217BDF6"></a>DeleteTexture(uint texture) : void
<small>`Static`</small>


#### <a name="DELEB4ADE79"></a>DeleteTexture(ref uint texture) : void
<small>`Static`</small>


#### <a name="IST7CB95371"></a>IsTexture(uint texture) : bool
<small>`Static`</small>


#### <a name="BIN71154183"></a>BindTexture([TextureTarget](Heirloom.OpenGLES.TextureTarget.md) target, uint texture) : void
<small>`Static`</small>


#### <a name="ACT628B5865"></a>ActiveTexture(uint texture) : void
<small>`Static`</small>


#### <a name="GET5B8C26B8"></a>GetTextureParameters([TextureTarget](Heirloom.OpenGLES.TextureTarget.md) index, [TextureParameter](Heirloom.OpenGLES.TextureParameter.md) parameter, float result) : void
<small>`Static`</small>


#### <a name="GET7C1592F5"></a>GetTextureParameters([TextureTarget](Heirloom.OpenGLES.TextureTarget.md) index, [TextureParameter](Heirloom.OpenGLES.TextureParameter.md) parameter, int result) : void
<small>`Static`</small>


#### <a name="LOA68F1E672"></a>LoadFunctions([GL.GetProcAddress](Heirloom.OpenGLES.GL.GetProcAddress.md) getProcAddress) : void
<small>`Static`</small>


#### <a name="CRE1483BD0C"></a>CreateShader([ShaderType](Heirloom.OpenGLES.ShaderType.md) type) : uint
<small>`Static`</small>


#### <a name="DEL21BC8470"></a>DeleteShader(uint shader) : void
<small>`Static`</small>


#### <a name="COM1CBF07C2"></a>CompileShader(uint shader) : void
<small>`Static`</small>


#### <a name="SHAFBB5810E"></a>ShaderSource(uint shader, string source) : void
<small>`Static`</small>


#### <a name="GETEC4B8CFD"></a>GetShader(uint shader, [ShaderParameter](Heirloom.OpenGLES.ShaderParameter.md) name) : int
<small>`Static`</small>


#### <a name="GETA56DD7D4"></a>GetShaderInfoLog(uint shader) : string
<small>`Static`</small>


#### <a name="CRE59931C79"></a>CreateProgram() : uint
<small>`Static`</small>

#### <a name="DEL832B292C"></a>DeleteProgram(uint program) : void
<small>`Static`</small>


#### <a name="DEL756563F9"></a>DeleteProgram(ref uint program) : void
<small>`Static`</small>


#### <a name="ISP28618AC1"></a>IsProgram(uint program) : bool
<small>`Static`</small>


#### <a name="LINA06AEF85"></a>LinkProgram(uint program) : void
<small>`Static`</small>


#### <a name="VAL2F630AFB"></a>ValidateProgram(uint program) : void
<small>`Static`</small>


#### <a name="ATT7E3D38E"></a>AttachShader(uint program, uint shader) : void
<small>`Static`</small>


#### <a name="DET189EDDF4"></a>DetachShader(uint program, uint shader) : void
<small>`Static`</small>


#### <a name="USE1B4AB48"></a>UseProgram(uint program) : void
<small>`Static`</small>


#### <a name="BINAC8699CB"></a>BindAttribLocation(uint program, uint index, string name) : void
<small>`Static`</small>


#### <a name="GETB740DDD1"></a>GetFragDataLocation(uint program, string name) : int
<small>`Static`</small>


#### <a name="GETDA25DA0D"></a>GetProgram(uint program, [ProgramParameter](Heirloom.OpenGLES.ProgramParameter.md) property) : int
<small>`Static`</small>

Get a value about the given program from the GL implementation.

<small>**program**: <param name="program"> A valid program name </param></small>  
<small>**property**: <param name="property"> The type of property to request </param></small>  

#### <a name="GET6633CB5A"></a>GetProgramInfoLog(uint program) : string
<small>`Static`</small>


#### <a name="GET47E2B245"></a>GetActiveAttribute(uint program, uint index) : [ActiveAttribute](Heirloom.OpenGLES.ActiveAttribute.md)
<small>`Static`</small>


#### <a name="GET30E4D7C0"></a>GetActiveAttributes(uint program) : [ActiveAttribute[]](Heirloom.OpenGLES.ActiveAttribute.md)
<small>`Static`</small>


#### <a name="GET22B856E9"></a>GetActiveUniform(uint program, uint index) : [ActiveUniform](Heirloom.OpenGLES.ActiveUniform.md)
<small>`Static`</small>


#### <a name="GET99D0186C"></a>GetActiveUniforms(uint program) : [ActiveUniform[]](Heirloom.OpenGLES.ActiveUniform.md)
<small>`Static`</small>


#### <a name="GETA4109E8F"></a>GetAttribLocation(uint program, string name) : int
<small>`Static`</small>


#### <a name="GET39135FAF"></a>GetUniformLocation(uint program, string name) : int
<small>`Static`</small>


#### <a name="GETA72AEAA1"></a>GetActiveUniformBlockName(uint program, uint blockIndex) : string
<small>`Static`</small>


#### <a name="GET20570024"></a>GetActiveUniformBlockIndex(uint program, string name) : uint
<small>`Static`</small>


#### <a name="GETC9511261"></a>GetActiveUniform(uint program, uint uniformIndices, [UniformParameter](Heirloom.OpenGLES.UniformParameter.md) pname, int result) : void
<small>`Static`</small>


#### <a name="GETA82108AA"></a>GetActiveUniformBlock(uint program, uint index) : [ActiveUniformBlock](Heirloom.OpenGLES.ActiveUniformBlock.md)
<small>`Static`</small>


#### <a name="GET47BE8CA5"></a>GetActiveUniformBlocks(uint program) : [ActiveUniformBlock[]](Heirloom.OpenGLES.ActiveUniformBlock.md)
<small>`Static`</small>


#### <a name="GET690765A"></a>GetActiveUniformBlock(uint program, uint blockIndex, [UniformBlockParameter](Heirloom.OpenGLES.UniformBlockParameter.md) pname, int result) : void
<small>`Static`</small>


#### <a name="GETE6975292"></a>GetActiveUniformBlock(uint program, uint blockIndex, [UniformBlockParameter](Heirloom.OpenGLES.UniformBlockParameter.md) pname, out int value) : void
<small>`Static`</small>


#### <a name="UNIAE68321C"></a>UniformBlockBinding(uint program, uint index, uint binding) : void
<small>`Static`</small>


#### <a name="UNID38C1B8C"></a>Uniform1(int location, float v) : void
<small>`Static`</small>


#### <a name="UNI676DEA4"></a>Uniform2(int location, float v1, float v2) : void
<small>`Static`</small>


#### <a name="UNIE807AF28"></a>Uniform3(int location, float v1, float v2, float v3) : void
<small>`Static`</small>


#### <a name="UNI9F0BFE21"></a>Uniform4(int location, float v1, float v2, float v3, float v4) : void
<small>`Static`</small>


#### <a name="UNIB2D07BCD"></a>Uniform1(int location, int v) : void
<small>`Static`</small>


#### <a name="UNI59918B04"></a>Uniform2(int location, int v1, int v2) : void
<small>`Static`</small>


#### <a name="UNIBF938047"></a>Uniform3(int location, int v1, int v2, int v3) : void
<small>`Static`</small>


#### <a name="UNIBA3FEEA1"></a>Uniform4(int location, int v1, int v2, int v3, int v4) : void
<small>`Static`</small>


#### <a name="UNI86BB5AA0"></a>Uniform1(int location, uint v) : void
<small>`Static`</small>


#### <a name="UNI2CEB812A"></a>Uniform2(int location, uint v1, uint v2) : void
<small>`Static`</small>


#### <a name="UNIA6665920"></a>Uniform3(int location, uint v1, uint v2, uint v3) : void
<small>`Static`</small>


#### <a name="UNIC5A3E35"></a>Uniform4(int location, uint v1, uint v2, uint v3, uint v4) : void
<small>`Static`</small>


#### <a name="UNIF5CAC555"></a>Uniform1(int location, int count, float arr) : void
<small>`Static`</small>


#### <a name="UNI8B32D506"></a>Uniform2(int location, int count, float arr) : void
<small>`Static`</small>


#### <a name="UNI60F2064B"></a>Uniform3(int location, int count, float arr) : void
<small>`Static`</small>


#### <a name="UNI4A7878FC"></a>Uniform4(int location, int count, float arr) : void
<small>`Static`</small>


#### <a name="UNI40E12FED"></a>Uniform1(int location, float arr) : void
<small>`Static`</small>


#### <a name="UNI51956B08"></a>Uniform2(int location, float arr) : void
<small>`Static`</small>


#### <a name="UNIADE2A23"></a>Uniform3(int location, float arr) : void
<small>`Static`</small>


#### <a name="UNIDDE9B43E"></a>Uniform4(int location, float arr) : void
<small>`Static`</small>


#### <a name="UNI4C389086"></a>Uniform1(int location, int count, int arr) : void
<small>`Static`</small>


#### <a name="UNIB5B904F5"></a>Uniform2(int location, int count, int arr) : void
<small>`Static`</small>


#### <a name="UNI790D9AD0"></a>Uniform3(int location, int count, int arr) : void
<small>`Static`</small>


#### <a name="UNIC5068D17"></a>Uniform4(int location, int count, int arr) : void
<small>`Static`</small>


#### <a name="UNI11547E8A"></a>Uniform1(int location, int arr) : void
<small>`Static`</small>


#### <a name="UNI206F1085"></a>Uniform2(int location, int arr) : void
<small>`Static`</small>


#### <a name="UNIC3F54C80"></a>Uniform3(int location, int arr) : void
<small>`Static`</small>


#### <a name="UNI50B8087B"></a>Uniform4(int location, int arr) : void
<small>`Static`</small>


#### <a name="UNI24B5B267"></a>Uniform1(int location, int count, uint arr) : void
<small>`Static`</small>


#### <a name="UNI62FDEF2C"></a>Uniform2(int location, int count, uint arr) : void
<small>`Static`</small>


#### <a name="UNI463EB5F1"></a>Uniform3(int location, int count, uint arr) : void
<small>`Static`</small>


#### <a name="UNI47E306B6"></a>Uniform4(int location, int count, uint arr) : void
<small>`Static`</small>


#### <a name="UNIDF535B17"></a>Uniform1(int location, uint arr) : void
<small>`Static`</small>


#### <a name="UNIB8239BD2"></a>Uniform2(int location, uint arr) : void
<small>`Static`</small>


#### <a name="UNIBE85690D"></a>Uniform3(int location, uint arr) : void
<small>`Static`</small>


#### <a name="UNIF9B322C8"></a>Uniform4(int location, uint arr) : void
<small>`Static`</small>


#### <a name="UNIC3B4E83A"></a>UniformMatrix2x2(int location, float values) : void
<small>`Static`</small>


#### <a name="UNI11C380FF"></a>UniformMatrix2x3(int location, float values) : void
<small>`Static`</small>


#### <a name="UNI62EE6D30"></a>UniformMatrix2x4(int location, float values) : void
<small>`Static`</small>


#### <a name="UNI8881B35F"></a>UniformMatrix3x2(int location, float values) : void
<small>`Static`</small>


#### <a name="UNI64089A9A"></a>UniformMatrix3x3(int location, float values) : void
<small>`Static`</small>


#### <a name="UNI80118255"></a>UniformMatrix3x4(int location, float values) : void
<small>`Static`</small>


#### <a name="UNID2FBBBF0"></a>UniformMatrix4x2(int location, float values) : void
<small>`Static`</small>


#### <a name="UNI1D78C5B5"></a>UniformMatrix4x3(int location, float values) : void
<small>`Static`</small>


#### <a name="UNIA2D2E5FA"></a>UniformMatrix4x4(int location, float values) : void
<small>`Static`</small>


#### <a name="UNI85E0DB10"></a>UniformMatrix2x2(int location, int count, float ptr) : void
<small>`Static`</small>


#### <a name="UNI1C0939D5"></a>UniformMatrix2x3(int location, int count, float ptr) : void
<small>`Static`</small>


#### <a name="UNI3C3893AE"></a>UniformMatrix2x4(int location, int count, float ptr) : void
<small>`Static`</small>


#### <a name="UNI7AA7C3B5"></a>UniformMatrix3x2(int location, int count, float ptr) : void
<small>`Static`</small>


