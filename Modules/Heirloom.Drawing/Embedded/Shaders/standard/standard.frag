#include "./standard.glsl"

// == Input ==

in PerFragment frag;

// == Output ==

out vec4 outColor;

// == Uniforms ==

uniform sampler2D uMainImage;

uniform Standard
{
	mat2x3 uMatrix;
};

// == Fragment Shader ==

// maps 'image space' to 'atlas space'
vec2 getAtlasUV(in vec2 uv) {
	return computeAtlasUV(uv, frag.uvRect);
}

// Alternative main function to implement when using this standard.frag
vec4 fragmentProgram(vec4 color);

void main(void)
{
	outColor  = texture(uMainImage, getAtlasUV(frag.uv));
	outColor  = fragmentProgram(outColor);
	outColor *= frag.color;
}