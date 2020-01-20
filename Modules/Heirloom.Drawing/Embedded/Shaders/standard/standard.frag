#ifdef GL_ES
precision highp sampler2DArray;
#endif

// == Input ==

flat in int fImageUnit;

in struct {
	vec4 color;
	vec2 atlasUV;
	vec2 UV;
} frag;

// == Output ==

out vec4 outColor;

// == Uniforms ==

uniform Standard
{
	mat2x3 uMatrix;
};

// == Functions ==

vec4  imageUnit    (int unit, vec2 uv); // auto-generated
ivec2 imageUnitSize(int unit);          // auto-generated

// == Fragment Shader ==

// Alternative main function to implement when using this standard.frag
vec4 fragmentProgram(vec4 color);

#include "./standard.glsl"

void main(void)
{
	outColor  = imageUnit(fImageUnit, frag.atlasUV);
	outColor  = fragmentProgram(outColor);
	outColor *= frag.color;
}