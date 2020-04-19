using System.IO;
using Newtonsoft.Json;
using WalkerGame.Graphics;
using WalkerGame.Metadata;
using WalkerGame.Resource;

namespace WalkerGame.Resource.Processors
{
    [ResourceProcessor(null, ".spritesheet")]
    public class SpritesheetProcessor : ResourceFileStreamProcessor
    {
        private readonly SpriteRepository spriteRepository;

        [Inject]
        public SpritesheetProcessor(SpriteRepository spriteRepository)
        {
            this.spriteRepository = spriteRepository;
        }
        protected override void Load(FileStream fs, string fileName)
        {
            using var sr = new StreamReader(fs);
            var str = sr.ReadToEnd();
            var spriteSheet = JsonConvert.DeserializeObject<SpritesheetDefinition>(str);
            spriteRepository.AttemptSubdivision(spriteSheet);
        }
    }
}