using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using WalkerGame.Metadata;

namespace WalkerGame.Logic
{
    [GamePart("collisionMessages")]
    public class CollisionMessageManager : PreUpdateTarget
    {
        private readonly EntityRegistry entityRegistry;
        private SortedSet<ValueTuple<ulong, ulong>> handled;

        [Inject]
        public CollisionMessageManager(EntityRegistry entityRegistry)
        {
            this.entityRegistry = entityRegistry;
            handled = new SortedSet<ValueTuple<ulong, ulong>>();
        }
        
        
        public delegate void OnCollisionDelegate(ulong thisEntity, ulong otherEntity, ref Rectangle thisBounds);

        public event OnCollisionDelegate OnCollisionEvent;

        public void Collide(ulong thisEntity, ulong otherEntity, ref Rectangle thisBounds)
        {
            var first = (thisEntity, otherEntity);
            var second = (otherEntity, thisEntity);

            if (handled.Contains(first) || handled.Contains(second))
                return;

            OnCollisionEvent?.Invoke(thisEntity, otherEntity, ref thisBounds);
            handled.Add(first);
        }

        public void PreUpdate(GameTime gameTime)
        {
            handled.Clear();
        }
    }
}