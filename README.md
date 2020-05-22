# Heirloom

A C# framework that provides utilities for *2D graphics, audio, mathematics, data structures and more*. Useful for prototyping and implementating games and other graphical applications. Heirloom currently is supported on *Windows, Linux and macOS*.

![screenshots](./Documentation/screenshots.png)

I've been developing this framework with **Visual Studio 2019**. Using the `dotnet` CLI has been straight forward enough experience to build and run the examples so using `VS Code` (or your otherwise favorite editor) and a basic comprehension of the command line should work well to contribute and/or use **Heirloom**.

Libraries are `NET Standard 2.1` compiant and examples run on `NET Core 3.1`. 

## Tutorials and Documentation

Visit the [wiki][wiki] for tutorials and documentation.

You can also find [screenshots][screenshot_dir] and [api documentation][api_dir] in the `Documentation/` directory.

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

*The projects are set to the standard `AnyCpu` platform, but it is important to note the native binaries are 64 bit.*

## Overview

**Note:** *Some projects may exist in the the repository (especially in a development branch) that are not mentioned here. You should consider these projects as experimental or 'in early development' and not rely on them whatsoever.*

#### Heirloom.Core

The heart of **Heirloom** is the hardware accelerated 2D drawing. This includes features to *draw images, text and other shapes* with **support for shaders** and other compositing effects. In addition, Heirloom provides a collection of mathematical data types and functions useful for 2D math. Includes tools and utilities for manipulating and creating vectors, matrices, polygons and other shapes. 

The `Sound` API provides mechanisms for controllnig audio playback, audio groups, effect chains (such as reverb) and more. Sound data can be either streamed from disk or loaded into memory first. Audio groups allow the user to mix sounds and apply effects in bulk.

Additionally, Heirloom gives the user access to data structures and utility functions for general quality of life. For example, Heirloom contains data structures like `Grid<T>`, `BvhSpatialCollection<T>` and `Graph<T>` and functions like heuristic based search and stable sorting.

#### Heirloom.Desktop

Provides the complete backend for drawing, audio and user input on desktop platforms. This library gives the user the ability to create and manage windows. Implemented over `GLFW` and depends on `Heirloom.OpenGLES` and `Heirloom.MiniAudio`.

#### Heirloom.OpenGLES

Provides an implementation of the graphics system over `OpenGL ES 3.1` core features.

#### Heirloom.MiniAudio

Provides an implementation for audio playback over `miniaudio`.
* Supports `.ogg` and `.mp3`

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
[wiki]: https://github.com/Chamberlain91/Heirloom/wiki
[screenshot_dir]: ./Documentation/Screenshots/
[api_dir]: ./Documentation/Api/
