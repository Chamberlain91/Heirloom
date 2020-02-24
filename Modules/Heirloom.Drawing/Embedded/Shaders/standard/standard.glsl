#ifdef GL_ES
precision highp sampler2DArray;
precision highp float;
#endif

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

// maps 'image space' to 'atlas space'
vec2 wrap(in vec2 uv, in vec2 offset, in vec2 size, in vec4 rect) {
	return mod((uv * rect.zw) + offset, rect.zw) + rect.xy;
}

// nearest sampling (no interpolation)
vec4 _H_SampleAtlasNearest(sampler2D img, vec2 uv, vec4 rect)
{
	// Acquire image size
	ivec2 size = textureSize(img, 0);

	// Transform atlas rect to pixels space
	rect = rect * vec4(size, size);

	// Compute sampling positions (in pixel space)
	// todo: wrap modes?
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
	// todo: wrap modes?
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

vec4 _H_SampleAtlas(sampler2D img, vec2 uv, vec4 rect)
{
	// Parameter 'rect' has special encoding with negative
	// values. The encoding is as follows
	// - X means "linear interpolation"
	// - Y means "repeat"
	// - Z ----
	// - W means "y-flip"

	// Select Repeat Mode
	if (rect.y >= 0)
	{
		// Clamp Mode
		uv = clamp(uv, vec2(0.0), vec2(1.0));
	}
	else
	{
		// Repeat Mode
		
		// Remove encoded value
		rect.y = 1.0 + rect.y;
	}

	// Select Filtering
	if (rect.x >= 0)
	{
		// Nearest Filtering
		return _H_SampleAtlasNearest(img, uv, rect);
	}
	else
	{
		// Remove encoded value
		rect.x = 1.0 + rect.x;

		// Linear Interpolation
		return _H_SampleAtlasLinear(img, uv, rect);
	}
}

// Cause replacement of texture() calls to use atlas function...
#define texture(img, uv) \
    _H_SampleAtlas(img, uv, img ## _UVRect)

// computes the luminance of a color
float luminance(vec3 rgb) {
	return dot(rgb, vec3(0.299, 0.587, 0.114));
}