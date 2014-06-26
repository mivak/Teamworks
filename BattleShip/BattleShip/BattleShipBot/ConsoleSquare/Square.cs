namespace BattleShip.ConsoleSquare
{
    using System.Text;

    public class Square
    {
        private char[,] bodyOfSquare = new char[2, 2];

        public Square()
        {
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < this.bodyOfSquare.GetLength(0); i++)
            {
                for (int k = 0; k < this.bodyOfSquare.GetLength(1); k++)
                {
                    sb.Append(this.bodyOfSquare[i, k]);
                }

                if (i == 0)
                {
                    sb.AppendLine();
                }
            }

            return sb.ToString().Trim();
        }

        protected void ChangeBodyOfSquare(char leftUp, char leftDown, char rightUp, char rightDown)
        {
            this.bodyOfSquare[0, 0] = leftUp;
            this.bodyOfSquare[0, 1] = rightUp;
            this.bodyOfSquare[1, 0] = leftDown;
            this.bodyOfSquare[1, 1] = rightDown;
        }
    }
}