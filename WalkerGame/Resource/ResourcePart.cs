using WalkerGame.Logic;
using WalkerGame.Metadata;
using NotImplementedException = System.NotImplementedException;

namespace WalkerGame.Resource
{
    [GamePart("content")]
    public class ResourcePart : ContentTarget
    {
        private readonly ResourceLoader resourceLoader;

        [Inject]
        public ResourcePart(ResourceLoader resourceLoader)
        {
            this.resourceLoader = resourceLoader;
        }

        public void Load()
        {
            resourceLoader.Include("res", true);
        }
    }
}