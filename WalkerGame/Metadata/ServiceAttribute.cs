using System;

namespace WalkerGame.Metadata
{
    public class ServiceAttribute : Attribute
    {
        public Type ForType { get; }

        public ServiceAttribute(Type forType = null)
        {
            ForType = forType;
        }
    }
}