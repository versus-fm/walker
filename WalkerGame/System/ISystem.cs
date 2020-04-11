using Microsoft.Xna.Framework;

namespace WalkerGame.System
{
    public interface ISystem
    {
        void Update(GameTime gameTime);
        void Draw(GameTime gameTime);

    }
}