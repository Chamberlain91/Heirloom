#include "standard/standard.frag"

uniform float uStrength;

#define GRAY_VECTOR vec3(0.21, 0.71, 0.07)

vec4 fragmentProgram(vec4 color) 
{ 
	// Compute grayscale via dot product
	float lum = dot(color.rgb, GRAY_VECTOR);

	// Create gray color
	vec4 gray = vec4(lum, lum, lum, color.a);

	// Computes the mixing between color and gray
	return mix(color, gray, uStrength);
}