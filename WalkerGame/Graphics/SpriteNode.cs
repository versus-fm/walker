using Microsoft.Xna.Framework;

namespace WalkerGame.Graphics
{
    public class SpriteNode
    {
        public SpriteNode down;
        public SpriteNode right;
        public Rectangle area;
        public bool used;

        public int x => area.X;
        public int y => area.Y;
        public int w => area.Width;
        public int h => area.Height;

        public SpriteNode(Rectangle area)
        {
            this.area = area;
            used = false;
        }

        public SpriteNode(int x, int y, int w, int h)
        {
            area = new Rectangle(x, y, w ,h);
            used = false;
        }
    }
}