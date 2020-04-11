using System.IO;

namespace WalkerGame
{
    public interface IResourceProcessor
    {
        void Load(FileStream fs, string fileName);
    }
}