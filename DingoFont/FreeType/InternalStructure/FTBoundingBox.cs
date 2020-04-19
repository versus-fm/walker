using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using FT_Pos = System.Int32;

namespace DingoFont.FreeType.InternalStructure
{
    [StructLayout(LayoutKind.Sequential)]
    public struct FTBoundingBox
    {
        FT_Pos xMin, yMin;
        FT_Pos xMax, yMax;
    }
}
