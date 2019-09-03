#include "standard/standard.frag"
 
vec4 fragmentProgram() 
{ 
	// Simply sample image
	return image(fUV);
}