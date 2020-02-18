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

// Basically "redefine" texture calls to use atlas function
#define texture(img, uv) \
	texture(img, computeAtlasUV(uv, img ## _UVRect))

// maps 'image space' to 'atlas space'
vec2 computeAtlasUV(in vec2 uv, in vec4 uvRect) {

	// Wrap UV coordinates
	uv = mod(uv, 1.0);

	// Scale and translate into atlas space
	uv = (uv * uvRect.zw) + uvRect.xy;

	// If we have a scale height, we need to "y-flip"
	if (uvRect.w < 0.0) {
		uv.y += 1.0;
	}

	// Returns the mapped coordinate
	return uv;
}

// computes the luminance of a color
float luminance(vec3 rgb) {
	return dot(rgb, vec3(0.299, 0.587, 0.114));
}