#include "standard/standard.frag"

uniform float uStrength;

vec4 fragmentProgram(vec4 color) 
{ 
	// Create gray color
	vec4 gray = color;
	gray.rgb = vec3(luminance(color.rgb));

	// Computes the mixing between color and gray
	return mix(color, gray, uStrength);
}