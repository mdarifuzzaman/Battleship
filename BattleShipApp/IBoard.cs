using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShipApp
{
    public interface IBoard: IInitializeGame
    {
        string[,] TenByTenBoard { get; }
        bool CheckMeWinner(IBoard oppositionBoard);
        string Attack(IPosition position, IBoard oppositionBoard);
        void AddShip(IShip ship, int shipNumber);       
        IShip[] Ships { get; }
        int TotalShip { get; }
        bool CheckForHit(IPosition position);
    }
}
