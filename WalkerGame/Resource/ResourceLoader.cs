using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using WalkerGame.Graphics;
using WalkerGame.Metadata;
using WalkerGame.Reflection;

namespace WalkerGame.Resource
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
            var collected = new List<(IResourceProcessor, string)>();
            foreach (var processorPair in processors)
            {
                var processor = processorPair.Value;
                var filetype = processorPair.Key;
                if (processor.GetType().TryAttribute<ResourceProcessorAttribute>(out var attribute))
                {
                    if (attribute.RunBefore != null)
                    {
                        var added = false;
                        for (var i = 0; i < collected.Count; i++)
                        {
                            if (collected[i].Item1.GetType() != attribute.RunBefore) continue;
                            collected.Insert(i, (processor, filetype));
                            added = true;
                            break;
                        }
                        if (!added)
                            collected.Add((processor, filetype));
                    }
                    else
                    {
                        collected.Add((processor, filetype));
                    }
                }
            }

            var sortedFiles = collected.SelectMany((tuple, i) => files.Where(file =>
                tuple.Item2.Equals(Path.GetExtension(file), StringComparison.CurrentCultureIgnoreCase))).Distinct().ToList();
            
            foreach (var file in sortedFiles)
            {
                var extension = Path.GetExtension(file);
                if (processors.TryGetValue(extension, out var processor))
                {
                    processor.Load(file, Path.GetFileNameWithoutExtension(file));
                }
            }
        }

        public void Post()
        {
            graph.DoOnAttribute<ResourceProcessorAttribute>((attribute, type) =>
            {
                var processor = (IResourceProcessor)graph.Construct(type);
                Console.WriteLine(processor.GetType().Name);
                for (var i = 0; i < attribute.FileTypes.Length; i++)
                {
                    var extension = attribute.FileTypes[i];
                    processors.Add(extension, processor);
                }
            });
        }
    }
}