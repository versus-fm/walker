using DingoFont.FreeType.InternalStructure;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using DingoFont.FreeType.Numerics;

namespace DingoFont.FreeType
{
    public unsafe class FreeTypeFace : IDisposable
    {
        private IntPtr reference;
        private FTFace _internal;
        private bool copy;
        private byte[] charCopy = new byte[4];
        
        public FontMetrics FontMetrics { get; private set; }

        public static FreeTypeFace CreateFace(IntPtr reference)
        {
            var ftf = new FreeTypeFace
            {
                reference = reference,
                _internal = *(FTFace*)reference,
                copy = false
            };
            return ftf;
        }

        public ref FTGlyphSlot GetGlyphSlot()
        {
            return ref *(FTGlyphSlot*)_internal.glyph;
        }

        public void SetCharSize(Fixed26Dot6 size)
        {
            var err = FreeTypeNative.FT_Set_Char_Size(reference, IntPtr.Zero, (IntPtr)size.Value, 96, 96);
            CalculateMetrics();
            if (err != 0)
                throw FreeTypeException.Except(err);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="glyph">Char index <see cref="TryCharIndex"/></param>
        /// <param name="flags"></param>
        /// <exception cref="FreeTypeException"></exception>
        public void LoadGlyph(uint glyph, LoadFlags flags)
        {
            var err = FreeTypeNative.FT_Load_Glyph(reference, glyph, (int)flags);
            if (err != 0)
                throw FreeTypeException.Except(err);
        }
        
        public void LoadChar(uint c, LoadFlags flags)
        {
            var err = FreeTypeNative.FT_Load_Char(reference, c, (int)flags);
            if (err != 0)
                throw FreeTypeException.Except(err);
        }

        /// <summary>
        /// Overload for loading glyphs that can't be expressed as a char literal like emojis.
        /// Grabs the first 4 bytes in the string
        /// </summary>
        /// <param name="glyph"></param>
        /// <param name="flags"></param>
        /// <returns>bool indicating whether the glyph exists in this face</returns>
        public bool LoadGlyph(string c, LoadFlags flags)
        {
            Encoding.UTF32.GetBytes(c, 0, 2, charCopy, 0);
            var glyphCode = BitConverter.ToUInt32(charCopy, 0);
            if (TryCharIndex(glyphCode, out var index))
            {
                LoadGlyph(index, flags);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Tries to get the char index for the char code
        /// </summary>
        /// <param name="c"></param>
        /// <param name="glyph"></param>
        /// <returns>Flag indicating whether the glyph exists</returns>
        public bool TryCharIndex(uint c, out uint glyph)
        {
            var index = FreeTypeNative.FT_Get_Char_Index(reference, c);
            glyph = index;
            return glyph != 0;
        }

        public void CalculateMetrics()
        {
            LoadChar('l', LoadFlags.RENDER);
            var slot = GetGlyphSlot();
            FontMetrics = new FontMetrics
            {
                Height = (int) slot.bitmap.rows,
                Width = (int) slot.bitmap.width
            };
        }

        public bool HasColor()
        {
            return _internal.HasColor;
        }

        public bool GetColorLayer(ref FTLayerIterator iterator, ref FTGlyphSlot glyph)
        {
            return FreeTypeNative.FT_Get_Color_Glyph_Layer(reference, glyph.Index, out var a, out var b, ref iterator);
        }

        public void Dispose()
        {
            if(!copy)
            {
                FreeTypeNative.FT_Done_Face(reference);
            }
        }
    }
}
