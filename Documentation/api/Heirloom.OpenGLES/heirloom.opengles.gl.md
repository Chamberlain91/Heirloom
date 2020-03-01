# Heirloom.OpenGLES

<small>**Framework**: .NETStandard,Version=v2.1</small>  
<small>**Assembly**: [Heirloom.OpenGLES](../heirloom.opengles/heirloom.opengles.md)</small>  

## GL (Static Class)
<small>**Namespace**: Heirloom.OpenGLES</sub></small>  

| Properties | Summary |
|------------|---------|
| [HasLoadedFunctions](#HAS31777ABC) |  |

| Methods | Summary |
|---------|---------|
| [WaitSync](#WAIA76D96C) |  |
| [DeleteSync](#DEL7FFEDED) |  |
| [IsSync](#ISS7737C2DC) |  |
| [SetTextureParameter](#SETF327D347) |  |
| [SetTextureParameter](#SET88B9DF76) |  |
| [SetTextureParameter](#SETF327D347) |  |
| [SetTextureParameter](#SET88B9DF76) |  |
| [TexStorage2D](#TEX146F91E4) |  |
| [TexStorage3D](#TEXA21E0AE7) |  |
| [TexImage2D](#TEX42DDBBCF) |  |
| [TexImage3D](#TEX9E09CC2E) |  |
| [TexSubImage2D](#TEX370280F8) |  |
| [TexSubImage3D](#TEX3E7673AB) |  |
| [CompressedTexImage2D](#COMBF79352D) |  |
| [CompressedTexImage3D](#COM8C1B325E) |  |
| [CompressedTexSubImage2D](#COMF3CCB8C9) |  |
| [CompressedTexSubImage3D](#COM59F89EA8) |  |
| [TexImage2D<T>](#TEX4AB8E7EE) |  |
| [TexImage3D<T>](#TEX2732107F) |  |
| [TexSubImage2D<T>](#TEXC0A48EFF) |  |
| [TexSubImage3D<T>](#TEX717373AC) |  |
| [GenerateMipmap](#GENCFFE251F) |  |
| [GenSampler](#GEN462995) |  |
| [GenSamplers](#GEN8D0AA7CB) |  |
| [DeleteSamplers](#DEL351E55A) |  |
| [DeleteSampler](#DELC60B2B6C) |  |
| [DeleteSampler](#DELAA6C8679) |  |
| [IsSampler](#ISSBBFD1881) |  |
| [BindSampler](#BINE903EA60) |  |
| [GetSamplerParameters](#GET12FDE9CF) |  |
| [GetSamplerParameters](#GET4BD93F00) |  |
| [SamplerParameter](#SAMCD44037E) |  |
| [SamplerParameter](#SAM4499C92F) |  |
| [SamplerParameter](#SAMCD44037E) |  |
| [SamplerParameter](#SAM4499C92F) |  |
| [Enable](#ENA436E9529) |  |
| [Disable](#DIS85A87DBA) |  |
| [IsEnabled](#ISECB3090C5) |  |
| [Clear](#CLEC0FA3C72) | Clears the buffers of the currently bound framebuffer specified by the mask. |
| [SetViewport](#SETBA21462D) |  |
| [SetScissor](#SET65FFEF41) |  |
| [FrontFace](#FRO82BA0658) |  |
| [SetCullFace](#SET727A9225) |  |
| [SetColorMask](#SETBFE6F35C) |  |
| [SetClearColor](#SET9C46BD97) |  |
| [SetClearColor](#SET9793B0D2) |  |
| [SetDepthMask](#SET516FF28F) |  |
| [SetDepthFunction](#SET473483D8) |  |
| [SetDepthRange](#SETBA1C5DE) |  |
| [SetClearDepth](#SETD0F3CAA) |  |
| [SetClearStencil](#SET2A097CEF) |  |
| [SetLineWidth](#SET527194B) |  |
| [SetHint](#SETEBBC03EA) |  |
| [SetSampleCoverage](#SET782157EC) |  |
| [SetPolygonOffset](#SET48B4C01C) |  |
| [SetPixelStore](#SETBF869713) |  |
| [SetReadBuffer](#SETAADC3FC8) | Changes the read buffer ( where reading operations occur ) |
| [SetDrawBuffers](#SET3D57A005) | Changes the read buffer ( where reading operations occur ) |
| [ReadPixels](#REA8FA00479) | Reads a block of pixels from the frame buffer. |
| [ReadPixels](#REAF7A3A33) | Reads a block of pixels from the frame buffer. |
| [ReadPixels](#REA830C853A) | Reads a block of pixels from the frame buffer. ( ReadPixelsFormat.RGBA and ReadPixelsType.UnsignedByte ) |
| [Finish](#FIN18529F62) |  |
| [Flush](#FLU2F0EB18F) |  |
| [SetBlendColor](#SET4095588B) |  |
| [SetBlendEquation](#SET502EB9E0) |  |
| [SetBlendEquation](#SETCF67A1D5) |  |
| [SetBlendFunction](#SET5B58E9B3) |  |
| [SetBlendFunction](#SETB60FE390) |  |
| [StencilFunction](#STE1CEFE43C) |  |
| [StencilFunction](#STE672D3347) |  |
| [SetStencilMask](#SET61F916D5) |  |
| [SetStencilMask](#SET7F65BACA) |  |
| [StencilOperation](#STE357F0A45) |  |
| [StencilOperation](#STE6BB52A4E) |  |
| [GenQueries](#GENE21F0AA0) |  |
| [GenQuery](#GENE8768477) |  |
| [DeleteQueries](#DELB61E69FC) |  |
| [DeleteQuery](#DELFBBF1CA8) |  |
| [DeleteQuery](#DEL8A27C005) |  |
| [BeginQuery](#BEGFC21D6ED) |  |
| [EndQuery](#END99BDA569) |  |
| [GetQuery](#GET277F60E) |  |
| [GetQueryObject](#GETB6BDDF4C) |  |
| [IsQuery](#ISQD3799C15) |  |
| [FenceSync](#FEN7939A174) |  |
| [UniformMatrix3x3](#UNI351C4D70) |  |
| [UniformMatrix3x4](#UNI8E0B2B53) |  |
| [UniformMatrix4x2](#UNICCA976E) |  |
| [UniformMatrix4x3](#UNIC92EAC33) |  |
| [UniformMatrix4x4](#UNI8CAA1ED0) |  |
| [GetString](#GETAA4AF7A4) |  |
| [GetBoolean](#GETEF3009BC) |  |
| [GetBoolean](#GET52FAD36E) |  |
| [GetInteger](#GETC43F9145) |  |
| [GetInternalformat](#GET9B26C3BA) |  |
| [GetIntegers](#GET226C3ABA) |  |
| [GetIntegers](#GETE9DF8B72) |  |
| [GetFloat](#GETCC72504) |  |
| [GetFloat](#GET3D100464) |  |
| [DrawArrays](#DRA62B40A1A) |  |
| [DrawArrays](#DRA5B9495D9) |  |
| [DrawElements](#DRA33A3AED6) |  |
| [DrawRangeElements](#DRA2B83E8D6) |  |
| [DrawArraysInstanced](#DRA315A0797) |  |
| [DrawArraysInstanced](#DRA46746B9E) |  |
| [DrawElementsInstanced](#DRA9DA026D1) |  |
| [EnableVertexAttribArray](#ENA7ED71A03) |  |
| [DisableVertexAttribArray](#DIS2BC74CD2) |  |
| [SetVertexAttribPointer](#SETFB11A279) |  |
| [SetVertexAttribPointer](#SET724D2D34) |  |
| [SetVertexAttribDivisor](#SET80B8FA60) |  |
| [GenBuffer](#GENDF648929) |  |
| [GenBuffers](#GENB8D06BEF) |  |
| [DeleteBuffers](#DEL9712FD6D) |  |
| [DeleteBuffer](#DELFD1B65F0) |  |
| [DeleteBuffer](#DEL77999AF9) |  |
| [IsBuffer](#ISBB302646D) |  |
| [BindBuffer](#BIN13B4877) |  |
| [BindBufferBase](#BINB84A9458) |  |
| [GetBufferParameters](#GET8361479C) |  |
| [BufferData](#BUF84435822) |  |
| [BufferSubData](#BUF99CEB285) |  |
| [BufferData<T>](#BUF3C160480) |  |
| [BufferSubData<T>](#BUF7BB9D9F1) |  |
| [MapBufferRange](#MAP1A5D6166) |  |
| [UnmapBuffer](#UNMEAB083D) |  |
| [FlushMappedBufferRange](#FLU40B29C8E) |  |
| [GenVertexArray](#GEN8B47E6F2) |  |
| [GenVertexArrays](#GEN29B3B4C4) |  |
| [DeleteVertexArrays](#DEL7794F3D4) |  |
| [DeleteVertexArray](#DELECF8CDD5) |  |
| [DeleteVertexArray](#DELE11B9748) |  |
| [IsVertexArray](#ISV38642E68) |  |
| [BindVertexArray](#BIN218FCA7D) |  |
| [GenFramebuffer](#GENE0A8E3DE) |  |
| [GenFramebuffers](#GEN64E5C1B0) |  |
| [DeleteFramebuffers](#DEL3D26BE68) |  |
| [DeleteFramebuffer](#DEL1DD82D49) |  |
| [DeleteFramebuffer](#DELE285B76E) |  |
| [IsFramebuffer](#ISFBC215260) |  |
| [BindFramebuffer](#BIN52A93EFC) |  |
| [GetFramebufferAttachmentParameters](#GET54485CC6) |  |
| [CheckFramebufferStatus](#CHE638ECAA5) |  |
| [FramebufferTexture2D](#FRA2B0764F1) |  |
| [FramebufferTextureLayer](#FRAAFB76D56) |  |
| [FramebufferRenderbuffer](#FRA155CC06C) |  |
| [BlitFramebuffer](#BLI5612057D) |  |
| [InvalidateFramebuffer](#INVE31C3455) |  |
| [InvalidateSubFramebuffer](#INV15EC5559) |  |
| [GenRenderbuffer](#GEN947CF017) |  |
| [GenRenderbuffers](#GEN66301189) |  |
| [DeleteRenderbuffer](#DEL7FA54C8) |  |
| [DeleteRenderbuffer](#DEL55B962EF) |  |
| [DeleteRenderbuffers](#DELC12674AB) |  |
| [IsRenderbuffer](#ISRF0CD12EF) |  |
| [RenderbufferStorage](#REN329CD676) |  |
| [RenderbufferStorage](#RENBEC2E37B) |  |
| [GetRenderbufferParameter](#GET23521B1F) |  |
| [BindRenderbuffer](#BIN58A7A0E8) |  |
| [GenTexture](#GEN37433AFE) |  |
| [GenTextures](#GEN3EBCE22C) |  |
| [DeleteTextures](#DELE1902C35) |  |
| [DeleteTexture](#DEL217BDF6) |  |
| [DeleteTexture](#DELEB4ADE79) |  |
| [IsTexture](#IST7CB95371) |  |
| [BindTexture](#BIN52C35843) |  |
| [ActiveTexture](#ACT628B5865) |  |
| [GetTextureParameters](#GET1E9874F8) |  |
| [GetTextureParameters](#GETE37978B5) |  |
| [LoadFunctions](#LOAF6258752) |  |
| [CreateShader](#CRE99ABC54C) |  |
| [DeleteShader](#DEL21BC8470) |  |
| [CompileShader](#COM1CBF07C2) |  |
| [ShaderSource](#SHAFBB5810E) |  |
| [GetShader](#GETDD4C703D) |  |
| [GetShaderInfoLog](#GETA56DD7D4) |  |
| [CreateProgram](#CRE59931C79) |  |
| [DeleteProgram](#DEL832B292C) |  |
| [DeleteProgram](#DEL756563F9) |  |
| [IsProgram](#ISP28618AC1) |  |
| [LinkProgram](#LINA06AEF85) |  |
| [ValidateProgram](#VAL2F630AFB) |  |
| [AttachShader](#ATT7E3D38E) |  |
| [DetachShader](#DET189EDDF4) |  |
| [UseProgram](#USE1B4AB48) |  |
| [BindAttribLocation](#BINAC8699CB) |  |
| [GetFragDataLocation](#GETB740DDD1) |  |
| [GetProgram](#GETD70E374D) | Get a value about the given program from the GL implementation. |
| [GetProgramInfoLog](#GET6633CB5A) |  |
| [GetActiveAttribute](#GET3392AF85) |  |
| [GetActiveAttributes](#GETB5733180) |  |
| [GetActiveUniform](#GETDABDC229) |  |
| [GetActiveUniforms](#GETD3471DEC) |  |
| [GetAttribLocation](#GETA4109E8F) |  |
| [GetUniformLocation](#GET39135FAF) |  |
| [GetActiveUniformBlockName](#GETA72AEAA1) |  |
| [GetActiveUniformBlockIndex](#GET20570024) |  |
| [GetActiveUniform](#GET498F14E1) |  |
| [GetActiveUniformBlock](#GET1CE2780A) |  |
| [GetActiveUniformBlocks](#GETF20F5DC5) |  |
| [GetActiveUniformBlock](#GET1BA4FD3A) |  |
| [GetActiveUniformBlock](#GETD772A2B2) |  |
| [UniformBlockBinding](#UNIAE68321C) |  |
| [Uniform1](#UNID38C1B8C) |  |
| [Uniform2](#UNI676DEA4) |  |
| [Uniform3](#UNIE807AF28) |  |
| [Uniform4](#UNI9F0BFE21) |  |
| [Uniform1](#UNIB2D07BCD) |  |
| [Uniform2](#UNI59918B04) |  |
| [Uniform3](#UNIBF938047) |  |
| [Uniform4](#UNIBA3FEEA1) |  |
| [Uniform1](#UNI86BB5AA0) |  |
| [Uniform2](#UNI2CEB812A) |  |
| [Uniform3](#UNIA6665920) |  |
| [Uniform4](#UNIC5A3E35) |  |
| [Uniform1](#UNIF5CAC555) |  |
| [Uniform2](#UNI8B32D506) |  |
| [Uniform3](#UNI60F2064B) |  |
| [Uniform4](#UNI4A7878FC) |  |
| [Uniform1](#UNI40E12FED) |  |
| [Uniform2](#UNI51956B08) |  |
| [Uniform3](#UNIADE2A23) |  |
| [Uniform4](#UNIDDE9B43E) |  |
| [Uniform1](#UNI4C389086) |  |
| [Uniform2](#UNIB5B904F5) |  |
| [Uniform3](#UNI790D9AD0) |  |
| [Uniform4](#UNIC5068D17) |  |
| [Uniform1](#UNI11547E8A) |  |
| [Uniform2](#UNI206F1085) |  |
| [Uniform3](#UNIC3F54C80) |  |
| [Uniform4](#UNI50B8087B) |  |
| [Uniform1](#UNI24B5B267) |  |
| [Uniform2](#UNI62FDEF2C) |  |
| [Uniform3](#UNI463EB5F1) |  |
| [Uniform4](#UNI47E306B6) |  |
| [Uniform1](#UNIDF535B17) |  |
| [Uniform2](#UNIB8239BD2) |  |
| [Uniform3](#UNIBE85690D) |  |
| [Uniform4](#UNIF9B322C8) |  |
| [UniformMatrix2x2](#UNIC3B4E83A) |  |
| [UniformMatrix2x3](#UNI11C380FF) |  |
| [UniformMatrix2x4](#UNI62EE6D30) |  |
| [UniformMatrix3x2](#UNI8881B35F) |  |
| [UniformMatrix3x3](#UNI64089A9A) |  |
| [UniformMatrix3x4](#UNI80118255) |  |
| [UniformMatrix4x2](#UNID2FBBBF0) |  |
| [UniformMatrix4x3](#UNI1D78C5B5) |  |
| [UniformMatrix4x4](#UNIA2D2E5FA) |  |
| [UniformMatrix2x2](#UNI85E0DB10) |  |
| [UniformMatrix2x3](#UNI1C0939D5) |  |
| [UniformMatrix2x4](#UNI3C3893AE) |  |
| [UniformMatrix3x2](#UNI7AA7C3B5) |  |

### Properties

#### <a name="HAS31777ABC"></a>HasLoadedFunctions : bool

<small>`Static`, `Read Only`</small>

### Methods

#### <a name="WAIA76D96C"></a>WaitSync(ulong sync, [WaitSyncFlags](heirloom.opengles.waitsyncflags.md) flags) : void

<small>`Static`</small>


#### <a name="DEL7FFEDED"></a>DeleteSync(ulong sync) : void

<small>`Static`</small>


#### <a name="ISS7737C2DC"></a>IsSync(ulong sync) : bool

<small>`Static`</small>


#### <a name="SETF327D347"></a>SetTextureParameter([TextureTarget](heirloom.opengles.texturetarget.md) index, [TextureParameter](heirloom.opengles.textureparameter.md) parameter, float value) : void

<small>`Static`</small>


#### <a name="SET88B9DF76"></a>SetTextureParameter([TextureTarget](heirloom.opengles.texturetarget.md) index, [TextureParameter](heirloom.opengles.textureparameter.md) parameter, int value) : void

<small>`Static`</small>


#### <a name="SETF327D347"></a>SetTextureParameter([TextureTarget](heirloom.opengles.texturetarget.md) index, [TextureParameter](heirloom.opengles.textureparameter.md) parameter, float value) : void

<small>`Static`</small>


#### <a name="SET88B9DF76"></a>SetTextureParameter([TextureTarget](heirloom.opengles.texturetarget.md) index, [TextureParameter](heirloom.opengles.textureparameter.md) parameter, int value) : void

<small>`Static`</small>


#### <a name="TEX146F91E4"></a>TexStorage2D([TextureImageTarget](heirloom.opengles.textureimagetarget.md) target, int levels, [TextureSizedFormat](heirloom.opengles.texturesizedformat.md) format, int width, int height) : void

<small>`Static`</small>


#### <a name="TEXA21E0AE7"></a>TexStorage3D([TextureImageTarget](heirloom.opengles.textureimagetarget.md) target, int levels, [TextureSizedFormat](heirloom.opengles.texturesizedformat.md) format, int width, int height, int depth) : void

<small>`Static`</small>


#### <a name="TEX42DDBBCF"></a>TexImage2D([TextureImageTarget](heirloom.opengles.textureimagetarget.md) target, int level, [TextureSizedFormat](heirloom.opengles.texturesizedformat.md) internalFormat, int width, int height, [TexturePixelFormat](heirloom.opengles.texturepixelformat.md) format, [TexturePixelType](heirloom.opengles.texturepixeltype.md) pixelFormat, IntPtr data) : void

<small>`Static`</small>


#### <a name="TEX9E09CC2E"></a>TexImage3D([TextureImageTarget](heirloom.opengles.textureimagetarget.md) target, int level, [TextureSizedFormat](heirloom.opengles.texturesizedformat.md) internalFormat, int width, int height, int depth, [TexturePixelFormat](heirloom.opengles.texturepixelformat.md) format, [TexturePixelType](heirloom.opengles.texturepixeltype.md) pixelFormat, IntPtr data) : void

<small>`Static`</small>


#### <a name="TEX370280F8"></a>TexSubImage2D([TextureImageTarget](heirloom.opengles.textureimagetarget.md) target, int level, int xoffset, int yoffset, int width, int height, [TexturePixelFormat](heirloom.opengles.texturepixelformat.md) format, [TexturePixelType](heirloom.opengles.texturepixeltype.md) pixelFormat, IntPtr data) : void

<small>`Static`</small>


#### <a name="TEX3E7673AB"></a>TexSubImage3D([TextureImageTarget](heirloom.opengles.textureimagetarget.md) target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, [TexturePixelFormat](heirloom.opengles.texturepixelformat.md) format, [TexturePixelType](heirloom.opengles.texturepixeltype.md) pixelFormat, IntPtr data) : void

<small>`Static`</small>


#### <a name="COMBF79352D"></a>CompressedTexImage2D([TextureImageTarget](heirloom.opengles.textureimagetarget.md) target, int level, int width, int height, [TextureCompressedFormat](heirloom.opengles.texturecompressedformat.md) format, int size, IntPtr data) : void

<small>`Static`</small>


#### <a name="COM8C1B325E"></a>CompressedTexImage3D([TextureImageTarget](heirloom.opengles.textureimagetarget.md) target, int level, int width, int height, int depth, [TextureCompressedFormat](heirloom.opengles.texturecompressedformat.md) format, int size, IntPtr data) : void

<small>`Static`</small>


#### <a name="COMF3CCB8C9"></a>CompressedTexSubImage2D([TextureImageTarget](heirloom.opengles.textureimagetarget.md) target, int level, int xoffset, int yoffset, int width, int height, int size, IntPtr data) : void

<small>`Static`</small>


#### <a name="COM59F89EA8"></a>CompressedTexSubImage3D([TextureImageTarget](heirloom.opengles.textureimagetarget.md) target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, int size, IntPtr data) : void

<small>`Static`</small>


#### <a name="TEX4AB8E7EE"></a>TexImage2D<T>([TextureImageTarget](heirloom.opengles.textureimagetarget.md) target, int level, [TextureSizedFormat](heirloom.opengles.texturesizedformat.md) internalFormat, int width, int height, [TexturePixelFormat](heirloom.opengles.texturepixelformat.md) format, [TexturePixelType](heirloom.opengles.texturepixeltype.md) pixelFormat, T data) : void

<small>`Static`</small>


#### <a name="TEX2732107F"></a>TexImage3D<T>([TextureImageTarget](heirloom.opengles.textureimagetarget.md) target, int level, [TextureSizedFormat](heirloom.opengles.texturesizedformat.md) internalFormat, int width, int height, int depth, [TexturePixelFormat](heirloom.opengles.texturepixelformat.md) format, [TexturePixelType](heirloom.opengles.texturepixeltype.md) pixelFormat, T data) : void

<small>`Static`</small>


#### <a name="TEXC0A48EFF"></a>TexSubImage2D<T>([TextureImageTarget](heirloom.opengles.textureimagetarget.md) target, int level, int xoffset, int yoffset, int width, int height, [TexturePixelFormat](heirloom.opengles.texturepixelformat.md) format, [TexturePixelType](heirloom.opengles.texturepixeltype.md) pixelFormat, T data) : void

<small>`Static`</small>


#### <a name="TEX717373AC"></a>TexSubImage3D<T>([TextureImageTarget](heirloom.opengles.textureimagetarget.md) target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, [TexturePixelFormat](heirloom.opengles.texturepixelformat.md) format, [TexturePixelType](heirloom.opengles.texturepixeltype.md) pixelFormat, T data) : void

<small>`Static`</small>


#### <a name="GENCFFE251F"></a>GenerateMipmap([TextureTarget](heirloom.opengles.texturetarget.md) target) : void

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


#### <a name="GET12FDE9CF"></a>GetSamplerParameters(uint sampler, [TextureParameter](heirloom.opengles.textureparameter.md) parameter, float result) : void

<small>`Static`</small>


#### <a name="GET4BD93F00"></a>GetSamplerParameters(uint sampler, [TextureParameter](heirloom.opengles.textureparameter.md) parameter, int result) : void

<small>`Static`</small>


#### <a name="SAMCD44037E"></a>SamplerParameter(uint sampler, [TextureParameter](heirloom.opengles.textureparameter.md) parameter, float value) : void

<small>`Static`</small>


#### <a name="SAM4499C92F"></a>SamplerParameter(uint sampler, [TextureParameter](heirloom.opengles.textureparameter.md) parameter, int value) : void

<small>`Static`</small>


#### <a name="SAMCD44037E"></a>SamplerParameter(uint sampler, [TextureParameter](heirloom.opengles.textureparameter.md) parameter, float value) : void

<small>`Static`</small>


#### <a name="SAM4499C92F"></a>SamplerParameter(uint sampler, [TextureParameter](heirloom.opengles.textureparameter.md) parameter, int value) : void

<small>`Static`</small>


#### <a name="ENA436E9529"></a>Enable([EnableCap](heirloom.opengles.enablecap.md) enable) : void

<small>`Static`</small>


#### <a name="DIS85A87DBA"></a>Disable([EnableCap](heirloom.opengles.enablecap.md) enable) : void

<small>`Static`</small>


#### <a name="ISECB3090C5"></a>IsEnabled([EnableCap](heirloom.opengles.enablecap.md) enable) : bool

<small>`Static`</small>


#### <a name="CLEC0FA3C72"></a>Clear([ClearMask](heirloom.opengles.clearmask.md) mask) : void

<small>`Static`</small>

Clears the buffers of the currently bound framebuffer specified by the mask.


#### <a name="SETBA21462D"></a>SetViewport(int x, int y, int width, int height) : void

<small>`Static`</small>


#### <a name="SET65FFEF41"></a>SetScissor(int x, int y, int width, int height) : void

<small>`Static`</small>


#### <a name="FRO82BA0658"></a>FrontFace([FrontFaceMode](heirloom.opengles.frontfacemode.md) mode) : void

<small>`Static`</small>


#### <a name="SET727A9225"></a>SetCullFace([Face](heirloom.opengles.face.md) face) : void

<small>`Static`</small>


#### <a name="SETBFE6F35C"></a>SetColorMask(bool r, bool g, bool b, bool a) : void

<small>`Static`</small>


#### <a name="SET9C46BD97"></a>SetClearColor(float r, float g, float b, float a) : void

<small>`Static`</small>


#### <a name="SET9793B0D2"></a>SetClearColor(uint c) : void

<small>`Static`</small>


#### <a name="SET516FF28F"></a>SetDepthMask(bool depth) : void

<small>`Static`</small>


#### <a name="SET473483D8"></a>SetDepthFunction([DepthFunction](heirloom.opengles.depthfunction.md) func) : void

<small>`Static`</small>


#### <a name="SETBA1C5DE"></a>SetDepthRange(float near, float far) : void

<small>`Static`</small>


#### <a name="SETD0F3CAA"></a>SetClearDepth(float depth) : void

<small>`Static`</small>


#### <a name="SET2A097CEF"></a>SetClearStencil(int stencil) : void

<small>`Static`</small>


#### <a name="SET527194B"></a>SetLineWidth(float width) : void

<small>`Static`</small>


#### <a name="SETEBBC03EA"></a>SetHint([Hint](heirloom.opengles.hint.md) hint, [HintMode](heirloom.opengles.hintmode.md) mode) : void

<small>`Static`</small>


#### <a name="SET782157EC"></a>SetSampleCoverage(float val, bool invert) : void

<small>`Static`</small>


#### <a name="SET48B4C01C"></a>SetPolygonOffset(float factor, float units) : void

<small>`Static`</small>


#### <a name="SETBF869713"></a>SetPixelStore([PixelStoreParameter](heirloom.opengles.pixelstoreparameter.md) pname, int value) : void

<small>`Static`</small>


#### <a name="SETAADC3FC8"></a>SetReadBuffer([FramebufferBuffer](heirloom.opengles.framebufferbuffer.md) mode) : void

<small>`Static`</small>

Changes the read buffer ( where reading operations occur )

<small>**mode**: <param name="mode"> The read mode/target </param>  
</small>

#### <a name="SET3D57A005"></a>SetDrawBuffers([FramebufferBuffer[]](heirloom.opengles.framebufferbuffer.md) mode) : void

<small>`Static`</small>

Changes the read buffer ( where reading operations occur )

<small>**mode**: <param name="mode"> The read mode/target </param>  
</small>

#### <a name="REA8FA00479"></a>ReadPixels(int x, int y, int width, int height, [ReadPixelsFormat](heirloom.opengles.readpixelsformat.md) format, [ReadPixelsType](heirloom.opengles.readpixelstype.md) type, void data) : void

<small>`Static`</small>

Reads a block of pixels from the frame buffer.


#### <a name="REAF7A3A33"></a>ReadPixels(int x, int y, int width, int height, [ReadPixelsFormat](heirloom.opengles.readpixelsformat.md) format, [ReadPixelsType](heirloom.opengles.readpixelstype.md) type,  byte data) : void

<small>`Static`</small>

Reads a block of pixels from the frame buffer.


#### <a name="REA830C853A"></a>ReadPixels(int x, int y, int width, int height) : uint

<small>`Static`</small>

Reads a block of pixels from the frame buffer. ( ReadPixelsFormat.RGBA and ReadPixelsType.UnsignedByte )


#### <a name="FIN18529F62"></a>Finish() : void

<small>`Static`</small>

#### <a name="FLU2F0EB18F"></a>Flush() : void

<small>`Static`</small>

#### <a name="SET4095588B"></a>SetBlendColor(float r, float g, float b, float a) : void

<small>`Static`</small>


#### <a name="SET502EB9E0"></a>SetBlendEquation([BlendEquation](heirloom.opengles.blendequation.md) eq) : void

<small>`Static`</small>


#### <a name="SETCF67A1D5"></a>SetBlendEquation([BlendEquation](heirloom.opengles.blendequation.md) eqColor, [BlendEquation](heirloom.opengles.blendequation.md) eqAlpha) : void

<small>`Static`</small>


#### <a name="SET5B58E9B3"></a>SetBlendFunction([BlendFunction](heirloom.opengles.blendfunction.md) source, [BlendFunction](heirloom.opengles.blendfunction.md) destination) : void

<small>`Static`</small>


#### <a name="SETB60FE390"></a>SetBlendFunction([BlendFunction](heirloom.opengles.blendfunction.md) sourceColor, [BlendFunction](heirloom.opengles.blendfunction.md) destinationColor, [BlendFunction](heirloom.opengles.blendfunction.md) sourceAlpha, [BlendFunction](heirloom.opengles.blendfunction.md) destinationAlpha) : void

<small>`Static`</small>


#### <a name="STE1CEFE43C"></a>StencilFunction([StencilFunction](heirloom.opengles.stencilfunction.md) func, int refVal, uint mask) : void

<small>`Static`</small>


#### <a name="STE672D3347"></a>StencilFunction([Face](heirloom.opengles.face.md) face, [StencilFunction](heirloom.opengles.stencilfunction.md) func, int refVal, uint mask) : void

<small>`Static`</small>


#### <a name="SET61F916D5"></a>SetStencilMask(uint mask) : void

<small>`Static`</small>


#### <a name="SET7F65BACA"></a>SetStencilMask([Face](heirloom.opengles.face.md) face, uint mask) : void

<small>`Static`</small>


#### <a name="STE357F0A45"></a>StencilOperation([StencilOperation](heirloom.opengles.stenciloperation.md) stencilFail, [StencilOperation](heirloom.opengles.stenciloperation.md) depthFail, [StencilOperation](heirloom.opengles.stenciloperation.md) depthPass) : void

<small>`Static`</small>


#### <a name="STE6BB52A4E"></a>StencilOperation([Face](heirloom.opengles.face.md) face, [StencilOperation](heirloom.opengles.stenciloperation.md) stencilFail, [StencilOperation](heirloom.opengles.stenciloperation.md) depthFail, [StencilOperation](heirloom.opengles.stenciloperation.md) depthPass) : void

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


#### <a name="BEGFC21D6ED"></a>BeginQuery([QueryTarget](heirloom.opengles.querytarget.md) target, uint query) : void

<small>`Static`</small>


#### <a name="END99BDA569"></a>EndQuery([QueryTarget](heirloom.opengles.querytarget.md) target) : void

<small>`Static`</small>


#### <a name="GET277F60E"></a>GetQuery([QueryTarget](heirloom.opengles.querytarget.md) target, [QueryParameter](heirloom.opengles.queryparameter.md) pname, int values) : void

<small>`Static`</small>


#### <a name="GETB6BDDF4C"></a>GetQueryObject(uint query, [QueryObjectParameter](heirloom.opengles.queryobjectparameter.md) pname, out uint value) : void

<small>`Static`</small>


#### <a name="ISQD3799C15"></a>IsQuery(uint query) : bool

<small>`Static`</small>


#### <a name="FEN7939A174"></a>FenceSync([SyncFenceCondition](heirloom.opengles.syncfencecondition.md) condition, [SyncFenceFlags](heirloom.opengles.syncfenceflags.md) flags) : ulong

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


#### <a name="GETAA4AF7A4"></a>GetString([StringParameter](heirloom.opengles.stringparameter.md) name) : string

<small>`Static`</small>


#### <a name="GETEF3009BC"></a>GetBoolean([GetParameter](heirloom.opengles.getparameter.md) name) : bool

<small>`Static`</small>


#### <a name="GET52FAD36E"></a>GetBoolean([GetParameter](heirloom.opengles.getparameter.md) name, bool values) : void

<small>`Static`</small>


#### <a name="GETC43F9145"></a>GetInteger([GetParameter](heirloom.opengles.getparameter.md) name) : int

<small>`Static`</small>


#### <a name="GET9B26C3BA"></a>GetInternalformat([RenderbufferFormat](heirloom.opengles.renderbufferformat.md) renderBufferFormat, [InternalFormatParameter](heirloom.opengles.internalformatparameter.md) pname, int bufferSize = 16) : int

<small>`Static`</small>


#### <a name="GET226C3ABA"></a>GetIntegers([GetParameter](heirloom.opengles.getparameter.md) name, int values) : void

<small>`Static`</small>


#### <a name="GETE9DF8B72"></a>GetIntegers([GetParameter](heirloom.opengles.getparameter.md) name) : int

<small>`Static`</small>


#### <a name="GETCC72504"></a>GetFloat([GetParameter](heirloom.opengles.getparameter.md) name) : float

<small>`Static`</small>


#### <a name="GET3D100464"></a>GetFloat([GetParameter](heirloom.opengles.getparameter.md) name, float values) : void

<small>`Static`</small>


#### <a name="DRA62B40A1A"></a>DrawArrays([DrawMode](heirloom.opengles.drawmode.md) mode, int count) : void

<small>`Static`</small>


#### <a name="DRA5B9495D9"></a>DrawArrays([DrawMode](heirloom.opengles.drawmode.md) mode, int first, int count) : void

<small>`Static`</small>


#### <a name="DRA33A3AED6"></a>DrawElements([DrawMode](heirloom.opengles.drawmode.md) mode, int count, [DrawElementType](heirloom.opengles.drawelementtype.md) type, int offset = 0) : void

<small>`Static`</small>


#### <a name="DRA2B83E8D6"></a>DrawRangeElements([DrawMode](heirloom.opengles.drawmode.md) mode, int start, int end, int count, [DrawElementType](heirloom.opengles.drawelementtype.md) type, int offset = 0) : void

<small>`Static`</small>


#### <a name="DRA315A0797"></a>DrawArraysInstanced([DrawMode](heirloom.opengles.drawmode.md) mode, int count, int primCount) : void

<small>`Static`</small>


#### <a name="DRA46746B9E"></a>DrawArraysInstanced([DrawMode](heirloom.opengles.drawmode.md) mode, int first, int count, int primCount) : void

<small>`Static`</small>


#### <a name="DRA9DA026D1"></a>DrawElementsInstanced([DrawMode](heirloom.opengles.drawmode.md) mode, int count, [DrawElementType](heirloom.opengles.drawelementtype.md) type, int primCount, int offset = 0) : void

<small>`Static`</small>


#### <a name="ENA7ED71A03"></a>EnableVertexAttribArray(uint index) : void

<small>`Static`</small>


#### <a name="DIS2BC74CD2"></a>DisableVertexAttribArray(uint index) : void

<small>`Static`</small>


#### <a name="SETFB11A279"></a>SetVertexAttribPointer(uint index, int size, [VertexAttributeType](heirloom.opengles.vertexattributetype.md) type, bool normalized, int stride, IntPtr pointer) : void

<small>`Static`</small>


#### <a name="SET724D2D34"></a>SetVertexAttribPointer(uint index, int size, [VertexAttributeType](heirloom.opengles.vertexattributetype.md) type, bool normalized, int stride, uint bufferOffset) : void

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


#### <a name="BIN13B4877"></a>BindBuffer([BufferTarget](heirloom.opengles.buffertarget.md) target, uint buffer) : void

<small>`Static`</small>


#### <a name="BINB84A9458"></a>BindBufferBase([BufferTarget](heirloom.opengles.buffertarget.md) target, uint index, uint buffer) : void

<small>`Static`</small>


#### <a name="GET8361479C"></a>GetBufferParameters([BufferTarget](heirloom.opengles.buffertarget.md) index, [BufferParameter](heirloom.opengles.bufferparameter.md) parameter, int result) : void

<small>`Static`</small>


#### <a name="BUF84435822"></a>BufferData([BufferTarget](heirloom.opengles.buffertarget.md) target, uint size, IntPtr data, [BufferUsage](heirloom.opengles.bufferusage.md) usage = 35044) : void

<small>`Static`</small>


#### <a name="BUF99CEB285"></a>BufferSubData([BufferTarget](heirloom.opengles.buffertarget.md) target, uint offset, uint size, IntPtr data) : void

<small>`Static`</small>


#### <a name="BUF3C160480"></a>BufferData<T>([BufferTarget](heirloom.opengles.buffertarget.md) target, T data, [BufferUsage](heirloom.opengles.bufferusage.md) usage = 35044) : void

<small>`Static`</small>


#### <a name="BUF7BB9D9F1"></a>BufferSubData<T>([BufferTarget](heirloom.opengles.buffertarget.md) target, uint offset, T data) : void

<small>`Static`</small>


#### <a name="MAP1A5D6166"></a>MapBufferRange([BufferTarget](heirloom.opengles.buffertarget.md) target, int offset, int length, [MapBufferAccess](heirloom.opengles.mapbufferaccess.md) access) : void

<small>`Static`</small>


#### <a name="UNMEAB083D"></a>UnmapBuffer([BufferTarget](heirloom.opengles.buffertarget.md) target) : bool

<small>`Static`</small>


#### <a name="FLU40B29C8E"></a>FlushMappedBufferRange([BufferTarget](heirloom.opengles.buffertarget.md) target, int offset, int size) : void

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


#### <a name="BIN52A93EFC"></a>BindFramebuffer([FramebufferTarget](heirloom.opengles.framebuffertarget.md) target, uint buffer) : void

<small>`Static`</small>


#### <a name="GET54485CC6"></a>GetFramebufferAttachmentParameters([FramebufferTarget](heirloom.opengles.framebuffertarget.md) target, [FramebufferAttachment](heirloom.opengles.framebufferattachment.md) attachment, [FramebufferAttachmentParameter](heirloom.opengles.framebufferattachmentparameter.md) parameter, int result) : void

<small>`Static`</small>


#### <a name="CHE638ECAA5"></a>CheckFramebufferStatus([FramebufferTarget](heirloom.opengles.framebuffertarget.md) target) : [FramebufferStatus](heirloom.opengles.framebufferstatus.md)

<small>`Static`</small>


#### <a name="FRA2B0764F1"></a>FramebufferTexture2D([FramebufferTarget](heirloom.opengles.framebuffertarget.md) target, [FramebufferAttachment](heirloom.opengles.framebufferattachment.md) attachment, [TextureImageTarget](heirloom.opengles.textureimagetarget.md) texTarget, uint tex, int mip) : void

<small>`Static`</small>


#### <a name="FRAAFB76D56"></a>FramebufferTextureLayer([FramebufferTarget](heirloom.opengles.framebuffertarget.md) target, [FramebufferAttachment](heirloom.opengles.framebufferattachment.md) attachment, uint tex, int mip, int layer) : void

<small>`Static`</small>


#### <a name="FRA155CC06C"></a>FramebufferRenderbuffer([FramebufferTarget](heirloom.opengles.framebuffertarget.md) target, [FramebufferAttachment](heirloom.opengles.framebufferattachment.md) attachment, uint renderbuffer) : void

<small>`Static`</small>


#### <a name="BLI5612057D"></a>BlitFramebuffer(int srcX0, int srcY0, int srcX1, int srcY1, int dstX0, int dstY0, int dstX1, int dstY1, [FramebufferBlitMask](heirloom.opengles.framebufferblitmask.md) mask, [FramebufferBlitFilter](heirloom.opengles.framebufferblitfilter.md) filter) : void

<small>`Static`</small>


#### <a name="INVE31C3455"></a>InvalidateFramebuffer([FramebufferAttachment[]](heirloom.opengles.framebufferattachment.md) attachments) : void

<small>`Static`</small>


#### <a name="INV15EC5559"></a>InvalidateSubFramebuffer([FramebufferAttachment[]](heirloom.opengles.framebufferattachment.md) attachments, int x, int y, int width, int height) : void

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


#### <a name="REN329CD676"></a>RenderbufferStorage([RenderbufferFormat](heirloom.opengles.renderbufferformat.md) format, int width, int height) : void

<small>`Static`</small>


#### <a name="RENBEC2E37B"></a>RenderbufferStorage([RenderbufferFormat](heirloom.opengles.renderbufferformat.md) format, int width, int height, int samples = 0) : void

<small>`Static`</small>


#### <a name="GET23521B1F"></a>GetRenderbufferParameter([RenderbufferValue](heirloom.opengles.renderbuffervalue.md) parameter, int result) : void

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


#### <a name="BIN52C35843"></a>BindTexture([TextureTarget](heirloom.opengles.texturetarget.md) target, uint texture) : void

<small>`Static`</small>


#### <a name="ACT628B5865"></a>ActiveTexture(uint texture) : void

<small>`Static`</small>


#### <a name="GET1E9874F8"></a>GetTextureParameters([TextureTarget](heirloom.opengles.texturetarget.md) index, [TextureParameter](heirloom.opengles.textureparameter.md) parameter, float result) : void

<small>`Static`</small>


#### <a name="GETE37978B5"></a>GetTextureParameters([TextureTarget](heirloom.opengles.texturetarget.md) index, [TextureParameter](heirloom.opengles.textureparameter.md) parameter, int result) : void

<small>`Static`</small>


#### <a name="LOAF6258752"></a>LoadFunctions([GL.GetProcAddress](heirloom.opengles.gl.getprocaddress.md) getProcAddress) : void

<small>`Static`</small>


#### <a name="CRE99ABC54C"></a>CreateShader([ShaderType](heirloom.opengles.shadertype.md) type) : uint

<small>`Static`</small>


#### <a name="DEL21BC8470"></a>DeleteShader(uint shader) : void

<small>`Static`</small>


#### <a name="COM1CBF07C2"></a>CompileShader(uint shader) : void

<small>`Static`</small>


#### <a name="SHAFBB5810E"></a>ShaderSource(uint shader, string source) : void

<small>`Static`</small>


#### <a name="GETDD4C703D"></a>GetShader(uint shader, [ShaderParameter](heirloom.opengles.shaderparameter.md) name) : int

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


#### <a name="GETD70E374D"></a>GetProgram(uint program, [ProgramParameter](heirloom.opengles.programparameter.md) property) : int

<small>`Static`</small>

Get a value about the given program from the GL implementation.

<small>**program**: <param name="program"> A valid program name </param>  
</small>
<small>**property**: <param name="property"> The type of property to request </param>  
</small>

#### <a name="GET6633CB5A"></a>GetProgramInfoLog(uint program) : string

<small>`Static`</small>


#### <a name="GET3392AF85"></a>GetActiveAttribute(uint program, uint index) : [ActiveAttribute](heirloom.opengles.activeattribute.md)

<small>`Static`</small>


#### <a name="GETB5733180"></a>GetActiveAttributes(uint program) : [ActiveAttribute[]](heirloom.opengles.activeattribute.md)

<small>`Static`</small>


#### <a name="GETDABDC229"></a>GetActiveUniform(uint program, uint index) : [ActiveUniform](heirloom.opengles.activeuniform.md)

<small>`Static`</small>


#### <a name="GETD3471DEC"></a>GetActiveUniforms(uint program) : [ActiveUniform[]](heirloom.opengles.activeuniform.md)

<small>`Static`</small>


#### <a name="GETA4109E8F"></a>GetAttribLocation(uint program, string name) : int

<small>`Static`</small>


#### <a name="GET39135FAF"></a>GetUniformLocation(uint program, string name) : int

<small>`Static`</small>


#### <a name="GETA72AEAA1"></a>GetActiveUniformBlockName(uint program, uint blockIndex) : string

<small>`Static`</small>


#### <a name="GET20570024"></a>GetActiveUniformBlockIndex(uint program, string name) : uint

<small>`Static`</small>


#### <a name="GET498F14E1"></a>GetActiveUniform(uint program, uint uniformIndices, [UniformParameter](heirloom.opengles.uniformparameter.md) pname, int result) : void

<small>`Static`</small>


#### <a name="GET1CE2780A"></a>GetActiveUniformBlock(uint program, uint index) : [ActiveUniformBlock](heirloom.opengles.activeuniformblock.md)

<small>`Static`</small>


#### <a name="GETF20F5DC5"></a>GetActiveUniformBlocks(uint program) : [ActiveUniformBlock[]](heirloom.opengles.activeuniformblock.md)

<small>`Static`</small>


#### <a name="GET1BA4FD3A"></a>GetActiveUniformBlock(uint program, uint blockIndex, [UniformBlockParameter](heirloom.opengles.uniformblockparameter.md) pname, int result) : void

<small>`Static`</small>


#### <a name="GETD772A2B2"></a>GetActiveUniformBlock(uint program, uint blockIndex, [UniformBlockParameter](heirloom.opengles.uniformblockparameter.md) pname, out int value) : void

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


