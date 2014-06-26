namespace BattleShip.ConsoleSquare
{
    public class EmptySquare : Square
    {
        public EmptySquare()
        {
            // could use other symbols for the rectangle 
            this.ChangeBodyOfSquare('╔', '╚', '╗', '╝');
        }
    }
}