using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using FT_UInt = System.UInt32;
using FT_Fixed = System.Int32;
using FT_Int = System.Int32;
using FT_Pos = System.Int32;

namespace DingoFont.FreeType.InternalStructure
{
    [StructLayout(LayoutKind.Sequential)]
    public struct FTGlyphSlot
    {
        public IntPtr library;
        public IntPtr face;
        public IntPtr next;
        public FT_UInt glyph_index; /* new in 2.10; was reserved previously */
        public FTGeneric generic;

        public FTMetrics metrics;
        public FT_Fixed linearHoriAdvance;
        public FT_Fixed linearVertAdvance;
        public FTVector advance;

        public FreeTypeGlyphFormat format;

        public FTBitmap bitmap;
        public FT_Int bitmap_left;
        public FT_Int bitmap_top;

        public  FTOutline outline;

        public FT_UInt num_subglyphs;
        public IntPtr subglyphs;

        public IntPtr control_data;
        public long control_len;

        public FT_Pos lsb_delta;
        public FT_Pos rsb_delta;

        public IntPtr other;

        public IntPtr _internal;

        public uint Index => glyph_index;
        public FTGlyphSlot GetNext()
        {
            return Marshal.PtrToStructure<FTGlyphSlot>(next);
        }

        public FTOutline GetOutline()
        {
            return outline;
        }
    }
}
