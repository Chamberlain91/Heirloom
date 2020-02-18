#include "standard/standard.frag"

uniform sampler2D uNoiseImage;

uniform float uTime;

vec4 fragmentProgram(vec4 color) 
{
	vec2 offset = texture(uNoiseImage, vec2(uTime / 5.0, uTime / 2.0) + frag.uv).xy;
	return texture(uMainImage, frag.uv + (offset * 2.0 - 1.0) * 0.05);
}