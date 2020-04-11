using Microsoft.Xna.Framework;
using WalkerGame.Metadata;

namespace WalkerGame.Component
{
    [Component(BufferType.Sparse)]
    public struct Transform
    {
        public Vector2 pos;
        public Vector2 size;
        public float rotation;
        
        public Rectangle Bounds => new Rectangle(pos.ToPoint(), size.ToPoint());
    }
}