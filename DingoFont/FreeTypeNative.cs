using DingoFont.FreeType.InternalStructure;
using DingoFont.FreeType.Numerics;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace DingoFont
{
    public enum FreeTypeEncoding : uint
    {
        NONE = (((uint)(0) << 24) |
                        ((uint)(0) << 16) |
                        ((uint)(0) << 8) |
                          (uint)(0)),
        MS_SYMBOL = (((uint)('s') << 24) |
                        ((uint)('y') << 16) |
                        ((uint)('m') << 8) |
                          (uint)('b')),
        UNICODE = (((uint)('u') << 24) |
                        ((uint)('n') << 16) |
                        ((uint)('i') << 8) |
                          (uint)('c')),
        SJIS = (((uint)('s') << 24) |
                        ((uint)('j') << 16) |
                        ((uint)('i') << 8) |
                          (uint)('s')),
        PRC = (((uint)('g') << 24) |
                        ((uint)('g') << 16) |
                        ((uint)(' ') << 8) |
                          (uint)(' ')),
        BIG5 = (((uint)('b') << 24) |
                        ((uint)('i') << 16) |
                        ((uint)('g') << 8) |
                          (uint)('5')),
        WANSUNG = (((uint)('w') << 24) |
                        ((uint)('a') << 16) |
                        ((uint)('n') << 8) |
                          (uint)('s')),
        JOHAB = (((uint)('j') << 24) |
                        ((uint)('o') << 16) |
                        ((uint)('h') << 8) |
                          (uint)('a'))
    }
    public enum FreeTypeGlyphFormat : uint
    {
        NONE = (((uint)0 << 24 ) | 
                    ( (uint)0 << 16 ) | 
                    ( (uint)0 << 8  ) | 
                      (uint)0),
        COMPOSITE = (((uint)'c' << 24) |
                    ((uint)'o' << 16) |
                    ((uint)'m' << 8) |
                      (uint)'p'),
        BITMAP = (((uint)'b' << 24) |
                    ((uint)'i' << 16) |
                    ((uint)'t' << 8) |
                      (uint)'s'),
        OUTLINE = (((uint)'o' << 24) |
                    ((uint)'u' << 16) |
                    ((uint)'t' << 8) |
                      (uint)'l'),
        PLOTTER = (((uint)'p' << 24) |
                    ((uint)'l' << 16) |
                    ((uint)'o' << 8) |
                      (uint)'t'),
    }
    public enum FreeTypeRenderMode
    {
        NORMAL = 0,
        LIGHT,
        MONO,
        LCD,
        LCD_V,

        MAX
    }
    //public struct BitmapSize
    //{
    //    public short height;
    //    public short width;

    //    public long size;

    //    public long x_ppem;
    //    public long y_ppem;

    //}
    //public class CharMap
    //{
    //    FreeTypeFace face;
    //    FreeTypeEncoding encoding;
    //    public ushort platform_id;
    //    public ushort encoding_id;
    //}
    //public class FreeTypeGeneric
    //{
    //    public IntPtr data;
    //    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    //    public delegate IntPtr finalizer(IntPtr _object);
    //}
    //public struct FreeTypeBoundingBox
    //{
    //    public long xMin, yMin;
    //    public long xMax, yMax;
    //}
    //public class FreeTypeMemory
    //{
    //    IntPtr user;
    //    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    //    public delegate IntPtr alloc(ref FreeTypeMemory memory, long size);
    //    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    //    public delegate void free(ref FreeTypeMemory memory, IntPtr block);
    //    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    //    public delegate void realloc(ref FreeTypeMemory memory, long cur_size, long new_size, IntPtr block);
    //}
    //public class FreeTypeModuleClass
    //{
    //    public ulong flags;
    //    public long size;
    //    public string name;
    //    public long version;
    //    public long requires;
    //    IntPtr _interface;
    //    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    //    public delegate void init(ref FreeTypeModule module);
    //    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    //    public delegate void done(ref FreeTypeModule module);
    //    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    //    public delegate void get_interface(ref FreeTypeModule module, string name);
    //}
    //public class FreeTypeModule
    //{
    //    public FreeTypeModuleClass clazz;
    //    //public FreeTypeLibrary library;
    //    public FreeTypeMemory memory;
    //}
    ////public class FreeTypeLibrary
    ////{
    ////    public FreeTypeMemory memory;
    ////    public int version_major;
    ////    public int version_minor;
    ////    public int version_patch;
    ////    public uint num_modules;

    ////    public FreeTypeModule[] modules;

    ////    public FreeTypeList renderers;
    ////    public IntPtr cur_renderer;
    ////    public FreeTypeModule auto_hinter;

    ////    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    ////    public delegate void debug_hook1(IntPtr arg);
    ////    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    ////    public delegate void debug_hook2(IntPtr arg);
    ////    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    ////    public delegate void debug_hook3(IntPtr arg);
    ////    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    ////    public delegate void debug_hook4(IntPtr arg);

    ////    public FreeTypeVector[] lcd_geometry;
    ////    public int refcount;


    ////}
    //public struct FreeTypeVector
    //{
    //    public long x;
    //    public long y;
    //}
    //public class FreeTypeBitmap
    //{
    //    public uint rows;
    //    public uint width;
    //    public int pitch;
    //    public byte[] buffer;
    //    public ushort num_grays;
    //    public byte pixel_mode;
    //    public byte palette_mode;
    //    public IntPtr palette;
    //}
    //public class FreeTypeGlyphSlot
    //{
    //    //public FreeTypeLibrary library;
    //    public FreeTypeFace face;
    //    public FreeTypeGlyphSlot next;
    //    public uint glyph_index;
    //    public FreeTypeGeneric generic;

    //    public FreeTypeGlyphMetrics metrics;

    //    public long linearHoriAdvance;
    //    public long linearVertAdvance;
    //    public FreeTypeVector advance;

    //    public FreeTypeGlyphFormat format;

    //    public FreeTypeBitmap bitmap;
    //    public int bitmap_left;
    //    public int bitmap_right;

    //    public FreeTypeOutline outline;

    //    public uint num_subglyphs;
    //    public FreeTypeSubGlyph[] subglyphs;

    //    public IntPtr control_data;
    //    public long control_len;

    //    public long lsb_delta;
    //    public long rsb_delta;

    //    public IntPtr other;

    //    public FreeTypeSlotInternal _internal;
    //}
    //public class FreeTypeGlyphLoader
    //{
    //    public FreeTypeMemory memory;
    //    public uint max_points;
    //    public uint max_contours;
    //    public uint max_subglyphs;
    //    public bool use_extra;

    //    public FreeTypeGlyphLoad _base;
    //    public FreeTypeGlyphLoad _current;

    //    public IntPtr other;

    //}
    //public class FreeTypeGlyphLoad
    //{
    //    public FreeTypeOutline outline;
    //    public FreeTypeVector[] extra_points;
    //    public FreeTypeVector[] extra_points2;
    //    public uint num_subglyphs;
    //    public FreeTypeSubGlyph[] subglyphs;
    //}
    //public class FreeTypeSlotInternal
    //{
    //    public FreeTypeGlyphLoader loader;
    //    public uint flags;
    //    public bool glyph_transformed;
    //    public FreeTypeMatrix glyph_matrix;
    //    public FreeTypeVector glyph_delta;
    //    public IntPtr glyph_hints;
    //    public int load_flags;
    //}
    //public struct FreeTypeMatrix
    //{
    //    public long xx, xy;
    //    public long yx, yy;
    //}
    //public class FreeTypeSubGlyph
    //{
    //    public int index;
    //    public ushort flags;
    //    public int arg1;
    //    public int arg2;
    //    public FreeTypeMatrix transform;
    //}
    //public struct FreeTypeOutline
    //{
    //    public short n_contours;
    //    public short n_points;
    //    public FreeTypeVector[] points;
    //    public byte[] tags;
    //    public short[] contours;

    //    public int flags;
    //}
    //public struct FreeTypeGlyphMetrics
    //{
    //    public long width;
    //    public long height;

    //    public long horiBearingX;
    //    public long horiBearingY;
    //    public long horiAdvance;

    //    public long vertBearingX;
    //    public long vertBearingY;
    //    public long vertAdvance;
    //}
    //public class FreeTypeRaster
    //{
    //    public byte address;
    //}
    //public class FreeTypeRasterFuncs
    //{
    //    public FreeTypeGlyphFormat glyph_format;

    //}
    //public class FreeTypeRendererClass
    //{
    //    public FreeTypeModuleClass root;
    //    public FreeTypeGlyphFormat glyph_format;

    //    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    //    public delegate int render_glyph(FreeTypeRenderer renderer, FreeTypeGlyphSlot slot, FreeTypeRenderMode mode, ref FreeTypeVector origin);
    //    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    //    public delegate int transform_glyph(FreeTypeRenderer renderer, FreeTypeGlyphSlot slot, ref FreeTypeMatrix matrix, ref FreeTypeVector delta);
    //    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    //    public delegate void get_glyph_cbox(FreeTypeRenderer renderer, FreeTypeGlyphSlot slot, ref FreeTypeBoundingBox cbox);
    //    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    //    public delegate int set_mode(FreeTypeRenderer renderer, ulong mode_tag, IntPtr mode_ptr);

    //    public FreeTypeRasterFuncs raster_class;
    //}
    //public class FreeTypeRenderer
    //{
    //    public FreeTypeModule root;

    //}
    //public class FreeTypeList
    //{
    //    public FreeTypeListNode head;
    //    public FreeTypeListNode tail;
    //}
    //public class FreeTypeListNode
    //{
    //    public FreeTypeListNode prev;
    //    public FreeTypeListNode next;
    //    public IntPtr data;

    //}
    //public struct FreeTypeSizeMetrics
    //{
    //    public ushort x_ppem;
    //    public ushort y_ppem;

    //    public long x_scale;
    //    public long y_scale;

    //    public long ascender;
    //    public long descender;
    //    public long height;
    //    public long max_advance;
    //}
    //public class FreeTypeFace
    //{
    //    public long num_faces;
    //    public long face_index;

    //    public long face_flags;
    //    public long style_flags;

    //    public long num_glyphs;

    //    public string family_name;
    //    public string style_name;

    //    public int num_fixed_sizes;
    //    public BitmapSize available_sizes;

    //    public int num_charmaps;
    //    public CharMap[] charmaps;

    //    public FreeTypeGeneric generic;

    //    /*# The following member variables (down to `underline_thickness`) */
    //    /*# are only relevant to scalable outlines; cf. @FT_Bitmap_Size    */
    //    /*# for bitmap fonts.                                              */
    //    public FreeTypeBoundingBox bbox;

    //    public ushort units_per_EM;
    //    public short ascender;
    //    public short descender;
    //    public short height;

    //    public short max_advance_width;
    //    public short max_advance_height;

    //    public short underline_position;
    //    public short underline_thickness;

    //    public FreeTypeGlyphSlot glyph;
    //    public FreeTypeSize size;
    //    public CharMap charmap;

    //    /*@private begin */

    //    public FreeTypeDriver driver;
    //    public FreeTypeMemory memory;
    //    public FreeTypeStream stream;

    //    FreeTypeSize[] sizes_list;

    //    public FreeTypeGeneric autohint;   /* face-specific auto-hinter data */
    //    IntPtr extensions; /* unused                         */

    //    FreeTypeFaceInternal _internal;
    //}
    //public class FreeTypeFaceInternal
    //{
    //    public FreeTypeFace face;
    //    public FreeTypeEncoding encoding;
    //    public ushort platform_id;
    //    public ushort encoding_id;
    //}
    //public class FreeTypeDriver
    //{
    //    public FreeTypeModule root;
    //    public FreeTypeDriverClass clazz;
    //    public FreeTypeList faces_list;
    //    public FreeTypeGlyphLoader glyph_loader;
    //}
    //public class FreeTypeDriverClass
    //{
    //    public FreeTypeModuleClass root;

    //    public long face_object_size;
    //    public long size_object_size;
    //    public long slot_object_size;

    //    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    //    public delegate int init_face(FreeTypeStream stream, FreeTypeFace face, int typeface_index, int num_params, FreeTypeParameter[] parameters);
    //    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    //    public delegate void done_face(FreeTypeFace face);

    //    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    //    public delegate int init_size(FreeTypeSize size);
    //    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    //    public delegate void done_size(FreeTypeSize size);

    //    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    //    public delegate int init_slot(FreeTypeGlyphSlot slot);
    //    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    //    public delegate void done_slot(FreeTypeGlyphSlot slot);

    //    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    //    public delegate int load_glyph(FreeTypeGlyphSlot slot, FreeTypeSize size, uint glyph_index, int load_flags);

    //    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    //    public delegate int get_kerning(FreeTypeFace face, uint left_glyph, uint right_glyph, ref FreeTypeVector kerning);
    //    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    //    public delegate int attach_file(FreeTypeFace face, FreeTypeStream stream);
    //    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    //    public delegate int get_advances(FreeTypeFace face, uint first, uint count, int flags, ref long _fixed);


    //}
    //public class FreeTypeParameter
    //{
    //    public ulong tag;
    //    public IntPtr data;
    //}
    //public class FreeTypeStream
    //{
    //    byte[] _base;
    //    ulong size;
    //    ulong pos;

    //    public StreamDesc descriptor;
    //    public StreamDesc pathname;

    //    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    //    public delegate ulong read(FreeTypeStream stream, ulong offset, byte[] buffer, ulong count);
    //    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    //    public delegate void close(FreeTypeStream stream);

    //    public FreeTypeMemory memory;
    //    public byte cursor;
    //    public byte limit;

    //}
    //[StructLayout(LayoutKind.Explicit)]
    //public struct StreamDesc
    //{
    //    [FieldOffset(0)]
    //    public long value;
    //    [FieldOffset(0)]
    //    public IntPtr pointer;
    //}
    //public class FreeTypeSize
    //{
    //    public FreeTypeFace face;
    //    public FreeTypeGeneric generic;
    //    public FreeTypeSizeMetrics metrics;
    //    public FreeTypeSizeInternal _internal;
    //}
    //public class FreeTypeSizeInternal
    //{
    //    public IntPtr module_data;

    //    public FreeTypeRenderMode autohint_mode;
    //    public FreeTypeSizeMetrics autohint_metrics;
    //}
    public class FreeTypeNative
    {
      
        public static readonly long FT_FACE_FLAG_COLOR = 1L << 14;
      
        [DllImport("freetype.dll", ExactSpelling=false, CallingConvention=CallingConvention.Cdecl)]
        public static extern int FT_Init_FreeType(out IntPtr library);

        [DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern int FT_New_Face(IntPtr library, string file_name, int face_index, out IntPtr face);

        [DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern int FT_Load_Char(IntPtr face, uint char_code, int load_flags);

        [DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        
        public static extern int FT_Load_Glyph(IntPtr face, uint glyph_index, int load_flags);

        [DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        
        public static extern uint FT_Get_Char_Index(IntPtr face, uint char_code);

        [DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern int FT_Set_Char_Size(IntPtr face, IntPtr width, IntPtr height, uint verticalResolution, uint horizontalResolution);

        [DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern int FT_Done_Face(IntPtr face);

        [DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern int FT_Outline_Decompose(ref FTOutline outline, ref FTOutlineFuncs funcs, IntPtr user);

        [DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern bool FT_Get_Color_Glyph_Layer(IntPtr face, uint base_glyph, out uint aglyph_index, out uint acolor_index, ref FTLayerIterator iterator);

    }
}
