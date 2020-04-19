using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace DingoFont.FreeType.InternalStructure
{
    [StructLayout(LayoutKind.Sequential)]
    public struct FTGeneric
    {
        public IntPtr data;
        public IntPtr finalizer;
    }
}
