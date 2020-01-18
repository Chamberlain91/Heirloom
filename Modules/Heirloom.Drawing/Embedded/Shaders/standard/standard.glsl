// maps 'image space' to 'atlas space'
vec2 transformUV(in vec2 uv, in vec4 uvScale) {
	
	// Scale and translate into atlas space
	uv = uv * uvScale.zw + uvScale.xy;
	
	// If we have a scale height, we need to "y-flip"
	if(uvScale.w < 0.0) { uv.y += 1.0; }

	// Returns the mapped coordinate
	return uv;
}