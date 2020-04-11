using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WalkerGame.Component;
using WalkerGame.Metadata;

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
        public SpriteRepository(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
            this.graphicsDevice = graphicsDevice;
            this.spriteBatch = spriteBatch;
            spriteMaps = new SpriteMap[256];
            mapCount = 0;
            internalPreCache = new List<SpriteEntry>();
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
        
        public bool PlaceInternalCache()
        {
            internalPreCache = internalPreCache.OrderByDescending(entry => Math.Max(entry.Texture2D.Width, entry.Texture2D.Height)).ToList();
            return Place(internalPreCache, 0);
        }
        
        private bool Place(List<SpriteEntry> sprites, int startIndex)
        {
            if (spriteMaps[mapCount] == null)
                spriteMaps[mapCount] = new SpriteMap(graphicsDevice);

            var fit = spriteMaps[mapCount].Place(sprites, startIndex, out var packed);
            if (packed == startIndex)
                return false;
            if (packed >= sprites.Count - 1)
                return true;
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
    }
}