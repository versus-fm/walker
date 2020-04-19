using System;
using System.Collections.Generic;
using System.Linq;
using DingoFont.FreeType;
using DingoFont.FreeType.InternalStructure;
using DingoFont.FreeType.Numerics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WalkerGame.Component;

namespace WalkerGame.Graphics
{
    public class Font
    {
        private readonly FontRepository fontRepository;
        private readonly GraphicsDevice graphicsDevice;
        public float FontSize { get; private set; }
        private LinkedList<FreeTypeFace> faces;
        private List<SpriteMap> spriteMaps;
        private Dictionary<uint, GlyphEntry> spriteReferences;

        private int whitespace;
        
        public Font(FontRepository fontRepository, GraphicsDevice graphicsDevice, float fontSize = 22)
        {
            this.fontRepository = fontRepository;
            this.graphicsDevice = graphicsDevice;
            spriteMaps = new List<SpriteMap>();
            faces = new LinkedList<FreeTypeFace>();
            spriteReferences = new Dictionary<uint, GlyphEntry>();
            SetFontSize(fontSize);
        }

        public void AddFallback(FreeTypeFace face)
        {
            faces.AddLast(face);
            face.SetCharSize(Fixed26Dot6.FromSingle(FontSize));
            if (whitespace < face.FontMetrics.Width)
                whitespace = face.FontMetrics.Width;
        }

        public Texture2D GetAtlas(int index)
        {
            return spriteMaps[index].GetAtlas();
        }

        private bool TryGlyph(uint c, out GlyphEntry glyphEntry)
        {
            return spriteReferences.TryGetValue(c, out glyphEntry) || CacheGlyph(c, out glyphEntry);
        }

        private unsafe bool CacheGlyph(uint c, out GlyphEntry glyphEntry)
        {
            foreach (var face in faces)
            {
                if (face.TryCharIndex(c, out var glyph))
                {
                    if (face.HasColor())
                        face.LoadGlyph(glyph, LoadFlags.RENDER);//NOT IMPLEMENTED face.LoadGlyph(glyph, LoadFlags.COLOR);
                    else
                        face.LoadGlyph(glyph, LoadFlags.RENDER);
                    var glyphSlot = face.GetGlyphSlot();
                    var w = glyphSlot.bitmap.width;
                    var h = glyphSlot.bitmap.rows;

                    var buffer = (byte*)glyphSlot.bitmap.buffer;
                    
                    

                    if (glyphSlot.bitmap.pitch < 0)
                        continue;
                    
                    var colorData = new Color[w * h];

                    var result = glyphSlot.bitmap.pixel_mode switch
                    {
                        FTPixelMode.FT_PIXEL_MODE_GRAY => FTPixelModeGrayPutColorData(colorData, w, h, buffer),
                        FTPixelMode.FT_PIXEL_MODE_BGRA => FTPixelModeColorPutColorData(colorData, w, h, buffer),
                        _ => throw new ArgumentException("Unsupported pixel mode")
                    };

                    if (result)
                    {
                        if (spriteMaps.Count > 0 && spriteMaps[spriteMaps.Count - 1].Place(colorData, (int) w, (int) h, out var idx))
                        {
                            glyphEntry = new GlyphEntry
                            {
                                sprite = new Sprite
                                {
                                    mapId = spriteMaps.Count - 1,
                                    spriteIdx = idx
                                },
                                height = (int)h,
                                width = (int)w,
                                leftOffset = glyphSlot.bitmap_left,
                                topOffset = glyphSlot.bitmap_top
                            };
                            spriteReferences.TryAdd(c, glyphEntry);
                        }
                        else
                        {
                            var newMap = new SpriteMap(graphicsDevice);
                            if (newMap.Place(colorData, (int) w, (int) h, out idx))
                            {
                                glyphEntry = new GlyphEntry
                                {
                                    sprite = new Sprite
                                    {
                                        mapId = spriteMaps.Count,
                                        spriteIdx = idx
                                    },
                                    height = (int)h,
                                    width = (int)w,
                                    leftOffset = glyphSlot.bitmap_left,
                                    topOffset = glyphSlot.bitmap_top
                                };
                                spriteReferences.TryAdd(c, glyphEntry);
                                spriteMaps.Add(newMap);
                            }
                        }
                    }

                }
            }
            glyphEntry = default;
            return false;
        }

        private unsafe bool FTPixelModeGrayPutColorData(Color[] colors, uint width, uint height, byte* buffer)
        {
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    var bix = width * y + x;
                    colors[bix] = new Color(buffer[bix], buffer[bix], buffer[bix], buffer[bix]);
                }
            }

            return true;
        }
        
        private unsafe bool FTPixelModeColorPutColorData(Color[] colors, uint width, uint height, byte* buffer)
        {
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    var bix = width * 4 * y + x;
                    colors[bix] = new Color(buffer[bix+2], buffer[bix+1], buffer[bix+0], buffer[bix+3]);
                }
            }

            return true;
        }

        public Vector2 MeasureString(string text)
        {
            var v = new Vector2();
            for (int i = 0; i < text.Length; i++)
            {
                if (char.IsWhiteSpace(text[i]))
                    v.X += FontSize / 4;
                
                var unicodeCodePoint = (uint)char.ConvertToUtf32(text, i);
                if (unicodeCodePoint > 0xffff)
                {
                    i++;
                }

                if (TryGlyph(unicodeCodePoint, out var glyphEntry))
                {
                    v.X += glyphEntry.width + glyphEntry.leftOffset;
                    if (v.Y < glyphEntry.height) v.Y = glyphEntry.height;
                }
            }

            return v;
        }

        public void DrawString(SpriteBatch spriteBatch, string text, Vector2 pos, Color color)
        {
            for (int i = 0; i < text.Length; i++)
            {
                if (char.IsWhiteSpace(text[i]))
                    pos.X += FontSize / 4;
                
                var unicodeCodePoint = (uint)char.ConvertToUtf32(text, i);
                if (unicodeCodePoint > 0xffff)
                {
                    i++;
                }

                if (TryGlyph(unicodeCodePoint, out var glyphEntry))
                {
                    var sprite = glyphEntry.sprite;
                    var map = spriteMaps[sprite.mapId];
                    var texture = map.GetAtlas();
                    var region = map.GetRegion(sprite.spriteIdx);
                    spriteBatch.Draw(texture, pos - new Vector2(-glyphEntry.leftOffset, glyphEntry.topOffset), region, color);

                    pos.X += region.Width + glyphEntry.leftOffset;
                }
            }
        }

        public void SetFontSize(float fontSize)
        {
            FontSize = fontSize;
            foreach (var freeTypeFace in faces)
            {
                freeTypeFace.SetCharSize(Fixed26Dot6.FromSingle(fontSize));
                if (whitespace < freeTypeFace.FontMetrics.Width)
                    whitespace = freeTypeFace.FontMetrics.Width;
            }
            foreach (var spriteMap in spriteMaps)
            {
                spriteMap.Dispose();
            }
            spriteMaps.Clear();
            spriteReferences.Clear();
        }
    }
}