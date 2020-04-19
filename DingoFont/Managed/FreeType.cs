using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using DingoFont.FreeType;

namespace DingoFont.Managed
{
    public class FreeType : IDisposable
    {
        private readonly FreeTypeLibrary freeTypeLibrary;
        private List<IDisposable> disposables;

        public FreeType(FreeTypeLibrary freeTypeLibrary)
        {
            this.freeTypeLibrary = freeTypeLibrary;
            disposables = new List<IDisposable>();
        }

        public FreeType() : this(new FreeTypeLibrary())
        {
            
        }

        public FreeTypeFace LoadFont(string file)
        {
            var face = freeTypeLibrary.FaceFromFile(file);
            disposables.Add(face);
            return face;
        }
        
        public void Dispose()
        {
            disposables.ForEach(x => x.Dispose());
        }
    }
}