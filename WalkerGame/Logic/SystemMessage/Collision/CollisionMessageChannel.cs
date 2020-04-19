using System;
using System.Collections.Generic;
using Svelto.ECS;
using WalkerGame.Component;

namespace WalkerGame.Logic.SystemMessage.Collision
{
    public class CollisionMessageChannel
    {
        public static readonly int Collision = 0;
        public static readonly int TriggerArea = 1;
        private int id;
        private Dictionary<object, CollisionMessageManager.OnCollisionDelegate> delegates;
        private SortedSet<ValueTuple<EGID, EGID>> handled;

        public CollisionMessageChannel(int id)
        {
            this.id = id;
            delegates = new Dictionary<object, CollisionMessageManager.OnCollisionDelegate>();
            handled = new SortedSet<(EGID, EGID)>();
        }

        public void Invoke(ref Transform thisTransform, ref Transform otherTransform)
        {
            var first = (thisTransform.ID, otherTransform.ID);
            var second = (otherTransform.ID, thisTransform.ID);

            if (handled.Contains(first) || handled.Contains(second))
                return;

            foreach (var onCollisionDelegate in delegates.Values)
            {
                onCollisionDelegate.Invoke(ref thisTransform, ref otherTransform);
            }
            
            handled.Add(first);
        }

        public void PreUpdate()
        {
            handled.Clear();
        }

        public void Subscribe(object source, CollisionMessageManager.OnCollisionDelegate @delegate)
        {
            delegates.TryAdd(source, @delegate);
        }
    }
}