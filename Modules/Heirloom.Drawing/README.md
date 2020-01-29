# Heirloom.Drawing

Provides an interface for drawing 2D in an 'immediate mode' style context. Has support to load images from standard formats, useful color utilities, offscreen surfaces, sprite handling, `.aseprite` format loading and more.

This library is written for `.Net Standard 2.1` and tested with a `.Net Core 3.0` console application.

```cs
var gfx = ... // an instance of Graphics acquired somehow
gfx.Clear(Color.Black);
gfx.Color = Color.Pink; // Draws the text pink
gfx.DrawText("Hello World", new Vector(20, 20), Font.Default, 16);
gfx.RefreshScreen();
```

### Graphics Context

This library requires a companion library to actually implement the `Graphics` class. Fortunately, this is available through `Heirloom.Desktop` and `Heirloom.Drawing.OpenGLES`.

----

## Version 1.X Roadmap

#### Shaders / Visual Effects System
  * You know, the thing that make visual things **really** pretty.
  * Undecided on how to construct this API, but I feel its very important to expose.