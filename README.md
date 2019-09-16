# Heirloom

A collection of C# libraries (Drawing, Sound, Collections, Math and more)
intended to help implement games and other graphical applications for Windows,
Linux, macOS (Darwin), and Android! 

I've been developing this framework (or whatever you would call it) with
**Visual Studio 2019**, another IDE may work to open and develop the projects 
with but I am entirely unfamiliar with them. Using the `dotnet` CLI on Windows
and Linux seemed straight forward enough to build and run the examples, so a
combination of `VS Code` and a terminal might suffice on devices without
Visual Studio.

Libraries are `NET Standard 2.0` and desktop examples run `NET Core 2.1`. To use
`Heirloom` on Android and run the relevant examples you'll need `Xamarin`.
While Android is availble, it is less "well defined" about how to use it at the
moment. In particular, I have not yet gotten the sound library to work on
Android yet.

**Note:** *I would like to extend support for iOS (if given tools or help), but
it is beyond my know-how, especially now that OpenGL is not a suitable target
on iOS anymore. In the same light, macOS will eventually pose problems as well.
Any advice for wrapping Vulkan (once implemented) with Molten or creating a
Metal backend would be highly appreciated.*

**Created by Chris Chamberlain**

## Getting Started w/ Nuget (Windows, Linux and macOS)

I've compiled most of the projects and created nuget packages and put them up 
on [Nuget](https://www.nuget.org/packages?q=heirloom). They may be occasionally
out of date with respect to the repository.

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
`Heirloom.Desktop` package. Including `Heirloom.Desktop` in a NET Core project
will transitively add `Heirloom.Drawing` and `Heirloom.Math`.

```sh
dotnet add package Heirloom.Desktop -v 1.1.0-beta
```

Now update `Program.cs` to match the following:

```cs
using Heirloom.Desktop;
using Heirloom.Drawing;

namespace Example
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Application.Run(() =>
            {
                // Create window
                var window = new Window(1280, 720, "Example");
                window.RenderContext.Clear(Color.Pink);
                window.RenderContext.SwapBuffers();
            });
        }
    }
}
```

You can then run the project calling `dotnet run` from the project folder. This
will by default run a `Debug` build. To run a `Release` build use `dotnet run -c
Release`. If everything has gone correctly, you should see a blank pink window.

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

A breif overview (in bullet point form) of each project and notable features.

**Note:** *Some features or projects are marked with some extra text to inform
you that either the project or feature is not yet implemented
**(Not Implemented)**, is not complete **(Incomplete)** or the implementation
is in a state I am not satisfied with **(Needs Review)***.

*This might be a little out of date to compared actual code but since this is
abstract is should be largely the same.*

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

### IO

Utilities for file access or other useful mechanisms for data manipulation.

* BitField (compact 8 bits of boolean state)
* Unified File Access
  * Assembly Embedded Files
  * Files on Disk

### Networking (Needs Review)

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