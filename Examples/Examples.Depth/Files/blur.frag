#include "standard/standard.frag";

uniform vec2 uTexelSize;
uniform vec2 uVector;

#define KERNEL_SIZE 5
const float[KERNEL_SIZE] kernel = float[](
    0.122581, 0.233062, 0.288713, 0.233062, 0.122581
);

vec4 fragmentProgram(vec4 color) 
{
    color = vec4(0.0);

    for (int i = 0; i < KERNEL_SIZE; i++)
    {
        vec2 offset = (float(i) / KERNEL_SIZE * 2.0 - 1.0) * uTexelSize * uVector;
        color += atlas(uMainImage, uMainImage_UVRect, frag.uv + offset) * kernel[i];
    }
    
    return color;
}