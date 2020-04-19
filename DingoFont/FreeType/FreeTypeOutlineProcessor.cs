using DingoFont.FreeType.InternalStructure;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DingoFont.FreeType
{
    public delegate void MoveToDelegate(in Vector2 from);
    public delegate void LineToDelegate(in Vector2 from, in Vector2 to);
    public delegate void ConicToDelegate(in Vector2 from, in Vector2 control, in Vector2 to);
    public delegate void CubicToDelegate(in Vector2 from, in Vector2 control1, in Vector2 control2, in Vector2 to);
    public class FreeTypeOutlineProcessor
    {
        private Vector2 last;
        private FTOutlineFuncs funcs;

        public event MoveToDelegate MoveTo;
        public event LineToDelegate LineTo;
        public event ConicToDelegate ConicTo;
        public event CubicToDelegate CubicTo;

        public void Prepare(ref FTOutlineFuncs func)
        {
            func.move_to = this.move_to;
            func.line_to = this.line_to;
            func.conic_to = this.conic_to;
            func.cubic_to = this.cubic_to;
            func.shift = 0;
            func.delta = 0;

            this.funcs = func;
        }

        public void Prepare()
        {
            this.funcs = new FTOutlineFuncs()
            {
                move_to = this.move_to,
                line_to = this.line_to,
                conic_to = this.conic_to,
                cubic_to = this.cubic_to,
                shift = 0,
                delta = 0
            };
        }

        public ref FTOutlineFuncs GetFunctionStructure()
        {
            return ref funcs;
        }

        public void ProcessOutline(ref FTOutline outline)
        {
            FreeTypeNative.FT_Outline_Decompose(ref outline, ref funcs, IntPtr.Zero);
        }

        private int move_to(ref FTVector to, IntPtr user)
        {
            MoveTo(to);
            last = to;
            return 0;
        }

        private int line_to(ref FTVector to, IntPtr user)
        {
            LineTo(last, to);
            last = to;
            return 0;
        }

        private int conic_to(ref FTVector control, ref FTVector to, IntPtr user)
        {
            ConicTo(last, control, to);
            last = to;
            return 0;
        }

        private int cubic_to(ref FTVector control1, ref FTVector control2, ref FTVector to, IntPtr user)
        {
            CubicTo(last, control1, control2, to);
            last = to;
            return 0;
        }
    }
}
