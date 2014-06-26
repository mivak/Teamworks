namespace BattleShip
{
    using System;

    public class Render
    {
        private readonly int distanceFromWall;

        public Render(Player player)
        {
            if (player == Player.Human)
            {
                this.distanceFromWall = 42;
            }
            else
            {
                this.distanceFromWall = 2;
            }
        }

        public void Draw(int x, int y, char symbol, ConsoleColor color = ConsoleColor.Red)
        {
            int realXCoordinate = (x * 3) + this.distanceFromWall;
            int realYCoordinate = (y * 2) + 2;
            Console.ForegroundColor = color;
            Console.SetCursorPosition(realXCoordinate, realYCoordinate);
            Console.Write(symbol);
            Console.SetCursorPosition(realXCoordinate + 1, realYCoordinate);
            Console.Write(symbol);
            Console.SetCursorPosition(realXCoordinate + 1, realYCoordinate + 1);
            Console.Write(symbol);
            Console.SetCursorPosition(realXCoordinate, realYCoordinate + 1);
            Console.Write(symbol);
            Console.SetCursorPosition(x * 3 + this.distanceFromWall, y * 2 + 2);
        }
    }
}