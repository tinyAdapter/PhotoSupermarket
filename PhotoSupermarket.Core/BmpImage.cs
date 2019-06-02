using System;
using System.Collections.Generic;
using System.Text;
using PhotoSupermarket.Core.Model;

namespace PhotoSupermarket.Core
{
    public class BmpImage
    {
        public BitmapFileHeader FileHeader { get; set; }
        public BitmapInfoHeader InfoHeader { get; set; }
        public BitmapPaletteEntry[] Palette { get; set; }
        public BitmapData Data { get; set; }

        public bool IsTrueColor() => (InfoHeader.BitCount == (ushort)BitmapColorMode.TrueColor
            || InfoHeader.BitCount == (ushort)BitmapColorMode.RGBA);

        public bool HasPalette() => (InfoHeader.BitCount == 1 || InfoHeader.BitCount == 4
                || InfoHeader.BitCount == 8);

        public int GetTotalPaletteEntries() => (int)(Math.Pow(2, InfoHeader.BitCount));
    }

    public class BitmapFileHeader
    {
        public ushort Type { get; set; }        // Must be 0x4D42 'BM'
        public uint Size { get; set; }          // In bytes
        public ushort Reserved1 { get; set; }   // Must be 0
        public ushort Reserved2 { get; set; }   // Muse be 0
        public uint OffBits { get; set; }       // Offset between file head and real data
    }

    public class BitmapInfoHeader
    {
        public uint Size { get; set; }        // Always = 40
        public int Width { get; set; }          // In pixels
        public int Height { get; set; }         // In pixels
        public ushort Planes { get; set; }      // Always = 1
        public ushort BitCount { get; set; }
        public uint Compression { get; set; }   // 0 for no compression
        public uint SizeImage { get; set; }     // In bytes
        public int XPelsPerMeter { get; set; }  // Resolution in X axis
        public int YPelsPerMeter { get; set; }  // Resolution in Y axis
        public uint ClrUsed { get; set; }       // 0 for all colors in use, which is 2 ^ BitCount
        public uint ClrImportanet { get; set; } // 0 for all colors important
    }

    public class BitmapPaletteEntry
    {
        public BitmapPaletteEntry() { }
        public BitmapPaletteEntry(byte R, byte G, byte B, byte F)
        {
            Red = R;
            Green = G;
            Blue = B;
            Flags = F;
        }
        public byte Red { get; set; }
        public byte Green { get; set; }
        public byte Blue { get; set; }
        public byte Flags { get; set; }         // Alpha channel
    }

    public class BitmapData
    {
        public byte[] Data { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public BitmapColorMode ColorMode { get; set; }

        public int GetRealSize()
        {
            int result = 0;
            switch (ColorMode)
            {
                case BitmapColorMode.MonoChrome:
                    result = ((int)Math.Ceiling((double)Width / 32) * 4) * Height;
                    break;
                case BitmapColorMode.TwoFiftySixColors:
                    result = ((int)Math.Ceiling((double)Width / 4) * 4) * Height;
                    break;
                case BitmapColorMode.TrueColor:
                    result = ((int)Math.Ceiling((double)Width * 3 / 4) * 4) * Height;
                    break;
                case BitmapColorMode.RGBA:
                    result = 4 * Width * Height;
                    break;
            }
            return result;
        }

        public bool Get1BitDataAt(int x, int y)
        {
            if (ColorMode != BitmapColorMode.MonoChrome) throw new NotThisColorModeException();
            if (x >= Width || y >= Height) throw new ArgumentOutOfRangeException();

            var theByteContainingResult = Data[((int)Math.Ceiling((double)Width / 32) * 4) * y + (x / 8)];
            var bitToShift = 7 - (x % 8);
            return (theByteContainingResult & (1 << bitToShift)) != 0;
        }

        public void Set1BitDataAt(int x, int y, bool data)
        {
            if (ColorMode != BitmapColorMode.MonoChrome) throw new NotThisColorModeException();
            if (x >= Width || y >= Height) throw new ArgumentOutOfRangeException();

            var theByteContainingResultIndex = ((int)Math.Ceiling((double)Width / 32) * 4) * y + (x / 8);
            var bitToShift = 7 - (x % 8);
            if (data == true)
            {
                Data[theByteContainingResultIndex] |= (byte) (1 << bitToShift);
            }
            else
            {
                Data[theByteContainingResultIndex] &= (byte)(~(1 << bitToShift));
            }
        }

        public byte Get8BitDataAt(int x, int y)
        {
            if (ColorMode != BitmapColorMode.TwoFiftySixColors) throw new NotThisColorModeException();
            if (x >= Width || y >= Height) throw new ArgumentOutOfRangeException();

            var theByte = Data[((int)Math.Ceiling((double)Width / 4) * 4) * y + x];
            return theByte;
        }

        public void Set8BitDataAt(int x, int y, byte data)
        {
            if (ColorMode != BitmapColorMode.TwoFiftySixColors) throw new NotThisColorModeException();
            if (x >= Width || y >= Height) throw new ArgumentOutOfRangeException();

            var theByteIndex = ((int)Math.Ceiling((double)Width / 4) * 4) * y + x;
            Data[theByteIndex] = data;
        }

        public RGB GetRGBDataAt(int x, int y)
        {
            if (ColorMode != BitmapColorMode.TrueColor) throw new NotThisColorModeException();
            if (x >= Width || y >= Height) throw new ArgumentOutOfRangeException();

            var firstByteIndex = ((int)Math.Ceiling((double)Width * 3 / 4) * 4) * y + x * 3;
            return new RGB
            {
                R = Data[firstByteIndex + 2],
                G = Data[firstByteIndex + 1],
                B = Data[firstByteIndex]
            };
        }

        public void SetRGBDataAt(int x, int y, RGB data)
        {
            if (ColorMode != BitmapColorMode.TrueColor) throw new NotThisColorModeException();
            if (x >= Width || y >= Height) throw new ArgumentOutOfRangeException();

            var firstByteIndex = ((int)Math.Ceiling((double)Width * 3 / 4) * 4) * y + x * 3;
            Data[firstByteIndex + 2] = data.R;
            Data[firstByteIndex + 1] = data.G;
            Data[firstByteIndex] = data.B;
        }

        public RGBA GetRGBADataAt(int x, int y)
        {
            if (ColorMode != BitmapColorMode.RGBA) throw new NotThisColorModeException();
            if (x >= Width || y >= Height) throw new ArgumentOutOfRangeException();

            var firstByteIndex = (Width * 4 * y + x * 4);
            return new RGBA
            {
                R = Data[firstByteIndex + 3],
                G = Data[firstByteIndex + 2],
                B = Data[firstByteIndex + 1],
                A = Data[firstByteIndex]
            };
        }

        public void SetRGBADataAt(int x, int y, RGBA data)
        {
            if (ColorMode != BitmapColorMode.RGBA) throw new NotThisColorModeException();
            if (x >= Width || y >= Height) throw new ArgumentOutOfRangeException();

            var firstByteIndex = (Width * 4 * y + x * 4);
            Data[firstByteIndex + 3] = data.R;
            Data[firstByteIndex + 2] = data.G;
            Data[firstByteIndex + 1] = data.B;
            Data[firstByteIndex] = data.A;
        }
    }

    public enum BitmapColorMode
    {
        MonoChrome = 1,
        TwoFiftySixColors = 8,
        TrueColor = 24,
        RGBA = 32
    }
}
