using Microsoft.Xna.Framework;
using WalkerGame.Metadata;

namespace WalkerGame.Component
{
    [Component(BufferType.Sparse)]
    public struct Sprite
    {
        public int mapId;
        public int spriteIdx;
    }
}