namespace BattleShip
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Playground
    {
        private readonly StatementOfPositionInMatrix[,] matrix;
        private readonly List<int> matrixElements;

        public Playground()
        {
            this.matrix = new StatementOfPositionInMatrix[10, 10];
            this.matrixElements = new List<int>();

            for (int i = 0; i < 100; i++)
            {
                this.matrixElements.Add(i);
            }
        }

        public void ChangeMatrixStatment(string[] positions, StatementOfPositionInMatrix possitionStatment)
        {
            if (positions.Length <= 0)
            {
                throw new ArgumentException("Array with positions is empty!");
            }

            if (possitionStatment == StatementOfPositionInMatrix.Empty)
            {
                string[] coordinatesArr = positions[0].Split(' ');
                int coordinateX = int.Parse(coordinatesArr[0]);
                int coordinateY = int.Parse(coordinatesArr[1]);
                this.matrix[coordinateX, coordinateY] = StatementOfPositionInMatrix.Empty;
                this.RemoveUsedElementsFromList(coordinateX, coordinateY);
            }
            else if (possitionStatment == StatementOfPositionInMatrix.Hitted)
            {
                string[] coordinatesArr = positions[0].Split(' ');
                int coordinateX = int.Parse(coordinatesArr[0]);
                int coordinateY = int.Parse(coordinatesArr[1]);
                this.matrix[coordinateX, coordinateY] = StatementOfPositionInMatrix.Hitted;
                this.RemoveUsedElementsFromList(coordinateX, coordinateY);
            }
            else if (possitionStatment == StatementOfPositionInMatrix.Destroyed)
            {
                foreach (var position in positions)
                {
                    this.ChangeDestroyedPossitionAndTheNeighborhood(position);
                }
            }
        }

        public int GetCountOfMatrixElelemnts()
        {
            return this.matrixElements.Count();
        }

        public int GetElementForAttack(int index)
        {
            if (index < 0 || index >= this.matrixElements.Count)
            {
                throw new IndexOutOfRangeException("Index does not exist in available positions!");
            }

            int numberFromIndexPossition = this.matrixElements[index];
            this.matrixElements.RemoveAt(index);
            return numberFromIndexPossition;
        }

        public StatementOfPositionInMatrix[,] GetMatrixWithElements()
        {
            return this.matrix;
        }

        public bool IsPositionVisited(string coordinates)
        {
            string[] coordinatesArr = coordinates.Split(' ');
            int coordinateX = int.Parse(coordinatesArr[0]);
            int coordinateY = int.Parse(coordinatesArr[1]);
            if (this.matrix[coordinateX, coordinateY] == StatementOfPositionInMatrix.NotChecked)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void ChangeDestroyedPossitionAndTheNeighborhood(string coordinate)
        {
            string[] coordinatesArr = coordinate.Split(' ');
            int coordinateX = int.Parse(coordinatesArr[0]);
            int coordinateY = int.Parse(coordinatesArr[1]);
            this.matrix[coordinateX, coordinateY] = StatementOfPositionInMatrix.Destroyed;
            this.RemoveUsedElementsFromList(coordinateX, coordinateY);

            // UpLeft -1, -1
            if (MatrixFunctions.IsPositionInTheMatrix(coordinateX - 1, coordinateY - 1))
            {
                this.matrix[coordinateX - 1, coordinateY - 1] = StatementOfPositionInMatrix.Destroyed;
                this.RemoveUsedElementsFromList(coordinateX - 1, coordinateY - 1);
            }

            // UpMid -1, 0
            if (MatrixFunctions.IsPositionInTheMatrix(coordinateX - 1, coordinateY))
            {
                this.matrix[coordinateX - 1, coordinateY] = StatementOfPositionInMatrix.Destroyed;
                this.RemoveUsedElementsFromList(coordinateX - 1, coordinateY);
            }

            // UpRight -1, +1
            if (MatrixFunctions.IsPositionInTheMatrix(coordinateX - 1, coordinateY + 1))
            {
                this.matrix[coordinateX - 1, coordinateY + 1] = StatementOfPositionInMatrix.Destroyed;
                this.RemoveUsedElementsFromList(coordinateX - 1, coordinateY + 1);
            }

            // MidLeft 0, -1
            if (MatrixFunctions.IsPositionInTheMatrix(coordinateX, coordinateY - 1))
            {
                this.matrix[coordinateX, coordinateY - 1] = StatementOfPositionInMatrix.Destroyed;
                this.RemoveUsedElementsFromList(coordinateX, coordinateY - 1);
            }

            // MidRight 0, +1
            if (MatrixFunctions.IsPositionInTheMatrix(coordinateX, coordinateY + 1))
            {
                this.matrix[coordinateX, coordinateY + 1] = StatementOfPositionInMatrix.Destroyed;
                this.RemoveUsedElementsFromList(coordinateX, coordinateY + 1);
            }

            // DownLeft +1 , -1
            if (MatrixFunctions.IsPositionInTheMatrix(coordinateX + 1, coordinateY - 1))
            {
                this.matrix[coordinateX + 1, coordinateY - 1] = StatementOfPositionInMatrix.Destroyed;
                this.RemoveUsedElementsFromList(coordinateX + 1, coordinateY - 1);
            }

            // DownMid +1, 0
            if (MatrixFunctions.IsPositionInTheMatrix(coordinateX + 1, coordinateY))
            {
                this.matrix[coordinateX + 1, coordinateY] = StatementOfPositionInMatrix.Destroyed;
                this.RemoveUsedElementsFromList(coordinateX + 1, coordinateY);
            }

            // DownRight +1, +1
            if (MatrixFunctions.IsPositionInTheMatrix(coordinateX + 1, coordinateY + 1))
            {
                this.matrix[coordinateX + 1, coordinateY + 1] = StatementOfPositionInMatrix.Destroyed;
                this.RemoveUsedElementsFromList(coordinateX + 1, coordinateY + 1);
            }
        }

        private void RemoveUsedElementsFromList(int coordinateX, int coordinateY)
        {
            string numberAsStr = coordinateX.ToString() + coordinateY.ToString();
            int coordinateNumber = int.Parse(numberAsStr);
            int position = this.matrixElements.BinarySearch(coordinateNumber);
            if (position < 0)
            {
                return;
            }

            this.matrixElements.RemoveAt(position);
        }
    }
}