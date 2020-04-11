using System;

namespace WalkerGame
{
    class Program
    {
        static void Main(string[] args)
        {
            using var game = new Walker();
            game.Run();
        }
    }
}