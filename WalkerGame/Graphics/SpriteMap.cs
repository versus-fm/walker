using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WalkerGame.Graphics
{
    public class SpriteMap
    {
        public const int MAP_SIZE = 1024;


        private readonly GraphicsDevice graphicsDevice;
        private int spriteCount;
        private Rectangle[] regions;
        private Dictionary<string, int> regionIndices;
        private Color[] sheetData;

        private Texture2D map;
        private SpriteNode root;

        public SpriteMap(GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
            spriteCount = 0;
            regions = new Rectangle[256];
            regionIndices = new Dictionary<string, int>();
            map = new Texture2D(graphicsDevice, MAP_SIZE, MAP_SIZE);
            sheetData = new Color[MAP_SIZE * MAP_SIZE];

            root = new SpriteNode(0, 0, MAP_SIZE, MAP_SIZE);
        }

        public ref readonly Rectangle GetRegion(int index)
        {
            return ref regions[index];
        }

        public Rectangle GetRegion(string name)
        {
            return regionIndices.TryGetValue(name, out var idx) ? regions[idx] : new Rectangle(0, 0, 0, 0);
        }

        public int GetIndex(string name)
        {
            return regionIndices.GetValueOrDefault(name, -1);
        }

        public bool HasRegion(string name) => regionIndices.ContainsKey(name);

        public Texture2D GetAtlas()
        {
            return map;
        }

        private void CopyTextureRegion(Texture2D texture2D, Point pos)
        {
            var box = new Rectangle(pos, texture2D.Bounds.Size);
            var textureData = new Color[texture2D.Width * texture2D.Height];
            texture2D.GetData(textureData);
            map.GetData(sheetData);

            for (int x = box.X; x < box.Right; x++)
            {
                for (int y = box.Y; y < box.Bottom; y++)
                {
                    var dx = x - box.X;
                    var dy = y - box.Y;
                    var di = dx + box.Width * dy;
                    var idx = x + map.Width * y;

                    sheetData[idx] = textureData[di];
                }
            }
            
            map.SetData(sheetData);
        }

        public bool Place(List<SpriteEntry> sprites, int startIndex, out int packed)
        {
            while (startIndex < sprites.Count)
            {
                var entry = sprites[startIndex];
                var block = entry.Texture2D.Bounds;
                var node = FindNode(root, block.Width, block.Height);
                if (node != null)
                {
                    var fit = SplitNode(node, block.Width, block.Height);
                    var region = new Rectangle(fit.x, fit.y, block.Width, block.Height);
                    var idx = AddRegion(region);
                    regionIndices[entry.Name] = idx;

                    CopyTextureRegion(entry.Texture2D, new Point(fit.x, fit.y));
                    startIndex++;
                    continue;
                }

                packed = startIndex;
                return false;
            }

            packed = startIndex;
            return true;
        }

        private SpriteNode FindNode(SpriteNode node, int width, int height)
        {
            if (node.used)
            {
                return FindNode(node.right, width, height) ?? FindNode(node.down, width, height);
            }
            if (width <= node.area.Width && height <= node.area.Height)
            {
                return node;
            }
            return null;
        }

        private SpriteNode SplitNode(SpriteNode node, int width, int height)
        {
            node.used = true;
            node.down = new SpriteNode(node.x, node.y + height, node.w, node.h - height);
            node.right = new SpriteNode(node.x + width, node.y, node.w - width, height);
            return node;
        }

        private int AddRegion(Rectangle region)
        {
            if (spriteCount >= regions.Length)
                GrowRegionArray();
            var idx = spriteCount;
            regions[spriteCount++] = region;
            return idx;
        }

        private void GrowRegionArray()
        {
            var newArr = new Rectangle[regions.Length * 2];
            Array.Copy(regions, newArr, spriteCount);
            regions = newArr;
        }
    }
}