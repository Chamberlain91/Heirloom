//! #version 330
#if GL_ES
precision highp float;
#endif

// == Per Vertex Attributes ==

in vec2 aPosition;
in vec2 aUV;

// == Per Instance Attributes ==

// todo: repeat for each named image
in vec4   aImageRect; // U, V, sU, sV ... aImageRect_main
in float  aImageUnit; //              ... aImageUnit_main

in mat2x3 aTransform;
in vec4   aColor;

// == Output (Fragment Shader) ==

flat out int  fImageUnit;
	 out vec4 fColor;
	 out vec2 fUV;

// == Uniforms ==

uniform Standard
{
	mat2x3 uMatrix;
};

// == Vertex Shader ==

// Alternative main function to implement when using this standard.frag
vec2 vertexProgram(vec2 position);

#include "./standard.glsl"

void main() 
{ 
	// Transform from object space to projection space
	vec3 vPosition = vec3(vertexProgram(aPosition), 1.0);
	     vPosition = vec3(vPosition * aTransform, 1.0);
         vPosition = vec3(vPosition * uMatrix, 1.0);

	// Transform UV to atlas space
	// todo: one each needed input image...?
	fUV = transformUV(aUV, aImageRect);

	// Emit desired texture slot
	fImageUnit = int(aImageUnit);

	// Emit blending color
	fColor = aColor;

	// Set final vertex position
    gl_Position = vec4(vPosition, 1.0);
}