#include "standard/standard.frag"

uniform sampler2D uNoiseImage;
uniform float uTime;

vec4 fragmentProgram(vec4 color) 
{ 
	vec4 noiseTexel = texture(uNoiseImage, vec2(uTime, uTime / 2.0) + frag.uv);
	vec4 offset = (noiseTexel * 2.0 - 1.0) * 0.05;

	vec2 distortUV = computeAtlasUV(frag.uv + offset.xy, frag.uvRect);
	return texture(uMainImage, distortUV);
}