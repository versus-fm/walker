using Microsoft.Xna.Framework;

namespace WalkerGame.Logic
{
    public interface PreUpdateTarget : PartTarget
    {
        void PreUpdate(GameTime gameTime);
    }
}