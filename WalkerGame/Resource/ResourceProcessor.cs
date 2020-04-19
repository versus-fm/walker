using System.IO;

namespace WalkerGame.Resource
{
    public interface IResourceProcessor
    {
        void Load(string path, string fileName);
    }
}