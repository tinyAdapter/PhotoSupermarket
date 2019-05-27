using System;
using System.Collections.Generic;
using System.Text;

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

        public byte GetDataAt(int x, int y)
        {
            if (x > Width || y > Height) throw new ArgumentOutOfRangeException();

            throw new NotImplementedException();
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
