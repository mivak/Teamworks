namespace BattleShip.BattleShipBot
{
    using System;
    using System.Collections.Generic;

    public class Bot : IAttacker
    {
        private readonly Playground playground;
        private string lastAttackPosstion;
        private bool isShipFound;
        private BotForFoundedShip botForFoundedShip;

        public Bot()
        {
            this.isShipFound = false;
            this.playground = new Playground();
        }

        public string Attack()
        {
            string coordinate = string.Empty;
            if (this.isShipFound)
            {
                coordinate = this.botForFoundedShip.Attack();
            }
            else
            {
                coordinate = this.GetPossitionForAttack(this.playground.GetCountOfMatrixElelemnts());
            }

            this.lastAttackPosstion = coordinate;
            return coordinate;
        }

        public void GetReportForAttackPosition(Dictionary<string, StatementOfPositionInMatrix> returnStat)
        {
            foreach (var coordinate in returnStat)
            {
                if (returnStat[coordinate.Key] == StatementOfPositionInMatrix.Empty)
                {
                    if (this.isShipFound)
                    {
                        this.botForFoundedShip.GetReportForAttackPosition(returnStat);
                    }

                    this.playground.ChangeMatrixStatment(new string[] { coordinate.Key }, StatementOfPositionInMatrix.Empty);
                }
                else if (returnStat[coordinate.Key] == StatementOfPositionInMatrix.Hitted)
                {
                    if (!this.isShipFound)
                    {
                        this.botForFoundedShip = new BotForFoundedShip(coordinate.Key, this.playground);
                    }
                    else
                    {
                        this.botForFoundedShip.GetReportForAttackPosition(returnStat);
                    }

                    this.isShipFound = true;
                }
                else if (returnStat[coordinate.Key] == StatementOfPositionInMatrix.Destroyed)
                {
                    if (!this.isShipFound)
                    {
                        this.playground.ChangeMatrixStatment(new string[] { coordinate.Key }, StatementOfPositionInMatrix.Destroyed);
                    }
                    else
                    {
                        this.botForFoundedShip.GetReportForAttackPosition(returnStat);
                        List<string> hittedPosition = new List<string>();
                        foreach (var destroyedCoordinate in returnStat)
                        {
                            hittedPosition.Add(destroyedCoordinate.Key);
                        }

                        this.playground.ChangeMatrixStatment(hittedPosition.ToArray(), StatementOfPositionInMatrix.Destroyed);
                    }

                    this.isShipFound = false;
                }
            }
        }

        private string GetPossitionForAttack(int allElements)
        {
            Random rand = new Random();
            string coordinatesStr = string.Empty;

            int posstion = rand.Next(allElements);
            int coordinates = this.playground.GetElementForAttack(posstion);
            int col = coordinates % 10;
            int row = coordinates / 10;
            coordinatesStr = row + " " + col;

            return coordinatesStr;
        }
    }
}