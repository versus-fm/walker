using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.BitmapFonts;
using Svelto.ECS;
using WalkerGame.Component;
using WalkerGame.Graphics;
using WalkerGame.Logic;
using WalkerGame.Metadata;
using WalkerGame.Reflection;
using WalkerGame.Resource;

namespace WalkerGame
{
    public class Walker : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private ObjectGraph objects;
        private PartManager parts;
        private EnginesRoot enginesRoot;
        private IEntityFactory entityFactory;
        private IEntityFunctions entityFunctions;
        
        
        public Walker()
        {
            objects = new ObjectGraph(Assembly.GetCallingAssembly());
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = @"res";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            IsMouseVisible = true;
            spriteBatch = new SpriteBatch(GraphicsDevice);
            graphics.SynchronizeWithVerticalRetrace = false;
            IsFixedTimeStep = false;

            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.ApplyChanges();
            
            enginesRoot = new EnginesRoot(new SimpleSubmissionEntityViewScheduler());
            entityFactory = enginesRoot.GenerateEntityFactory();
            entityFunctions = enginesRoot.GenerateEntityFunctions();

            objects.RegisterInstance(spriteBatch);
            objects.RegisterInstance(graphics);
            objects.RegisterInstance(GraphicsDevice);
            objects.RegisterInstance(Content);
            objects.RegisterInstance(typeof(IEntityFactory), entityFactory);
            objects.RegisterInstance(typeof(IEntityFunctions), entityFunctions);

            objects.DiscoverServices();

            objects.Verify();
            
            objects.DoOnAttribute<SystemAttribute>((attribute, type) =>
            {
                if (type.Implements<IEngine>())
                {
                    var engine = (IEngine)objects.Get(type);
                    enginesRoot.AddEngine(engine);
                }
            });
            
            parts = objects.Get<PartManager>();
            
            parts.Load();
            var resourceLoader = objects.Get<ResourceLoader>();
            resourceLoader.Process();
            var repository = objects.Get<SpriteRepository>();
            repository.PlaceInternalCache();

            
            
            parts.Ready();
        }

        protected override void UnloadContent()
        {
            objects.Dispose();
        }

        protected override void Update(GameTime gameTime)
        {
            parts.PreUpdate(gameTime);
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            parts.Update(gameTime);
            
            base.Update(gameTime);
            parts.PostUpdate(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            parts.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}