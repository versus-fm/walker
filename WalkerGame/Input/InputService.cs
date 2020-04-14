using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using WalkerGame.Logic;
using WalkerGame.Metadata;

namespace WalkerGame.Input
{
    [GamePart("input")]
    public class InputService : PreUpdateTarget, PostUpdateTarget
    {
        private KeyboardState currentKeyState, previousKeyState;
        private MouseState currentMouseState, previousMouseState;
        
        
        [Inject]
        public InputService()
        {
            currentKeyState = previousKeyState = Keyboard.GetState();
            currentMouseState = previousMouseState = Mouse.GetState();
        }
        public void PreUpdate(GameTime gameTime)
        {
            currentKeyState = Keyboard.GetState();
            currentMouseState = Mouse.GetState();
        }

        public void PostUpdate(GameTime gameTime)
        {
            previousKeyState = currentKeyState;
            previousMouseState = currentMouseState;
        }
    }
}