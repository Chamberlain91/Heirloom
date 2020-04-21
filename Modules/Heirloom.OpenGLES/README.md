# Heirloom.Drawing.OpenGLES

Provides an implemention of `Graphics` over generic OpenGLES 3.0. This library requires a companion library to actually create and "make current" the OpenGL context. Fortunately, this is available through `Heirloom.Desktop`.

This library is written for `.Net Standard 2.1` and tested with a `.Net Core 3.0` console application.

**Note:** On desktop platforms, this will use `OpenGL 3.3 Core`, though the design has been constrained to also work on platforms that only support `OpenGLES 3.0`. Specifically, the shaders system (which is not exposed to the client yet) automatically prepends `#version 330` or `#version 300 es` based on context detected.