namespace BattleShip
{
    using System;

    public static class ConsoleDraw
    {
        public static void Draw(int x, int y, char symbol, ConsoleColor foregroundColor = ConsoleColor.Yellow, ConsoleColor backgroundColor = ConsoleColor.Black)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.Write(symbol);
            Console.ResetColor();
        }
    }
}