using System;
using System.Collections.Generic;
using System.Text;

namespace DingoFont.FreeType
{
    [Flags]
    public enum LoadFlags
    {
         DEFAULT            =          0x0,
         NO_SCALE           =          ( 1 << 0 ),
         NO_HINTING         =          ( 1 << 1 ),
         RENDER             =          ( 1 << 2 ),
         NO_BITMAP          =          ( 1 << 3 ),
         VERTICAL_LAYOUT    =          ( 1 << 4 ),
         FORCE_AUTOHINT     =          ( 1 << 5 ),
         CROP_BITMAP        =          ( 1 << 6 ),
         PEDANTIC           =          ( 1 << 7 ),
         IGNORE_GLOBAL_ADVANCE_WIDTH=  ( 1 << 9 ),
         NO_RECURSE         =          ( 1 << 10 ),
         IGNORE_TRANSFORM   =          ( 1 << 11 ),
         MONOCHROME         =          ( 1 << 12 ),
         LINEAR_DESIGN      =          ( 1 << 13 ),
         NO_AUTOHINT        =          ( 1 << 15 ),
                /* Bits 16-19 are used by `FT_LOAD_TARGET_` */
         COLOR              =          ( 1 << 20 ),
         COMPUTE_METRICS    =          ( 1 << 21 ),
         BITMAP_METRICS_ONLY=          ( 1 << 22 ),
    }
}
