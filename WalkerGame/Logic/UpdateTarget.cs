using Microsoft.Xna.Framework;

namespace WalkerGame.Logic
{
    public interface UpdateTarget : PartTarget
    {
        void Update(GameTime gameTime);
    }
}