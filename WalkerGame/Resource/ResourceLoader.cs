using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using WalkerGame.Graphics;
using WalkerGame.Metadata;

namespace WalkerGame
{
    [Service]
    public class ResourceLoader : PostConstruct
    {
        private readonly ObjectGraph graph;
        private List<string> files;
        private Dictionary<string, IResourceProcessor> processors;

        [Inject]
        public ResourceLoader(ObjectGraph graph)
        {
            this.graph = graph;
            files = new List<string>();
            processors = new Dictionary<string, IResourceProcessor>();
        }

        public void Include(string path, bool recursive)
        {
            var files = Directory.GetFiles(path, "*.*",
                recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
            this.files.AddRange(files);
        }

        public void Process()
        {
            foreach (var file in files)
            {
                var extension = Path.GetExtension(file);
                if (processors.TryGetValue(extension, out var processor))
                {
                    using (var fs = new FileStream(file, FileMode.Open))
                    {
                        processor.Load(fs, Path.GetFileNameWithoutExtension(file));
                    }
                }
            }
        }

        public void Post()
        {
            graph.DoOnAttribute<ResourceProcessorAttribute>((attribute, type) =>
            {
                for (var i = 0; i < attribute.FileTypes.Length; i++)
                {
                    var extension = attribute.FileTypes[i];
                    processors.Add(extension, (IResourceProcessor)graph.Construct(type));
                }
            });
        }
    }
}