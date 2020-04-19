using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using FT_Long = System.Int32;
using FT_Int = System.Int32;
using FT_UShort = System.UInt16;
using FT_Short = System.Int16;

namespace DingoFont.FreeType.InternalStructure
{
    [StructLayout(LayoutKind.Sequential)]
    public struct FTFace
    {
        public FT_Long num_faces;
        public FT_Long face_index;

        public bool HasColor => (face_flags & FreeTypeNative.FT_FACE_FLAG_COLOR) == 0;

        public FT_Long face_flags;
        public FT_Long style_flags;

        public FT_Long num_glyphs;

        public IntPtr family_name;
        public IntPtr style_name;

        public FT_Int num_fixed_sizes;
        public IntPtr available_sizes;

        public FT_Int num_charmaps;
        public IntPtr charmaps;

        public FTGeneric generic;

        /*# The following member variables (down to `underline_thickness`) */
        /*# are only relevant to scalable outlines; cf. @FT_Bitmap_Size    */
        /*# for bitmap fonts.                                              */
        public FTBoundingBox bbox;

        public FT_UShort units_per_EM;
        public FT_Short ascender;
        public FT_Short descender;
        public FT_Short height;

        public FT_Short max_advance_width;
        public FT_Short max_advance_height;

        public FT_Short underline_position;
        public FT_Short underline_thickness;

        public IntPtr glyph;
        public IntPtr size;
        public IntPtr charmap;

        /*@private begin */

        public IntPtr driver;
        public IntPtr memory;
        public IntPtr stream;

        public IntPtr sizes_list;

        public FTGeneric autohint;   /* face-specific auto-hinter data */
        public IntPtr extensions; /* unused                         */

        public IntPtr _internal;

    /*@private end */
    }
}
