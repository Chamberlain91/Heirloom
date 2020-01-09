#include "standard/standard.frag"

uniform float uStrength;

struct Moo {
	vec4 Meow;
};

struct Foo {
	float[4] Boo;
	Moo Hoo;
	bool thing;
};

uniform Foo kungFoo;

vec4 fragmentProgram(vec4 color) 
{ 
	// Invert RGB values
	vec4 inv = vec4(1.0 - color.rgb, color.a);

	// Computes how much inversion to have
	// return mix(color, inv, uStrength) + vec4(kungFoo.Boo[0], kungFoo.Boo[1], kungFoo.Boo[2], 0.0) / 100.0 + kungFoo.Hoo.Meow;
	if(kungFoo.thing) {
		return vec4(kungFoo.Boo[0], kungFoo.Boo[1], kungFoo.Boo[2], 1.0) * uStrength;
	} else {
		return inv;
	}
}