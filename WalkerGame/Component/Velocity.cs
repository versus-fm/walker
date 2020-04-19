using Microsoft.Xna.Framework;
using Svelto.ECS;
using WalkerGame.Metadata;

namespace WalkerGame.Component
{
    public struct Velocity : IEntityStruct
    {
        public Vector2 vel;
    }
}