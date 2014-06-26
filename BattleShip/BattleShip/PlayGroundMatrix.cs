namespace BattleShip
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PlayGroundMatrix
    {
        private readonly Render rend;
        private readonly Player player;
        private int countOfDestroyedShip = 10;
        private int[,] matrix;
        private int[] arrayWithShipsPoints;
        private List<List<string>> allShips;

        public PlayGroundMatrix(Player player)
        {
            this.matrix = new int[10, 10];
            this.rend = new Render(player);
            this.player = player;
        }

        public int CountOfDestroyedShip
        {
            get { return this.countOfDestroyedShip; }
        }

        public void FillPlaySpace(List<List<string>> coordinates)
        {
            this.allShips = coordinates;
            this.arrayWithShipsPoints = new int[coordinates.Count() + 1];
            for (int i = 0; i < coordinates.Count; i++)
            {
                this.arrayWithShipsPoints[i + 1] = coordinates[i].Count();
                foreach (var item in coordinates[i])
                {
                    var currentSpot = item.Split();
                    int x = int.Parse(currentSpot[0]);
                    int y = int.Parse(currentSpot[1]);
                    this.matrix[x, y] = i + 1;
                }
            }

            if (this.player == Player.Human)
            {
                for (int i = 0; i < 10; i++)
                {
                    for (int k = 0; k < 10; k++)
                    {
                        if (this.matrix[i, k] != 0)
                        {
                            this.rend.Draw(i, k, 'X', ConsoleColor.DarkYellow);
                        }
                    }
                }
            }
        }

        public Dictionary<string, StatementOfPositionInMatrix> ReturnStat(string coordinates)
        {
            Dictionary<string, StatementOfPositionInMatrix> hittedPosition = new Dictionary<string, StatementOfPositionInMatrix>();
            int x = int.Parse(coordinates[0].ToString());
            int y = int.Parse(coordinates[2].ToString());

            if (this.matrix[x, y] != 0)
            {
                if (this.arrayWithShipsPoints[this.matrix[x, y]] > 1)
                {
                    this.arrayWithShipsPoints[this.matrix[x, y]]--;
                    this.matrix[x, y] = 0;
                    this.rend.Draw(x, y, 'O', ConsoleColor.Red);
                    hittedPosition.Add(coordinates, StatementOfPositionInMatrix.Hitted);
                    return hittedPosition;
                }
                else if (this.arrayWithShipsPoints[this.matrix[x, y]] == 1)
                {
                    this.countOfDestroyedShip--;
                    this.matrix[x, y] = 0;
                    this.rend.Draw(x, y, 'X', ConsoleColor.Red);
                    int shipIndex = this.FoundTheShip(x, y);
                    foreach (var coordinate in this.allShips[shipIndex])
                    {
                        hittedPosition.Add(coordinate, StatementOfPositionInMatrix.Destroyed);
                    }

                    this.DrawWhenIsDestroyed(shipIndex);
                    return hittedPosition;
                }

                throw new ArgumentException("Problem with calculating values!");
            }
            else
            {
                this.rend.Draw(x, y, 'o', ConsoleColor.Blue);
                hittedPosition.Add(coordinates, StatementOfPositionInMatrix.Empty);
                return hittedPosition;
            }
        }

        private bool GotShipOnPosition(int x, int y)
        {
            if (this.matrix[x, y] != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private int FoundTheShip(int x, int y)
        {
            for (int i = 0; i < this.allShips.Count; i++)
            {
                foreach (var item in this.allShips[i])
                {
                    var currentSpot = item.Split();
                    int tempX = int.Parse(currentSpot[0]);
                    int tempY = int.Parse(currentSpot[1]);
                    if (x == tempX && y == tempY)
                    {
                        return i;
                    }
                }
            }

            throw new ArgumentException("Invalid Method WORK!!!");
        }

        private void DrawWhenIsDestroyed(int shipIndex)
        {
            HashSet<string> coordinateForChange = new HashSet<string>();
            foreach (var coordinate in this.allShips[shipIndex])
            {
                string[] coordinatesArr = coordinate.Split(' ');
                int coordinateX = int.Parse(coordinatesArr[0]);
                int coordinateY = int.Parse(coordinatesArr[1]);

                // UpLeft -1, -1
                if (MatrixFunctions.IsPositionInTheMatrix(coordinateX - 1, coordinateY - 1))
                {
                    string strCoordinate = (coordinateX - 1).ToString() + " " + (coordinateY - 1).ToString();
                    if (!coordinateForChange.Contains(strCoordinate))
                    {
                        coordinateForChange.Add(strCoordinate);
                        this.rend.Draw(coordinateX - 1, coordinateY - 1, 'o', ConsoleColor.Cyan);
                    }
                }

                // UpMid -1, 0
                if (MatrixFunctions.IsPositionInTheMatrix(coordinateX - 1, coordinateY))
                {
                    string strCoordinate = (coordinateX - 1).ToString() + " " + coordinateY.ToString();
                    if (!coordinateForChange.Contains(strCoordinate))
                    {
                        coordinateForChange.Add(strCoordinate);
                        this.rend.Draw(coordinateX - 1, coordinateY, 'o', ConsoleColor.Cyan);
                    }
                }

                // UpRight -1, +1
                if (MatrixFunctions.IsPositionInTheMatrix(coordinateX - 1, coordinateY + 1))
                {
                    string strCoordinate = (coordinateX - 1).ToString() + " " + (coordinateY + 1).ToString();
                    if (!coordinateForChange.Contains(strCoordinate))
                    {
                        coordinateForChange.Add(strCoordinate);
                        this.rend.Draw(coordinateX - 1, coordinateY + 1, 'o', ConsoleColor.Cyan);
                    }
                }

                // MidLeft 0, -1
                if (MatrixFunctions.IsPositionInTheMatrix(coordinateX, coordinateY - 1))
                {
                    string strCoordinate = coordinateX.ToString() + " " + (coordinateY - 1).ToString();
                    if (!coordinateForChange.Contains(strCoordinate))
                    {
                        coordinateForChange.Add(strCoordinate);
                        this.rend.Draw(coordinateX, coordinateY - 1, 'o', ConsoleColor.Cyan);
                    }
                }

                // MidRight 0, +1
                if (MatrixFunctions.IsPositionInTheMatrix(coordinateX, coordinateY + 1))
                {
                    string strCoordinate = coordinateX.ToString() + " " + (coordinateY + 1).ToString();
                    if (!coordinateForChange.Contains(strCoordinate))
                    {
                        coordinateForChange.Add(strCoordinate);
                        this.rend.Draw(coordinateX, coordinateY + 1, 'o', ConsoleColor.Cyan);
                    }
                }

                // DownLeft +1 , -1
                if (MatrixFunctions.IsPositionInTheMatrix(coordinateX + 1, coordinateY - 1))
                {
                    string strCoordinate = (coordinateX + 1).ToString() + " " + (coordinateY - 1).ToString();
                    if (!coordinateForChange.Contains(strCoordinate))
                    {
                        coordinateForChange.Add(strCoordinate);
                        this.rend.Draw(coordinateX + 1, coordinateY - 1, 'o', ConsoleColor.Cyan);
                    }
                }

                // DownMid +1, 0
                if (MatrixFunctions.IsPositionInTheMatrix(coordinateX + 1, coordinateY))
                {
                    string strCoordinate = (coordinateX + 1).ToString() + " " + coordinateY.ToString();
                    if (!coordinateForChange.Contains(strCoordinate))
                    {
                        coordinateForChange.Add(strCoordinate);
                        this.rend.Draw(coordinateX + 1, coordinateY, 'o', ConsoleColor.Cyan);
                    }
                }

                // DownRight +1, +1
                if (MatrixFunctions.IsPositionInTheMatrix(coordinateX + 1, coordinateY + 1))
                {
                    string strCoordinate = (coordinateX + 1).ToString() + " " + (coordinateY + 1).ToString();
                    if (!coordinateForChange.Contains(strCoordinate))
                    {
                        coordinateForChange.Add(strCoordinate);
                        this.rend.Draw(coordinateX + 1, coordinateY + 1, 'o', ConsoleColor.Cyan);
                    }
                }
            }

            foreach (var coordinate in this.allShips[shipIndex])
            {
                string[] coordinatesArr = coordinate.Split(' ');
                int coordinateX = int.Parse(coordinatesArr[0]);
                int coordinateY = int.Parse(coordinatesArr[1]);
                this.rend.Draw(coordinateX, coordinateY, 'X', ConsoleColor.Red);
            }
        }
    }
}