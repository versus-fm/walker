using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace DingoFont.FreeType.InternalStructure
{
    public enum BitMask : byte
    {
        BezierMask = 0b00000010,
        CurveMask = 0b00000001
}
    public enum PointTag
    {
        None, ThirdOrderBezier, SecondOrderBezier, OnCurve
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct FTOutline
    {
        private short n_contours;
        private short n_points;

        private IntPtr points;
        private IntPtr tags;
        private IntPtr contours;

        private int flags;

        public unsafe Span<FTVector> GetPoints()
        {
            return new Span<FTVector>(this.points.ToPointer(), this.n_points);
        }

        public unsafe Span<short> GetContours()
        {
            return new Span<short>(this.contours.ToPointer(), this.n_contours);
        }

        public unsafe Span<byte> GetTags()
        {
            return new Span<byte>(this.tags.ToPointer(), this.n_points);
        }

        public unsafe PointTag GetTag(int index)
        {
            if (index >= n_points
                || index < 0)
                return PointTag.None;

            byte tag = *(((byte*)tags) + index);

            return FTOutline.GetTag(tag);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointTag GetTag(byte tag)
        {
            if ((tag & (byte)BitMask.CurveMask) == (byte)BitMask.CurveMask)
            {
                return PointTag.OnCurve;
            }
            else
            {
                if ((tag & (byte)BitMask.BezierMask) == (byte)BitMask.BezierMask)
                {
                    return PointTag.ThirdOrderBezier;
                }
                else
                {
                    return PointTag.SecondOrderBezier;
                }
            }
        }
    }
}
