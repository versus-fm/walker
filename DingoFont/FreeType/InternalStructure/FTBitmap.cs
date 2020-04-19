using System;
using System.Collections.Generic;
using System.Text;

namespace DingoFont.FreeType.InternalStructure
{
    public struct FTBitmap
    {
        public uint rows;
        public uint width;
        public int pitch;
        public IntPtr buffer;
        public ushort num_grays;
        public FTPixelMode pixel_mode;
        public byte palette_mode;
        public IntPtr palette;
    }
}
