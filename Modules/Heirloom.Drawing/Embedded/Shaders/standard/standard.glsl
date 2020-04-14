#ifdef GL_ES
precision highp sampler2DArray;
precision highp float;
#endif

#define TRANSPARENT vec4(0.0)

// Used to interpolate per-fragment data
struct PerFragment
{
	// Vertex color
	vec4 color;

	// UV coordinates mapped in image space
	vec2 uv;
};

// == Uniforms ==

uniform Standard
{
	mat2x3 uMatrix;
};

// nearest sampling
vec4 _H_SampleAtlasNearest(sampler2D img, vec2 uv, vec4 rect)
{
	// Acquire image size
	ivec2 size = textureSize(img, 0);

	// Transform atlas rect to pixels space
	rect = rect * vec4(size, size);

	// Compute sampling positions (in pixel space)
	vec2 st = mod(uv * rect.zw, rect.zw) + rect.xy;

	// We encode 'flip on y' in negative height atlas rectangle.
	// So if we see this, we need to compensate for this.
	if (rect.w < 0.0)
	{
		// todo: This is probably incorrect in the general case,
		// but appears to make surfaces work as I intended.
		st.y += size.y;
	}

	// Fetch texel
	return texelFetch(img, ivec2(st), 0);
}

// linear sampling
vec4 _H_SampleAtlasLinear(sampler2D img, vec2 uv, vec4 rect)
{
	// Acquire image size
	ivec2 size = textureSize(img, 0);

	// Transform atlas rect to pixels space
	rect = rect * vec4(size, size);

	// Compute sampling positions (in pixel space)
	vec2 st00 = mod((uv * rect.zw) + vec2(0, 0), rect.zw) + rect.xy;
	vec2 st10 = mod((uv * rect.zw) + vec2(1, 0), rect.zw) + rect.xy;
	vec2 st01 = mod((uv * rect.zw) + vec2(0, 1), rect.zw) + rect.xy;
	vec2 st11 = mod((uv * rect.zw) + vec2(1, 1), rect.zw) + rect.xy;

	// Get fractional component
	vec2 fst = fract(st00);

	// We encode 'flip on y' in negative height atlas rectangle.
	// So if we see this, we need to compensate for this.
	if (rect.w < 0.0)
	{
		// todo: This is probably incorrect in the general case,
		// but appears to make surfaces work as I intended.
		st00.y += size.y;
		st10.y += size.y;
		st01.y += size.y;
		st11.y += size.y;
	}

	// Fetch texels
	vec4 t00 = texelFetch(img, ivec2(st00), 0);
	vec4 t10 = texelFetch(img, ivec2(st10), 0);
	vec4 t01 = texelFetch(img, ivec2(st01), 0);
	vec4 t11 = texelFetch(img, ivec2(st11), 0);

	// Interpolate
	vec4 t0 = mix(t00, t10, fst.x);
	vec4 t1 = mix(t01, t11, fst.x);
	return mix(t0, t1, fst.y);
}

bool _H_CheckNegativeEncoding(inout float val, float key) 
{
	if (val >= key) 
	{
		// Detected encoding
		val -= key; // remove encoded key
		return true;
	} else {
		// Was not encoded
		return false;
	}
}

vec4 atlas(sampler2D img, vec4 rect, vec2 uv)
{
	// Parameter 'rect' has special encoding with negative
	// values. The encoding is as follows:
	// - X (0: "nearest"  -1: "linear")
	// - Y (0: "blank"    -1: "repeat"  -2: "clamp")
	// - Z ----
	// - W (0: "none"     -1: "y-flip")

	// Repeat mode flags
	const float REPEAT_BLANK  =  0.0;
	const float REPEAT_REPEAT = -1.0;
	const float REPEAT_CLAMP  = -2.0;

	// Note: Flags must be in descending order!
	if (_H_CheckNegativeEncoding(rect.y, REPEAT_BLANK))
	{
		if (uv.x < 0.0) { return TRANSPARENT; }
		if (uv.y < 0.0) { return TRANSPARENT; }
		if (uv.x > 1.0) { return TRANSPARENT; }
		if (uv.y > 1.0) { return TRANSPARENT; }
	}
	else 
	if (_H_CheckNegativeEncoding(rect.y, REPEAT_REPEAT))
	{
		// Does nothing
	}
	else
	if (_H_CheckNegativeEncoding(rect.y, REPEAT_CLAMP))
	{
		// Clamp UV to zero-to-one box
		uv = clamp(uv, vec2(0.0), vec2(1.0));
	}

	// Filtering mode flags
	const float FILTER_NEAREST =  0.0;
	const float FILTER_LINEAR  = -1.0;

	// Select Filtering
	if (_H_CheckNegativeEncoding(rect.x, FILTER_NEAREST))
	{
		return _H_SampleAtlasNearest(img, uv, rect);
	}
	else
	if (_H_CheckNegativeEncoding(rect.x, FILTER_LINEAR))
	{
		return _H_SampleAtlasLinear(img, uv, rect);
	}
}

// computes the luminance of a color
float luminance(vec3 rgb) {
	return dot(rgb, vec3(0.299, 0.587, 0.114));
}