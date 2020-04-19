using System;

namespace WalkerGame.Metadata
{
    public class GamePartAttribute : ServiceAttribute
    {
        public string Name { get; }

        public GamePartAttribute(string name, Type forType = null) : base(forType)
        {
            Name = name;
        }
    }
}