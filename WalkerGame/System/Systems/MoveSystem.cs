using Microsoft.Xna.Framework;
using WalkerGame.Component;
using WalkerGame.Metadata;
using NotImplementedException = System.NotImplementedException;

namespace WalkerGame.System.Systems
{
    [System]
    public class MoveSystem : ISystem
    {
        private readonly EntityRegistry entityRegistry;
        
        [Inject]
        public MoveSystem(EntityRegistry entityRegistry)
        {
            this.entityRegistry = entityRegistry;
        }
        public void Update(GameTime gameTime)
        {
            entityRegistry.Loop((int entity, ref Transform t, ref Velocity v) => { t.pos += v.vel; });
        }

        public void Draw(GameTime gameTime)
        {
            
        }
    }
}