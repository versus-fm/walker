using System.IO;
using Newtonsoft.Json;
using WalkerGame.Graphics;
using WalkerGame.Metadata;

namespace WalkerGame.Resource.Processors
{
    [ResourceProcessor(null, ".font")]
    public class FontCollectionProcessor : ResourceFileStreamProcessor
    {
        private readonly FontRepository fontRepository;

        [Inject]
        public FontCollectionProcessor(FontRepository fontRepository)
        {
            this.fontRepository = fontRepository;
        }
        protected override void Load(FileStream fs, string fileName)
        {
            using var sr = new StreamReader(fs);
            var str = sr.ReadToEnd();
            var font = JsonConvert.DeserializeObject<FontCollectionDefinition>(str);
            fontRepository.RegisterFontCollection(font.fontName, font.fontSize, font.fonts);
        }
    }
}