namespace BattleShip
{
    using System;
    using System.Collections.Generic;

    public class HumanPlayer : IAttacker
    {
        private readonly Playground playground;
        private int coordinateX;
        private int coordinateY;
        private bool booleanForWhile;
        private string lastAttackPosition;

        public HumanPlayer()
        {
            this.coordinateX = 0;
            this.coordinateY = 0;
            this.playground = new Playground();
            this.ChangeNextPositionColor(this.coordinateX, this.coordinateY);
        }

        public void GetReportForAttackPosition(Dictionary<string, StatementOfPositionInMatrix> returnStat)
        {
            foreach (var coordinate in returnStat)
            {
                if (returnStat[coordinate.Key] == StatementOfPositionInMatrix.Empty)
                {
                    this.playground.ChangeMatrixStatment(new string[] { coordinate.Key }, StatementOfPositionInMatrix.Empty);
                }
                else if (returnStat[coordinate.Key] == StatementOfPositionInMatrix.Hitted)
                {
                    this.playground.ChangeMatrixStatment(new string[] { coordinate.Key }, StatementOfPositionInMatrix.Hitted);
                }
                else if (returnStat[coordinate.Key] == StatementOfPositionInMatrix.Destroyed)
                {
                    List<string> hittedPosition = new List<string>();
                    foreach (var destroyedCoordinate in returnStat)
                    {
                        hittedPosition.Add(destroyedCoordinate.Key);
                    }

                    this.playground.ChangeMatrixStatment(hittedPosition.ToArray(), StatementOfPositionInMatrix.Destroyed);
                    return;
                }
            }
        }

        public string Attack()
        {
            this.FirstPosition();
            Console.SetCursorPosition(0, 0);
            this.booleanForWhile = true;
            while (this.booleanForWhile)
            {
                ConsoleKeyInfo playerKey = Console.ReadKey();
                this.InvalidKey("            ");
                switch (playerKey.Key)
                {
                    case ConsoleKey.UpArrow:
                        this.PressedUpArrow();
                        break;
                    case ConsoleKey.DownArrow:
                        this.PressedDownArrow();
                        break;
                    case ConsoleKey.LeftArrow:
                        this.PressedLeftArrow();
                        break;
                    case ConsoleKey.RightArrow:
                        this.PressedRightArrow();
                        break;
                    case ConsoleKey.Spacebar:
                        this.PressedSpacebar();
                        break;
                    default: this.InvalidKey("Invalid Key!");
                        break;
                }
            }

            string coordinate = this.coordinateX.ToString() + " " + this.coordinateY.ToString();
            this.lastAttackPosition = coordinate;
            return coordinate;
        }

        private void InvalidKey(string message)
        {
            Console.SetCursorPosition(5, 28);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ResetColor();
            Console.SetCursorPosition(0, 0);
        }

        private void FirstPosition()
        {
            StatementOfPositionInMatrix[,] matrix = this.playground.GetMatrixWithElements();
            for (int x = this.coordinateX; x < matrix.GetLength(0); x++)
            {
                for (int y = this.coordinateY; y < matrix.GetLength(1); y++)
                {
                    if (matrix[x, y] == StatementOfPositionInMatrix.NotChecked)
                    {
                        this.MakeDefaultPosistionColor(this.coordinateX, this.coordinateY);
                        this.ChangeNextPositionColor(x, y);
                        this.coordinateX = x;
                        this.coordinateY = y;
                        return;
                    }
                }
            }

            for (int x = this.coordinateX; x >= 0; x--)
            {
                for (int y = this.coordinateY; y >= 0; y--)
                {
                    if (matrix[x, y] == StatementOfPositionInMatrix.NotChecked)
                    {
                        this.MakeDefaultPosistionColor(this.coordinateX, this.coordinateY);
                        this.ChangeNextPositionColor(x, y);
                        this.coordinateX = x;
                        this.coordinateY = y;
                        return;
                    }
                }
            }
        }

        private void PressedLeftArrow()
        {
            if (MatrixFunctions.IsPositionInTheMatrix(this.coordinateX - 1, this.coordinateY))
            {
                StatementOfPositionInMatrix[,] matrix = this.playground.GetMatrixWithElements();
                for (int y = this.coordinateY; y < matrix.GetLength(1); y++)
                {
                    for (int x = this.coordinateX - 1; x >= 0; x--)
                    {
                        if (matrix[x, y] == StatementOfPositionInMatrix.NotChecked)
                        {
                            this.MakeDefaultPosistionColor(this.coordinateX, this.coordinateY);
                            this.ChangeNextPositionColor(x, y);
                            this.coordinateX = x;
                            this.coordinateY = y;
                            return;
                        }
                    }
                }

                for (int y = this.coordinateY; y >= 0; y--)
                {
                    for (int x = this.coordinateX - 1; x >= 0; x--)
                    {
                        if (matrix[x, y] == StatementOfPositionInMatrix.NotChecked)
                        {
                            this.MakeDefaultPosistionColor(this.coordinateX, this.coordinateY);
                            this.ChangeNextPositionColor(x, y);
                            this.coordinateX = x;
                            this.coordinateY = y;
                            return;
                        }
                    }
                }
            }
        }

