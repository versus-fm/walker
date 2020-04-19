using System.IO;

namespace WalkerGame.Resource
{
    public abstract class ResourceFileStreamProcessor : IResourceProcessor
    {
        public void Load(string path, string fileName)
        {
            using var fs = new FileStream(path, FileMode.Open);
            Load(fs, fileName);
        }

        protected abstract void Load(FileStream fs, string fileName);
    }
}