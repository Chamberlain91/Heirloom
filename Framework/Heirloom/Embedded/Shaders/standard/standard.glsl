#ifdef GL_ES
precision highp sampler2DArray;
precision highp float;
#endif

#define _H_REPEAT_BLANK  0
#define _H_REPEAT_REPEAT 1
#define _H_REPEAT_CLAMP  2

#define _H_FILTER_NEAREST 0
#define _H_FILTER_LINEAR  1

#define _H_FLIP_VERT  1
#define _H_FLIP_HORZ  2
#define _H_FLIP_BOTH  3

#define TRANSPARENT vec4(0.0)

#define MIN_UV_EDGE 0.0001
#define MAX_UV_EDGE 0.9999

// Used to interpolate per-fragment data
struct PerFragment
{
    // Vertex color
    vec4 color;

    // UV coordinates mapped in image space
    vec2 uv;
};

// == Uniforms ==

// NO UNIFORMS

// == Atlas Functions ==

int _H_ComputeMipLevel(in vec2 uv)
{
#ifdef FRAGMENT_SHADER

#ifdef GL_ES

    // Android has a weird spike in magnitude at the edge of images.
    // So, as a lame fix, disable mip sampling.
    return 0;

#else

    vec2 dx = dFdx(uv);
    vec2 dy = dFdy(uv);
    float dt = max(dot(dx, dx), dot(dy, dy));
    return int(0.5 * log2(dt));

#endif

#else

    // Top mip (Vertex Shader)
    return 0;

#endif
}

// nearest sampling
vec4 _H_SampleAtlasNearest(sampler2D img, vec2 size, vec2 uv, vec4 rect, int repeat_mode)
{
    switch(repeat_mode)
    {
        case _H_REPEAT_REPEAT:
            uv = mod(uv, vec2(1.0));
            break;
            
        case _H_REPEAT_CLAMP:
            uv = clamp(uv, vec2(MIN_UV_EDGE), vec2(MAX_UV_EDGE));
            break;
            
        case _H_REPEAT_BLANK:
            if (uv.x < MIN_UV_EDGE || uv.y < MIN_UV_EDGE) { return TRANSPARENT; }
            if (uv.x > MAX_UV_EDGE || uv.y > MAX_UV_EDGE) { return TRANSPARENT; }
            break;
    }

    // Map UV to atlas domain
    uv = (uv * rect.zw) + rect.xy;
    uv = uv * size;

    // Adjust for appropriate mip-map level
    int mip = max(_H_ComputeMipLevel(uv), 0);
    uv /= float(1 << mip);

    // Fetch texel
    return texelFetch(img, ivec2(uv), mip);
}

// linear sampling
vec4 _H_SampleAtlasLinear(sampler2D img, vec2 size, vec2 uv, vec4 rect, int repeat_mode)
{
    // Compute the 'pixel space' coordinate
    vec2 st = uv * size * rect.zw;

    // 
    if (_H_ComputeMipLevel(st) < 0) 
    // Magnification
    {
        vec2 step = 1.0 / (rect.zw * size);

        // Recompute 'centered' coordinate
        uv -= step * 0.5;
        st = uv * size * rect.zw;

        // Sample 4 corners
        vec4 t00 = _H_SampleAtlasNearest(img, size, uv,                     rect, repeat_mode);
        vec4 t10 = _H_SampleAtlasNearest(img, size, uv + vec2(step.x, 0.0), rect, repeat_mode);
        vec4 t01 = _H_SampleAtlasNearest(img, size, uv + vec2(0.0, step.y), rect, repeat_mode);
        vec4 t11 = _H_SampleAtlasNearest(img, size, uv + step,              rect, repeat_mode);

        // Compute fractional step between pixels
        vec2 fr = fract(st);

        // Interpolate
        vec4 t0 = mix(t00, t10, fr.x);
        vec4 t1 = mix(t01, t11, fr.x);
        return mix(t0, t1, fr.y);
    }
    else
    // Minification
    {
        return _H_SampleAtlasNearest(img, size, uv, rect, repeat_mode);
    }
}

// 0.0 to 1.0, zero inclusive
int _H_GetIntegerEncoding0(inout float val) 
{
    float key = floor(val);
    val -= key; // Remove key
    return int(key);
}

// 0.0 to 1.0, one inclusive
int _H_GetIntegerEncoding1(inout float val) 
{
    if (val > 1.0)
    {
        // 
        float key = ceil(val) - 1.0;
        val -= key; // Remove key
        return int(key);
    }
    else
    {
        // Default (no encoding)
        return 0;
    }
}

vec2 atlasSize(sampler2D img, vec4 rect) 
{
    // Acquire image size
    vec2 size = vec2(textureSize(img, 0));
    return size * rect.zw;
}

vec4 atlas(sampler2D img, vec4 rect, vec2 uv)
{
    // Parameter 'rect' has special encoding with the integer component.
    // The encoding is as follows:
    // - X (0: "nearest"    1: "linear")
    // - Y (0: "blank"      1: "repeat"     2: "clamp")
    // - Z (0: "unit 0"     1: "unit 1"     N: "unit N" )
    // - W (0: "none"       1: "y-flip"     2: "x-flip"     3: "xy-flip")

    // Extract encoded values
    int filter_mode = _H_GetIntegerEncoding0(rect.x);
    int repeat_mode = _H_GetIntegerEncoding0(rect.y); 
    int page_number = _H_GetIntegerEncoding1(rect.z);
    int flip_mode   = _H_GetIntegerEncoding1(rect.w);

    // Apply UV Flips
    if (flip_mode == _H_FLIP_VERT || flip_mode == _H_FLIP_BOTH) { uv.y = 1.0 - uv.y; }
    if (flip_mode == _H_FLIP_HORZ || flip_mode == _H_FLIP_BOTH) { uv.x = 1.0 - uv.x; }

    // Acquire image size
    vec2 size = vec2(textureSize(img, 0));

    // Select Filtering
    if (filter_mode == _H_FILTER_NEAREST)
    {
        return _H_SampleAtlasNearest(img, size, uv, rect, repeat_mode);
    }
    else // _H_FILTER_LINEAR
    {
        return _H_SampleAtlasLinear(img, size, uv, rect, repeat_mode);
    }
}

// == Helper Functions ==

// computes the luminance of a color
float luminance(vec3 rgb) 
{
    return dot(rgb, vec3(0.299, 0.587, 0.114));
}