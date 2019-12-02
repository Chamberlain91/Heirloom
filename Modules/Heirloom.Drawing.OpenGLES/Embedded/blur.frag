#include "standard/standard.frag"

uniform VectorBlurParameters 
{
	// Vector manipulate the blur axis
	vec2 uBlurVector;
};

vec4 fragmentProgram()
{	
	vec2 dir = uBlurVector / vec2(imageUnitSize(fImageUnit));
	vec4 color = vec4(0.0);

	// 9 Tap
    color += imageUnit(fImageUnit, fUV - dir * 4.0) * 0.036917;
	color += imageUnit(fImageUnit, fUV - dir * 3.0) * 0.075154;
	color += imageUnit(fImageUnit, fUV - dir * 2.0) * 0.124871;
	color += imageUnit(fImageUnit, fUV - dir * 1.0) * 0.169340;
	color += imageUnit(fImageUnit, fUV)             * 0.187438;
	color += imageUnit(fImageUnit, fUV + dir * 1.0) * 0.169340;
	color += imageUnit(fImageUnit, fUV + dir * 2.0) * 0.124871;
	color += imageUnit(fImageUnit, fUV + dir * 3.0) * 0.075154;
	color += imageUnit(fImageUnit, fUV + dir * 4.0) * 0.036917;
	
	// 
	return color;
}