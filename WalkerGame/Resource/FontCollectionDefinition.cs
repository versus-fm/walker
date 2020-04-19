using System.Collections.Generic;

namespace WalkerGame.Resource
{
    public class FontCollectionDefinition
    {
        public string fontName { get; set; }
        public List<string> fonts { get; set; }
        public float fontSize { get; set; }

        public FontCollectionDefinition()
        {
            fonts = new List<string>();
            fontName = "";
        }
        
    }
}