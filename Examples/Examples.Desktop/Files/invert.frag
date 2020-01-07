#include "standard/standard.frag"
 
vec4 fragmentProgram(vec4 color) 
{ 
	// simple pass-through
	return 1.0 - color;
}