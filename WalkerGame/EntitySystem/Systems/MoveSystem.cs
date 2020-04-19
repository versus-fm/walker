using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using Svelto.ECS;
using WalkerGame.Component;
using WalkerGame.Data;
using WalkerGame.Logic;
using WalkerGame.Logic.SystemMessage.Collision;
using WalkerGame.Metadata;
using NotImplementedException = System.NotImplementedException;
using RectangleF = System.Drawing.RectangleF;

namespace WalkerGame.EntitySystem.Systems
{
    [System("move_system")]
    public class MoveSystem : IQueryingEntitiesEngine, UpdateTarget
    {
        private readonly SpatialGrid spatialGrid;
        private readonly CollisionMessageManager collisionMessageManager;
        private readonly SortedSet<EGID> entityQuery;

        [Inject]
        public MoveSystem(SpatialGrid spatialGrid,
            CollisionMessageManager collisionMessageManager)
        {
            this.spatialGrid = spatialGrid;
            this.collisionMessageManager = collisionMessageManager;

            entityQuery = new SortedSet<EGID>();
        }
        public void Update(GameTime gameTime)
        {
            var entities = entitiesDB.QueryEntities<Transform, Velocity>(EntityGroups.Collidables);
            foreach (var entity in entities)
            {
                var old = entity.entityStructA.bounds;
                var velocity = entity.entityStructB;
                entity.entityStructA.bounds.X += velocity.vel.X * gameTime.GetElapsedSeconds();
                entity.entityStructA.bounds.Y += velocity.vel.Y * gameTime.GetElapsedSeconds();
                
                spatialGrid.MoveEntity(entity.entityStructA.ID, ref old, ref entity.entityStructA.bounds);
                spatialGrid.QueryRegion(entityQuery, ref entity.entityStructA.bounds);
                foreach (var other in entityQuery)
                {
                    if (other != entity.entityStructA.ID)
                    {
                        var otherTransform = entitiesDB.QueryEntity<Transform>(other);
                        if (entity.entityStructA.bounds.Intersects(otherTransform.bounds))
                            collisionMessageManager.Collide(ref entity.entityStructA, ref otherTransform, CollisionMessageChannel.Collision);
                    }
                }
            }
            /*entityRegistry.Loop((int entity, ref Transform t, ref Velocity v) =>
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
            });*/
        }

        public IEntitiesDB entitiesDB { get; set; }
        public void Ready()
        {
            
        }
    }
}