using System;

namespace WalkerGame.Metadata
{
    public class SystemAttribute : Attribute
    {
        public Type RunBefore { get; }
        public SystemAttribute(Type runBefore = null)
        {
            RunBefore = runBefore;
        }
    }
}