        private void PressedDownArrow()
        {
            if (MatrixFunctions.IsPositionInTheMatrix(this.coordinateX, this.coordinateY + 1))
            {
                StatementOfPositionInMatrix[,] matrix = this.playground.GetMatrixWithElements();
                for (int x = this.coordinateX; x < matrix.GetLength(0); x++)
                {
                    for (int y = this.coordinateY + 1; y < matrix.GetLength(1); y++)
                    {
                        if (matrix[x, y] == StatementOfPositionInMatrix.NotChecked)
                        {
                            this.MakeDefaultPosistionColor(this.coordinateX, this.coordinateY);
                            this.ChangeNextPositionColor(x, y);
                            this.coordinateX = x;
                            this.coordinateY = y;
                            return;
                        }
                    }
                }

                for (int x = this.coordinateX; x >= 0; x--)
                {
                    for (int y = this.coordinateY + 1; y < matrix.GetLength(1); y++)
                    {
                        if (matrix[x, y] == StatementOfPositionInMatrix.NotChecked)
                        {
                            this.MakeDefaultPosistionColor(this.coordinateX, this.coordinateY);
                            this.ChangeNextPositionColor(x, y);
                            this.coordinateX = x;
                            this.coordinateY = y;
                            return;
                        }
                    }
                }
            }
        }

        private void PressedUpArrow()
        {
            if (MatrixFunctions.IsPositionInTheMatrix(this.coordinateX, this.coordinateY - 1))
            {
                StatementOfPositionInMatrix[,] matrix = this.playground.GetMatrixWithElements();
                for (int x = this.coordinateX; x < matrix.GetLength(0); x++)
                {
                    for (int y = this.coordinateY - 1; y >= 0; y--)
                    {
                        if (matrix[x, y] == StatementOfPositionInMatrix.NotChecked)
                        {
                            this.MakeDefaultPosistionColor(this.coordinateX, this.coordinateY);
                            this.ChangeNextPositionColor(x, y);
                            this.coordinateX = x;
                            this.coordinateY = y;
                            return;
                        }
                    }
                }

                for (int x = this.coordinateX; x >= 0; x--)
                {
                    for (int y = this.coordinateY - 1; y >= 0; y--)
                    {
                        if (matrix[x, y] == StatementOfPositionInMatrix.NotChecked)
                        {
                            this.MakeDefaultPosistionColor(this.coordinateX, this.coordinateY);
                            this.ChangeNextPositionColor(x, y);
                            this.coordinateX = x;
                            this.coordinateY = y;
                            return;
                        }
                    }
                }
            }
        }

        private void PressedRightArrow()
        {
            if (MatrixFunctions.IsPositionInTheMatrix(this.coordinateX + 1, this.coordinateY))
            {
                StatementOfPositionInMatrix[,] matrix = this.playground.GetMatrixWithElements();
                for (int y = this.coordinateY; y < matrix.GetLength(1); y++)
                {
                    for (int x = this.coordinateX + 1; x < matrix.GetLength(0); x++)
                    {
                        if (matrix[x, y] == StatementOfPositionInMatrix.NotChecked)
                        {
                            this.MakeDefaultPosistionColor(this.coordinateX, this.coordinateY);
                            this.ChangeNextPositionColor(x, y);
                            this.coordinateX = x;
                            this.coordinateY = y;
                            return;
                        }
                    }
                }

                for (int y = this.coordinateY; y >= 0; y--)
                {
                    for (int x = this.coordinateX + 1; x < matrix.GetLength(0); x++)
                    {
                        if (matrix[x, y] == StatementOfPositionInMatrix.NotChecked)
                        {
                            this.MakeDefaultPosistionColor(this.coordinateX, this.coordinateY);
                            this.ChangeNextPositionColor(x, y);
                            this.coordinateX = x;
                            this.coordinateY = y;
                            return;
                        }
                    }
                }
            }
        }

        private void PressedSpacebar()
        {
            string possiton = this.coordinateX.ToString() + " " + this.coordinateY.ToString();
            if (!this.playground.IsPositionVisited(possiton))
            {
                this.booleanForWhile = false;
            }
        }

        private void ChangeNextPositionColor(int x, int y)
        {
            string possiton = x.ToString() + " " + y.ToString();
            if (!this.playground.IsPositionVisited(possiton))
            {
                x = (x * 3) + 2;
                y = (y * 2) + 2;
                ConsoleDraw.Draw(x, y, '╔', ConsoleColor.Yellow, ConsoleColor.Blue);
                ConsoleDraw.Draw(x + 1, y, '╗', ConsoleColor.Yellow, ConsoleColor.Blue);
                ConsoleDraw.Draw(x, y + 1, '╚', ConsoleColor.Yellow, ConsoleColor.Blue);
                ConsoleDraw.Draw(x + 1, y + 1, '╝', ConsoleColor.Yellow, ConsoleColor.Blue);
                Console.SetCursorPosition(0, 0);
            }
        }

        private void MakeDefaultPosistionColor(int x, int y)
        {
            string possiton = x.ToString() + " " + y.ToString();
            if (!this.playground.IsPositionVisited(possiton))
            {
                x = (x * 3) + 2;
                y = (y * 2) + 2;
                ConsoleDraw.Draw(x, y, '╔', ConsoleColor.Yellow, ConsoleColor.Black);
                ConsoleDraw.Draw(x + 1, y, '╗', ConsoleColor.Yellow, ConsoleColor.Black);
                ConsoleDraw.Draw(x, y + 1, '╚', ConsoleColor.Yellow, ConsoleColor.Black);
                ConsoleDraw.Draw(x + 1, y + 1, '╝', ConsoleColor.Yellow, ConsoleColor.Black);
                Console.SetCursorPosition(0, 0);
            }
        }
    }
}