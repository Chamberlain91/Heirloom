#include "./standard.glsl"

// == Per Vertex Attributes ==

in vec2 aPosition;
in vec2 aUV;

// == Per Instance Attributes ==

// todo: repeat for each named image
in vec4   aAtlasRect; // U, V, sU, sV

in mat2x3 aTransform;
in vec4   aColor;

// == Output (Fragment Shader) ==

out vec4 uMainImage_UVRect;
out PerFragment frag;

// == Uniforms ==

uniform Standard
{
	mat2x3 uMatrix;
};

// == Vertex Shader ==

// Alternative main function to implement when using this standard.frag
vec2 vertexProgram(vec2 position);

void main() 
{ 
	// Transform from object space to projection space
	vec3 vPosition = vec3(vertexProgram(aPosition), 1.0);
	     vPosition = vec3(vPosition * aTransform, 1.0);
         vPosition = vec3(vPosition * uMatrix, 1.0);
	
	// Emit atlas transform rect
	uMainImage_UVRect = aAtlasRect;

	// Emit per fragment values
	frag.color = aColor; 
	frag.uv = aUV;

	// Set final vertex position
    gl_Position = vec4(vPosition, 1.0);
}