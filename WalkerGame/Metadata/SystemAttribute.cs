using System;

namespace WalkerGame.Metadata
{
    public class SystemAttribute : GamePartAttribute
    {
        public Type RunBefore { get; }
        public SystemAttribute(string name, Type runBefore = null) : base(name)
        {
            RunBefore = runBefore;
        }
    }
}