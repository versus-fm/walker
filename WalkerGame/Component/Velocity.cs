using Microsoft.Xna.Framework;
using WalkerGame.Metadata;

namespace WalkerGame.Component
{
    [Component(BufferType.Sparse)]
    public struct Velocity
    {
        public Vector2 vel;
    }
}