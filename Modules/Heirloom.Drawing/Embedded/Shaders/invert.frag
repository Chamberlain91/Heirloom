#include "standard/standard.frag"

uniform float uBlend;

vec4 fragmentProgram(vec4 color) 
{
	// Inverse RGB
	color.rgb = mix(color.rgb, 1.0 - color.rgb, uBlend);
	return color;
}