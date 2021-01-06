#include "standard/standard.frag";

uniform vec2 uVector;

// Define a default KERNEL_SIZE
#ifndef KERNEL_SIZE
#define KERNEL_SIZE 5
#endif

#if KERNEL_SIZE == 5

const float[KERNEL_SIZE] kernel
    = float[](0.058187, 0.244649, 0.394327, 0.244649, 0.058187);

#elif KERNEL_SIZE == 7

const float[KERNEL_SIZE] kernel 
    = float[](0.028265, 0.10305, 0.223795, 0.289781, 0.223795, 0.10305, 0.028265);

#elif KERNEL_SIZE == 9

const float[KERNEL_SIZE] kernel 
    = float[](0.017867, 0.054364, 0.120338, 0.193829, 0.227204, 0.193829, 0.120338, 0.054364, 0.017867);

#elif KERNEL_SIZE == 11

const float[KERNEL_SIZE] kernel 
    = float[](0.012788, 0.033571, 0.07111, 0.12155, 0.167663, 0.186637, 0.167663, 0.12155, 0.07111, 0.033571, 0.012788);

#elif KERNEL_SIZE == 13

const float[KERNEL_SIZE] kernel 
    = float[](0.009859, 0.023027, 0.046093, 0.079076, 0.116273, 0.146533, 0.158278, 0.146533, 0.116273, 0.079076, 0.046093, 0.023027, 0.009859);

#elif KERNEL_SIZE == 15

const float[KERNEL_SIZE] kernel 
    = float[](0.008371, 0.017542, 0.032806, 0.05475, 0.081544, 0.108384, 0.12856, 0.136088, 0.12856, 0.108384, 0.081544, 0.05475, 0.032806, 0.017542, 0.008371);

#else

#error Must define KERNEL_SIZE (odd numbers from 5 to 15)

#endif

vec4 fragmentProgram(vec4 color) 
{
    color = vec4(0.0);

    vec2 texelSize = 1.0 / atlasSize(uMainImage, uMainImage_UVRect);
    for (int i = 0; i < KERNEL_SIZE; i++)
    {
        vec2 offset = (float(i) / KERNEL_SIZE * 2.0 - 1.0) * texelSize * uVector;
        color += atlas(uMainImage, uMainImage_UVRect, frag.uv + offset) * kernel[i];
    }
    
    return color;
}