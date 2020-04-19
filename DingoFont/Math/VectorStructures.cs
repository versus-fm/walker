using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace DingoFont.Math
{
    public struct Vector2x4
    {
        public Vector2 V1;
        public Vector2 V2;
        public Vector2 V3;
        public Vector2 V4;

        public Vector2x4(Vector2 V1, Vector2 V2, Vector2 V3, Vector2 V4)
        {
            this.V1 = V1;
            this.V2 = V2;
            this.V3 = V3;
            this.V4 = V4;
        }
    }

    public struct Vector2x3
    {
        public Vector2 V1;
        public Vector2 V2;
        public Vector2 V3;

        public Vector2x3(Vector2 V1, Vector2 V2, Vector2 V3)
        {
            this.V1 = V1;
            this.V2 = V2;
            this.V3 = V3;
        }
    }

    public struct Vector2x2
    {
        public Vector2 V1;
        public Vector2 V2;

        public Vector2x2(Vector2 V1, Vector2 V2)
        {
            this.V1 = V1;
            this.V2 = V2;
        }
    }
}
