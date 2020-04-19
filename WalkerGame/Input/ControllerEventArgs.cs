using System;

namespace WalkerGame.Input
{
    public class ControllerEventArgs : EventArgs
    {
        public int PlayerIndex { get; }

        public ControllerEventArgs(int playerIndex)
        {
            PlayerIndex = playerIndex;
        }
    }
}