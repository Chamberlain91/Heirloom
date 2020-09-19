using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Text;

using Meadows.Mathematics;

using DWORD = System.UInt32;
using FIXED = System.Single;
using LONG = System.Int32;
using SHORT = System.Int16;
using STRING = System.String;
using WORD = System.UInt16;

namespace Meadows.Drawing
{
    internal class AsepriteFile : IDisposable
    // todo: origin of each image
    {
        public const int MagicNumber = 0xA5E0;

        private readonly BinaryReader _reader;
        private readonly Stream _stream;

        private bool _isNewPalette = false;

        public WORD Width { get; }

        public WORD Height { get; }

        private readonly DWORD _fileSize;
        private readonly WORD _magicNumber;
        private readonly WORD _frameCount;
        private readonly WORD _colorDepth;
        private readonly DWORD _flags;
        private readonly WORD _speed; // DEPRECATED
        // DWORD 0
        // DWORD 0
        private readonly byte _transparentIndex;
        // BYTE[3]
        private readonly WORD _numberOfColors;
        private readonly byte _pixelWidth;
        private readonly byte _pixelHeight;
        // BYTE[92]

        /// <summary>
        /// The frames available in the aseprite file.
        /// </summary>
        public Frame[] Frames { get; }

        /// <summary>
        /// The loaded palette.
        /// </summary>
        public ColorBytes[] Palette { get; set; }

        // 
        public IReadOnlyList<LayerData> Layers => Frames[0].Layers;

        public IReadOnlyList<TagData> Tags => Frames[0].Tags;

        public AsepriteFile(Stream stream)
        {
            // 
            _stream = stream ?? throw new ArgumentNullException(nameof(stream));
            _reader = new BinaryReader(stream);

            // Read Header //

            _fileSize = ReadDWord();
            _magicNumber = ReadWord();

            // Validate magic number
            if (_magicNumber != MagicNumber) { throw new InvalidOperationException($"Unable to read aseprite header, invalid magic number."); }

            _frameCount = ReadWord();

            Width = ReadWord();
            Height = ReadWord();
            _colorDepth = ReadWord();

            _flags = ReadDWord();
            _speed = ReadWord(); // deprecated for frame duration value

            ReadDWord(); // spec, ignored
            ReadDWord(); // spec, ignored

            _transparentIndex = ReadByte();

            ReadBytes(3); // spec, ignored

            _numberOfColors = ReadWord();
            _numberOfColors = _numberOfColors == 0 ? (ushort) 256 : _numberOfColors;

            _pixelWidth = ReadByte();
            _pixelHeight = ReadByte();

            // Spec says if either is zero, assume 1:1
            if (_pixelWidth == 0 || _pixelHeight == 0)
            {
                _pixelWidth = _pixelHeight = 1;
            }

            DebugPrint($"Loading Sprite");
            DebugPrint($"  Dimensions: {Width} x {Height} ({_colorDepth} bit)");
            DebugPrint($"  Frames: {_frameCount}");
            DebugPrint($"  Colors: {_numberOfColors}");
            DebugPrint($"  Pixel Aspect: {_pixelWidth}:{_pixelHeight}");

            ReadBytes(92); // spec, for future use

            // Read Frames //

            Frames = new Frame[_frameCount];
            for (var i = 0; i < _frameCount; i++)
            {
                Frames[i] = ReadFrame();
            }

            // Generate images

            foreach (var frame in Frames)
            {
                frame.GenerateImage();
            }

            // Merge images into atlas
            // Image.CreateAtlas(Frames.Select(f => f.Image));
            // TODO: REMOVE
        }

        private Frame ReadFrame()
        {
            // Size of frame in bytes
            var frameSize = ReadDWord();

            // Magic number
            if (ReadWord() != 0xF1FA) { throw new InvalidOperationException($"Unable to read aseprite frame, invalid magic number."); }

            // The 'old chunks' count, could be more than 0xFFFF so refer to new chunks count
            var oldChunks = ReadWord();

            // The frame duration in milliseconds
            var duration = ReadWord();

            ReadBytes(2); // spec, for future use

            // New chunks count
            var chunkCount = ReadDWord();

            if (chunkCount == 0)
            {
                // spec, if 0 use 'old chunks' count
                chunkCount = oldChunks;
            }

            // 
            var frame = new Frame(this, duration);

            // Read Chunks
            for (var i = 0; i < chunkCount; i++)
            {
                var chunkSize = ReadDWord();
                var chunkType = (ChunkType) ReadWord();

                switch (chunkType)
                {
                    case ChunkType.OldPalette256:
                        Palette = ReadOldPaletteChunk(0xFF);
                        break;

                    case ChunkType.OldPalette64:
                        Palette = ReadOldPaletteChunk(0x40);
                        break;

                    case ChunkType.Layer:
                        frame.Layers.Add(ReadLayerChunk());
                        break;

                    case ChunkType.Cel:
                        frame.Cels.Add(ReadCelChunk((int) chunkSize));
                        break;

                    case ChunkType.CelExtra:
                        ReadCelExtraChunk();
                        break;

                    case ChunkType.ColorProfile:
                        ReadColorProfileChunk();
                        break;

                    case ChunkType.Tags:
                        frame.Tags.AddRange(ReadFrameTags());
                        break;

                    case ChunkType.Palette:
                        Palette = ReadPaletteChunk();
                        break;

                    case ChunkType.UserData:
                        ReadUserDataChunk();
                        break;

                    case ChunkType.Slice:
                        frame.Slices.Add(ReadSliceData());
                        break;

                    default:
                        throw new InvalidOperationException($"Unknown chunk type 0x{chunkType.ToString("X")}.");
                }
            }

            return frame;
        }

        private LayerData ReadLayerChunk()
        {
            var layer = new LayerData();

            // 
            layer.Flags = (LayerFlags) ReadWord();
            layer.Type = (LayerType) ReadWord();
            layer.Level = ReadWord();

            ReadWord(); // width, spec ignored
            ReadWord(); // width, spec ignored

            layer.Blend = (BlendMode) ReadWord();
            layer.Opacity = ReadByte();

            ReadBytes(3); // spec, for future use

            layer.Name = ReadString();

            return layer;
        }

        private CelData ReadCelChunk(int chunkSize)
        {
            var cel = new CelData();

            // Read Properties
            cel.Layer = ReadWord();
            cel.X = ReadShort();
            cel.Y = ReadShort();
            cel.Opacity = ReadByte();
            cel.Type = ReadWord();

            ReadBytes(7); // spec, for future use

            // Raw or Compressed Cel
            if (cel.Type == 0 || cel.Type == 2)
            {
                cel.Width = ReadWord();
                cel.Height = ReadWord();

                // Create pixel storage
                cel.Pixels = new ColorBytes[cel.Width * cel.Height];

                // If a 'raw' cel
                if (cel.Type == 0)
                {
                    // Read Raw 
                    var data = ReadBytes(chunkSize - 26);
                    CopyDataToPixelArray(data, cel.Pixels);
                }
                else
                {
                    // Read Compressed
                    var data = ReadCompressedBytes(chunkSize - 26);
                    CopyDataToPixelArray(data, cel.Pixels);
                }
            }
            else
            {
                // What is a linked cel?
                cel.LinkedCel = ReadWord();
            }

            return cel;
        }

        private CelExtraData ReadCelExtraChunk()
        {
            var extra = new CelExtraData();

            // Read flags
            extra.Flags = (CelExtraFlags) ReadDWord();

            // Read cel rectangle transform
            extra.X = ReadFixed();
            extra.Y = ReadFixed();
            extra.Width = ReadFixed();
            extra.Height = ReadFixed();

            ReadBytes(16); // spec, future use

            return extra;
        }

        private ColorProfile ReadColorProfileChunk()
        {
            var profile = new ColorProfile();

            profile.Type = (ColorProfileType) ReadWord();
            profile.Flags = (ColorProfileFlags) ReadWord();
            profile.Gamma = ReadFixed();

            ReadBytes(8); // spec, future use

            if (profile.Type == ColorProfileType.UseEmbeddedICC)
            {
                var length = ReadDWord();
                profile.ICCData = ReadBytes((LONG) length);
            }

            return profile;
        }

        private TagData[] ReadFrameTags()
        {
            var count = ReadWord();
            ReadBytes(8); // spec, for future use

            // For each tag
            var tags = new TagData[count];
            for (var i = 0; i < count; i++)
            {
                var tag = new TagData();

                // Animation Range
                tag.From = ReadWord();
                tag.To = ReadWord();

                // Animation Direction
                tag.Direction = (AnimationDirection) ReadByte();

                ReadBytes(8); // spec, for future use

                var R = ReadByte();
                var G = ReadByte();
                var B = ReadByte();

                ReadByte(); // spec, extra byte (zero)

                // Tag Color and Name
                tag.Color = new ColorBytes(R, G, B);
                tag.Name = ReadString();

                // 
                tags[i] = tag;
            }

            return tags;
        }

        private ColorBytes[] ReadPaletteChunk()
        {
            var size = ReadDWord();
            var palette = new ColorBytes[size];

            var from = ReadDWord();
            var to = ReadDWord();

            ReadBytes(8); // spec, for future use

            DebugPrint($"  Reading New Palette: {size} from {from} to {to}");

            // For the colors in the range
            for (var i = from; i <= to; i++)
            {
                var flags = (PaletteEntryFlags) ReadWord();

                var R = ReadByte();
                var G = ReadByte();
                var B = ReadByte();
                var A = ReadByte();

                // 
                palette[i] = new ColorBytes(R, G, B, A);

                // 
                if (flags.HasFlag(PaletteEntryFlags.HasName))
                {
                    var name = ReadString();
                    DebugPrint($"    Color {i}: {palette[i]} ({name})");
                }
                else
                {
                    DebugPrint($"    Color {i}: {palette[i]}");
                }
            }

            // We have loaded the new palette, set a flag so we can ignore the old palette format
            _isNewPalette = true;
            return palette;
        }

        private UserData ReadUserDataChunk()
        {
            var data = new UserData();

            var flags = ReadDWord();

            if ((flags & 1) == 1)
            {
                ReadString();
            }

            if ((flags & 2) == 2)
            {
                ReadBytes(4); // RGBA
            }

            return data;
        }

        private SliceData ReadSliceData()
        {
            var slice = new SliceData();

            var count = ReadDWord();
            var flags = (SliceFlags) ReadDWord();

            ReadDWord(); // reserved

            slice.Name = ReadString();

            for (var i = 0; i < count; i++)
            {
                slice.From = ReadDWord();
                slice.X = ReadLong();
                slice.Y = ReadLong();
                slice.Width = ReadDWord(); // can be 0 if hidden
                slice.Height = ReadDWord();

                // 9 slice...
                if (flags.HasFlag(SliceFlags.IsNinePatch))
                {
                    var cx = ReadLong();
                    var cy = ReadLong();
                    var cw = ReadDWord();
                    var ch = ReadDWord();
                }

                // 
                if (flags.HasFlag(SliceFlags.HasPivot))
                {
                    slice.PivotX = ReadLong();
                    slice.PivotY = ReadLong();
                }
            }

            return slice;
        }

        private ColorBytes[] ReadOldPaletteChunk(int depth)
        {
            var packetCount = ReadWord();
            var palette = new ColorBytes[256];

            // For each packet
            for (var p = 0; p < packetCount; p++)
            {
                var offset = ReadByte();
                var count = ReadByte();

                // For each color
                for (var c = 0; c < count; c++)
                {
                    var r = (byte) (ReadByte() / (float) depth * 0xFF);
                    var g = (byte) (ReadByte() / (float) depth * 0xFF);
                    var b = (byte) (ReadByte() / (float) depth * 0xFF);
                    palette[offset + c] = new ColorBytes(r, g, b);
                }
            }

            // Have new palette, return that instead.
            return _isNewPalette && Palette != null ? Palette : palette;
        }

        #region Read Data

        private byte ReadByte()
        {
            return _reader.ReadByte();
        }

        private WORD ReadWord()
        {
            return _reader.ReadUInt16();
        }

        private SHORT ReadShort()
        {
            return _reader.ReadInt16();
        }

        private DWORD ReadDWord()
        {
            return _reader.ReadUInt32();
        }

        private LONG ReadLong()
        {
            return _reader.ReadInt32();
        }

        private FIXED ReadFixed()
        {
            var f = _reader.ReadInt32();
            return f / 100F;
        }

        private byte[] ReadBytes(int n)
        {
            return _reader.ReadBytes(n);
        }

        private byte[] ReadCompressedBytes(int n)
        {
            // Skip Header
            ReadByte();
            ReadByte();

            using var rawStream = new MemoryStream(ReadBytes(n - 6));
            using var deflateStream = new DeflateStream(rawStream, CompressionMode.Decompress);
            using var dataStream = new MemoryStream();

            // Read everything (decompressed) into memory
            deflateStream.CopyTo(dataStream);
            var data = dataStream.ToArray();

            ReadDWord(); // Skip ADLER32 Checksum

            return data;
        }

        private STRING ReadString()
        {
            var n = ReadWord();
            var b = ReadBytes(n);
            return Encoding.UTF8.GetString(b);
        }

        private void CopyDataToPixelArray(byte[] data, ColorBytes[] pixels)
        {
            if (pixels.Length * (_colorDepth / 8) != data.Length)
            {
                throw new InvalidOperationException("Pixel storage and data length don't match...?");
            }

            if (_colorDepth == 32)
            {
                // RGBA
                for (var i = 0; i < pixels.Length; i++)
                {
                    var R = data[i * 4 + 0];
                    var G = data[i * 4 + 1];
                    var B = data[i * 4 + 2];
                    var A = data[i * 4 + 3];

                    pixels[i] = new ColorBytes(R, G, B, A);
                }
            }
            else if (_colorDepth == 16)
            {
                // Grayscale + Alpha
                for (var i = 0; i < pixels.Length; i++)
                {
                    var G = data[i * 2 + 0];
                    var A = data[i * 2 + 1];

                    pixels[i] = new ColorBytes(G, G, G, A);
                }
            }
            else
            {
                // Indexed
                for (var i = 0; i < pixels.Length; i++)
                {
                    var color = Palette[data[i]];
                    pixels[i] = color;
                }
            }
        }

        #endregion

        public void Dispose()
        {
            _reader.Dispose();
        }

        #region Nested Types

        public class Frame
        {
            public ushort Duration { get; }

            public Image Image { get; private set; }

            internal List<CelData> Cels { get; }
            internal List<TagData> Tags { get; }
            internal List<SliceData> Slices { get; }
            internal List<LayerData> Layers { get; }

            private readonly AsepriteFile _sprite;

            public Frame(AsepriteFile sprite, ushort duration)
            {
                Duration = duration;
                _sprite = sprite;

                Cels = new List<CelData>();
                Tags = new List<TagData>();
                Slices = new List<SliceData>();
                Layers = new List<LayerData>();
            }

            internal void GenerateImage()
            {
                Image = new Image(_sprite.Width, _sprite.Height);

                // For each cel (draw pixels into image)
                foreach (var cel in Cels)
                {
                    // TODO: Get blend from cel.Layer
                    var blend = _sprite.Layers[cel.Layer].Blend; // sprite.Frames[0].Layers[cel.Layer].Blend;

                    // BlendMode blend = cel.bl
                    if (cel.Pixels == null)
                    {
                        // Linked Cel...?
                        throw new NotImplementedException("Whats a linked cell?");
                    }
                    else
                    {
                        // Copy pixels into image (ignoring blending at the moment)
                        foreach (var q in Rasterizer.Rectangle(0, 0, cel.Width, cel.Height))
                        {
                            var p = new IntVector(cel.X + q.X, cel.Y + q.Y);
                            var i = q.X + q.Y * cel.Width;

                            // 
                            Image.SetPixel(p, BlendColor(cel.Pixels[i], Image.GetPixel(p), cel.Opacity, blend));
                        }
                    }
                }

                // Pre-Multiply Composed Image
                foreach (var co in Rasterizer.Rectangle(Image.Size))
                {
                    var pixel = Image.GetPixel(co);
                    pixel.R = MultiplyU8(pixel.R, pixel.A);
                    pixel.B = MultiplyU8(pixel.B, pixel.A);
                    pixel.G = MultiplyU8(pixel.G, pixel.A);
                    Image.SetPixel(co, pixel);
                }
            }

            private ColorBytes BlendColor(ColorBytes src, ColorBytes dst, byte opacity, BlendMode blend)
            {
                switch (blend)
                {
                    default:
                        return src;

                    case BlendMode.Normal:
                        return BlendNormal(src, dst, opacity);

                    case BlendMode.Addition:
                        return BlendAddition(src, dst, opacity);

                    case BlendMode.Subtract:
                        return BlendSubtract(src, dst, opacity);
                }
            }

            internal static ColorBytes BlendNormal(ColorBytes src, ColorBytes dst, byte opacity)
            {
                if (dst.A == 0)
                {
                    // Destination is invisible, only blend source
                    src.A = MultiplyU8(src.A, opacity);
                    return src;
                }
                else if (src.A == 0)
                {
                    // Source color is invisible, only provide destination.
                    return dst;
                }

                // 
                src.A = MultiplyU8(src.A, opacity);

                // 
                var Ra = (byte) (src.A + dst.A - MultiplyU8(dst.A, src.A));
                var Rr = (byte) (dst.R + (src.R - dst.R) * src.A / Ra);
                var Rg = (byte) (dst.G + (src.G - dst.G) * src.A / Ra);
                var Rb = (byte) (dst.B + (src.B - dst.B) * src.A / Ra);

                return new ColorBytes(Rr, Rg, Rb, Ra);
            }

            internal static ColorBytes BlendAddition(ColorBytes src, ColorBytes dst, byte opacity)
            {
                src.R = (byte) Calc.Min(dst.R + src.R, 0xFF);
                src.G = (byte) Calc.Min(dst.G + src.G, 0xFF);
                src.B = (byte) Calc.Min(dst.B + src.B, 0xFF);

                return BlendNormal(src, dst, opacity);
            }

            internal static ColorBytes BlendSubtract(ColorBytes src, ColorBytes dst, byte opacity)
            {
                src.R = (byte) Calc.Max(dst.R - src.R, 0);
                src.G = (byte) Calc.Max(dst.G - src.G, 0);
                src.B = (byte) Calc.Max(dst.B - src.B, 0);

                return BlendNormal(src, dst, opacity);
            }

            internal static byte MultiplyU8(byte b, byte s)
            {
                return (byte) (b * s / 0xFF);
            }
        }

        public sealed class LayerData
        {
            public string Name { get; set; }

            public ushort Level { get; set; }

            public LayerType Type { get; set; }

            public LayerFlags Flags { get; set; }

            public BlendMode Blend { get; set; }

            public byte Opacity { get; set; }
        }

        public sealed class CelData
        {
            public ushort Layer { get; set; }

            public ushort LinkedCel { get; set; }

            public WORD Type { get; set; }

            public short X { get; set; }
            public short Y { get; set; }

            public ushort Width { get; internal set; }
            public ushort Height { get; internal set; }

            public ColorBytes[] Pixels { get; set; }

            public byte Opacity { get; set; }

            public CelExtraData Extra { get; set; }
        }

        public sealed class CelExtraData
        {
            public CelExtraFlags Flags { get; set; }

            public float X { get; set; }
            public float Y { get; set; }

            public float Width { get; set; }
            public float Height { get; set; }
        }

        public sealed class ColorProfile
        {
            public ColorProfileType Type { get; set; }

            public ColorProfileFlags Flags { get; set; }

            public FIXED Gamma { get; set; }

            public byte[] ICCData { get; set; }
        }

        public sealed class UserData
        {
            public UserDataFlags Flags { get; set; }

            public string Text { get; set; }

            public ColorBytes Color { get; set; }
        }

        public sealed class SliceData
        {
            public string Name { get; internal set; }

            public uint From { get; internal set; }

            public int PivotX { get; internal set; }
            public int PivotY { get; internal set; }

            public int X { get; internal set; }
            public int Y { get; internal set; }

            public uint Width { get; internal set; }
            public uint Height { get; internal set; }
        }

        public sealed class TagData
        {
            public string Name { get; set; }

            public ushort From { get; set; }
            public ushort To { get; set; }

            public AnimationDirection Direction { get; set; }

            public ColorBytes Color { get; set; }
        }

        #endregion

        #region Enum and Flags

        [Flags]
        public enum LayerFlags : WORD
        {
            Visible = 1 << 0,
            Editable = 1 << 1,
            LockMovement = 1 << 2,
            Background = 1 << 3,
            PreferLinkedCels = 1 << 4,
            Collapsed = 1 << 5,
            ReferenceLayer = 1 << 6
        }

        public enum LayerType : WORD
        {
            Image,
            Group
        }

        [Flags]
        public enum PaletteEntryFlags : WORD
        {
            HasName = 1 << 0
        }

        [Flags]
        public enum SliceFlags : WORD
        {
            IsNinePatch = 1 << 0,
            HasPivot = 1 << 1
        }

        [Flags]
        public enum UserDataFlags : DWORD
        {
            HasText = 1 << 0,
            HasColor = 1 << 1
        }

        [Flags]
        public enum CelExtraFlags : DWORD
        {
            PreciseBounds = 1 << 0
        }

        public enum ColorProfileType : WORD
        {
            NoProfile,
            UseSRGB,
            UseEmbeddedICC
        }

        [Flags]
        public enum ColorProfileFlags : WORD
        {
            UseFixedGamma = 1 << 0
        }

        public enum BlendMode : WORD
        {
            Normal = 0,
            Multiply = 1,
            Screen = 2,
            Overlay = 3,
            Darken = 4,
            Lighten = 5,
            ColorDodge = 6,
            ColorBurn = 7,
            HardLight = 8,
            SoftLight = 9,
            Difference = 10,
            Exclusion = 11,
            Hue = 12,
            Saturation = 13,
            Color = 14,
            Luminosity = 15,
            Addition = 16,
            Subtract = 17,
            Divide = 18
        }

        public enum ChunkType : ushort
        {
            OldPalette256 = 0x0004,
            OldPalette64 = 0x0011,

            Layer = 0x2004,
            Cel = 0x2005,
            CelExtra = 0x2006,
            ColorProfile = 0x2007,
            Tags = 0x2018,
            Palette = 0x2019,
            UserData = 0x2020,
            Slice = 0x2022,
        }

        #endregion

        [Conditional("DEBUG")]
        private void DebugPrint(string message) { Console.WriteLine(message); }
    }
}
