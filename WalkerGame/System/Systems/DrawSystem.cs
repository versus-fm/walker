using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WalkerGame.Component;
using WalkerGame.Graphics;
using WalkerGame.Metadata;
using NotImplementedException = System.NotImplementedException;

namespace WalkerGame.System.Systems
{
    [System]
    public class DrawSystem : ISystem
    {
        private readonly EntityRegistry entityRegistry;
        private readonly SpriteRepository spriteRepository;
        private readonly SpriteBatch spriteBatch;

        [Inject]
        public DrawSystem(EntityRegistry entityRegistry,
            SpriteRepository spriteRepository,
            SpriteBatch spriteBatch)
        {
            this.entityRegistry = entityRegistry;
            this.spriteRepository = spriteRepository;
            this.spriteBatch = spriteBatch;
        }

        public void Update(GameTime gameTime)
        {
            
        }

        public void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);
            entityRegistry.Loop((int entity, ref Sprite sprite, ref Transform transform) =>
            {
                spriteRepository.Draw(ref sprite, ref transform.pos, ref transform.size, Color.White, transform.rotation);
            });
            spriteBatch.End();
        }
    }
}