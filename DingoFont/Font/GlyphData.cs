using DingoFont.Math;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace DingoFont.Font
{
    public class GlyphData
    {
        public List<Vector2x3> triangles;
        public List<Vector2x3> conicCurves;
        public List<Vector2x4> cubicCurves;
        public List<Vector2x2> lines;

        public GlyphData()
        {
            triangles = new List<Vector2x3>();
            conicCurves = new List<Vector2x3>();
            cubicCurves = new List<Vector2x4>();
            lines = new List<Vector2x2>();
        }
    }
}
