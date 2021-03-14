#include "./standard.glsl"

// == Per Vertex Attributes ==

in vec2 aPosition;
in vec2 aUV;

// == Per Instance Attributes ==

in vec4   aAtlasRect; // U, V, sU, sV

in mat2x3 aTransform;
in vec4   aColor;

// == Output (Fragment Shader) ==

out vec4 uMainImageRect;
out PerFragment frag;

// == Uniforms ==
 
uniform mat2x3 uProjection;

// == Vertex Shader ==

// Alternative main function to implement when using this standard.frag
vec2 vertexProgram(vec2 position);

void main() 
{ 
	// Transform from object space to pixel space
	vec3 vPosition = vec3(vertexProgram(aPosition), 1.0);
	     vPosition = vec3(vPosition * aTransform, 1.0);
	
	// Transform from pixel space to normalized space
	vPosition = vec3(vPosition * uProjection, 1.0);
	
	// Emit atlas transform rect
	uMainImageRect = aAtlasRect;

	// Emit per fragment values
	frag.color = aColor; 
	frag.uv = aUV;

	// Set final vertex position
    gl_Position = vec4(vPosition, 1.0);
}