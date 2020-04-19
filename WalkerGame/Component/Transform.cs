using Microsoft.Xna.Framework;
using MonoGame.Extended;
using Svelto.ECS;
using WalkerGame.Data;
using WalkerGame.Metadata;

namespace WalkerGame.Component
{
    public struct Transform : IEntityStruct, INeedEGID
    {
        public RectangleF bounds;
        public float rotation;
        public EGID ID { get; set; }
    }
}