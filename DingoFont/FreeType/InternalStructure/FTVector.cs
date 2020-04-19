using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using FT_Long = System.Int32;

namespace DingoFont.FreeType.InternalStructure
{
    public struct FTVector
    {
        public FT_Long x;
        public FT_Long y;

        public float X => x / 65536.0f;
        public float Y => y / 65536.0f;


        public static implicit operator Vector2(FTVector v)
        {
            return new Vector2(v.x, v.y);
        }

        public override string ToString()
        {
            return $"<{x}, {y}>";
        }

    }
}
