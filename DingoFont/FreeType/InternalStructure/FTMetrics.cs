using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using FT_Long = System.Int32;

namespace DingoFont.FreeType.InternalStructure
{
    [StructLayout(LayoutKind.Sequential)]
    public struct FTMetrics
    {
        public FT_Long width;
        public FT_Long height;

        public FT_Long horiBearingX;
        public FT_Long horiBearingY;
        public FT_Long horiAdvance;

        public FT_Long vertBearingX;
        public FT_Long vertBearingY;
        public FT_Long vertAdvance;
    }
}
