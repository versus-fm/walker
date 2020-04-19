using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace DingoFont.Math
{
    public static class VectorMathHelper
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 GetBezierPoint(Vector2 p1, Vector2 p2, Vector2 c, float t)
        {
            var q1 = Vector2.Lerp(p1, c, t);
            var q2 = Vector2.Lerp(c, p2, t);
            return Vector2.Lerp(q1, q2, t);

        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 GetBezierPoint(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, float t)
        {
            var q1 = Vector2.Lerp(p1, p2, t);
            var q2 = Vector2.Lerp(p2, p3, t);
            var q3 = Vector2.Lerp(p3, p4, t);

            var r1 = Vector2.Lerp(q1, q2, t);
            var r2 = Vector2.Lerp(q2, q3, t);

            return Vector2.Lerp(r1, r2, t);

        }

        public static void PrintBezierCurve(Vector2 p1, Vector2 p2, Vector2 c, StringBuilder sb, float resolution = 0.02f)
        {
            for (float f = 0; f < 1.0f; f += resolution)
            {
                var point = GetBezierPoint(p1, p2, c, f);
                sb.AppendLine($"{point.X} {point.Y}");
            }
        }

        public static void PrintBezierCurve(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, StringBuilder sb, float resolution = 0.02f)
        {
            for (float f = 0; f < 1.0f; f += resolution)
            {
                var point = GetBezierPoint(p1, p2, p3, p4, f);
                sb.AppendLine($"{point.X} {point.Y}");
            }
        }

        public static void PrintLinear(Vector2 p1, Vector2 p2, StringBuilder sb, float resolution = 0.02f)
        {
            for (float f = 0; f < 1.0f; f += resolution)
            {
                var point = Vector2.Lerp(p1, p2, f);
                sb.AppendLine($"{point.X} {point.Y}");
            }
        }
    }
}
