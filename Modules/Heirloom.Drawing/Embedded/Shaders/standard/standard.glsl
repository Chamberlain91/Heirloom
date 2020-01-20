#ifdef GL_ES
precision highp sampler2DArray;
precision highp float;
#endif

// maps 'image space' to 'atlas space'
vec2 transformUV(in vec2 uv, in vec4 uvScale) {
	
	// Scale and translate into atlas space
	uv = (uv * uvScale.zw) + uvScale.xy;
	
	// If we have a scale height, we need to "y-flip"
	if(uvScale.w < 0.0) { 
		uv.y += 1.0;
	}

	// Returns the mapped coordinate
	return uv;
}

// Used to interpolate per-fragment data
struct PerFragment 
{
	// Vertex color
	vec4 color;

	// Encodes atlas mapping
	vec4 atlasRect;

	// UV coordinates mapped in atlas space
	vec2 uvAtlas;

	// UV coordinates mapped in image space
	vec2 uv;
};