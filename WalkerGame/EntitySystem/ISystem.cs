using Microsoft.Xna.Framework;

namespace WalkerGame.EntitySystem
{
    public interface ISystem
    {
        void Update(GameTime gameTime);
        void Draw(GameTime gameTime);

    }
}