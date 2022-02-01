namespace BattleShipApp
{
    public interface IShip: IPosition
    {        
        string Name { get; }
        int Radius { get; }
        bool IsShipPlacedInHorizontalDirection { get; }
        int HitCount { get; }
        bool IsShipSunk { get; }
        bool IsOverlapping(IShip[] existingShip);
        bool IsOverflowing();
        int[] OverlappingPoints();
        bool CheckForHit(IPosition position);
        Directions Directions { get; }
    }
}
