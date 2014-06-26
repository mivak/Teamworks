namespace BattleShip.BattleShipBot
{
    using System;
    using System.Collections.Generic;

    public class BotForFoundedShip : IAttacker
    {
        private const int MaxShipSize = 4;
        private readonly int firstXCoordinate;
        private readonly int secondXCoordinate;
        private readonly Playground playground;
        private string lastAttackPosstion;
        private Queue<string> toLeft;
        private Queue<string> toRight;
        private Queue<string> toUp;
        private Queue<string> toDown;
        private SideForAttack sideForAttack;

        public BotForFoundedShip(string coordinates, Playground enemyPlayGroundFromBot)
        {
            this.lastAttackPosstion = coordinates;
            string[] coordinatesArr = coordinates.Split(' ');
            this.firstXCoordinate = int.Parse(coordinatesArr[0]);
            this.secondXCoordinate = int.Parse(coordinatesArr[1]);
            this.FullTheQueues(this.firstXCoordinate, this.secondXCoordinate);
            this.sideForAttack = SideForAttack.Left;
            this.playground = enemyPlayGroundFromBot;
        }

        public string Attack()
        {
            string posstion = string.Empty;

            if (this.sideForAttack == SideForAttack.Left)
            {
                if (this.AttackIsAvailable(this.toLeft))
                {
                    posstion = this.toLeft.Dequeue();
                }
                else
                {
                    this.sideForAttack = SideForAttack.Right;
                }
            }

            if (this.sideForAttack == SideForAttack.Right)
            {
                if (this.AttackIsAvailable(this.toRight))
                {
                    posstion = this.toRight.Dequeue();
                }
                else
                {
                    this.sideForAttack = SideForAttack.Up;
                }
            }

            if (this.sideForAttack == SideForAttack.Up)
            {
                if (this.AttackIsAvailable(this.toUp))
                {
                    posstion = this.toUp.Dequeue();
                }
                else
                {
                    this.sideForAttack = SideForAttack.Down;
                }
            }

            if (this.sideForAttack == SideForAttack.Down)
            {
                if (this.AttackIsAvailable(this.toDown))
                {
                    posstion = this.toDown.Dequeue();
                }
                else
                {
                    throw new ArgumentException("Problem in Method for Attack");
                }
            }

            this.lastAttackPosstion = posstion;
            return posstion;
        }

        public void GetReportForAttackPosition(Dictionary<string, StatementOfPositionInMatrix> returnStat)
        {
            foreach (var coordinate in returnStat)
            {
                if (returnStat[coordinate.Key] == StatementOfPositionInMatrix.Hitted)
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
                }
                else if (returnStat[coordinate.Key] == StatementOfPositionInMatrix.Empty)
                {
                    this.sideForAttack++;
                }
            }
        }

        private void FullTheQueues(int coordinateX, int coordinateY)
        {
            if (coordinateX < 0 || coordinateX > 9 || coordinateY < 0 || coordinateY > 9)
            {
                throw new IndexOutOfRangeException("Values of Coordinates are out of matrix size!");
            }

            this.toLeft = new Queue<string>();
            this.toRight = new Queue<string>();
            this.toUp = new Queue<string>();
            this.toDown = new Queue<string>();

            // attack to left
            for (int i = 1; i < MaxShipSize; i++)
            {
                if (coordinateX - i >= 0)
                {
                    int newXCoordinate = coordinateX - i;
                    string coordinate = newXCoordinate + " " + coordinateY;
                    this.toLeft.Enqueue(coordinate);
                }
                else
                {
                    break;
                }
            }

            // attack to right
            for (int i = 1; i < MaxShipSize; i++)
            {
                if (coordinateX + i <= 9)
                {
                    int newXCoordinate = coordinateX + i;
                    string coordinate = newXCoordinate + " " + coordinateY;
                    this.toRight.Enqueue(coordinate);
                }
                else
                {
                    break;
                }
            }

            // attack to down
            for (int i = 1; i < MaxShipSize; i++)
            {
                if (coordinateY + i <= 9)
                {
                    int newYCoordinate = coordinateY + i;
                    string coordinate = coordinateX + " " + newYCoordinate;
                    this.toDown.Enqueue(coordinate);
                }
                else
                {
                    break;
                }
            }

            // attack to up
            for (int i = 1; i < MaxShipSize; i++)
            {
                if (coordinateY - i >= 0)
                {
                    int newYCoordinate = coordinateY - i;
                    string coordinate = coordinateX + " " + newYCoordinate;
                    this.toUp.Enqueue(coordinate);
                }
                else
                {
                    break;
                }
            }
        }

        private bool AttackIsAvailable(Queue<string> queue)
        {
            if (queue.Count <= 0)
            {
                return false;
            }

            if (this.playground.IsPositionVisited(queue.Peek()))
            {
                return false;
            }

            return true;
        }
    }
}