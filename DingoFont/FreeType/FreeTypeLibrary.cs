using System;

namespace DingoFont.FreeType
{
    public unsafe class FreeTypeLibrary
    {
        private IntPtr Reference;
        public FreeTypeLibrary()
        {
            FreeTypeNative.FT_Init_FreeType(out var refer);
            Reference = refer;
        }
        public FreeTypeFace FaceFromFile(string path)
        {
            var error = FreeTypeNative.FT_New_Face(Reference, path, 0, out var face_ptr);
            if (error != 0)
                throw FreeTypeException.Except(error);
            var face = FreeTypeFace.CreateFace(face_ptr);
            return face;
        }
    }
}
