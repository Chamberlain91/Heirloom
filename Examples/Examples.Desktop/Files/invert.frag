#include "standard/standard.frag"

uniform float uStrength;

vec4 fragmentProgram(vec4 color) 
{ 
	// Invert RGB values
	vec4 inv = vec4(1.0 - color.rgb, color.a);

	// Computes how much inversion to have
	return mix(color, inv, uStrength);
}