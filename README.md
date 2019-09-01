# Heirloom

A collection of C# libraries (Drawing, Sound, Collections, Math and more)
intended to help implement games and other graphical applications for Windows,
Linux, macOS (Darwin), and Android!

**Note:** *I would like to extend support for iOS (if given tools or help), but
it beyond my know-how especially now that OpenGL is not a suitable target on
iOS anymore. In the same light, macOS will eventually pose problems as well.
Any advice for wrapping Vulkan (once implemented) with Molten or creating a
Metal backend would be highly appreciated.*

**Created by Chris Chamberlain**

## Getting Started

I've been developing this framework (or whatever you would call it) with
**Visual Studio 2019**, another IDE may work to open and develop the projects 
with but I am entirely unfamiliar with them.

### Windows 10

1. Clone this repository.
2. Open the `Heirloom.sln` in Visual Studio.
3. Right Click > Build on any example project OR `Set as StartUp Project`
   and press `Ctrl + F5`.
4. Will probably complain about missing `glfw3` or `miniaudio`, see below.

### Non-Windows Platforms

I am completely unaware how to build the C# projects on other platforms, but
once built with Windows I was able to copy and paste onto Linux or macOS and
run with `mono app.exe`. On these platforms I used Mono 5.2. Another 5.X
version may work, but that is what I tested with. Essentially, the runtime must
support  `.Net Standard 2.0` for the libraries and `.Net Framework v4.7.2` for
the examples. When running on mono for non-windows, a `*.dll.config` file may
be needed with appropriate remapping (ie. `glfw3.dll` maps to `libglfw.so.3.3`
on linux), or the files themselves copied and renamed into the executables 
directory.

**The projects are set to the standard `AnyCpu` platform, but its important to
note I've only ever used *64 bit* binaries. New projects might have to uncheck
the `Prefer 32-bit` in `.Net Framework` style projects. It might be nice to
have a solution for smart loading `32 bit` libraries, but I'm unsure of use
cases for this framework on such machines.**

**Note:** *The application is dependant on some C libraries (namely `glfw3.dll`
and `miniaudio.dll`) and may be unable to be found and loaded with a fresh
clone. They need to be built / copied into a place that the runtime will be
able to resovle. I have included for convenience some binaries in
`Framework/Binaries` (possibly only for now, I'm not sure what the legality of
distributing them and whatnot).

*I expect some more thought is needed about resolving and loading the native
libraries in future.*

## Overview

A breif overview (bullet point form) of each project and notable
features.

**Note:** *Some features or projects are marked with some extra text to inform
you that either the project or feature is not yet implemented
**(Not Implemented)**, is not complete **(Incomplete)** or the implementation
is in a state I am not satisfied with **(Needs Review)***.

### Drawing

A hardware accelerated 2D drawing library.

* JPEG and PNG Image Encode and Decode
* Text Rendering w/ Truetype Fonts
* Supports drawing Quads (Images) and Meshes.
* Offscreen Rendering
* Composition
    + Various Blending (Alpha, Additive, Multiply, etc)
    + Configurable Effects / Shader **(Not Implemented)**
* Image Atlas / Rectangle Packing
    + Assist with drawing performance (via improved batched rendering)
    + To pack animated sprites or tilesets

**Note:** *Image and font support is implemented by a [C to C# machine-port of
STB][stbcsharp], additional functionality or unexpected quirks might exist. For
example loading additional image formats not listed above.*

### Input (Needs Review)

* Keyboard
    + Unicode Text Input
    + Key Events (including scancode)
* Mouse
    + Move, Scroll and Button Events
    + Raw Motion **(Not Implemented)**
* Gamepad **(Not Implemented)**
* Touch **(Not Implemented)**

### Audio

A cross platform audio library (both a high and low level wrapper on 
`miniaudio.dll`).

* Supports Decoding MP3, Vorbis, FLAC and WAV
* Streaming Audio Sources
* In-Memory Audio Clips
* Custom AudioSourceProvider
  * Ie, Synthesized Sound!

### Math

A collection of mathematical data types and functions useful for 2D math.

* General Computation
  * Trig
  * Interpolation (Linear, Cosine, Bezier, etc)
  * Noise (Value, Perlin and Simplex)
  * Vector and Matrix
* Shapes
    * Rectangle
    * Circle
    * Triangle
    * Polygons (Simple and Convex)
    * Line Segment
* Polygon Tools
    * Polygon Convex Decomposition
    * Polygon Triangulation
* Collision Detection
    * Overlap Detection
    * Contact Manifolds

### Collections

A collection of data structures and other algorithms.

* Heap (Min and Max)
* Graph **(Not Implemented)**
* Search
    + Heuristic
    + Depth First
    + Breadth First
* Alternative Sort Algorithms
* Type Dictionary
* Extension Methods

### Collections.Spatial (Needs Review)

A collection of data structures for spatially accelerated queries, such as
grids.

* Grid (Finite and Sparse)
* Broad Phase (Bounding Box Spatial Query)
* etc

### IO / Networking (Needs Review)

Utilities for file access or other useful mechanisms for data manipulation.

* Embedded Files
* BitField (compact 8 bits of boolean state)
* Message style Networking **(Needs Review)**
  * NetworkListener
  * NetworkConnection

**Note:** The `Networking` features within this module are the most significant
part that is needing review.

### Runtime (Incomplete)
* **!! Incomplete Design !!**
* Asset System
    * Load / Cache Mechanism
    * Load Fonts, Image and Sprites
    * Loader for Aseprite files
    * Loader for Tiled Map XML and Tileset XML
* Game Loop
    * Entity-Component
    * Coroutines
    * Timing Metrics
* Tile Map Mechanism
* Extension Methods

## License

I haven't fully settled on a license for Heirloom yet, so I wouldn't 
recommended using these libraries commercial use. *However*, I am tentatively 
releasing Heirloom under a modified zlib/libpng license requiring attribution
and only for non-commercial use. Please be aware that this will likely change
once I review the licensing options I desire.

### Special Thanks

Media

* https://www.kenney.nl/
* https://datagoblin.itch.io/monogram

Software

* https://github.com/glfw/glfw
* https://github.com/nothings/stb
* https://github.com/rds1983/StbSharp
* https://github.com/dr-soft/miniaudio
* https://github.com/RandyGaul/cute_headers (partial port)

[stbcsharp]: https://github.com/rds1983/StbSharp