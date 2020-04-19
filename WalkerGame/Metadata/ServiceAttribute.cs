using System;

namespace WalkerGame.Metadata
{
    public class ServiceAttribute : Attribute
    {
        public Type ForType { get; }
        public Type Generic { get; }

        public ServiceAttribute(Type forType = null, Type generic = null)
        {
            ForType = forType;
            Generic = generic;
        }
    }
}