#include "standard/standard.frag"

uniform float uBlend;

vec4 fragmentProgram(vec4 color) 
{
	// Compute grayscale
	float lum = luminance(color.rgb);
	color.rgb = mix(color.rgb, vec3(lum), uBlend);
	return color;
}