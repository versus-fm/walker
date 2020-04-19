using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

using FT_Pos = System.Int32;

namespace DingoFont.FreeType.InternalStructure
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int move_to(ref FTVector to, IntPtr user);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int line_to(ref FTVector to, IntPtr user);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int conic_to(ref FTVector control, ref FTVector to, IntPtr user);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int cubic_to(ref FTVector control1, ref FTVector control2, ref FTVector to, IntPtr user);
    public struct FTOutlineFuncs
    {
        public move_to move_to;
        public line_to line_to;
        public conic_to conic_to;
        public cubic_to cubic_to;

        public int shift;
        public FT_Pos delta;
    }
}
