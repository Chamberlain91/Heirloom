# Getting Started w/ NuGet

Let us begin by creating a very simple application that simply opens a window
and clears it to pink.

```sh
$ mkdir Example
$ cd ./Example

$ dotnet new console
```

This will create a C# project file in the `Example/` directory called 
`Example.csproj`. It should also create a simple *"Hello World"* example app in
`Program.cs`.

For desktop applications using `Heirloom` we need to include the
`Heirloom.Desktop` package. We also include additional packages for future
tutorials.

```sh
$ dotnet add package Heirloom.Desktop
$ dotnet add package Heirloom.Drawing
$ dotnet add package Heirloom.Math
$ dotnet add package Heirloom.IO
```

You may want to use a pre-release version. Append `-v 1.1.1-beta` or whatever
version string you desire. See the projects on [NuGet](https://www.nuget.org/packages?q=heirloom)
for their version numbers.

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