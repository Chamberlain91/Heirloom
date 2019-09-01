// == Input ==

flat in int fImageUnit;

in vec4 fColor;
in vec2 fUV;

// == Output ==

out vec4 outColor;

// == Functions ==

// 
// A function that samples a specific image unit. We abstract texture sampling
// this way with the image and effect functions below for the method used for 
// batched rendering. Can concatinate multiple textures into one large draw call
// if they have the same shader, blend, viewport, etc. For a typical 2D game,
// I would imagine most images are drawing with the simple 'alpha blended' shader
// in mind.
// 
// vec4 imageUnit(int unit, vec2 uv);
// vec2 imageDims(int unit);
#include "/generated/image"

// 
// Samples the specific image that the rendering system has specifically chosen.
// 
vec4 image(vec2 uv)
{
	return imageUnit(fImageUnit, uv);
}

// 
// Samples one of the four texture units set aside for custom effect shaders.
// For eaxmple, the color grading texture in Effects.ColorMap
// 
vec4 effect(vec2 uv, int slot)
{
	return imageUnit(EFFECT_UNIT_START + slot, uv);
}

// == Fragment Shader ==

// 
// Alternative main function to implement when using this standard.frag
// 
vec4 fragmentProgram();

void main(void)
{
	outColor  = fragmentProgram();
	outColor *= fColor;
}