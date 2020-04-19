using WalkerGame.Graphics;
using WalkerGame.Metadata;
using WalkerGame.Resource;

namespace WalkerGame.Resource.Processors
{
    [ResourceProcessor(typeof(FontCollectionProcessor), ".otf", ".ttf")]
    public class FontProcessor : IResourceProcessor
    {
        private readonly FontRepository fontRepository;

        [Inject]
        public FontProcessor(FontRepository fontRepository)
        {
            this.fontRepository = fontRepository;
        }
        public void Load(string path, string fileName)
        {
            fontRepository.Load(path, fileName);
        }
    }
}