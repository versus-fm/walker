namespace WalkerGame.Input
{
    public interface InputAxis
    {
        public float Horizontal(int playerIndex = 0);
        public float Vertical(int playerIndex = 0);
    }
}