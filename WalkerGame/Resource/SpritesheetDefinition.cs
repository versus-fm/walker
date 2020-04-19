using System.Collections.Generic;

namespace WalkerGame.Resource
{
    public class Animation
    {
        public string name { get; set; }
        public int start { get; set; }
        public int end { get; set; }
    }

    public class SpritesheetDefinition
    {
        public string spriteName { get; set; }
        public int spriteWidth { get; set; }
        public int spriteHeight { get; set; }
        public Dictionary<string, int> names { get; set; }
        public List<Animation> animation { get; set; }

        public SpritesheetDefinition()
        {
            names = new Dictionary<string, int>();
            animation = new List<Animation>();
        }
    }
}