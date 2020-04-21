namespace Heirloom.OpenGLES
{
    public enum GetParameter
    {
        AliasedLineWidthRange = 0x846E,
        AliasedPointSizeRange = 0x846D,

        // Blending
        Blend = 0x0BE2,
        BlendColor = 0x8005,
        BlendDestinationAlpha = 0x80CA,
        BlendDestinationRGB = 0x80C8,
        BlendEquationAlpha = 0x883D,
        BlendEquationRGB = 0x8009,
        BlendSourceAlpha = 0x80CB,
        BlendSourceRGB = 0x80C9,

        // Framebuffer
        RedBits = 0x0D52,
        GreenBits = 0x0D53,
        BlueBits = 0x0D54,
        AlphaBits = 0x0D55,

        // 
        CullFace = 0x0B44,
        CullFaceMode = 0x0B45,

        // Compressed Texture Info
        NumberCompressedTextureFormats = 0x86A2,
        CompressedTextureFormats = 0x86A3,

        // Shader
        CurrentProgram = 0x8B8D,

        // Shader Binary Formats
        NumberShaderBinaryFormats = 0x8DF9,
        ShaderBinaryFormats = 0x8DF8,
        ShaderCompiler = 0x8DFA,

        MaxCombinedTextureImageUnits = 0x8B4D,
        // ^^ 34930
        MaxTextureCubeSize = 0x851C,
        MaxFragmentUniformVectors = 0x8DFD,
        MaxRenderbufferSize = 0x84E8,
        MaxTextureImageUnits = 0x8872,
        MaxTextureSize = 0x0D33,
        MaxVaryingVectors = 0x8DFC,
        MaxVertexAttribs = 0x8869,
        MaxVertexTextureImageUnits = 0x8B4C,
        MaxVertexUniformVectors = 0x8DFB,

        MaxUniformBufferBindings = 0x8A2F,
        MaxUniformBlockSize = 0x8A30,

        GenerateMipmapHint = 0x8192,

        ArrayBufferBinding = 0x889F,
        ElementArrayBufferBinding = 0x8895,
        RenderbufferBinding = 0x8CA7,

        FramebufferBinding = 0x8CA6,
        DrawFramebufferBinding = 0x8CA6,
        ReadFramebufferBinding = 0x8CAA,

        Dither = 0x0BD0,
        FrontFace = 0x0B46,

        ImplementationColorReadFormat = 0x8B9B,
        ImplementationColorReadType = 0x8B9A,

        PackAlignment = 0x0D05,
        UnpackAlignment = 0x0CF5,

        LineWidth = 0x0B21,
        PolygonOffsetFactor = 0x8038,
        PolygonOffsetFill = 0x8037,
        PolygonOffsetUnits = 0x2A00,

        /// <summary>
        /// The currently active texture unit
        /// </summary>
        ActiveTexture = 0x84E0,

        TextureBinding2D = 0x8069,
        TextureBindingCubeMap = 0x8514,

        SampleAlphaToCoverage = 0x809E,
        SampleBuffers = 0x80A8,
        SampleCoverage = 0x80A0,
        SampleCoverageInvert = 0x80AB,
        SampleCoverageValue = 0x80AA,
        Samples = 0x80A9,

        // 
        ColorClearValue = 0x0C22,
        ColorWriteMask = 0x0C23,

        // Depth
        DepthBits = 0x0D56,
        DepthClearValue = 0x0B73,
        DepthFunction = 0x0B74,
        DepthRange = 0x0B70,
        DepthTest = 0x0B71,
        DepthWriteMask = 0x0B72,

        // Stencil
        StencilBackFail = 0x8801,
        StencilBackFunc = 0x8800,
        StencilBackPassDepthFail = 0x8802,
        StencilBackPassDepthPass = 0x8803,
        StencilBackReference = 0x8CA3,
        StencilBackValueMask = 0x8CA4,
        StencilBackWriteMask = 0x8CA5,
        StencilBits = 0x0D57,
        StencilClearValue = 0x0B91,
        StencilFail = 0x0B94,
        StencilFunc = 0x0B92,
        StencilPassDepthFail = 0x0B95,
        StencilPassDepthPass = 0x0B96,
        StencilReference = 0x0B97,
        StencilTest = 0x0B90,
        StencilValueMask = 0x0B93,
        StencilWriteMask = 0x0B98,

        // ?
        SubPixelBits = 0x0D50,

        // Viewport/Scissor
        ScissorBox = 0x0C10,
        ScissorTest = 0x0C11,
        MaxViewportDimensions = 0x0D3A,
        Viewport = 0x0BA2
    }
}
