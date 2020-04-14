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
using WalkerGame.Component;
using WalkerGame.Graphics;
using WalkerGame.Logic;
using WalkerGame.Metadata;
using WalkerGame.Reflection;

namespace WalkerGame
{
    public class Walker : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private EntityRegistry entities;
        private ObjectGraph objects;
        private PartManager parts;

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
            entities = new EntityRegistry();
            graphics.SynchronizeWithVerticalRetrace = false;
            IsFixedTimeStep = false;

            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.ApplyChanges();
            objects.DoOnAttribute<ComponentAttribute>((attribute, type) => entities.InvokeGenericMethod("RegisterComponent", new [] { type }, attribute.BufferType, 1024));
            
            objects.RegisterInstance(spriteBatch);
            objects.RegisterInstance(graphics);
            objects.RegisterInstance(GraphicsDevice);
            objects.RegisterInstance(Content);
            objects.RegisterInstance(entities);
            
            objects.DiscoverServices();
            
            objects.Verify();

            parts = objects.Get<PartManager>();
            
            parts.Load();
            var resourceLoader = objects.Get<ResourceLoader>();
            resourceLoader.Process();
            var repository = objects.Get<SpriteRepository>();
            repository.PlaceInternalCache();


            for (int x = 0; x < 20; x++)
            {
                for (int y = 0; y < 20; y++)
                {
                    var ent = entities.CreateEntity();
                    entities.AddComponent(ent, new Transform
                    {
                        pos = new Vector2(100 + x*32, 100 + y*32),
                        rotation = 0f,
                        size = new Vector2(32)
                    });
                    entities.AddComponent(ent, repository.CreateSprite("stone"));
                }
            }

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