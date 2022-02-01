namespace BattleShipApp
{
    public interface IBoardState: IInitializeGame
    {
        string[,] HitMissPosition { get; set; }
        int MissCount { get; set; }
        int HitCount { get; set; }
        string Player { get; set; }

    }
}
