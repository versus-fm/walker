using Microsoft.Xna.Framework;
using WalkerGame.Data;
using WalkerGame.Metadata;

namespace WalkerGame.Component
{
    [Component(BufferType.Sparse)]
    public struct Transform
    {
        public Vector2 pos;
        public Vector2 size;
        public float rotation;
        public int gridId;
        

        public Rectangle Bounds()
        {
            return new Rectangle(pos.ToPoint(), pos.ToPoint());
        }
    }
}