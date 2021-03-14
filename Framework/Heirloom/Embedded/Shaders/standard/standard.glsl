#ifdef GL_ES
precision highp sampler2DArray;
precision highp float;
#endif

#define _H_REPEAT_BLANK  0
#define _H_REPEAT_REPEAT 1
#define _H_REPEAT_CLAMP  2

#define _H_FILTER_NEAREST 0
#define _H_FILTER_LINEAR  1

#define _H_FLIP_VERT  1
#define _H_FLIP_HORZ  2
#define _H_FLIP_BOTH  3

#define TRANSPARENT vec4(0.0)

// Used to interpolate per-fragment data
struct PerFragment
{
    // Vertex color
    vec4 color;

    // UV coordinates mapped in image space
    vec2 uv;
};

// == Helper Functions ==

// computes the luminance of a color
float luminance(vec3 rgb) 
{
    return dot(rgb, vec3(0.299, 0.587, 0.114));
}