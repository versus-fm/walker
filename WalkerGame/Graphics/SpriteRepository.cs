using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WalkerGame.Component;
using WalkerGame.Metadata;
using WalkerGame.Reflection;
using WalkerGame.Resource;

namespace WalkerGame.Graphics
{
    [Service]
    public class SpriteRepository
    {
        private readonly GraphicsDevice graphicsDevice;
        private readonly SpriteBatch spriteBatch;
        private int mapCount;
        private SpriteMap[] spriteMaps;
        private List<SpriteEntry> internalPreCache;
        private List<SpritesheetDefinition> internalSpritesheets;
        public SpriteRepository(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
            this.graphicsDevice = graphicsDevice;
            this.spriteBatch = spriteBatch;
            spriteMaps = new SpriteMap[256];
            mapCount = 0;
            internalPreCache = new List<SpriteEntry>();
            internalSpritesheets = new List<SpritesheetDefinition>();
        }

        public void PreRegister(Texture2D texture2D, string name)
        {
            internalPreCache.Add(new SpriteEntry
            {
                Name = name,
                Texture2D = texture2D
            });
        }

        public bool Place(List<SpriteEntry> sprites)
        {
            sprites = sprites.OrderByDescending(entry => Math.Max(entry.Texture2D.Width, entry.Texture2D.Height)).ToList();
            return Place(sprites, 0);
        }

        public bool AttemptSubdivision(SpritesheetDefinition spritesheetDefinition)
        {
            if (string.IsNullOrEmpty(spritesheetDefinition.spriteName))
                return false;
            if (internalPreCache.Count > 0)
                internalSpritesheets.Add(spritesheetDefinition);
            else
            {
                for (var i = 0; i < mapCount; i++)
                {
                    if (spriteMaps[i].Subdivide(spritesheetDefinition.spriteName, spritesheetDefinition.spriteWidth,
                        spritesheetDefinition.spriteHeight, spritesheetDefinition.names.ToArray()))
                        return true;
                }
            }

            return true;
        }

        public bool TryGetTexture(string name, out Texture2D texture, out Rectangle region)
        {
            foreach (var spriteMap in spriteMaps)
            {
                int idx;
                if ((idx = spriteMap.GetIndex(name)) != -1)
                {
                    texture = spriteMap.GetAtlas();
                    region = spriteMap.GetRegion(idx);
                    return true;
                }
            }

            texture = null;
            region = Rectangle.Empty;
            return false;
        }
        
        public bool PlaceInternalCache()
        {
            internalPreCache = internalPreCache.OrderByDescending(entry => Math.Max(entry.Texture2D.Width, entry.Texture2D.Height)).ToList();
            var result = Place(internalPreCache, 0);
            internalPreCache.Clear();
            
            internalSpritesheets.ForEach(definition => AttemptSubdivision(definition));
            internalSpritesheets.Clear();
            
            return result;
        }
        
        private bool Place(List<SpriteEntry> sprites, int startIndex)
        {
            if (spriteMaps[mapCount] == null)
            {
                var size = Math.Max(1024, Math.Max(sprites[startIndex].Texture2D.Width, sprites[startIndex].Texture2D.Height));
                spriteMaps[mapCount] = new SpriteMap(graphicsDevice, size);
            }

            var fit = spriteMaps[mapCount].Place(sprites, startIndex, out var packed);
            if (packed == startIndex)
                return false;
            if (packed >= sprites.Count - 1)
            {
                mapCount++;
                return true;
            }
            mapCount++;
            return Place(sprites, packed);
        }

        public SpriteMap GetMap(int index)
        {
            return spriteMaps[index];
        }

        public Sprite CreateSprite(string name)
        {
            for (var mapIndex = 0; mapIndex < spriteMaps.Length; mapIndex++)
            {
                var map = spriteMaps[mapIndex];
                if (map.HasRegion(name))
                {
                    return new Sprite
                    {
                        mapId = mapIndex,
                        spriteIdx = map.GetIndex(name)
                    };
                }
            }

            return new Sprite
            {
                mapId = -1,
                spriteIdx = -1
            };
        }

        public void Draw(ref Sprite sprite, ref Vector2 pos, ref Vector2 size, Color color, 
            float rotation = 0f, Vector2 origin = default, SpriteEffects spriteEffects = SpriteEffects.None, float layerDepth = 1f)
        {
            if (sprite.mapId < 0 || sprite.spriteIdx < 0)
                return;
            var map = spriteMaps[sprite.mapId];
            var region = map.GetRegion(sprite.spriteIdx);
            
            spriteBatch.Draw(map.GetAtlas(), new Rectangle(pos.ToPoint(), size.ToPoint()), region, color, rotation, origin, spriteEffects, layerDepth);
        }

        public void DrawSpritesheetIndices(Font font, Vector2 pos, string name, int tileWidth, int tileHeight, int scale = 2)
        {
            if (TryGetTexture(name, out var texture, out var region))
            {
                spriteBatch.Draw(texture, new Rectangle((int)pos.X, (int)pos.Y, region.Width * scale, region.Height * scale), region, Color.White);
                var tw = tileWidth * scale;
                var th = tileHeight * scale;

                var w = region.Width * scale;
                var h = region.Height * scale;

                var xw = w / tw;
                var xh = h / th;

                var maxIndex = xh * xw;

                for (int i = 0; i < maxIndex; i++)
                {
                    var x = i % xw;
                    var y = i / xw;
                    font.DrawString(spriteBatch, i.ToString(), pos + new Vector2(x * tw, y * th) + new Vector2(tw / 2, th / 2), Color.White);
                }
            }
        }
    }
}