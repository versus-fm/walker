using Microsoft.Xna.Framework;

namespace WalkerGame.Logic
{
    public interface DrawTarget : PartTarget
    {
        void Draw(GameTime gameTime);
    }
}