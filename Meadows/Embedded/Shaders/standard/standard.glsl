#ifdef GL_ES
precision highp sampler2DArray;
precision highp float;
#endif

#define _H_REPEAT_BLANK  0
#define _H_REPEAT_REPEAT 1
#define _H_REPEAT_CLAMP  2

#define _H_FILTER_NEAREST 0
#define _H_FILTER_LINEAR  1

#define TRANSPARENT vec4(0.0)

#define MIN_UV_EDGE 0.001
#define MAX_UV_EDGE 0.999

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
	
	vec2 dx = dFdx(uv);
	vec2 dy = dFdy(uv);
    float dt = max(dot(dx, dx), dot(dy, dy));
    return int(0.5 * log2(dt));

#else

	// Top mip (Vertex Shader)
	return 0;

#endif
}

// nearest sampling
vec4 _H_SampleAtlasNearest(sampler2D img, ivec2 size, vec2 uv, vec4 rect, int repeat_mode)
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
	uv /= 1 << mip;

	// Fetch texel
	return texelFetch(img, ivec2(uv), mip);
}

// linear sampling
vec4 _H_SampleAtlasLinear(sampler2D img, ivec2 size, vec2 uv, vec4 rect, int repeat_mode)
{
	// Compute the 'pixel space' coordinate
	vec2 st = uv * size * rect.zw;

	// 
	if (_H_ComputeMipLevel(st) < 0.0) 
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

int _H_GetNegativeEncoding(inout float val) 
{
	int key = -int(floor(val));
	val += key; // remove key
	return key;
}

vec2 atlasSize(sampler2D img, vec4 rect) 
{
	// Acquire image size
	ivec2 size = textureSize(img, 0);
	return size * rect.zw;
}

vec4 atlas(sampler2D img, vec4 rect, vec2 uv)
{
	// Parameter 'rect' has special encoding with negative
	// values. The encoding is as follows:
	// - X (0: "nearest"  -1: "linear")
	// - Y (0: "blank"    -1: "repeat"  -2: "clamp")
	// - Z ----
	// - W (0: "none"     -1: "y-flip")

	// Get repeat mode
	int filter_mode = _H_GetNegativeEncoding(rect.x);
	int repeat_mode = _H_GetNegativeEncoding(rect.y);
	// note: no z encoding
	int flip_mode   = _H_GetNegativeEncoding(rect.w);

	// Vertical Flip UV
	if (flip_mode == 1)
	{
		uv.y = 1.0 - uv.y;
	}
	 
	// Acquire image size
	ivec2 size = textureSize(img, 0);

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