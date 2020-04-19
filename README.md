# Heirloom

A set of *C# libraries that provide drawing, audio playback, mathematics and more*. Useful for prototyping and implementating games, tools and other graphical applications. Heirloom currently is supported on *Windows, Linux and macOS*.

![screenshots](./Documentation/screenshots.png)

I've been developing this framework with **Visual Studio 2019**. Using the `dotnet` CLI has been straight forward enough experience to build and run the examples so using `VS Code` (or your favorite editor) and basic comprehension of the command line should work well to contribute and/or use **Heirloom**.

Libraries are `NET Standard 2.1` compiant and examples run on `NET Core 3.0`. 

## Tutorials and Documentation

Visit the [wiki](https://github.com/Chamberlain91/Heirloom/wiki) for tutorials and documentation.

You can also find [screenshots](./Documentation/Screenshots/) and [api documentation](./Documentation/Api) in the `Documentation/` directory.

## Nuget Packages

I've compiled most of the projects and created nuget packages and put them up on [Nuget][nuget_search]. They may be out of date with respect to the repository, but I will try to keep them relevant.

If you build the projects in the `Modules\` directory, it should generate the associated `*.nupkg`. You can then reference them manually if you desire.

## Building

### Using Visual Studio

1. Clone this repository.
2. Open the `Heirloom.sln` in Visual Studio.
3. Build or Run Examples from the IDE
   * Ensure the configuration is set to `Release`

### Using Command Line

1. Clone this repository.
2. Build or Run Examples
   * Run `dotnet build -c Release` in the solution or project folder
     * Note: The solution build may fail because of experimental projects.
   * Run `dotnet run -c Release` in any example project folder

*The projects are set to the standard `AnyCpu` platform, but it is important to note the GLFW binaries are 64 bit.*

## Overview

A breif overview of each project.

**Note:** *Some projects may exist in the the repository that are not mentioned here. You should consider these projects as experimental 'in early development' and not rely on them whatsoever.*

### Drawing

A hardware accelerated 2D drawing library. Provides features to draw images, text and othe shapes to a window or offscreen surfaces. Supports shaders and other compositing effects.
### Sound

A cross platform audio library (built on top of `miniaudio.dll`). Provides mechanisms for streaming audio sources, loading clips into memory, mixing audio groups, effect chains (such as reverb) and more.

### Math

A collection of mathematical data types and functions useful for 2D math. Includes tools and utilities for manipulating and creating vectors, matrices, polygons and other shapes.

### Collections

A collection of data structures and other algorithms. Contains implementation of a stable sort, search algorithms and more.

### Collections.Spatial

A collection of data structures for spatially accelerated queries, such as grids.

### IO

Utilities for file access or other useful mechanisms for data manipulation.

### Desktop

Provides tools for managing windows, monitors and user input. Implements the graphics context needed to use the drawing library on desktop platforms.

## License

See [LICENSE.md](./LICENSE.md) for complete details.

### Special Thanks

Media

* https://www.kenney.nl/
* https://datagoblin.itch.io/monogram

Software

* https://github.com/glfw/glfw
* https://github.com/nothings/stb
* https://github.com/rds1983/StbSharp
* https://github.com/dr-soft/miniaudio

[stbcsharp]: https://github.com/rds1983/StbSharp
[nuget_search]: https://www.nuget.org/packages?q=heirloom
