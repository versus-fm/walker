using System;
using System.Collections.Generic;
using System.Text;

namespace DingoFont.FreeType
{
    public class FreeTypeNumbers
    {
        public static int ToFixed26Dot6(float f)
        {
            return (int)(f * 64);
        }
    }
}
