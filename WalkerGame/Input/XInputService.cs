using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using WalkerGame.Logic;
using WalkerGame.Metadata;

namespace WalkerGame.Input
{
    [GamePart("xinput", typeof(IInputService))]
    public class XInputService : PreUpdateTarget, PostUpdateTarget, IInputService
    {
        private KeyboardState currentKeyState, previousKeyState;
        private MouseState currentMouseState, previousMouseState;
        private Dictionary<int, (GamePadState current, GamePadState previous)> gamePadState;
        private Dictionary<string, Func<XInputService, int, Vector2>> axis;

        public EventHandler<ControllerEventArgs> ControllerConnectedEvent;
        public EventHandler<ControllerEventArgs> ControllerDisconnectedEvent;
        
        
        [Inject]
        public XInputService()
        {
            axis = new Dictionary<string, Func<XInputService, int, Vector2>>();

            currentKeyState = previousKeyState = Keyboard.GetState();
            currentMouseState = previousMouseState = Mouse.GetState();
            
            
            gamePadState = new Dictionary<int, (GamePadState current, GamePadState previous)>();
            
            for (var playerIndex = 0; playerIndex < GamePad.MaximumGamePadCount; playerIndex++)
            {
                gamePadState.Add(playerIndex, (GamePad.GetState(playerIndex), GamePad.GetState(playerIndex)));
            }

        }
        public void PreUpdate(GameTime gameTime)
        {
            currentKeyState = Keyboard.GetState();
            currentMouseState = Mouse.GetState();
            for (var playerIndex = 0; playerIndex < GamePad.MaximumGamePadCount; playerIndex++)
            {
                var (current, previous) = gamePadState[playerIndex];
                if (!current.IsConnected && previous.IsConnected)
                    ControllerDisconnectedEvent?.Invoke(this, new ControllerEventArgs(playerIndex));
                else if (!previous.IsConnected && current.IsConnected)
                    ControllerConnectedEvent?.Invoke(this, new ControllerEventArgs(playerIndex));
                gamePadState[playerIndex] = (GamePad.GetState(playerIndex), previous);
            }
        }

        public void PostUpdate(GameTime gameTime)
        {
            previousKeyState = currentKeyState;
            previousMouseState = currentMouseState;
            for (var playerIndex = 0; playerIndex < GamePad.MaximumGamePadCount; playerIndex++)
            {
                var (current, _) = gamePadState[playerIndex];
                gamePadState[playerIndex] = (current, current);
            }
        }

        public bool KeyDown(Keys key) => currentKeyState.IsKeyDown(key);

        public bool KeyUp(Keys key) => currentKeyState.IsKeyUp(key);

        public bool KeyPressed(Keys key, bool onRelease = true)
        {
            return onRelease
                ? currentKeyState.IsKeyUp(key) && previousKeyState.IsKeyDown(key)
                : previousKeyState.IsKeyUp(key) && currentKeyState.IsKeyDown(key);
        }

        public bool ButtonPressed(Buttons button, int playerIndex = 0, bool onRelease = true)
        {
            if (TryAllPlayerIndex(playerIndex, out var state))
            {
                return onRelease
                    ? state.current.IsButtonUp(button) && state.previous.IsButtonDown(button)
                    : state.previous.IsButtonUp(button) && state.current.IsButtonDown(button);
            }

            return false;
        }

        public bool ButtonDown(Buttons button, int playerIndex = 0)
        {
            return TryPlayerIndex(playerIndex, out var state) && state.IsButtonDown(button);
        }
        
        public bool ButtonUp(Buttons button, int playerIndex = 0)
        {
            return TryPlayerIndex(playerIndex, out var state) && state.IsButtonUp(button);
        }

        public Vector2 CurrentMousePos => currentMouseState.Position.ToVector2();

        public bool MouseClicked(MouseButton button, bool onRelease = true)
        {
            var current = GetCurrentMouseButtonState(button);
            var previous = GetPreviousMouseButtonState(button);

            return onRelease
                ? current == ButtonState.Released && previous == ButtonState.Pressed
                : current == ButtonState.Pressed && previous == ButtonState.Released;
        }

        public bool MouseDown(MouseButton button) => GetCurrentMouseButtonState(button) == ButtonState.Pressed;
        public bool MouseUp(MouseButton button) => GetCurrentMouseButtonState(button) == ButtonState.Released;

        private ButtonState GetCurrentMouseButtonState(MouseButton button)
        {
            return GetMouseButtonState(ref currentMouseState, button);
        }
        
        private ButtonState GetPreviousMouseButtonState(MouseButton button)
        {
            return GetMouseButtonState(ref previousMouseState, button);
        }
        
        private ButtonState GetMouseButtonState(ref MouseState mouseState, MouseButton button)
        {
            return button switch
            {
                MouseButton.Left => mouseState.LeftButton,
                MouseButton.Middle => mouseState.MiddleButton,
                MouseButton.Right => mouseState.RightButton,
                MouseButton.X1 => mouseState.XButton1,
                MouseButton.X2 => mouseState.XButton2,
                _ => ButtonState.Released
            };
        }

        private bool TryPlayerIndex(int playerIndex, out GamePadState state)
        {
            if (playerIndex < GamePad.MaximumGamePadCount && gamePadState.TryGetValue(playerIndex, out var states))
            {
                state = states.current;
                return true;
            }
            state = GamePadState.Default;
            return false;
        }
        
        private bool TryAllPlayerIndex(int playerIndex, out (GamePadState current, GamePadState previous) state)
        {
            if (playerIndex < GamePad.MaximumGamePadCount && gamePadState.TryGetValue(playerIndex, out var states))
            {
                state = states;
                return true;
            }

            state = (GamePadState.Default, GamePadState.Default);
            return false;
        }

        public Vector2 GetAxis(string name, int playerIndex = 0)
        {
            if (playerIndex >= GamePad.MaximumGamePadCount)
                return Vector2.Zero;
            return axis.TryGetValue(name, out var func) ? func(this, playerIndex) : Vector2.Zero;
        }

        public void RegisterAxis(string name, Func<XInputService, int, Vector2> axisAction)
        {
            axis.TryAdd(name, axisAction);
        }

        public void RegisterAxis(string name, Func<GamePadThumbSticks, Vector2> axisAction)
        {
            axis.TryAdd(name, (service, index) => axisAction(service.gamePadState[index].current.ThumbSticks));
        }

        public void RegisterAxis(string name, Keys up, Keys down, Keys left, Keys right)
        {
            axis.TryAdd(name, (service, index) =>
            {
                var a = new Vector2(0);
                if (KeyDown(up)) a.Y -= 1;
                if (KeyDown(down)) a.Y += 1;
                if (KeyDown(left)) a.X -= 1;
                if (KeyDown(right)) a.X += 1;
                return a;
            });
        }
    }
}