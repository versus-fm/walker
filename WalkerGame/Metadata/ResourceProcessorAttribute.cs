using System;

namespace WalkerGame.Metadata
{
    public class ResourceProcessorAttribute : Attribute
    {
        public string[] FileTypes { get; }
        public Type RunBefore { get; }

        public ResourceProcessorAttribute(Type runBefore = null, params string[] fileTypes)
        {
            FileTypes = fileTypes;
            RunBefore = runBefore;
        }
    }
}