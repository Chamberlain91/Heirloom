Working document for the finalization of the Drawing API

```cs
// == Fundamental Operations

// [Mutable Properties]
ctx.Surface = ...
ctx.Viewport = ...
ctx.Transform = ...
ctx.Blending = ...
ctx.Shader = ... // not yet implemented
ctx.Color = ...

// [Accessor Properties]
... = ctx.DefaultSurface
... = ctx.InverseTransform
... = ctx.ApproximatePixelScale

// []
ctx.GrabPixels(pixelRegion)
ctx.SwapBuffers()

// [Fundamental Drawing]
ctx.Clear(color)
ctx.Draw(mesh, image, transform)

// == Derivative Operations

// [Image]

ctx.DrawImage(image, transform)

ctx.DrawImage(image, position, rotation, scale)
ctx.DrawImage(image, position, rotation)
ctx.DrawImage(image, position)

// [Text]

ctx.DrawText(text, rect, font, size, hAlign, vAlign, callback)
ctx.DrawText(text, rect, font, size, hAlign, vAlign)
ctx.DrawText(text, rect, font, size, hAlign, callback)
ctx.DrawText(text, rect, font, size, hAlign)
ctx.DrawText(text, rect, font, size, callback)
ctx.DrawText(text, rect, font, size)

ctx.DrawText(text, position, font, size, hAlign, vAlign, callback)
ctx.DrawText(text, position, font, size, hAlign, vAlign)
ctx.DrawText(text, position, font, size, hAlign, callback)
ctx.DrawText(text, position, font, size, hAlign)
ctx.DrawText(text, position, font, size, callback)
ctx.DrawText(text, position, font, size)

// [Primitives]

ctx.DrawLine(start, end, width=1)

ctx.DrawCurve(start, mid, end, width=1) // cubic
ctx.DrawCurve(curve, width=1)           // any?

ctx.DrawRect(rect)
ctx.DrawRectOutline(rect, width=1)

ctx.DrawCircle(center, radius)
ctx.DrawCircleOutline(center, radius, width=1)

ctx.DrawPolygon(sides, radius)
ctx.DrawPolygonOutline(sides, radius, width=1)

ctx.DrawPolygon(polygon)
ctx.DrawPolygonOutline(polygon, width=1)

ctx.DrawTriangle(a, b, c)
ctx.DrawTriangleOutline(a, b, c, width=1)

```

The `DrawCircle`, `DrawPolygon`, and `DrawTriangle` (ie, filled primitives)
calls are fairly inefficient with the current implementation of batching optimizations (determined by the *same, blending, shader and texture cluster* and ultimately rendered using instancing).

All primitive operations are drawn with flat colors. The (out)line and curve 
drawing can be properly batched with the image and text operations. The filled 
variants won't be batched by the current underlying design. In order to 
efficiently batch - for example - hundreds of duplicate triangles, it would be 
more efficient to use the fundamental `Draw(mesh, Image.White, transform)` 
operation above as that will be able to batch based on that mesh instance.
