#include "standard/standard.frag"

uniform sampler2D uNoiseImage;
uniform vec4      uNoiseImage_UVRect;

uniform float uTime;

vec2 shrink(vec2 uv, float factor) {
	return uv * (1.0 + (2.0 * factor)) - factor;
}

vec4 fragmentProgram(vec4 color) 
{
	vec4 offset = atlas(uNoiseImage, uNoiseImage_UVRect, vec2(uTime / 5.0, uTime / 2.0) + frag.uv) * 2.0 - 1.0;
	return atlas(uMainImage, uMainImage_UVRect, shrink(frag.uv, 0.01) + offset.xy * 0.02);
}