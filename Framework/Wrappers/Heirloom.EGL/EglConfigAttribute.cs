namespace Heirloom.OpenGLES.Platform
{
    internal enum EglConfigAttribute : uint
    {
        /// <summary> EGL_ALPHA_SIZE </summary>
        AlphaSize = 0x3021,
        /// <summary> EGL_ALPHA_MASK_SIZE </summary>
        AlphaMaskSize = 0x303E,
        /// <summary> EGL_BIND_TO_TEXTURE_RGB </summary>
        BindToTextureRGB = 0x3039,
        /// <summary> EGL_BIND_TO_TEXTURE_RGBA </summary>
        BindToTextureRGBA = 0x303A,
        /// <summary> EGL_BLUE_SIZE </summary>
        BlueSize = 0x3022,
        /// <summary> EGL_BUFFER_SIZE </summary>
        BufferSize = 0x3020,
        /// <summary> EGL_COLOR_BUFFER_TYPE </summary>
        ColorBufferType = 0x303F,
        /// <summary> EGL_CONFIG_CAVEAT </summary>
        ConfigCaveat = 0x3027,
        /// <summary> EGL_CONFIG_ID </summary>
        ConfigID = 0x3028,
        /// <summary> EGL_CONFORMANT </summary>
        Conformant = 0x3042,
        /// <summary> EGL_DEPTH_SIZE </summary>
        DepthSize = 0x3025,
        /// <summary> EGL_GREEN_SIZE </summary>
        GreenSize = 0x3023,
        /// <summary> EGL_LEVEL </summary>
        Level = 0x3029,
        /// <summary> EGL_LUMINANCE_SIZE </summary>
        LuminanceSize = 0x303D,
        /// <summary> EGL_MAX_PBUFFER_WIDTH </summary>
        MaxPBufferWidth = 0x302C,
        /// <summary> EGL_MAX_PBUFFER_HEIGHT </summary>
        MaxPBufferHeight = 0x302A,
        /// <summary> EGL_MAX_PBUFFER_PIXELS </summary>
        MaxPBufferPixels = 0x302B,
        /// <summary> EGL_MAX_SWAP_INTERVAL </summary>
        MaxSwapInterval = 0x303C,
        /// <summary> EGL_MIN_SWAP_INTERVAL </summary>
        MinSwapInterval = 0x303B,
        /// <summary> EGL_NATIVE_RENDERABLE </summary>
        NativeRenderable = 0x302D,
        /// <summary> EGL_NATIVE_VISUAL_ID </summary>
        NativeVisualID = 0x302E,
        /// <summary> EGL_NATIVE_VISUAL_TYPE </summary>
        NativeVisualType = 0x302F,
        /// <summary> EGL_RED_SIZE </summary>
        RedSize = 0x3024,
        /// <summary> EGL_RENDERABLE_TYPE </summary>
        RenderableType = 0x3040,
        /// <summary> EGL_SAMPLE_BUFFERS </summary>
        SampleBuffers = 0x3032,
        /// <summary> EGL_SAMPLES </summary>
        Samples = 0x3031,
        /// <summary> EGL_STENCIL_SIZE </summary>
        StencilSize = 0x3026,
        /// <summary> EGL_SURFACE_TYPE </summary>
        SurfaceType = 0x3033,
        /// <summary> EGL_TRANSPARENT_TYPE </summary>
        TransparentType = 0x3034,
        /// <summary> EGL_TRANSPARENT_RED_VALUE </summary>
        TransparentRedValue = 0x3037,
        /// <summary> EGL_TRANSPARENT_GREEN_VALUE </summary>
        TransparentGreenValue = 0x3036,
        /// <summary> EGL_TRANSPARENT_BLUE_VALUE </summary>
        TransparentBlueValue = 0x3035,
    }
}
