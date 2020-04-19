using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace WalkerGame.Input
{
    public interface IInputService
    {
        bool KeyDown(Keys key);
        bool KeyUp(Keys key);
        bool KeyPressed(Keys key, bool onRelease = true);
        bool ButtonPressed(Buttons button, int playerIndex = 0, bool onRelease = true);
        bool ButtonDown(Buttons button, int playerIndex = 0);
        bool ButtonUp(Buttons button, int playerIndex = 0);
        Vector2 CurrentMousePos { get; }
        bool MouseClicked(MouseButton button, bool onRelease = true);
        bool MouseDown(MouseButton button);
        bool MouseUp(MouseButton button);
        Vector2 GetAxis(string name, int playerIndex = 0);
    }
}