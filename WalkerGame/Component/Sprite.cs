using Microsoft.Xna.Framework;
using Svelto.ECS;
using WalkerGame.Metadata;

namespace WalkerGame.Component
{
    public struct Sprite : IEntityStruct
    {
        public int mapId;
        public int spriteIdx;
    }
}