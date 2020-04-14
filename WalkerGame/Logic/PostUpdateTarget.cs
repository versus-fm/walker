using Microsoft.Xna.Framework;

namespace WalkerGame.Logic
{
    public interface PostUpdateTarget
    {
        void PostUpdate(GameTime gameTime);
    }
}