#include "standard/standard.frag"

uniform sampler2D uNoiseImage;
uniform vec4      uNoiseImage_UVRect;

uniform float uTime;

vec4 fragmentProgram(vec4 color) 
{
	vec2 offset = atlas(uNoiseImage, uNoiseImage_UVRect, vec2(uTime / 5.0, uTime / 2.0) + frag.uv).xy;
	return atlas(uMainImage, uMainImage_UVRect, frag.uv + (offset * 2.0 - 1.0) * 0.05);
}