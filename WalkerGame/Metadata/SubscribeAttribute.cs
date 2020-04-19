using System;

namespace WalkerGame.Metadata
{
    public class SubscribeAttribute : Attribute
    {
        public string MessageName { get; }

        public SubscribeAttribute(string messageName)
        {
            MessageName = messageName;
        }
    }
}