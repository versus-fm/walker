using System.IO;
using Microsoft.Xna.Framework.Graphics;
using WalkerGame.Graphics;
using WalkerGame.Metadata;

namespace WalkerGame.Processors
{
    [ResourceProcessor(".png", ".bmp")]
    public class Texture2DProcessor : IResourceProcessor
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
        public void Load(FileStream fs, string fileName)
        {
            spriteRepository.PreRegister(Texture2D.FromStream(graphicsDevice, fs), fileName);
        }
    }
}