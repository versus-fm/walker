using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using WalkerGame.Component;
using WalkerGame.Data;
using WalkerGame.Logic;
using WalkerGame.Metadata;
using NotImplementedException = System.NotImplementedException;

namespace WalkerGame.EntitySystem.Systems
{
    [System]
    public class MoveSystem : ISystem
    {
        private readonly EntityRegistry entityRegistry;
        private readonly SpatialGrid spatialGrid;
        private readonly CollisionMessageManager collisionMessageManager;
        private readonly SortedSet<ulong> entityQuery;

        [Inject]
        public MoveSystem(EntityRegistry entityRegistry,
            SpatialGrid spatialGrid,
            CollisionMessageManager collisionMessageManager)
        {
            this.entityRegistry = entityRegistry;
            this.spatialGrid = spatialGrid;
            this.collisionMessageManager = collisionMessageManager;

            entityQuery = new SortedSet<ulong>();
        }
        public void Update(GameTime gameTime)
        {
            entityRegistry.Loop((int entity, ref Transform t, ref Velocity v) =>
            {
                var uuid = entityRegistry.EntityUIDFromIdx(entity);
                var oldRect = t.Bounds();
                t.pos += v.vel * gameTime.GetElapsedSeconds();
                var newRect = t.Bounds();
                
                spatialGrid.MoveEntity(uuid, ref oldRect, ref newRect);
                spatialGrid.QueryRegion(entityQuery, ref newRect);
                foreach (var other in entityQuery)
                {
                    if (other != uuid)
                        collisionMessageManager.Collide(uuid, other, ref newRect);
                }
            });
        }

        public void Draw(GameTime gameTime)
        {
            
        }
    }
}