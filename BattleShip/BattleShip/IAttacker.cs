namespace BattleShip
{
    using System.Collections.Generic;

    public interface IAttacker
    {
        string Attack();

        void GetReportForAttackPosition(Dictionary<string, StatementOfPositionInMatrix> returnStat);
    }
}