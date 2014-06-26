namespace BattleShip
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Threading;
    using BattleShip.BattleShipBot;

    public class BattleShipMain
    {
        public static void Main()
        {
            Console.Title = "BattleShip";
            int botFirstScore = 0;
            int botSecondScore = 0;
            while (true)
            {
                Console.Clear();
                Console.SetCursorPosition(5, 25);
                Console.WriteLine("Player - {0} : Bot - {1}", botFirstScore, botSecondScore);
                Console.SetCursorPosition(0, 0);
                DrawPlaygroundField();

                // Bot secondBot = new Bot();
                PlayGroundMatrix firstMatrix = new PlayGroundMatrix(Player.Human);
                PlayGroundMatrix secondMatrix = new PlayGroundMatrix(Player.Bot);
                firstMatrix.FillPlaySpace(ShipsGenerator.GenerateShipsForMatrix());
                secondMatrix.FillPlaySpace(ShipsGenerator.GenerateShipsForMatrix());
                Bot firstBot = new Bot();
                HumanPlayer player = new HumanPlayer();

                while (true)
                {
                    player.GetReportForAttackPosition(secondMatrix.ReturnStat(player.Attack()));
                    string firstAttackPossition = firstBot.Attack();
                    Dictionary<string, StatementOfPositionInMatrix> stateOfAttackedPosition = firstMatrix.ReturnStat(firstAttackPossition);
                    firstBot.GetReportForAttackPosition(stateOfAttackedPosition);

                    Thread.Sleep(100);

                    if (secondMatrix.CountOfDestroyedShip <= 0)
                    {
                        botFirstScore++;
                        break;
                    }
                    else if (firstMatrix.CountOfDestroyedShip <= 0)
                    {
                        botSecondScore++;
                        break;
                    }
                }
            }
        }

        private static void DrawPlaygroundField()
        {
            StreamReader playGround = new StreamReader("../../PlayGroundField.txt", Encoding.UTF8);
            string currentLine = playGround.ReadLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WindowHeight = 30;
            Console.BufferHeight = 30;

            while (currentLine != null)
            {
                Console.WriteLine(currentLine);
                currentLine = playGround.ReadLine();
            }
        }
    }
}
