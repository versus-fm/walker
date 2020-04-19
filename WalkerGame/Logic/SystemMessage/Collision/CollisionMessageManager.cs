using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using Svelto.ECS;
using WalkerGame.Component;
using WalkerGame.Metadata;
using WalkerGame.Reflection;

namespace WalkerGame.Logic.SystemMessage.Collision
{
    [GamePart("collisionMessages")]
    public class CollisionMessageManager : PreUpdateTarget, PostConstruct
    {
        private readonly ObjectGraph objectGraph;
        private Dictionary<int, CollisionMessageChannel> channels;

        [Inject]
        public CollisionMessageManager(ObjectGraph objectGraph)
        {
            this.objectGraph = objectGraph;
            channels = new Dictionary<int, CollisionMessageChannel>();
        }
        
        
        public delegate void OnCollisionDelegate(ref Transform thisTransform, ref Transform otherTransform);

        public void Collide(ref Transform thisTransform, ref Transform otherTransform, int channel)
        {
            if (channels.ContainsKey(channel))
                channels[channel].Invoke(ref thisTransform, ref otherTransform);
        }

        public void PreUpdate(GameTime gameTime)
        {
            foreach (var collisionMessageChannel in channels.Values)
            {
                collisionMessageChannel.PreUpdate();
            }
        }

        public void Post()
        {
            objectGraph.DoOnAttribute<SubscribeAttribute>((attribute, type) =>
            {
                if (type.Implements<CollisionSubscriber>() && attribute.MessageName.Equals("collision", StringComparison.CurrentCultureIgnoreCase))
                {
                    var subscriber = (CollisionSubscriber)objectGraph.Construct(type);
                    if (!channels.ContainsKey(subscriber.GetChannel()))
                        channels.Add(subscriber.GetChannel(), new CollisionMessageChannel(subscriber.GetChannel()));
                    
                    channels[subscriber.GetChannel()].Subscribe(subscriber, subscriber.Collide);
                }
            });
        }
    }
}