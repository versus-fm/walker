using System;

namespace WalkerGame.Metadata
{
    [AttributeUsage(AttributeTargets.Struct)]
    public class ComponentAttribute : Attribute
    {
        public BufferType BufferType { get; }

        public ComponentAttribute(BufferType bufferType)
        {
            BufferType = bufferType;
        }
    }
}