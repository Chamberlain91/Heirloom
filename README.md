# Heirloom

A collection of C# libraries (Drawing, Sound, Collections, Math and more) for 
prototyping and the implementation of games and other graphical applications on 
**Windows**, **Linux** and **macOS**.
 
---

**Articles, Docs and Tutorials**: https://chamberlain91.github.io/Heirloom/

---

I've been developing this framework (or whatever you would call it) with
**Visual Studio 2019**, another IDE may work to open and develop the projects 
with but I am entirely unfamiliar with them. Using the `dotnet` CLI on Windows
and Linux seemed straight forward enough to build and run the examples, so a
combination of `VS Code` and a terminal might suffice on devices without
Visual Studio.

Libraries are `NET Standard 2.0` and examples run on `NET Core 2.1`. 

**Note**: *Android is partially supported!* The drawing features are available, 
but audio currently is unavailable (anyone want to help?). To use `Heirloom` on 
Android and/or run the relevant examples you'll need to have the `Xamarin SDK` 
installed and `Android SDK` API Level 22 or higher.

## Getting Started w/ Nuget (Windows, Linux and macOS)

I've compiled most of the projects and created nuget packages and put them up 
on [Nuget](https://www.nuget.org/packages?q=heirloom). They may be out of date with respect to the repository, but I will try to keep them relevant.

### dotnet CLI

```sh
mkdir Example
cd ./Example
dotnet new console
```

This will create a C# project file in the `Example/` directory called 
`Example.csproj`. It should also create a simple *"Hello World"* example app in
`Program.cs`.

For desktop applications using `Heirloom` we need to include the
`Heirloom.Desktop` package.

```sh
dotnet add package Heirloom.Desktop -v 1.1.1-beta
```

Now update `Program.cs` to match the following:

```cs
using Heirloom.Desktop;
using Heirloom.Drawing;
using Heirloom.Math;

namespace Example
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var image = Image.CreateCheckerboardPattern(512, 512, Pixel.White, 64);

            Application.Run(() =>
            {
                // Create window
                var window = new Window(512, 512, "Example");
                window.RenderContext.ResetState(); // bug, should not be needed here
                window.RenderContext.DrawImage(image, Vector.Zero);
                window.RenderContext.SwapBuffers();
            });
        }
    }
}
```

You can then run the project calling `dotnet run` from the project folder. This
will by default run a `Debug` build. To run a `Release` build use `dotnet run -c
Release`. If everything has gone correctly, you should see a window with a 
checkerboard pattern drawn in it.

## Building

### Windows 10

1. Clone this repository.
2. Open the `Heirloom.sln` in Visual Studio.

### Non-Windows Platforms

1. Clone this repository.
2. Build Solution or Run Examples
   * Run `dotnet build -c Release` in the solution folder
   * Run `dotnet run -c Release` in any example project folder

Essentially, it is required to use `dotnet` CLI. Technically the runtime must
support `.Net Standard 2.0` for the libraries and `.Net Core 2.1` for the
examples. Tested with `dotnet` on Windows 10 and Linux. Executing *already*
compiled examples with  `mono` on Linux (via WSL) things did appear to "work",
but don't seem to terminate the threads nicely.

**The projects are set to the standard `AnyCpu` platform, but its important to
note I've only ever used *64 bit* binaries.**

## Overview

A breif overview (in bullet point form) of each project and their notable 
features. The specifics may be a little off to compared actual code but 
since these descriptions are fairly abstract is should be largely the same. Some projects will be marked `(Alpha)` or `(Beta)`. These represent my confidence in their stability and likelyhood to remain with the same API, respectively.

**Note:** *Some projects may exist in the the repository that are not mentioned here. You should consider these projects as pre-alpha 'in early development' and not rely on them.*

### Drawing

A hardware accelerated 2D drawing library.

* JPEG and PNG Image Encode and Decode
* Text Rendering w/ Truetype Fonts
* Supports drawing Quads (Images) and Meshes.
* Offscreen Rendering (Render Targets)
* Composition
    + Various Blending (Alpha, Additive, Multiply, etc)
    + Configurable Effects / Shader **(Not Implemented)**
* Image Atlas / Rectangle Packing
    + Assist with drawing performance (via improved batched rendering)
    + To pack animated sprites or tilesets

**Note:** *Image and font support is implemented by a [C to C# machine-port of
STB][stbcsharp], additional functionality or unexpected quirks might exist. For
example loading additional image formats not listed above.*

### Audio (Beta)

A cross platform audio library (both a high and low level wrapper on 
`miniaudio.dll`).

* Supports Decoding MP3, Vorbis, FLAC and WAV
* Streaming Audio Sources
* In-Memory Audio Clips
* Custom AudioSourceProvider
  * Ie, Synthesized Sound!

**Note:** In theory, `miniaudio` should support Android via `OpenSL|ES` or `AAudio`. My attempts to compile, and run this code in a `Xamarin` based app have been unsucessful thus far.

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
* Polygon Tools (Beta)
    * Polygon Convex Decomposition
    * Polygon Triangulation
* Collision Detection (Alpha)
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

### Collections.Spatial (Alpha)

A collection of data structures for spatially accelerated queries, such as
grids.

* Grid (Finite and Sparse)
* Broad Phase (Bounding Box Spatial Query)
* etc

### IO (Beta)

Utilities for file access or other useful mechanisms for data manipulation.

* BitField (compact 8 bits of boolean state)
* Unified File Access
  * Assembly Embedded Files
  * Files on Disk

### Networking (Alpha)

Utilities for simple message based networking.

* Message style Networking
  * NetworkListener
  * NetworkConnection

## License

I haven't fully settled on a license for Heirloom yet, so I wouldn't 
recommended using these libraries commercial use. *However*, I am tentatively 
releasing Heirloom under a modified zlib/libpng license requiring attribution
and only for non-commercial use. Please be aware that this may change once I
properly review licensing options.

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