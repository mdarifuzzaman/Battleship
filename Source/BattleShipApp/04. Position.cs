namespace BattleShipApp
{
    public class Position : IPosition
    {
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }
        public int X { get; }

        public int Y { get; }
    }
}
