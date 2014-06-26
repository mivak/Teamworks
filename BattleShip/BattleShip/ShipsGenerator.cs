namespace BattleShip
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    public static class ShipsGenerator
    {
        private static bool[,] matrix;
        private static List<int> matrixElements;

        public static List<List<string>> GenerateShipsForMatrix()
        {
            Thread.Sleep(50);
            matrix = new bool[10, 10];
            matrixElements = new List<int>();
            List<List<string>> allShips = new List<List<string>>();
            for (int i = 0; i < 100; i++)
            {
                matrixElements.Add(i);
            }

            Thread.Sleep(5);
            allShips.Add(GenerateFourSquareShip());

            Thread.Sleep(10);
            allShips.Add(GenerateThreeSquareShip());
            Thread.Sleep(10);
            allShips.Add(GenerateThreeSquareShip());

            Thread.Sleep(10);
            allShips.Add(GenerateTwoSquareShip());
            Thread.Sleep(10);
            allShips.Add(GenerateTwoSquareShip());
            Thread.Sleep(10);
            allShips.Add(GenerateTwoSquareShip());

            allShips.Add(GenerateOneSquareShip());
            allShips.Add(GenerateOneSquareShip());
            allShips.Add(GenerateOneSquareShip());
            allShips.Add(GenerateOneSquareShip());

            return allShips;
        }

        private static List<string> GenerateFourSquareShip()
        {
            List<string> ships = new List<string>();
            Random rand = new Random();
            ShipPosition shipPosition = GenerateShipPosition();

            int coordinateX = rand.Next(2, 9);
            int coordinateY = rand.Next(2, 9);

            string firstCoordinate = coordinateX.ToString() + " " + coordinateY.ToString();
            if (shipPosition == ShipPosition.Vertical)
            {
                string secondCoordinate = (coordinateX - 1).ToString() + " " + coordinateY.ToString();
                string thirdCoordinate = (coordinateX - 2).ToString() + " " + coordinateY.ToString();
                string forthCoordinate = (coordinateX + 1).ToString() + " " + coordinateY.ToString();

                ships.Add(firstCoordinate);
                ships.Add(secondCoordinate);
                ships.Add(thirdCoordinate);
                ships.Add(forthCoordinate);

                ChangeDestroyedPossitionAndTheNeighborhood(firstCoordinate);
                ChangeDestroyedPossitionAndTheNeighborhood(secondCoordinate);
                ChangeDestroyedPossitionAndTheNeighborhood(thirdCoordinate);
                ChangeDestroyedPossitionAndTheNeighborhood(forthCoordinate);
            }
            else
            {
                string secondCoordinate = coordinateX.ToString() + " " + (coordinateY - 1).ToString();
                string thirdCoordinate = coordinateX.ToString() + " " + (coordinateY - 2).ToString();
                string forthCoordinate = coordinateX.ToString() + " " + (coordinateY + 1).ToString();

                ships.Add(firstCoordinate);
                ships.Add(secondCoordinate);
                ships.Add(thirdCoordinate);
                ships.Add(forthCoordinate);

                ChangeDestroyedPossitionAndTheNeighborhood(firstCoordinate);
                ChangeDestroyedPossitionAndTheNeighborhood(secondCoordinate);
                ChangeDestroyedPossitionAndTheNeighborhood(thirdCoordinate);
                ChangeDestroyedPossitionAndTheNeighborhood(forthCoordinate);
            }

            return ships;
        }

        private static ShipPosition GenerateShipPosition()
        {
            Random rand = new Random();
            ShipPosition shipPosition;
            int position = rand.Next(0, 2);

            if (position == 0)
            {
                shipPosition = ShipPosition.Horizontal;
            }
            else
            {
                shipPosition = ShipPosition.Vertical;
            }

            return shipPosition;
        }

        private static List<string> GenerateThreeSquareShip()
        {
            List<string> ships = new List<string>();
            List<string> positionForHorizontal = new List<string>();
            List<string> positionForVertical = new List<string>();

            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 1; col < matrix.GetLength(1) - 1; col++)
                {
                    if (!matrix[row, col] && !matrix[row, col + 1] && !matrix[row, col - 1])
                    {
                        positionForHorizontal.Add(row.ToString() + " " + col.ToString());
                    }
                }
            }

            for (int col = 0; col < matrix.GetLength(1); col++)
            {
                for (int row = 1; row < matrix.GetLength(0) - 1; row++)
                {
                    if (!matrix[row, col] && !matrix[row + 1, col] && !matrix[row - 1, col])
                    {
                        positionForVertical.Add(row.ToString() + " " + col.ToString());
                    }
                }
            }

            ShipPosition shipPosition = GenerateShipPosition();
            Random rand = new Random();

            if (shipPosition == ShipPosition.Horizontal)
            {
                int index = rand.Next(positionForHorizontal.Count);
                string[] coordinate = positionForHorizontal[index].Split();
                int coordinateX = int.Parse(coordinate[0]);
                int coordinateY = int.Parse(coordinate[1]);

                string secondCoordinate = coordinateX.ToString() + " " + (coordinateY - 1).ToString();
                string thirdCoordinate = coordinateX.ToString() + " " + (coordinateY + 1).ToString();

                ships.Add(positionForHorizontal[index]);
                ships.Add(secondCoordinate);
                ships.Add(thirdCoordinate);

                ChangeDestroyedPossitionAndTheNeighborhood(positionForHorizontal[index]);
                ChangeDestroyedPossitionAndTheNeighborhood(secondCoordinate);
                ChangeDestroyedPossitionAndTheNeighborhood(thirdCoordinate);
            }
            else
            {
                int index = rand.Next(positionForVertical.Count);
                string[] coordinate = positionForVertical[index].Split();
                int coordinateX = int.Parse(coordinate[0]);
                int coordinateY = int.Parse(coordinate[1]);

                string secondCoordinate = (coordinateX - 1).ToString() + " " + coordinateY.ToString();
                string thirdCoordinate = (coordinateX + 1).ToString() + " " + coordinateY.ToString();

                ships.Add(positionForVertical[index]);
                ships.Add(secondCoordinate);
                ships.Add(thirdCoordinate);

                ChangeDestroyedPossitionAndTheNeighborhood(positionForVertical[index]);
                ChangeDestroyedPossitionAndTheNeighborhood(secondCoordinate);
                ChangeDestroyedPossitionAndTheNeighborhood(thirdCoordinate);
            }

            return ships;
        }

        private static List<string> GenerateTwoSquareShip()
        {
            List<string> ships = new List<string>();
            List<string> positionForHorizontal = new List<string>();
            List<string> positionForVertical = new List<string>();

            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1) - 1; col++)
                {
                    if (!matrix[row, col] && !matrix[row, col + 1])
                    {
                        positionForHorizontal.Add(row.ToString() + " " + col.ToString());
                    }
                }
            }

            for (int col = 0; col < matrix.GetLength(0); col++)
            {
                for (int row = 0; row < matrix.GetLength(1) - 1; row++)
                {
                    if (!matrix[row, col] && !matrix[row + 1, col])
                    {
                        positionForVertical.Add(row.ToString() + " " + col.ToString());
                    }
                }
            }

            ShipPosition shipPosition = GenerateShipPosition();
            Random rand = new Random();

            if (shipPosition == ShipPosition.Horizontal)
            {
                int index = rand.Next(positionForHorizontal.Count);
                string[] coordinate = positionForHorizontal[index].Split();
                int coordinateX = int.Parse(coordinate[0]);
                int coordinateY = int.Parse(coordinate[1]);

                string secondCoordinate = coordinateX.ToString() + " " + (coordinateY + 1).ToString();

                ships.Add(positionForHorizontal[index]);
                ships.Add(secondCoordinate);

                ChangeDestroyedPossitionAndTheNeighborhood(positionForHorizontal[index]);
                ChangeDestroyedPossitionAndTheNeighborhood(secondCoordinate);
            }
            else
            {
                int index = rand.Next(positionForVertical.Count);
                string[] coordinate = positionForVertical[index].Split();
                int coordinateX = int.Parse(coordinate[0]);
                int coordinateY = int.Parse(coordinate[1]);

                string secondCoordinate = (coordinateX + 1).ToString() + " " + coordinateY.ToString();

                ships.Add(positionForVertical[index]);
                ships.Add(secondCoordinate);

                ChangeDestroyedPossitionAndTheNeighborhood(positionForVertical[index]);
                ChangeDestroyedPossitionAndTheNeighborhood(secondCoordinate);
            }

            return ships;
        }

        private static List<string> GenerateOneSquareShip()
        {
            List<string> ships = new List<string>();
            Random rand = new Random();
            int index = rand.Next(matrixElements.Count);
            int coordinates = matrixElements[index];
            int col = coordinates % 10;
            int row = coordinates / 10;
            string coordinatesStr = row + " " + col;

            ships.Add(coordinatesStr);
            ChangeDestroyedPossitionAndTheNeighborhood(coordinatesStr);

            return ships;
        }
       
        private static void RemoveUsedElementsFromList(int coordinateX, int coordinateY)
        {
            string numberAsStr = coordinateX.ToString() + coordinateY.ToString();
            int coordinateNumber = int.Parse(numberAsStr);
            int position = matrixElements.BinarySearch(coordinateNumber);
            if (position < 0)
            {
                return;
            }

            matrixElements.RemoveAt(position);
        }

        private static int GetRandomPosition()
        {
            Random rand = new Random();
            string coordinatesStr = string.Empty;

            int posstion = rand.Next(matrixElements.Count);
            int coordinates = matrixElements[posstion];

            return coordinates;
        }

        private static void ChangeDestroyedPossitionAndTheNeighborhood(string coordinate)
        {
            string[] coordinatesArr = coordinate.Split(' ');
            int coordinateX = int.Parse(coordinatesArr[0]);
            int coordinateY = int.Parse(coordinatesArr[1]);
            matrix[coordinateX, coordinateY] = true;
            RemoveUsedElementsFromList(coordinateX, coordinateY);

            // UpLeft -1, -1
            if (MatrixFunctions.IsPositionInTheMatrix(coordinateX - 1, coordinateY - 1))
            {
                matrix[coordinateX - 1, coordinateY - 1] = true;
                RemoveUsedElementsFromList(coordinateX - 1, coordinateY - 1);
            }

            // UpMid -1, 0
            if (MatrixFunctions.IsPositionInTheMatrix(coordinateX - 1, coordinateY))
            {
                matrix[coordinateX - 1, coordinateY] = true;
                RemoveUsedElementsFromList(coordinateX - 1, coordinateY);
            }

            // UpRight -1, +1
            if (MatrixFunctions.IsPositionInTheMatrix(coordinateX - 1, coordinateY + 1))
            {
                matrix[coordinateX - 1, coordinateY + 1] = true;
                RemoveUsedElementsFromList(coordinateX - 1, coordinateY + 1);
            }

            // MidLeft 0, -1
            if (MatrixFunctions.IsPositionInTheMatrix(coordinateX, coordinateY - 1))
            {
                matrix[coordinateX, coordinateY - 1] = true;
                RemoveUsedElementsFromList(coordinateX, coordinateY - 1);
            }

            // MidRight 0, +1
            if (MatrixFunctions.IsPositionInTheMatrix(coordinateX, coordinateY + 1))
            {
                matrix[coordinateX, coordinateY + 1] = true;
                RemoveUsedElementsFromList(coordinateX, coordinateY + 1);
            }

            // DownLeft +1 , -1
            if (MatrixFunctions.IsPositionInTheMatrix(coordinateX + 1, coordinateY - 1))
            {
                matrix[coordinateX + 1, coordinateY - 1] = true;
                RemoveUsedElementsFromList(coordinateX + 1, coordinateY - 1);
            }

            // DownMid +1, 0
            if (MatrixFunctions.IsPositionInTheMatrix(coordinateX + 1, coordinateY))
            {
                matrix[coordinateX + 1, coordinateY] = true;
                RemoveUsedElementsFromList(coordinateX + 1, coordinateY);
            }

            // DownRight +1, +1
            if (MatrixFunctions.IsPositionInTheMatrix(coordinateX + 1, coordinateY + 1))
            {
                matrix[coordinateX + 1, coordinateY + 1] = true;
                RemoveUsedElementsFromList(coordinateX + 1, coordinateY + 1);
            }
        }
    }
}