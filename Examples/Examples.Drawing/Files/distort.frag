#include "standard/standard.frag"

uniform sampler2D uNoiseImage;
uniform float uTime;

vec4 fragmentProgram(vec4 color) 
{ 
	vec4 noiseTexel = texture(uNoiseImage, vec2(uTime, uTime / 2.0) * 0.2 + frag.uv) * 2.0 - 1.0;
	return texture(uMainImage, frag.uvAtlas + noiseTexel.xy * frag.atlasRect.zw * 0.05);
}