#include "./standard.glsl"

#define MAX_SAMPLERS 8

// == Input ==

in PerFragment frag;

// == Output ==

out vec4 outColor;

// == Uniforms ==

uniform sampler2D uSamplers[MAX_SAMPLERS];
in vec4 uMainImageRect;

uniform float uAlphaCutoff;

// == Atlas Functions ==

vec4 _H_AtlasRead(int index, ivec2 pos) 
{
    #define function(x) texelFetch(uSamplers[x], pos, 0)
    #define case_sampler(x, y) if (x == y) return function(y); else

        #if MAX_SAMPLERS > 0
            case_sampler(index, 0)
        #endif

        #if MAX_SAMPLERS > 1
            case_sampler(index, 1)
        #endif

        #if MAX_SAMPLERS > 2
            case_sampler(index, 2)
        #endif

        #if MAX_SAMPLERS > 3
            case_sampler(index, 3)
        #endif

        #if MAX_SAMPLERS > 4
            case_sampler(index, 4)
        #endif

        #if MAX_SAMPLERS > 5
            case_sampler(index, 5)
        #endif

        #if MAX_SAMPLERS > 6
            case_sampler(index, 6)
        #endif

        #if MAX_SAMPLERS > 7
            case_sampler(index, 7)
        #endif
    
        return function(0);

    #undef case_sampler
    #undef function
}

vec2 _H_AtlasSize(int index) 
{
    #define function(x) vec2(textureSize(uSamplers[x], 0))
    #define case_sampler(x, y) if (x == y) return function(y); else

        #if MAX_SAMPLERS > 0
            case_sampler(index, 0)
        #endif

        #if MAX_SAMPLERS > 1
            case_sampler(index, 1)
        #endif

        #if MAX_SAMPLERS > 2
            case_sampler(index, 2)
        #endif

        #if MAX_SAMPLERS > 3
            case_sampler(index, 3)
        #endif

        #if MAX_SAMPLERS > 4
            case_sampler(index, 4)
        #endif

        #if MAX_SAMPLERS > 5
            case_sampler(index, 5)
        #endif

        #if MAX_SAMPLERS > 6
            case_sampler(index, 6)
        #endif

        #if MAX_SAMPLERS > 7
            case_sampler(index, 7)
        #endif

        // default case
        return function(0);
        
    #undef case_sampler
    #undef function
}

int _H_ComputeMipLevel(in vec2 uv)
{
#ifdef GL_ES

    // Android (or at least on a Adreno 640) has this weird spike in dFdx magnitude
    // at the edge of images. So, as a lame fix, use only 0 mip level.
    return 0;

#else

    vec2 dx = dFdx(uv);
    vec2 dy = dFdy(uv);
    float dt = max(dot(dx, dx), dot(dy, dy));
    return int(0.5 * log2(dt));

#endif
}

vec2 _H_TransformUVToAtlas(in vec2 uv, in vec4 rect, in vec2 size)
{
    // Map UV to atlas domain
    uv = (uv * rect.zw) + rect.xy;
    return uv * size;
}

// nearest sampling
vec4 _H_SampleAtlasNearest(int page_number, vec2 size, vec2 uv, vec4 rect, int repeat_mode)
{
    switch(repeat_mode)
    {
        case _H_REPEAT_REPEAT:
            uv = fract(uv);
            break;
            
        case _H_REPEAT_CLAMP:
            uv = clamp(uv, vec2(0.0), vec2(1.0));
            break;
            
        case _H_REPEAT_BLANK:
            if (uv.x < 0.0 || uv.y < 0.0) return TRANSPARENT;
            if (uv.x > 1.0 || uv.y > 1.0) return TRANSPARENT;
            break;
    }

    // Map UV to atlas domain
    uv = _H_TransformUVToAtlas(uv, rect, size);

    // Fetch texel
    // return texelFetch(_H_GetAtlasSampler(page_number), ivec2(uv), 0);
    return _H_AtlasRead(page_number, ivec2(uv));
}

// linear sampling
vec4 _H_SampleAtlasLinear(int page_number, vec2 size, vec2 uv, vec4 rect, int repeat_mode)
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
        vec4 t00 = _H_SampleAtlasNearest(page_number, size, uv,                     rect, repeat_mode);
        vec4 t10 = _H_SampleAtlasNearest(page_number, size, uv + vec2(step.x, 0.0), rect, repeat_mode);
        vec4 t01 = _H_SampleAtlasNearest(page_number, size, uv + vec2(0.0, step.y), rect, repeat_mode);
        vec4 t11 = _H_SampleAtlasNearest(page_number, size, uv + step,              rect, repeat_mode);

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
        return _H_SampleAtlasNearest(page_number, size, uv, rect, repeat_mode);
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

vec2 atlasSize(vec4 rect) 
{
    // Extract page number
    int page_number = _H_GetIntegerEncoding1(rect.z);

    // Get size scaled to atlas space
    //vec2 size = vec2(textureSize(_H_GetAtlasSampler(page_number), 0));
    return _H_AtlasSize(page_number) * rect.zw;
}

vec4 atlas(vec4 rect, vec2 uv)
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
    // vec2 size = vec2(textureSize(_H_GetAtlasSampler(page_number), 0));
    vec2 size = _H_AtlasSize(page_number);

    // Select Filtering
    if (filter_mode == _H_FILTER_NEAREST)
    {
        return _H_SampleAtlasNearest(page_number, size, uv, rect, repeat_mode);
    }
    else // _H_FILTER_LINEAR
    {
        return _H_SampleAtlasLinear(page_number, size, uv, rect, repeat_mode);
    }
}

// == Fragment Shader ==

// Alternative main function to implement when using this standard.frag
vec4 fragmentProgram(vec4 color);

void main(void)
{
	outColor  = atlas(uMainImageRect, frag.uv);
	outColor  = fragmentProgram(outColor);
	outColor *= frag.color;

	// Cutoff alpha
	if (outColor.a < uAlphaCutoff) { discard; }
}