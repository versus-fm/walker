using System;
using System.Collections.Generic;
using System.Text;

namespace DingoFont.FreeType
{
    public class FreeTypeException : Exception
    {
        public FreeTypeException(int code, string message) : base($"Native freetype exception {code}: {message}")
        {

        }

        public static FreeTypeException Except(int code)
        {
            var Message = "";
            switch (code)
            {
                case 0x01:
                    Message = "cannot open resource"; break;
                case 0x02:
                    Message = "unknown file format"; break;
                case 0x03:
                    Message = "broken file"; break;
                case 0x04:
                    Message = "invalid FreeType version"; break;
                case 0x05:
                    Message = "module version is too low"; break;
                case 0x06:
                    Message = "invalid argument"; break;
                case 0x07:
                    Message = "unimplemented feature"; break;
                case 0x08:
                    Message = "broken table"; break;
                case 0x09:
                    Message = "broken offset within table"; break;
                case 0x0A:
                    Message = "array allocation size too large"; break;
                case 0x0B:
                    Message = "missing module"; break;
                case 0x0C:
                    Message = "missing property"; break;

                /* glyph/character errors */

                case 0x10:
                    Message = "invalid glyph index"; break;
                case 0x11:
                    Message = "invalid character code"; break;
                case 0x12:
                    Message = "unsupported glyph image format"; break;
                case 0x13:
                    Message = "cannot render this glyph format"; break;
                case 0x14:
                    Message = "invalid outline"; break;
                case 0x15:
                    Message = "invalid composite glyph"; break;
                case 0x16:
                    Message = "too many hints"; break;
                case 0x17:
                    Message = "invalid pixel size"; break;

                /* handle errors */

                case 0x20:
                    Message = "invalid object handle"; break;
                case 0x21:
                    Message = "invalid library handle"; break;
                case 0x22:
                    Message = "invalid module handle"; break;
                case 0x23:
                    Message = "invalid face handle"; break;
                case 0x24:
                    Message = "invalid size handle"; break;
                case 0x25:
                    Message = "invalid glyph slot handle"; break;
                case 0x26:
                    Message = "invalid charmap handle"; break;
                case 0x27:
                    Message = "invalid cache manager handle"; break;
                case 0x28:
                    Message = "invalid stream handle"; break;

                /* driver errors */

                case 0x30:
                    Message = "too many modules"; break;
                case 0x31:
                    Message = "too many extensions"; break;

                /* memory errors */

                case 0x40:
                    Message = "out of memory"; break;
                case 0x41:
                    Message = "unlisted object"; break;

                /* stream errors */

                case 0x51:
                    Message = "cannot open stream"; break;
                case 0x52:
                    Message = "invalid stream seek"; break;
                case 0x53:
                    Message = "invalid stream skip"; break;
                case 0x54:
                    Message = "invalid stream read"; break;
                case 0x55:
                    Message = "invalid stream operation"; break;
                case 0x56:
                    Message = "invalid frame operation"; break;
                case 0x57:
                    Message = "nested frame access"; break;
                case 0x58:
                    Message = "invalid frame read"; break;

                /* raster errors */

                case 0x60:
                    Message = "raster uninitialized"; break;
                case 0x61:
                    Message = "raster corrupted"; break;
                case 0x62:
                    Message = "raster overflow"; break;
                case 0x63:
                    Message = "negative height while rastering"; break;

                /* cache errors */

                case 0x70:
                    Message = "too many registered caches"; break;

                /* TrueType and SFNT errors */

                case 0x80:
                    Message = "invalid opcode"; break;
                case 0x81:
                    Message = "too few arguments"; break;
                case 0x82:
                    Message = "stack overflow"; break;
                case 0x83:
                    Message = "code overflow"; break;
                case 0x84:
                    Message = "bad argument"; break;
                case 0x85:
                    Message = "division by zero"; break;
                case 0x86:
                    Message = "invalid reference"; break;
                case 0x87:
                    Message = "found debug opcode"; break;
                case 0x88:
                    Message = "found ENDF opcode in execution stream"; break;
                case 0x89:
                    Message = "nested DEFS"; break;
                case 0x8A:
                    Message = "invalid code range"; break;
                case 0x8B:
                    Message = "execution context too long"; break;
                case 0x8C:
                    Message = "too many function definitions"; break;
                case 0x8D:
                    Message = "too many instruction definitions"; break;
                case 0x8E:
                    Message = "SFNT font table missing"; break;
                case 0x8F:
                    Message = "horizontal header (hhea; break; table missing"; break;
                case 0x90:
                    Message = "locations (loca; break; table missing"; break;
                case 0x91:
                    Message = "name table missing"; break;
                case 0x92:
                    Message = "character map (cmap; break; table missing"; break;
                case 0x93:
                    Message = "horizontal metrics (hmtx; break; table missing"; break;
                case 0x94:
                    Message = "PostScript (post; break; table missing"; break;
                case 0x95:
                    Message = "invalid horizontal metrics"; break;
                case 0x96:
                    Message = "invalid character map (cmap; break; format"; break;
                case 0x97:
                    Message = "invalid ppem value"; break;
                case 0x98:
                    Message = "invalid vertical metrics"; break;
                case 0x99:
                    Message = "could not find context"; break;
                case 0x9A:
                    Message = "invalid PostScript (post; break; table format"; break;
                case 0x9B:
                    Message = "invalid PostScript (post; break; table"; break;
                case 0x9C:
                    Message = "found FDEF or IDEF opcode in glyf bytecode"; break;
                case 0x9D:
                    Message = "missing bitmap in strike"; break;

                /* CFF, CID, and Type 1 errors */

                case 0xA0:
                    Message = "opcode syntax error"; break;
                case 0xA1:
                    Message = "argument stack underflow"; break;
                case 0xA2:
                    Message = "ignore"; break;
                case 0xA3:
                    Message = "no Unicode glyph name found"; break;
                case 0xA4:
                    Message = "glyph too big for hinting"; break;

                /* BDF errors */

                case 0xB0:
                    Message = "`STARTFONT' field missing"; break;
                case 0xB1:
                    Message = "`FONT' field missing"; break;
                case 0xB2:
                    Message = "`SIZE' field missing"; break;
                case 0xB3:
                    Message = "`FONTBOUNDINGBOX' field missing"; break;
                case 0xB4:
                    Message = "`CHARS' field missing"; break;
                case 0xB5:
                    Message = "`STARTCHAR' field missing"; break;
                case 0xB6:
                    Message = "`ENCODING' field missing"; break;
                case 0xB7:
                    Message = "`BBX' field missing"; break;
                case 0xB8:
                    Message = "`BBX' too big"; break;
                case 0xB9:
                    Message = "Font header corrupted or missing fields"; break;
                case 0xBA:
                    Message = "Font glyphs corrupted or missing fields"; break;
            }
            return new FreeTypeException(code, Message);
        }
    }
}
