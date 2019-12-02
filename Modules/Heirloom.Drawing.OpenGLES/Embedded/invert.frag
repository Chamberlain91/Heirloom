#include "standard/standard.frag"

vec4 fragmentProgram() 
{ 
	// Inverts the RGB portion of the fragment
	vec4 color = imageUnit(fImageUnit, fUV);
	color.rgb = vec3(1.0) - color.rgb;

	return color;
}