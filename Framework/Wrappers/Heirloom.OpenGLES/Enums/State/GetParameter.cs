namespace Heirloom.OpenGLES
{
    public enum GetParameter
    {
        ALIASED_LINE_WIDTH_RANGE = 0x846E,
        ALIASED_POINT_SIZE_RANGE = 0x846D,

        ARRAY_BUFFER_BINDING = 0x889F,

        // Blending
        Blend = 0x0BE2,
        BLEND_COLOR = 0x8005,
        BLEND_DST_ALPHA = 0x80CA,
        BLEND_DST_RGB = 0x80C8,
        BLEND_EQUATION_ALPHA = 0x883D,
        BLEND_EQUATION_RGB = 0x8009,
        BLEND_SRC_ALPHA = 0x80CB,
        BLEND_SRC_RGB = 0x80C9,

        // Framebuffer
        RedBits = 0x0D52,
        GreenBits = 0x0D53,
        BlueBits = 0x0D54,
        AlphaBits = 0x0D55,

        NUM_COMPRESSED_TEXTURE_FORMATS = 0x86A2,
        COMPRESSED_TEXTURE_FORMATS = 0x86A3,

        COLOR_CLEAR_VALUE = 0x0C22,
        ColorWriteMask = 0x0C23,
        CULL_FACE = 0x0B44,
        CULL_FACE_MODE = 0x0B45,

        // Depth
        DEPTH_BITS = 0x0D56,
        DEPTH_CLEAR_VALUE = 0x0B73,
        DEPTH_FUNC = 0x0B74,
        DEPTH_RANGE = 0x0B70,
        DEPTH_TEST = 0x0B71,
        DepthWriteMask = 0x0B72,

        // Shader
        CURRENT_PROGRAM = 0x8B8D,

        NUM_SHADER_BINARY_FORMATS = 0x8DF9,
        SHADER_BINARY_FORMATS = 0x8DF8,
        SHADER_COMPILER = 0x8DFA,
         
        MaxCombinedTextureImageUnits = 0x8B4D,
        // ^^ 34930
        MaxTextureCubeSize = 0x851C,
        MAX_FRAGMENT_UNIFORM_VECTORS = 0x8DFD,
        MAX_RENDERBUFFER_SIZE = 0x84E8,
        MaxTextureImageUnits = 0x8872,
        MaxTextureSize = 0x0D33,
        MAX_VARYING_VECTORS = 0x8DFC,
        MaxVertexAttribs = 0x8869,
        MaxVertexTextureImageUnits = 0x8B4C,
        MAX_VERTEX_UNIFORM_VECTORS = 0x8DFB,

        GENERATE_MIPMAP_HINT = 0x8192,

        ELEMENT_ARRAY_BUFFER_BINDING = 0x8895,
        FramebufferBinding = 0x8CA6,

        DITHER = 0x0BD0,
        FRONT_FACE = 0x0B46,

        IMPLEMENTATION_COLOR_READ_FORMAT = 0x8B9B,
        IMPLEMENTATION_COLOR_READ_TYPE = 0x8B9A,

        PACK_ALIGNMENT = 0x0D05,
        UnpackAlignment = 0x0CF5,

        LINE_WIDTH = 0x0B21,
        POLYGON_OFFSET_FACTOR = 0x8038,
        POLYGON_OFFSET_FILL = 0x8037,
        POLYGON_OFFSET_UNITS = 0x2A00,

        RENDERBUFFER_BINDING = 0x8CA7,

        /// <summary>
        /// The currently active texture unit
        /// </summary>
        ActiveTexture = 0x84E0,

        TextureBinding2D = 0x8069,
        TextureBindingCubeMap = 0x8514,

        SAMPLE_ALPHA_TO_COVERAGE = 0x809E,
        SAMPLE_BUFFERS = 0x80A8,
        SAMPLE_COVERAGE = 0x80A0,
        SAMPLE_COVERAGE_INVERT = 0x80AB,
        SAMPLE_COVERAGE_VALUE = 0x80AA,
        SAMPLES = 0x80A9,

        // Stencil
        STENCIL_BACK_FAIL = 0x8801,
        STENCIL_BACK_FUNC = 0x8800,
        STENCIL_BACK_PASS_DEPTH_FAIL = 0x8802,
        STENCIL_BACK_PASS_DEPTH_PASS = 0x8803,
        STENCIL_BACK_REF = 0x8CA3,
        STENCIL_BACK_VALUE_MASK = 0x8CA4,
        STENCIL_BACK_WRITEMASK = 0x8CA5,
        STENCIL_BITS = 0x0D57,
        STENCIL_CLEAR_VALUE = 0x0B91,
        STENCIL_FAIL = 0x0B94,
        STENCIL_FUNC = 0x0B92,
        STENCIL_PASS_DEPTH_FAIL = 0x0B95,
        STENCIL_PASS_DEPTH_PASS = 0x0B96,
        STENCIL_REF = 0x0B97,
        STENCIL_TEST = 0x0B90,
        STENCIL_VALUE_MASK = 0x0B93,
        StencilWriteMask = 0x0B98,

        SubPixelBits = 0x0D50,

        // Viewport/Scissor
        ScissorBox = 0x0C10,
        ScissorTest = 0x0C11,
        MaxViewportDimensions = 0x0D3A,
        Viewport = 0x0BA2
    }
}
