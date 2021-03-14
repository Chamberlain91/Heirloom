#include "standard/standard.frag"

uniform vec4 uNoiseImageRect;

uniform float uStrength;
uniform vec2 uOffset;

vec4 fragmentProgram(vec4 color) 
{
	// Sample noise image
	vec2 offset = atlas(uNoiseImageRect, frag.uv + uOffset).xy * 2.0 - 1.0;
	// Sample main image with offset
	return atlas(uMainImageRect, frag.uv + offset * uStrength);
}