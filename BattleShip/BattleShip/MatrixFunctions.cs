namespace BattleShip
{
    public static class MatrixFunctions
    {
        public static bool IsPositionInTheMatrix(int coordinateX, int coordinateY)
        {
            if (coordinateX > 9 || coordinateX < 0 || coordinateY > 9 || coordinateY < 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}