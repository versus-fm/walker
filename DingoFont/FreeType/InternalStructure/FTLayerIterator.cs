using System;
using System.Collections.Generic;
using System.Text;
using FT_UInt = System.UInt32;

namespace DingoFont.FreeType.InternalStructure
{
    public struct FTLayerIterator
    {
        public FT_UInt num_layers;
        public FT_UInt layer;

        public IntPtr p;
    }
}
