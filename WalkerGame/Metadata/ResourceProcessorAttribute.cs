using System;

namespace WalkerGame.Metadata
{
    public class ResourceProcessorAttribute : Attribute
    {
        public string[] FileTypes { get; }

        public ResourceProcessorAttribute(params string[] fileTypes)
        {
            FileTypes = fileTypes;
        }
    }
}