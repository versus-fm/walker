using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using WalkerGame.Component;
using WalkerGame.Graphics;
using WalkerGame.Input;
using WalkerGame.Logic;
using WalkerGame.Metadata;
using WalkerGame.Metadata.Hinting;
using WalkerGame.Reflection;
using NotImplementedException = System.NotImplementedException;

namespace WalkerGame.EntitySystem.Systems
{
    [System("draw_system")]
    public class DrawSystem : DrawTarget, UpdateTarget, ReadyTarget
    {
        private readonly SpriteRepository spriteRepository;
        private readonly SpriteBatch spriteBatch;
        private readonly FontRepository fontRepository;
        private readonly XInputService input;

        private Font font;

        [Inject]
        public DrawSystem(SpriteRepository spriteRepository,
            SpriteBatch spriteBatch,
            FontRepository fontRepository,
            [Hint(typeof(IInputService))] XInputService input)
        {
            this.spriteRepository = spriteRepository;
            this.spriteBatch = spriteBatch;
            this.fontRepository = fontRepository;
            this.input = input;
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, default, SamplerState.PointClamp);
            spriteRepository.DrawSpritesheetIndices(font, new Vector2(100, 100), "sheet", 16, 16, 4);
            spriteBatch.End();
        }

        public void ServiceReady()
        {
            font = fontRepository.GetFont("roboto");
        }
    }
}