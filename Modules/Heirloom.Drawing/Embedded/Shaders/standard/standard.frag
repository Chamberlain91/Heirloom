// Bang comments below are to silence the VS extension used to syntax highlight
// 
//! #version 300 es
//! precision highp sampler2DArray;
//! precision highp float;
#ifdef GL_ES
precision highp sampler2DArray;
#endif

// == Input ==

flat in int fImageUnit;

in vec4 fColor;
in vec2 fUV;

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
	outColor  = imageUnit(fImageUnit, fUV);
	outColor  = fragmentProgram(outColor);
	outColor *= fColor;
}