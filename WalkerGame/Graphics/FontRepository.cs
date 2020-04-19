using System;
using System.Collections.Generic;
using DingoFont.FreeType;
using DingoFont.FreeType.Numerics;
using DingoFont.Managed;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WalkerGame.Metadata;

namespace WalkerGame.Graphics
{
    [Service]
    public class FontRepository : IDisposable
    {
        private readonly GraphicsDevice graphicsDevice;
        private readonly FreeType freeType;
        private Dictionary<string, FreeTypeFace> faces;
        private Dictionary<string, Font> fonts;

        [Inject]
        public FontRepository(GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
            freeType = new FreeType();
            faces = new Dictionary<string, FreeTypeFace>();
            fonts = new Dictionary<string, Font>();
        }

        public void Load(string path, string name)
        {
            var face = freeType.LoadFont(path);
            faces.TryAdd(name, face);
        }

        public Font GetFont(string name)
        {
            return fonts.GetValueOrDefault(name, null);
        }

        public void RegisterFontCollection(string name, float fontSize, List<string> fontNames)
        {
            var font = new Font(this, graphicsDevice, fontSize);
            foreach (var f in fontNames)
            {
                if (faces.TryGetValue(f, out var face))
                {
                    font.AddFallback(face);
                }
            }

            fonts.TryAdd(name, font);
        }

        public void Draw(string fontName, SpriteBatch spriteBatch, string text, Vector2 pos, Color color)
        {
            if (fonts.TryGetValue(fontName, out var font))
            {
                font.DrawString(spriteBatch, text, pos, color);
            }
        }

        public Vector2 MeasureString(string fontName, string text)
        {
            if (fonts.TryGetValue(fontName, out var font))
            {
                return font.MeasureString(text);
            }
            return Vector2.Zero;
        }

        public void Dispose()
        {
            freeType?.Dispose();
        }
    }
}