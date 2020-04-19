using System.IO;
using Microsoft.Xna.Framework.Graphics;
using WalkerGame.Graphics;
using WalkerGame.Metadata;
using WalkerGame.Resource;

namespace WalkerGame.Resource.Processors
{
    [ResourceProcessor(typeof(SpritesheetProcessor), ".png", ".bmp")]
    public class Texture2DProcessor : ResourceFileStreamProcessor
    {
        private readonly GraphicsDevice graphicsDevice;
        private readonly SpriteRepository spriteRepository;

        [Inject]
        public Texture2DProcessor(GraphicsDevice graphicsDevice,
            SpriteRepository spriteRepository)
        {
            this.graphicsDevice = graphicsDevice;
            this.spriteRepository = spriteRepository;
        }
        protected override void Load(FileStream fs, string fileName)
        {
            spriteRepository.PreRegister(Texture2D.FromStream(graphicsDevice, fs), fileName);
        }
    }
}