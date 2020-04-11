using System;

namespace WalkerGame.Metadata
{
    public class GamePartAttribute : Attribute
    {
        public string Name { get; }

        public GamePartAttribute(string name)
        {
            Name = name;
        }
    }
}