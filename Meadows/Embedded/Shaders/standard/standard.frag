#include "./standard.glsl"

// == Input ==

in PerFragment frag;

// == Output ==

out vec4 outColor;

// == Uniforms ==

uniform sampler2D uMainImage;
in vec4 uMainImage_UVRect; 

// == Fragment Shader ==

// Alternative main function to implement when using this standard.frag
vec4 fragmentProgram(vec4 color);

void main(void)
{
	outColor  = atlas(uMainImage, uMainImage_UVRect, frag.uv);
	outColor  = fragmentProgram(outColor);
	outColor *= frag.color;

	// Cutoff alpha
	if (outColor.a < 0.05) { discard; }
}