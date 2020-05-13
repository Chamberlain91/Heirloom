#include "standard/standard.frag"

uniform sampler2D uNoiseImage;
uniform vec4      uNoiseImage_UVRect;

uniform float uStrength;
uniform vec2 uOffset;

vec4 fragmentProgram(vec4 color) 
{
	// Sample noise image
	vec2 offset = atlas(uNoiseImage, uNoiseImage_UVRect, frag.uv + uOffset).xy * 2.0 - 1.0;
	// Sample main image with offset
	return atlas(uMainImage, uMainImage_UVRect, frag.uv + offset * uStrength);
}